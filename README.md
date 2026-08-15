# HSE Management Platform

پلتفرم سازمانی مدیریت HSE روی **ABP Framework 10** و **.NET 10**.

## وضعیت

Phase 2 — Foundation. Solution در پوشهٔ [`Hse.Platform`](Hse.Platform/).

## اجرا (توسعه)

```bash
export DOTNET_ROOT="/opt/homebrew/opt/dotnet/libexec"
export PATH="$DOTNET_ROOT:$HOME/.dotnet/tools:$PATH"

cd Hse.Platform
docker compose up -d
dotnet run --project src/Hse.Platform.DbMigrator
dotnet run --project src/Hse.Platform.Blazor
```

ورود پیش‌فرض ABP: `admin` / `1q2w3E*`

زبان پیش‌فرض: فارسی (تقویم شمسی). انگلیسی از انتخابگر زبان (تقویم میلادی).

مستندات معماری: [docs/README.md](docs/README.md)
