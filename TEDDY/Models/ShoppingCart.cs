using SQLite;

namespace TEDDY.Models
{
    public class ShoppingCart
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ProfileId { get; set; } // Link to Profile
        public int ItemId { get; set; } // Link to ShoppingItem
        public int Quantity { get; set; }
        [Ignore]
        public ShoppingItem Item { get; set; } // To load associated ShoppingItem details
    }
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class CartItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }

}
