# ADR-013 — Reporting

- Status: Accepted
- Date: 2026-08-15

## Context

داشبورد و گزارش‌های HSE چند ماژول را با هم می‌بینند. Schema-per-module Join دامنه را ممنوع کرده است.

## Problem

گزارش را کجا اجرا کنیم؟

## Options

1. **همان Aggregateها و Repository دامنه**  
   hydrate سنگین؛ نقض مرز؛ کند.

2. **Query/DTO داخل همان ماژول برای لیست عملیاتی**  
   مناسب صفحه‌های همان Bounded Context. Projection و `AsNoTracking`.

3. **Read model / schema `rpt` / View برای گزارش و داشبورد cross-module**  
   جدا از write model. می‌تواند از View یا جدول همگام‌شده با رویداد پر شود.

4. **ابزار BI خارجی از روز اول**  
   وقتی KPI پایدار شد. برای v1 زود است.

## Decision

**لیست عملیاتی = Query ماژول. داشبورد/گزارش سنگین = Reporting جدا (View یا read table در `rpt`). دامنه write مدل گزارش را در خود نگه نمی‌دارد.**

Widgetهای داشبورد هر کدام Query خود را می‌زنند؛ یک God Query ممنوع.

Stored Procedure فقط وقتی Query در EF غیرقابل نگهداری یا به‌طور اندازه‌گیری‌شده کند شد.

## Consequences

- مثبت: مدل دامنه غنی می‌ماند و anemic برای گزارش نمی‌شود.
- منفی: دو مسیر خواندن باید مستند شود تا تکرار منطق فیلتر (مثلاً فقط Incidents باز) رخ ندهد — فیلتر کسب‌وکار پیچیده در Domain Service/specification می‌ماند؛ گزارش از دادهٔ از قبل تصمیم‌گرفته استفاده می‌کند.
