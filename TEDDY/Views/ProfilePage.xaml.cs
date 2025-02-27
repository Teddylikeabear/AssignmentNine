using TEDDY.Database;
using TEDDY.Models;
using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Storage;


namespace TEDDY.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly AppDatabase _database = AppDatabase.Instance; // Singleton instance

        public ProfilePage()
        {
            InitializeComponent();
            LoadProfile();
        }

        private async void LoadProfile()
        {
            var profile = await _database.GetProfileAsync();
            if (profile != null)
            {
                nameEntry.Text = profile.Name;
                surnameEntry.Text = profile.Surname;
                emailEntry.Text = profile.Email;
                bioEditor.Text = profile.Bio;
            }
        }

        private async void OnAddPictureClicked(object sender, EventArgs e)
        {
            // Handle the image picker here
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select Profile Picture"
                });

                if (result != null)
                {
                    ProfileImage.Source = ImageSource.FromFile(result.FullPath);
                    // You could also save the image path or upload it to a server if needed
                }
            }
            catch (Exception ex)
            {
                // Handle any errors during image picking
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var profile = new Profile
            {
                Name = nameEntry.Text,
                Surname = surnameEntry.Text,
                Email = emailEntry.Text,
                Bio = bioEditor.Text
            };

            await _database.SaveProfileAsync(profile);
            await DisplayAlert("Success", "Profile saved!", "OK");
        }
    }
}
