using Application.DTOs.PageDtos;
using FluentValidation;
using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;

namespace PresentationCreatorAPI.Application.Services;

public class PageService(IUnitOfWork unitOfWork,
                         IValidator<Page> validator) : IPageService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Page> _validator = validator;

    public Task CreateDescriptionForWordsPageAsync(Presentation presentation, string title)
    {
        throw new NotImplementedException();
    }

    public async Task CreateInformationPageAsync(Presentation presentation, byte pageNumber)
    {
        byte titleIndex = (byte)(pageNumber - 2);
        var page = new Page
        {
            PageNumber = pageNumber,
            Title = presentation.Titles[titleIndex],
            Text = await PresentationHelper.GetInformationAsync(presentation.Language, presentation.Titles[titleIndex]),
            PageType = PresentationPageType.Information,
            PresentationId = presentation.Id
        };
        await _unitOfWork.Page.CreateAsync(page);
    }

    public async Task CreateInformationWithImagePageAsync(Presentation presentation, byte pageNumber)
    {
        byte titleIndex = (byte)(pageNumber - 2);
        byte imageIndex = 0;
        if (pageNumber <= 5)
            imageIndex = 0;
        else if (pageNumber <= 8)
            imageIndex = 1;
        else if (pageNumber <= 11)
            imageIndex = 2;
        else if (pageNumber <= 14)
            imageIndex = 3;
        else if (pageNumber <= 17)
            imageIndex = 4;
        else if (pageNumber <= 20)
            imageIndex = 5;
        var page = new Page
        {
            PageNumber = pageNumber,
            Title = presentation.Titles[titleIndex],
            Text = await PresentationHelper.GetInformationAsync(presentation.Language, presentation.Titles[titleIndex]),
            PageType = PresentationPageType.InformationWithImage,
            ImagesPath = presentation.ImagesPaths[imageIndex],
            PresentationId = presentation.Id
        };
        await _unitOfWork.Page.CreateAsync(page);
    }

    public async Task CreatePlanPageAsync(Presentation presentation)
    {
        var plan = PresentationHelper.GetPlanWithLang(presentation.Language);
        var plans = await PresentationHelper.GetTitlesAsync(presentation.Language, presentation.Theme);
        var firstThreeElements = plans.Take(3);
        //var plans = "AAAAA";
        var page = new Page
        {
            PageNumber = 2,
            Title = plan,
            Text = string.Join("\n", firstThreeElements),
            PageType = PresentationPageType.Plan,
            PresentationId = presentation.Id
        };
        await _unitOfWork.Page.CreateAsync(page);
    }

    public async Task CreateThemePageAsync(Presentation presentation)
    {
        var page = new Page
        {
            PageNumber = 1,
            Title = presentation.Theme,
            Text = presentation.Author,
            PageType = PresentationPageType.Theme,
            PresentationId = presentation.Id
        };
        await _unitOfWork.Page.CreateAsync(page);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PageDto>> GetAllAsync()
    {
        var payments = await _unitOfWork.Page.GetAllAsync();
        return payments.Select(x => (PageDto)x).ToList();
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
