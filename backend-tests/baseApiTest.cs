using System;

public class BaseApiTest
{
    protected readonly HttpClient _client;

    public BaseApiTest()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
    }
}