using Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.DTOs.PageDtos;
using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IPageService
{
    Task CreateThemePageAsync(Presentation presentation);
    Task CreatePlanPageAsync(string plan);
    Task CreateInformationPageAsync(string title);
    Task CreateInformationWithImagePageAsync(string title);
    Task CreateDescriptionForWordsPageAsync(string title);
    Task UpdateAsync(UpdatePageDto page);
    Task<PageDto> GetByIdAsync(int id);
    Task<List<PageDto>> GetAllAsync();
    Task DeleteAsync(int id);
}
