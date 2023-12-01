using Library_Management_System.Models.Literatures;

namespace Library_Management_System.Models;

public class Member : IPrintable
{
	public required string Name { get; set; }
	public required string Surname { get; set; }
	public required string Id { get; set; }
	private List<Literature> LendedLiteratures { get; set; } = new();

	public void Lend(Literature literature)
	{
		LendedLiteratures.Add(literature);
		Console.WriteLine($"{literature.Name} borrowed successfully!");
	}

	public void Refund(Literature literature)
	{
		LendedLiteratures.Remove(literature);
		Console.WriteLine($"{literature.Name} returned successfully!");
	}

	public void Print()
	{
		Console.WriteLine($"Member: {Name} {Surname} ({Id})");
	}
}