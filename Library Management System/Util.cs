namespace Library_Management_System;

public static class Util
{
	public static string GenerateUniqueLiteratureId(Library library)
	{
		while (true)
		{
			var guid = Guid.NewGuid().ToString();

			if (library.Literatures.Exists(literature => literature.Id == guid))
				GenerateUniqueLiteratureId(library);
			else return guid;
		}
	}

	public static string GenerateUniqueMemberId(Library library)
	{
		while (true)
		{
			var guid = Guid.NewGuid().ToString();

			if (library.Members.Exists(member => member.Id == guid))
				GenerateUniqueMemberId(library);
			else return guid;
		}
	}

	public static string PromptForNonEmptyInput(string message)
	{
		while (true)
		{
			Console.Clear();

			Console.WriteLine(message);
			var input = Console.ReadLine();

			if (!string.IsNullOrEmpty(input)) return input;

			Console.WriteLine("Input can't be empty! Please try again.");
		}
	}

	public static string? PromptForNullableInput(string message)
	{
		while (true)
		{
			Console.Clear();

			Console.WriteLine(message);
			var input = Console.ReadLine();

			return string.IsNullOrEmpty(input) ? null : input;
		}
	}

	public static DateOnly? PromptForNullableDateOnlyInput(string message)
	{
		while (true)
		{
			Console.Clear();

			Console.WriteLine(message);
			var input = Console.ReadLine();

			if (string.IsNullOrEmpty(input))
				return null;

			if (DateOnly.TryParse(input, out var date))
				return date;

			Console.WriteLine("Invalid date format. Please try again.");
			Console.ReadKey();
		}
	}
}