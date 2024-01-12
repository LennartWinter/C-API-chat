namespace VelocityCs.Models;

public class Message
{
    public int id { get; set; }
    public int user_id { get; set; }
    public string message { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public string? attachment_url { get; set; }
    public int? reply_id { get; set; }
    public string username { get; set; }
    public string? avatar_url { get; set; }
}