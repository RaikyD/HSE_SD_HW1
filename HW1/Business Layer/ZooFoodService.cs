using HW1.Domain_Layer;

namespace HW1.Business_Layer;

public class ZooFoodService
{
    public int CalculateTotalFoodConsumption(IEnumerable<Animal> animals)
    {
        return animals.Sum(a => a.Food);
    }
}
