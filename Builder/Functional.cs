using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder
{
	public class Person
	{
		public string Name = string.Empty;
		public string Position = string.Empty;
	}

	public sealed class PersonBuilder
	{
		public readonly List<Action<Person>> Actions
		  = new List<Action<Person>>();

		public PersonBuilder Called(string name)
		{
			Actions.Add(p => { p.Name = name; });
			return this;
		}

		public Person Build()
		{
			var p = new Person();
			Actions.ForEach(a => a(p));
			return p;
		}
	}

	public static class PersonBuilderExtensions
	{
		public static PersonBuilder WorksAsA
		  (this PersonBuilder builder, string position)
		{
			builder.Actions.Add(p =>
			{
				p.Position = position;
			});
			return builder;
		}
	}

	public static class Functional
	{
		public static void Run()
		{
			var pb = new PersonBuilder();
			var person = pb.Called("Dmitri").WorksAsA("Programmer").Build();
		}
	}
}
