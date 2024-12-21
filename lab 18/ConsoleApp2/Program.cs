using Lab18;

public class program
{
    static void Main(string[] args)
    {
        MyHashMap<string, int> Hash = new MyHashMap<string, int>();
        Hash.Put("Matreshka", 47);
        Hash.Put("John", 48);
        Hash.Put("Peter", 49);
        Hash.Put("Balalaika", 46);
        Hash.Put("Kamish", 45);
        Console.WriteLine("Элементы хэш-карты:");
        Hash.Print();
        Hash.Remove("Kamish");
        Console.WriteLine($"\n{Hash.ContainsValue(45)}\n");
        Console.WriteLine("Элементы хэш-карты после удаления элемента:");
        Hash.Print();
    }
}