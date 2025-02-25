using System.Reflection;

namespace HW1.Domain_Layer;

/// <summary>
/// Нам не нужно создавать экзмепляры класса Animal, поэтому лучше создать абстракцию
/// </summary>
public abstract class Animal : IAlive, IInventory
{
    public int Food { get; set; }
    public int Number { get; set; }
    public string _name { get; init; }
    public bool _isHealthy { get; set; }
    
    public Animal(int food, int number, string name, bool isHealthy)
    {
        Food = food;
        Number = number;
        _name = name;
        _isHealthy = isHealthy;
    }
}



