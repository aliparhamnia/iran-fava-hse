# ADR-002 — Database Strategy

- Status: Accepted
- Date: 2026-08-15

## Context

SQL Server و EF Core انتخاب محصول هستند. ماژول‌ها باید تا حد ممکن مستقل بمانند و در آینده قابل جداسازی باشند.

## Problem

چگونه داده را جدا کنیم بدون Database-per-service زودهنگام؟

## Options

1. **یک Database، یک Schema `dbo`، یک DbContext**  
   ساده. مرز ماژول در سطح داده از بین می‌رود. Joinهای بی‌رویه وسوسه‌انگیز می‌شوند.

2. **یک Database، Schema per module، DbContext per module**  
   جداسازی منطقی؛ backup و عملیات یکسان؛ استخراج بعدی = move schema.

3. **Database per module از روز اول**  
   استقلال قوی. تراکنش بین‌ماژولی، migration، و عملیات Private Cloud پیچیده می‌شود. Overengineering برای v1.

## Decision

**یک SQL Server database، Schema per module، یک DbContext per module.**

Schemaها: `idt`, `org`, `health`, `prev`, `ins`, `env`, `act`, `doc`, `wf`, `aud`, `ntf` و بعداً `rpt`.

Join دامنه بین DbContextها ممنوع است. گزارش cross-module از read model.

## Consequences

- مثبت: نزدیک به آیندهٔ microservice بدون هزینهٔ چند دیتابیس.
- مثبت: Naming تمیزتر از پیشوند `Health_MedicalExams` در `dbo`.
- منفی: گزارش‌های بین‌ماژولی نیاز به طراحی جدا دارند (ADR-013).
- منفی: تراکنش بین دو ماژول فقط از طریق Outbox/رویداد، نه dual-context SaveChanges در یک Unit of Work پنهان.
