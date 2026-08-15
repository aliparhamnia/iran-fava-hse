# Localization and Calendar

مرجع: [ADR-016](../adr/ADR-016-localization-and-calendar.md).

## زبان‌ها

| Culture | UI | جهت | تقویم نمایش |
| --- | --- | --- | --- |
| `fa` | فارسی | RTL | شمسی (Jalali) |
| `en` | English | LTR | میلادی (Gregorian) |

پیش‌فرض استقرار ایران: `fa`. کاربر می‌تواند زبان را عوض کند؛ انتخاب در cookie / پروفایل ذخیره می‌شود.

## منابع متن

هر ماژول JSON جدا در Domain.Shared:

```text
Localization/HseHealth/fa.json
Localization/HseHealth/en.json
```

کلید انگلیسی پایدار است (`MedicalExamination:Complete`). هیچ رشتهٔ کاربرنهایی در Razor هاردکد نمی‌شود.

منو، Validation، Permission display name، Workflow state display همگی localize می‌شوند.

## تاریخ

- ذخیره: `DateOnly` برای تاریخ کسب‌وکار؛ `DateTimeOffset` UTC برای رخداد
- نمایش: فقط از `AppDatePicker` / `AppDateDisplay`
- Job و Reporting روی مقدار ذخیره‌شدهٔ میلادی کار می‌کنند
- تبدیل شمسی فقط در UI

کتابخانهٔ تبدیل پشت wrapper است تا تعویض آن ADR-016 را نشکند.
