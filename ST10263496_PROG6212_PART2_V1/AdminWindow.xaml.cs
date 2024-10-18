// References
// dotnet-bot (2024). DbContext Class (Microsoft.EntityFrameworkCore). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-6.0 [Accessed 18 Oct. 2024].
// adegeo (2023). Data binding overview - WPF .NET Framework. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/data-binding-overview?view=netframeworkdesktop-4.8 [Accessed 18 Oct. 2024].
// 
using System.Windows;

namespace ST10263496_PROG6212_PART2_V1
{
    public partial class AdminWindow : Window
    {
        // Constructor for the AdminWindow
        public AdminWindow()
        {
            InitializeComponent();
            LoadClaims(); // Load claims data when the window is initialized
        }

        // Method to load all claims from the database and display them in the DataGrid
        private void LoadClaims()
        {
            using (var context = new Data.ClaimDbContext())
            {
                var claimsList = context.Claims.ToList();
                ClaimsDataGrid.ItemsSource = claimsList;
            }
        }

        // Event handler for refreshing the data manually (optional)
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadClaims(); // Reload claims when the refresh button is clicked
            MessageBox.Show("Claim data refreshed.", "Data Refreshed", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event handler for closing the window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the Admin window
        }

        // Event handler for signing out
        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();

            // Open the login window (assuming it is named LoginWindow)
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
// ---------------------------------------------------------END OF FILE-------------------------------------------------------------------