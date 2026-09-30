---
globs: ["*.ts", "*.tsx"]
---

# TypeScript Testing Rules

## General Principles
- Every new feature or bug fix must include tests
- Tests must be deterministic — no flaky tests allowed
- Prefer integration tests over unit tests when testing behavior across boundaries
- Use descriptive test names that explain the scenario and expected outcome
- Follow Arrange-Act-Assert (AAA) pattern
- Each test should verify one behavior
- Do not mock what you don't own — wrap external dependencies behind interfaces and mock those

## Structure
- Colocate test files next to source: `service.ts` → `service.test.ts`
- Use the naming convention: `describe('ClassName/module')` → `it('should ... when ...')`
- Group related tests with nested `describe` blocks

## Frameworks & Libraries
- Use **Vitest** as the default test runner (or Jest if already in use)
- Use built-in `expect` for assertions
- Use `vi.fn()` / `vi.mock()` (Vitest) or `jest.fn()` / `jest.mock()` for mocking
- Use **MSW** (Mock Service Worker) for HTTP mocking in integration tests
- Use **Testing Library** for component tests (React, Vue, etc.)
- Use **Supertest** for Express/Fastify API integration tests

## Practices
- Prefer `it.each` / `test.each` for parameterized tests
- Mock at module boundaries, not deep internals
- Use `beforeEach` for shared setup; avoid shared mutable state between tests
- Always `await` async operations — never let promises float
- Use `vi.useFakeTimers()` when testing time-dependent logic, and restore after

## Example
```typescript
describe('OrderService', () => {
  describe('placeOrder', () => {
    it('should return confirmation for a valid order', async () => {
      // Arrange
      const repo = { save: vi.fn().mockResolvedValue(undefined) };
      const sut = new OrderService(repo);
      const order = { items: [{ name: 'Widget', qty: 2 }] };

      // Act
      const result = await sut.placeOrder(order);

      // Assert
      expect(result.status).toBe('confirmed');
      expect(repo.save).toHaveBeenCalledOnce();
    });
  });
});
```
