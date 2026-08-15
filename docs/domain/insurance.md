# Insurance

## Business Capabilities

1. نگهداری شرکت بیمه، قرارداد و بیمه‌نامه
2. پوشش کارکنان، حوادث، مسئولیت، تجهیزات، پروژه، پیمانکار
3. سقف تعهد و پوشش‌ها
4. خسارت (Claim) و پیگیری پرداخت
5. تمدید و هشدار انقضا
6. گزارش خسارت (Reporting)

## Aggregates

### Insurer

شرکت بیمه (طرف قرارداد).

### InsuranceContract

قرارداد چارچوب با Insurer (اختیاری اگر سازمان مستقیم Policy می‌خرد).

### InsurancePolicy

- شماره بیمه‌نامه (کلید کسب‌وکار)
- Subject: Employee / Equipment / Project / Contractor / Liability / Other (`PolicySubject`)
- `DateRange` اعتبار
- پوشش‌ها (Entity `Coverage`)
- سقف تعهد (`Money` یا decimal+currency داخل ماژول)
- وضعیت فرآیندی از Workflow + وضعیت کسب‌وکار Active/Expired/Cancelled

### Claim

- لینک به Policy
- تاریخ حادثه/خسارت، مبلغ درخواستی، مبلغ پرداختی
- مدارک از Documents
- وضعیت پرداخت

## Value Objects

- `PolicyNumber`, `CoverageCode`, `Money`, `DateRange`, `PolicySubject`

## Domain Events

- `PolicyIssued`
- `PolicyRenewed`
- `PolicyExpired`
- `ClaimOpened`
- `ClaimSettled`

## Integration Events

- `InsurancePolicyExpiring`
- `ClaimOpened` (ممکن است به Prevention Incident وصل شود با ID مرجع، نه Join)

## Business Rules

- Policy با EndDate < StartDate نامعتبر است.
- Claim فقط روی Policy معتبر در تاریخ خسارت (یا Rule استثناء با دلیل).
- تمدید Policy جدید می‌سازد یا version می‌کند — تصمیم slice: **نسخه/رکورد جدید با لینک PreviousPolicyId** تا تاریخچه خراب نشود.

## State Machine

Policy: `draft → active → expired | cancelled` با `renewalPending` اختیاری.

Claim: `draft → submitted → underReview → approved | rejected → settled | closed`.
