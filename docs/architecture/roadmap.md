# Development Roadmap

روش: Vertical Slice + Incremental. Big Bang ممنوع.

## Phase 0 — Discovery

Requirements، Assumptions، Risks، Questions، Scope.

**وضعیت:** انجام‌شده در `/docs`.

## Phase 1 — Architecture

Solution structure، Module boundaries، Domain model، Database، Security، Workflow، Infrastructure docs، ADRها.

**وضعیت:** انجام‌شده در `/docs`.

## Phase 2 — Foundation

**وضعیت:** انجام‌شده در `Hse.Platform/` (بیلد و تست دامنه/معماری سبز). Phase 3 فقط پس از تأیید صریح.

هسته بدون دامنهٔ HSE، روی ABP:

- تولید قالب ABP (Blazor Web App + MudBlazor + Basic)
- Identity / Permission / Audit آمادهٔ ABP
- Localization `fa` و `en`
- `AppDatePicker` (شمسی/میلادی)
- Encryption configuration
- OrganizationUnit + Employee حداقلی
- Blob File System
- Hangfire
- Workflow ماژول اسکلت
- Docker Compose
- Architecture tests
- CI

## Phase 3 — First Vertical Slice

**Health → Medical Examination** از Database تا UI و Test.

جزئیات: [domain/first-vertical-slice.md](../domain/first-vertical-slice.md).

## Phase 4+

ترتیب پیشنهادی پس از Slice اول:

1. Fitness / Work Restriction
2. Exam due reminders (Hangfire + Notification)
3. Prevention: Incident ساده + Workflow
4. Actions/CAPA از روی Incident
5. Insurance: Policy + expiry warning
6. Environment: Incident یا Waste (یکی)
7. Dashboard widgets
8. Reporting read models

هر Feature قبل از کد یک Feature Plan دارد: [development/feature-plan-template.md](../development/feature-plan-template.md).

## Git

Conventional Commits:

```text
feat(health): add medical examination domain
feat(health): add medical examination workflow
feat(health): add medical examination UI
test(health): add medical examination tests
docs(adr): add ADR-006 workflow engine
```
