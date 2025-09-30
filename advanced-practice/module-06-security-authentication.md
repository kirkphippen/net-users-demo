# Module 6: Security & Authentication

[⬅️ Back to Index](README.md) | [← Previous: Module 5](module-05-configuration-environment.md) | [Next: Module 7 →](module-07-observability-operations.md)

---

### Exercise 6.1: API Key Authentication

**Goal**: Implement basic API key authentication

**Sample Prompt**:
```
Implement API key authentication for the users API.
Clients must provide X-API-Key header with valid key.
Store API keys in configuration (for now). Create an authentication middleware
that validates the key. Return 401 Unauthorized if missing/invalid.
Allow unauthenticated access to /health endpoint.
Add multiple API keys with different permissions (read-only vs full access).
```

**Acceptance Criteria**:
- [ ] Authentication middleware created
- [ ] X-API-Key header validated
- [ ] Returns 401 for invalid/missing keys
- [ ] Configuration stores valid keys
- [ ] Health endpoint remains public
- [ ] Tests verify authentication

**Stretch Goals**:
- Implement API key management endpoints
- Add key rotation mechanism
- Support key expiration
- Implement rate limits per API key
- Add API key usage analytics
- Create key hashing/storage security

---

### Exercise 6.2: JWT-Based Authentication

**Goal**: Implement modern token-based authentication

**Sample Prompt**:
```
Implement JWT authentication for the API.
Add a /api/v1/auth/login endpoint that accepts username/password
and returns a JWT token. Protect user endpoints with [Authorize] attribute.
Configure JWT bearer authentication in Program.cs with signing key from config.
Add token expiration (15 minutes). Include user ID and role in JWT claims.
Add /api/v1/auth/refresh endpoint for token renewal.
```

**Acceptance Criteria**:
- [ ] JWT authentication configured
- [ ] Login endpoint returns valid tokens
- [ ] Protected endpoints require valid tokens
- [ ] Token expiration works correctly
- [ ] Claims included in tokens
- [ ] Refresh token endpoint implemented

**Stretch Goals**:
- Implement refresh token rotation
- Add token revocation/blacklist
- Support multiple token audiences
- Implement sliding expiration
- Add OAuth2/OpenID Connect flows
- Create token introspection endpoint

**Sample Prompt for OAuth2**:
```
Enhance the authentication to support OAuth2 authorization code flow.
Install Microsoft.AspNetCore.Authentication.OAuth package.
Configure GitHub or Google as OAuth provider. Add callback endpoint
for OAuth redirect. Store provider tokens securely. Support multiple providers.
```

---

### Exercise 6.3: Input Security Hardening

**Goal**: Protect against common security vulnerabilities

**Sample Prompt**:
```
Implement comprehensive input security measures.
Add request size limits (max 1MB). Implement input sanitization for string fields.
Add SQL injection prevention (demonstrate with parameterized queries).
Implement XSS protection headers. Add CSRF protection for state-changing operations.
Create security middleware that adds security headers: X-Content-Type-Options,
X-Frame-Options, X-XSS-Protection, Strict-Transport-Security.
```

**Acceptance Criteria**:
- [ ] Request size limits enforced
- [ ] Input validation prevents injection attacks
- [ ] Security headers added to responses
- [ ] CSRF tokens implemented
- [ ] Content Security Policy configured
- [ ] Security tests pass

**Stretch Goals**:
- Implement Content Security Policy (CSP)
- Add CORS configuration with whitelist
- Implement request signing
- Add IP whitelist/blacklist
- Create security audit logging
- Implement honeypot fields

---

### Exercise 6.4: Audit Logging

**Goal**: Create comprehensive audit trail for sensitive operations

**Sample Prompt**:
```
Implement audit logging for all user creation, modification, and deletion.
Create AuditLog model with: Id, Timestamp, UserId, Action, EntityType, EntityId,
OldValue (JSON), NewValue (JSON), IpAddress, UserAgent.
Store audit logs separately from application logs.
Create a repository for audit logs. Add endpoint to query audit history.
Ensure audit logs are tamper-evident (add hash chain).
```

**Acceptance Criteria**:
- [ ] AuditLog model created
- [ ] Audit logging middleware/filter implemented
- [ ] All CUD operations logged
- [ ] Audit logs stored separately
- [ ] Query endpoint for audit history
- [ ] Includes contextual information (IP, user)

**Stretch Goals**:
- Implement audit log signing
- Add audit log encryption
- Create hash chain for tamper detection
- Implement audit log retention policies
- Add compliance reporting (GDPR, HIPAA)
- Create audit log export functionality

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 5](module-05-configuration-environment.md) | [Next: Module 7 →](module-07-observability-operations.md)
