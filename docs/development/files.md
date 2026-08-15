# File Management Architecture

مرجع: [ADR-005](../adr/ADR-005-file-storage.md), [ADR-017](../adr/ADR-017-encryption.md).

## Abstraction

ABP `IBlobContainer` / `IBlobContainer<T>` — نه `IFileStorage` موازی.

```text
IBlobContainer<MedicalExamFiles>
  SaveAsync / GetAsync / DeleteAsync / ExistsAsync
```

## v1 Implementation

File System provider روی volume داکر، خارج از wwwroot، مثلاً `/var/hse/files`.

فایل Restricted قبل از Save رمز می‌شود.

## Metadata (schema `doc`)

`Files`: FileNameOriginal, ContentType, Size, Hash, Sensitivity, BlobName, UploadedBy, UploadedAt

`FileLinks`: OwnerType, OwnerId, Category, FileId

## امنیت

طبق [../security/controls.md](../security/controls.md) و [../security/encryption.md](../security/encryption.md). دانلود مدارک Restricted: Authorize + Phi.

## ویروس‌اسکن

`IFileScanner` اختیاری؛ v1 no-op.

## جایگزینی بعدی

MinIO / S3-compatible با تعویض Blob provider. متادیتا ثابت می‌ماند.
