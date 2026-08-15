# Module Architecture

## لایه‌بندی ماژول‌ها

| لایه | ماژول‌های v1 | نقش |
| --- | --- | --- |
| ABP built-in | Identity, Permission, AuditLogging, Blob, BackgroundJobs, Setting, Tenant | زیرساخت — بازنویسی ممنوع |
| Platform سفارشی | Organization, Workflow, Documents, Notifications, Actions | قابلیت مشترک محصول |
| Business | Health, Prevention, Insurance, Environment | دامنه HSE |
| Later | Training, Contractor Management, Equipment, Dashboards | بدون تغییر Host اضافه می‌شوند |

Actions (CAPA) عمداً داخل Prevention نیست.

## ساختار Solution

طبق [solution-structure.md](solution-structure.md) و [ADR-018](../adr/ADR-018-abp-framework.md).

جزئیات convention: [../development/abp-conventions.md](../development/abp-conventions.md).

## ساختار داخلی Application یک ماژول

Vertical Slice داخل `Application`:

```text
Application/MedicalExaminations/
  MedicalExaminationAppService.cs
  Schedule/
  Complete/
  List/
```

هر Feature: DTO + AppService method + Validator + Authorize.

## قواعد coupling

1. ماژول A به Domain ماژول B رفرنس نمی‌دهد.
2. تنها مرجع مجاز بین‌ماژولی: `Application.Contracts`.
3. همگام و نادر: lookup در Contracts.
4. ناهمگام و رایج: ABP Event Bus.
5. Join بین‌اسکیمایی در Domain ممنوع است.
6. گزارش‌های cross-module از Read Model / schema `rpt`.
7. Host ماژول‌ها را composition می‌کند.

## کاتالوگ منوی UI

منوی ABP (`IMenuContributor`) + Permission. کاربر ماژول یا منویی که Permission ندارد را نمی‌بیند. متن منو از Localization (`fa`/`en`) می‌آید.

```text
Dashboard
├── بهداشت و سلامت / Occupational Health      Health.*
├── پیشگیری / Prevention                      Prevention.*
├── بیمه / Insurance                          Insurance.*
├── محیط زیست / Environment                   Environment.*
├── گزارش‌ها / Reports                        Reporting.View
├── داشبوردها / Dashboards                    Dashboard.View
├── مدیریت اسناد / Documents                  Documents.View
├── اعلان‌ها / Notifications                  Notifications.View
├── Workflow                                  Workflow.View
├── Audit                                     Audit.View
└── Administration                            AbpIdentity.* / Organization.*
```
