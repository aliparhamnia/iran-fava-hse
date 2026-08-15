# ADR-018 — ABP Framework as Implementation Standard

- Status: Accepted
- Date: 2026-08-15

## Context

معماری محصول Modular Monolith + DDD + Clean Architecture است. تصمیم محصول: **استاندارد ABP در تولید پیاده شود.** ABP Framework 10 روی .NET 10 است، Modular Monolith و DDD را رسمی پشتیبانی می‌کند، و از 10.5 MudBlazor برای Blazor Web App دارد.

## Problem

هسته را از صفر بنویسیم (BuildingBlocks سفارشی + MediatR) یا روی ABP بسازیم؟

## Options

1. **هستهٔ سفارشی** (طرح اولیهٔ ADR-011)  
   کنترل کامل. تکرار Identity، Permission، Audit، Localization، UoW، Blob، Multi-tenancy.

2. **ABP Framework OSS (لایه‌ای / Modular Monolith) + ماژول‌های کسب‌وکار خودمان**  
   استاندارد سازمانی .NET؛ ماژول آمادهٔ Identity/Permission/Audit/Blob/Localization. ساختار پروژهٔ ABP را می‌پذیریم.

3. **ABP Commercial / Suite / LeptonX**  
   سرعت تولید CRUD. لایسنس و قفل تم. برای v1 لازم نیست مگر سازمان لایسنس داشته باشد.

## Decision

**گزینه ۲: ABP Framework 10 OSS به‌عنوان استاندارد تولید.**

لایسنس Commercial فرض نیست. تم: **Basic + MudBlazor** (`--blazor-ui-library mudblazor --theme basic`).

### قالب Phase 2

```text
abp new Hse.Platform -t app -u blazor-webapp --blazor-ui-library mudblazor --theme basic -d ef -dbms sqlserver --mobile none
```

ماژول‌های کسب‌وکار با قالب `module` و همان UI library اضافه می‌شوند.

### چه چیزی از ABP استفاده می‌شود (بازنویسی ممنوع)

| قابلیت | مکانیزم ABP |
| --- | --- |
| Modularity / DI | `AbpModule` |
| DDD building blocks | Entity, AggregateRoot, DomainService, Repository |
| Application API | Application Service + DTO (نه MediatR به‌عنوان استاندارد) |
| Validation | DataAnnotations + FluentValidation integration ABP |
| Authorization | Permission system (`PermissionDefinitionProvider`) |
| Identity | `Volo.Abp.Identity` (محلی، مطابق ADR-003) |
| Multi-tenancy | ABP tenant + data filter (تک‌تننت v1) |
| Audit | `Volo.Abp.AuditLogging` + audit سفارشی PHI read |
| Localization | JSON resources `fa` / `en` |
| Blob | `IBlobContainer` + File System provider |
| Jobs | ABP Background Jobs + Hangfire integration |
| Events | Local Event Bus؛ Distributed later |
| UoW / Data filter | پیش‌فرض ABP |
| GUID | `IGuidGenerator` متوالی SQL Server |
| Exception / CorrelationId | پیش‌فرض ABP |
| Encryption | `IStringEncryptionService` (ADR-017) |

### چه چیزی سفارشی می‌ماند

- موتور Workflow قابل‌پیکربندی (ماژول `Hse.Workflow`) — Elsa در ABP تجاری است و در v1 نمی‌آید
- تقویم شمسی/میلادی وابسته به فرهنگ (`AppDatePicker`)
- رمزنگاری PHI و فایل Restricted روی Value Converter / blob
- ماژول‌های Health, Prevention, Insurance, Environment, Actions, Organization (Employee/Position)
- Reporting read model

### MediatR

استاندارد ABP **Application Service** است نه MediatR. MediatR به Solution اضافه نمی‌شود مگر نیاز اثبات‌شده. Vertical Slice به‌صورت پوشهٔ Feature داخل لایهٔ Application همان ماژول حفظ می‌شود.

### شکل پروژهٔ ماژول (جایگزین ADR-011)

هر ماژول کسب‌وکار طبق ABP:

```text
Hse.{Module}.Domain.Shared
Hse.{Module}.Domain
Hse.{Module}.Application.Contracts
Hse.{Module}.Application
Hse.{Module}.EntityFrameworkCore
Hse.{Module}.HttpApi
Hse.{Module}.Blazor
```

پروژه‌های تست جدا. تعداد csproj بیشتر از طرح اولیه است؛ این هزینهٔ پذیرفته‌شدهٔ استاندارد ABP است.

## Consequences

- مثبت: Identity، Permission، Audit، Localization، Blob، Job، Multi-tenancy از روز اول استاندارد و تست‌شده
- مثبت: MudBlazor رسمی در ABP 10.5+
- مثبت: مسیر استخراج ماژول با قرارداد ABP مشخص است
- منفی: ADR-011 (۲ پروژه per module) منسوخ می‌شود؛ Solution شلوغ‌تر است
- منفی: یادگیری قراردادهای ABP برای تیم الزامی است
- منفی: نباید چرخ ABP را دوباره ساخت (مثلاً Permission engine دوم)
