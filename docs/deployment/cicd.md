# CI/CD Strategy

## CI (از Phase 2)

روی هر push/PR:

1. Restore + Build .NET 10
2. Unit tests
3. Architecture tests
4. Integration tests با Docker (Testcontainers)
5. `dotnet format --verify` یا editorconfig check اگر فعال شد
6. Build Docker image (tag = git sha) — publish به registry خصوصی فقط روی main/release

## CD

Private Cloud:

- Staging: deploy خودکار از main پس از CI سبز (اگر سازمان بخواهد)
- Production: دستی / approve

ابزار: GitHub Actions. قالب: [`.github/workflows/ci.yml`](../../.github/workflows/ci.yml) — restore، build، Domain tests، Architecture tests. Integration/Testcontainers و Docker image در job جدا بعداً اضافه می‌شود.

## ممنوع

- Skip hooks و `--no-verify` مگر درخواست صریح
- Deploy بدون migration plan
- راز در log CI

## نسخه‌گذاری

SemVer وقتی محصول release شد. تا آن زمان sha کافی است.
