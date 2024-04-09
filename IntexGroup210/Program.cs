using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML.OnnxRuntime;
using IntexGroup210.Models;
using Microsoft.EntityFrameworkCore;
namespace Zoo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load the ONNX model
            var session = LoadModel();
            CreateHostBuilder(args, session).Build().Run();
        }
        private static InferenceSession LoadModel()
        {
            try
            {
                return new InferenceSession("/Users/daxtonjergensen/Downloads/ASP.NET-ML-ONNX-Deployment/decision_tree_model.onnx");
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine($"Error loading the ONNX model: {ex.Message}");
                return null;
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args, InferenceSession session) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSingleton(session); // Register the session as a singleton
                        // Register ZooContext with the DI container
                        services.AddDbContext<IntexContext>(options =>
                            options.UseSqlite("Data Source=Transactions.db"));
                    })
                    .Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }
                        app.UseHttpsRedirection();
                        app.UseStaticFiles();
                        app.UseRouting();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                        });
                    });
                });
    }
}