# Configuration Architecture

## لایه‌ها

1. `appsettings.json` — غیرحساس
2. `appsettings.{Environment}.json`
3. Environment variables / Docker secrets
4. User Secrets در Development

## Options pattern

کلاس‌های Options اعتبارسنجی‌شده در startup (`IValidateOptions`). شکست config = عدم شروع Host.

## بخش‌های اصلی

- ConnectionStrings
- StringEncryption passphrase
- FileStorage / Blob root path
- Identity password policy
- Hangfire
- SMTP (optional)
- Seed admin
- Tenant default id
- Upload limits
- Audit retention

## Feature flags

در v1 حداقل. ماژول خاموش از composition حذف می‌شود نه flag پراکنده مگر نیاز واقعی.

## چندسازمانی

`TenantId` پیش‌فرض در config تا فیلتر آینده.
