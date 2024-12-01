using DemoBlazorWebAppWasmSignalRChat.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorWebAppWasmSignalRChat.ChatHubs
{
    public class ChatHub : Hub
    {
        private readonly ChatAppContext _context;

        public ChatHub(ChatAppContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string userName, string Message, DateTime date)
        {
            Console.WriteLine($"SendMessage called with: {userName} - {Message} - {date}");
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(Message))
            {
                Console.WriteLine("Error: Empty username or message.");
                throw new HubException("Tên người dùng và tin nhắn không được để trống.");
            }

            var chatMessage = new ChatMessage
            {
                userName = userName,
                Message = Message,
                Date = date,
            };

            try
            {
                _context.ChatMessages.Add(chatMessage);
                await _context.SaveChangesAsync(); // Lưu vào database
                Console.WriteLine("Message saved to database.");

                await Clients.All.SendAsync("ReceiveMessage", userName, Message, date);
                Console.WriteLine("Message sent to all clients.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                throw new HubException("Không thể gửi tin nhắn. Vui lòng thử lại.");
            }
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            try
            {
                var messages = await _context.ChatMessages
                    .OrderByDescending(m => m.Date)
                    .Take(50)
                    .ToListAsync();

                await Clients.Caller.SendAsync("LoadMessages", messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading messages: {ex.Message}");
                throw new HubException("Cannot load messages. Please try again.");
            }
        }

    }
}
