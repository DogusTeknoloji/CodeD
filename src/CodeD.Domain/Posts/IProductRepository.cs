using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Posts;

public interface IPostRepository : IEntityRepository<Post, PostId>;