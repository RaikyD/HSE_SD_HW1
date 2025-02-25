using Xunit;
using Moq;
using HW1.Domain_Layer;
using HW1.Business_Layer;
using System.Collections.Generic;
using System.Linq;

public class VetClinicStub : IVetClinic
{
    public string Name { get; init; } = "Stub Clinic";
}

public class ZooTests
{
    private readonly Zoo _zoo;
    private readonly Mock<IVetClinic> _vetClinicMock;
    private readonly ZooService _zooService;
    private readonly ZooFoodService _foodService;
    private readonly ZooValidator _validator;

    public ZooTests()
    {
        _vetClinicMock = new Mock<IVetClinic>();
        _zooService = new ZooService();
        _foodService = new ZooFoodService();
        _validator = new ZooValidator();

        _zoo = new Zoo(_vetClinicMock.Object, _zooService, _foodService, _validator);
    }

    [Fact]
    public void AddAnimal_ShouldAddHealthyAnimal()
    {
        var herbo = new Herbo(5, 1, "Rabbit", true, 6)
        {
            Kindness = 6,
            Food = 5,
            Number = 1,
            _name = "Rabbit",
            _isHealthy = true
        };
        _vetClinicMock.Setup(v => v.IsHealthy(herbo)).Returns(true);

        _zoo.AddAnimal(herbo);

        Assert.Contains(herbo, _zoo.Animals);
        Assert.Contains(herbo, _zoo.ContactZoo);
    }

    [Fact]
    public void AddAnimal_ShouldNotAddUnhealthyAnimal()
    {
        var herbo = new Herbo(3, 2, "Rabbit", false, 4);
        _vetClinicMock.Setup(v => v.IsHealthy(herbo)).Returns(false);

        _zoo.AddAnimal(herbo);

        Assert.DoesNotContain(herbo, _zoo.Animals);
    }

    [Fact]
    public void AddAnimal_ShouldNotAddAnimalWithDuplicateNumber()
    {
        var herbo1 = new Herbo(2, 3, "Rabbit1", true, 6);
        var herbo2 = new Herbo(2, 3, "Rabbit2", true, 5);

        _vetClinicMock.Setup(v => v.IsHealthy(herbo1)).Returns(true);
        _vetClinicMock.Setup(v => v.IsHealthy(herbo2)).Returns(true);

        _zoo.AddAnimal(herbo1);
        _zoo.AddAnimal(herbo2);

        Assert.Single(_zoo.Animals, a => a.Number == 3);
    }

    [Fact]
    public void AddThing_ShouldAddUniqueThing()
    {
        var table = new Table(1, "Wooden Table", "oak")
        {
            Number = 0,
            _thingName = null
        };

        _zoo.AddThing(table);

        Assert.Contains(table, _zoo.Things);
    }

    [Fact]
    public void AddThing_ShouldNotAddDuplicateThing()
    {
        var table1 = new Table(2, "Table1")
        {
            Number = 0,
            _thingName = null
        };
        var table2 = new Table(2, "Table2")
        {
            Number = 0,
            _thingName = null
        };

        _zoo.AddThing(table1);
        _zoo.AddThing(table2);

        Assert.Single(_zoo.Things);
    }

    [Fact]
    public void GetTotalFoodConsumption_ShouldCalculateCorrectSum()
    {
        var herbo1 = new Herbo(5, 1, "Rabbit1", true, 6);
        
        var herbo2 = new Herbo(3, 2, "Rabbit2", true, 5);

        _vetClinicMock.Setup(v => v.IsHealthy(It.IsAny<Animal>())).Returns(true);

        _zoo.AddAnimal(herbo1);
        _zoo.AddAnimal(herbo2);

        int totalFood = _zoo.GetTotalFoodConsumption();

        Assert.Equal(8, totalFood);
    }

    [Fact]
    public void GetTotalAnimals_ShouldReturnCorrectCount()
    {
        var herbo1 = new Herbo(4, 5, "Rabbit1", true, 6);
        
        var herbo2 = new Herbo(3, 6, "Rabbit2", true, 4);

        _vetClinicMock.Setup(v => v.IsHealthy(It.IsAny<Animal>())).Returns(true);

        _zoo.AddAnimal(herbo1);
        _zoo.AddAnimal(herbo2);

        int totalAnimals = _zoo.GetTotalAnimals();

        Assert.Equal(2, totalAnimals);
    }
    [Fact]
    public void AddAnimal_HerboKindnessEqualFive_ShouldNotBeInContactZoo()
    {
        // Arrange
        var herbo = new Herbo(food: 4, number: 10, name: "Goat", isHealthy: true, kindness: 5);
        _vetClinicMock.Setup(v => v.IsHealthy(herbo)).Returns(true);

        // Act
        _zoo.AddAnimal(herbo);

        // Assert
        // Животное должно попасть в общий список
        Assert.Contains(herbo, _zoo.Animals);
        // Но в контактный зоопарк не должно, так как Kindness == 5
        Assert.DoesNotContain(herbo, _zoo.ContactZoo);
    }

    [Fact]
    public void AddAnimal_HealthyPredator_ShouldNotBeInContactZoo()
    {
        // Arrange
        // Создаём хищника, который здоров
        var predator = new Predator(food: 6, number: 11, name: "Lion", isHealthy: true);
        _vetClinicMock.Setup(v => v.IsHealthy(predator)).Returns(true);

        // Act
        _zoo.AddAnimal(predator);

        // Assert
        // Хищник должен добавиться в общий список
        Assert.Contains(predator, _zoo.Animals);
        // Но не должен оказаться в контактном зоопарке
        Assert.DoesNotContain(predator, _zoo.ContactZoo);
    }
    [Fact]
    public void Monkey_Constructor_SetsProperties()
    {
        // Arrange
        int food = 5;
        int number = 10;
        string name = "FunnyMonkey";
        bool isHealthy = true;
        int kindness = 8;

        // Act
        var monkey = new Monkey(food, number, name, isHealthy, kindness);

        // Assert
        Assert.Equal(food, monkey.Food);
        Assert.Equal(number, monkey.Number);
        Assert.Equal(name, monkey._name);
        Assert.Equal(isHealthy, monkey._isHealthy);
        Assert.Equal(kindness, monkey.Kindness);
        Assert.True(monkey.CanBeInContactZoo);
    }

    [Fact]
    public void Rabit_Constructor_SetsProperties()
    {
        // Arrange
        int food = 2;
        int number = 20;
        string name = "WhiteRabit";
        bool isHealthy = false;
        int kindness = 10;

        // Act
        var rabit = new Rabit(food, number, name, isHealthy, kindness);

        // Assert
        Assert.Equal(food, rabit.Food);
        Assert.Equal(number, rabit.Number);
        Assert.Equal(name, rabit._name);
        Assert.Equal(isHealthy, rabit._isHealthy);
        Assert.Equal(kindness, rabit.Kindness);
        Assert.True(rabit.CanBeInContactZoo);
    }

    [Fact]
    public void Tiger_Constructor_SetsProperties()
    {
        // Arrange
        int food = 12;
        int number = 30;
        string name = "BengalTiger";
        bool isHealthy = true;

        // Act
        var tiger = new Tiger(food, number, name, isHealthy);

        // Assert
        Assert.Equal(food, tiger.Food);
        Assert.Equal(number, tiger.Number);
        Assert.Equal(name, tiger._name);
        Assert.Equal(isHealthy, tiger._isHealthy);
    }

    [Fact]
    public void Wolf_Constructor_SetsProperties()
    {
        // Arrange
        int food = 6;
        int number = 40;
        string name = "GreyWolf";
        bool isHealthy = true;

        // Act
        var wolf = new Wolf(food, number, name, isHealthy);

        // Assert
        Assert.Equal(food, wolf.Food);
        Assert.Equal(number, wolf.Number);
        Assert.Equal(name, wolf._name);
        Assert.Equal(isHealthy, wolf._isHealthy);
    }

    [Fact]
    public void Computer_Properties_ShouldSetAndGet()
    {
        // Arrange
        
        int number = 101;
        string thingName = "Gaming PC";
        
        var computer = new Computer(number, thingName);
        
        Assert.Equal(number, computer.Number);
        Assert.Equal(thingName, computer._thingName);
    }

    [Fact]
    public void Table_Constructor_AndProperties()
    {
        // Arrange
        int number = 202;
        string name = "Office Table";
        string material = "Metal";

        // Act
        var table = new Table(number, name, material);

        // Assert
        Assert.Equal(number, table.Number);
        Assert.Equal(name, table._thingName);
        Assert.Equal(material, table.material);
    }

    [Fact]
    public void Table_Constructor_WithDefaultMaterial()
    {
        // Arrange
        int number = 303;
        string name = "Unknown Table";

        // Act
        var table = new Table(number, name); // material по умолчанию = \"неизвестно\"

        // Assert
        Assert.Equal(number, table.Number);
        Assert.Equal(name, table._thingName);
        Assert.Equal("неизвестно", table.material);
    }

    [Fact]
    public void VetClinic_Constructor_SetsName1()
    {
        // Arrange
        string clinicName = "SuperVet";

        // Act
        var vetClinic = new VetClinic(clinicName);

        // Assert
        Assert.Equal(clinicName, vetClinic.Name);
    }

    [Fact]
    public void VetClinic_IsHealthy_ReturnsCorrectValues()
    {
        // Arrange
        var clinic = new VetClinic("TestVet");
        var healthyPredator = new Predator(5, 1, "Lion", true);
        var sickHerbo = new Herbo(3, 2, "Goat", false, 3);

        // Act
        bool isHealthyLion = clinic.IsHealthy(healthyPredator);
        bool isHealthyGoat = clinic.IsHealthy(sickHerbo);

        // Assert
        Assert.True(isHealthyLion);
        Assert.False(isHealthyGoat);
    }
    [Fact]
    public void VetClinic_Constructor_SetsName()
    {
        // Arrange
        string clinicName = "SuperVet";

        // Act
        var vetClinic = new VetClinic(clinicName);

        // Assert
        Assert.Equal(clinicName, vetClinic.Name);
    }

    [Fact]
    public void VetClinic_IsHealthy_ReturnsTrue_WhenAnimalIsHealthy()
    {
        // Arrange
        var clinic = new VetClinic("HealthyVet");
        var healthyAnimal = new Predator(5, 1, "Lion", true);

        // Act
        bool result = clinic.IsHealthy(healthyAnimal);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VetClinic_IsHealthy_ReturnsFalse_WhenAnimalIsNotHealthy()
    {
        // Arrange
        var clinic = new VetClinic("HealthyVet");
        var sickAnimal = new Predator(5, 2, "Wolf", false);

        // Act
        bool result = clinic.IsHealthy(sickAnimal);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void IsHealthy_ReturnsTrue_WhenAnimalIsHealthy()
    {
        // Arrange
        IVetClinic clinic = new VetClinicStub { Name = "Test Clinic" };
        var healthyAnimal = new Predator(5, 1, "Lion", true);

        // Act
        bool result = clinic.IsHealthy(healthyAnimal);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsHealthy_ReturnsFalse_WhenAnimalIsNotHealthy()
    {
        // Arrange
        IVetClinic clinic = new VetClinicStub { Name = "Test Clinic" };
        var sickAnimal = new Predator(5, 2, "Wolf", false);

        // Act
        bool result = clinic.IsHealthy(sickAnimal);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Name_CanBeSetAndRetrieved()
    {
        // Arrange
        IVetClinic clinic = new VetClinicStub { Name = "Custom Clinic Name" };

        // Act
        string actualName = clinic.Name;

        // Assert
        Assert.Equal("Custom Clinic Name", actualName);
    }
}