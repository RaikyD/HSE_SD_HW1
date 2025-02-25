using System.Data;
using HW1.Domain_Layer;

namespace HW1.Business_Layer;

public class ZooValidator
{
    public bool IsNumberUnique(int number, IEnumerable<IInventory> items)
    {
        if (items.Count() != 0)
        {
            return items.All(item => item.Number != number);
        }

        return true;
    }
    
}