
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CarRepository : ICarRepository
{
    private readonly CarWashContext _context;

    public CarRepository(CarWashContext context)
    {
        _context = context;
    }

    public async Task AddCarAsync(CarAddDTO carDto, string userId)
    {
        var car = new Car
        {
            Make = carDto.Make,
            Model = carDto.Model,
            Year = carDto.Year,
            UserId = userId
        };

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }

    public List<Car> GetCars(string userId)
    {
        return _context.Cars.Where(c => c.UserId == userId).ToList();
    }

    public bool DeleteCar(string userId, int carId)
    {
        var car = _context.Cars.FirstOrDefault(c => c.UserId == userId && c.Id == carId);
        if (car == null)
        {
            return false;
        }

        _context.Remove(car);
        _context.SaveChanges();
        return true;
    }

    public bool EditDetails(string userId, CarAddDTO carDto, int id)
    {
        var car = _context.Cars.FirstOrDefault(c => c.UserId == userId && c.Id == id);
        if (car == null)
        {
            return false;
        }

        car.Make = carDto.Make;
        car.Model = carDto.Model;
        car.Year = carDto.Year;
        _context.SaveChanges();
        return true;
    }
}
