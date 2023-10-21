using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureDataProvider();
ConfigureRepositories();
ConfigureEntities();
ConfigureServices();
ConfigureJwt();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.MaxValue));

app.Run();

void ConfigureDataProvider()
{
    var connectionString = builder.Configuration.GetConnectionString("Task");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
}

void ConfigureRepositories()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();

    builder.Services.AddScoped<IWorkSpaceRepository, WorkSpaceRepository>();
    builder.Services.AddScoped<IWorkSpaceUserRepository, WorkSpaceUserRepository>();
    builder.Services.AddScoped<IWorkSpaceDeskRepository, WorkSpaceDeskRepository>();

    builder.Services.AddScoped<IDeskRepository, DeskRepository>();
    builder.Services.AddScoped<IDeskVisibilityTypeRepository, DeskVisibilityTypeRepository>();
}

void ConfigureEntities()
{
    builder.Services.AddTransient<IUser, UserEntity>();
    builder.Services.AddTransient<IUserRefreshToken, UserRefreshTokenEntity>();

    builder.Services.AddTransient<IWorkSpace, WorkSpaceEntity>();
    builder.Services.AddTransient<IWorkSpaceUser, WorkSpaceUserEntity>();
    builder.Services.AddTransient<IWorkSpaceDesk, WorkSpaceDeskEntity>();

    builder.Services.AddTransient<IDesk, DeskEntity>();
    builder.Services.AddTransient<IDeskVisibilityType, DeskVisibilityTypeEntity>();

}

void ConfigureServices() 
{
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IWorkSpaceService, WorkSpaceService>();
    builder.Services.AddScoped<IDeskService, DeskService>();
}

void ConfigureJwt() 
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
        };
    });
}