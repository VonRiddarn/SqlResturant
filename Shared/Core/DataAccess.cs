using System;
using System.IO;

namespace Core;

public static class DataAccess
{
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
}
