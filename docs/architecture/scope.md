# Scope

## داخل محدودهٔ معماری (از روز اول طراحی می‌شود)

حتی اگر در نسخهٔ اول پیاده نشود، معماری باید این‌ها را تحمل کند:

- چهار ماژول کسب‌وکار: Health, Prevention, Insurance, Environment
- ماژول‌های بعدی: ریسک گسترده، Incident Management جدا، آموزش، بازرسی، پیمانکار، تجهیزات، اسناد، داشبورد، گزارش، کاربران/سازمان، Notification، Workflow، CAPA
- Identity, Authorization, Organization structure
- Audit, Logging, File management, Notification, Reporting
- Multi-tenancy آماده (بدون فعال‌سازی کامل)
- Integration از طریق Application.Contracts / ABP HttpApi
- استخراج تدریجی ماژول به Microservice طبق قرارداد ABP
- Localization دوزبانه و تقویم وابسته به فرهنگ
- رمزنگاری PHI و فایل Restricted

## داخل محدودهٔ نسخهٔ اول محصول (پس از Foundation)

ترتیب در [roadmap.md](roadmap.md):

1. Foundation (Identity, Organization حداقلی, Host, Logging, Persistence)
2. Vertical Slice: Medical Examination
3. سپس Featureهای بعدی یکی‌یکی

نسخهٔ اول **چهار ماژول کامل** نیست.

## خارج از محدودهٔ نسخهٔ اول

- Microservice واقعی
- Database per tenant
- Elsa / موتور BPMN
- SMS (فقط abstraction)
- همگام‌سازی Active Directory
- Always Encrypted / HSM مگر الزام حقوقی فراتر از ADR-017
- ABP Commercial / LeptonX / Elsa Pro
- MediatR موازی با Application Service
- OpenTelemetry کامل و APM تجاری
- E2E گسترده روی همهٔ صفحات
- موتور گزارش تجاری (Power BI embed و مشابه) مگر نیاز قطعی شود
- اپ موبایل native
- WASM / Interactive Auto به‌عنوان حالت پیش‌فرض

## Assumptions

1. استقرار v1 روی **Private Cloud با Docker** است (تأییدشده).
2. هویت v1 با **ABP Identity محلی** است (تأییدشده).
3. UI **دوزبانه** `fa` / `en` است؛ `fa` RTL+شمسی، `en` LTR+میلادی.
4. یک سازمان عملیاتی در v1؛ هلدینگ چندشرکتی از طریق OrganizationUnit مدل می‌شود نه Tenant جدا.
5. کارمند در همین سامانه Master Data اولیه دارد تا Integration HR مشخص شود.
6. SQL Server در دسترس Private Cloud است.
7. SMTP سازمانی ممکن است بعداً وصل شود؛ In-App Notification از روز اول طراحی می‌شود.
8. تولید روی **ABP Framework 10 OSS** است (تأییدشده).
9. PHI در SQL و فایل Restricted **رمز** می‌شود (تأییدشده).

## Non-goals معماری

- Generic Repository موازی با `IRepository` ABP
- Shared Kernel بزرگ که همهٔ Entityها را قاطی کند
- یک DbContext برای کل سیستم
- دادن Entity دیتابیس به UI
- Hard-code کردن Permission و Workflow state به‌عنوان تنها منبع حقیقت
- MediatR / IFileStorage موازی با استاندارد ABP
