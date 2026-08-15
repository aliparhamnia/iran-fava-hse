# Actions (CAPA)

## Bounded Context

اقدام اصلاحی و پیشگیرانهٔ مشترک. منبع می‌تواند Incident، Inspection، Exam، Permit، یا دستی باشد.

## Aggregate

### CorrectiveAction

- `SourceReference` (SourceType + SourceId)
- Title، Description، Assignee (UserId یا EmployeeId)
- DueDate
- Type: Corrective | Preventive | Both
- Verification (اثربخشی) به‌عنوان Entity وابسته وقتی بسته می‌شود
- Workflow state

در v1 یک Aggregate کافی است؛ تفکیک CAPA رسمی ISO به چند نوع رکورد اگر محصول خواست بعداً با Type و فرم انجام می‌شود نه دو موتور.

## Value Objects

- `SourceReference`
- `ActionPriority`
- `VerificationResult`

## Domain Events

- `CorrectiveActionCreated`
- `CorrectiveActionCompleted`
- `CorrectiveActionBecameOverdue`

## Integration Events

- `CorrectiveActionOverdue`
- `CorrectiveActionClosed` (منبع بتواند Incident را ببندد)

## Business Rules

- بدون SourceReference یا دلیل «دستی» ساخته نمی‌شود.
- Complete بدون توضیح انجام و در صورت پیکربندی بدون Verification ممنوع است.
- Assignee باید کاربر Active باشد.

## State Machine

```text
draft → assigned → inProgress → pendingVerification → closed
                              ↘ cancelled
```

قابل پیکربندی per tenant در آینده؛ seed پیش‌فرض بالا.
