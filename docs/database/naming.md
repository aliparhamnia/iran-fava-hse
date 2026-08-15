# Database Naming Convention

## تصمیم

به‌جای `Health_MedicalExams` در `dbo`، از **schema + PascalCase table** استفاده می‌شود.

دلیل: هم‌خوانی با استخراج ماژول، خوانایی در SSMS، عدم تکرار نام ماژول در هر جدول.

## قواعد

| جزء | قاعده | مثال |
| --- | --- | --- |
| Schema | lowercase کوتاه | `health` |
| Table | PascalCase جمع | `MedicalExaminations` → `health.MedicalExaminations` |
| Column | PascalCase | `EmployeeId`, `CreatedAtUtc` |
| PK | `PK_{Table}` | `PK_MedicalExaminations` |
| FK | `FK_{Table}_{RefTable}_{Col}` | `FK_MedicalExaminations_HealthProfiles_HealthProfileId` |
| Index | `IX_{Table}_{Cols}` | `IX_MedicalExaminations_EmployeeId_ExamDate` |
| Unique | `UX_{Table}_{Cols}` | `UX_Employees_TenantId_EmployeeNumber` |
| Check | `CK_{Table}_{Meaning}` | `CK_InsurancePolicies_DateRange` |

## زمان

- Instant: `DateTimeOffset` UTC با پسوند `Utc` در نام اگر `DateTime` استفاده شد؛ ترجیح `DateTimeOffset`
- تاریخ تقویمی کسب‌وکار بدون ساعت: `DateOnly` (`DueDate`, `ExamDate`)

## ممنوع

- `tbl` prefix
- `sp_` برای رویه‌های خودمان
- نام فارسی جدول/ستون
- `nvarchar(max)` بی‌دلیل برای کد و status

## نمونه چهار ماژول

```text
health.HealthProfiles
health.MedicalExaminations

prev.Incidents
prev.Inspections

ins.Policies
ins.Claims

env.WasteRecords
env.Inspections
```

`CorrectiveActions` در `act` است نه `prev`.
