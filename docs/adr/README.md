# Architecture Decision Records

هر تصمیم معماری مهم یک ADR دارد.

قالب اجباری:

```text
Context
Problem
Options
Decision
Consequences
```

وضعیت: Accepted = تصمیم نسخهٔ اول. Superseded وقتی ADR جدید جایگزین شود.

## فهرست

| ID | عنوان | تصمیم |
| --- | --- | --- |
| [ADR-001](ADR-001-modular-monolith.md) | سبک سیستم | Modular Monolith |
| [ADR-002](ADR-002-database-strategy.md) | دیتابیس | یک DB + Schema per module |
| [ADR-003](ADR-003-authentication.md) | احراز هویت | ABP Identity محلی |
| [ADR-004](ADR-004-multi-tenancy.md) | چندمستأجری | ABP Tenant + data filter |
| [ADR-005](ADR-005-file-storage.md) | فایل | ABP Blob + File System |
| [ADR-006](ADR-006-workflow-engine.md) | Workflow | Configurable state machine |
| [ADR-007](ADR-007-ui-library.md) | UI Kit | MudBlazor در قالب ABP |
| [ADR-008](ADR-008-blazor-rendering.md) | Rendering | Blazor Web App / Interactive Server |
| [ADR-009](ADR-009-background-jobs.md) | Job | ABP + Hangfire |
| [ADR-010](ADR-010-primary-key-strategy.md) | PK | ABP sequential Guid |
| [ADR-011](ADR-011-module-project-shape.md) | شکل پروژه | **Superseded** توسط ADR-018 |
| [ADR-012](ADR-012-cross-module-communication.md) | ارتباط ماژول | ABP Local/Distributed Event Bus |
| [ADR-013](ADR-013-reporting.md) | گزارش | جدا از write model |
| [ADR-014](ADR-014-phi-protection.md) | PHI | Permission + mask + read-audit + encryption |
| [ADR-015](ADR-015-host-composition.md) | Host | ABP app غیر tiered |
| [ADR-016](ADR-016-localization-and-calendar.md) | زبان و تقویم | fa شمسی / en میلادی |
| [ADR-017](ADR-017-encryption.md) | رمزنگاری | IStringEncryptionService + فایل Restricted |
| [ADR-018](ADR-018-abp-framework.md) | استاندارد تولید | ABP Framework 10 OSS |
