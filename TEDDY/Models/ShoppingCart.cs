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
    }
}
