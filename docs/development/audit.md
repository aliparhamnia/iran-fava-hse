# Audit Architecture

Audit ≠ Log.

## ذخیره

`aud.AuditEntries`

| فیلد | شرح |
| --- | --- |
| Id | Guid از IGuidGenerator |
| TenantId | |
| Who | UserId + نام نمایشی در لحظه |
| When | UTC |
| What | Action: Create/Update/Delete/Complete/ReadPhi/Approve/LoginFailed ... |
| EntityType | |
| EntityId | |
| OldValue | JSON |
| NewValue | JSON |
| Ip | |
| UserAgent | |
| CorrelationId | |

## چگونه پر می‌شود

1. **EF interceptor** برای تغییرات حساس پیکربندی‌شده (نه همهٔ جدول‌های lookup)
2. **صریح در Use Case** برای Read PHI، Approve، Login، Download فایل Restricted

## PHI در Old/New

ماسک یا حذف کلیدهای بالینی. خود عمل ReadPhi بدون ذخیرهٔ متن یافته ثبت می‌شود (`{"fields":["Findings"]}`).

## Retention

قابل‌پیکربندی. حذف فیزیکی طبق سیاست سازمان؛ تا آن زمان append-only.

## UI

صفحهٔ Audit با `Audit.View`. فیلتر Entity/User/بازه. بدون این Permission در منو نیست.

## Performance

Write async داخل همان request با همان تراکنش ترجیح دارد تا Audit گم نشود. اگر حجم بالا شد: همان تراکنش برای writeهای مالی/پزشکی حیاتی؛ بقیه می‌تواند Outbox به جدول audit باشد — v1 همان تراکنش برای سادگی و صحت.
