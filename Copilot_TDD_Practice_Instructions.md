# Copilot_TDD_Practice_Instructions.md

# GitHub Copilot TDD Practice Instructions for C# User Profile REST API

Follow these tasks to practice Test-Driven Development (TDD) using GitHub Copilot's custom chat modes within your ASP.NET Core REST API project.

---

## ✅ Task 1: Implement Unit Tests Using Agent Mode

**Goal:** Create comprehensive unit tests for the existing User Profile API endpoints using Copilot's Agent mode.

### Step-by-Step Instructions:

1. **Open Copilot Agent Mode**
   - Open the Copilot Chat panel in VS Code (Ctrl+Shift+I or Cmd+Shift+I)
   - Click on the Agent button in the sidebar (or use the sparkle icon ✨)
   - Agent mode provides autonomous, multi-step task execution

2. **Generate and Run Unit Tests**
   - Use this simple prompt in Agent mode:

   ```
   Create and run unit tests for me
   ```

   - Agent will automatically:
     - Analyze the existing codebase
     - Create a test project structure (net-users-api.Tests)
     - Generate comprehensive xUnit test files
     - Add necessary NuGet packages (xUnit, Moq, etc.)
     - Run the tests and report results

3. **Review the Generated Tests**
   - Agent will create test files in the `net-users-api.Tests` project
   - Review the generated `UsersControllerTests.cs` for completeness
   - Verify tests cover all existing endpoints:
     - GetUsers (GET /api/v1/users)
     - GetUser by ID (GET /api/v1/users/{id})
     - CreateUser (POST /api/v1/users)
     - UpdateUser (PUT /api/v1/users/{id})

4. **Verify Test Results**
   - Agent will automatically run `dotnet test` and show results
   - All tests for existing functionality should pass ✅
   - Tests for DeleteUser will fail ❌ (expected - not yet implemented)

5. **Analyze Test Coverage (Optional)**
   - Run tests with coverage: `dotnet test --collect:"XPlat Code Coverage"`
   - Aim for >80% test coverage on implemented features

---

## ✅ Task 2: Implement Delete Functionality Using TDD Custom Chat Mode

**Goal:** Use Test-Driven Development approach to implement the missing `DeleteUser` functionality by writing tests first, then implementing the feature.

### Step-by-Step Instructions:

> **Note:** Copilot may combine several of these steps and complete them without you specifically prompting for it. Keep an eye on what is happening and the end result should have all of these steps completed.

1. **Switch to TDD Custom Chat Mode**
   - In the Copilot Chat panel, change the chat mode to "TDD"
   - This mode follows Test-Driven Development principles

2. **Request Delete Functionality Implementation**
   - Provide this prompt to the TDD chat mode:

   ```
   Implement a DeleteUser method for the REST API
   ```

   - The TDD chat mode will automatically intervene and suggest writing tests first
   - Follow Copilot's guidance to create tests before implementation

3. **Review and Accept Test Cases (Red Phase)**
   - Accept the generated test cases that Copilot suggests
   - Ensure tests cover all scenarios:
     - Successful deletion (204 No Content)
     - User not found (404 Not Found)
     - Edge cases

4. **Run Tests to Confirm They Fail (Red Phase)**
   - Run the tests: `dotnet test`
   - Confirm the DeleteUser tests fail (as expected, since function isn't implemented)
   - This validates we're in the "Red" phase of TDD

5. **Proceed with Implementation (Green Phase)**
   - Once tests are failing, ask Copilot to continue with the implementation:

   ```
   Now implement the DeleteUser method to make the tests pass
   ```

6. **Run Tests to Confirm They Pass (Green Phase)**
   - Run the tests again: `dotnet test`
   - Verify all tests now pass
   - Confirm we've reached the "Green" phase

7. **Refactor if Needed (Refactor Phase)**
   - Ask Copilot to review the implementation:

   ```
   Review the DeleteUser implementation for potential improvements
   ```

8. **Integration Testing**
   - Start the server: `dotnet run`
   - Test the delete endpoint manually:
     ```bash
     # Create a user first
     curl -X POST http://localhost:8080/api/v1/users \
       -H "Content-Type: application/json" \
       -d '{"id":"test123","fullName":"Test User","emoji":"🧪"}'
     
     # Delete the user
     curl -X DELETE http://localhost:8080/api/v1/users/test123 -v
     
     # Verify user is deleted
     curl http://localhost:8080/api/v1/users/test123
     ```

9. **Final Test Run**
    - Run all tests one final time: `dotnet test`
    - Ensure 100% of tests pass
    - Check test coverage: `dotnet test --collect:"XPlat Code Coverage"`

---

## 🎯 TDD Principles Reinforced

Through this exercise, you've practiced:

- **Red-Green-Refactor Cycle**: Writing failing tests first, implementing minimal code to pass, then refactoring
- **Test-First Development**: Defining behavior through tests before implementation
- **Incremental Development**: Building functionality step by step with immediate feedback
- **Agent Mode**: Leveraging Copilot's autonomous Agent mode for comprehensive test generation
- **TDD Custom Chat Mode**: Using specialized TDD mode for test-driven feature development

---

## 🎉 Completion

You've successfully practiced Test-Driven Development using GitHub Copilot's Agent and TDD modes! You now have:
- Comprehensive unit tests for your C# REST API (created via Agent mode)
- A fully implemented delete functionality following TDD principles (via TDD mode)
- Experience with Copilot's Agent mode for autonomous task execution
- Experience with Copilot's TDD custom chat mode for test-driven development
- A robust testing foundation for future development
