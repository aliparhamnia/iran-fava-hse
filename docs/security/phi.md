# PHI and Sensitive Data

مرجع: [ADR-014](../adr/ADR-014-phi-protection.md).

## چیست PHI در این محصول

- تشخیص و یافته‌های معاینه
- نتایج آزمایش
- مدارک پزشکی
- توضیحات بالینی
- هر فیلدی که برای Fitness لازم نیست

## چه چیزی PHI نیست (ولی ممکن است شخصی باشد)

- اینکه معاینه در تاریخ X انجام شده
- نوع معاینه
- FitnessStatus
- وجود محدودیت شغلی (متن محدودیت شغلی برای سرپرست — شخصی است اما بالینی کامل نیست؛ با `Health.MedicalExam.View` یا Permission جدا `Health.Fitness.View` اگر لازم شد)

کد ملی و اطلاعات هویتی کارمند در Organization است؛ در Log نمی‌آید؛ دسترسی‌اش `Organization.Manage` / View کارمند است نه Phi.

## کنترل‌ها

| کنترل | v1 |
| --- | --- |
| Permission جدا `Health.Phi.View` | بله |
| DTO جدا | بله |
| Read audit | بله |
| Mask در Audit Old/New | بله برای متن بالینی |
| عدم حضور در Integration Event | بله |
| عدم حضور در Log | بله |
| Always Encrypted | خیر مگر الزام فراتر از ADR-017 (TD-005) |
| رمزنگاری application-level | بله — ADR-017 |
| فایل Restricted رمزشده | بله |

## اصل حداقل داده در UI

صفحهٔ لیست و داشبورد مدیر: بدون ستون تشخیص. جزئیات پزشکی فقط برای نقش پزشک/بهداشت با Phi.
