using ADO.NETCRUD.Helper;
using ADO.NETCRUD.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repo.IRepositoy;
using Repo.Repository;
using System.Security.Claims;
using System.Text;
using WebApi.IRepositoy;
using WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            //ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysceretasd23123123wqaS21312QE2SQAE12E12"))
        };
    });

builder.Services.AddAuthorization();

//{
//    option.AddPolicy("onlyAdmin", build =>
//    {
//        build.RequireRole("Admin");
//        build.RequireClaim(ClaimTypes.Name, "Admin");
//    });
//    option.AddPolicy("AdminOrTeacher", build =>
//    {
//        build.RequireRole(new string[] { "Admin", "Teacher" });
//    });
//    option.AddPolicy("AdminOrTeacherOrStudent", build =>
//    {
//        build.RequireRole(new string[] { "Admin", "Teacher", "Student" });
//    });
//}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStudenRepo,StudentRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<JwtSettings>();
builder.Services.AddScoped<PasswordHashing>();

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPloicy", builder =>
    {
        builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPloicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
