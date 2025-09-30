# Module 2: Validation & Error Handling

[⬅️ Back to Index](README.md) | [← Previous: Module 1](module-01-crud-enhancements.md) | [Next: Module 3 →](module-03-testing-expansion.md)

---

### Exercise 2.1: Centralized Error Model

**Goal**: Create consistent error response structure across all endpoints

**Sample Prompt**:
```
Create a centralized error response model in Models folder called ApiError.
Include properties: ErrorCode (string), Message (string), Details (optional object),
Timestamp (DateTime), Path (request path).
Create an exception filter or middleware to catch exceptions and return 
consistent ApiError responses. Map common exceptions to appropriate status codes.
```

**Acceptance Criteria**:
- [ ] `ApiError` model created with required properties
- [ ] Global exception handler implemented
- [ ] All endpoints return consistent error format
- [ ] HTTP status codes properly mapped
- [ ] Include request path and timestamp in errors

**Stretch Goals**:
- Implement RFC 7807 Problem Details (application/problem+json)
- Add correlation IDs for error tracking
- Create custom exception classes for domain errors
- Add stack trace in development environment only
- Implement error code enumeration/catalog

**Sample Prompt for RFC 7807**:
```
Refactor the error handling to use RFC 7807 Problem Details format.
Use Microsoft.AspNetCore.Mvc.ProblemDetails as the base.
Include type (URI), title, status, detail, instance, and custom extension members.
Configure the API to return application/problem+json content type for errors.
```

---

### Exercise 2.2: Comprehensive Input Validation

**Goal**: Add robust validation layer for all input data

**Sample Prompt**:
```
Add comprehensive validation to UserProfile model and API endpoints.
Use data annotations: Required, StringLength, EmailAddress, RegularExpression.
Email must be valid format. FullName must be 2-100 characters.
Emoji must be a single emoji character. Id must follow specific format (alphanumeric, 1-50 chars).
Return 400 Bad Request with specific validation errors listing each field problem.
```

**Acceptance Criteria**:
- [ ] Data annotations added to `UserProfile` properties
- [ ] Model validation executed automatically on requests
- [ ] Returns 400 with field-specific validation errors
- [ ] Error messages are clear and actionable
- [ ] Custom validators implemented where needed

**Stretch Goals**:
- Implement FluentValidation library for complex rules
- Add conditional validation (e.g., field required if another is set)
- Internationalize validation error messages
- Create reusable validation attributes
- Add business rule validation (e.g., no duplicate names)

**Sample Prompt for FluentValidation**:
```
Install and configure FluentValidation for ASP.NET Core.
Create UserProfileValidator class with validation rules.
Email is required and must be valid. FullName required, 2-100 chars, no numbers.
Emoji must be 1-10 characters. Id required, alphanumeric only.
Register validator with DI and integrate with model binding validation.
```

---

### Exercise 2.3: Rate Limiting Implementation

**Goal**: Protect API from abuse with rate limiting

**Sample Prompt**:
```
Implement a simple rate limiting middleware for the API.
Limit requests to 100 per minute per IP address.
Use in-memory token bucket or sliding window algorithm.
Return 429 Too Many Requests when limit exceeded, include Retry-After header.
Add configuration for limits in appsettings.json.
Exclude /health endpoint from rate limiting.
```

**Acceptance Criteria**:
- [ ] Rate limiting middleware created
- [ ] Tracks requests by IP address
- [ ] Returns 429 status with Retry-After header
- [ ] Configuration stored in appsettings
- [ ] Health check endpoint excluded
- [ ] Thread-safe implementation

**Stretch Goals**:
- Implement distributed rate limiting with Redis
- Add different limits per endpoint
- API key-based rate limiting (higher limits for authenticated users)
- Rate limit by user ID instead of IP
- Implement leaky bucket algorithm
- Add rate limit status headers (X-RateLimit-Limit, X-RateLimit-Remaining)

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 1](module-01-crud-enhancements.md) | [Next: Module 3 →](module-03-testing-expansion.md)
