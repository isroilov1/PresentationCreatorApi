using Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.DTOs.PageDtos;
using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IPageService
{
    Task CreateThemePageAsync(Presentation presentation);
    Task CreatePlanPageAsync(Presentation presentation);
    Task CreateInformationPageAsync(Presentation presentation, string title);
    Task CreateInformationWithImagePageAsync(Presentation presentation, string title);
    Task CreateDescriptionForWordsPageAsync(Presentation presentation, string title);
    Task UpdateAsync(UpdatePageDto page);
    Task<PageDto> GetByIdAsync(int id);
    Task<List<PageDto>> GetAllAsync();
    Task DeleteAsync(int id);
}
