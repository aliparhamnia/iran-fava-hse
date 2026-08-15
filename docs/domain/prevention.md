# Prevention & Safety

## Business Capabilities

1. شناسایی خطر و ارزیابی ریسک
2. بازرسی و چک‌لیست ایمنی
3. ثبت رویداد ایمنی (حادثه، شبه حادثه، عمل/شرایط ناایمن)
4. تحلیل علت ریشه‌ای در حد نیاز
5. صدور و پایش Permit to Work
6. مشاهده ایمنی و پیگیری
7. KPI ایمنی (از Reporting)

CAPA در این Context ساخته نمی‌شود؛ رویداد `CorrectiveActionRequested` به Actions می‌رود.

## Aggregates — طراحی اولیه

### Hazard

شناسایی خطر در واحد/شغل/فعالیت.

### RiskAssessment

- لینک به Hazardها
- ماتریس احتمال/شدت (مقادیر پیکربندی نه عدد جادویی در UI)
- اقدامات پیشنهادی → می‌تواند به Actions برود

### Inspection

- نوع بازرسی، چک‌لیست، یافته‌ها
- Workflow تأیید

### Incident

یک Aggregate برای همهٔ انواع رویداد ایمنی:

- Type: `Incident | NearMiss | UnsafeAct | UnsafeCondition | Accident`
- نه پنج Aggregate جدا در v1
- Severity، زمان، مکان (OrganizationUnitId)، افراد درگیر به‌صورت EmployeeId
- RCA به‌صورت Entity/VO وابسته؛ اگر رشد کرد جدا می‌شود

### PermitToWork

- نوع مجوز، بازه، شرایط
- انقضا → Notification

### SafetyObservation

- مشاهده مثبت/منفی
- می‌تواند به Incident یا Action منجر شود

## Value Objects

- `IncidentType`, `Severity`, `LocationRef`, `ChecklistScore`
- `RootCause` (دسته + شرح)

## Domain Services

- قواعد بستن Incident فقط وقتی Actions مرتبط Closed باشند — از facade/query Actions نه Join دامنه. اگر consistency سخت شد: سیاست «بستن Incident با هشدار Actions باز» در v1 قابل قبول است (مستند کن).

## Domain Events

- `IncidentReported`
- `IncidentClosed`
- `InspectionCompleted`
- `PermitIssued`
- `PermitExpiring`

## Integration Events

- `CorrectiveActionRequested` (SourceType, SourceId, Title, DueDate)
- `IncidentReported` (برای Dashboard/Reporting)
- `PermitExpiresSoon`

## Business Rules (نمونه)

- Incident بدون Type و OrganizationUnit ثبت نمی‌شود.
- Accident با Severity بالا نمی‌تواند مستقیم Closed شود بدون Review (Transition permission).
- تغییر Type پس از Submitted محدود است.

## State Machine

الگوی مشترک:

```text
draft → submitted → underReview → approved → closed
                              ↘ rejected → submitted
```

Definition جدا per entity type (`prevention.incident`, `prevention.inspection`, `prevention.permit-to-work`).
