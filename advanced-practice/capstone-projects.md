# Capstone Projects

[⬅️ Back to Index](README.md) | [← Previous: Module 10](module-10-architecture-scalability.md) | [Next: Tips & Resources →](tips-and-resources.md)

---

### Capstone 1: Complete Modernization

**Goal**: Apply all learned concepts to fully modernize the API

**Sample Prompt**:
```
Perform a complete modernization of the net-users-demo API incorporating:
1. Replace in-memory storage with Entity Framework Core + PostgreSQL
2. Add JWT authentication with refresh tokens
3. Implement comprehensive validation with FluentValidation
4. Add distributed caching with Redis
5. Implement OpenTelemetry tracing and Prometheus metrics
6. Add background jobs with Hangfire
7. Create full test suite (unit, integration, load tests)
8. Add CI/CD pipeline with Docker and Kubernetes deployment
9. Create comprehensive documentation (OpenAPI, ADRs, README)
10. Implement all security best practices
Document architecture decisions and provide migration guide.
```

**Deliverables**:
- [ ] Fully modernized codebase
- [ ] Database with migrations
- [ ] Complete test coverage (>80%)
- [ ] CI/CD pipeline functional
- [ ] Docker and K8s manifests
- [ ] Comprehensive documentation
- [ ] Architecture decision records
- [ ] Performance benchmarks

---

### Capstone 2: Multi-Tenant SaaS Platform

**Goal**: Convert the API into a multi-tenant SaaS platform

**Sample Prompt**:
```
Transform the API into a multi-tenant SaaS platform:
1. Add Tenant model and tenant_id to all entities
2. Implement tenant isolation at data layer
3. Add tenant registration and management API
4. Implement per-tenant rate limiting
5. Add tenant-specific configuration
6. Create tenant admin dashboard
7. Implement usage tracking and billing integration
8. Add tenant-level analytics
9. Create tenant onboarding workflow
10. Implement tenant data export/deletion (GDPR compliance)
Support both shared-database and database-per-tenant strategies.
```

**Deliverables**:
- [ ] Tenant management system
- [ ] Data isolation working correctly
- [ ] Tenant registration flow
- [ ] Admin dashboard
- [ ] Usage tracking system
- [ ] GDPR compliance features
- [ ] Documentation for tenant architecture
- [ ] Migration path from single-tenant

---

### Capstone 3: Event-Driven Microservices

**Goal**: Decompose into event-driven microservices

**Sample Prompt**:
```
Refactor the monolithic API into event-driven microservices:
1. Identify bounded contexts: User Service, Notification Service, Analytics Service
2. Implement event bus (RabbitMQ or Azure Service Bus)
3. Define domain events: UserCreated, UserUpdated, UserDeleted
4. Create service-to-service communication patterns
5. Implement saga pattern for distributed transactions
6. Add API gateway (Ocelot or YARP)
7. Implement service discovery
8. Add distributed tracing across services
9. Create resilience patterns (circuit breaker, retry, timeout)
10. Implement event sourcing for audit trail
Document microservices architecture and deployment topology.
```

**Deliverables**:
- [ ] Multiple microservices deployed
- [ ] Event bus configured
- [ ] Domain events published/consumed
- [ ] API gateway functional
- [ ] Service resilience implemented
- [ ] Distributed tracing working
- [ ] Architecture documentation
- [ ] Deployment diagrams

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 10](module-10-architecture-scalability.md) | [Next: Tips & Resources →](tips-and-resources.md)
