namespace AutoStrongTestApi.Dto
{
    public record UpdatePostDto : PostDto
    {
        public Guid Id { get; set; }
    }
}
