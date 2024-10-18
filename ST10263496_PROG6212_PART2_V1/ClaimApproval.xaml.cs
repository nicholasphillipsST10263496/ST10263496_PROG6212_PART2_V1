// References
// dotnet-bot (2024). ObservableCollection Class (System.Collections.ObjectModel). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netframework-4.8 [Accessed 18 Oct. 2024].
// BillWagner (2023). Language Integrated Query (LINQ) in C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/linq/.
// dotnet-bot (2024). MessageBox Class (System.Windows). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.windows.messagebox?view=netframework-4.8 [Accessed 18 Oct. 2024].
// dotnet-bot (2024). Window Class (System.Windows). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.windows.window?view=netframework-4.8 [Accessed 18 Oct. 2024].
//--------------------------------------------------------------------------------------------------------------------------------------------------------

using ST10263496_PROG6212_PART2_V1.Data;
using ST10263496_PROG6212_PART2_V1.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ST10263496_PROG6212_PART2_V1
{
    public partial class ClaimApprovalWindow : Window
    {
        // ObservableCollection to hold the claims for data binding in the DataGrid
        private ObservableCollection<Claim> _claims;

        public ClaimApprovalWindow()
        {
            InitializeComponent();
            // Initialize the claims collection and bind it to the DataGrid
            _claims = new ObservableCollection<Claim>();
            ClaimDataGrid.ItemsSource = _claims;
            LoadPendingClaims(); // Load pending claims from the database
        }

        // Method to load pending claims from the database
        private void LoadPendingClaims()
        {
            using (var context = new ClaimDbContext())
            {
                // Query to retrieve claims with "Pending" status
                var pendingClaims = context.Claims.Where(c => c.Status == "Pending").ToList();
                foreach (var claim in pendingClaims)
                {
                    _claims.Add(claim); // Add each pending claim to the ObservableCollection
                }
            }
        }

        // Event handler for approving a claim
        private void ApproveClaimButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedClaim = ClaimDataGrid.SelectedItem as Claim; // Get the selected claim
            if (selectedClaim != null)
            {
                // Update the status to "Approved" and show a message
                UpdateClaimStatus(selectedClaim, "Approved", $"Claim approved for {selectedClaim.LecturerName}.");
            }
            else
            {
                // Show a warning if no claim is selected
                MessageBox.Show("Please select a claim to approve.", "No Claim Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Event handler for rejecting a claim
        private void RejectClaimButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedClaim = ClaimDataGrid.SelectedItem as Claim; // Get the selected claim
            if (selectedClaim != null)
            {
                // Update the status to "Rejected" and show a message
                UpdateClaimStatus(selectedClaim, "Rejected", $"Claim rejected for {selectedClaim.LecturerName}.");
            }
            else
            {
                // Show a warning if no claim is selected
                MessageBox.Show("Please select a claim to reject.", "No Claim Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Method to update the status of a claim and refresh the view
        private void UpdateClaimStatus(Claim claim, string newStatus, string message)
        {
            claim.Status = newStatus; // Update the claim status
            MessageBox.Show(message, "Claim Status Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            _claims.Remove(claim); // Remove the claim from the collection

            // Update the claim in the database
            using (var context = new ClaimDbContext())
            {
                context.Claims.Update(claim); // Update the claim in the context
                context.SaveChanges(); // Save changes to the database
            }
        }

        // Method to add a new claim to the ObservableCollection
        public void AddNewClaim(Claim newClaim)
        {
            _claims.Add(newClaim); // Add the new claim to the collection
        }

        // Event handler for the Sign Out button click
        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have been signed out.", "Sign Out", MessageBoxButton.OK, MessageBoxImage.Information);

            // Create a new instance of the LoginWindow
            var loginWindow = new LoginWindow(); // Adjust this to your actual login window class

            // Close the current window and show the login window
            this.Hide(); // Hide the ClaimApprovalWindow
            loginWindow.Show(); // Show the LoginWindow

            // Optional: Subscribe to the Closed event of the LoginWindow 
            // to show this window again when the login window is closed, if required.
            loginWindow.Closed += (s, args) => this.Show(); // Show the ClaimApprovalWindow again after logging in
        }
    }
}
//----------------------------------END OF FILE----------------------------------------------------------------------------