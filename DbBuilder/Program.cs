using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Core;

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

		try
		{
			string path = DataAccess.GetDatabasePath();
			string? dir = Path.GetDirectoryName(path);

			if (dir is not null && !Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			using SqliteConnection db = new($"Data source = {path}");
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
			  PriceInSEK  INT NOT NULL,
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
			  Id         	INTEGER PRIMARY KEY,
			  OrderId    	INT NOT NULL,
			  MenuItemId 	INT NOT NULL,
			  Quantity   	INT NOT NULL,
			  Name       	TEXT NOT NULL,
			  PriceInSEK 	INT NOT NULL,
			  DiscountInSEK	INT NOT NULL,
			  FOREIGN KEY (OrderId) REFERENCES "Order"(Id),
			  FOREIGN KEY (MenuItemId) REFERENCES MenuItem(Id)
			);

			-- SEED

			INSERT INTO PaymentMethod (Name)
			VALUES ('Cash'), ('Card');

			INSERT INTO MenuItem (Name, Description, PriceInSEK, Deleted, MetaItem)
			VALUES
			('Äta här', 'Kund äter mat på plats.', 15, FALSE, TRUE),
		    ('Pastafari', 'Jamaicansk pastarätt med timjan, curry och gurkmeja.', 70, FALSE, FALSE),
		    ('Pizza la vista', 'Rustik pizza med pepperonikorv och salsiccia. Perfekt för en filmkväll!', 110, FALSE, FALSE),
		    ('No more room in bell(pepper)', 'Superstark fylld paprika med ost, kött ris och sås. Inte för den svage!', 65, FALSE, FALSE),
		    ('Torsken', 'Panerad torsk med remouladsås, citron och krispiga pommes. En svensk klassiker.', 120, FALSE, FALSE),
		    ('Et tu, Brute?', 'Förrädiskt god Caesarsallad med grillad kyckling och extra krispiga krutonger.', 95, FALSE, FALSE),
		    ('Goulashnikov', 'Explosivt smakrik och kryddig ungersk gulaschsoppa. Serveras med en klick gräddfil.', 85, FALSE, FALSE),
		    ('Wok This Way', 'Krispig kycklingwok med sweet chili, cashewnötter och nudlar direkt från DMC.', 105, FALSE, FALSE),
		    ('Jurassic Pork', 'Långkokt pulled pork i en rökig BBQ-sås som smakar förhistoriskt gott.', 135, FALSE, FALSE),
		    ('Sagan om Lökringarna', 'En korg med krispiga, gyllene lökringar. Ett brödraskap av smak.', 45, FALSE, FALSE);
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
