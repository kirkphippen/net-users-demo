# Module 4: Persistence & Data Layer Improvements

[⬅️ Back to Index](README.md) | [← Previous: Spec-Kit Intro](spec-kit-intro.md) | [Next: Module 5 →](module-05-configuration-environment.md)

---

**Note**: The exercises in this module can be approached using traditional prompting or by using Spec-Kit slash commands for a more structured workflow.

### Exercise 4.1: Repository Pattern Implementation

**Goal**: Abstract data access behind repository interfaces

#### Approach 1: Traditional Prompting

**Sample Prompt**:
```
Implement the Repository pattern to abstract data access.
Create IUserRepository interface with methods: GetAll, GetById, Add, Update, Delete.
Create InMemoryUserRepository class that implements the interface using the existing static list.
Update UsersController to use IUserRepository through dependency injection.
Register the repository in Program.cs service collection.
```

#### Approach 2: Using Spec-Kit (Recommended)

**Step 1 - Specify**:
```text
/specify Refactor the user data access layer to use the Repository pattern.
Create an IUserRepository interface that abstracts all user data operations 
(GetAll, GetById, Add, Update, Delete). Implement an in-memory version that 
wraps the existing static list. Update the UsersController to depend on the 
interface rather than accessing the list directly. This will make it easier 
to swap data storage implementations in the future.
```

**Step 2 - Plan**:
```text
/plan Create IUserRepository interface in a new Repositories folder. Add async 
methods for all CRUD operations. Implement InMemoryUserRepository with 
thread-safe operations. Update UsersController constructor to inject 
IUserRepository. Register repository as singleton in Program.cs. 
Ensure all existing tests still pass. Follow SOLID principles, 
particularly dependency inversion.
```

**Step 3 - Generate Tasks & Implement**:
```text
/tasks
/implement
```

**Acceptance Criteria**:
- [ ] `IUserRepository` interface created
- [ ] `InMemoryUserRepository` implementation created
- [ ] Controller refactored to use repository
- [ ] DI configured in Program.cs
- [ ] All existing functionality works unchanged
- [ ] Repository methods are async

**Stretch Goals**:
- Create additional implementations (SQL, file-based)
- Add specification pattern for queries
- Implement Unit of Work pattern
- Add generic repository base class
- Create repository tests with mock implementation
- Add repository caching layer

**Stretch with Spec-Kit**:
```text
/specify Extend the repository pattern to support multiple storage backends.
Add a SqliteUserRepository implementation using Entity Framework Core. 
Allow configuration-based selection between InMemory and Sqlite storage. 
The application should be able to switch between storage types by changing 
appsettings.json without code modifications.

/plan Use Entity Framework Core with SQLite provider. Create UserDbContext 
with UserProfile entity. Add factory pattern (IUserRepositoryFactory) to 
create repositories based on configuration. Add StorageType enum and 
configuration section in appsettings.json. Include database migrations 
for SQLite. Add connection string configuration. Ensure proper disposal 
of database contexts.
```

---

### Exercise 4.2: Database Migrations

**Goal**: Implement proper database schema versioning

#### Approach 1: Traditional Prompting

**Sample Prompt**:
```
Add Entity Framework Core migrations to the project.
Install Microsoft.EntityFrameworkCore.Design and Tools packages.
Create initial migration for UserProfile entity with SQLite provider.
Add migration for adding Email property. Add migration for soft delete support.
Create a database initialization/seeding strategy for development data.
Document the migration workflow in README.
```

#### Approach 2: Using Spec-Kit

**Spec-Kit Workflow**:
```text
/specify Add database migration support to the project using Entity Framework Core.
Implement a proper migration strategy for schema versioning. Create an initial 
migration for the UserProfile entity. Add subsequent migrations for schema evolution 
(Email property, soft delete support). Include database seeding for development data. 
Document the migration workflow for other developers.

/plan Use Entity Framework Core with SQLite provider. Install required packages: 
Microsoft.EntityFrameworkCore.Design and Microsoft.EntityFrameworkCore.Tools. 
Create UserDbContext with UserProfile entity. Generate initial migration using 
dotnet ef migrations add. Create HasData configuration for seed data. 
Add migration documentation to README with commands for add, update, remove. 
Include appsettings configuration for connection strings.

/tasks
/implement
```

**Acceptance Criteria**:
- [ ] EF Core packages installed
- [ ] DbContext created with UserProfile DbSet
- [ ] Initial migration created and applied
- [ ] Additional migrations for schema changes
- [ ] Seed data configured
- [ ] Migration commands documented

**Stretch Goals**:
- Add automatic migration on startup (dev only)
- Implement migration rollback testing
- Add migration history tracking
- Create data migration scripts
- Add idempotent migration support
- Implement blue-green deployment migrations

---

### Exercise 4.3: Optimistic Concurrency Control

**Goal**: Prevent concurrent update conflicts

#### Approach 1: Traditional Prompting

**Sample Prompt**:
```
Add optimistic concurrency control to prevent lost updates.
Add a Version or RowVersion property to UserProfile (timestamp or int).
Increment version on each update. Accept If-Match header with version in PUT requests.
Return 409 Conflict if version doesn't match (concurrent update detected).
Include current version in ETag response header for GET requests.
```

#### Approach 2: Using Spec-Kit

**Spec-Kit Workflow**:
```text
/specify Implement optimistic concurrency control to prevent lost updates in 
concurrent scenarios. Add versioning to UserProfile entities. When a user is 
updated, verify the version hasn't changed since it was read. If concurrent 
modification is detected, return an appropriate error. Use HTTP ETags to 
communicate version information to clients following REST best practices.

/plan Add RowVersion property to UserProfile (byte array with [Timestamp] attribute). 
Configure EF Core concurrency token. Update PUT endpoint to accept If-Match header 
with ETag. Compare ETag with current RowVersion. Return 409 Conflict with helpful 
message if mismatch. Add ETag to response headers for GET requests. 
Handle DbUpdateConcurrencyException. Add middleware to automatically set ETag headers. 
Create integration tests for concurrent update scenarios.

/tasks
/implement
```

**Acceptance Criteria**:
- [ ] Version property added to model
- [ ] Version checked before updates
- [ ] Returns 409 on version mismatch
- [ ] ETag headers implemented
- [ ] Version included in responses
- [ ] Tests verify conflict detection

**Stretch Goals**:
- Implement automatic retry with exponential backoff
- Add conflict resolution strategies
- Support weak vs strong ETags
- Implement version history tracking
- Add precondition checks (If-Unmodified-Since)

---

### 💡 Spec-Kit for Remaining Modules

**You've now learned both traditional prompting and Spec-Kit workflows!**

For the remaining modules (5-10), you can apply either approach. We recommend using Spec-Kit's structured workflow (`/specify` → `/plan` → `/tasks` → `/implement`) for:

- **Complex multi-component features** (e.g., authentication systems, observability stacks)
- **Features requiring detailed planning** (e.g., multi-environment configs, CI/CD pipelines)
- **When you want comprehensive documentation** (specs, plans, tasks all generated automatically)

**Pro Tip**: You can mix approaches! Use Spec-Kit for initial planning and architecture, then use direct prompts for quick tactical changes.

---

---

[⬅️ Back to Index](README.md) | [← Previous: Spec-Kit Intro](spec-kit-intro.md) | [Next: Module 5 →](module-05-configuration-environment.md)
