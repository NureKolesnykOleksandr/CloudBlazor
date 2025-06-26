using CloudBlazor.Components;
using CloudBlazor.Interfaces;
using CloudBlazor.Repositories;
using Syncfusion.Blazor;

namespace CloudBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense
            (
            "Ngo9BigBOggjHTQxAR8/V1NNaF1cWWhOYVJzWmFZfVtgc19GaFZURGYuP1ZhSXxWdkNiXH9ZcHxQQmhVWEZ9XUs="
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
