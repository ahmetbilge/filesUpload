// FileController.cs - Debug versiyonu
using Microsoft.AspNetCore.Mvc;
using filesUpload.Models;

public class FileController : Controller
{
    private readonly string _baseUploadPath;

    public FileController()
    {
        _baseUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(_baseUploadPath))
        {
            Directory.CreateDirectory(_baseUploadPath);
        }
    }

    private string? GetCurrentUser()
    {
        return HttpContext.Session.GetString("Username");
    }

    private string GetUserUploadPath(string username)
    {
        var userPath = Path.Combine(_baseUploadPath, username.ToLower());
        if (!Directory.Exists(userPath))
        {
            Directory.CreateDirectory(userPath);
        }
        return userPath;
    }
    /* ahmet bilge bgeahmet2@gmail.com*/
    public IActionResult Index()
    {
        // Debug bilgileri
        ViewBag.SessionId = HttpContext.Session.Id;
        ViewBag.SessionAvailable = HttpContext.Session.IsAvailable;

        var currentUser = GetCurrentUser();
        ViewBag.DebugCurrentUser = currentUser ?? "NULL";

        if (currentUser == null)
        {
            TempData["Error"] = "Session bulunamad�! L�tfen tekrar giri� yap�n.";
            return RedirectToAction("Login", "Account");
        }

        var userUploadPath = GetUserUploadPath(currentUser);
        var files = new List<FileModel>();

        try
        {
            if (Directory.Exists(userUploadPath))
            {
                files = Directory.GetFiles(userUploadPath)
                    .Select(f => {
                        var fileInfo = new FileInfo(f);
                        return new FileModel
                        {
                            FileName = Path.GetFileName(f),
                            FilePath = $"/uploads/{currentUser.ToLower()}/{Path.GetFileName(f)}",
                            Owner = currentUser,
                            UploadDate = fileInfo.CreationTime,
                            FileSize = fileInfo.Length
                        };
                    })
                    .OrderByDescending(f => f.UploadDate)
                    .ToList();
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Dosya listesi al�namad�: {ex.Message}";
        }

        ViewBag.CurrentUser = currentUser;
        ViewBag.FileCount = files.Count;
        ViewBag.TotalSize = files.Sum(f => f.FileSize);
        ViewBag.UserPath = userUploadPath; // Debug i�in

        return View(files);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var currentUser = GetCurrentUser();
        if (currentUser == null)
        {
            TempData["Error"] = "Oturum sonlanm��! L�tfen tekrar giri� yap�n.";
            return RedirectToAction("Login", "Account");
        }

        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "L�tfen bir dosya se�in.";
            return RedirectToAction("Index");
        }

        // Dosya uzant�s� kontrol�
        var ext = Path.GetExtension(file.FileName).ToLower();
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".txt" };

        if (!allowedExtensions.Contains(ext))
        {
            TempData["Error"] = $"Desteklenmeyen dosya t�r�: {ext}. �zin verilenler: {string.Join(", ", allowedExtensions)}";
            return RedirectToAction("Index");
        }

        // Dosya boyutu kontrol� (5MB)
        if (file.Length > 5 * 1024 * 1024)
        {
            TempData["Error"] = $"Dosya �ok b�y�k: {Math.Round(file.Length / 1024.0 / 1024.0, 2)} MB. Maksimum 5MB.";
            return RedirectToAction("Index");
        }

        try
        {
            var userUploadPath = GetUserUploadPath(currentUser);
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(userUploadPath, fileName);

            // Ayn� isimde dosya varsa yeni isim olu�tur
            if (System.IO.File.Exists(filePath))
            {
                var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                var extension = Path.GetExtension(fileName);
                var counter = 1;

                do
                {
                    fileName = $"{nameWithoutExt}_{counter}{extension}";
                    filePath = Path.Combine(userUploadPath, fileName);
                    counter++;
                } while (System.IO.File.Exists(filePath));
            }

            // Dosyay� kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            TempData["Success"] = $"'{fileName}' ba�ar�yla y�klendi! Konum: {filePath}";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Dosya y�kleme hatas�: {ex.Message}";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(string fileName)
    {
        var currentUser = GetCurrentUser();
        if (currentUser == null)
        {
            TempData["Error"] = "Oturum sonlanm��!";
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var userUploadPath = GetUserUploadPath(currentUser);
            var filePath = Path.Combine(userUploadPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                TempData["Success"] = $"'{fileName}' silindi.";
            }
            else
            {
                TempData["Error"] = "Dosya bulunamad�.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Silme hatas�: {ex.Message}";
        }

        return RedirectToAction("Index");
    }
}