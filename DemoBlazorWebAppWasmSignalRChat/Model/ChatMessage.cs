using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DemoBlazorWebAppWasmSignalRChat.Model
{
    [Table("ChatMessages")]
    public partial class ChatMessage
    {
        [Key]
        public int id { get; set; }
        public required string userName { get; set; } = string.Empty;
        public required string Message { get; set; } = string.Empty ;
        public DateTime Date { get; set; }
    }

}
