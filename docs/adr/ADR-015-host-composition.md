# ADR-015 — Host Composition

- Status: Accepted
- Date: 2026-08-15

## Context

Blazor در همین Solution است اما Application Boundary باید واضح بماند. Integration با سیستم‌های دیگر در آینده لازم است.

## Problem

یک Host برای UI+API، یا دو Host جدا (`Hse.Web` و `Hse.Api`)؟

## Options

1. **یک Host: Blazor Web App + Minimal APIs**  
   یک Deployable در Private Cloud. UI و API هر دو Application Service. مرز با DTO حفظ می‌شود. عملیات ساده‌تر.

2. **دو Host جدا**  
   مقیاس و انتشار مستقل UI و API. دو Pipeline، دو CORS، دو نسخه در v1 اضافی است. برای WASM عمومی معنی‌دارتر است.

3. **فقط Blazor بدون API**  
   Integration بعدی نیازمند شکافتن Host در عجله.

## Decision

**یک Host طبق قالب ABP غیر tiered: Blazor Web App که HttpApi ماژول‌ها را هم در همان فرآیند سرو می‌کند.**

`--tiered` در v1 خیر (سه فرآیند Auth/API/UI برای Private Cloud اولیه زیاد است).

UI و API فقط Application Service / DTO می‌بینند. Entity به کلاینت نمی‌رود.

وقتی مصرف‌کنندهٔ خارجی و مقیاس جدا شد، می‌توان به الگوی tiered ABP مهاجرت کرد.

## Consequences

- مثبت: یک تصویر Docker؛ یک ورود برای Cookie UI.
- مثبت: OpenAPI از همین Host برای Integration.
- منفی: API cookie-auth برای SPA خارجی مناسب نیست — JWT بعداً به همان Host یا split اضافه می‌شود.
- منفی: بار UI و API روی یک process در v1.
