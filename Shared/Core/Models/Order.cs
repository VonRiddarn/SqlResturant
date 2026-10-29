namespace Core.Models;

public sealed class Order
{
	public int Id { get; set; } = -1;
	public DateTime Date { get; set; }
	public int OrderNumber { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public bool IsPaid { get; set; }

	/// From DB to object
	public Order(int Id, string dateIso8601, int orderNumber, PaymentMethod paymentMethod, bool isPaid)
	{
		Id = id;
		Date = new Date(dateIso8601);
		OrderNumber = orderNumber;
		PaymentMethod = (PaymentMethod)paymentMethod;
		IsPaid = isPaid;
	}

	/// New object
	public Order(DateTime dateTime, int orderNumber, PaymentMethod paymentMethod, bool isPaid)
	{
		Date = dateTime;
		OrderNumber = orderNumber;
		PaymentMethod = paymentMethod;
		IsPaid = isPaid;
	}
}