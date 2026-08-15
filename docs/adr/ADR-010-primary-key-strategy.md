# ADR-010 — Primary Key Strategy

- Status: Accepted
- Date: 2026-08-15

## Context

ماژولار بودن، استخراج بعدی، و Replication/ادغام داده مطرح است. SQL Server ایندکس clustered روی کلید تصادفی را دوست ندارد.

## Problem

`int`/`long` identity، `Guid` تصادفی، یا `Guid` نسخه‌دار ترتیبی؟

## Options

1. **`int` / `bigint` IDENTITY**  
   باریک و سریع. ادغام بین سیستم‌ها و استخراج microservice با برخورد ID. افشای تعداد ردیف در API.

2. **`uniqueidentifier` تصادفی (Guid.NewGuid)**  
   یکتا جهانی. fragmentation ایندکس clustered.

3. **Guid v7 (`Guid.CreateVersion7()`)**  
   یکتا جهانی، زمان‌مند، برای ایندکس بسیار بهتر از Guid v4. در .NET 9+ در BCL است.

4. **HiLo / Comb سفارشی**  
   کنترل بیشتر. پیچیدگی بی‌دلیل وقتی Guid v7 وجود دارد.

## Decision

**`IGuidGenerator` استاندارد ABP (ترتیبی مناسب SQL Server / SequentialAtEnd).**

هدف همان Guid v7 است: یکتایی جهانی + ایندکس بهتر از Guid تصادفی. در کد دامنه `Guid.CreateVersion7()` دستی استفاده نمی‌شود تا با ABP یکدست بماند.

کلید طبیعی کسب‌وکار (شماره پرونده) جدا از PK است.

## Consequences

- مثبت: آمادهٔ ادغام و استخراج؛ بدون sequence مرکزی.
- مثبت: ایندکس قابل قبول‌تر از Guid تصادفی.
- منفی: ۱۶ بایت به‌جای ۸؛ Joinها کمی سنگین‌تر — برای این دامنه قابل قبول.
- منفی: نمایش Guid در UI لازم نیست؛ کد کسب‌وکار جدا نمایش داده می‌شود.
