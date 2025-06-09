
public interface IAddOnRepository
{
    Task<IEnumerable<AddOnReadDto>> GetAllAsync();
    Task<AddOnReadDto> AddAsync(AddOnCreateDto dto);
    Task<AddOnReadDto> UpdateAsync(int id, AddOnCreateDto dto);

    Task<bool> DeleteAsync(int addOnId);

}
