using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Project.Services
{
    public class AIService : IAIService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _model;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public AIService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;

            _apiKey = _configuration["OpenRouterSettings:ApiKey"] ?? "";
            _baseUrl = _configuration["OpenRouterSettings:BaseUrl"] ?? "https://openrouter.ai/api/v1";
            _model = _configuration["OpenRouterSettings:Model"] ?? "meta-llama/llama-3.2-3b-instruct:free";
        }

        public async Task<string> GetFitnessChatResponseAsync(string userMessage, float weight, float height, int age)
        {
            try
            {
                float bmi = weight / ((height / 100) * (height / 100));
                string bmiCategory = GetBmiCategory(bmi);

                var systemPrompt = $@"You are CoreX Coach, a friendly and knowledgeable AI fitness coach for CoreX Fitness app.

User's Physical Stats:
- Weight: {weight} kg
- Height: {height} cm
- Age: {age} years
- BMI: {bmi:F1} ({bmiCategory})

Guidelines:
1. Only answer fitness, health, nutrition, and exercise-related questions.
2. If asked about non-fitness topics, politely redirect to fitness topics.
3. Personalize advice based on the user's physical stats when relevant.
4. Keep responses concise (under 200 words).
5. Be encouraging and supportive.
6. Always recommend consulting a healthcare professional for medical concerns.
7. Do not provide specific medical diagnoses or treatment plans.";

                return await SendChatRequestAsync(systemPrompt, userMessage);
            }
            catch (Exception ex)
            {
                return $"I'm sorry, I'm having trouble responding right now. Please try again later. Error: {ex.Message}";
            }
        }

        public async Task<string> GetExerciseRecommendationsAsync(float weight, float height, int age, string fitnessGoal, string fitnessLevel)
        {
            try
            {
                float bmi = weight / ((height / 100) * (height / 100));
                string bmiCategory = GetBmiCategory(bmi);

                var systemPrompt = $@"You are an expert fitness trainer creating personalized exercise recommendations.

User Profile:
- Weight: {weight} kg
- Height: {height} cm
- Age: {age} years
- BMI: {bmi:F1} ({bmiCategory})
- Fitness Goal: {fitnessGoal}
- Fitness Level: {fitnessLevel}

Create a JSON response with exactly this structure (no markdown, just pure JSON):
{{
  ""fitnessGoal"": ""{fitnessGoal}"",
  ""fitnessLevel"": ""{fitnessLevel}"",
  ""exercises"": [
    {{
      ""name"": ""Exercise Name"",
      ""description"": ""How to perform the exercise"",
      ""muscleGroup"": ""Target muscles"",
      ""sets"": 3,
      ""reps"": 10,
      ""difficulty"": ""Easy/Medium/Hard""
    }}
  ],
  ""additionalTips"": ""General advice for the user"",
  ""weeklyFrequency"": ""How many times per week"",
  ""estimatedDuration"": ""Duration per session""
}}

Guidelines:
1. Recommend 5-7 exercises appropriate for the user's level and goal.
2. Consider BMI when recommending exercises (avoid high-impact for overweight users).
3. Include warm-up and cool-down suggestions in tips.
4. Be specific with sets, reps, and form cues.
5. For beginners, focus on bodyweight exercises.
6. For weight loss, include cardio elements.
7. For muscle gain, focus on progressive overload principles.";

                var userMessage = $"Create an exercise plan for my goal: {fitnessGoal}, level: {fitnessLevel}";
                return await SendChatRequestAsync(systemPrompt, userMessage);
            }
            catch (Exception ex)
            {
                return $"{{\"error\": \"Unable to generate recommendations: {ex.Message}\"}}";
            }
        }

        public async Task<string> GenerateMotivationalMessageAsync(string userName, float weight, float height, int age, int daysSinceRegistration)
        {
            try
            {
                float bmi = weight / ((height / 100) * (height / 100));
                string bmiCategory = GetBmiCategory(bmi);

                var systemPrompt = $@"You are a motivational fitness coach writing a personalized email message.

User Profile:
- Name: {userName}
- Age: {age} years
- BMI Category: {bmiCategory}
- Member for: {daysSinceRegistration} days

Write a short, personalized motivational message (100-150 words) that:
1. Addresses the user by their first name
2. Acknowledges their fitness journey duration
3. Provides encouragement based on their profile
4. Includes one practical fitness tip
5. Uses an uplifting and supportive tone
6. Uses simple HTML formatting (use <p>, <strong>, <em> tags)
7. Does NOT include a greeting or signature (those will be added separately)

Make it personal and inspiring!";

                return await SendChatRequestAsync(systemPrompt, "Generate a motivational message for this user");
            }
            catch (Exception ex)
            {
                return $"<p>Keep pushing towards your fitness goals! Every step counts on your journey to a healthier you.</p><p><em>Error generating personalized message: {ex.Message}</em></p>";
            }
        }

        private async Task<string> SendChatRequestAsync(string systemPrompt, string userMessage)
        {
            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userMessage }
                },
                max_tokens = 1000
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Create HttpClient from factory for each request
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://corex-fitness.com");
            httpClient.DefaultRequestHeaders.Add("X-Title", "CoreX Fitness");

            // Use full URL to avoid BaseAddress issues
            var fullUrl = $"{_baseUrl.TrimEnd('/')}/chat/completions";
            var response = await httpClient.PostAsync(fullUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Check if response is HTML (error page) instead of JSON
            if (responseString.TrimStart().StartsWith("<"))
            {
                throw new Exception($"API returned HTML instead of JSON. Status: {response.StatusCode}. This usually means invalid API key or no credits. Response preview: {responseString.Substring(0, Math.Min(200, responseString.Length))}");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API request failed: {response.StatusCode} - {responseString}");
            }

            using var doc = JsonDocument.Parse(responseString);
            
            // Check if there's an error in the JSON response
            if (doc.RootElement.TryGetProperty("error", out var errorElement))
            {
                var errorMessage = errorElement.TryGetProperty("message", out var msgElement) 
                    ? msgElement.GetString() 
                    : errorElement.ToString();
                throw new Exception($"API Error: {errorMessage}");
            }

            var messageContent = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return messageContent ?? "No response generated :(";
        }

        private string GetBmiCategory(float bmi)
        {
            return bmi switch
            {
                < 18.5f => "Underweight",
                < 25f => "Normal weight",
                < 30f => "Overweight",
                _ => "Obese"
            };
        }
    }
}