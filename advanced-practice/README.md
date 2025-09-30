# Advanced Practice Instructions for GitHub Copilot

This guide provides advanced exercises for practicing GitHub Copilot features with the net-users-demo API project. These tasks build upon the foundational exercises and introduce real-world scenarios involving CRUD operations, testing, security, observability, and architecture patterns.

## Navigation

### Getting Started
📖 [How to Use This Guide](#how-to-use-this-guide)

### Modules
1. 📝 [Module 1: Core CRUD & API Design Enhancements](module-01-crud-enhancements.md)
2. ✅ [Module 2: Validation & Error Handling](module-02-validation-error-handling.md)
3. 🧪 [Module 3: Testing Expansion](module-03-testing-expansion.md)
4. 🌱 [Interlude: Introduction to GitHub Spec-Kit](spec-kit-intro.md)
5. 💾 [Module 4: Persistence & Data Layer Improvements](module-04-persistence-data-layer.md)
6. ⚙️ [Module 5: Configuration & Environment Management](module-05-configuration-environment.md)
7. 🔐 [Module 6: Security & Authentication](module-06-security-authentication.md)
8. 📊 [Module 7: Observability & Operations](module-07-observability-operations.md)
9. 📚 [Module 8: Documentation & Developer Experience](module-08-documentation-dx.md)
10. 🔧 [Module 9: Tooling & Automation](module-09-tooling-automation.md)
11. 🏗️ [Module 10: Architecture & Scalability](module-10-architecture-scalability.md)
12. 🎓 [Capstone Projects](capstone-projects.md)

### Additional Resources
- 💡 [Tips for Success](tips-and-resources.md#tips-for-success)
- 📖 [Additional Resources](tips-and-resources.md#additional-resources)
- ⚡ [Quick Reference: Spec-Kit Commands](tips-and-resources.md#quick-reference-spec-kit-commands)
- 📋 [Appendix: Spec-Kit Without uvx](tips-and-resources.md#appendix-spec-kit-without-uvx)

---

## Getting Started

### Fork and Clone the Repository

**Step 1: Fork the Repository**
1. Navigate to the repository on GitHub: `https://github.com/frye/net-users-demo`
2. Click the **Fork** button in the top-right corner
3. Select your GitHub account as the destination
4. Wait for GitHub to create your fork (this takes a few seconds)
5. You now have your own copy at `https://github.com/YOUR-USERNAME/net-users-demo`

**Step 2: Clone Your Fork**

Open your terminal and run:

```bash
# Clone your forked repository
git clone https://github.com/YOUR-USERNAME/net-users-demo.git

# Navigate into the project directory
cd net-users-demo
```

**Step 3: Set Up Upstream Remote (Optional but Recommended)**

This allows you to pull updates from the original repository:

```bash
# Add the original repository as upstream
git remote add upstream https://github.com/frye/net-users-demo.git

# Verify your remotes
git remote -v
```

You should see:
```
origin    https://github.com/YOUR-USERNAME/net-users-demo.git (fetch)
origin    https://github.com/YOUR-USERNAME/net-users-demo.git (push)
upstream  https://github.com/frye/net-users-demo.git (fetch)
upstream  https://github.com/frye/net-users-demo.git (push)
```

**Step 4: Sync with Upstream (When Needed)**

To get the latest changes from the original repository:

```bash
# Fetch changes from upstream
git fetch upstream

# Merge upstream changes into your main branch
git checkout main
git merge upstream/main

# Push updates to your fork
git push origin main
```

### Prerequisites

Before beginning these exercises:

1. **Complete the basic practice instructions** in `Copilot_Practice_Instructions.md`
2. **Familiarize yourself with TDD** using `Copilot_TDD_Practice_Instructions.md`
3. **Ensure your development environment is ready**: .NET 9 SDK installed, project builds successfully
4. **Review the project structure** in `.github/copilot-instructions.md`

## How to Use This Guide

Each exercise includes:
- **Goal**: What you'll accomplish
- **Sample Prompt**: What to ask Copilot
- **Acceptance Criteria**: How to verify success
- **Stretch Goals**: Optional advanced variations

**Pro Tip**: Try solving exercises with different Copilot features:
- 💬 **Copilot Chat**: For planning and guidance
- ⚡ **Inline Suggestions**: For quick code completion
- 🔧 **Copilot Edits**: For multi-file refactoring
- 📝 **Test Generation**: For creating test cases

---

## Learning Path Recommendations

### Beginner Path (Modules 1-3)
Perfect if you're new to ASP.NET Core or just getting started with Copilot:
1. Start with [Module 1](module-01-crud-enhancements.md) - Core CRUD operations
2. Move to [Module 2](module-02-validation-error-handling.md) - Input validation and error handling
3. Continue with [Module 3](module-03-testing-expansion.md) - Testing fundamentals

### Intermediate Path (Modules 4-7)
For developers comfortable with basics, ready to tackle architecture and operations:
1. Read the [Spec-Kit Introduction](spec-kit-intro.md) to learn structured development
2. Explore [Module 4](module-04-persistence-data-layer.md) - Data access patterns
3. Master [Module 5](module-05-configuration-environment.md) - Configuration management
4. Secure your API with [Module 6](module-06-security-authentication.md)
5. Add observability in [Module 7](module-07-observability-operations.md)

### Advanced Path (Modules 8-11)
For experienced developers looking to master professional practices:
1. Professional docs in [Module 8](module-08-documentation-dx.md)
2. Automation in [Module 9](module-09-tooling-automation.md)
3. Scale patterns in [Module 10](module-10-architecture-scalability.md)
4. Capstone challenge: [Complete Projects](capstone-projects.md)

---

## Quick Start

**Want to dive right in?**

1. 🚀 **Start with Module 1** - [Core CRUD & API Design](module-01-crud-enhancements.md)
2. 🌱 **Learn Spec-Kit** - [Structured development workflow](spec-kit-intro.md)
3. 🎓 **Challenge yourself** - [Capstone Projects](capstone-projects.md)

---

## Feedback and Contributions

Found an issue or have suggestions for additional exercises? Please open an issue or submit a pull request!

Happy coding with GitHub Copilot! 🚀
