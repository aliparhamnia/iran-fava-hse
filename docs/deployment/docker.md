# Docker

## Compose v1 (Phase 2)

فایل: [`../../Hse.Platform/docker-compose.yml`](../../Hse.Platform/docker-compose.yml)

سرویس‌های فعلی:

- `db` — Azure SQL Edge (ARM-native برای Docker Desktop روی Apple Silicon). تصاویر `mssql/server` amd64 اینجا با QEMU کرش می‌کنند. روی Linux/CI می‌توان همان سرویس را به `mcr.microsoft.com/mssql/server:2025-CU1-ubuntu-24.04` عوض کرد. پورت `1433`، volume `hse-sql`.

اجرای Host هنوز با `dotnet run` است (گواهی OpenIddict و InteractiveAuto).

Dockerfile چندمرحله‌ای Host: [`../../Hse.Platform/src/Hse.Platform.Blazor/Dockerfile`](../../Hse.Platform/src/Hse.Platform.Blazor/Dockerfile) — برای image بعدی CD، هنوز در Compose به‌صورت سرویس `web` وصل نشده.

## Volumes بعدی

- `hse-files` برای Blob File System
- `hse-keys` برای گواهی/کلید رمزنگاری

## Reverse proxy

TLS در proxy سازمان (Traefik/Nginx/IIS ARR). اپ HTTPS را پشت forwarded headers می‌فهمد.
