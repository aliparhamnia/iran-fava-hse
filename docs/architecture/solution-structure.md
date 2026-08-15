# Solution Structure

استاندارد تولید: [ADR-018](../adr/ADR-018-abp-framework.md) و [../development/abp-conventions.md](../development/abp-conventions.md).

ADR-011 (دو پروژه per module) منسوخ است.

## Host و ماژول‌ها

```text
hse/
├── docs/
├── src/
│   ├── Hse.Platform.Domain.Shared
│   ├── Hse.Platform.Domain
│   ├── Hse.Platform.Application.Contracts
│   ├── Hse.Platform.Application
│   ├── Hse.Platform.EntityFrameworkCore
│   ├── Hse.Platform.HttpApi
│   ├── Hse.Platform.HttpApi.Client
│   ├── Hse.Platform.Blazor                      # Blazor Web App host (UI + API)
│   ├── Hse.Platform.DbMigrator
│   └── modules/
│       ├── Hse.Organization.*
│       ├── Hse.Health.*
│       ├── Hse.Prevention.*
│       ├── Hse.Insurance.*
│       ├── Hse.Environment.*
│       ├── Hse.Actions.*
│       ├── Hse.Documents.*
│       ├── Hse.Notifications.*
│       └── Hse.Workflow.*
├── test/
│   ├── Hse.Architecture.Tests
│   ├── Hse.Health.Domain.Tests
│   ├── Hse.Health.Application.Tests
│   └── Hse.Platform.EntityFrameworkCore.Tests
├── docker-compose.yml
└── NuGet.Config
```

Identity، AuditLogging، Permission، Blob، BackgroundJobs، SettingManagement از پکیج‌های `Volo.Abp.*` می‌آیند نه ماژول بازنویسی‌شده.

Organization در ABP Identity درخت OU دارد؛ ماژول `Hse.Organization` Employee و Position و نوع واحد قابل‌پیکربندی را اضافه می‌کند و در صورت امکان به OU هویت لینک می‌شود.

## لایه‌های هر ماژول کسب‌وکار

```text
Hse.Health.Domain.Shared
Hse.Health.Domain
Hse.Health.Application.Contracts
Hse.Health.Application
Hse.Health.EntityFrameworkCore
Hse.Health.HttpApi
Hse.Health.Blazor
```

## قانون رفرنس (Architecture Tests)

```text
Domain ↛ EF / HttpApi / Blazor
Application.Contracts ↛ EF / Blazor
Application → Domain + Contracts
HttpApi → Application.Contracts
Blazor → Application.Contracts (+ HttpApi.Client اگر جدا شد)
ModuleA.Domain ↛ ModuleB.Domain
```

Host (`Hse.Platform.Blazor`) ماژول‌ها را `DependsOn` می‌کند.
