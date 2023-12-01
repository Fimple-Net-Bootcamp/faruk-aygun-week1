using Library_Management_System.Models;
using Library_Management_System.Models.Literatures;

namespace Library_Management_System;

public interface ILendTransactions
{
	public enum LoanType
	{
		ShortTerm,
		LongTerm
	}

	public abstract void Lend(Literature literature, Member member);
	public abstract void Return(Literature literature, Member member);
}