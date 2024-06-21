using Application.DTOs.PageDtos;
using FluentValidation;
using PresentationCreatorAPI.Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Enums;

namespace PresentationCreatorAPI.Application.Services;

public class PageService(IUnitOfWork unitOfWork,
                         IValidator<Page> validator) : IPageService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Page> _validator = validator;

    public Task CreateDescriptionForWordsPageAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task CreateInformationPageAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task CreateInformationWithImagePageAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task CreatePlanPageAsync(string plan)
    {
        throw new NotImplementedException();
    }

    public Task CreateThemePageAsync(string theme, string author)
    {
        var page = new Page
        {
            Title = theme,
            Text = author,
            PageType = PresentationPageType.Theme
        };
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
