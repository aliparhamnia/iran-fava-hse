# Domain and Integration Events

Domain Event داخل مرز ماژول است و می‌تواند جزئیات بیشتری داشته باشد. Integration Event قرارداد پایدار بین ماژول‌هاست و PHI ندارد.

## Occupational Health

| Domain | Integration | مصرف‌کننده |
| --- | --- | --- |
| MedicalExamScheduled | — | داخل Health |
| MedicalExamCompleted | MedicalExamCompleted | Reporting, Organization display |
| FitnessForWorkChanged | FitnessForWorkChanged | Supervisor UI / Reporting |
| MedicalExamBecameOverdue | MedicalExamDue | Notifications |

## Prevention

| Domain | Integration | مصرف‌کننده |
| --- | --- | --- |
| IncidentReported | IncidentReported | Reporting, Notifications |
| InspectionCompleted | — / CorrectiveActionRequested | Actions |
| PermitExpiring | PermitExpiresSoon | Notifications |

## Insurance

| Domain | Integration | مصرف‌کننده |
| --- | --- | --- |
| PolicyIssued | — | |
| PolicyExpired / expiring job | InsurancePolicyExpiring | Notifications |
| ClaimOpened | ClaimOpened | Reporting |

## Environment

| Domain | Integration | مصرف‌کننده |
| --- | --- | --- |
| EnvironmentalIncidentReported | EnvironmentalIncidentReported | Reporting, Actions |
| Permit expiring | PermitExpiresSoon | Notifications |

## Actions

| Domain | Integration | مصرف‌کننده |
| --- | --- | --- |
| CorrectiveActionCreated | — | |
| CorrectiveActionBecameOverdue | CorrectiveActionOverdue | Notifications |
| CorrectiveActionClosed | CorrectiveActionClosed | Prevention/Environment برای بستن منبع |

## قوانین انتشار

1. Domain Event در همان تراکنش Aggregate جمع می‌شود و پس از Save dispatch می‌شود (یا داخل همان pipeline).
2. Integration Event در Outbox همان تراکنش SQL نوشته می‌شود سپس توسط Hangfire/dispatcher منتشر می‌شود.
3. Handler باید idempotent باشد (کلید Inbox: EventId).
4. نام رویداد گذشته‌نگر است (اتفاقی که افتاده).
