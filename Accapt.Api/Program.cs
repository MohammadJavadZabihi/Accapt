using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//dontForgot to use this for patch :)
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;

}).AddNewtonsoftJson();

#region DbContext

builder.Services.AddDbContext<AccaptFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionDB")));

#endregion

#region Jwt

var key = Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(oprions =>
{
    oprions.RequireHttpsMetadata = false;
    oprions.SaveToken = true;
    oprions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"]
    };
});

#endregion

#region IOC

builder.Services.AddTransient<IRegisterUserServies, RegisterUserServies>();
builder.Services.AddTransient<IFindUserServies, FindeUserServies>();
builder.Services.AddTransient<ILoginUserServies, LoginUserServies>();
builder.Services.AddTransient<IUserServies, UserServies>();
builder.Services.AddTransient<IAuthenticationJwtServies, AuthenticationJwtServies>();
builder.Services.AddTransient<IProductServies, ProductServies>();
builder.Services.AddTransient<IFindeProductServies, FindeProductServies>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
