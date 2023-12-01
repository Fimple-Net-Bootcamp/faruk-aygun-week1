using System.Net.Mime;
using Library_Management_System.Models;
using Library_Management_System.Models.Literatures;

namespace Library_Management_System;

public class Menu
{
	private readonly Library library = new();

	public void MainMenu()
	{
		var menuItemsArray = new[]
		{
			"1. Show Literatures",
			"2. Show Members",
			"3. Add Literature",
			"4. Remove Literature",
			"5. Add Member",
			"6. Remove Member",
			"7. Loan Literature",
			"8. Refund Literature",
			"0. Exit"
		};

		while (true)
		{
			Console.Clear();

			foreach (var item in menuItemsArray)
				Console.WriteLine($"{item}");

			var selection = Console.ReadLine() ?? string.Empty;

			switch (selection)
			{
				case "1":
					Console.Clear();
					library.ShowLiteratures();
					WaitForInputToReturn();
					break;
				case "2":
					Console.Clear();
					library.ShowMembers();
					WaitForInputToReturn();
					break;
				case "3":
					AddLiteratureMenu();
					WaitForInputToReturn();
					break;
				case "4":
					RemoveLiteratureMenu();
					WaitForInputToReturn();
					break;
				case "5":
					AddMemberMenu();
					WaitForInputToReturn();
					break;
				case "6":
					RemoveMemberMenu();
					WaitForInputToReturn();
					break;
				case "7":
					LendLiteratureMenu();
					WaitForInputToReturn();
					break;
				case "8":
					RefundLiteratureMenu();
					WaitForInputToReturn();
					break;
				case "0":
					Console.WriteLine("Exiting...");
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Invalid selection! Please try again.");
					Console.ReadKey();
					break;
			}
		}
	}

	public void AddLiteratureMenu()
	{
		var menuItemsArray = new[] { "1. Add Book", "2. Add Magazine", "0. Back" };

		while (true)
		{
			Console.Clear();

			foreach (var item in menuItemsArray)
				Console.WriteLine($"{item}\n");

			var selection = Console.ReadLine() ?? string.Empty;

			switch (selection)
			{
				case "1":
					AddLiterature(Literature.LiteratureType.Book);
					return;
				case "2":
					AddLiterature(Literature.LiteratureType.Magazine);
					return;
				case "0":
					MainMenu();
					return;

				default:
					Console.WriteLine("Invalid selection! Please try again.");
					Console.ReadKey();
					break;
			}
		}

		void AddLiterature(Literature.LiteratureType literatureType)
		{
			var name = Util.PromptForNonEmptyInput("Please enter the name:");
			var author = Util.PromptForNullableInput("Please enter the author." +
			                                         "\n If the author is unknown, please leave it blank:");
			var date = Util.PromptForNullableDateOnlyInput("Please enter the date as YYYY-MM-DD format. " +
			                                               "\n If the date is unknown, please leave it blank:");

			var literature = new Literature
			{
				Id = Util.GenerateUniqueLiteratureId(library),
				Name = name,
				Author = string.IsNullOrEmpty(author) ? null : author,
				Date = date
			};

			switch (literatureType)
			{
				case Literature.LiteratureType.Book:
					literature.Type = Literature.LiteratureType.Book;
					library.AddLiterature(literature);
					break;
				case Literature.LiteratureType.Magazine:
					literature.Type = Literature.LiteratureType.Magazine;
					library.AddLiterature(literature);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(literatureType), literatureType, null);
			}
		}
	}

	public void RemoveLiteratureMenu()
	{
		var id = Util.PromptForNonEmptyInput("Please enter the id of the literature you want to remove:");
		library.RemoveLiterature(id);
	}

	public void AddMemberMenu()
	{
		var name = Util.PromptForNonEmptyInput("Please enter the name:");
		var surname = Util.PromptForNonEmptyInput("Please enter the surname:");

		var member = new Member
		{
			Id = Util.GenerateUniqueMemberId(library),
			Name = name,
			Surname = surname
		};

		library.AddMember(member);
	}

	public void RemoveMemberMenu()
	{
		var id = Util.PromptForNonEmptyInput("Please enter the id of the member you want to remove:");
		library.RemoveMember(id);
	}

	public void LendLiteratureMenu()
	{
		var menuItemsArray = new[] { "1. Short Term Lend", "2. Long Term Lend", "0. Back" };

		while (true)
		{
			Console.Clear();

			foreach (var item in menuItemsArray)
				Console.WriteLine($"{item}\n");

			var selection = Console.ReadLine() ?? string.Empty;

			switch (selection)
			{
				case "1":
					LendLiterature(ILendTransactions.LoanType.ShortTerm);
					return;
				case "2":
					LendLiterature(ILendTransactions.LoanType.LongTerm);
					return;
				case "0":
					MainMenu();
					return;
				default:
					Console.WriteLine("Invalid selection! Please try again.");
					Console.ReadKey();
					break;
			}
		}

		void LendLiterature(ILendTransactions.LoanType loanType)
		{
			var memberId =
				Util.PromptForNonEmptyInput("Please enter the id of the member you want to lend the book to:");
			var literatureId = Util.PromptForNonEmptyInput("Please enter the id of the literature you want to loan:");

			switch (loanType)
			{
				case ILendTransactions.LoanType.ShortTerm:
					library.Lend(memberId, literatureId, new ShortTermLend());
					return;
				case ILendTransactions.LoanType.LongTerm:
					library.Lend(memberId, literatureId, new LongTermLend());
					return;
				default:
					throw new ArgumentOutOfRangeException(nameof(loanType), loanType, null);
			}
			
			WaitForInputToReturn();
		}
	}

	public void RefundLiteratureMenu()
	{
		var menuItemsArray = new[] { "1. Short Term Refund", "2. Long Term Refund", "0. Back" };

		while (true)
		{
			Console.Clear();

			foreach (var item in menuItemsArray)
				Console.WriteLine($"{item}\n");

			var selection = Console.ReadLine() ?? string.Empty;

			switch (selection)
			{
				case "1":
					RefundLiterature(ILendTransactions.LoanType.ShortTerm);
					return;
				case "2":
					RefundLiterature(ILendTransactions.LoanType.LongTerm);
					return;
				case "0":
					MainMenu();
					return;
				default:
					Console.WriteLine("Invalid selection! Please try again.");
					Console.ReadKey();
					break;
			}
		}

		void RefundLiterature(ILendTransactions.LoanType loanType)
		{
			var memberId =
				Util.PromptForNonEmptyInput("Please enter the id of the member you want to lend the book to:");
			var literatureId = Util.PromptForNonEmptyInput("Please enter the id of the literature you want to loan:");

			switch (loanType)
			{
				case ILendTransactions.LoanType.ShortTerm:
					library.Refund(memberId, literatureId, new ShortTermLend());
					return;
				case ILendTransactions.LoanType.LongTerm:
					library.Refund(memberId, literatureId, new LongTermLend());
					return;
				default:
					throw new ArgumentOutOfRangeException(nameof(loanType), loanType, null);
			}
		}

		// var id = Util.PromptForNonEmptyInput("Please enter the id of the member you want to refund:");
		// library.Refund(id);
	}

	public void WaitForInputToReturn()
	{
		Console.WriteLine("Press any key to return to main menu.");
		while (true)
		{
			if (Console.ReadKey().Key == ConsoleKey.Enter)
			{
				MainMenu();
			}
		}
	}
}