namespace Core.Models;

public sealed class OrderItem
{
	public int Id { get; set; } = -1;
	public int OrderId { get; set; }
	public int MenuItemId { get; set; }
	public int Quantity { get; set; }
	public string Name { get; set; }
	public int Price { get; set; }
	public int Discount { get; set; }

	/// From DB to object
	public OrderItem(
		int id,
		int orderId,
		int menuItemId,
		int quantity,
		string name,
		int price,
		int discount)
	{
		Id = id;
		OrderId = orderId;
		MenuItemId = menuItemId;
		Quantity = quantity;
		Name = name;
		Price = price;
		Discount = discount;
	}

	/// Create new object in app
	public OrderItem(int menuItemId, int quantity, string name, int price, int discount)
	{
		MenuItemId = menuItemId;
		Quantity = quantity;
		Name = name;
		Price = price;
		Discount = discount;
	}
}