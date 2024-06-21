using Application.DTOs.PageDtos;
using FluentValidation;
using PresentationCreatorAPI.Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.Services;

public class PageService(IUnitOfWork unitOfWork,
                         IValidator<Page> validator) : IPageService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Page> _validator = validator;

    public Task CreateAsync(AddPageDto page)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PageDto>> GetAllPagesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PageDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdatePageDto page)
    {
        throw new NotImplementedException();
    }
}
