using FluentValidation;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Application.Common.Validators;
using PresentationCreatorAPI.Application.DTOs.PresentationDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using System.Net;
using Presentation = PresentationCreatorAPI.Domain.Entites.Presentation;

namespace PresentationCreatorAPI.Application.Services;

public class PresentationServise(IUnitOfWork unitOfWork,
                                 IPageService pageService,
                                 IValidator<Presentation> validator) : IPresentationServise
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPageService _pageService = pageService;
    private readonly IValidator<Presentation> _validator = validator;

    public async Task CreateAsync(AddPresentationDto dto, int userId)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.GetErrorMessages());

        var presentation = (Presentation)dto;
        string rootPath = $"uploads/presentations/{userId}";
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);
        presentation.FilePath = FileHelper.PresentationFilePathCreator(rootPath);

        var user = await _unitOfWork.User.GetByIdAsync(userId);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi!");
        presentation.UserId = userId;
        var titles = await PresentationHelper.GetTitlesAsync(dto.Language, dto.Theme);
        presentation.Titles = titles;

        // Images uploads
        byte count = 0;
        var pageCount = presentation.PageCount - 2;
        while (pageCount > 0)
        {
            pageCount = pageCount - 3;
            count++;
        }
        var images = await FileHelper.UploadImagesForTheme(presentation.Theme, count);
        presentation.ImagesPaths = images;
        await _unitOfWork.Presentation.CreateAsync(presentation);

        // Pagelar yaratilishi kerak
        pageCount = presentation.PageCount;
        byte pageNumber = 1;
        await _pageService.CreateThemePageAsync(presentation);
        pageNumber++;
        await _pageService.CreatePlanPageAsync(presentation);
        pageNumber++;
        await _pageService.CreateInformationPageAsync(presentation, pageNumber);
        pageNumber++;
        await _pageService.CreateInformationPageAsync(presentation, pageNumber);
        pageNumber++;
        await _pageService.CreateInformationWithImagePageAsync(presentation, pageNumber);
        while (presentation.PageCount > pageNumber)
        {
            pageNumber++;
            await _pageService.CreateInformationPageAsync(presentation, pageNumber);
            if (presentation.PageCount <= pageNumber)
                break;
            pageNumber++;
            await _pageService.CreateInformationPageAsync(presentation, pageNumber);
            if (presentation.PageCount <= pageNumber)
                break;
            pageNumber++;
            await _pageService.CreateInformationWithImagePageAsync(presentation, pageNumber);
        }


        // Taqdimot fileni yaratish
        var filePath = await PresentationFileCreator.CreatePresentation(presentation);

        //static void RunScript(string script)


        presentation.FilePath = filePath.Replace("wwwroot/", "");
        await _unitOfWork.Presentation.UpdateAsync(presentation);

        //User update qilish
        user.PresentationCount += 1;
        if (user.Presentations == null)
            user.Presentations = new List<Presentation>();
        user.Presentations.Add(presentation);
        await _unitOfWork.User.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var presentation = await _unitOfWork.Presentation.GetByIdAsync(id);
        if (presentation == null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Taqdimot topilmadi.");
        var imagesPaths = presentation.ImagesPaths;
        foreach (var imagePath in imagesPaths)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        await _unitOfWork.Presentation.DeleteAsync(presentation);
    }

    public async Task<List<PresentationDto>> GetAllAsync()
    {
        var presentations = await _unitOfWork.Presentation.GetAllIncludeAsync();
        if (presentations is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Taqdimotlar topilmadi!");

        return presentations.Select(u => (PresentationDto)u).Reverse().ToList();

    }
    public async Task<PresentationDto> GetByIdAsync(int id)
    {
        var presentation = await _unitOfWork.Presentation.GetByIdAsync(id);
        if (presentation == null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Taqdimot topilmadi!");

        return (PresentationDto)presentation;
    }

    public async Task<List<PresentationDto>> GetByUserAsync(int userId)
    {
        var user = await _unitOfWork.User.GetByIdIncludeAsync(userId);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        var presentations = user.Presentations;
        if (presentations is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Ushbu foydalanuvchining taqdimotlari mavjud emas!");
        return presentations.Select(x => (PresentationDto)x).ToList();
    }

    public async Task UpdateAsync(UpdatePresentationDto dto)
    {
        var presentation = await _unitOfWork.Presentation.GetByIdAsync(dto.Id);
        if (presentation == null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Taqdimot topilmadi!");
        
    }
}
