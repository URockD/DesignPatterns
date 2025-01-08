using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder
{
	public enum CarType
	{
		Sedan,
		Crossover
	};
	public class Car
	{
		public CarType Type;
		public int WheelSize;
	}

	public interface ISpecifyCarType
	{
		public ISpecifyWheelSize OfType(CarType type);
	}

	public interface ISpecifyWheelSize
	{
		public IBuildCar WithWheels(int size);
	}

	public interface IBuildCar
	{
		public Car Build();
	}

	public class CarBuilder
	{
		public static ISpecifyCarType Create()
		{
			return new Impl();
		}

		private class Impl :
		  ISpecifyCarType,
		  ISpecifyWheelSize,
		  IBuildCar
		{
			private Car car = new Car();

			public ISpecifyWheelSize OfType(CarType type)
			{
				car.Type = type;
				return this;
			}

			public IBuildCar WithWheels(int size)
			{
				switch (car.Type)
				{
					case CarType.Crossover when size < 17 || size > 20:
					case CarType.Sedan when size < 15 || size > 17:
						throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
				}
				car.WheelSize = size;
				return this;
			}

			public Car Build()
			{
				return car;
			}
		}
	}

	internal static class Stepwise
	{
		public static void Run()
		{
			var car = CarBuilder.Create()
				.OfType(CarType.Sedan)
				.WithWheels(16)
				.Build();
			Console.WriteLine($"Car type: {car.Type}, wheel size: {car.WheelSize}");
		}
	}
}
