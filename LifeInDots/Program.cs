using AspNetCoreRateLimit;
using LifeInDots.Middleware;
using LifeInDots.Services;
using LifeInDots.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
builder.Services.AddSingleton<SvgImageGenerator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add rate limiting services
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Running in Development mode");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();

// Apply rate limiting after key is checked
app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
