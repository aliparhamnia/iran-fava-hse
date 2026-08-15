# Logging Architecture

## Stack

Serilog structured logging. Sinks v1: Console + File یا Seq در Private Cloud اگر موجود باشد.

OpenTelemetry وقتی نیاز عملیاتی شد (TD-006) — از روز صفر اجباری نیست اما `TraceId` در Log enrich می‌شود تا اتصال بعدی آسان باشد.

## هر Request

- `CorrelationId` (ساخته یا از header `X-Correlation-ID`)
- `TraceId` / `SpanId` اگر Activity موجود باشد
- `UserId` اگر authenticated
- `TenantId`

## سطوح

- Information: شروع/پایان use case موفق با شناسه‌ها
- Warning: BusinessRule رد شده از نظر عملیاتی تکراری، retry
- Error: Unexpected با Exception داخل سرور

## ممنوع در پیام و properties

PHI، رمز، token، کد ملی، شماره کامل کارت/بیمه‌نامه، محتویات فایل.

## Request logging

Body لاگ نمی‌شود. Path و status و duration بله.
