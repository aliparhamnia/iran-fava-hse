# Non-Functional Requirements

## امنیت

- Authentication و Authorization از روز اول
- Permission-based؛ منو و داده هر دو فیلتر می‌شوند
- PHI جدا از دادهٔ عملیاتی Fitness
- رمزنگاری ستون‌های PHI و فایل Restricted ([encryption.md](../security/encryption.md))
- Audit برای عملیات حساس و خواندن PHI
- Upload امن، Anti-forgery، Secure headers
- Secrets خارج از Git
- اطلاعات حساس در Log نمی‌آید

جزئیات: [security](../security/authentication.md).

## قابلیت نگهداری و توسعه

- Modular Monolith با قرارداد ABP
- Vertical Slice برای Featureهای جدید
- ADR برای تصمیم مهم
- Architecture Tests برای جلوگیری از coupling غیرمجاز
- Coding standards و DoD اجباری

## تست‌پذیری

حداقل: Unit، Integration، Architecture.
جزئیات: [development/testing.md](../development/testing.md).

## کارایی

- Async/Await و CancellationToken در I/O
- Pagination اجباری برای لیست‌ها
- Projection و `AsNoTracking` برای Query
- جلوگیری از N+1؛ Lazy Loading پیش‌فرض خاموش
- ایندکس روی FK و فیلترهای لیست
- Caching فقط با دلیل مشخص (مثلاً Permission catalog)

## مقیاس

- v1: یک instance در Private Cloud کافی است
- Blazor Server برای مقیاس افقی بعدی: sticky session یا Redis backplane (بدهی آگاهانه)
- استخراج ماژول وقتی مرز پایدار و بار جدا شد

## در دسترس بودن و عملیات

- Docker Compose برای Private Cloud v1
- Health checks
- Serilog structured + CorrelationId / TraceId
- Hangfire Dashboard محافظت‌شده
- Backup SQL و volume فایل جدا

## بین‌المللی‌سازی و UI

- UI دوزبانه: `fa` (RTL، تقویم شمسی) و `en` (LTR، تقویم میلادی)
- Localization ABP JSON per module
- ذخیرهٔ تاریخ در DB میلادی (`DateOnly` / `DateTimeOffset` UTC)
- جزئیات: [../development/localization.md](../development/localization.md)

## انطباق و Audit

- Who / When / What / Entity / EntityId / Old / New / IP / UserAgent / CorrelationId
- Retention قابل‌پیکربندی
- Audit ≠ Log

## Integration

- ABP HttpApi روی همان Host غیر tiered
- DTO جدا از Entity
- JWT / OpenIddict فقط وقتی مصرف‌کنندهٔ خارجی واقعی وجود دارد
