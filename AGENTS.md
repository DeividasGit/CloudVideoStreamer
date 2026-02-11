## Review guidelines

- Skipping layers is forbidden: Controllers → Services → Repositories → DbContext
- Controllers MUST NOT reference DbContext, EF Core entities, Repositories, UnitOfWork.
- Repositories MUST Call Services only, Contain no business logic, Handle HTTP concerns only (status codes, validation responses).
- Services MUST Contain business logic, Use repositories **only via IUnitOfWork**, Be transaction-aware where needed.
- Services MUST NOT Expose EF entities, Reference DbContext directly.
- Services SHOULD Be cohesive (single responsibility), Be unit-testable without EF Core.
- Unit of Work - All data access MUST go through IUnitOfWork.
- IUnitOfWork is the only allowed entry point to repositories.
- DbContext lifetime MUST be scoped to UnitOfWork.
- Repositories MUST Encapsulate EF queries.
- Repositories MUST NOT Contain business logic, Call other repositories.
- Repositories MAY Return IQueryable internally, but NEVER expose it upward.
