// 1. PROGRAM.CS - Session ayarlar�n� kontrol et
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Session ayarlar� (MUTLAK� OLMALI!)
builder.Services.AddDistributedMemoryCache(); // ? BU SATIRLAR EKS�KSE SORUN BURADA!
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // ? �NEML�: GDPR bypass i�in
    options.Cookie.Name = "MyApp.Session";
});

var app = builder.Build();

// Pipeline s�ras� �OK �NEML�!
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // ? BU UseRouting()'den SONRA, UseAuthorization()'dan �NCE olmal�!

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();