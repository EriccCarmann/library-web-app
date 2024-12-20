using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FluentValidation;
using Library.Infrastructure.UnitOfWork;
using LibraryWebApi.ExceptionHandlerMiddleware;
using Library.Application.Profiles.BookProfiles;
using Library.Application.Profiles.AuthorProfiles;
using Library.Application.Validators;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Library.Application.UseCases.BookUseCases;
using Library.Application.UseCases.AuthorUseCases;
using Library.Application.UseCases.AccountUseCases;
using Swashbuckle.AspNetCore.Filters;
using Library.Infrastructure.TokenServices;

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

#region UseCases
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<LogoutUseCase>();
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddScoped<RefreshTokensUseCase>();

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
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Authorization. Bearer Scheme (\"bearer {token}\")",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
#endregion

#region DB and Identity
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<DBConfig>();

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("vaWE8WuA19cleeg2RhHLB7qp8wsSpUTVGgbjq6AhIcPELx42jm8feBUH5c7m5oc7")),
            ValidateIssuer = false,
            ValidateAudience = false,
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

builder.Services.AddScoped<IGenerateToken, GenerateToken>();
builder.Services.AddScoped<IGenerateRefreshToken, GenerateRefreshToken>();

builder.Services.AddHttpContextAccessor();
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

app.UseCookiePolicy();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
