# ADR-006 — Workflow Engine

- Status: Accepted
- Date: 2026-08-15

## Context

تقریباً همهٔ فرآیندهای HSE جریان تأیید دارند (معاینه، حادثه، CAPA، بیمه‌نامه، مجوز محیط زیست). Stateها نباید هاردکد باشند. Overengineering هم ممنوع است.

## Problem

موتور Workflow مستقل از روز اول لازم است یا یک State Machine قابل‌توسعه کافی است؟

## Options

1. **Enum ثابت در هر Aggregate**  
   سریع. هر فرآیند جدید تغییر کد. برخلاف اصل «State هاردکد نشود».

2. **Configurable state machine مشترک (`IWorkflowEngine`)**  
   Definition نسخه‌دار در DB: States، Transitions، RequiredPermission، Guard ساده. Instance روی Entity.

3. **Elsa Workflows 3**  
   Designer، long-running، bookmark، مناسب BPM پیچیده. هزینه یادگیری، عملیات، و مدل ذهنی جدا برای v1.

4. **موتور تجاری BPMN**  
   قفل فروشنده و عملیات سنگین.

## Decision

**گزینه ۲: State machine قابل‌پیکربندی مشترک.**

```text
IWorkflowEngine
IWorkflowDefinition
IWorkflowInstance
IWorkflowTransition
```

State شناسهٔ پایدار رشته‌ای است (`submitted`) نه enum بسته. تاریخچه در `wf.WorkflowHistories`.

اگر شاخهٔ موازی، تایمر پیچیده، یا designer برای کارشناس غیر فنی لازم شد: ADR ارتقا به Elsa با حفظ facade.

## Consequences

- مثبت: یک موتور برای همهٔ ماژول‌ها؛ فرآیند جدید بدون engine جدید.
- مثبت: تست واحد روی transition و permission.
- منفی: BPMN و شاخه‌های موازی پشتیبانی نمی‌شود.
- منفی: وسوسهٔ گذاشتن business rule پیچیده داخل Guard به‌جای Domain — باید منع شود. Guard فقط «چه کسی / چه شرط سادهٔ فرایندی».
