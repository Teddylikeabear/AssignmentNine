using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using TEDDY.Models;

namespace TEDDY.Views
{
    public partial class ShoppingCartPage : ContentPage
    {
        public string UserName { get; set; } = "Teddy Marageni"; 
        public List<CartItem> CartItems { get; set; }
        public double TotalAmount { get; set; }

        public ShoppingCartPage()
        {
            InitializeComponent();

            // Set the profile name on the page
            UserNameLabel.Text = UserName;

            // Initialize cart items
            CartItems = new List<CartItem>
            {
                new CartItem { Item = new Item { Name = "Apple", Price = 1.99 }, Quantity = 2 },
                new CartItem { Item = new Item { Name = "Banana", Price = 0.99 }, Quantity = 5 }
            };

            // Bind the cart items to the CollectionView
            CartItemsCollectionView.ItemsSource = CartItems;

            // Update the total amount
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = CartItems.Sum(item => item.Item.Price * item.Quantity);
            TotalLabel.Text = $"Total: R{TotalAmount:F2}";
        }

        private void OnBackToShoppingListClicked(object sender, EventArgs e)
        {
            // Navigate back to the shopping list
            Navigation.PopAsync();
        }

        private void OnAddOneMoreClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is CartItem cartItem)
            {
                cartItem.Quantity++;
                UpdateTotalAmount(); // Recalculate total
            }
        }

        private void OnRemoveFromCartClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is CartItem cartItem)
            {
                CartItems.Remove(cartItem);
                UpdateTotalAmount(); // Recalculate total
            }
        }
    }
}
