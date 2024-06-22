using Application.DTOs.PageDtos;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using FluentValidation;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Application.Common.Validators;
using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.DTOs.PresentationDtos;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.presntations.Presentationpresntations;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Enums;
using System.Net;
using Presentation = PresentationCreatorAPI.Entites.Presentation;

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
        presentation.FilePath = FileHelper.PresentationFilePathCreator(rootPath);

        var user = await _unitOfWork.User.GetByIdAsync(userId);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi!");
        presentation.UserId = userId;
        await _unitOfWork.Presentation.CreateAsync(presentation);

        // Pagelar yaratilishi kerak
        await _pageService.CreateThemePageAsync(presentation);
        PresentationFileCreator.CreatePresentation(presentation);

        await _unitOfWork.Presentation.UpdateAsync(presentation);
        //User update qilish
        user.PresentationCount += 1;
        if (user.Presentations == null)
            user.Presentations = new List<Presentation>();
        user.Presentations.Add(presentation);
        await _unitOfWork.User.UpdateAsync(user);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PresentationDto>> GetAllAsync()
{
    var presentations = await _unitOfWork.Presentation.GetAllIncludeAsync();
    if (presentations is null)
        throw new StatusCodeException(HttpStatusCode.NotFound, "Taqdimotlar topilmadi!");
    
    return presentations.Select(u => (PresentationDto)u).ToList();

}
    public Task<PageDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdatePresentationDto dto)
    {
        throw new NotImplementedException();
    }
}
