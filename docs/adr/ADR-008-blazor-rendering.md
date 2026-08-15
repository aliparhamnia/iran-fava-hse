# ADR-008 — Blazor Interactive Rendering Mode

- Status: Accepted
- Date: 2026-08-15

## Context

.NET 10 Blazor Web App چند حالت دارد: SSR، Interactive Server، WASM، Auto. پنل داخلی Private Cloud با دادهٔ پزشکی است.

## Problem

کدام حالت برای صفحات عملیاتی پیش‌فرض باشد؟

## Options

1. **Interactive Server**  
   منطق روی سرور؛ Circuit؛ دادهٔ PHI در مرورگر به‌صورت مدل WASM کامل نیست؛ مناسب اینترانت. مقیاس افقی نیاز به sticky/Redis دارد.

2. **Interactive WebAssembly**  
   بار روی کلاینت؛ دانلود بزرگ؛ DTOها در مرورگر؛ امنیت XSS روی مدل کلاینت مهم‌تر می‌شود.

3. **Interactive Auto**  
   WASM بعد از Server. پیچیدگی hydration و دو مدل اجرا برای v1 زیاد است.

4. **فقط SSR**  
   ساده و مقیاس‌پذیر. UX فرم‌های پیچیده و Grid تعاملی ضعیف‌تر؛ SignalR Circuit نیست.

## Decision

**قالب ABP: Blazor Web App (`-u blazor-webapp`) با صفحات عملیاتی Interactive Server.**

Login/کم‌تعامل: SSR طبق قالب. WASM/Auto در v1 برای صفحات PHI خیر.

## Consequences

- مثبت: مدل امنیتی ساده‌تر برای PHI؛ UX تعاملی کافی.
- منفی: هر کاربر یک Circuit؛ حافظهٔ سرور.
- منفی: scale-out بدون Redis backplane دردناک است (TD-008).
