# Open Business Questions

اگر Requirement مبهم است حدس زده نمی‌شود.

## پاسخ‌داده‌شده

| # | سؤال | پاسخ |
| --- | --- | --- |
| 1 | استقرار نسخهٔ اول؟ | Private Cloud / Docker |
| 2 | منبع هویت v1؟ | ASP.NET Core Identity محلی از طریق ABP Identity |
| 3 | زبان UI؟ | دوزبانهٔ فارسی و انگلیسی |
| 8 | رمزنگاری دادهٔ حساس؟ | الزامی — ADR-017 |
| 11 | تقویم؟ | فارسی → شمسی؛ انگلیسی → میلادی |
| 12 | محیط اجرا؟ | Linux container از روز اول (Private Cloud / Docker) |
| — | استاندارد تولید؟ | ABP Framework 10 OSS — ADR-018 |

## باز

4. آیا «کارمند» از سیستم HR موجود می‌آید یا Master Data همین سامانه است؟
   - پیشنهاد: Master Data داخلی در Organization تا Integration مشخص شود.

5. پزشک/کلینیک داخلی است یا پیمانکار طب کار خارجی هم کاربر سیستم می‌شود؟
   - تأثیر: حساب کاربری خارجی، scoping PHI، Workflow معاینه.

6. آیا مدیر غیرپزشک اجازهٔ دیدن تشخیص را هرگز دارد یا فقط Fitness؟
   - پیشنهاد معماری (قفل‌شده مگر خلافش گفته شود): هرگز تشخیص بدون `Health.Phi.View`؛ Fitness با Permission جدا.

7. ساختار سازمانی واقعی چند سطح است و آیا چند شرکت هلدینگ از روز اول هست؟
   - پیشنهاد: OrganizationUnit قابل‌پیکربندی؛ هلدینگ = چند Company زیر یک Organization، نه Tenant جدا.

9. ایمیل سازمانی SMTP در دسترس است؟ SMS از v1 لازم است؟
   - پیشنهاد: In-App اجباری؛ Email وقتی SMTP بود؛ SMS فقط interface ABP.

10. تأیید چندمرحله‌ای برای معاینه لازم است یا یک مرحله کافی است؟
    - پیشنهاد slice اول: Draft → Submitted → Completed با امکان افزودن UnderReview در Definition.

## نحوهٔ تصمیم

برای هر مورد باز: گزینه‌ها و trade-off در Feature Plan. تصمیم در ADR فقط اگر معماری کل سیستم را عوض کند.
