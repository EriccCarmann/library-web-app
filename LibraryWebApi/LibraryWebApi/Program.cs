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
using Library.Infrastructure.UnitOfWork;
using LibraryWebApi.ExceptionHandlerMiddleware;
using LibraryWebApi.Services;
using Library.Application.Profiles.BookProfiles;
using Library.Application.Profiles.AuthorProfiles;
using Library.Application.Validators;
using Library.Application.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Library.Application.UseCases.BookUseCases;
using Library.Application.UseCases.AuthorUseCases;
using Library.Application.UseCases.AccountUseCases;

var builder = WebApplication.CreateBuilder(args);

#region Exceptions
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<BookCreateProfile>();
    config.AddProfile<BookReadProfile>();
    config.AddProfile<AuthorCreateProfile>();
    config.AddProfile<AuthorReadProfile>();
});
#endregion

#region Repository
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Validation
builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LibraryUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AuthorValidator>();
#endregion

#region Services
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<AccountService>();
#endregion

#region UseCases
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<LogoutUseCase>();
builder.Services.AddScoped<RegisterUseCase>();

builder.Services.AddScoped<CreateAuthorUseCase>();
builder.Services.AddScoped<DeleteAuthorUseCase>();
builder.Services.AddScoped<FindAuthorByNameUseCase>();
builder.Services.AddScoped<GetAllAuthorsUseCase>();
builder.Services.AddScoped<GetAuthorByIdUseCase>();
builder.Services.AddScoped<UpdateAuthorUseCase>();

builder.Services.AddScoped<AddCoverUseCase>();
builder.Services.AddScoped<CreateBookUseCase>();
builder.Services.AddScoped<DeleteBookUseCase>();
builder.Services.AddScoped<GetAllBooksUseCase>();
builder.Services.AddScoped<GetBookByIdUseCase>();
builder.Services.AddScoped<GetBookByISBNUseCase>();
builder.Services.AddScoped<GetTakenBooksUseCase>();
builder.Services.AddScoped<ReturnBookUseCase>();
builder.Services.AddScoped<TakeBookUseCase>();
builder.Services.AddScoped<UpdateBookUseCase>();
#endregion

#region API
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer [jwt]'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, Array.Empty<string>() } });
});
#endregion

#region DB and Identity
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
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
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

/*builder.Services.AddIdentityServer()
    .AddAspNetIdentity<LibraryUser>()
    .AddInMemoryApiResources(Configuration.GetApiResources())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddInMemoryClients(Configuration.GetClients())
    .AddInMemoryApiScopes(Configuration.GetApiScopes())
    .AddDeveloperSigningCredential();*/

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecuritykeyfortokenssymmetric")),
            ClockSkew = new TimeSpan(0, 0, 5)
        };
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
    app.UseSwaggerUI();
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
