using HospitalSystemTeamTask.Components;
using HospitalSystemTeamTask.Helper;
using HospitalSystemTeamTask.Repositories;
using HospitalSystemTeamTask.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace HospitalSystemTeamTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Add services to the container.

            builder.Services.AddScoped<IBookingRepo, BookingRepo>();
            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddScoped<IClinicRepocs, ClinicRepo>();
            builder.Services.AddScoped<IClinicService, ClinicService>();

            builder.Services.AddScoped<IPatientRecordRepository, PatientRecordRepository>();
            builder.Services.AddScoped<IPatientRecordService, PatientRecordService>();


            builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();

            builder.Services.AddScoped<IBranchDepartmentRepo, BranchDepartmentRepo>();
            builder.Services.AddScoped<IBranchDepartmentService, BranchDepartmentService>();

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();


            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IBranchService, BranchService>();

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IUserService, UserService>();

            //SMS service
            builder.Services.AddScoped<ISMS, SMS>();


            builder.Services.AddScoped<ISendEmail, SendEmail>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
        

            builder.Services.AddMudServices();


            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

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
