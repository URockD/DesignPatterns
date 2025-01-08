using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Solid
{
	internal class Rectangle
	{
		//public int Width { get; set; }
		//public int Height { get; set; }

		public virtual int Width { get; set; }
		public virtual int Height { get; set; }

		public Rectangle()
		{

		}

		public Rectangle(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public override string ToString()
		{
			return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
		}
	}

	internal class Square : Rectangle
	{
		//public new int Width
		//{
		//  set { base.Width = base.Height = value; }
		//}

		//public new int Height
		//{ 
		//  set { base.Width = base.Height = value; }
		//}

		public override int Width // nasty side effects
		{
			set { base.Width = base.Height = value; }
		}

		public override int Height
		{
			set { base.Width = base.Height = value; }
		}
	}


	internal static class LSP
	{
		static internal int Area(Rectangle r) => r.Width * r.Height;

		public static void Demo()
		{
			Rectangle rc = new Rectangle(2, 3);
			Console.WriteLine($"{rc} has area {Area(rc)}");
			Rectangle sq = new Square();
			sq.Width = 4;
			Console.WriteLine($"{sq} has area {Area(sq)}");
		}
	}
}
