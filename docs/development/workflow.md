# Workflow Architecture

مرجع: [ADR-006](../adr/ADR-006-workflow-engine.md).

## هدف

یک abstraction برای همهٔ ماژول‌ها. State هاردکد در enum بسته نیست. Overengineering (Elsa) در v1 نیست.

## قراردادها

```text
IWorkflowEngine
IWorkflowDefinition
IWorkflowInstance
IWorkflowTransition
```

## Definition

- `EntityType` (مثلاً `health.medical-examination`)
- Version (فعال یکی)
- States: Code, DisplayName, IsInitial, IsTerminal
- Transitions: From, To, RequiredPermission, GuardKey اختیاری

Seed در migration/seed ماژول Workflow + definitions مورد نیاز Health در Phase 3.

## Instance

- EntityType + EntityId
- CurrentState
- StartedAt / ChangedAt
- RowVersion اگر ویرایش همزمان transition ممکن است

## History

`wf.WorkflowHistories`: From, To, Actor, At, Comment

## Guards

کلیدهای محدود و صریح (`InitiatorOnly`, `AssignedPhysician`) در کد ثبت می‌شوند. Guard پیچیده = قانون دامنه در Aggregate نه موتور گردش‌کار.

## یکپارچگی با دامنه

1. کاربر Transition را درخواست می‌کند.
2. Engine Permission و Guard را چک می‌کند.
3. Aggregate invariant را برای آن انتقال اجرا می‌کند (مثلاً Complete نیاز به Fitness).
4. Instance به‌روز می‌شود.
5. در یک تراکنش با Outbox در صورت نیاز.

وضعیت نمایشی UI از Workflow Instance خوانده می‌شود نه از فیلد موازی مگر denormalize آگاهانه روی Root برای لیست (`WorkflowState` کپی با به‌روزرسانی همزمان در همان تراکنش — قابل قبول برای Query لیست بدون Join به wf).
