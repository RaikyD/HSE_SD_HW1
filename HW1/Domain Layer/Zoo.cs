using HW1.Business_Layer;
namespace HW1.Domain_Layer;

using System.Linq;

public class Zoo
{
    private readonly List<Animal> _animals = new();
    public IReadOnlyList<Animal> Animals => _animals;

    private readonly List<Thing> _things = new();
    public IReadOnlyList<Thing> Things => _things;

    private readonly List<Animal> _contactZoo = new();
    public IReadOnlyList<Animal> ContactZoo => _contactZoo;

    private readonly IVetClinic _vetClinic;
    private readonly ZooService _zooService;
    private readonly ZooFoodService _foodService;
    private readonly ZooValidator _validator;

    public Zoo(IVetClinic vetClinic, ZooService zooService, ZooFoodService foodService, ZooValidator validator)
    {
        _vetClinic = vetClinic;
        _zooService = zooService;
        _foodService = foodService;
        _validator = validator;
    }
    
    public void AddAnimal(Animal animal)
    {
        // Проверка здоровья
        if (!_vetClinic.IsHealthy(animal))
        {
            Console.WriteLine($"Животное {animal._name} не прошло проверку здоровья и не может быть добавлено.");
            return;
        }

        if (!_validator.IsNumberUnique(animal.Number, Animals))
        {
            Console.WriteLine("Животное с таким номером уже существует.");
            return;
        }
        
        _animals.Add(animal);
    
        // Проверка, если животное подходит для контактного зоопарка
        if (animal is Herbo herboAnimal && _zooService.GetContactZooAnimals(new List<Herbo> { herboAnimal }).Any())
        {
            _contactZoo.Add(animal);
            Console.WriteLine($"Животное {animal._name} добавлено в обычный и контактный зоопарк!");
            return;
        }

        // Вывод статистики
        Console.WriteLine($"Животное {animal._name} добавлено! Номер: {animal.Number}, Еда: {animal.Food} кг/день.");
    }
    
    public void AddThing(Thing thing)
    {
        if (!_validator.IsNumberUnique(thing.Number, Things))
        {
            Console.WriteLine("Вещь с таким номером уже существует.");
            return;
        }
        _things.Add(thing);
        Console.WriteLine($"{thing._thingName} успешно добавлена! Номер: {thing.Number}");
    }
    
    public int GetTotalFoodConsumption()
    {
        return _foodService.CalculateTotalFoodConsumption(Animals);
    }
    
    public int GetTotalAnimals()
    {
        return _zooService.CalculateTotalAnimals(Animals);
    }
    
}
