---
globs: "*.cs"
---

# .NET Testing Rules

## General Principles
- Every new feature or bug fix must include tests
- Tests must be deterministic — no flaky tests allowed
- Prefer integration tests over unit tests when testing behavior across boundaries
- Use descriptive test names that explain the scenario and expected outcome
- Follow Arrange-Act-Assert (AAA) pattern
- Each test should verify one behavior
- Do not mock what you don't own — wrap external dependencies behind interfaces and mock those

## Structure
- Test projects mirror source projects: `MyProject` → `MyProject.Tests`
- Use the naming convention: `MethodName_Scenario_ExpectedResult`
- Group tests by class under test using nested classes or separate files

## Frameworks & Libraries
- Use **xUnit** as the default test framework (or NUnit if already in use)
- Use **FluentAssertions** for readable assertions
- Use **NSubstitute** or **Moq** for mocking
- Use **Bogus** for test data generation when needed
- Use **TestContainers** for integration tests requiring real infrastructure

## Practices
- Use `[Fact]` for single-case tests and `[Theory]` with `[InlineData]` for parameterized tests
- Register test dependencies via a test-specific DI container or fixture
- Use `IAsyncLifetime` for async setup/teardown
- Do not use `Thread.Sleep` — use async patterns and cancellation tokens
- Keep test execution fast: mock I/O at the boundary, use in-memory databases only when appropriate

## Example
```csharp
public class OrderService_PlaceOrder
{
    [Fact]
    public async Task ValidOrder_ReturnsConfirmation()
    {
        // Arrange
        var repo = Substitute.For<IOrderRepository>();
        var sut = new OrderService(repo);
        var order = new Order { Items = [new("Widget", 2)] };

        // Act
        var result = await sut.PlaceOrderAsync(order);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(OrderStatus.Confirmed);
        await repo.Received(1).SaveAsync(Arg.Any<Order>());
    }
}
```
