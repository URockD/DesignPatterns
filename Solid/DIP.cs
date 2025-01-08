using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;
namespace DesignPatterns.Solid
{
	internal enum Relationship
	{
		Parent,
		Child,
		Sibling
	}

	internal class Person
	{
		public string Name = string.Empty;
	}

	internal interface IRelationshipBrowser
	{
		IEnumerable<Person> FindAllChildrenOf(string name);
	}

	internal class Relationships : IRelationshipBrowser // low-level
	{
		private List<(Person, Relationship, Person)> relations
		  = new List<(Person, Relationship, Person)>();

		public void AddParentAndChild(Person parent, Person child)
		{
			relations.Add((parent, Relationship.Parent, child));
			relations.Add((child, Relationship.Child, parent));
		}

		public List<(Person, Relationship, Person)> Relations => relations;

		public IEnumerable<Person> FindAllChildrenOf(string name)
		{
			return relations
			  .Where(x => x.Item1.Name == name
						  && x.Item2 == Relationship.Parent).Select(r => r.Item3);
		}
	}

	internal class Research
	{
		internal Research(Relationships relationships)
		{
			// high-level: find all of john's children
			//var relations = relationships.Relations;
			//foreach (var r in relations
			//  .Where(x => x.Item1.Name == "John"
			//              && x.Item2 == Relationship.Parent))
			//{
			//  WriteLine($"John has a child called {r.Item3.Name}");
			//}
		}

		internal Research(IRelationshipBrowser browser)
		{
			foreach (var p in browser.FindAllChildrenOf("John"))
			{
				WriteLine($"John has a child called {p.Name}");
			}
		}

	}

	internal static class DIP
	{
		static void Main(string[] args)
		{
			var parent = new Person { Name = "John" };
			var child1 = new Person { Name = "Chris" };
			var child2 = new Person { Name = "Matt" };
			var relationships = new Relationships();
			relationships.AddParentAndChild(parent, child1);
			relationships.AddParentAndChild(parent, child2);
			new Research(relationships);
		}
	}


}
