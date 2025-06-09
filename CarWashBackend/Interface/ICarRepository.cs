
public interface ICarRepository
{
    Task AddCarAsync(CarAddDTO carDto, string userId);
    List<Car> GetCars(string userId);
    bool DeleteCar(string userId, int carId);
    bool EditDetails(string userId, CarAddDTO carDto, int id);
}
