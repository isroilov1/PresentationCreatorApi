using Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.DTOs.PageDtos;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IPresentationServise
{
    Task CreateAsync(AddPageDto page);
    Task UpdateAsync(UpdatePageDto page);
    Task<PageDto> GetByIdAsync(int id);
    Task<List<PageDto>> GetAllPagesAsync();
    Task DeleteAsync(int id);
}
