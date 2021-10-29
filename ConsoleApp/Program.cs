using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleApp
{
    public class MessageDto
    {
        [JsonPropertyName("from")]
        public string From { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
    
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Кто");
            var from = Console.ReadLine();
            Console.WriteLine("Кому");
            var to = Console.ReadLine();
            
            var hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://localhost:5000/chat?username={from}")
                .Build();

            hubConnection.On("message", (MessageDto data) =>
            {
                var unescapedData = JsonSerializer.Deserialize<MessageDto>(
                    Regex.Unescape(JsonSerializer.Serialize(data))
                );
                
                Console.WriteLine($"{unescapedData.From}: {unescapedData.Message}");
            });

            await hubConnection.StartAsync();

            while (true)
            {
                var message = Console.ReadLine();
                await hubConnection.InvokeAsync("Send", to, from, message);
            }
        }
    }
}