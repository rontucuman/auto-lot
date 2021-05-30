using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AutoLot.Dal.BulkImport;
using AutoLot.Dal.DataOperations;
using AutoLot.Dal.Models;

namespace AutoLot.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      DoBulkCopy();
      //FlagCustomer();

      InventoryDal dal = new();
      List<CarViewModel> list = dal.GetAllInventory();

      Console.WriteLine(" ************** All Cars ************** ");
      Console.WriteLine("Id\tMake\tColor\tPet Name");

      foreach (CarViewModel carViewModel in list)
      {
        Console.WriteLine($"{carViewModel.Id}\t{carViewModel.Make}\t{carViewModel.Color}\t{carViewModel.PetName}");
      }

      Console.WriteLine();

      CarViewModel car = dal.GetCar(list.OrderBy(x => x.Color).Select(x => x.Id).First());

      Console.WriteLine(" ************** First Car By Color ************** ");
      Console.WriteLine("CarId\tMake\tColor\tPet Name");
      Console.WriteLine($"{car.Id}\t{car.Make}\t{car.Color}\t{car.PetName}");

      try
      {
        dal.DeleteCar(5);
        Console.WriteLine("Car deleted.");
      }
      catch (Exception e)
      {
        Console.WriteLine($"An exception occured: {e.Message}");
      }

      dal.InsertAuto("Blue", 5, "TowMonster");

      list = dal.GetAllInventory();

      CarViewModel newCar = list.First(x => x.PetName == "TowMonster");

      Console.WriteLine(" ************** New Car ************** ");
      Console.WriteLine("CarId\tMake\tColor\tPet Name");
      Console.WriteLine($"{newCar.Id}\t{newCar.Make}\t{newCar.Color}\t{newCar.PetName}");

      dal.DeleteCar(newCar.Id);

      string petName = dal.LookUpPetName(car.Id);

      Console.WriteLine(" ************** New Car ************** ");

      Console.WriteLine($"Car pet name: {petName}");
      Console.WriteLine("Press enter to continue...");
      Console.ReadLine();
    }

    public static void FlagCustomer()
    {
      Console.WriteLine("***** Simple Transaction Example *****\n");

      bool throwEx = true;

      Console.Write("Do you want to throw an exception (Y or N): ");
      var userAnswer = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(userAnswer) || userAnswer.Equals("N", StringComparison.OrdinalIgnoreCase))
      {
        throwEx = false;
      }

      var dal = new InventoryDal();
      dal.ProcessCreditRisk(throwEx, 1);

      Console.WriteLine("Check CreditRisk table for results");
      Console.ReadLine();
    }

    public static void DoBulkCopy()
    {
      Console.WriteLine(" ************** Do Bulk Copy ************** ");
      List<Car> cars = new()
      {
        new Car {Color = "Blue", MakeId = 1, PetName = "MyCar1"},
        new Car {Color = "Red", MakeId = 2, PetName = "MyCar2"},
        new Car {Color = "White", MakeId = 3, PetName = "MyCar3"},
        new Car {Color = "Yellow", MakeId = 4, PetName = "MyCar4"}
      };

      ProcessBulkImport.ExecuteBulkImport(cars, "Inventory");
      InventoryDal dal = new();

      List<CarViewModel> list = dal.GetAllInventory();

      Console.WriteLine(" ************** All Cars ************** ");
      Console.WriteLine("CarId\tMake\tColor\tPet Name");

      foreach (CarViewModel car in list)
      {
        Console.WriteLine($"{car.Id}\t{car.Make}\t{car.Color}\t{car.PetName}");
      }

      Console.WriteLine();
    }
  }
}
