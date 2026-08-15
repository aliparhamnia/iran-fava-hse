# Validation

## چهار سطح

1. **UI** — الزامی بودن، طول، فرمت؛ UX فوری
2. **Application** — FluentValidation روی DTO ورودی AppService (یکپارچگی ABP)
3. **Domain** — invariant؛ منبع حقیقت قانون کسب‌وکار
4. **Database** — nullability، unique، check، FK

UI به‌تنهایی کافی نیست. Database به‌تنهایی کافی نیست.

## FluentValidation

- یک Validator per DTO/ورودی AppService
- ABP به‌صورت خودکار Validator را اجرا می‌کند
- پیام‌ها از Localization (`fa` / `en`)

## Domain

متدهای Aggregate `Result` یا exception دامنه‌ای مشخص (`BusinessRuleViolation`) برمی‌گردانند. Setter عمومی که invariant را دور بزند وجود ندارد.
