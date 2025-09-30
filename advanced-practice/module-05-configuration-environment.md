# Module 5: Configuration & Environment Management

[⬅️ Back to Index](README.md) | [← Previous: Module 4](module-04-persistence-data-layer.md) | [Next: Module 6 →](module-06-security-authentication.md)

---

### Exercise 5.1: Structured Configuration Management

**Goal**: Implement robust configuration system

**Sample Prompt**:
```
Create a strongly-typed configuration system for the application.
Create AppSettings class with nested sections: Database, Logging, RateLimit, Security.
Bind configuration from appsettings.json, environment variables, and command line.
Add configuration validation on startup (fail fast if invalid).
Support configuration hot reload for specific settings.
Use IOptions pattern for injecting configuration.
```

**Acceptance Criteria**:
- [ ] Configuration classes created with properties
- [ ] Configuration bound from multiple sources
- [ ] Validation implemented with data annotations
- [ ] Configuration injected via IOptions
- [ ] Startup validation fails fast
- [ ] Configuration precedence documented

**Stretch Goals**:
- Implement feature flags configuration
- Add Azure Key Vault configuration provider
- Create configuration change listeners
- Add configuration encryption for secrets
- Implement configuration versioning
- Add configuration documentation generator

---

### Exercise 5.2: Secrets Management

**Goal**: Implement secure handling of sensitive configuration

**Sample Prompt**:
```
Implement proper secrets management for the application.
Use .NET User Secrets for local development. Add .env file support.
Create a secrets template file (.env.example) with all required secrets.
Add secret validation on startup. Document secret rotation procedures.
Show how to use Azure Key Vault or AWS Secrets Manager in production.
Ensure secrets never appear in logs or error messages.
```

**Acceptance Criteria**:
- [ ] User Secrets configured for development
- [ ] .env support added (if needed)
- [ ] .env.example template created
- [ ] Secrets validation implemented
- [ ] Production secrets documentation added
- [ ] Secret scrubbing in logs

**Stretch Goals**:
- Implement automatic secret rotation
- Add secrets health checks
- Create secrets audit logging
- Implement secret versioning
- Add emergency secret revocation
- Create secrets access control

---

### Exercise 5.3: Multi-Environment Configuration

**Goal**: Support different configurations per environment

**Sample Prompt**:
```
Create distinct configuration profiles for Development, Staging, and Production.
Add appsettings.Staging.json and enhance appsettings.Production.json.
Configure different settings: log levels, connection strings, CORS policies,
rate limits, feature flags. Add environment indicator to API responses (header).
Document environment-specific setup requirements.
```

**Acceptance Criteria**:
- [ ] Environment-specific appsettings files created
- [ ] Configuration varies appropriately per environment
- [ ] Environment detection working correctly
- [ ] API indicates current environment
- [ ] Environment setup documented
- [ ] Environment variables override files

**Stretch Goals**:
- Implement infrastructure-as-code for environments
- Add environment smoke tests
- Create environment promotion pipeline
- Implement blue-green deployment support
- Add canary deployment configuration
- Create environment comparison tools

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 4](module-04-persistence-data-layer.md) | [Next: Module 6 →](module-06-security-authentication.md)
