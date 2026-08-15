# Architecture Risks

| ریسک | اثر | کاهش |
| --- | --- | --- |
| مرز Employee/Health اشتباه شود و PHI به ماژول‌های دیگر نشت کند | نقض حریم و انطباق | Employee در Organization؛ Health فقط EmployeeId؛ ADR-014؛ Architecture Tests |
| Blazor Server و چند instance بدون backplane | قطع Session | v1 تک‌instance؛ بدهی Redis backplane مستند |
| Workflow بیش‌ازحد انتزاعی | تأخیر تحویل | State machine ساده در DB؛ نه Elsa در v1 |
| Workflow هاردکد | بازنویسی هر فرآیند | State به‌صورت string پایدار در Definition |
| Schema-per-module گزارش cross-module را سخت کند | Queryهای نادرست یا Join ممنوع در Domain | Reporting جدا + schema `rpt` |
| MudBlazor RTL در چند کنترل ناقص باشد | UI شکسته | CSS سفارشی؛ wrapper؛ ADR-007 |
| Hangfire روی چند replica شغل تکراری اجرا کند | نوتیفیکیشن/جاب دوبل | DisableConcurrentExecution؛ v1 تک worker |
| الزام حقوقی فراتر از رمزنگاری application-level | Always Encrypted / HSM | ADR-017 پوشش v1 را می‌دهد؛ مکمل در TD-005 |
| گم شدن کلید رمزنگاری | از دست رفتن PHI | secrets جدا از SQL؛ backup کلید؛ version prefix |
| Master Data کارمند بعداً از HR بیاید | همگام‌سازی dual | EmployeeId پایدار؛ import بعدی پشت facade |
| فایل روی volume جدا از SQL | Backup ناقص | Runbook: SQL + files + encryption secrets |
| TenantId بدون فیلتر کامل | نشت بین‌تننت در آینده | data filter ABP وقتی Tenant دوم فعال شد |
| تقویم شمسی در UI و میلادی در DB | باگ سررسید | ADR-016؛ تبدیل فقط در UI؛ تست round-trip |
| لایه‌های زیاد ABP | کندی Solution / دور زدن لایه | ADR-018؛ Architecture Tests؛ TD-010 |

سؤالات باز مرتبط: [questions.md](questions.md).
