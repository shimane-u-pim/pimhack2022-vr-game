using LazyHttpServerLib;

string endpoint = "http://192.168.10.103/messages";

HttpClient client = new HttpClient();
HttpServer server = new(new string[]{ "http://localhost:15080/" });



server.IncomingHttpRequest += async (sender, e) =>
{
    var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
    request.Headers.Add("X-Requested-With", "local");
    switch (e.Request.RawUrl)
    {
        case "/on":
            request.Content = new StringContent("{}", System.Text.Encoding.UTF8, @"application/json");
            break;

        case "/off":
            request.Content = new StringContent("{}", System.Text.Encoding.UTF8, @"application/json");
            break;

        default:
            Console.WriteLine("Invalid Request");
            return;
    }

    await client.SendAsync(request);

    Console.WriteLine("Request Handled");
    e.Response.Close();
};

server.Start();

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();