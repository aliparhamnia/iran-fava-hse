# Notification Architecture

## مدل

Event-driven. ماژول کسب‌وکار Notification را مستقیم UI-call نمی‌کند؛ رویداد می‌دهد.

```text
Integration Event → Notification handler → Channel dispatcher
```

## کانال‌ها

| کانال | v1 |
| --- | --- |
| In-App | بله |
| Email | بله اگر SMTP پیکربندی شده؛ وگرنه skip با log |
| SMS | interface؛ پیاده‌سازی ندارد |

## انواع اولیه

- `MedicalExamDue`
- `InsuranceExpiresSoon`
- `InspectionOverdue`
- `CorrectiveActionOverdue`
- `PermitExpiresSoon`

## ذخیره In-App

`ntf.Notifications`: UserId, Title, Body, Link, IsRead, OccurredAt, Type

UI زنگوله با `Notifications.View`.

## Jobها

Hangfire recurring روزانه (ساعت قابل‌پیکربندی) برای due/expiry که رویداد دامنهٔ کاربر ندارند.

Idempotent: یک نوتیفیکیشن per (Type, EntityId, DueDate, UserId).

## قالب

متن از resource؛ بدون PHI در ایمیل (فقط «معاینه سررسید دارد» + لینک + نام کارمند در حد مجاز).
