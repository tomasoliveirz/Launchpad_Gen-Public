using Moongy.RD.Launchpad.Tools.Aissistant;
using Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opts =>
  {
    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    opts.SerializerSettings.ContractResolver  = new CamelCasePropertyNamesContractResolver();
  });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var aissistantOptions = builder.Configuration.GetSection(nameof(Aissistant));
builder.Services.Configure<AissistantOptions>(aissistantOptions);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAissistant, Aissistant>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "Launchpad"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
