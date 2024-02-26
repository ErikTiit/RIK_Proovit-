using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers(); // Add this line

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Add this line
});

app.Run();