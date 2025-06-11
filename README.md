# 📂 FilesUpload Projesi

Bu proje, ASP.NET Core MVC kullanılarak geliştirilmiş bir **dosya yükleme ve kullanıcı yönetimi** sistemidir. Kullanıcılar sisteme giriş yaparak dosya yükleyebilir ve yüklenen dosyaları görüntüleyebilir.

---

## 🚀 Özellikler

- Kullanıcı girişi ve oturum yönetimi
- Dosya yükleme ve listeleme işlemleri
- MVC mimarisi kullanımı
- Razor View sayfaları
- Katmanlı yapı (Controller, Model, View)
- Basit ve anlaşılır kullanıcı arayüzü

---

## 📁 Proje Klasör Yapısı

```
Controllers/
├── AccountController.cs
├── FileController.cs
├── HomeController.cs

Models/
├── User.cs
├── FileModel.cs
├── LoginModel.cs
├── ErrorViewModel.cs

Views/
├── Account/Login.cshtml
├── File/Index.cshtml
├── Home/Index.cshtml, Privacy.cshtml
├── Shared/_Layout.cshtml, Error.cshtml, _ValidationScriptsPartial.cshtml
```

---

## 🛠️ Kurulum Adımları

1. Reponun bir kopyasını bilgisayarınıza klonlayın:
   ```
   git clone https://github.com/ahmetbilge/filesUpload.git
   ```
2. Visual Studio ile projeyi açın.
3. Gerekli NuGet paketlerini yükleyin.
4. `appsettings.json` dosyasından veritabanı bağlantısını düzenleyin.
5. Package Manager Console’da aşağıdaki komutu çalıştırın:
   ```
   Update-Database
   ```

---

## 🔐 Örnek Giriş Bilgileri

```
name: ahmet
Şifre: 123456
```

> Bilgiler örnek amaçlıdır. Gerçek kullanıcılar veri tabanı kullanılmadan ornek kullanım açısından proje yapılmıştır.

---

## 👤 Geliştirici

- GitHub: [ahmetbilge](https://github.com/ahmetbilge)

---

## 📄 Lisans

Bu proje MIT lisansı ile lisanslanmıştır.
