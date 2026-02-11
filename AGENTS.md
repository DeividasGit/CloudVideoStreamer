## Review guidelines

## Purpose
These rules define non-negotiable architectural and coding standards.
Pull requests must comply. Violations should be flagged explicitly.

---

## Backend (.NET Web API)

### Layering (Strict)
Controllers → Services → Repositories → DbContext

Skipping layers is forbidden.

### Controllers
- MUST NOT reference:
  - DbContext
  - EF Core entities
  - Repositories
- MUST:
  - Call Services only
  - Contain no business logic
  - Handle HTTP concerns only (status codes, validation responses)

### Services
- MUST:
  - Contain business logic
  - Use repositories **only via IUnitOfWork**
  - Be transaction-aware where needed
- MUST NOT:
  - Expose EF entities
  - Reference DbContext directly
- SHOULD:
  - Be cohesive (single responsibility)
  - Be unit-testable without EF Core

### Unit of Work
- All data access MUST go through IUnitOfWork
- IUnitOfWork is the only allowed entry point to repositories
- DbContext lifetime MUST be scoped to UnitOfWork

### Repositories
- MUST:
  - Encapsulate EF queries
- MUST NOT:
  - Contain business logic
  - Call other repositories
- MAY:
  - Return IQueryable internally, but NEVER expose it upward

### DbContext
- MUST:
  - Exist only in the Infrastructure/Data layer
- MUST NOT:
  - Be injected into Controllers or Services

### DTOs & Mapping
- Controllers MUST use DTOs
- Services MUST NOT return EF entities
- Mapping logic MUST NOT live in Controllers

### Async & Error Handling
- Async must be end-to-end
- `.Result`, `.Wait()` are forbidden
- Exceptions must not be swallowed

---

## Frontend (React)

### Architecture
- Components → Facades/Services → API services
- Components MUST NOT:
  - Call HttpClient directly
  - Contain business logic

### Services
- API services handle HTTP only
- Facades handle state & orchestration

### General
- No logic in templates
- Observables preferred over manual subscriptions
- Unsubscribe handled via async pipe or lifecycle helpers

---

## Cross-Cutting Rules
- New business logic MUST include tests
- Public contracts MUST NOT change silently
- Prefer clarity over cleverness
- Explicit is better than implicit

---

## Review Expectations
The reviewer should:
- Flag rule violations explicitly
- Call out architectural drift
- Identify over-engineering or missing abstractions
- Comment like a senior developer mentoring a mid-level dev
