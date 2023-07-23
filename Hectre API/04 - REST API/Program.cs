
using Microsoft.EntityFrameworkCore;

namespace Hectre
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure the IWebHostEnvironment
            builder.Services.AddSingleton<IWebHostEnvironment>(env => builder.Environment);

            JwtHelper jwtHelper = new JwtHelper(builder.Configuration.GetValue<string>("JWT:Key"));

            builder.Services.AddSingleton(jwtHelper);

            //Enable JWT authentication
            builder.Services.AddAuthentication(options => jwtHelper.SetAuthenticationOptions(options)).AddJwtBearer(options => jwtHelper.SetBearerOptions(options));

            builder.Services.AddScoped<HarvestsLogic>(); // Change the lifetime to scoped
            builder.Services.AddScoped<OrchardsLogic>();
            builder.Services.AddScoped<TimesheetsLogic>();
            builder.Services.AddScoped<UserLogic>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.MapControllers();

            app.Run();
        }
    }
}