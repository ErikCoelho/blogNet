using BlogNet.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
ConfigureMvc(builder);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("DevPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<BlogDataContext>(
        options =>
            options.UseSqlServer(connectionString));

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "https://securetoken.google.com/blog-728ed";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "https://securetoken.google.com/blog-728ed",
                ValidateAudience = true,
                ValidAudience = "blog-728ed",
                ValidateLifetime = true
            };
        });
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}
