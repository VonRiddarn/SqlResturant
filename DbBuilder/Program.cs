using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace DbBuilder;

/*
  	NOTE: 
  		This program nukes the database completely and resets all the tables.
		That means any data already present is removed forever.		 
*/

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Initializing database...");

		const string DB_PATH = "../Shared/DataBase/database.db";

		try
		{
			string? dir = Path.GetDirectoryName(DB_PATH);

			if (dir is not null && !Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			using SqliteConnection db = new($"Data source = {DB_PATH}");
			db.Open();

			var cmd = db.CreateCommand();

			cmd.CommandText = """
			-- SQLite
			PRAGMA foreign_keys = ON;

			DROP TABLE IF EXISTS OrderItem;
			DROP TABLE IF EXISTS "Order";
			DROP TABLE IF EXISTS PaymentMethod;
			DROP TABLE IF EXISTS MenuItem;

			CREATE TABLE MenuItem (
			  Id          INTEGER PRIMARY KEY,
			  Name        TEXT NOT NULL,
			  Description TEXT NOT NULL,
			  Price       INT NOT NULL,
			  Deleted     BOOL NOT NULL,
			  MetaItem    BOOL NOT NULL
			);

			CREATE TABLE PaymentMethod (
			  Id   INTEGER PRIMARY KEY,
			  Name TEXT NOT NULL
			);

			CREATE TABLE "Order" (
			  Id              INTEGER PRIMARY KEY,
			  CreatedAt       DATE NOT NULL,
			  OrderNumber     INT NOT NULL,
			  PaymentMethodId INT NOT NULL,
			  IsPaid          BOOL NOT NULL,
			  FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethod(Id)
			);

			CREATE TABLE OrderItem (
			  Id         INTEGER PRIMARY KEY,
			  OrderId    INT NOT NULL,
			  MenuItemId INT NOT NULL,
			  Quantity   INT NOT NULL,
			  Name       TEXT NOT NULL,
			  Price      INT NOT NULL,
			  Discount   INT NOT NULL,
			  FOREIGN KEY (OrderId) REFERENCES "Order"(Id),
			  FOREIGN KEY (MenuItemId) REFERENCES MenuItem(Id)
			);
		""";

			cmd.ExecuteNonQuery();

			Console.WriteLine("Success!");
			Environment.Exit(0);
		}
		catch (Exception e)
		{
			Console.WriteLine($"Failed: {e.Message}");
			Environment.Exit(-1);
		}
	}
}
