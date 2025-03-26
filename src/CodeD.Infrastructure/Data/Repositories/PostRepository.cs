using CodeD.Domain.Posts;

namespace CodeD.Infrastructure.Data.Repositories;

public class PostRepository(CodeDDbContext _codeDDbContext)
    : EntityRepository<Post, PostId>(_codeDDbContext), IPostRepository;