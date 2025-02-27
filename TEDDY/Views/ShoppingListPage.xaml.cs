using TEDDY.Database;
using TEDDY.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace TEDDY.Views
{
    public partial class ShoppingListPage : ContentPage
    {
        private readonly AppDatabase _database = AppDatabase.Instance; // Singleton instance

        public ShoppingListPage()
        {
            InitializeComponent();
            LoadShoppingItems();
        }

        private async void LoadShoppingItems()
        {
            shoppingListView.ItemsSource = await _database.GetShoppingItemsAsync();
        }

        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedItem = button.BindingContext as ShoppingItem;

            var cartItem = new ShoppingCart
            {
                ProfileId = 1, // Assuming one user for simplicity
                ItemId = selectedItem.Id,
                Quantity = 1
            };

            await _database.AddToCartAsync(cartItem);
            await DisplayAlert("Added", $"{selectedItem.Name} added to cart", "OK");
        }
    }
}
