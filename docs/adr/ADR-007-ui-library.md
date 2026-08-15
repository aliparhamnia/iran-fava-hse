# ADR-007 — UI Component Library

- Status: Accepted
- Date: 2026-08-15

## Context

Frontend Blazor Web App است، فارسی RTL، پنل سازمانی با فرم، Grid، Dialog، Wizard، Upload، DatePicker. TypeScript فقط در موارد ضروری.

## Problem

کدام کتابخانهٔ کامپوننت برای سرعت ساخت، RTL، و ظاهر حرفه‌ای مناسب است بدون قفل گران‌قیمت؟

## Options

1. **MudBlazor**  
   جامعه بزرگ، Material، فرم و Dialog و Stepper قوی، `MudRTLProvider`، تقریباً بدون JS. DataGrid خوب ولی نه قوی‌ترین. RTL گاهی نیاز به CSS دارد.

2. **Radzen.Blazor (MIT components)**  
   DataGrid بسیار قوی، RTL رسمی، مناسب data-entry. ظاهر و تم نسبت به Mud محدودتر/متفاوت. ترکیب دو کتابخانه CSS را پیچیده می‌کند اگر همه‌جا پخش شود.

3. **Fluent UI Blazor**  
   ظاهر Microsoft 365، a11y خوب. Grid ضعیف‌تر برای خط کسب‌وکار. RTL فارسی اولویت محصول آن‌ها نیست.

4. **Syncfusion / Telerik**  
   Grid و Chart تجاری عالی. هزینه، لایسنس، و قفل فروشنده برای v1 نامناسب.

5. **بدون کتابخانه (HTML/CSS خودمان)**  
   کنترل کامل. زمان ساخت پنل Enterprise غیرقابل قبول.

## Decision

**MudBlazor به‌عنوان کتابخانهٔ UI در قالب ABP 10.5+ (`--blazor-ui-library mudblazor`) با تم Basic OSS.**

LeptonX تجاری استفاده نمی‌شود مگر لایسنس جدا. کنترل‌های کسب‌وکار پشت wrapper (`AppDataGrid`, `AppDialog`, `AppDatePicker`). اگر Grid خیلی پیچیده شد، Radzen فقط داخل `AppDataGrid`.

## Consequences

- مثبت: سرعت UI، RTL قابل قبول، یک زبان طراحی.
- منفی: ممکن است بعداً بخشی از Grid عوض شود — wrapper این هزینه را کم می‌کند.
- منفی: DatePicker شمسی بومی Mud نیست؛ wrapper جدا طبق ADR-016 (TD-009).
- مثبت: پشتیبانی رسمی MudBlazor در ABP 10.5.
