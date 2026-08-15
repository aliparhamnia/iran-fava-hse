# Deployment Strategy

تصمیم محصول: **Private Cloud / Docker** (نه On-prem کلاسیک به‌عنوان پیش‌فرض، نه Azure عمومی).

## v1 Topology

```text
┌─────────────────────────────────────┐
│ Private Cloud                        │
│  docker compose / later k8s          │
│                                      │
│  [reverse proxy TLS]                 │
│         │                            │
│         ▼                            │
│  hse-web (Hse.Platform.Blazor)  1 replica       │
│         │                            │
│         ├─ SQL Server                │
│         ├─ volume files              │
│         ├─ volume dataprotection     │
│         └─ Seq or file logs (opt)    │
└─────────────────────────────────────┘
```

## تصاویر

- Linux containers از روز اول
- `Hse.Platform.Blazor` روی `aspnet:10.0`
- SQL: تصویر رسمی یا SQL مدیریت‌شدهٔ ابر خصوصی سازمان

## Config

- `appsettings.json` بدون راز
- env: ConnectionString, FileStoragePath, Hangfire, SMTP optional
- Environments: Development, Staging, Production

## Health

`/health` برای liveness/readiness (DB ping). پشت proxy برای ارکستراتور.

## مقیاس

v1: یک replica Host (TD-008). SQL جدا. Volume فایل مشترک قبل از replica دوم اجباری است.

## Secrets

Docker secrets / env تزریق‌شده از vault داخلی سازمان. مستند کردن کلیدها در [configuration.md](../development/configuration.md).

## Backup

- SQL full + log طبق سیاست سازمان
- Volume فایل همزمان
- تست restore دوره‌ای در runbook (سند deployment عملیاتی در Phase 2 تکمیل می‌شود)
