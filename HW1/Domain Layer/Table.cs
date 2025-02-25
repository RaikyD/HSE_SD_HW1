namespace HW1.Domain_Layer;

public class Table : Thing
{
    public string material { get; set; }

    public Table(int number, string name, string mat = "неизвестно")
    {
        Number = number;
        _thingName = name;
        material = mat;
    }
}