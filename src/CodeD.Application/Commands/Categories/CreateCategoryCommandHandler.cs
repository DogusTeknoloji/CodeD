using CodeD.Application.Abstractions.Messaging;
using CodeD.Domain.Abstractions;
using CodeD.Domain.Categories;
using CodeD.Domain.Shared;

namespace CodeD.Application.Commands.Categories;

public sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Guid?>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        using (_unitOfWork)
        {
            Key key = Key.Create(request.Key);
            var entity = await _categoryRepository.GetByKeyAsync(key);

            if (entity != null)
            {
                return Result.Failure<Guid?>(ApplicationErrors.CategoryFound(request.Key));
            }

            ExternalReference? externalReference = null;

            if (!string.IsNullOrWhiteSpace(request.SourceProviderKey) && !string.IsNullOrWhiteSpace(request.SourceItemId))
            {
                externalReference = ExternalReference.Create(request.SourceProviderKey, request.SourceItemId, request.SourceVersion);
            }

            entity = Category.Create(key,
                Title.Create(request.Title),
                WhoColumns.Empty(),
                externalReference);

            await _categoryRepository.AddAsync(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success<Guid?>(entity.Id.Value);
        }
    }
}