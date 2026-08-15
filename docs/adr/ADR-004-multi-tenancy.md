# ADR-004 — Multi-tenancy

- Status: Accepted
- Date: 2026-08-15

## Context

محصول باید در آینده چندسازمانی/SaaS شود. نسخهٔ اول یک استقرار برای یک سازمان (با ساختار درختی داخلی) است.

## Problem

کدام استراتژی داده برای Tenant انتخاب شود بدون قفل اشتباه؟

## Options

1. **Shared Database + Shared Schema + discriminator `TenantId`**  
   ساده؛ یک migration؛ خطر باگ isolation اگر query filter فراموش شود.

2. **Shared Database + Schema per tenant**  
   جداسازی بهتر داخل یک instance. Migration و تعداد schema در هلدینگ‌های زیاد سخت است. با Schema per module تداخل مفهومی دارد.

3. **Database per tenant**  
   Isolation قوی (backup، restore، رمزنگاری جدا). عملیات، pool اتصال، و هزینه Private Cloud برای v1 زیاد است.

4. **بدون هیچ نشانهٔ Tenant تا روز SaaS**  
   ساده‌ترین v1. افزودن TenantId بعداً به همهٔ جدول‌ها migration دردناک است.

## Decision

**مدل چندمستأجری ABP: Shared Database + discriminator `TenantId` + data filter آماده.**

v1 تک‌تننت (host + یک tenant پیش‌فرض). هلدینگ با Organization Unit مدل می‌شود نه Tenant جدا.

Database-per-tenant فقط با ADR جدید.

## Consequences

- مثبت: آمادهٔ Tenant دوم بدون بازنویسی جداول.
- مثبت: هلدینگ چندشرکتی در v1 بدون پیچیدگی SaaS.
- منفی: isolation باید با global query filter و تست، وقتی Tenant دوم آمد، کامل شود (TD-002).
- منفی: یک backup همهٔ تننت‌ها را با هم دارد.
