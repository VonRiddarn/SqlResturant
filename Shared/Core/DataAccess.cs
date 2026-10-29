using System;
using System.Collections.Generic;
using System.IO;
using Core.Models;
using Microsoft.Data.Sqlite;

namespace Core;

public static class DataAccess
{
	static readonly string _source = $"Data source = {GetDatabasePath()}";

	public static string GetDatabasePath()
	{
		// Get the current executables directory path, ie: /bin/Debug/Net10.0
		string? currentDir = AppContext.BaseDirectory;

		// Traverse upwards until we hit the sln file or until we reach outisde of the path.
		while (currentDir != null && Directory.GetFiles(currentDir, "*.sln*").Length == 0)
			currentDir = Directory.GetParent(currentDir)?.FullName;

		if (currentDir == null)
			throw new Exception("Could not find the solution root directory!");


		// Build and absolute path from the solution file.
		return Path.Combine(currentDir, "Shared", "DataBase", "database.db");
	}

	static MenuItem[] GetAllMenuItems()
	{
		using SqliteConnection db = new(_source);

		var cmd = db.CreateCommand();
		cmd.CommandText = """SELECT (Id, Name, Description, Price, Deleted, MetaItem) From "MenuItem";""";

		var reader = cmd.ExecuteReader();

		List<MenuItem> mis = [];

		while (reader.Read())
		{
			mis.Add(new(
				reader.GetInt32(0),
				reader.GetString(1),
				reader.GetString(2),
				reader.GetInt32(3),
				reader.GetBoolean(4),
				reader.GetBoolean(5)
			));
		}

		return [.. mis];
	}
}
