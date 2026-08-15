# Security Controls (سایر)

## Anti-forgery

پیش‌فرض Blazor / ASP.NET Core برای فرم‌ها و interactive server.

## Input validation

چهار سطح: UI، FluentValidation، Domain invariant، Database constraint. قانون اصلی در Domain.

## Secure file upload

- Whitelist پسوند و MIME
- سقف حجم قابل‌پیکربندی
- نام ذخیره‌سازی تصادفی/Guid نه نام اصلی روی دیسک
- خارج از webroot
- عدم اجرای محتوا
- Sensitivity روی متادیتا

## Headers

HSTS (پشت HTTPS terminator)، `X-Content-Type-Options: nosniff`، `Referrer-Policy`, `X-Frame-Options` / CSP متناسب با Blazor (Blazor به inline/script خاصی نیاز دارد — CSP را سخت ولی عملی تنظیم کن، نه آن‌قدر که Circuit بشکند).

## Rate limiting

روی `/login` و API عمومی. پنل داخلی پشت شبکهٔ Private Cloud همچنان برای brute force مفید است.

## Secrets

User Secrets در dev؛ env / Docker secrets در Private Cloud. هیچ رازی در Git و appsettings commit‌شدهٔ حاوی password.

## Data Protection

کلیدها روی volume پایدار. برای چند replica بعداً key ring مشترک.

## Sensitive data در Log

ممنوع: کد ملی، رمز، cookie، تشخیص، شماره کامل بیمه‌نامه، token.

مجاز: Idها، PermissionCode، Workflow state، CorrelationId، TraceId.
