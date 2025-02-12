using CodeD.Application.Abstractions.Messaging;
using CodeD.Domain.Abstractions;
using CodeD.Domain.Categories;
using CodeD.Domain.Shared;

namespace CodeD.Application.Commands.Categories;

public sealed class CreateCategoryCommand : ICommand<Guid>
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public string SourceProviderKey { get; set; } = string.Empty;

    public string SourceItemId { get; set; } = string.Empty;

    public string? SourceVersion { get; set; } = null;
}

public sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        using (_unitOfWork)
        {
            Key key = Key.Create(request.Key);
            var entity = await _categoryRepository.GetByKeyAsync(key);

            if (entity != null)
            {
                return Result.Failure<Guid>(ApplicationErrors.CategoryNotFound(request.Key));
            }

            entity = Category.Create(key,
                Title.Create(request.Title),
                WhoColumns.Create(DateTimeOffset.Now, Guid.Empty, DateTimeOffset.Now, Guid.Empty),
                ExternalReference.Create(request.SourceProviderKey, request.SourceItemId, request.SourceVersion));

            await _categoryRepository.AddAsync(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(entity.Id.Value);
        }
    }
}