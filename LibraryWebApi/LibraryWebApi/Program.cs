using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Library.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(  /*options => 
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
                TokenUrl = new Uri("https://localhost:10001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"SwaggerAPI", "Swagger API DEMO"}
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

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddIdentity<LibraryUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
})
    .AddEntityFrameworkStores<ApplicationDBContext>();
    //.AddDefaultTokenProviders();

/*builder.Services.AddIdentityServer()
    .AddAspNetIdentity<LibraryUser>();*/

/*////////////////////////don't
builder.Services.AddAuthentication(options =>{
    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
}).AddIdentityServerAuthentication(options =>{
    options.ApiName = "SwaggerAPI";
    options.Authority = "https://localhost:10001";
    options.RequireHttpsMetadata = false;
}); 
////////////////////////don't*/

builder.Services.AddAuthorization();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorBookRepository, AuthorBookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(/*options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Demo");
        options.DocumentTitle = "Title";
        options.DocExpansion(DocExpansion.List);
        options.OAuthClientId("client_id_swagger");
        options.OAuthScopeSeparator(" ");
        options.OAuthClientSecret("client_secret_swagger");
    }*/);
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
