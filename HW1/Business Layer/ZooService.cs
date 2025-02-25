using HW1.Domain_Layer;

namespace HW1.Business_Layer;

public class ZooService
{
    public IEnumerable<Herbo> GetContactZooAnimals(IEnumerable<Herbo> herbivores)
    {
        return herbivores.Where(h => h.Kindness > 5);
    }
    
    public int CalculateTotalAnimals(IEnumerable<Animal> animals)
    {
        return animals.Count();
    }
    
}