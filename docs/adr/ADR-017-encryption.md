# ADR-017 — Encryption of Sensitive Data

- Status: Accepted
- Date: 2026-08-15

## Context

اطلاعات پزشکی (PHI)، مدارک Restricted، و داده‌های هویتی حساس باید در برابر خواندن خام از SQL، لاگ، و دیسک فایل محافظت شوند. تصمیم محصول: **رمزنگاری انجام شود** (نه بدهی آینده).

## Problem

کدام لایه رمزنگاری برای Private Cloud + SQL Server + ABP مناسب است بدون از بین بردن قابلیت Query ضروری؟

## Options

1. **بدون رمزنگاری؛ فقط Permission و Audit**  
   DBA و backup خام متن را می‌بینند. رد شده.

2. **Application-level encryption با `IStringEncryptionService` ABP + EF Value Converter**  
   ستون‌های متنی PHI در DB به‌صورت ciphertext. کلید از secrets. جستجوی LIKE روی متن بالینی ممکن نیست (قابل قبول).

3. **SQL Always Encrypted**  
   حفاظت قوی در برابر DBA. پیچیدگی گواهی، درایور، و محدودیت کوئری. مکمل است نه جایگزین فوری برای همهٔ فیلدها.

4. **رمزنگاری کل دیتابیس (TDE)**  
   از دیسک سرور محافظت می‌کند؛ DBA متصل همچنان plaintext می‌بیند.

## Decision

**گزینه ۲ به‌عنوان الزام v1، به‌علاوه TDE اگر SQL سازمان فراهم کند.**

Always Encrypted برای فیلدهای فوق‌حساس بعدی در صورت الزام حقوقی جداگانه (مکمل).

### چه چیزی رمز می‌شود

| داده | روش |
| --- | --- |
| یافته، تشخیص، شرح بالینی، نتایج آزمایش متنی | AES از `IStringEncryptionService`؛ Value Converter؛ nondeterministic |
| کد ملی / شناسه ملی در صورت ذخیره | رمز + **blind index** (HMAC) جدا برای جستجوی exact match |
| فایل پزشکی Restricted | رمز جریان قبل از Blob Save (envelope با همان key ring) |
| FitnessStatus، نوع معاینه، تاریخ، Id | رمز نمی‌شود (برای لیست و KPI لازم است) |

### چه چیزی رمز نمی‌شود

- کلیدهای خارجی، وضعیت Workflow، تاریخ‌ها، مبلغ بیمه، نام نمایشی کارمند در حد عملیاتی

### کلید

- `AbpStringEncryptionOptions.PassPhrase` و کلید فایل از Docker secrets / env
- چرخش کلید: version prefix روی ciphertext (`v1:...`) تا re-encrypt تدریجی ممکن باشد
- کلید در Git نیست؛ در Development از User Secrets

### Audit و Log

- Old/New برای فیلد رمزشده ذخیره نمی‌شود یا فقط `{"Findings":"[encrypted]"}`
- Exception و Serilog هرگز plaintext PHI ندارند
- رمزگشایی فقط در Application پس از Authorization `Health.Phi.View`

## Consequences

- مثبت: backup SQL بدون کلید برای مهاجم متن بالینی ندارد
- مثبت: هم‌خوان با ABP؛ بدون وابستگی به Azure
- منفی: فیلتر SQL روی متن تشخیص ممکن نیست
- منفی: گم شدن کلید = از دست رفتن PHI؛ backup کلید جدا و اجباری است
- منفی: Always Encrypted هنوز برای جداسازی کامل از DBA در همان session SQL لازم نیست — اگر واحد حقوقی بخواهد، ADR تکمیلی
