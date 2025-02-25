using System.ComponentModel.DataAnnotations;

namespace HW1.Domain_Layer;

public class Thing : IInventory
{
    public  int Number { get; set; }
    public  string _thingName { get; set; }
}



