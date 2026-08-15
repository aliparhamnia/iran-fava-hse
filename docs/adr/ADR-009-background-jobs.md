# ADR-009 — Background Jobs

- Status: Accepted
- Date: 2026-08-15

## Context

کارهای زمان‌بندی‌شده لازم است: سررسید معاینه، انقضای بیمه، CAPA معوق، ارسال Notification، گزارش‌های دوره‌ای.

## Problem

Hangfire، Quartz.NET، یا `BackgroundService`؟

## Options

1. **`BackgroundService` فقط**  
   بدون وابستگی. بدون persistence بعد از restart، بدون dashboard، بدون retry استاندارد. برای cronهای کسب‌وکار ناکافی.

2. **Hangfire + SQL Server storage**  
   Fire-and-forget، recurring، retry، dashboard، persistence. با SQL موجود هم‌خوان. روی چند instance باید همزمانی Job مدیریت شود.

3. **Quartz.NET**  
   تقویم و misfire قوی. Dashboard و retry را باید خودمان بسازیم. برای v1 سنگین‌تر از نیاز.

4. **هر دو Hangfire و Quartz**  
   دو مکانیزم. ممنوع مگر دلیل جدا.

## Decision

**ABP Background Jobs با یکپارچگی Hangfire و SQL Server storage.**

کد شغل‌ها به `IBackgroundJobManager` / workers ABP وابسته است نه به API خام Hangfire در دامنه. Dashboard پشت Permission. Jobها idempotent.

## Consequences

- مثبت: شغل بعد از deploy گم نمی‌شود؛ مشاهدهٔ شکست.
- منفی: جداول Hangfire در همان SQL.
- منفی: چند replica بدون دقت = اجرای دوبل (ریسک مستند).
