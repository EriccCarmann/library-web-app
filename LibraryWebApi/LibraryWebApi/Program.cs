using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using LibraryWebApi;
using LibraryWebApi.Validators;
using LibraryWebApi.Profiles;
using Library.Domain.Interfaces.UnitOfWork;
using Library.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<BookProfile>();
    config.AddProfile<AuthorProfile>();
});

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGenericRepository<LibraryUser>, GenericRepository<LibraryUser>>();

#endregion

#region Validation
builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AuthorValidator>();
#endregion

#region 
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(/*options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Demo Swagger API v1",
        Title = "Swagger with IdentityServer4",
        Version = "1.0.0"
    });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:5233/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"LibraryWebApi", "Web API"}
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
}*/);
#endregion

#region DB and Identity
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = "oidc";
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect("oidc", config =>
    {
        config.Authority = "https://localhost:7076";
        config.ClientId = "client_id_cf";
        config.ClientSecret = "client_secret_cf";
        config.SaveTokens = true;

        config.ResponseType = "code";

        config.GetClaimsFromUserInfoEndpoint = true;
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Admin");
    });

    opt.AddPolicy("User", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "User")
                                      || x.User.HasClaim(ClaimTypes.Role, "Admin"));
    });
});

builder.Services.AddIdentity<LibraryUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<LibraryUser>()
    .AddInMemoryApiResources(Configuration.GetApiResources())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddInMemoryClients(Configuration.GetClients())
    .AddInMemoryApiScopes(Configuration.GetApiScopes())
    .AddDeveloperSigningCredential();
#endregion

#region Authentication and Authorization

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Library.Identity.Cookie";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddHttpClient();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(/*options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Demo");
        options.OAuthClientId("client_id_swagger");
        options.DocExpansion(DocExpansion.List);
        options.OAuthScopeSeparator(" ");
        options.OAuthClientSecret("client_secret_swagger");
    }*/);
}

app.UseExceptionHandler();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();

app.UseCookiePolicy();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
