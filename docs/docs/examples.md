# Examples

## Railway oriented programming
With the help of `Result<T, E>` type failing operations can be chained together
and all errors can be handled in a structured and consice manner:

```c#

FetchDb("John")
    .SelectMany(user => GetPlan(user))
    .Switch(
        ok: (Plan plan) => Console.WriteLine($"The cost is {plan.Cost}."),
        error: (string msg) => Console.WriteLine(msg)
    );

record User(string Name, int Age);
record Plan(int Cost);

Result<User, string> FetchDb(string name)
{
    // ...
    if (!found)
    {
        return Error($"User {name} not found.");
    }

    return Ok(user);
}

Result<Plan, string> FetchPlan(User user)
{
    // ...
    if (!isASubscriber)
    {
        return Error($"User {name} doesn't have a registered plan.");
    }

    return Ok(plan);
}
```