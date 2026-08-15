# ADR-011 — Module Project Shape

- Status: Superseded by [ADR-018](ADR-018-abp-framework.md)
- Date: 2026-08-15
- Superseded: 2026-08-15

## Context

Clean Architecture کلاسیک چهار پروژه per module می‌خواهد. با حدود ۱۰ ماژول این یعنی ده‌ها csproj.

## Problem

چگونه مرز لایه و مرز ماژول را نگه داریم بدون Solution غیرقابل‌ناوبری؟

## Options

1. **۴ پروژه per module (Domain, Application, Infrastructure, Presentation)**  
   مرز کامپایل قوی. حدود ۴۰ پروژه فقط برای ماژول‌ها. کند و پرتشریفات برای v1.

2. **۲ پروژه per module: `{Module}` + `{Module}.Contracts`**  
   پوشه‌های Domain/Application/Features/Infrastructure داخل ماژول. Architecture Tests روی namespace. Contracts تنها API عمومی.

3. **۱ پروژه per module بدون Contracts جدا**  
   InternalsVisibleTo و discipline. Internals به‌راحتی leak می‌شود بین ماژول‌ها.

4. **Vertical Slice فقط در یک پروژه Host**  
   ساده. Modular Monolith واقعی نیست.

## Decision

**گزینه ۲ منسوخ است.** تولید روی **لایه‌های استاندارد ماژول ABP** است. جزئیات در [ADR-018](ADR-018-abp-framework.md).

## Consequences

- این ADR دیگر مبنای پیاده‌سازی نیست.
- تعداد پروژه per module بیشتر می‌شود؛ هزینهٔ پذیرفته‌شدهٔ استاندارد ABP است.
