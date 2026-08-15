# Encryption

مرجع: [ADR-017](../adr/ADR-017-encryption.md), [ADR-014](../adr/ADR-014-phi-protection.md).

## سرویس

ABP `IStringEncryptionService` برای رشته‌ها. EF Value Converter روی ستون‌های مشخص. کلید از secrets.

پیشوند نسخه روی ciphertext برای چرخش کلید: `v1:`.

## نگاشت فیلد

رمز می‌شود:

- ExaminationFinding.Text
- شرح بالینی / تشخیص
- نتایج آزمایش متنی
- NationalId (به‌علاوه HMAC blind index برای جستجوی دقیق)
- محتوای فایل Restricted قبل از Blob

رمز نمی‌شود:

- Id، تاریخ، ExamType، FitnessStatus، WorkflowState، مبالغ، نام کارمند عملیاتی

## Authorization قبل از Decrypt

Application Service فیلد PHI را فقط اگر `Health.Phi.View` باشد decrypt و در DTO می‌گذارد. در غیر این صورت DTO بدون آن فیلد است.

## فایل

`EncryptingBlobContainer` (decorator) روی container پزشکی. کلید همان key ring.

## Backup

بدون backup کلید، PHI غیرقابل بازیابی است. Runbook: SQL + volume فایل + secrets جدا.
