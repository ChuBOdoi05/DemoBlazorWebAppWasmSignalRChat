using DemoBlazorWebAppWasmSignalRChat.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace DemoBlazorWebAppWasmSignalRChat.ChatHubs

{
    public class ChatAppContext : DbContext
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options) { }
    }
}
