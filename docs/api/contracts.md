# Application Contracts and API

مرجع: [ADR-015](../adr/ADR-015-host-composition.md).

## مرز

حتی با Blazor داخل Host:

- DTO ≠ EF Entity ≠ Aggregate
- UI Application Service را صدا می‌زند (in-process در Blazor Server) با همان DTO که HttpApi استفاده می‌کند
- HttpApi کنترلرهای ABP برای Integration
- OpenAPI در Development؛ در Production محدود/محافظت‌شده
- MediatR استفاده نمی‌شود

## Pagination استاندارد

هر لیست:

```text
Page (1-based)
PageSize (سقف مثلاً 100)
Sort
Filter
Search
```

پاسخ:

```text
Items
TotalCount
Page
PageSize
```

بدون Pagination روی dataset بزرگ = باگ معماری؛ در code review رد شود.

## نسخه‌گذاری API

v1 مسیر `/api/v1/...`. شکستن قرارداد = نسخهٔ جدید.

## Idempotency

POSTهای حساس (Complete, Approve) می‌توانند `Idempotency-Key` بگیرند وقتی Integration خارجی آمد. UI in-process همان Command یک‌بار در click.
