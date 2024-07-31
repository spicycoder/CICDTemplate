# Pending Items - Kubernetes

- [ ] Verify CRON
- [ ] Verify Pub / Sub
- [ ] Aspire Dashboard on Kubernetes

To verify secrets, create a kubernetes secret

```PowerShell
kubectl create secret generic hello --from-literal=hello=world
```

Then verify it from API
