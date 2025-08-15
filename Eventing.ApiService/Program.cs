using System.Text;
using Eventing.ApiService.ConfigurationSettings;
using Eventing.ApiService.Data.Entities;
using Eventing.ApiService.Data.Entities.Seeders;
using Eventing.ApiService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<EventingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
/*
builder.AddNpgsqlDbContext<DbContext>("eventing-db", configureDbContextOptions: options =>
{
    options.UseAsyncSeeding(async (DbContext, _, CancellationToken) =>
        await UserSeeder.SeedAsync(context, cancellationToken);
        await EventSeeder.SeedAsync(context,cancellationToken);
    )
};*/
builder.Services.AddRazorPages();


var jwtSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSection);

var jwtSettings = jwtSection.Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.Key);


builder.Services.AddIdentity<Users, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<EventingDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        )
    .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };
        }
        );

builder.Services.AddScoped<JwtTokenService>();



var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();

    const string scalarUiPath = "/api-reference";
    
    app.MapPost("/migrate-db", (EventingDbContext dbContext) => dbContext.Database.MigrateAsync());

    app.MapScalarApiReference(scalarUiPath,
        options => options
            .WithTitle("Eventing Api Reference")
            .WithFavicon("https://scalar.com/logo-light.svg")
            .WithTheme(ScalarTheme.DeepSpace)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient));
    app.MapGet("/", () => Results.Redirect(scalarUiPath, permanent: true))
        .ExcludeFromDescription();
}

//middleware
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<Users>();

app.MapControllers();
app.MapRazorPages();
app.Run();