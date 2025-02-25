namespace HW1.Domain_Layer;

public class Herbo : Animal, IContactZooCandidate
{
    public int Kindness { get; set; }
    public bool CanBeInContactZoo => Kindness > 5;

    public Herbo(int food, int number, string name, bool isHealthy, int kindness)
        : base(food, number, name, isHealthy)
    {
        Kindness = kindness;
    }
}