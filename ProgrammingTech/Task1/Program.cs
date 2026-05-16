namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        var tanks = GetTanks();
        var units = GetUnits();
        var factories = GetFactories();

        Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

        // Поиск установки по имени резервуара (как в каркасе из задания)
        var foundUnit = FindUnit(units, tanks, "Резервуар 2");
        if (foundUnit != null)
        {
            var factory = FindFactory(factories, foundUnit);
            Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory!.Name}");
        }

        // Полный вывод всех резервуаров с указанием установки и завода
        Console.WriteLine();
        Console.WriteLine("Все резервуары:");
        foreach (var tank in tanks)
        {
            var unit = FindUnitById(units, tank.UnitId);
            var factory = unit != null ? FindFactory(factories, unit) : null;
            string type = ClassifyTank(tank.Description);
            Console.WriteLine($"{tank.Name} [{type}] — установка {unit?.Name ?? "(нет)"}, завод {factory?.Name ?? "(нет)"}, загрузка {tank.Volume}/{tank.MaxVolume}");
        }

        // Общая сумма загрузки всех резервуаров
        var totalVolume = GetTotalVolume(tanks);
        Console.WriteLine();
        Console.WriteLine($"Общий объём резервуаров: {totalVolume}");

        // Поиск резервуара по имени, введённому пользователем
        Console.WriteLine();
        Console.Write("Введите имя резервуара для поиска: ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Имя не введено.");
        }
        else
        {
            var tank = FindTankByName(tanks, input);
            if (tank == null)
            {
                Console.WriteLine($"Резервуар '{input}' не найден.");
            }
            else
            {
                var unit = FindUnitById(units, tank.UnitId);
                var factory = unit != null ? FindFactory(factories, unit) : null;
                string type = ClassifyTank(tank.Description);
                Console.WriteLine($"Найден: {tank.Name}, тип: {type}, установка {unit?.Name}, завод {factory?.Name}, загрузка {tank.Volume}/{tank.MaxVolume}");
            }
        }
    }

    public static Tank[] GetTanks()
    {
        return new Tank[]
        {
            new Tank { Id = 1, Name = "Резервуар 1",                Description = "Надземный - вертикальный",   Volume = 1500, MaxVolume = 2000, UnitId = 1 },
            new Tank { Id = 2, Name = "Резервуар 2",                Description = "Надземный - горизонтальный", Volume = 2500, MaxVolume = 3000, UnitId = 1 },
            new Tank { Id = 3, Name = "Дополнительный резервуар 24", Description = "Надземный - горизонтальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
            new Tank { Id = 4, Name = "Резервуар 35",               Description = "Надземный - вертикальный",   Volume = 3000, MaxVolume = 3000, UnitId = 2 },
            new Tank { Id = 5, Name = "Резервуар 47",               Description = "Подземный - двустенный",     Volume = 4000, MaxVolume = 5000, UnitId = 2 },
            new Tank { Id = 6, Name = "Резервуар 256",              Description = "Подводный",                  Volume = 500,  MaxVolume = 500,  UnitId = 3 },
        };
    }

    public static Unit[] GetUnits()
    {
        return new Unit[]
        {
            new Unit { Id = 1, Name = "ГФУ-2",  Description = "Газофракционирующая установка",  FactoryId = 1 },
            new Unit { Id = 2, Name = "АВТ-6",  Description = "Атмосферно-вакуумная трубчатка", FactoryId = 1 },
            new Unit { Id = 3, Name = "АВТ-10", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 2 },
        };
    }

    public static Factory[] GetFactories()
    {
        return new Factory[]
        {
            new Factory { Id = 1, Name = "НПЗ№1", Description = "Первый нефтеперерабатывающий завод" },
            new Factory { Id = 2, Name = "НПЗ№2", Description = "Второй нефтеперерабатывающий завод" },
        };
    }

    // Возвращает установку (Unit), которой принадлежит резервуар (Tank),
    // найденный в массиве резервуаров по имени. Если резервуар не найден — null.
    public static Unit? FindUnit(Unit[] units, Tank[] tanks, string tankName)
    {
        Tank? foundTank = null;
        foreach (var t in tanks)
        {
            if (t.Name == tankName)
            {
                foundTank = t;
                break;
            }
        }

        if (foundTank == null)
        {
            return null;
        }

        foreach (var u in units)
        {
            if (u.Id == foundTank.UnitId)
            {
                return u;
            }
        }
        return null;
    }

    // Возвращает завод, к которому принадлежит установка
    public static Factory? FindFactory(Factory[] factories, Unit unit)
    {
        foreach (var f in factories)
        {
            if (f.Id == unit.FactoryId)
            {
                return f;
            }
        }
        return null;
    }

    // Сумма Volume по всем резервуарам
    public static int GetTotalVolume(Tank[] tanks)
    {
        int total = 0;
        foreach (var t in tanks)
        {
            total += t.Volume;
        }
        return total;
    }

    // Вспомогательный метод — установка по её Id (нужен для перебора всех резервуаров)
    private static Unit? FindUnitById(Unit[] units, int unitId)
    {
        foreach (var u in units)
        {
            if (u.Id == unitId)
            {
                return u;
            }
        }
        return null;
    }

    private static Tank? FindTankByName(Tank[] tanks, string name)
    {
        foreach (var t in tanks)
        {
            if (t.Name == name)
            {
                return t;
            }
        }
        return null;
    }

    // Классификация типа резервуара по первому слову в описании (через switch)
    private static string ClassifyTank(string description)
    {
        string firstWord = description.Split(' ')[0].ToLower();
        switch (firstWord)
        {
            case "надземный":
                return "надземный";
            case "подземный":
                return "подземный";
            case "подводный":
                return "подводный";
            default:
                return "неизвестный";
        }
    }
}
