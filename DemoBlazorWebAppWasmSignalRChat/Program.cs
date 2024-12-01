using DemoBlazorWebAppWasmSignalRChat.ChatHubs;
using DemoBlazorWebAppWasmSignalRChat.Client.Pages;
using DemoBlazorWebAppWasmSignalRChat.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Đối với AddDbContext


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<ChatAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DemoBlazorWebAppWasmSignalRChat.Client._Imports).Assembly);


app.MapHub<ChatHub>("/chathub");
app.Run();
