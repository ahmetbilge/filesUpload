using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private static readonly List<User> Users = new()
    {
        new User { Username = "ahmet", Password = "123456" },
        new User { Username = "mehmet", Password = "123456" }
    };

    public IActionResult Login()
    {
        // Debug: Session durumunu kontrol et
        ViewBag.SessionId = HttpContext.Session.Id;
        ViewBag.SessionAvailable = HttpContext.Session.IsAvailable;

        var currentUser = HttpContext.Session.GetString("Username");
        if (!string.IsNullOrEmpty(currentUser))
        {
            ViewBag.DebugMessage = $"Zaten giriş yapılmış: {currentUser}";
            return RedirectToAction("Index", "File");
        }

        return View(new LoginModel());
    }

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            model.ErrorMessage = "Kullanıcı adı ve şifre boş olamaz.";
            return View(model);
        }

        var user = Users.FirstOrDefault(u =>
            u.Username.ToLower() == model.Username.ToLower() &&
            u.Password == model.Password);

        if (user != null)
        {
            try
            {
                // Session'a kaydet
                HttpContext.Session.SetString("Username", user.Username);

                // Debug: Session kaydedildi mi kontrol et
                var savedUser = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(savedUser))
                {
                    model.ErrorMessage = "Session kaydedilemedi! Program.cs ayarlarını kontrol edin.";
                    return View(model);
                }

                TempData["Success"] = $"Hoş geldin {user.Username}!";
                return RedirectToAction("Index", "File");
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Session hatası: {ex.Message}";
                return View(model);
            }
        }

        model.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
        return View(model);
    }
    /* ahmet bilge bgeahmet2@gmail.com*/

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}