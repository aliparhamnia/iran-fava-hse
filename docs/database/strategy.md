# Database Strategy

مرجع تصمیم: [ADR-002](../adr/ADR-002-database-strategy.md), [ADR-010](../adr/ADR-010-primary-key-strategy.md).

## اصول

- SQL Server + EF Core
- یک database در v1
- Schema per module + DbContext per module
- Migration per module؛ Host همه را اعمال می‌کند
- Repository سفارشی فقط وقتی query خاص ارزش معماری دارد؛ در غیر این صورت `IRepository<T, Guid>` ABP
- UoW استاندارد ABP؛ تراکنش بین‌دو DbContext ممنوع؛ رویداد ABP برای side-effect

## Schemaها

| Schema | ماژول |
| --- | --- |
| `idt` | Identity |
| `org` | Organization |
| `health` | Health |
| `prev` | Prevention |
| `ins` | Insurance |
| `env` | Environment |
| `act` | Actions |
| `doc` | Documents |
| `wf` | Workflow |
| `aud` | Audit (+ Outbox می‌تواند اینجا یا `ntf` باشد؛ تصمیم: `aud.OutboxMessages` برای رویدادهای یکپارچهٔ زیرساخت یا جدول outbox per module — ترجیح **per module** `OutboxMessages` داخل همان schema برای تراکنش محلی) |
| `ntf` | Notifications |
| `rpt` | Reporting read models (بعداً) |

Hangfire جداول خودش را در schema `hangfire` یا پیش‌فرض خود می‌سازد — با prefix مشخص تا با دامنه قاطی نشود.

## کلید، ایندکس، محدودیت

- PK: `uniqueidentifier` از `IGuidGenerator` ABP، ستون `Id`
- ستون‌های متنی PHI: `nvarchar` ciphertext؛ جستجوی LIKE روی آن‌ها طراحی نمی‌شود
- Blind index HMAC برای کد ملی در صورت نیاز به exact lookup
- FK با نام `FK_{Table}_{RefTable}_{Col}`
- Unique: `UX_{Table}_{Cols}`
- Index: `IX_{Table}_{Cols}`
- ایندکس اجباری الگو: همهٔ FKها، فیلتر لیست (`WorkflowState`, `DueDate`, `OrganizationUnitId`)، `TenantId`

## Concurrency

`RowVersion` `rowversion` / `byte[]` IsRowVersion روی Aggregate Rootهای قابل ویرایش همزمان. Conflict = خطای `Conflict` به UI.

## Soft Delete

فقط مرجع پایدار: Employee، Policy، OrganizationUnit. موجودیت فرآیندی: state دامنه + در صورت نیاز legal hold. فیلتر سراسری Soft Delete فقط روی همان موجودیت‌ها.

## Audit ستون‌های فنی (غیر از aud.AuditEntries)

روی Rootها:

- `CreatedAtUtc`, `CreatedBy`
- `ModifiedAtUtc`, `ModifiedBy`
- `TenantId`

این جایگزین Audit Trail نیست.

## Seed

- Permissions
- Roleهای پایه و نگاشت
- SuperAdmin اولیه از configuration (رمز از env)
- OrganizationUnit ریشه
- Workflow definitions پایه از جمله `health.medical-examination`
- Tenant پیش‌فرض

## Performance

- Pagination اجباری
- Projection در Query
- `AsNoTracking` برای خواندن
- Lazy Loading خاموش
- Include آگاهانه؛ بدون N+1

نمونهٔ جداول slice اول: [naming.md](naming.md) و [../domain/first-vertical-slice.md](../domain/first-vertical-slice.md).
