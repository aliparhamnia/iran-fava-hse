# Coding Standards

- Clean, readable, maintainable, SOLID, testable, consistent
- Abstraction بی‌دلیل، Generic پیچیده، و Pattern نمایشی ممنوع
- زبان کد: انگلیسی؛ UI از Localization `fa`/`en` — هیچ متن کاربرنهایی هاردکد نیست
- قرارداد ABP: ApplicationService، Permission، Event Bus، Blob — نه موازی‌سازی با MediatR / IFileStorage
- تاریخ در دامنه میلادی؛ تبدیل شمسی فقط در UI
- Async با پسوند Async و CancellationToken در I/O
- Nullable enable
- فایل‌های کوچک؛ Feature folder

## دامنه

- غنی؛ نه Anemic
- بدون رفرنس به EF/Blazor/HTTP
- رویداد برای اتفاق مهم

## نام‌گذاری C#

- PascalCase نوع و public member
- `_camelCase` فیلد خصوصی اگر استفاده شد؛ primary constructor ترجیح .NET 10 وقتی خوانا است

## Git

طبق [git.md](git.md). Commit کوچک و معنادار. بدون commit تا درخواست کاربر — این قاعدهٔ عامل است نه محصول.

## بازنویسی

کد موجود بی‌دلیل Rewrite نمی‌شود. اگر طراحی برای آینده کافی نبود: Technical Debt یا ADR، پنهان‌کاری ممنوع.
