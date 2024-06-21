using Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.DTOs.PresentationDtos;
using PresentationCreatorAPI.Application.presntations.Presentationpresntations;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IPresentationServise
{
    Task CreateAsync(AddPresentationDto dto);
    Task UpdateAsync(UpdatePresentationDto dto);
    Task<PageDto> GetByIdAsync(int id);
    Task<List<PresentationDto>> GetAllPagesAsync();
    Task DeleteAsync(int id);
}
