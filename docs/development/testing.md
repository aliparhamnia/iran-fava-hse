# Testing Strategy

## هرم

1. **Unit** — قوانین دامنه، Guardهای Workflow، Result mapping. بدون DB.
2. **Integration** — Testcontainers SQL Server؛ یک slice کامل.
3. **Architecture** — NetArchTest یا ArchUnitNET.
4. **E2E** — پس از چند slice؛ مسیرهای حیاتی. bUnit برای کامپوننت‌های wrapper.

## Architecture Tests (حداقل)

- Domain به EF/HttpApi/Blazor وابسته نباشد
- Application.Contracts به EF/Blazor وابسته نباشد
- ماژول A.Domain به ماژول B.Domain رفرنس نداشته باشد
- هیچ پروژه‌ای جز Hostهای قالب به پروژهٔ Blazor Host وابسته نباشد

## Integration

- Testcontainers Microsoft SQL Server
- اعمال migrations
- یک تست: schedule exam → complete → list

## دادهٔ تست

PHI ساختگی؛ نه دادهٔ واقعی تولید.

## CI

هر PR: unit + architecture + integration (اگر runner بتواند Docker). اگر Integration در CI اولیه گران بود، job جدا با علامت — هدف: Integration از همان Phase 2 سبز شود.

## پوشش

قانون مهم کسب‌وکار بدون Unit Test در DoD پذیرفته نیست. پوشش درصدی اجباری سراسری در v1 تعیین نمی‌شود تا عدد توخالی نشود.
