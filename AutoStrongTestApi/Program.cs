using AutoMapper;
using AutoStrongTestApi;
using AutoStrongTestApi.Dto;
using AutoStrongTestApi.Extensions;
using AutoStrongTestApi.Interfaces;
using AutoStrongTestApi.Models;

var builder = WebApplication.CreateBuilder(args).InitServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Constants.PolicyName);

app.MapGet("/post/{id}", (Guid id, IPostService postService) =>
{
    var post = postService.GetPost(id);
    return post == null ? Results.NotFound() : Results.Ok(post);
})
.WithName("GetPost")
.WithOpenApi();

app.MapGet("/posts", async(IPostService postService) =>
{
    var posts = await postService.GetPosts();
    return Results.Ok(posts);
})
.WithName("GetPosts")
.WithOpenApi()
.DisableAntiforgery();

app.MapPost("/post", async (PostDto postDto, IPostService postService, IMapper mapper, IConfiguration configuration) =>
{
    var maxFileSize = configuration.GetValue<int>(Constants.MaxFileSize);
    if (postDto.Image.Length > maxFileSize)
    {
        Results.BadRequest("Max file size is 5 MB");
    }

    var post = mapper.Map<Post>(postDto);
    var createdPost = await postService.CreatePostAsync(post);
    return Results.Created($"/post/{createdPost.Id}", createdPost);
})
.WithName("CreatePost")
.WithOpenApi()
.DisableAntiforgery();

app.MapPut("/post", async (UpdatePostDto updatePostDto, IPostService postService, IMapper mapper, IConfiguration configuration) =>
{
    var maxFileSize = configuration.GetValue<int>(Constants.MaxFileSize);
    if (updatePostDto.Image.Length > maxFileSize)
    {
        Results.BadRequest("Max file size is 5 MB");
    }

    var post = mapper.Map<Post>(updatePostDto);
    var updatedPost = await postService.UpdatePostAsync(post);
    return updatedPost == null ? Results.BadRequest() : Results.Ok(updatedPost);
})
.WithName("UpdatePost")
.WithOpenApi()
.DisableAntiforgery();


app.MapDelete("/post", (Guid id, IPostService postService) =>
{
    postService.DeletePost(id);
    return Results.Ok();
})
.WithName("DeletePost")
.WithOpenApi()
.DisableAntiforgery();

app.Run();