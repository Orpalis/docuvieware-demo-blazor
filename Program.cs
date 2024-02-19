using Blazored.Toast;
using DocuVieware.Blazor.Sample.Components;
using GdPicture14.WEB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddDocuVieware(config =>
{
    config.LicenseKey = builder.Configuration.GetValue("DocuVieware:LicenseKey", "DEMO_PSPDFKIT_LICENSE");
    config.CacheDirectory = builder.Configuration.GetValue("DocuVieware:CachePath", "/tmp/docuvieware");
    config.SessionMode = DocuViewareSessionStateMode.InProc;
});
builder.Services.AddAntiforgery();
builder.Services.AddRouting();
builder.Services.AddBlazoredToast();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.UseDocuVieware();

app.Run();