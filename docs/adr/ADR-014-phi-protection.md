# ADR-014 — Sensitive Medical Data (PHI) Protection

- Status: Accepted
- Date: 2026-08-15

## Context

پروندهٔ سلامت، تشخیص، نتایج آزمایش و مدارک پزشکی Sensitive Data هستند. HR و مدیر ممکن است فقط Fitness for Work و محدودیت شغلی را لازم داشته باشند.

## Problem

چگونه حداقل افشا، دسترسی دقیق، و حسابرسی خواندن را طراحی کنیم بدون ساختن سیستم طبقه‌بندی بیش‌ازحد؟

## Options

1. **همان Permission ماژول Health برای همهٔ فیلدها**  
   ساده. تشخیص به هر کسی که لیست معاینات را می‌بیند نشت می‌کند.

2. **Permission جدا برای PHI + تفکیک DTO**  
   `Health.MedicalExam.View` برای وجود معاینه/تاریخ/نوع/Fitness. `Health.Phi.View` برای تشخیص، یافته، فایل پزشکی. Read audit برای PHI.

3. **رمزنگاری ستونی از روز اول (Always Encrypted)**  
   حفاظت در برابر DBA. پیچیدگی کلید و کوئری. الزام حقوقی هنوز باز است (TD-005).

4. **ماژول Health کاملاً جدا از نظر شبکه**  
   معادل microservice پزشکی. برای v1 Overengineering.

## Decision

**گزینه ۲ + scoping سازمانی + رمزنگاری ADR-017.**

- DTO غیرپزشکی هرگز فیلد PHI ندارد.
- فایل پزشکی Sensitivity=Restricted، Permission Phi، و ciphertext در storage.
- خواندن PHI در Audit ثبت می‌شود (متن بالینی در Old/New نیست).
- Log و Exception: بدون کد ملی، تشخیص، شرح بالینی.
- ستون‌های متنی PHI در SQL رمز می‌شوند؛ DBA بدون کلید متن را نمی‌بیند.

## Consequences

- مثبت: نیاز HR/HSE بدون دیدن بالینی برآورده می‌شود.
- مثبت: Architecture/UI می‌تواند دو نما بسازد.
- مثبت: backup خام برای مهاجم بدون کلید قابل خواندن نیست.
- منفی: هر Query لیست باید آگاه به projection باشد.
- منفی: جستجوی متنی روی تشخیص در SQL ممکن نیست.
