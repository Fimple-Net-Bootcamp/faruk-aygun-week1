using Library_Management_System.Models;
using Library_Management_System.Models.Literatures;

namespace Library_Management_System;

public class LongTermLend : ILendTransactions
{
	public void Lend(Literature literature, Member member)
	{
		member.Lend(literature);
		Console.WriteLine($"{literature.Name}, long term loaned.");
	}

	public void Return(Literature literature, Member member)
	{
		member.Refund(literature);
		Console.WriteLine($"{literature.Name}, long term loan returned.");
	}
}