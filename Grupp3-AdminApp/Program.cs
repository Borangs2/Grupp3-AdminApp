using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Azure.Identity;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString");
builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["KeyVaultUri"]), new DefaultAzureCredential());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddTransient<IElevatorService, ElevatorService>();
builder.Services.AddTransient<IErrandService, ErrandService>();
builder.Services.AddTransient<ITechnicianService, TechnicianService>();
builder.Services.AddTransient<IErrandCommentService, ErrandCommentService>();
builder.Services.AddMudServices();

builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = false,
    PositionClass = ToastPositions.BottomRight
});

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();