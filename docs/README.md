# مستندات HSE Management Platform

این پوشه منبع حقیقت معماری است. قبل از هر تغییر مهم در کد (پس از شروع Phase 2)، ابتدا همین اسناد و ADRهای مرتبط را بخوانید.

## ساختار

```text
docs/
├── architecture/     Vision, Scope, Modules, NFR, Roadmap, Risks
├── adr/              Architecture Decision Records
├── domain/           Bounded Contexts, Aggregates, Events, First Slice
├── database/         Schema, naming, conventions
├── security/         AuthN, AuthZ, PHI, errors
├── api/              Application contracts, pagination
├── deployment/       Private Cloud, Docker, CI/CD
└── development/      Standards, Git, DoD, testing, logging
```

## نقشهٔ خروجی‌های Phase 1

| # | خروجی | فایل |
| --- | --- | --- |
| 1 | Executive Architecture Overview | [architecture/overview.md](architecture/overview.md) |
| 2 | Proposed Solution Structure | [architecture/solution-structure.md](architecture/solution-structure.md) |
| 3 | Module Architecture | [architecture/modules.md](architecture/modules.md) |
| 4 | Bounded Context Map | [domain/bounded-contexts.md](domain/bounded-contexts.md) |
| 5 | Domain Model اولیه | [domain/README.md](domain/README.md) |
| 6 | Database Architecture | [database/strategy.md](database/strategy.md) |
| 7 | Security Architecture | [security/README.md](security/README.md) |
| 8 | Workflow Architecture | [development/workflow.md](development/workflow.md) |
| 9 | Audit Architecture | [development/audit.md](development/audit.md) |
| 10 | File Management | [development/files.md](development/files.md) |
| 11 | Notification | [development/notifications.md](development/notifications.md) |
| 12 | Reporting | [development/reporting.md](development/reporting.md) |
| 13 | Testing Strategy | [development/testing.md](development/testing.md) |
| 14 | Deployment | [deployment/strategy.md](deployment/strategy.md) |
| 15 | ADR List | [adr/README.md](adr/README.md) |
| 16 | Development Roadmap | [architecture/roadmap.md](architecture/roadmap.md) |
| 17 | اولین Vertical Slice | [domain/first-vertical-slice.md](domain/first-vertical-slice.md) |
| 18 | ریسک‌ها | [architecture/risks.md](architecture/risks.md) |
| 19 | Technical Debt | [architecture/technical-debt.md](architecture/technical-debt.md) |
| 20 | سؤالات Business | [architecture/questions.md](architecture/questions.md) |

تصمیم‌های بعدی قفل‌شده: [ADR-016 Localization](adr/ADR-016-localization-and-calendar.md)، [ADR-017 Encryption](adr/ADR-017-encryption.md)، [ADR-018 ABP](adr/ADR-018-abp-framework.md).

## خواندن پیشنهادی

1. [architecture/overview.md](architecture/overview.md)
2. [architecture/scope.md](architecture/scope.md)
3. [architecture/modules.md](architecture/modules.md)
4. [adr/README.md](adr/README.md)
5. [domain/bounded-contexts.md](domain/bounded-contexts.md)
6. [architecture/roadmap.md](architecture/roadmap.md)
7. [architecture/phase-gate.md](architecture/phase-gate.md)

## قانون عامل / توسعه‌دهنده

قبل از هر تغییر مهم:

1. Repository و معماری موجود را بررسی کن.
2. Documentation را بخوان.
3. وابستگی‌ها را بررسی کن.
4. تغییرات پیشنهادی را توضیح بده و Impact Analysis انجام بده.
5. سپس کدنویسی کن.

کد موجود را بی‌دلیل Rewrite نکن. Requirement مبهم را حدس نزن؛ سؤال بپرس.
