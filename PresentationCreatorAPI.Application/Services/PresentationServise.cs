using Application.DTOs.PageDtos;
using FluentValidation;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Application.Common.Validators;
using PresentationCreatorAPI.Application.DTOs.PresentationDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.presntations.Presentationpresntations;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Entites;
using System.Net;

namespace PresentationCreatorAPI.Application.Services;

public class PresentationServise(IUnitOfWork unitOfWork,
                                 IValidator<Presentation> validator) : IPresentationServise
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Presentation> _validator = validator;

    public async Task CreateAsync(AddPresentationDto dto, int userId)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.GetErrorMessages());

        var presentation = (Presentation)dto;
        string rootPath = $"uploads/presentations/{userId}";
        string filePath = FileHelper.SaveFile(dto.File, rootPath);
        presentation.FilePath = filePath;

        var user = await _unitOfWork.User.GetByIdIncludeAsync(userId);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi!");
        presentation.UserId = userId;
        presentation.User = user;
        PresentationFileCreator.CreatePresentation(presentation);
        await _unitOfWork.Presentation.CreateAsync(presentation);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PresentationDto>> GetAllPagesAsync()
    {
        throw new NotImplementedException();
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
