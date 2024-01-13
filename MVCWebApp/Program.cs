using MVCWebApp;

var builder = WebApplication.CreateBuilder(args);

// Register services
ServiceConfigurations.ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error").UseHsts();

app.UseHttpsRedirection()
.UseStaticFiles()
.UseRouting()
.UseAuthentication()
.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
