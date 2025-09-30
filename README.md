# IdentityAuthApp

Bu proje, ASP.NET Identity kullanarak oluşturulmuş bir **kimlik doğrulama ve yetkilendirme uygulamasıdır**. Kullanıcılar kayıt olabilir, giriş yapabilir; adminler ise roller ve kullanıcı yönetimini yapabilir.

---

## Özellikler

- **Kullanıcı Yönetimi**: Kayıt ve giriş işlemleri, kullanıcı profili yönetimi  
- **Rol ve Yetki Yönetimi**: Adminler kullanıcı rollerini yönetebilir  
- **Form Doğrulama**: FluentValidation ile backend validation  
- **Güvenlik**: Şifreler hashlenir, cookie tabanlı oturum yönetimi  
- **UI Desteği**: Modern kullanıcı dostu arayüz  
- **Veritabanı**: PostgreSQL ile veri yönetimi  

---

## Kullanılan Teknolojiler

- **Backend:** ASP.NET Core 9, Entity Framework Core, ASP.NET Identity  
- **Frontend/UI:** React   
- **Veritabanı:** PostgreSQL
- **Ek Paketler:**  
  - `FluentValidation` – Form ve veri doğrulama  
  - `Mapster` – DTO mapping  
  - `MediatR` – CQRS pattern  
  - `TS.EntityFrameworkCore.GenericRepository` – Repository pattern  
  - `TS.Result` – Result wrapping  

---

## Kurulum

1. Depoyu klonlayın:

```bash
git clone https://github.com/kullaniciAdin/IdentityAuthApp.git
cd IdentityAuthApp
