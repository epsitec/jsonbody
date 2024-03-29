# JsonBody

Compile and run the project (using .NET Core 3.0 preview 8). The simplest way is to
start Visual Studio Code in the project folder, then press F5.

Following routes are handled:

- `GET` &rarr; `http://localhost:5000/test/hello`.  
  This should return `Hello, world.`.
- `POST` &rarr; `http://localhost:5000/test/demo`.  
  This should accept a JSON payload in the body of the `POST` message and display
  the formatted JSON in the console.

## POST with `Content-Type` header

Using `curl` to post an empty JSON payload works only if the `Content-Type` HTTP header
is properly set to `application/json`:

```cmd
curl --data "{}" --header "Content-Type: application/json" -X POST http://localhost:5000/test/demo
```

## POST without `Content-Type` header

Without the `Content-Type` header, this would not work:

```cmd
curl --data "{}" --header "Content-Type:" -X POST http://localhost:5000/test/demo
```

By default, AspNetCore returns this error:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.13",
  "title": "Unsupported Media Type",
  "status": 415,
  "traceId": "|c74b9116-4d2a19eef496c2f3."
}
```

## Injecting `Content-Type` with a middleware

It is straightforward with ASP.NET Core to add a dedicated middleware:

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  // ...
  app.UseMiddleware<AddDefaultContentTypeMiddleware> ();
  // ...
}
```

The middleware itself is implemented in `Middlewares/AddDefaultContentTypeMiddleware.cs`.

Basically, all you have to do is implement a constructor
and a method named `InvokeAsync()`:

```cs
public async Task InvokeAsync(HttpContext context)
{
    if ((context.Request.Method == "POST") &&
        (context.Request.Headers.ContainsKey ("Content-Type") == false))
    {
        //  No Content-Type found for this request...
        context.Request.Headers.Add ("Content-Type", new Microsoft.Extensions.Primitives.StringValues ("application/json"));
    }

    await this.next (context);
}
```

## Injecting `Content-Type` with a resource filter

Another solution to inject a default content type is the resource filter;
it can be provided as an implemention of `IAsyncResourceFilter` in an
_attribute class_, which can then be used to decorate the action methods
where the specific behavior is required:

```cs
[AttributeUsage (AttributeTargets.Method | AttributeTargets.Class)]
public class AddDefaultContentTypeAttribute : System.Attribute, IAsyncResourceFilter
{
  public AddDefaultContentTypeAttribute(string contentType) { ... }
  public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next) { ... }
}
```

The action method (or the whole controller class) can then be decorated
with an `[AddDefaultContentType]` attribute:

```cs
[HttpPost]
[AddDefaultContentType ("application/json")]
public IActionResult Demo([FromBody] JObject x)
{
  // ...
}
```

This provides more flexibility than the middleware and does not impact every
request coming in through the pipeline, as would be the case with the first
solution (middleware).
