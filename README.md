### Project Description
Worker platform is designed to gather moroccan worker community with a smart way to build their portfolio/ rating, anothre user role is a customer which launch an offer for s certain task type with a price, then the worker can apply if they are available
and the customer then choose based on responder profile and pricing. the worker platform tends also to control pricing and project progress to protect customers.

### Project architrecure
Api layer: contains api controllers, authentication/authorization, http filters/middlewares.

Application layer: contains applicaiton logic encapsulated into modular services, this layer is origanized into folders by domain.

Domain layer: contains all entities that defines the domain of the app.

Infrastructure layer: contains database repositories, cache repositories, email... and potentially access data classes.


### Coding practices
- Keep services modular (Single responsibility pattern), if a route/background job have to access multiple services, let that class use many modular services (dont add complexity having services depends on services that they dont need to),
- UnitOfWork pattern, use unitofwork class to manage transaction/ state commit/rollback (Atomic).
- Use Dependency Injection whenerver it's possible.
- Define repository interfaces in application layer and implement in infrasturcture layer (Dependency inversion)
- Use constants and avoid magic values
- Code First approach for changing database model (EF migration)
- Use Fluent validation for all api routes (they will be excuted globally and return 400 if model is not valid) then we have specific validation classes that needs deeper logic.
- Use Exception for each business case, that inherit from some base exception ( that we use in exception handler middlewaer to return correct http status)
- use Logger in data alter routes, and logs responsible user.
- In application services, Try to have the lowest complexity/excecution time either using cache patterns, or peformant algorithmics.
- About Nuget packages, the priority is to use microsoft official packages if they dont exist for our case then go for active community package.
