namespace Core.Models;

// We will never create menu items from within the applications as of now,
// so we just make it a record as it can ONLY be fetched from the DB.
public sealed record MenuItem(
	int Id,
	string Name,
	string Description,
	int Price,
	bool Deleted,
	bool MetaItem);