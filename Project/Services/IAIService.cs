namespace Project.Services
{
    public interface IAIService
    {
        Task<string> GetFitnessChatResponseAsync(string userMessage, float weight, float height, int age);
        Task<String> GetExerciseRecommendationsAsync(float weight, float height, int age, string fitnessGoal, string fitnessLevel);

        Task<String> GenerateMotivationalMessageAsync(string userName, float weight, float height, int age, int daysSinceRegistration);
    }
}
