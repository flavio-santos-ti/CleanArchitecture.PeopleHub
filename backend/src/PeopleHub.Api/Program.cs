using Asp.Versioning;
using Microsoft.AspNetCore.HttpOverrides;
using PeopleHub.Configuration.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configura a AppConfig Layer
builder.Services.AddAppConfigServices(builder.Configuration);

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders(); 
    logging.AddConsole(); 
    logging.SetMinimumLevel(LogLevel.Debug); 
});

// Adds support for API versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddMvc();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
