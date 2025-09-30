# Module 10: Architecture & Scalability

[⬅️ Back to Index](README.md) | [← Previous: Module 9](module-09-tooling-automation.md) | [Next: Capstone Projects →](capstone-projects.md)

---

### Exercise 10.1: Response Caching

**Goal**: Improve performance with caching

**Sample Prompt**:
```
Implement response caching for the GET users endpoints.
Add in-memory caching for GetUsers and GetUser(id) responses.
Set appropriate cache duration (5 minutes for GetUsers, 15 min for GetUser).
Add cache invalidation on Create, Update, Delete operations.
Add X-Cache header to indicate cache hit/miss. Support Cache-Control
request headers (no-cache, max-age). Add configuration for cache TTL.
Implement cache warming strategy for frequently accessed data.
```

**Acceptance Criteria**:
- [ ] Response caching middleware configured
- [ ] GET endpoints cached with appropriate TTL
- [ ] Cache invalidated on mutations
- [ ] Cache headers included in responses
- [ ] Cache-Control headers respected
- [ ] Configuration for cache settings

**Stretch Goals**:
- Implement distributed caching with Redis
- Add cache partitioning by user
- Implement cache compression
- Add cache statistics endpoint
- Create cache monitoring dashboard
- Implement cache warming on startup
- Add cache stampede prevention

**Sample Prompt for Redis**:
```
Replace in-memory caching with Redis distributed cache.
Install StackExchange.Redis package. Configure Redis connection.
Implement cache-aside pattern for user data. Add serialization/deserialization.
Handle Redis connection failures gracefully (fallback to source).
Add Redis health check. Support cache tagging for bulk invalidation.
```

---

### Exercise 10.2: Asynchronous Processing

**Goal**: Implement background job processing

**Sample Prompt**:
```
Add background job processing using Hangfire or similar.
When a user is created, enqueue a background job to send welcome email.
Create IEmailService interface with mock implementation that logs.
Configure Hangfire with in-memory storage (or SQL if available).
Add Hangfire dashboard at /hangfire (secured). Create recurring job
to cleanup old users (soft-deleted > 30 days). Add job retry policy.
Log job execution and failures.
```

**Acceptance Criteria**:
- [ ] Background job framework installed
- [ ] Jobs enqueued on user creation
- [ ] Job processing working correctly
- [ ] Dashboard accessible and secured
- [ ] Recurring jobs configured
- [ ] Job failures handled with retries

**Stretch Goals**:
- Implement job prioritization
- Add job scheduling (delayed execution)
- Create job status tracking
- Implement job cancellation
- Add batch job processing
- Create job monitoring alerts
- Implement dead letter queue

---

### Exercise 10.3: Graceful Shutdown

**Goal**: Handle application shutdown properly

**Sample Prompt**:
```
Implement graceful shutdown handling for the application.
Register IHostApplicationLifetime events. On shutdown signal (SIGTERM):
1. Stop accepting new requests
2. Wait for in-flight requests to complete (max 30 seconds)
3. Close database connections
4. Flush logs
5. Complete background jobs
Add /admin/shutdown endpoint for manual shutdown (authenticated).
Log shutdown events. Test with container stop command.
```

**Acceptance Criteria**:
- [ ] Shutdown events registered
- [ ] Stops accepting new requests
- [ ] Waits for in-flight requests
- [ ] Resources cleaned up properly
- [ ] Shutdown timeouts configured
- [ ] Shutdown logged appropriately

**Stretch Goals**:
- Implement pre-shutdown hooks
- Add shutdown coordination for replicas
- Create shutdown health check state
- Implement connection draining
- Add shutdown notifications
- Create graceful restart mechanism
- Test chaos engineering scenarios

---

### Exercise 10.4: Horizontal Scaling Considerations

**Goal**: Prepare application for multi-instance deployment

**Sample Prompt**:
```
Analyze and refactor the application for horizontal scaling.
Issues to address:
1. Static list storage - needs shared storage (database/Redis)
2. In-memory cache - needs distributed cache
3. Background jobs - need distributed job coordination
4. Session state - must be stateless or externalized
Create a document analyzing scaling bottlenecks and solutions.
Implement at least one fix (e.g., replace static list with repository pattern).
Add load balancer simulation with multiple instances.
```

**Acceptance Criteria**:
- [ ] Scaling analysis document created
- [ ] Shared state identified and documented
- [ ] At least one scaling issue fixed
- [ ] Application tested with multiple instances
- [ ] Session management addressed
- [ ] Recommendations documented

**Stretch Goals**:
- Implement distributed locking
- Add sticky session alternatives
- Create instance coordination mechanism
- Implement rate limiting across instances
- Add distributed tracing correlation
- Create capacity planning guide
- Implement auto-scaling triggers

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 9](module-09-tooling-automation.md) | [Next: Capstone Projects →](capstone-projects.md)
