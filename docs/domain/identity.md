# Identity & Access

## Bounded Context

مالک کاربران سامانه، نقش‌ها، مجوزها و جلسهٔ ورود — **از طریق ماژول ABP Identity** نه مدل موازی. مالک کارمند سازمانی نیست.

جدول‌های `AbpUsers` / `AbpRoles` / `AbpPermissions` استاندارد ABP استفاده می‌شوند. Permissionهای HSE در `PermissionDefinitionProvider` تعریف و seed می‌شوند.

## Aggregates

### User

- موجودیت ورود به سامانه
- لینک اختیاری به `EmployeeId` (یک کاربر ممکن است کارمند باشد؛ پزشک پیمانکار ممکن است Employee نباشد)
- Status: Active / Locked / Disabled

### Role

- مجموعهٔ Permission
- نقش‌های seed: `SystemAdmin`, `HseOfficer`, `OccupationalPhysician`, `Supervisor`, `InsuranceOfficer`, `EnvironmentOfficer` — قابل تغییر؛ seed فقط نقطهٔ شروع است

### Permission

- `PermissionCode` پایدار مثلاً `Health.MedicalExam.View`
- در DB ذخیره می‌شود؛ در کد به‌صورت constants برای جلوگیری از typo تکرار می‌شود؛ منبع حقیقت DB + seed است نه enum بسته

## Value Objects

- `PermissionCode` (module, resource, action)
- `PersonName` در صورت نیاز نمایش؛ ترجیح: از Employee lookup

## Domain Services

- تخصیص Role به User
- ارزیابی «آیا User این Permission را دارد» در Domain لازم نیست اگر Application Policy آن را از store می‌خواند — duplicated logic ممنوع. ارزیابی در Application/Infrastructure Authorization handler.

## Application Services / Features

- Login / Logout / ChangePassword / Reset (admin)
- Manage users, roles, permission assignments
- Query: current user permissions for menu

## Repository

فقط اگر query فراتر از Identity API لازم شد. ASP.NET Core Identity store در اولویت است؛ Repository سفارشی برای Permission catalog.

## Domain Events

- `UserRegistered`
- `UserDeactivated`
- `UserLockedOut`

## Integration Events

- `UserDeactivated` → ماژول‌های دیگر نباید write روی کاربر غیرفعال را به نام او بپذیرند (Guard در Application)

## Business Rules

- Permission code پس از انتشار عمومی حذف نمی‌شود؛ deprecate می‌شود.
- آخرین SystemAdmin را نمی‌توان Disable کرد.
- کاربر بدون Role نمی‌تواند وارد بخش‌های دارای Authorize شود (به‌جز صفحهٔ «دسترسی ندارید»).

## State Machine

ندارد. Status ساده روی User کافی است.

## Permission catalog اولیه (نمونه)

```text
Health.View
Health.Create
Health.Update
Health.Delete
Health.Approve
Health.MedicalExam.View
Health.MedicalExam.Create
Health.MedicalExam.Update
Health.MedicalExam.Complete
Health.Phi.View

Prevention.View
Prevention.Create
Prevention.Update
Prevention.Approve

Insurance.View
Insurance.Create
Insurance.Update
Insurance.Approve

Environment.View
Environment.Create
Environment.Update
Environment.Approve

Actions.View
Actions.Create
Actions.Update
Actions.Approve

Documents.View
Documents.Upload
Notifications.View
Workflow.View
Audit.View
Identity.Manage
Organization.Manage
Dashboard.View
Reporting.View
```

کدها hard-code به‌عنوان تنها منبع نیستند؛ seed و جدول `idt.Permissions`.
