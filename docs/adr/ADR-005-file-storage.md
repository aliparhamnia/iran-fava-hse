# ADR-005 — File Storage

- Status: Accepted
- Date: 2026-08-15

## Context

مدارک پزشکی، بیمه‌نامه، عکس حادثه، PDF مجوز و مستندات محیط زیست باید ذخیره شوند. استقرار Private Cloud / Docker است. Azure Blob از روز اول فرض نیست.

## Problem

چگونه ذخیرهٔ فایل را از دامنه جدا کنیم تا بعداً Local / S3-compatible / Azure قابل تعویض باشد؟

## Options

1. **فایل در SQL (`varbinary`)**  
   Backup یکپارچه. DB سریع بزرگ و کند می‌شود. برای حجم HSE مناسب نیست.

2. **سیستم فایل محلی / Docker volume پشت `IFileStorage`**  
   ساده روی Private Cloud. Backup جدا از SQL. تعویض بعدی با پیاده‌سازی جدید.

3. **S3-compatible (MinIO و مشابه) از روز اول**  
   HA و object storage درست. یک سرویس دیگر برای v1.

4. **Azure Blob**  
   مناسب ابر عمومی. با تصمیم Private Cloud هم‌خوان نیست مگر اتصال هیبرید.

## Decision

**ABP Blob Storing: `IBlobContainer` + File System provider روی volume خارج از webroot در v1.**

قرارداد دامنه همچنان از طریق Application به Blob می‌رود نه به دیسک مستقیم. متادیتا در schema ماژول Documents. مدارک پزشکی Sensitivity=Restricted و طبق ADR-017 روی جریان رمز می‌شوند.

ویروس‌اسکن: interface اختیاری؛ در v1 no-op.

جایگزینی بعدی: MinIO / S3-compatible با تعویض provider بدون تغییر دامنه.

## Consequences

- مثبت: دامنه به vendor قفل نیست.
- مثبت: بدون سرویس object storage در روز اول.
- منفی: Runbook backup برای SQL و volume (TD-003).
- منفی: چند replica باید volume مشترک داشته باشند اگر scale-out شود.
