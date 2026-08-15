# ADR-001 — Modular Monolith

- Status: Accepted
- Date: 2026-08-15

## Context

HSE Management Platform باید Enterprise-grade، ماژولار، قابل نگهداری و در آینده قابل استخراج به Microservice باشد. چهار ماژول کسب‌وکار و چندین ماژول پلتفرم از روز اول مطرح‌اند، اما مرزها هنوز در حال یادگیری از محصول واقعی هستند.

## Problem

بین یک Monolith کلاسیک، Modular Monolith، و Microservices از روز اول کدام را انتخاب کنیم طوری که Technical Debt و Overengineering هم‌زمان کنترل شوند؟

## Options

1. **Monolith کلاسیک با لایه‌های افقی فقط (UI / App / DAL)**  
   ساده برای شروع. ماژول‌ها به‌سرعت قاطی می‌شوند. استخراج بعدی پرهزینه است.

2. **Modular Monolith**  
   یک Deployable؛ ماژول‌ها Bounded Context هستند؛ ارتباط فقط از Contracts/Events. Architecture Tests مرز را نگه می‌دارند.

3. **Microservices از روز اول**  
   استقلال بالا. هزینه عملیات، تراکنش توزیع‌شده، و قفل زودهنگام قرارداد برای تیمی که محصول را تازه شکل می‌دهد زیاد است.

## Decision

**Modular Monolith + Clean Architecture + DDD + Vertical Slice داخل هر ماژول.**

یک Host، یک Pipeline استقرار، مرز کامپایل‌شده بین ماژول‌ها.

## Consequences

- مثبت: یک عملیات Private Cloud؛ تراکنش محلی؛ بازطراحی مرز با refactor نه با distributed deploy.
- مثبت: مسیر استخراج ماژول (مثلاً Notifications) بدون بازنویسی Domain.
- منفی: نظم تیم و Architecture Tests اجباری است وگرنه Modular Monolith به Big Ball of Mud تبدیل می‌شود.
- منفی: مقیاس مستقل per module در v1 وجود ندارد.
