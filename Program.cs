using System.Formats.Tar;
using System.Text;
using BackOffice;
using BackOffice.Interfaces;
using BackOffice.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{

    // hash, simetrik asimetrik
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = "http://localhost",
        ValidAudience = "http://localhost",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YavuzyavuzyavuzA.YavuzyavuzyavuzAYavuzyavuzyavuzA.YavuzyavuzyavuzA"))
    };
});

builder.Services.AddDbContext<ReactDbContext>(opt=>{
    opt.UseSqlServer("server=.; database=ReactBootcampDb2; user id=sa; password=<YourStrong@Passw0rd>;TrustServerCertificate=true;");
});
builder.Services.AddScoped<IRepo,UserEfRepo>();

// builder.Services.AddScoped<IRepo, UserRepo>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

//swagger açıkla
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//pipeline order


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
