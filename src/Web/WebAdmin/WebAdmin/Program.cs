using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.FluentUI.AspNetCore.Components;
using WebAdmin.Components;
using WebAdmin.Components.Shared;
using WebAdmin.Configurations;
using WebAdmin.Infrastructure;
using _Imports = WebAdmin.Client._Imports;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.

var adminUiOptions = new AdminUiOptions();
adminUiOptions.BindConfiguration(builder.Configuration);

builder.Services.AddSingleton(adminUiOptions);
builder.Services.AddSingleton(adminUiOptions.Admin);
builder.Services.AddSingleton(adminUiOptions.Culture);
builder.Services.AddSingleton(adminUiOptions.Http);


builder.Services.AddScoped<CacheStorageAccessor>();
builder.Services.AddSingleton<NavProvider>();
builder.Services.AddSingleton<IAppVersionService, AppVersionService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddFluentUIComponents();
builder.Services.AddOrderAppDataConnection(builder.Configuration);

builder.Services.AddLocalization(opt => opt.ResourcesPath = ConfigurationConsts.ResourcesPath);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", true);
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

    if (supportedCultureCodes.Length == 0) supportedCultureCodes = CultureConfiguration.AvailableCultures;

    var defaultCultureCode = string.IsNullOrEmpty(cultureConfiguration!.DefaultCulture)
        ? CultureConfiguration.DefaultRequestCulture
        : cultureConfiguration.DefaultCulture;

    if (!supportedCultureCodes.Contains(defaultCultureCode))
        defaultCultureCode = supportedCultureCodes.FirstOrDefault();

    options
        .SetDefaultCulture(defaultCultureCode!)
        .AddSupportedCultures(supportedCultureCodes)
        .AddSupportedUICultures(supportedCultureCodes);
});
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(_Imports).Assembly);

app.Run();