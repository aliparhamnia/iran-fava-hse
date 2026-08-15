# Environment

## Business Capabilities

1. جنبه‌ها و پیامدهای زیست‌محیطی و ریسک مرتبط
2. پسماند (نوع، دفع، ردیابی)
3. پایش مصرف و انتشار (آب، انرژی، هوا، صدا، خاک، پساب) در یک مدل Monitoring
4. بازرسی و حادثه زیست‌محیطی
5. مجوز و الزام قانونی / Compliance
6. KPI محیط زیست (Reporting)

## Aggregates

### EnvironmentalAspect

جنبه + پیامد (Impact می‌تواند Entity وابسته باشد نه Aggregate جدا در v1).

### WasteRecord

- WasteType (catalog)
- مقدار، واحد، روش دفع، پیمانکار دفع
- تاریخ

### MonitoringRecord

- `MonitoringKind`: Water | Energy | Emission | AirQuality | Noise | Soil | Wastewater | Other
- مقدار، واحد، نقطهٔ پایش، زمان
- نه شش Aggregate جدا از روز اول

### EnvironmentalIncident

مشابه Prevention Incident اما در Context محیط زیست (مواد، نشت، تخلف). اگر شباهت بیش از حد شد، بعداً می‌توان الگوی مشترک در Domain.Shared محصول فقط برای فیلدهای غیرکسب‌وکاری (نه Generic Incident خدا).

### EnvironmentalPermit

مجوز با اعتبار و شرایط.

### ComplianceObligation

الزام قانونی، منبع، سررسید ارزیابی.

## Value Objects

- `MonitoringKind`, `Quantity` (value + unit), `WasteTypeCode`, `GeoPoint` اختیاری

## Domain Events

- `EnvironmentalIncidentReported`
- `MonitoringRecordCaptured`
- `PermitExpiring`
- `ComplianceAssessmentDue`

## Integration Events

- `CorrectiveActionRequested`
- `EnvironmentalIncidentReported`
- `PermitExpiresSoon`

## Business Rules

- Monitoring بدون واحد اندازه‌گیری ثبت نمی‌شود.
- Permit منقضی برای عملیات نیازمند مجوز باید در گزارش Compliance دیده شود (write model فقط Status را عوض می‌کند).

## State Machine

Incident و Permit مانند Prevention از Workflow Engine مشترک.
