// 1. PROGRAM.CS - Session ayarlarýný kontrol et
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Session ayarlarý (MUTLAKÝ OLMALI!)
builder.Services.AddDistributedMemoryCache(); // ? BU SATIRLAR EKSÝKSE SORUN BURADA!
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // ? ÖNEMLÝ: GDPR bypass için
    options.Cookie.Name = "MyApp.Session";
});

var app = builder.Build();

// Pipeline sýrasý ÇOK ÖNEMLÝ!
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // ? BU UseRouting()'den SONRA, UseAuthorization()'dan ÖNCE olmalý!

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();