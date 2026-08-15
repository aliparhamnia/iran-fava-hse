# Git Strategy

## Branch

- `main` پایدار
- Feature branch: `feat/health-medical-exam`
- اصلاح: `fix/...`

مدل دقیق GitFlow در v1 اجباری نیست؛ PR به main کافی است.

## Commit

Conventional Commits:

```text
feat(health): add medical examination domain
feat(health): add medical examination workflow
feat(health): add medical examination UI
test(health): add medical examination tests
docs(adr): record PHI read-audit decision
chore(host): add serilog enrichment
```

## ممنوع در Git

- `.env` با راز
- باینری مدارک نمونهٔ واقعی
- `--no-verify` مگر درخواست صریح
