# Authentication

مرجع: [ADR-003](../adr/ADR-003-authentication.md).

## v1

- ماژول `Volo.Abp.Identity` (روی ASP.NET Core Identity)
- Cookie برای Blazor Web App
- Password policy از Setting/Identity Options ABP
- Session: cookie امن (`HttpOnly`, `Secure`, `SameSite` متناسب با reverse proxy)
- JWT / OpenIddict جدا: نه تا Integration خارجی یا الگوی tiered

## حساب‌ها

- User سامانه ≠ Employee
- پیوند اختیاری UserId ↔ EmployeeId
- Provision اولیه: کاربر admin از env در seed

## آیندهٔ AD

بدون dual-write در v1. مسیر: external login / sync به User موجود. Permission مدل عوض نمی‌شود.

## صفحات

Login با SSR. پس از ورود Circuit Server برای پنل.
