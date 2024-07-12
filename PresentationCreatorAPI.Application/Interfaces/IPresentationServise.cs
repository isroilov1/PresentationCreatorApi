using PresentationCreatorAPI.Application.DTOs.PresentationDtos;

namespace PresentationCreatorAPI.Application.Interfaces;
public interface IPresentationServise
{
    Task CreateAsync(AddPresentationDto dto, int userId);
    Task UpdateAsync(UpdatePresentationDto dto);
    Task<PresentationDto> GetByIdAsync(int id);
    Task<List<PresentationDto>> GetAllAsync();
    Task<List<PresentationDto>> GetByUserAsync(int userId);
    Task DeleteAsync(int id);
}
