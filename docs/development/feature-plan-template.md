# Feature Plan Template

قبل از پیاده‌سازی هر Feature این قالب پر می‌شود (مثال پرشده: [../domain/first-vertical-slice.md](../domain/first-vertical-slice.md)).

```text
Feature:
Goal:
Business Rules:
Domain Changes:
Database Changes:
Application Changes:
UI Changes:
Security:
Audit:
Logging:
Tests:
Risks:
Dependencies:
```

سپس پیاده‌سازی Vertical Slice:

```text
Database → Domain → Application → Validation → Authorization → UI → Tests → Audit → Logging
```
