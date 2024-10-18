// References
// adegeo (n.d.). Windows Presentation Foundation for .NET 5 documentation. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/?view=netdesktop-6.0.
// Rick-Anderson (n.d.). Getting Started - EF Core. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli.
// dotnet-bot (2024). OpenFileDialog Class (System.Windows.Forms). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-6.0 [Accessed 18 Oct. 2024].
// adegeo (2023). Data binding overview - WPF .NET. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-7.0.
//----------------------------------------------------------------------------------------------------------
using ST10263496_PROG6212_PART2_V1.Data; // Data access layer for claims
using System.Windows.Controls; // WPF controls
using System.Windows; // WPF Window class
using ST10263496_PROG6212_PART2_V1.Models; // Models for claims
using Microsoft.Win32; // OpenFileDialog for file selection
using System.IO; // File handling
using System; // Base class library

namespace ST10263496_PROG6212_PART2_V1
{
    public partial class MainWindow : Window
    {
        // Reference to the ClaimApprovalWindow
        private ClaimApprovalWindow _claimApprovalWindow;

        // Constructor for MainWindow, takes ClaimApprovalWindow as a parameter
        public MainWindow(ClaimApprovalWindow claimApprovalWindow)
        {
            InitializeComponent(); // Initializes components defined in XAML
            _claimApprovalWindow = claimApprovalWindow; // Store reference to the ClaimApprovalWindow
            SetPlaceholderVisibility(); // Set initial visibility of placeholders
        }

        // Method to set visibility of placeholders based on text box content
        private void SetPlaceholderVisibility()
        {
            LecturerNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(LecturerNameTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            HoursWorkedPlaceholder.Visibility = string.IsNullOrWhiteSpace(HoursWorkedTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            HourlyRatePlaceholder.Visibility = string.IsNullOrWhiteSpace(HourlyRateTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            DocumentPathPlaceholder.Visibility = string.IsNullOrWhiteSpace(DocumentPathTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        // Event handler for submitting a claim
        private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input and get the hours worked and hourly rate
            if (!ValidateInput(out int hoursWorked, out decimal hourlyRate))
            {
                return; // Exit if validation fails
            }

            // Create a new claim object with user inputs
            var newClaim = new Claim
            {
                LecturerName = LecturerNameTextBox.Text,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                Status = "Pending", // Set default status to Pending
                DocumentPath = DocumentPathTextBox.Text
            };

            try
            {
                // Save the new claim to the database
                using (var context = new ClaimDbContext())
                {
                    context.Claims.Add(newClaim); // Add the new claim to the context
                    context.SaveChanges(); // Save changes to the database
                }

                _claimApprovalWindow.AddNewClaim(newClaim); // Update the approval window with the new claim
                MessageBox.Show("Claim submitted successfully!"); // Notify user of success
                ClearInputFields(); // Clear input fields after submission
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs
                MessageBox.Show("An error occurred while saving the claim: " + ex.Message);
            }
        }

        // Method to validate user input
        private bool ValidateInput(out int hoursWorked, out decimal hourlyRate)
        {
            hoursWorked = 0; // Initialize hours worked
            hourlyRate = 0; // Initialize hourly rate

            // Validate Lecturer Name
            if (string.IsNullOrWhiteSpace(LecturerNameTextBox.Text))
            {
                MessageBox.Show("Please enter the Lecturer's name."); // Notify user
                LecturerNameTextBox.BorderBrush = System.Windows.Media.Brushes.Red; // Highlight text box
                return false; // Return false to indicate validation failure
            }

            // Validate Hours Worked
            if (!int.TryParse(HoursWorkedTextBox.Text, out hoursWorked) || hoursWorked <= 0 || hoursWorked > 24)
            {
                MessageBox.Show("Please enter a valid number of hours worked (1-24).");
                HoursWorkedTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                return false;
            }

            // Validate Hourly Rate
            if (!decimal.TryParse(HourlyRateTextBox.Text, out hourlyRate) || hourlyRate <= 0)
            {
                MessageBox.Show("Please enter a valid hourly rate (positive decimal).");
                HourlyRateTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                return false;
            }

            // Validate Document Path
            if (string.IsNullOrWhiteSpace(DocumentPathTextBox.Text) || !File.Exists(DocumentPathTextBox.Text))
            {
                MessageBox.Show("Please select a valid document file.");
                DocumentPathTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                return false;
            }

            // Reset border colors to indicate valid input
            LecturerNameTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;
            HoursWorkedTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;
            HourlyRateTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;
            DocumentPathTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;

            return true; // Return true if all validations pass
        }

        // Method to clear input fields
        private void ClearInputFields()
        {
            LecturerNameTextBox.Clear(); // Clear Lecturer Name
            HoursWorkedTextBox.Clear(); // Clear Hours Worked
            HourlyRateTextBox.Clear(); // Clear Hourly Rate
            DocumentPathTextBox.Clear(); // Clear Document Path
            SetPlaceholderVisibility(); // Reset placeholder visibility
        }

        // Event handlers for text changed events
        private void LecturerNameTextBox_TextChanged(object sender, TextChangedEventArgs e) => SetPlaceholderVisibility();
        private void HoursWorkedTextBox_TextChanged(object sender, TextChangedEventArgs e) => SetPlaceholderVisibility();
        private void HourlyRateTextBox_TextChanged(object sender, TextChangedEventArgs e) => SetPlaceholderVisibility();
        private void DocumentPathTextBox_TextChanged(object sender, TextChangedEventArgs e) => SetPlaceholderVisibility();

        // Event handler for document upload button click
        private void UploadDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Document", // Dialog title
                Filter = "All Files (*.*)|*.*" // File filter
            };

            // Show the dialog and check if the user selects a file
            if (openFileDialog.ShowDialog() == true)
            {
                DocumentPathTextBox.Text = openFileDialog.FileName; // Set the selected file path
            }
        }

        // Event handler for sign out button click
        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();

            // Open the LoginWindow
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show(); // Display the login window
        }
    }
}
//---------------------------------------------------------END OF FILE--------------------------------------------------------------//