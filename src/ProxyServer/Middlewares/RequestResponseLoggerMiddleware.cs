public class RequestResponseLoggerMiddleware
{
    private readonly bool _isRequestResponseLoggingEnabled;
    private readonly RequestDelegate _next;

    public RequestResponseLoggerMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _isRequestResponseLoggingEnabled = config.GetValue("EnableRequestResponseLogging", true);
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Middleware is enabled only when the EnableRequestResponseLogging config value is set.
        if (_isRequestResponseLoggingEnabled)
        {
            Console.WriteLine($"HTTP request information:\n" +
                              $"\tMethod: {httpContext.Request.Method}\n" +
                              $"\tPath: {httpContext.Request.Path}\n" +
                              $"\tQueryString: {httpContext.Request.QueryString}\n" +
                              $"\tHeaders: {FormatHeaders(httpContext.Request.Headers)}\n" +
                              $"\tSchema: {httpContext.Request.Scheme}\n" +
                              $"\tHost: {httpContext.Request.Host}\n" +
                              //$"\tBody: {await ReadBodyFromRequest(httpContext.Request)}\n" +
                              ""
                              );

            // Temporarily replace the HttpResponseStream, which is a write-only stream, with a MemoryStream to capture it's value in-flight.
            var originalResponseBody = httpContext.Response.Body;
            await using var newResponseBody = new MemoryStream();
            httpContext.Response.Body = newResponseBody;

            // Call the next middleware in the pipeline
            await _next(httpContext);

            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

            Console.WriteLine($"HTTP request information:\n" +
                              $"\tStatusCode: {httpContext.Response.StatusCode}\n" +
                              $"\tContentType: {httpContext.Response.ContentType}\n" +
                              $"\tHeaders: {FormatHeaders(httpContext.Response.Headers)}\n" +
                              //$"\tBody: {responseBodyText}\n" +
                                ""
                              );

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);
        }
        else
        {
            await _next(httpContext);
        }
    }

    private static string FormatHeaders(IHeaderDictionary headers)
    {
        return string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value.ToString())}}}"));
    }

    private static async Task<string> ReadBodyFromRequest(HttpRequest request)
    {
        // Ensure the request's body can be read multiple times (for the next middleware\s in the pipeline).
        request.EnableBuffering();

        using var streamReader = new StreamReader(request.Body, leaveOpen: true);
        var requestBody = await streamReader.ReadToEndAsync();

        // Reset the request's body stream position for next middleware in the pipeline.
        request.Body.Position = 0;
        return requestBody;
    }
}