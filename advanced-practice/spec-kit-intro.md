# Interlude: Introduction to GitHub Spec-Kit

[⬅️ Back to Index](README.md) | [← Previous: Module 3](module-03-testing-expansion.md) | [Next: Module 4 →](module-04-persistence-data-layer.md)

---

Before diving into more complex architectural patterns, let's explore **GitHub Spec-Kit** - a powerful tool for spec-driven development that can help you plan and implement features systematically with AI assistance.

### What is Spec-Kit?

**Spec-Kit** is a spec-driven development toolkit that helps you build high-quality software faster by focusing on **what** you want to build (specifications) before diving into **how** to build it (implementation). It provides structured workflows with slash commands that guide you through:

1. **Specification** - Define what you're building and why
2. **Planning** - Choose your tech stack and architecture
3. **Task Breakdown** - Generate actionable, dependency-ordered tasks
4. **Implementation** - Execute the plan systematically

### Why Use Spec-Kit?

- 📋 **Structured Approach**: Follow a proven workflow from idea to implementation
- 🤖 **AI-Powered**: Works seamlessly with GitHub Copilot, Claude, and other AI agents
- 📝 **Documentation-First**: Generate comprehensive specs, plans, and task lists
- 🔄 **Iterative Refinement**: Refine specifications before writing code
- ✅ **Test-Driven**: Built-in support for TDD workflows

### Getting Started with Spec-Kit

#### Installing uvx (Required)

Spec-Kit uses **uvx** (part of the uv package manager) to run without installation. Here's how to install it on common platforms:

**macOS/Linux**:
```bash
# Install uv (includes uvx)
curl -LsSf https://astral.sh/uv/install.sh | sh
```

**Windows (PowerShell)**:
```powershell
# Install uv (includes uvx)
powershell -c "irm https://astral.sh/uv/install.ps1 | iex"
```

**Alternative - Using pip**:
```bash
pip install uv
```

**Verify installation**:
```bash
uvx --version
```

For more installation options, see the [uv documentation](https://docs.astral.sh/uv/getting-started/installation/).

> **Can't install uvx?** See [Appendix: Spec-Kit Without uvx](#appendix-spec-kit-without-uvx) for alternative installation methods.

#### Installation in This Repository

To initialize Spec-Kit in your cloned net-users-demo repository:

```bash
# Navigate to your project root
cd /path/to/net-users-demo

# Initialize Spec-Kit in the current directory
uvx --from git+https://github.com/github/spec-kit.git specify init --here --ai copilot --force
```

**What these flags mean**:
- `--here`: Initialize in the current directory (instead of creating a new folder)
- `--ai copilot`: Configure for GitHub Copilot
- `--force`: Allow initialization in a non-empty directory

> **Note**: For other initialization options (new projects, different AI agents, persistent installation), see the [Spec-Kit Installation Guide](https://github.com/github/spec-kit/blob/main/docs/installation.md).

#### Prerequisites

Before running Spec-Kit, ensure you have:
- **Python 3.11+** installed
- **uv** package manager: `pip install uv` or follow [uv installation docs](https://docs.astral.sh/uv/)
- **Git** installed
- **GitHub Copilot** (or another supported AI agent)

#### Verification

After initialization, you should see:
- `.specify/` directory with templates and scripts
- New slash commands available: `/specify`, `/plan`, `/tasks`, `/implement`
- Scripts in both `.sh` (Bash) and `.ps1` (PowerShell) formats

To verify everything is set up correctly:

```bash
# Check that all required tools are installed
uvx --from git+https://github.com/github/spec-kit.git specify check
```

### Core Spec-Kit Workflow

#### 1. Establish Project Principles with `/constitution`

Before creating features, establish your project's governing principles:

```text
/constitution Create principles focused on C# best practices, RESTful API design, 
comprehensive testing, clean architecture, and maintainability. Include guidance 
for ASP.NET Core development and API documentation standards.
```

This creates `.specify/memory/constitution.md` with your project's foundational guidelines.

#### 2. Create Specification with `/specify`

Define **what** you want to build without worrying about the tech details:

```text
/specify Add user authentication to the API. Users should be able to register 
with email and password, log in to receive a JWT token, and use that token to 
access protected endpoints. Include password reset functionality and email verification.
```

This creates a detailed specification document in `specs/[feature-number]/spec.md`.

#### 3. Generate Technical Plan with `/plan`

Now specify **how** to build it - your tech stack and architecture:

```text
/plan Use ASP.NET Core Identity for user management with Entity Framework Core 
and PostgreSQL. Implement JWT bearer authentication with refresh tokens. 
Use FluentValidation for input validation. Add email service abstraction 
with SendGrid implementation. Follow the existing repository pattern.
```

This generates:
- `plan.md` - Overall implementation plan
- `data-model.md` - Entity definitions and relationships
- `contracts/` - API specifications (OpenAPI schemas)
- `research.md` - Technical decisions and library choices
- `quickstart.md` - Test scenarios

#### 4. Break Down into Tasks with `/tasks`

Generate an actionable, dependency-ordered task list:

```text
/tasks
```

This creates `tasks.md` with:
- Sequential tasks with clear dependencies
- Parallel execution opportunities marked
- Test-first approach for each task
- Acceptance criteria per task

#### 5. Execute Implementation with `/implement`

Execute the plan systematically:

```text
/implement
```

The AI agent will:
- Execute tasks in dependency order
- Follow TDD principles
- Run tests after each implementation
- Handle errors and provide progress updates

### Using Spec-Kit with Existing Features

You can use Spec-Kit to plan additions to the existing net-users-demo API:

**Sample Prompt for Spec-Kit Planning**:
```text
/specify Add pagination, filtering, and sorting capabilities to the existing 
GET /users endpoint. Support query parameters for page number, page size, 
search term filtering on FullName, and sorting by any field. Include 
pagination metadata in the response.
```

Then follow with:
```text
/plan Use LINQ for query building, add PaginationHelper class for 
consistent pagination logic across endpoints, return pagination metadata 
in custom headers (X-Total-Count, X-Page-Number, X-Page-Size). 
Add comprehensive validation for pagination parameters.
```

### Spec-Kit Resources

- 📘 **GitHub Repository**: [github.com/github/spec-kit](https://github.com/github/spec-kit)
- 📖 **Full Documentation**: [Spec-Driven Development Guide](https://github.com/github/spec-kit/blob/main/spec-driven.md)
- 🎥 **Video Overview**: [Watch on YouTube](https://www.youtube.com/watch?v=a9eR1xsfvHg)
- 💬 **Support**: [Open an Issue](https://github.com/github/spec-kit/issues/new)

### Exercise: Initialize Spec-Kit (Optional)

**Goal**: Set up Spec-Kit in your net-users-demo repository

**Steps**:
1. Ensure prerequisites are installed (Python 3.11+, uv, Git)
2. Navigate to your project root
3. Run: `uvx --from git+https://github.com/github/spec-kit.git specify init --here --ai copilot --force`
4. Verify: Check for `.specify/` directory and slash commands
5. Create constitution: `/constitution` with .NET/C# best practices

**Acceptance Criteria**:
- [ ] `.specify/` directory created with templates and scripts
- [ ] Slash commands available in GitHub Copilot Chat
- [ ] `constitution.md` created with project principles
- [ ] Scripts are executable (`.sh` files on Unix-like systems)

**Benefits**:
Once set up, you can use Spec-Kit slash commands for all subsequent exercises in this guide, providing a more structured and systematic approach to feature development.

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 3](module-03-testing-expansion.md) | [Next: Module 4 →](module-04-persistence-data-layer.md)
