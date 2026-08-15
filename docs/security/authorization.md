# Authorization

## مدل

Permission-based:

```text
User → Roles → Permissions
```

Claim برای موارد خاص (مثلاً `MedicalOfficer`) علاوه بر Permission، نه جایگزین کاتالوگ.

Policy:

```text
[Authorize(HealthPermissions.MedicalExam.Complete)]
```

تعریف در `PermissionDefinitionProvider`؛ نام‌ها در `*Permissions` class. Seed از ABP Permission Management.

## لایه‌ها

1. **UI:** عدم نمایش منو و دکمه
2. **Route / Endpoint:** `[Authorize(Policy = ...)]` یا filter معادل
3. **Application:** pipeline behavior که attribute/convention روی Request را می‌خواند
4. **داده:** scoping OrganizationUnit (بعداً)؛ PHI projection

لایهٔ ۱ به‌تنهایی امنیت نیست.

## Resource-level

- PHI: [phi.md](phi.md)
- در آینده: کاربر فقط سایت خودش را ببیند — `IDataScope` از Organization assignments. در Foundation اسکلت، در slice اول اگر زمان بود فیلتر ساده؛ در غیر این صورت TD با اولویت Medium.

## Administration

`Identity.Manage` و `Organization.Manage` جدا از Permissionهای کسب‌وکار.
