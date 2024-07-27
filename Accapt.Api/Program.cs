using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region DbContext

builder.Services.AddDbContext<AccaptFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionDB")));

#endregion


#region IOC

builder.Services.AddTransient<ICallApiServies, CallApiServies>();
builder.Services.AddTransient<IRegisterUserServies, RegisterUserServies>();
builder.Services.AddTransient<IApiCallServies, ApiCallServies>();
builder.Services.AddTransient<IFindUserServies, FindeUserServies>();
builder.Services.AddTransient<ILoginUserServies, LoginUserServies>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
