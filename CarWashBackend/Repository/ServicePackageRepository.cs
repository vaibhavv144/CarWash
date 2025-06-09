using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ServicePackageRepository : IServicePackageRepository
{
    private readonly CarWashContext _context;

    public ServicePackageRepository(CarWashContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServicePackage>> GetAllPackagesAsync()
    {
        return await _context.ServicePackages.ToListAsync();
    }

    public async Task<ServicePackage> AddPackageAsync(ServicePackage package)
    {
        _context.ServicePackages.Add(package);
        await _context.SaveChangesAsync();
        return package;
    }

    public async Task<ServicePackage> UpdatePackageAsync(int packageId, ServicePackage package)
    {
        var existingPackage = await _context.ServicePackages.FindAsync(packageId);
        if (existingPackage == null) return null;

        existingPackage.Name = package.Name;
        existingPackage.Description = package.Description;
        existingPackage.Price = package.Price;
        existingPackage.IsActive = package.IsActive;

        await _context.SaveChangesAsync();
        return existingPackage;
    }

    public async Task<bool> DeletePackageAsync(int packageId)
{
    var package = await _context.ServicePackages.FindAsync(packageId);
    if (package == null) return false;

    _context.ServicePackages.Remove(package);
    await _context.SaveChangesAsync();
    return true;
}


    public async Task<bool> DeactivatePackageAsync(int packageId)
    {
        var package = await _context.ServicePackages.FindAsync(packageId);
        if (package == null) return false;

        package.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}
