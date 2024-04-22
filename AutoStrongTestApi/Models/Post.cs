namespace AutoStrongTestApi.Models
{
    public class Post : BaseModel
    {
        public string? Text { get; set; }

        public byte[]? Image { get; set; }
    }
}
