# Reporting and Dashboard Architecture

مرجع: [ADR-013](../adr/ADR-013-reporting.md).

## جداسازی

Write model / Aggregate برای گزارش سنگین hydrate نمی‌شود.

- **Operational list:** Query ماژول + DTO + pagination
- **Dashboard widgets:** هر Widget یک Query؛ component-based
- **گزارش سنگین:** schema `rpt` View یا جدول همگام

## داشبورد اولیه (پس از چند slice)

- Total Incidents
- Near Miss
- Open CAPA
- Overdue Actions
- Medical Exams Due
- Insurance Expiring
- Environmental Incidents
- Safety KPI / Environmental KPI وقتی تعریف عملیاتی شد

Widget بدون Permission ماژول مربوطه رندر نمی‌شود.

## KPI

تعریف KPI در دامنهٔ Reporting مستند می‌شود نه داخل Entity حادثه. نسخهٔ اول می‌تواند شمارندهٔ ساده باشد.

## ممنوع

- God Query برای کل داشبورد
- SP پیش از اندازه‌گیری
- نمایش PHI روی ویجت مدیریتی
