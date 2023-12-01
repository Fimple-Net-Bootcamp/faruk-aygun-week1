namespace Library_Management_System.Models.Literatures;

public class Literature : IPrintable
{
	public enum LiteratureType
	{
		Book,
		Magazine
	}

	public required string Id { get; set; }
	public required string Name { get; set; }
	public DateOnly? Date { get; set; } = new(1900, 01, 01);
	public string? Author { get; set; } = "Unknown";
	public LiteratureType Type { get; set; }

	public void Print()
	{
		Console.WriteLine($"Book: {Name} by {Author}\t{Date}\t({Id})");
	}
}