namespace HW1.Domain_Layer;

public class Wolf : Predator
{
    public Wolf(int food, int number, string name, bool isHealthy)
        : base(food, number, name, isHealthy)
    {
    }
}