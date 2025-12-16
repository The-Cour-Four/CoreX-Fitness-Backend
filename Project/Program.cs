using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Rate limiting configuration.
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests; //setting the error code and message 249 for too many request sending.

    //fixed policy, allows 10 requests every one minute.
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 10;
        limiterOptions.Window = TimeSpan.FromSeconds(60);
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 2;
    });
}
);
//useless as now we have deployed our work. No longer in a localhost server.
//var frontEndOrigin = "http://localhost:5173";

// Policy for Local Development
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalDev", policy => {
//        policy.WithOrigins(frontEndOrigin)
//        .AllowAnyHeader()
//        .AllowCredentials()
//        .AllowAnyMethod();
//    });
//});

// Policy for Azure/Production
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("https://the-cour-four.github.io",  //our hosted frontend
                                    "http://127.0.0.1:5500",            //VS Code Live Server (common port)
                                    "http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// --- FIX 1: SWAGGER MUST BE OUTSIDE THE 'IF' BLOCK ---
// This ensures it runs on Azure (Production)
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // You can keep dev-only settings here
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors(myAllowSpecificOrigins);
// app.UseCors("AllowLocalDev"); //swapping this if testing locally

app.UseRateLimiter();

app.UseAuthorization();


app.UseAuthorization();


//bug: earlier the ratelimiting was not working due to: app.MapControllers(); exists only.
app.MapControllers().RequireRateLimiting("fixed");



app.Run();
