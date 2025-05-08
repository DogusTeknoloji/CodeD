using CodeD.Application.Abstractions.Messaging;
using CodeD.Domain.Abstractions;
using CodeD.Domain.Categories;
using CodeD.Domain.Shared;

namespace CodeD.Application.Commands.Categories;

public sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Category?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Category?>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        using (_unitOfWork)
        {
            Category? entity = null;
            if (request.Id is not null)
            {
                var id = CategoryId.Create(request.Id.Value);
                entity = await _categoryRepository.GetByIdAsync(id);
                if (entity is null)
                {
                    return Result.Failure<Category?>(ApplicationErrors.CategoryNotFound(id.Value));
                }
            }
            else if (request.Key is not null)
            {
                var key = Key.Create(request.Key);
                entity = await _categoryRepository.GetByKeyAsync(key);
                if (entity is null)
                {
                    return Result.Failure<Category?>(ApplicationErrors.CategoryNotFound(key.Value));
                }
            }

            ExternalReference? externalReference = null;

            if (!string.IsNullOrWhiteSpace(request.SourceProviderKey) && !string.IsNullOrWhiteSpace(request.SourceItemId))
            {
                externalReference = ExternalReference.Create(request.SourceProviderKey, request.SourceItemId, request.SourceVersion);
            }

            entity!.Update(Title.Create(request.Title), externalReference);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success<Category?>(entity);
        }
    }
}