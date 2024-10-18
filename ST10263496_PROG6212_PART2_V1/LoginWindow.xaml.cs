// References
// adegeo (2023). Controls - WPF .NET Framework. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/?view=netframeworkdesktop-4.8 [Accessed 18 Oct. 2024].
// dotnet-bot (2024). String.Trim Method (System). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.string.trim?view=net-6.0 [Accessed 18 Oct. 2024].
//----------------------------------------------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace ST10263496_PROG6212_PART2_V1
{
    public partial class LoginWindow : Window
    {
        // ClaimApprovalWindow object is created and stored here for approvers.
        private ClaimApprovalWindow _claimApprovalWindow;

        public LoginWindow()
        {
            InitializeComponent();

            // Initialize the ClaimApprovalWindow here so that it can be passed to the Lecturer or shown to the Approver.
            _claimApprovalWindow = new ClaimApprovalWindow();
        }

        // This method is triggered when the login button is clicked.
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the username from the UsernameTextBox.
            string username = UsernameTextBox.Text.Trim();

            // Get the password from the PasswordBox.
            string password = PasswordBox.Password.Trim();

            // Get the selected role (Lecturer, Approver, Administrator) from the RoleComboBox.
            string selectedRole = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            // Ensure all fields are filled in and role is selected.
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Please fill in all fields and select a role.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Verify the credentials based on the username, password, and role.
            if (VerifyCredentials(username, password, selectedRole))
            {
                // If the user is a Lecturer, open the MainWindow.
                if (selectedRole == "Lecturer")
                {
                    // The ClaimApprovalWindow is passed to the Lecturer's window for further use.
                    MainWindow lecturerWindow = new MainWindow(_claimApprovalWindow);
                    lecturerWindow.Show(); // Show the Lecturer's window.
                }
                // If the user is an Approver, show the ClaimApprovalWindow.
                else if (selectedRole == "Approver")
                {
                    _claimApprovalWindow.Show(); // Show the Approver's window.
                }
                // If the user is an Administrator, show the AdminWindow to view the database.
                else if (selectedRole == "Administrator")
                {
                    AdminWindow adminWindow = new AdminWindow(); // Create and show the Admin window.
                    adminWindow.Show();
                }

                // Close the login window after successful login.
                this.Close();
            }
            else
            {
                // If the credentials are invalid, display an error message.
                MessageBox.Show("Invalid credentials. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to verify the user's credentials based on the role, username, and password.
        private bool VerifyCredentials(string username, string password, string role)
        {
            // Placeholder credentials for simplicity.
            // Replace with secure authentication mechanisms (e.g., database checks, hashed passwords) for production.

            switch (role)
            {
                case "Lecturer":
                    return username == "lUser" && password == "lPass";
                case "Approver":
                    return username == "apUser" && password == "apPass";
                case "Administrator":
                    return username == "adUser" && password == "adPass";
                default:
                    return false;
            }
        }

        // Event handler for the Exit button click
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Close the application
        }
    }
}
//--------------------------------------END OF FILE------------------------------------------------------------------------------
