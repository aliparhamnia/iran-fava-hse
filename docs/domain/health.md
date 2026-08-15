# Occupational Health

## Business Capabilities (نه فهرست CRUD)

1. نگهداری پرونده سلامت شغلی کارمند
2. برنامه‌ریزی و اجرای معاینات (بدو استخدام، دوره‌ای، موردی)
3. تعیین Fitness for Work و محدودیت شغلی
4. ثبت واکسیناسیون مرتبط با کار
5. ثبت مواجهه و عوامل زیان‌آور (پس از slice اول)
6. پایش سررسید معاینه و یادآوری
7. گزارش سلامت با حداقل افشای PHI

## Bounded Context

مالک دادهٔ بالینی و تصمیم تناسب با کار. مالک هویت کارمند نیست.

## Aggregates — v1

### HealthProfile

- یک پروفایل per Employee
- خلاصهٔ غیرحساس: گروه خونی اختیاری؟ — اگر حساس است پشت PHI
- اشاره‌گر به آخرین Fitness شناخته‌شده (کپی تصمیم، نه تشخیص)
- موجودیت ریشه برای دسترسی پرونده

### MedicalExamination

Aggregate اصلی slice اول. جزئیات در [first-vertical-slice.md](first-vertical-slice.md).

### VaccinationRecord

- نوع واکسن، تاریخ، دوز، سررسید بعدی
- پس از slice اول

### WorkRestriction

- می‌تواند از روی Fitness با محدودیت ساخته شود
- شرح محدودیت شغلی (قابل‌دیدن برای سرپرست)
- بازه اعتبار
- تشخیص پشت آن PHI است و در این Aggregate نمی‌آید

## Aggregates — بعدی

`OccupationalExposure`, `HazardousAgent`, `ExamProgram`, `MedicalReferral`, `LabResult`

## MedicalExamination — مدل

**Root:** `MedicalExamination`

**Entities:**

- `ExaminationFinding` (PHI)
- `FitnessAssessment`
- `ExamAttachmentRef` (FileId + category)

**Value Objects:**

- `ExamType`: PreEmployment / Periodic / ReturnToWork / Special / Other (کد پایدار؛ قابل‌گسترش از catalog)
- `ExamPeriod` / due date
- `FitnessStatus`: Fit / FitWithRestriction / Unfit
- یافته‌ها به‌عنوان VO/Entity با Classification=PHI

## Domain Services

- محاسبهٔ overdue بر اساس due date و clock
- جلوگیری از معاینهٔ باز تکراری هم‌نوع در بازه (Rule قابل‌پیکربندی)

## Application Services / Features

- Schedule / Submit / Complete / Cancel
- List (صفحه‌بندی، فیلتر سررسید) با دو DTO: خلاصه vs PHI
- Attach file

## Repository

`IMedicalExaminationRepository` فقط اگر بارگذاری Aggregate با فرزندان و invariant فراتر از DbContext باشد. در غیر این صورت DbContext در Handler با Aggregate methods. ایجاد بی‌دلیل Generic Repository ممنوع.

## Domain Events

- `MedicalExamScheduled`
- `MedicalExamCompleted`
- `FitnessForWorkChanged`
- `MedicalExamBecameOverdue` (ممکن است از Job صادر شود نه از mutation کاربر)

## Integration Events (Contracts)

- `MedicalExamCompletedIntegrationEvent` (EmployeeId, FitnessStatus, ExamDate — بدون تشخیص)
- `MedicalExamDueIntegrationEvent`
- `FitnessForWorkChangedIntegrationEvent`

## Business Rules

- Complete بدون `FitnessAssessment` ممکن نیست.
- تغییر Fitness نیاز به دلیل دارد.
- Finding و فایل پزشکی فقط با `Health.Phi.View`.
- کارمند Terminated: معاینهٔ جدید فقط با Rule استثناء (مثلاً پروندهٔ حقوقی) — پیش‌فرض ممنوع.
- Optimistic concurrency روی Root (`RowVersion`).

## State Machine

Definition: `health.medical-examination`

پیشنهاد v1:

```text
draft → submitted → completed
draft → cancelled
submitted → draft (برگشت در صورت Permission)
```

`underReview` / `approved` / `rejected` در Definition قابل افزودن است بدون تغییر engine. Slice اول سه حالت اصلی را seed می‌کند مگر سؤال ۱۰ خلاف بگوید.

Workflow state جدا از FitnessStatus است. Completed یعنی فرآیند ثبت تمام شده؛ Fitness یکی از نتایج است.
