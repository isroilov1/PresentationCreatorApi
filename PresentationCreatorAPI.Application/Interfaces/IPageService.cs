using Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.DTOs.PageDtos;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IPageService
{
    Task CreateThemePageAsync(Presentation presentation);
    Task CreatePlanPageAsync(Presentation presentation);
    Task CreateInformationPageAsync(Presentation presentation, byte pageNumber);
    Task CreateInformationWithImagePageAsync(Presentation presentation, byte pageNumber);
    Task CreateDescriptionForWordsPageAsync(Presentation presentation, string title);
    Task UpdateAsync(UpdatePageDto page);
    Task<PageDto> GetByIdAsync(int id);
    Task<List<PageDto>> GetAllAsync();
    Task DeleteAsync(int id);
}
