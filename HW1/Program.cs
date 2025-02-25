using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using HW1.Domain_Layer;
using HW1.Business_Layer;
using HW1.Presentation_Layer;

namespace HW1
{
    class Program
    {
        static void Main()
        {
            // Настраиваем DI 
            var services = new ServiceCollection();

            // Регистрируем бизнес-сервисы
            services.AddSingleton<ZooService>();
            services.AddSingleton<ZooFoodService>();
            services.AddSingleton<ZooValidator>();
            services.AddSingleton<ZooStatisticsService>();

            // Сначала запрашиваем у пользователя название Ветклиники, сделал в случае дальнейшего улучшения:
            Console.Write("Введите название Ветклиники: ");
            string vetName = Console.ReadLine() ?? "Default Vet Clinic";
            var vetClinic = new VetClinic(vetName);
            services.AddSingleton<IVetClinic>(vetClinic);
            
            services.AddSingleton<Zoo>();
            
            var serviceProvider = services.BuildServiceProvider();

            
            var zoo = serviceProvider.GetService<Zoo>();
            var manageServ = serviceProvider.GetService<ZooStatisticsService>();
            
            while (true)
            {
                int choice = ShowMenu(new string[]
                {
                    "Добавить животное",
                    "Добавить вещь",
                    "Вывести текущую информацию обо всём",
                    "Вывести список и описание животных в контактном зоопарке",
                    "Выход из программы"
                });

                switch (choice)
                {
                    case 0:
                        AddAnimal(zoo);
                        break;
                    case 1:
                        AddThing(zoo);
                        break;
                    case 2:
                        manageServ?.PrintStatistics();
                        break;
                    case 3:
                        manageServ?.PrintContactStatistics();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        // Метод для отображения меню с выбором с помощью стрелочек
        static int ShowMenu(string[] options)
        {
            int selected = 0;
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Используйте стрелки для выбора и Enter для подтверждения:\n");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                        Console.WriteLine($"> {options[i]}");
                    else
                        Console.WriteLine($"  {options[i]}");
                }

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                    selected = (selected == 0) ? options.Length - 1 : selected - 1;
                else if (key == ConsoleKey.DownArrow)
                    selected = (selected == options.Length - 1) ? 0 : selected + 1;
            } while (key != ConsoleKey.Enter);

            return selected;
        }

        /*
        static void AddAnimal(Zoo zoo)
        {
            // Выбор типа животного: Травоядное или Хищник
            int typeChoice = ShowMenu(new string[] { "Травоядное", "Хищник" });
            Console.Clear();
            Console.WriteLine("Введите данные для животного. Для отказа введите пустую строку на любом этапе.");

            try
            {
                Console.Write("Имя: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) return;

                Console.Write("Номер: ");
                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.WriteLine("Некорректный ввод! Возвращение в начальное меню.... (Нажмите любую клавишу)");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Количество еды (кг/день): ");
                if (!int.TryParse(Console.ReadLine(), out int food))
                {
                    Console.WriteLine("Некорректный ввод! Возвращение в начальное меню.... (Нажмите любую клавишу)");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Животное здорово? (true/false): ");
                if (!bool.TryParse(Console.ReadLine(), out bool isHealthy))
                {
                    Console.WriteLine("Некорректный ввод! Возвращение в начальное меню.... (Нажмите любую клавишу)");
                    Console.ReadLine();
                    return;
                }

                if (typeChoice == 0)
                {
                    // Для травоядного требуется уровень доброжелательности
                    Console.Write("Уровень доброжелательности (от 1 до 10): ");
                    if (!int.TryParse(Console.ReadLine(), out int kindness)) return;

                    var herbo = new Herbo(food, number, name, isHealthy, kindness);
                    zoo.AddAnimal(herbo);
                }
                else
                {
                    // Для хищника создаём экземпляр SimplePredator (реализация для демонстрации)
                    var predator = new Predator(food, number, name, isHealthy);
                    zoo.AddAnimal(predator);
                }
            }
            catch
            {
                // В случае исключения возвращаемся в главное меню
                return;
            }

            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
        */
        static void AddAnimal(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("Выберите тип животного:");
            int typeChoice = ShowMenu(new string[] { "Травоядное", "Хищник" });

            // Запрашиваем общие параметры для всех животных
            Console.Write("Введите имя животного (или пустую строку для отмены|||выбор типа животного будет позже): ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
                return;

            Console.Write("Введите номер животного: ");
            if (!int.TryParse(Console.ReadLine(), out int number))
                return;

            Console.Write("Введите количество еды (кг/день): ");
            if (!int.TryParse(Console.ReadLine(), out int food))
                return;

            Console.Write("Животное здорово? (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool isHealthy))
                return;

            Animal animal = null;

            if (typeChoice == 0) // Травоядное
            {
                Console.WriteLine("Выберите тип травоядного:");
                int herbChoice = ShowMenu(new string[] { "Rabit", "Monkey" });
                Console.Write("Введите уровень доброжелательности (от 1 до 10): ");
                if (!int.TryParse(Console.ReadLine(), out int kindness))
                    return;

                if (herbChoice == 0)
                {
                    animal = new Rabit(food, number, name, isHealthy, kindness);
                }
                else
                {
                    animal = new Monkey(food, number, name, isHealthy, kindness);
                }
            }
            else 
            {
                Console.WriteLine("Выберите тип хищника:");
                int predChoice = ShowMenu(new string[] { "Wolf", "Tiger" });
                if (predChoice == 0)
                {
                    animal = new Wolf(food, number, name, isHealthy);
                }
                else
                {
                    animal = new Tiger(food, number, name, isHealthy);
                }
            }

            zoo.AddAnimal(animal);
            Console.WriteLine("Нажмите любую клавишу для возврата в главное меню...");
            Console.ReadKey();
        }
        static void AddThing(Zoo zoo)
        {
            // Выбор типа вещи: Стол или Компьютер
            int typeChoice = ShowMenu(new string[] { "Стол", "Компьютер" });
            Console.Clear();
            Console.WriteLine("Введите данные для вещи. Для отказа введите пустую строку на любом этапе.");

            try
            {
                Console.Write("Название: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) return;

                Console.Write("Номер: ");
                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.WriteLine("Некорректный ввод! Возвращение в начальное меню.... (Нажмите любую клавишу)");
                    Console.ReadLine();
                    return;
                };

                if (typeChoice == 0)
                {
                    Console.Write("Материал (например, дерево, металл): ");
                    string material = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(material)) return;

                    var table = new Table(number, name, material);
                    zoo.AddThing(table);
                }
                else
                {
                    var comp = new Computer(number, name);
                    zoo.AddThing(comp);
                }
            }
            catch
            {
                return;
            }
        }
    }
}
