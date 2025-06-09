


using Microsoft.EntityFrameworkCore;


public class AddOnRepository : IAddOnRepository
{
    private readonly CarWashContext _context;

    public AddOnRepository(CarWashContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AddOnReadDto>> GetAllAsync()
    {
        return await _context.AddOns
            .Select(a => new AddOnReadDto
            {
                Id = a.Id,
                Name = a.Name,
                Price = a.Price,
                IsActive = a.IsActive
            })
            .ToListAsync();
    }

    public async Task<AddOnReadDto> AddAsync(AddOnCreateDto dto)
    {
        var addOn = new AddOn
        {
            Name = dto.Name,
            Price = dto.Price,
            IsActive = dto.IsActive
        };

        _context.AddOns.Add(addOn);
        await _context.SaveChangesAsync();

        return new AddOnReadDto
        {
            Id = addOn.Id,
            Name = addOn.Name,
            Price = addOn.Price,
            IsActive = addOn.IsActive
        };
    }

    public async Task<AddOnReadDto> UpdateAsync(int id, AddOnCreateDto dto)
    {
        var existing = await _context.AddOns.FindAsync(id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.Price = dto.Price;
        existing.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return new AddOnReadDto
        {
            Id = existing.Id,
            Name = existing.Name,
            Price = existing.Price,
            IsActive = existing.IsActive
        };
    }

    public async Task<bool> DeleteAsync(int addOnId)
{
    var addOn = await _context.AddOns.FindAsync(addOnId);
    if (addOn == null) return false;

    _context.AddOns.Remove(addOn);
    await _context.SaveChangesAsync();
    return true;
}

}
