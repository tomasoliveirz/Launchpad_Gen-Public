using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Business.BusinessObjects;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.LaunchPad.DataAccess;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;
using Scalar.AspNetCore;
using WebApi.DataSeed;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                       builder.Configuration.GetConnectionString("LaunchpadDb");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IGenericDataAccessObject, GenericDataAccessObject>();
builder.Services.AddScoped<IContractTypeDataAccessObject, ContractTypeDataAccessObject>();
builder.Services.AddScoped<IBlockchainNetworkDataAccessObject, BlockchainNetworkDataAccessObject>();
builder.Services.AddScoped<ICharacteristicInContractVariantDataAccessObject, CharacteristicInContractVariantDataAccessObject>();
builder.Services.AddScoped<IContractCharacteristicDataAccessObject, ContractCharacteristicDataAccessObject>();
builder.Services.AddScoped<IContractFeatureDataAccessObject, ContractFeatureDataAccessObject>();
builder.Services.AddScoped<IContractGenerationResultDataAccessObject, ContractGenerationResultDataAccessObject>();
builder.Services.AddScoped<IContractVariantDataAccessObject, ContractVariantDataAccessObject>();
builder.Services.AddScoped<IPublishResultDataAccessObject, PublishResultDataAccessObject>();
builder.Services.AddScoped<IFeatureInContractTypeDataAccessObject, FeatureInContractTypeDataAccessObject>();
builder.Services.AddScoped<IGenerationResultFeatureValueDataAccessObject, GenerationResultFeatureValueDataAccessObject>();

builder.Services.AddScoped<IContractTypeBusinessObject, ContractTypeBusinessObject>();
builder.Services.AddScoped<IBlockchainNetworkBusinessObject, BlockchainNetworkBusinessObject>();
builder.Services.AddScoped<ICharacteristicInContractVariantBusinessObject, CharacteristicInContractVariantBusinessObject>();
builder.Services.AddScoped<IContractCharacteristicBusinessObject, ContractCharacteristicBusinessObject>();
builder.Services.AddScoped<IContractFeatureBusinessObject, ContractFeatureBusinessObject>();
builder.Services.AddScoped<IContractGenerationResultBusinessObject, ContractGenerationResultBusinessObject>();
builder.Services.AddScoped<IContractVariantBusinessObject, ContractVariantBusinessObject>();
builder.Services.AddScoped<IPublishResultBusinessObject, PublishResultBusinessObject>();
builder.Services.AddScoped<IFeatureInContractTypeBusinessObject, FeatureInContractTypeBusinessObject>();
builder.Services.AddScoped<IGenerationResultFeatureValueBusinessObject, GenerationResultFeatureValueBusinessObject>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<LaunchpadContext>(opts =>
    opts.UseSqlServer(connectionString)
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<LaunchpadContext>();
    await DataSeeder.Seed(context);

    app.MapOpenApi();
    app.MapScalarApiReference(o =>
    {
        o.WithTheme(ScalarTheme.Default);
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
