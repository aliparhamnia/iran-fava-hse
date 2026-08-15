# Organization

## Bounded Context

ساختار سازمانی قابل‌پیکربندی و Master Data کارمند / سمت.

## Aggregates

### OrganizationUnit

درخت واحدها. نوع سطح هاردکد شش‌تایی نیست.

- ParentId اختیاری (ریشه = سازمان)
- Type: رکورد قابل‌پیکربندی (`Organization`, `Company`, `Site`, `Plant`, `Department`, `Unit`, + سفارشی)
- Path / Depth برای کوئری (materialized path)
- Soft delete با احتیاط: واحد دارای فرزند فعال حذف نمی‌شود

### Employee

- کد پرسنلی (کلید کسب‌وکار، Unique per tenant)
- نام
- انتساب فعلی به OrganizationUnit و Position
- HireDate، Status (Active/Terminated)
- لینک اختیاری به UserId

Health به این Aggregate وابسته نیست؛ فقط `EmployeeId` می‌گیرد.

### Position

- عنوان شغلی
- می‌تواند به نوع واحد محدود شود یا آزاد باشد

## Value Objects

- `OrganizationUnitTypeCode`
- `EmployeeNumber`
- `EmploymentPeriod`

## Domain Services

- جلوگیری از حلقه در درخت
- انتقال کارمند بین واحدها (تاریخچه انتساب اگر لازم شد در Entity `EmployeeAssignment`)

## Application Services / Features

- CRUD واحد و سمت
- ثبت/ویرایش کارمند
- Lookup کارمند برای سایر ماژول‌ها (`IEmployeeLookup` در Contracts)

## Repository

`IEmployeeLookup` در Contracts. Persistence جزئیات درخت ممکن است query اختصاصی بخواهد (نه Generic Repository).

## Domain Events

- `EmployeeHired`
- `EmployeeTransferred`
- `EmployeeTerminated`
- `OrganizationUnitCreated`

## Integration Events

- `EmployeeTerminated` — Health/Insurance نباید فرآیند جدید روی کارمند خاتمه‌یافته باز کنند مگر Rule خلاف بگوید
- `EmployeeTransferred` — برای scoping بعدی

## Business Rules

- کارمند Active باید حداقل یک انتساب سازمانی داشته باشد.
- کد پرسنلی در Tenant یکتا است.
- نمی‌توان واحد ریشه را حذف کرد.
- نوع واحدهای پیش‌فرض seed می‌شوند؛ حذف نوعی که در حال استفاده است ممنوع است.

## State Machine

ندارد.

## ساختار نمونه (قابل پیکربندی)

```text
Organization
 └── Company
      └── Site
           └── Plant
                └── Department
                     └── Unit
```

این یک پیکربندی پیش‌فرض است نه مدل ثابت دامنه.
