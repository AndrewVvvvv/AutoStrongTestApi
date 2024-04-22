namespace AutoStrongTestApi.Dto
{
    public record PostDto
    {
        public required string Text { get; set; }

        public required byte[] Image { get; set; }
    }
}
