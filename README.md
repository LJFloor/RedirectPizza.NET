# RedirectPizza.NET
A way to interact with [redirect.pizza's api](https://redirect.pizza/api) in c sharp.

# Usage
```csharp
const apiToken = "XXXXXX";  // You can find this token on https://redirect.pizza/api
var pizza = new RedirectPizza.NET.RedirectPizza(apiToken);
```

# Metadata
The `ListRedirects()` and `ListEmailForwards()` methods return two things: The `Data` and `Meta` information.
The meta property contains things like the current page, the next page number and the
amount of pages. This makes it easy to add pages to your application.
```csharp
var redirects = pizza.Redirects.ListRedirects();

Console.WriteLine($"Current page: {redirects.Meta.CurrentPage}");
```

# Examples
Each property, method and field is documented with `<summary></summary>` tags. Your
IDE should automatically add intellisense to everything.

## Redirects
### List all redirects
```csharp
var redirects = pizza.Redirects.ListRedirects();
// or asynchronously 
var redirects = await pizza.Redirects.ListRedirectsAsync();
```

### List all redirects with pagination
```csharp
// Get the first 25 redirects.
var redirects = pizza.Redirects.ListRedirects(1, 25);
```

### Creating and updating redirects.
For creating and updating redirects you have to use separate models. This is because
the redirect.pizza api works with different models too.

```csharp
var redirect = pizza.Redirects.CreateRedirect(new CreateRedirect()
{
    Sources = new[] { "old.example.com", "testing.example.com" },
    Destination = "https://new.example.com",
    Tracking = true,
    KeepQueryString = false,
    UriForwarding = false,
    Tags = new[] { "NewDomain" }
});
```

You can update a redirect by making the modifications you want to the model itself.
Then you confirm the changes by running `redirect.Update()`:
```csharp
redirect.Tracking = false;
redirect.Update();
```

Alternatively, you can specify a set of changes to the `UpdateRedirect()` or 
`UpdateRedirectAsync()` methods:
```csharp
pizza.Redirects.UpdateRedirect(123456789, new UpdateRedirect() {
    Tracking = false,
});
```

### LINQ
RedirectPizza.NET supports interacting with endpoints using a previously returned
model. This allows you to create LINQ queries.
```csharp
pizza.Redirects.ListRedirects().Data
    .Where(r => r.Tags.Contains("Marketing"))
    .ToList()
    .ForEach(r => 
    {
        r.Tags.Add("Sales");
        r.Update();  
    });
```

Or you maybe you want to change all redirect types:
```csharp
pizza.Redirects.ListRedirects().Data
    .Where(r => r.RedirectType != RedirectType.Permantent)
    .ToList()
    .ForEach(r => 
    {
        r.RedirectType = RedirectType.Permanent;
        r.Update();  
    });
```

Or delete all marketing redirects:
```csharp
pizza.Redirects.ListRedirects().Data
    .Where(r => r.Tags.Contains("Marketing"))
    .ToList()
    .ForEach(r => r.Delete());
```

Or perhaps you gave up on the whole tags thing:
```csharp
pizza.Redirects.ListRedirects().Data
    .Where(r => r.Tags.Any())
    .ToList()
    .ForEach(r =>
    {
        r.Tags.Clear();
        r.Update();
    });
```

Or simply delete a redirect by its id:
```csharp
pizza.Redirects.DeleteRedirect(123456789);
```