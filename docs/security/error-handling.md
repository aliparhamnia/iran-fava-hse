# Error Handling

## انواع خطا (به Client)

| کد | HTTP | معنی |
| --- | --- | --- |
| ValidationError | 400 | ورودی |
| Unauthorized | 401 | بدون احراز |
| Forbidden | 403 | بدون Permission |
| NotFound | 404 | موجودیت |
| Conflict | 409 | RowVersion یا uniqueness |
| BusinessRuleViolation | 409 یا 422 | invariant دامنه |
| UnexpectedError | 500 | خطا داخلی |

ABP exception handling + ProblemDetails. جزئیات Exception و stack به Client نمی‌رود. Business exception با کد localize (`fa`/`en`).

## Result Pattern

Handlerها `Result<T>` برمی‌گردانند. Exception فقط برای شکست غیرمنتظره.

Blazor: Result را به پیام UI ترجمه می‌کند (Snackbar / Error state). API: map به TypedResults/ProblemDetails.

## Global

Middleware برای API. Error boundary برای Blazor. هر دو CorrelationId را نشان می‌دهند (شناسهٔ پیگیری) نه متن داخلی.
