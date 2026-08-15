# Technical Debt Register

بدهی پنهان نمی‌شود. هر مورد آگاهانه است.

قالب:

```text
Problem
Current Workaround
Risk
Suggested Future Solution
Priority
```

## TD-001 In-process integration events

**Problem:** ارتباط ماژول‌ها in-process است، نه message bus.

**Current Workaround:** Domain/Integration events + transactional outbox جدولی.

**Risk:** هنگام استخراج Microservice باید transport عوض شود.

**Suggested Future Solution:** همان `IIntegrationEvent` / Outbox؛ publisher به RabbitMQ/Azure Service Bus.

**Priority:** Low تا قبل از استخراج ماژول.

## TD-002 Tenant filter ناقص

**Problem:** ستون `TenantId` هست؛ isolation کامل چندتننت نیست.

**Current Workaround:** تک‌تننت با مقدار پیش‌فرض.

**Risk:** فعال‌سازی عجولانه Tenant دوم بدون query filter سراسری.

**Suggested Future Solution:** EF global query filter + testهای isolation.

**Priority:** Medium قبل از Tenant دوم.

## TD-003 Local file volume

**Problem:** فایل روی دیسک کانتینر/volume است نه object storage.

**Current Workaround:** ABP `IBlobContainer` + File System.

**Risk:** Backup جدا؛ جابجایی node.

**Suggested Future Solution:** S3-compatible داخل Private Cloud.

**Priority:** Medium وقتی حجم فایل یا HA لازم شد.

## TD-004 CAPA حداقلی

**Problem:** ماژول Actions تا قبل از Incident/Inspection زنده نیست.

**Current Workaround:** قرارداد و schema رزرو؛ پیاده‌سازی با اولین مصرف‌کننده.

**Risk:** اگر Health قبل از Actions نیاز به اقدام پیدا کند، وسوسهٔ دفن CAPA در Health.

**Suggested Future Solution:** اولین CAPA از Prevention Incident.

**Priority:** Low.

## TD-005 Encryption کلید و جستجو

**Problem:** PHI رمز می‌شود (ADR-017)؛ جستجوی متنی SQL روی تشخیص ممکن نیست. گم شدن کلید = از دست رفتن داده.

**Current Workaround:** `IStringEncryptionService` + Value Converter + backup جداگانهٔ secrets. Blind index فقط برای کد ملی.

**Risk:** مدیریت کلید ضعیف؛ Always Encrypted هنوز برای جداسازی session DBA اضافه نشده.

**Suggested Future Solution:** key rotation runbook + در صورت الزام حقوقی Always Encrypted.

**Priority:** High برای runbook کلید؛ Medium برای Always Encrypted.

## TD-006 OpenTelemetry کامل نیست

**Problem:** Observability محدود به Serilog است.

**Current Workaround:** CorrelationId / TraceId در Log.

**Suggested Future Solution:** OTel + collector داخلی Private Cloud.

**Priority:** Low تا درد عملیاتی واقعی.

## TD-007 بدون همگام‌سازی AD

**Problem:** کاربران محلی‌اند.

**Current Workaround:** Identity؛ قرارداد `IExternalUserDirectory` رزرو نمی‌شود تا نیاز قطعی شود (جلوگیری از abstraction نمایشی). ADR-003 مسیر را مشخص کرده.

**Risk:** ورود سازمانی بعداً dual-source می‌شود.

**Suggested Future Solution:** LDAP/AD sync یا Federation.

**Priority:** Low تا درخواست مشتری.

## TD-008 Blazor Server scale-out

**Problem:** Circuit روی حافظهٔ یک node است.

**Current Workaround:** یک replica در v1.

**Risk:** HA واقعی نیاز به Redis backplane و sticky session دارد.

**Suggested Future Solution:** Redis + replicaهای محدود.

**Priority:** Medium وقتی دومین instance لازم شد.

## TD-010 تعداد پروژهٔ ABP

**Problem:** هر ماژول ۷ لایهٔ csproj دارد؛ Solution شلوغ می‌شود.

**Current Workaround:** پذیرش استاندارد ABP (ADR-018)؛ ADR-011 منسوخ.

**Risk:** کندی IDE؛ وسوسهٔ دور زدن لایه.

**Suggested Future Solution:** Solution folders دقیق؛ ماژول جدید فقط وقتی Bounded Context پایدار است.

**Priority:** Low.

## TD-009 تقویم وابسته به فرهنگ

**Problem:** MudBlazor DatePicker پیش‌فرض میلادی است.

**Current Workaround:** `AppDatePicker` — شمسی برای `fa`، میلادی برای `en` (ADR-016).

**Risk:** تعویض کتابخانهٔ شمسی.

**Suggested Future Solution:** همان wrapper؛ ذخیرهٔ دامنه مستقل از UI.

**Priority:** Low.

## TD-011 Organization و Workflow داخل Host

**Problem:** ADR-018 هفت لایه csproj per module می‌خواهد؛ در Foundation برای جلوگیری از ۱۴ پروژهٔ خالی، Employee و Workflow به‌صورت namespace داخل Host پیاده شدند.

**Current Workaround:** `Hse.Platform.Organization` و `Hse.Platform.Workflow` در لایه‌های موجود Host؛ schema جدا (`org`, `wf`).

**Risk:** استخراج ماژول بعداً نیاز به جابجایی نوع‌ها و DbContext دارد.

**Suggested Future Solution:** وقتی Bounded Context پایدار شد، `abp new` module و انتقال Vertical Slice.

**Priority:** Low تا قبل از ماژول دوم کسب‌وکار (Health).
