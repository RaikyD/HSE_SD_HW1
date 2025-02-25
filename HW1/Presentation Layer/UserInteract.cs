namespace HW1.Presentation_Layer;
using HW1.Business_Layer;
using HW1.Domain_Layer;

public class UserInteract
{
    private readonly Zoo _zoo;

    public UserInteract(Zoo zoo)
    {
        _zoo = zoo;
    }
}