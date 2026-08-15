# Frontend Architecture

## اصول

- Frontend به EF Core یا Database دسترسی ندارد.
- UI از Application Service / HttpApi Client استفاده می‌کند (استاندارد ABP).
- Entity دیتابیس هرگز به کامپوننت داده نمی‌شود.
- Component-based با Loading / Empty / Error.
- همهٔ متن‌های کاربر از Localization؛ هیچ رشتهٔ فارسی/انگلیسی هاردکد در Razor.

## Rendering

طبق [ADR-008](../adr/ADR-008-blazor-rendering.md): قالب **Blazor Web App**؛ صفحات عملیاتی **Interactive Server**.

## UI Library

طبق [ADR-007](../adr/ADR-007-ui-library.md): **MudBlazor** داخل ABP (`MudBlazorBasicTheme`).

## زبان و جهت

طبق [ADR-016](../adr/ADR-016-localization-and-calendar.md):

| فرهنگ | جهت | تقویم DatePicker |
| --- | --- | --- |
| `fa` | RTL (`MudRTLProvider`) | شمسی |
| `en` | LTR | میلادی |

تعویض زبان در runtime منوی کاربر. `AppDatePicker` تنها ورودی تاریخ کسب‌وکار است.

## کامپوننت‌های اشتراکی

- Layout + Permission-filtered menu (ABP `IMenuContributor`)
- Data Grid wrapper
- Modal / Dialog
- Wizard / Stepper
- File Upload
- `AppDatePicker` / `AppDateDisplay`
- Search / Filter / Pagination
- Validation display (پیام localize)
- Notification (toast + in-app)
- Loading / Empty / Error
- Language switch

## Validation در UI

فقط UX. قانون کسب‌وکار در Domain. FluentValidation در Application طبق یکپارچگی ABP.
