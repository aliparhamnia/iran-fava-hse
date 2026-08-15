# ADR-003 — Authentication

- Status: Accepted
- Date: 2026-08-15

## Context

کاربران داخلی سازمان باید وارد پنل Blazor شوند. استقرار روی Private Cloud است. مشتری ممکن است بعداً Active Directory بخواهد. تصمیم محصول برای v1: **ASP.NET Core Identity محلی**.

## Problem

منبع هویت و پروتکل جلسه برای Host واحد چیست؟

## Options

1. **ASP.NET Core Identity + Cookie برای Blazor Server**  
   کامل، روی SQL موجود، Password policy، Lockout، Role/Permission قابل اتصال. AD ندارد.

2. **Active Directory / LDAP از روز اول**  
   برای سازمان‌های بزرگ آشنا است. وابستگی به زیرساخت مشتری؛ توسعهٔ محلی سخت‌تر؛ کاربر خارجی طب کار پیچیده‌تر.

3. **Identity محلی + همگام‌سازی AD همزمان**  
   Dual-source از روز اول. پیچیدگی حساب و password و disable در دو جا.

4. **OpenID Connect / Keycloak جدا**  
   استاندارد Integration. یک جزء عملیاتی اضافه در Private Cloud v1.

## Decision

**ASP.NET Core Identity محلی از طریق ماژول `Volo.Abp.Identity` (استاندارد ABP) + Cookie برای Blazor Web App.**

JWT / OpenIddict جدا فقط وقتی Integration خارجی یا `--tiered` لازم شد.

Permission با سیستم مجوز ABP (`PermissionDefinitionProvider` + Role). مسیر آیندهٔ AD: external login روی همان User بدون تعویض مدل Permission.

## Consequences

- مثبت: توسعه و تست بدون AD؛ کنترل Password policy؛ مناسب Private Cloud.
- منفی: کاربران باید در سامانه provision شوند تا sync ساخته شود.
- منفی: SSO سازمانی در v1 نیست (TD-007).
