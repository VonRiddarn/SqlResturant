https://trangius.se/sql_visualizer/

```sql
MenuItem
  Id
  Name text
  Description text
  Price int
  Deleted bool
  MetaItem bool

Order
  Id
  CreatedAt date
  OrderNumber int
  PaymentMethodId -> PaymentMethod.Id
  IsPaid bool

# This can be an enum in C#.
PaymentMethod
  Id
  Name text

# We re-store some meta about the orderitem so that if it changes, legacy orders still show the correct stuff. Keeps Skatteverket happy.
# We could also add an "AtPurchase" suffix to be clear, but this looks clean enough.
OrderItem
  Id
  OrderId -> Order.Id
  MenuItemId -> MenuItem.Id
  Quantity int
  Name text
  Price int
  Discount int
```
