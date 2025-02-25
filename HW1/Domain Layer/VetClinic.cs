namespace HW1.Domain_Layer;

public class VetClinic : IVetClinic
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

    public VetClinic(string name)
    {
        Name = name;
    }
}