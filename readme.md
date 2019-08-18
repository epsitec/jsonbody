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

Without the `Content-Type` header, this does not work:

```cmd
curl --data "{}" --header "Content-Type:" -X POST http://localhost:5000/test/demo
```

AspNetCore returns this instead of `200 OK`:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.13",
  "title": "Unsupported Media Type",
  "status": 415,
  "traceId": "|c74b9116-4d2a19eef496c2f3."
}
```
