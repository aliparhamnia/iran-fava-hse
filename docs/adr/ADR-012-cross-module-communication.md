# ADR-012 — Cross-Module Communication

- Status: Accepted
- Date: 2026-08-15

## Context

ماژول‌ها نباید Domain یکدیگر را ببینند. نمونه‌ها: Prevention می‌خواهد CAPA بسازد؛ Health سررسید دارد و Notification می‌فرستد.

## Problem

همگام از طریق پروژهٔ داخلی، یا رویداد، یا bus خارجی؟

## Options

1. **Project reference مستقیم به Domain ماژول دیگر**  
   سریع و مخرب. ممنوع.

2. **Facade همگام در `.Contracts` (مثلاً `IEmployeeLookup`)**  
   برای خواندن مرجع پایدار (نام کارمند، وجود ID) مناسب. تراکنش کسب‌وکار دو ماژول را قاطی نکند.

3. **Integration Event درون‌فرآیندی + Outbox در همان تراکنش SQL**  
   decoupling. Handler ماژول دیگر بعد از commit. مناسب اکثر side-effectها.

4. **Message broker از روز اول**  
   عملیات اضافه. برای یک Host اضافی است.

## Decision

**پیش‌فرض ABP: Local Event Bus برای رویداد داخل فرآیند؛ Distributed Event Bus (بدون broker در v1، in-process) برای Integration Event پایدار در Application.Contracts.**

Transactional outbox وقتی به broker واقعی رفت فعال می‌شود؛ تا آن زمان local/distributed in-process ABP کافی است.

**استثناء محدود:** Application Service / facade همگام فقط برای lookup هویت/سازمان.

Domain Event داخل ماژول. Integration Event در Contracts و بدون PHI.

## Consequences

- مثبت: مسیر آینده به broker بدون تغییر قرارداد رویداد.
- مثبت: سازگاری تراکنشی write + outbox.
- منفی: eventual consistency برای CAPA ساخته‌شده از Incident.
- منفی: باید Inbox/idempotency ساده برای Handlerها رعایت شود.
