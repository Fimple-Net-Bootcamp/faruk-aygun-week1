namespace Library_Management_System.Models.Literatures;

public class Magazine : Literature, IPrintable
{
	public void Print()
	{
		Console.WriteLine($"Magazine: {Name} ({Date}) ({Id})");
	}
}