# ABP Implementation Conventions

مرجع: [ADR-018](../adr/ADR-018-abp-framework.md).

در تولید، قرارداد ABP بر BuildingBlocks سفارشی مقدم است. چرخ زیرساخت ABP دوباره ساخته نمی‌شود.

## نسخه و قالب

- ABP Framework **10.x** روی **.NET 10**
- OSS؛ تم Basic؛ UI library MudBlazor
- غیر tiered در v1

```text
abp new Hse.Platform -t app -u blazor-webapp --blazor-ui-library mudblazor --theme basic -d ef -dbms sqlserver --mobile none
```

ماژول جدید:

```text
abp new Hse.Health -t module -u blazor --blazor-ui-library mudblazor
```

سپس ماژول به Host اضافه می‌شود (`DependsOn`).

## لایه‌های هر ماژول

| پروژه | مسئولیت |
| --- | --- |
| Domain.Shared | Constants، Enums، Localization JSON، Error codes |
| Domain | Aggregate، VO، Domain Service، Domain Event |
| Application.Contracts | Interface سرویس، DTO، Permission names، Integration Event |
| Application | Application Service، mapping، Validation |
| EntityFrameworkCore | DbContext، configuration، repository impl |
| HttpApi | Controllers / API |
| Blazor | صفحات و کامپوننت‌های ماژول |

## قواعد کد ABP که الزامی است

- کلاس ماژول: `{Module}HttpApiModule` و مشابه طبق قالب
- Entity: `AggregateRoot<Guid>` / `Entity<Guid>` با `IGuidGenerator`
- Multi-tenant: `IMultiTenant` روی Rootها
- Audited: `FullAuditedAggregateRoot` برای موجودیت‌های حساس فرآیندی
- Permission: `PermissionDefinitionProvider` — کد پایدار مثل `Health.MedicalExam.View`
- Localization: `Hse/Localization/{Module}/fa.json` و `en.json`
- Application Service: `ApplicationService`؛ `[Authorize(HealthPermissions.MedicalExam.Default)]`
- Repository: `IRepository<T, Guid>` پیش‌فرض؛ سفارشی فقط با دلیل
- رویداد: `ILocalEventBus` / `IDistributedEventBus`
- Blob: `IBlobContainer<HealthExamFiles>`
- Job: `AsyncBackgroundJob<TArgs>`
- Setting: فقط برای config کسب‌وکار؛ راز در Setting plaintext ممنوع

## چه چیزی ممنوع است

- MediatR موازی با Application Service
- Permission engine دوم
- DbContext خدا برای همهٔ ماژول‌ها (هر ماژول DbContext خودش را دارد؛ می‌تواند به همان SQL وصل شود)
- Entity در Blazor
- تاریخ شمسی در Domain/SQL
- PHI plaintext در SQL یا Log

## Vertical Slice

پوشهٔ Feature داخل Application مجاز است:

```text
Application/MedicalExaminations/ScheduleMedicalExamination/
  ScheduleMedicalExaminationDto.cs
  MedicalExaminationAppService.cs (یا سرویس جدا per feature اگر بزرگ شد)
```

یک God AppService برای کل ماژول نه. سرویس per aggregate/feature.

## Architecture Tests

علاوه بر قوانین قبلی:

- Domain به EF/HttpApi/Blazor رفرنس ندارد
- Application.Contracts به EF رفرنس ندارد
- ماژول A.Domain به ماژول B.Domain رفرنس ندارد (مگر Shared تعریف‌شده)
