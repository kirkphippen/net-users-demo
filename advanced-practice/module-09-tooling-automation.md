# Module 9: Tooling & Automation

[⬅️ Back to Index](README.md) | [← Previous: Module 8](module-08-documentation-dx.md) | [Next: Module 10 →](module-10-architecture-scalability.md)

---

### Exercise 9.1: Build Automation with Make/Script

**Goal**: Simplify common development tasks

**Sample Prompt**:
```
Create a Makefile (or build.sh script for cross-platform) with targets for:
- build: Compile the solution
- test: Run all tests with coverage
- run: Start the application
- clean: Remove build artifacts
- restore: Restore NuGet packages
- lint: Run code analysis
- format: Format code
- docker-build: Build Docker image
Add help target that lists all available targets.
Make targets idempotent and composable.
```

**Acceptance Criteria**:
- [ ] Makefile or build script created
- [ ] All common tasks automated
- [ ] Help documentation included
- [ ] Works on all target platforms
- [ ] Properly handles errors
- [ ] Documents any prerequisites

**Stretch Goals**:
- Add watch mode for auto-rebuild
- Implement parallel execution
- Add pre-commit hooks
- Create release automation
- Add dependency update checks
- Implement task caching
- Create task dependency graph

---

### Exercise 9.2: CI/CD Pipeline

**Goal**: Automate build, test, and deployment

**Sample Prompt**:
```
Create a GitHub Actions workflow for CI/CD in .github/workflows/ci.yml.
Trigger on push to main and pull requests. Jobs:
1. Build - restore, build, run tests
2. Test - run unit and integration tests, publish coverage
3. Analyze - run code quality checks (SonarCloud optional)
4. Package - build Docker image
5. Deploy - deploy to staging (can be manual approval)
Use matrix strategy to test on multiple .NET versions.
Cache dependencies for faster builds. Add status badges to README.
```

**Acceptance Criteria**:
- [ ] GitHub Actions workflow created
- [ ] Build and test steps working
- [ ] Runs on PRs and main branch
- [ ] Test results published
- [ ] Build artifacts saved
- [ ] Status badges in README

**Stretch Goals**:
- Add deployment to multiple environments
- Implement semantic versioning
- Add release automation
- Create deployment rollback capability
- Add smoke tests after deployment
- Implement feature flag deployments
- Add security scanning (Dependabot, CodeQL)

**Sample Prompt for Advanced Pipeline**:
```
Enhance the CI/CD pipeline with advanced features:
- Add CodeQL security scanning
- Implement semantic release with conventional commits
- Add Docker image signing and scanning
- Create separate staging and production deployment jobs with approvals
- Add rollback job in case deployment fails smoke tests
- Publish NuGet package on release
- Send notifications to Slack on build status
```

---

### Exercise 9.3: Containerization

**Goal**: Package application as Docker container

**Sample Prompt**:
```
Create a production-ready Dockerfile for the application.
Use multi-stage build: restore, build, publish, runtime.
Use official .NET SDK and ASP.NET runtime images.
Optimize image size (use alpine or distroless base).
Set non-root user for security. Expose port 8080.
Add health check instruction. Include build args for version info.
Create docker-compose.yml for local development with dependencies.
Create .dockerignore file.
```

**Acceptance Criteria**:
- [ ] Dockerfile created with multi-stage build
- [ ] Image builds successfully
- [ ] Container runs application correctly
- [ ] Image size optimized
- [ ] Runs as non-root user
- [ ] docker-compose.yml for local dev
- [ ] .dockerignore configured

**Stretch Goals**:
- Create separate debug and release images
- Add Docker build caching optimization
- Implement image scanning for vulnerabilities
- Create Kubernetes deployment manifests
- Add SBOM generation
- Implement image signing
- Create Docker Compose for full stack

**Sample Prompt for Kubernetes**:
```
Create Kubernetes deployment manifests for the application.
Include: Deployment, Service (ClusterIP), Ingress, ConfigMap, Secret.
Configure resource limits and requests. Add liveness and readiness probes.
Use environment-specific overlays with Kustomize.
Add HorizontalPodAutoscaler for auto-scaling. Document deployment commands.
```

---

### Exercise 9.4: Code Quality Tools

**Goal**: Enforce code quality standards

**Sample Prompt**:
```
Set up comprehensive code quality tooling for the project.
Configure .editorconfig with C# coding conventions.
Add .NET analyzers (Microsoft.CodeAnalysis.NetAnalyzers).
Configure StyleCop or Roslynator for additional rules.
Set warnings as errors for critical issues. Add code coverage threshold (80%).
Create pre-commit Git hook that runs format and lint.
Add SonarCloud or similar for static analysis (optional).
Document how to run quality checks locally.
```

**Acceptance Criteria**:
- [ ] .editorconfig configured
- [ ] Analyzers installed and configured
- [ ] Linting rules enforced
- [ ] Format checking automated
- [ ] Coverage thresholds set
- [ ] Pre-commit hooks installed
- [ ] Quality checks in CI

**Stretch Goals**:
- Add custom analyzers for domain rules
- Implement code complexity metrics
- Add duplicate code detection
- Create custom code fix providers
- Implement architecture rules (ArchUnit)
- Add technical debt tracking
- Create code review checklists

---

---

[⬅️ Back to Index](README.md) | [← Previous: Module 8](module-08-documentation-dx.md) | [Next: Module 10 →](module-10-architecture-scalability.md)
