using Repository;
using Repository.Options;
using Service;
using Web.ExceptionHandlers;

namespace Web;

public class Startup(IConfiguration configuration, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransient<ApplicationContext>();
        services.AddTransient<ISurveyRepository, SurveyRepository>();
        services.AddTransient<ISurveyService, SurveyService>();

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();

        services.Configure<DbConnectionOptions>(configuration.GetSection("DbConnectionSettings"));
    }

    public void Configure(IApplicationBuilder app)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        //app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
    }
}