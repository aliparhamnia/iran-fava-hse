# Platform Contexts (Workflow, Documents, Notifications, Audit)

## Workflow

مالک Definition و Instance و History. مالک کسب‌وکار معاینه/حادثه نیست.

**Aggregates:** `WorkflowDefinition` (versioned), `WorkflowInstance`

**VO:** `StateCode`, `TransitionGuard`

**Rule:** Instance به `(EntityType, EntityId)` یکتا برای definition فعال. Transition بدون Permission لازم رد می‌شود. Domain ماژول کسب‌وکار پس از موفقیت Transition، invariant خودش را اجرا می‌کند.

جزئیات فنی: [../deployment](../deployment/strategy.md) نیست؛ [../development](../development/coding-standards.md) و سند workflow در docs مربوط به cross-cutting.

## Documents

**Aggregates:** `StoredFile` (متادیتا), لینک `FileLink` می‌تواند Entity وابسته باشد.

**VO:** `FileSensitivity` (Public/Internal/Restricted), `ContentType`, `FileHash`

**Rule:** حذف فایل Restricted فقط با Permission و اگر لینک فعال کسب‌وکاری نشکند (یا soft-delete متادیتا).

## Notifications

**Aggregates:** `InAppNotification`؛ Preference بعدی.

ارسال Email از Infrastructure + Hangfire است نه منطق دامنه غنی.

**Integration:** گوش دادن به رویدادهای `*Due`, `*Expiring`, `*Overdue`.

## Audit

ثبت `AuditEntry` از Infrastructure و Use Case. مدل دامنهٔ غنی لازم نیست؛ اما جدول و قرارداد بخشی از معماری است نه «فقط Serilog».

Write از ماژول‌های دیگر از طریق `IAuditingStore` / `IAuditLogRepository` ABP و در صورت نیاز سرویس سفارشی PHI-read در Application.Contracts ماژول Audit، نه رفرنس به Domain.
