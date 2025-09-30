# Module 7: Observability & Operations

[⬅️ Back to Index](README.md) | [← Previous: Module 6](module-06-security-authentication.md) | [Next: Module 8 →](module-08-documentation-dx.md)

---

### Exercise 7.1: Structured Logging with Context

**Goal**: Implement comprehensive structured logging

**Sample Prompt**:
```
Enhance application logging to use structured logging throughout.
Add Serilog with JSON formatter. Include context in all logs: RequestId,
CorrelationId, UserId, Method, Path, StatusCode, Duration.
Create logging middleware that captures request/response details.
Log to both console and file (rolling file per day).
Use different log levels appropriately: Debug, Information, Warning, Error.
Add correlation ID propagation across service boundaries.
```

**Acceptance Criteria**:
- [ ] Serilog configured with JSON output
- [ ] Request/response logging middleware added
- [ ] Correlation IDs implemented
- [ ] Contextual properties included in logs
- [ ] Multiple sinks configured (console, file)
- [ ] Log levels used appropriately

**Stretch Goals**:
- Send logs to centralized system (Elasticsearch, Seq)
- Implement log sampling for high-volume endpoints
- Add sensitive data masking in logs
- Create log aggregation dashboards
- Implement distributed tracing correlation
- Add log-based alerting

---

### Exercise 7.2: Metrics and Monitoring

**Goal**: Expose application metrics for monitoring

**Sample Prompt**:
```
Implement application metrics using App.Metrics or Prometheus.NET.
Expose /metrics endpoint in Prometheus format. Track metrics:
- Request count (by endpoint, status code)
- Request duration histogram
- Active requests gauge
- User creation rate
- Error rate by type
Add custom business metrics: total users, users created today.
Create a metrics middleware to automatically track HTTP metrics.
```

**Acceptance Criteria**:
- [ ] Metrics library installed and configured
- [ ] /metrics endpoint exposes Prometheus format
- [ ] HTTP request metrics tracked automatically
- [ ] Custom business metrics implemented
- [ ] Metrics middleware created
- [ ] Documentation for available metrics

**Stretch Goals**:
- Create Grafana dashboards for metrics
- Implement metric alerting rules
- Add percentile tracking (p50, p95, p99)
- Track database query metrics
- Implement custom metric labels
- Add metric cardinality limits
- Create SLO/SLI definitions

**Sample Prompt for Grafana**:
```
Create Grafana dashboard JSON definition for the API metrics.
Include panels for: request rate, error rate, response time percentiles,
active users count, endpoint-specific latency. Add variables for time range
and endpoint filtering. Include alerting rules for error rate > 5%.
```

---

### Exercise 7.3: Distributed Tracing

**Goal**: Implement distributed tracing for request flows

**Sample Prompt**:
```
Add OpenTelemetry instrumentation to the application.
Install OpenTelemetry.Instrumentation.AspNetCore package.
Create spans for: HTTP requests, repository operations, validation.
Add custom attributes to spans: userId, endpoint, queryParams.
Configure export to Jaeger or Zipkin (optional, can be no-op exporter).
Propagate trace context in W3C Trace Context format.
```

**Acceptance Criteria**:
- [ ] OpenTelemetry configured
- [ ] Automatic instrumentation for ASP.NET Core
- [ ] Custom spans for repository operations
- [ ] Span attributes include relevant context
- [ ] Trace context propagation works
- [ ] Tracing can be enabled/disabled via config

**Stretch Goals**:
- Add baggage for cross-cutting concerns
- Implement trace sampling strategies
- Add trace-based error analysis
- Create service dependency maps
- Implement trace-driven testing
- Add custom span events
- Create trace retention policies

---

### Exercise 7.4: Health Checks

**Goal**: Implement comprehensive health monitoring

**Sample Prompt**:
```
Add health check endpoints to the application.
Create /health/live endpoint (liveness probe) - always returns 200 if app is running.
Create /health/ready endpoint (readiness probe) - checks dependencies are available.
Add health checks for: memory usage, database connectivity (when added).
Return 503 Service Unavailable if not ready. Include health check details
in response JSON. Add a degraded state for partial availability.
```

**Acceptance Criteria**:
- [ ] Health check middleware configured
- [ ] /health/live endpoint returns liveness status
- [ ] /health/ready endpoint checks dependencies
- [ ] Returns appropriate status codes
- [ ] Health check results include details
- [ ] Degraded state supported

**Stretch Goals**:
- Add custom health checks for external dependencies
- Implement health check caching
- Add health check history tracking
- Create health-based auto-scaling triggers
- Implement circuit breaker pattern
- Add health check publishing (to monitoring system)
- Create health check dashboards

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 6](module-06-security-authentication.md) | [Next: Module 8 →](module-08-documentation-dx.md)
