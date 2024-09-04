using victors.Models.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tailwind;
using Microsoft.AspNetCore.Identity;
using victors.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using NuGet.Protocol;
using victors.Factory;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Add services to the container.
//Using Mysql
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
//Using Msql server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
            builder.AllowAnyOrigin();
        });
});
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5000",
        ValidAudience = "https://localhost:5000",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretoftheuniverse"))
    };
});

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 4;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
    
   

})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/Authentication/Login");
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
            //pattern: "{controller=Students}/{action=GetStudents}/{id?}");
            //pattern: "{controller=Home}/{action=Index}/{id?}");

pattern: "{controller=Login}/{action=Login}/{id?}");

if (app.Environment.IsDevelopment())
{
    _ = app.RunTailwind("tailwind", "./");
}

app.Run();
