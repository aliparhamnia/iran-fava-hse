# First Vertical Slice — Medical Examination

این سند Feature Plan قبل از Phase 3 است. Phase 2 فقط Foundation است. این slice پس از تأیید Foundation پیاده می‌شود.

## Feature

برنامه‌ریزی و تکمیل معاینه پزشکی شغلی

## Goal

یک مسیر واقعی end-to-end که Identity، Organization/Employee، Permission/PHI، Workflow، File، Audit، و الگوی Vertical Slice را مجبور کند درست کار کنند — بدون ساختن چهار ماژول ناقص.

## Business Rules

1. معاینه فقط برای Employee فعال (پیش‌فرض).
2. نوع معاینه الزامی است (catalog).
3. DueDate یا ExamDate برنامه‌ریزی‌شده الزامی است.
4. Complete بدون FitnessStatus ممکن نیست.
5. FitWithRestriction بدون ثبت حداقل یک WorkRestriction یا متن محدودیت مجاز نیست (اگر WorkRestriction در همین slice نیامد: فیلد RestrictionSummary اجباری روی FitnessAssessment).
6. Finding و شرح بالینی PHI است.
7. فایل پیوست معاینه Restricted است.
8. Concurrency: RowVersion؛ Conflict به UI.

## Domain Changes

- Aggregates: `HealthProfile` (حداقلی، ایجاد خودکار در صورت نبود)، `MedicalExamination`
- Entities: `FitnessAssessment`, `ExamAttachmentRef`, `ExaminationFinding` (می‌تواند در slice اول یک فیلد Findings روی exam بماند اگر Anemic نشود — ترجیح: Entity جدا حتی اگر UI ساده باشد)
- VOs: `ExamType`, `FitnessStatus`
- Events: Scheduled, Completed, FitnessForWorkChanged
- Invariantها در خود Aggregate

از Anemic Model اجتناب: `exam.Complete(fitness, findings, actor)` نه setter عمومی روی Status.

## Database Changes

Schema `health`:

- `HealthProfiles`
- `MedicalExaminations`
- `FitnessAssessments`
- `ExaminationFindings`
- `ExamAttachmentRefs`

ایندکس: EmployeeId+ExamDate، Status/WorkflowState، DueDate، TenantId.

Migration در ماژول Health.

## Application Changes

Features:

- `ScheduleMedicalExamination`
- `SubmitMedicalExamination` (اگر draft جدا باشد)
- `CompleteMedicalExamination`
- `ListMedicalExaminations` (pagination, filter, search)
- `GetMedicalExamination` (دو DTO بر اساس PHI)

FluentValidation + ABP Authorization روی AppService. Decrypt PHI فقط با `Health.Phi.View`. DTO جدا برای لیست بدون PHI.

## UI Changes

- منوی بهداشت و سلامت (Permission + localize fa/en)
- لیست معاینات: Grid، فیلتر سررسید، paging
- فرم ایجاد با `AppDatePicker` (شمسی/میلادی بر اساس فرهنگ)
- صفحه جزئیات + Complete dialog/wizard
- Upload فایل Restricted (رمز در storage)
- مخفی‌سازی PHI
- حالت‌های Loading/Empty/Error
- نمایش Conflict همزمانی

## Security

- `Health.MedicalExam.View|Create|Update|Complete`
- `Health.Phi.View` برای یافته/فایل/جزئیات بالینی
- Route و API هر دو Authorize
- Anti-forgery پیش‌فرض ABP
- یافته‌ها در SQL ciphertext؛ فایل پیوست encrypted blob

## Audit

- Create, Update, Complete: Old/New (PHI ماسک در صورت نیاز)
- Get با PHI: read audit

## Logging

- Information: examId, employeeId, action, userId, correlationId
- بدون یافته، کد ملی، نام فایل اصلی حاوی دادهٔ حساس

## Tests

- Unit: Complete بدون Fitness شکست؛ Complete با Fitness رویداد می‌سازد
- Unit: PHI در integration event نیست
- Architecture: Health.Domain به Infrastructure/UI رفرنس ندارد
- Integration: schedule → persist → complete → query list

## Risks

- تقویم وابسته به فرهنگ در فرم تاریخ (ADR-016)
- رمزنگاری یافته‌ها و فایل Restricted (ADR-017)
- Workflow seed باید قبل از Complete موجود باشد
- Employee lookup همگام از Organization.Contracts

## Dependencies

- Phase 2 Foundation کامل (ABP Host)
- Employee حداقل یک رکورد seed/قابل ثبت
- `IBlobContainer` پزشکی
- IWorkflowEngine با definition `health.medical-examination`
- Permission seed
- Localization fa/en برای صفحات slice
- Encryption options پیکربندی‌شده

## Out of slice

واکسیناسیون، مواجهه، برنامه سالانه، ارجاع، Lab result، داشبورد KPI، CAPA از روی معاینه.

## Definition of Done

طبق [../development/definition-of-done.md](../development/definition-of-done.md).
