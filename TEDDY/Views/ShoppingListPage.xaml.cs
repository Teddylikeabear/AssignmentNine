using TEDDY.Models;
using TEDDY.Database;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace TEDDY.Views
{
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage()
        {
            InitializeComponent();
            LoadShoppingItems();
        }

        // Load shopping items with additional products
        private async void LoadShoppingItems()
        {
            // Add a few items for demonstration purposes
            var shoppingItems = new List<ShoppingItem>
            {
                new ShoppingItem { Name = "Teddy Bear", Price = 19.99, Stock = 10 },
                new ShoppingItem { Name = "Toy Car", Price = 9.99, Stock = 15 },
                new ShoppingItem { Name = "Lego Set", Price = 29.99, Stock = 5 },
                new ShoppingItem { Name = "Doll", Price = 14.99, Stock = 8 },
                new ShoppingItem { Name = "Puzzle", Price = 7.99, Stock = 20 },
                new ShoppingItem { Name = "Board Game", Price = 24.99, Stock = 12 }
            };

            // Optionally, you can save these items to the database, so they persist
            foreach (var item in shoppingItems)
            {
                await AppDatabase.Instance.SaveShoppingItemAsync(item);
            }

            ShoppingItemsCollectionView.ItemsSource = shoppingItems;
        }

        // Add selected item to the shopping cart
        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var item = (ShoppingItem)button.CommandParameter;

            var profile = await AppDatabase.Instance.GetProfileAsync();

            // Check if the user is logged in and has a profile
            if (profile != null)
            {
                var cartItem = new ShoppingCart
                {
                    ProfileId = profile.Id,
                    ItemId = item.Id,
                    Quantity = 1 // Initially add one item to the cart
                };

                await AppDatabase.Instance.AddToCartAsync(cartItem);
                item.Stock--; // Decrease stock when added to the cart
                await AppDatabase.Instance.SaveShoppingItemAsync(item);

                LoadShoppingItems(); // Refresh shopping list
            }
            else
            {
                await DisplayAlert("Error", "You must be logged in to add items to your cart.", "OK");
            }
        }

        // Navigate to the shopping cart page
        private async void OnGoToCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShoppingCartPage());
        }
    }
}
