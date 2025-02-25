namespace HW1.Domain_Layer;
public interface IAlive
{
    public int Food { get; set; }
}

public interface IInventory
{
    public int Number { get; set; }
}

public interface IContactZooCandidate
{
    bool CanBeInContactZoo { get; }
}

public interface IVetClinic
{
    public string Name { get; init; }

    public bool IsHealthy(Animal animal)
    {
        if (animal._isHealthy)
        {
            return true;
        }

        return false;
    }
}