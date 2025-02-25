using HW1.Domain_Layer;

public class ZooStatisticsService
{
    private readonly Zoo _zoo;

    public ZooStatisticsService(Zoo zoo)
    {
        _zoo = zoo;
    }

            public void PrintStatistics()
        {
            Console.WriteLine("📊 Статистика зоопарка:");
            Console.WriteLine($"- Количество животных: {_zoo.GetTotalAnimals()}");
            Console.WriteLine($"- Суммарное потребление еды в день: {_zoo.GetTotalFoodConsumption()} кг");

            var contactZooAnimals = _zoo.ContactZoo.ToList();
            Console.WriteLine($"- Животных в контактном зоопарке: {contactZooAnimals.Count}");
            if (_zoo.GetTotalAnimals() > 0)
            {
                Console.WriteLine("  🐾 Список животных в зоопарке:");
                foreach (var animal in _zoo.Animals)
                {
                    // Получаем имя типа (например, Monkey, Rabit, Tiger и т.д.)
                    string animalType = animal.GetType().Name;
                    Console.WriteLine($"    - [{animalType}] {animal._name} (№{animal.Number}, ест {animal.Food} кг/день)");
                }
            }
            if (contactZooAnimals.Count > 0)
            {
                Console.WriteLine("  🐾 Список животных в контактном зоопарке:");
                foreach (var animal in contactZooAnimals)
                {
                    string animalType = animal.GetType().Name;
                    Console.WriteLine($"    - [{animalType}] {animal._name} (№{animal.Number}, ест {animal.Food} кг/день)");
                }
            }

            var inventoryItems = _zoo.Things.ToList();
            Console.WriteLine($"- Всего инвентарных объектов: {inventoryItems.Count}");
            if (inventoryItems.Count > 0)
            {
                Console.WriteLine("  🛠️ Список инвентаря:");
                foreach (var thing in _zoo.Things)
                {
                    string thingType = thing.GetType().Name;
                    Console.WriteLine($"- [{thingType}] {thing._thingName} (#{thing.Number})");
                }
            }
            Console.ReadLine();
        }

        public void PrintContactStatistics()
        {
            Console.WriteLine("📊 Статистика контактного зоопарка:");
            var contactAnimals = _zoo.ContactZoo.ToList();
            Console.WriteLine($"- Количество животных в контактном зоопарке: {contactAnimals.Count}");

            if (contactAnimals.Count > 0)
            {
                Console.WriteLine("  🐾 Список животных в контактном зоопарке:");
                foreach (var animal in contactAnimals)
                {
                    string animalType = animal.GetType().Name;
                    Console.WriteLine(
                        $"    - [{animalType}] {animal._name} (№{animal.Number}, ест {animal.Food} кг/день, " +
                        $"{(animal._isHealthy ? "Здоров" : "Болен")})");
                }
            }
            else
            {
                Console.WriteLine("Животных в контактном зоопарке нет!");
            }

            Console.ReadLine();
        }
}