using System.Globalization;
using Microsoft.FluentUI.AspNetCore.Components;
using WebAdmin.Components;
using WebAdmin.Shared.Configurations;
using WebAdmin.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var adminUiOptions = new AdminUiOptions();

adminUiOptions.BindConfiguration(builder.Configuration);
builder.Services.AddSingleton(adminUiOptions);
builder.Services.AddSingleton(adminUiOptions.Admin);
builder.Services.AddSingleton(adminUiOptions.Culture);
builder.Services.AddSingleton(adminUiOptions.Http);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddLocalization(opts => opts.ResourcesPath = ConfigurationConsts.ResourcesPath);
builder.Services.AddAdminUiServerServices();
builder.Services.AddFluentUIComponents();
builder.Services.AddControllers();
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
app.MapControllers();
app.UseRequestLocalization(options =>
{
    var cultureConfiguration = adminUiOptions.Culture;
    var supportedCultureCodes =
        (cultureConfiguration?.Cultures?.Count > 0
            ? cultureConfiguration.Cultures.Intersect(CultureConfiguration.AvailableCultures)
            : CultureConfiguration.AvailableCultures).ToArray();
    if (!supportedCultureCodes.Any()) 
        supportedCultureCodes = CultureConfiguration.AvailableCultures;
    var defaultCultureCode = string.IsNullOrEmpty(cultureConfiguration?.DefaultCulture)
        ? CultureConfiguration.DefaultRequestCulture
        : cultureConfiguration?.DefaultCulture;
    if (!supportedCultureCodes.Contains(defaultCultureCode))
        defaultCultureCode = supportedCultureCodes.FirstOrDefault();

    options.AddSupportedCultures(supportedCultureCodes)
        .AddSupportedUICultures(supportedCultureCodes)
        .SetDefaultCulture(defaultCultureCode!);
});
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebAdmin.Client._Imports).Assembly);
app.Run();
