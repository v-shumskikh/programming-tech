namespace Task1;

// Установка. Принадлежит заводу через FactoryId. Содержит резервуары (связь — через UnitId в Tank)
public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int FactoryId { get; set; }
}
