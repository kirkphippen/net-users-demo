# Module 3: Testing Expansion

[⬅️ Back to Index](README.md) | [← Previous: Module 2](module-02-validation-error-handling.md) | [Next: Spec-Kit Intro →](spec-kit-intro.md)

---

### Exercise 3.1: Comprehensive Unit Test Suite

**Goal**: Achieve high unit test coverage for controllers and models

**Sample Prompt**:
```
Create an xUnit test project at solution level: net-users-api.Tests.
Add tests for UsersController covering all endpoints: GetUsers, GetUser, 
CreateUser, UpdateUser, DeleteUser. Test happy paths and error cases.
Mock the logger. Use the Arrange-Act-Assert pattern.
Test edge cases: null inputs, invalid IDs, duplicate entries, empty lists.
```

**Acceptance Criteria**:
- [ ] Test project created with xUnit
- [ ] Tests for all controller methods
- [ ] Both success and failure paths covered
- [ ] Edge cases tested
- [ ] Tests use proper naming convention
- [ ] All tests pass

**Stretch Goals**:
- Achieve >80% code coverage
- Use theory/inline data for parameterized tests
- Add performance/benchmark tests
- Test concurrent access scenarios
- Add mutation testing
- Use AutoFixture for test data generation

**Sample Prompt for Advanced Testing**:
```
Enhance the test suite with Theory tests using InlineData for multiple scenarios.
Add a test class for testing pagination: various page sizes, boundary conditions,
invalid page numbers. Use AutoFixture to generate test data.
Add a benchmark test using BenchmarkDotNet to measure GetUsers performance.
```

---

### Exercise 3.2: Integration Testing

**Goal**: Create integration tests that exercise the full API stack

**Sample Prompt**:
```
Create integration tests using WebApplicationFactory in the test project.
Test the full HTTP request/response cycle for all endpoints.
Use a test fixture to setup and teardown the test server.
Test content negotiation, status codes, response headers, and JSON serialization.
Create a test data seeder for consistent test state.
```

**Acceptance Criteria**:
- [ ] Integration test class created
- [ ] Uses `WebApplicationFactory<Program>`
- [ ] Tests actual HTTP requests/responses
- [ ] Tests don't interfere with each other (isolated)
- [ ] Validates response status codes and content
- [ ] All integration tests pass

**Stretch Goals**:
- Use TestContainers for database integration
- Test with different content types (JSON, XML)
- Add tests for middleware pipeline
- Test authentication/authorization flows
- Add load testing scenarios
- Test graceful degradation

---

### Exercise 3.3: Property-Based Testing

**Goal**: Use property-based testing to find edge cases

**Sample Prompt**:
```
Add FsCheck or FsCheck.Xunit to the test project for property-based testing.
Create property tests for UserProfile validation: any valid user should
successfully serialize/deserialize, Id generation should always produce unique values,
email validation should accept all valid RFC formats.
Test that pagination always returns correct counts and never loses/duplicates items.
```

**Acceptance Criteria**:
- [ ] FsCheck or similar library added
- [ ] At least 3 property-based tests created
- [ ] Tests find edge cases not covered by example-based tests
- [ ] Properties are clearly defined and documented
- [ ] Tests run with sufficient iterations

**Stretch Goals**:
- Add shrinking to find minimal failing cases
- Test invariants that should always hold
- Use custom generators for domain objects
- Integrate fuzzing for input validation
- Create property tests for API contract compliance

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 2](module-02-validation-error-handling.md) | [Next: Spec-Kit Intro →](spec-kit-intro.md)
