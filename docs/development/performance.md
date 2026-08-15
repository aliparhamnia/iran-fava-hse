# Performance Guidelines

- Async/Await برای I/O؛ CPU-bound نادر را بلاک نکن
- CancellationToken از UI/API تا EF
- Pagination اجباری
- Projection به DTO در Query
- `AsNoTracking` برای خواندن
- جلوگیری از N+1 با Include/split query آگاهانه
- ایندکس مطابق الگوی فیلتر
- Caching فقط با دلیل (کاتالوگ Permission، Workflow definition)
- Lazy Loading خاموش
- Blazor: از bind دوطرفه روی لیست بزرگ پرهیز؛ Virtualize در Grid
