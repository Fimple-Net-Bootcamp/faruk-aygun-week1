using Library_Management_System.Models;
using Library_Management_System.Models.Literatures;

namespace Library_Management_System;

public class Library
{
	public List<Literature> Literatures { get; private set; } = new();
	public List<Member> Members { get; private set; } = new();

	public void ShowLiteratures()
	{
		foreach (var literature in Literatures)
		{
			literature.Print();
			Console.WriteLine("\n");
		}
	}

	public void ShowMembers()
	{
		foreach (var member in Members)
		{
			member.Print();
			Console.WriteLine("\n");
		}
	}

	public void AddLiterature(Literature literature)
	{
		Literatures.Add(literature);
		Console.WriteLine($"{literature.Name} added to database successfully!");
	}

	public void RemoveLiterature(string id)
	{
		var literature = Literatures.Find(literature => literature.Id == id);

		if (literature is null)
		{
			Console.WriteLine("Literature not found!");
			return;
		}

		Literatures.Remove(literature);
		Console.WriteLine($"{literature.Name} removed from database successfully!");
	}

	public void AddMember(Member member)
	{
		Members.Add(member);
		Console.WriteLine($"{member.Name} added to database successfully!");
	}

	public void RemoveMember(string id)
	{
		var member = Members.Find(member => member.Id == id);

		if (member is null)
		{
			Console.WriteLine("Member not found!");
			return;
		}

		Members.Remove(member);
		Console.WriteLine($"{member.Name} removed from database successfully!");
	}

	public void Lend(string memberId, string literatureId, ILendTransactions lendTransactions)
	{
		var member = Members.Find(member => member.Id == memberId);
		var literature = Literatures.Find(literature => literature.Id == literatureId);

		if (literature is null)
		{
			Console.WriteLine("Literature not found!");
			return;
		}

		if (member is null)
		{
			Console.WriteLine("Member not found!");
			return;
		}

		lendTransactions.Lend(literature, member);
	}

	public void Refund(string memberId, string literatureId, ILendTransactions lendTransactions)
	{
		var member = Members.Find(member => member.Id == memberId);
		var literature = Literatures.Find(literature => literature.Id == literatureId);

		if (literature is null)
		{
			Console.WriteLine("Literature not found!");
			return;
		}

		if (member is null)
		{
			Console.WriteLine("Member not found!");
			return;
		}

		lendTransactions.Return(literature, member);
	}
}