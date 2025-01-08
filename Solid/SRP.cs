using System.Diagnostics;


namespace DesignPatterns.Solid
{
	internal class Journal
	{
		private readonly List<string> entries = new List<string>();

		private static int count = 0;

		public int AddEntry(string text)
		{
			entries.Add($"{++count}: {text}");
			return count; // memento pattern!
		}

		public void RemoveEntry(int index)
		{
			entries.RemoveAt(index);
		}

		public override string ToString()
		{
			return string.Join(Environment.NewLine, entries);
		}

		// breaks single responsibility principle
		public void Save(string filename, bool overwrite = false)
		{
			File.WriteAllText(filename, ToString());
		}
	}

	// handles the responsibility of persisting objects
	internal class Persistence
	{
		public void SaveToFile(Journal journal, string filename, bool overwrite = false)
		{
			if (overwrite || !File.Exists(filename))
				File.WriteAllText(filename, journal.ToString());
		}
	}

	internal static class SRP
	{
		public static void Run()
		{
			var j = new Journal();
			j.AddEntry("I cried today.");
			j.AddEntry("I ate a bug.");
			Console.WriteLine(j);
			var p = new Persistence();
			var filename = @"c:\temp\journal.txt";
			p.SaveToFile(j, filename, true);
			Process.Start(filename);
		}
	}
}
