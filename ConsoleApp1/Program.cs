namespace ConsoleApp1;

public static class Program
{
    private static readonly HttpClient Client = new();
    private static String _url = "https://nx-assessment.azurewebsites.net/api/test";
    
    // to exit the program type in: /exit
    public static void Main(){
        Run().Wait();
    }

    static async Task Run()
    {
        Console.WriteLine("Enter a single Word:");
        while (Console.ReadLine() is { } line)
        {
            if (line == "/exit")
            {
                Environment.Exit(1);
            }
            
            var arr = line.ToCharArray();
            var output = String.Join(" ", arr.ToList());
        
            Console.WriteLine(output);
            output = ConvertToBase64(output);
            var response = await Post(output);

            Console.WriteLine("Response: "+response);
        }
    }

    private static async Task<String> Post(string output)
    {
        var tmp = "{'input':'%content%'}".Replace("%content%", output);
        var content = new StringContent(tmp);

        var response = await Client.PostAsync(_url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        return responseString;
    }

    private static string ConvertToBase64(String text)
    {
        var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(textBytes);
    }
}