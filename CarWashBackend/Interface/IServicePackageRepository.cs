
public interface IServicePackageRepository
{
    Task<IEnumerable<ServicePackage>> GetAllPackagesAsync();
    Task<ServicePackage> AddPackageAsync(ServicePackage package);
    Task<ServicePackage> UpdatePackageAsync(int packageId, ServicePackage package);
    Task<bool> DeactivatePackageAsync(int packageId);
    Task<bool> DeletePackageAsync(int packageId);

}
