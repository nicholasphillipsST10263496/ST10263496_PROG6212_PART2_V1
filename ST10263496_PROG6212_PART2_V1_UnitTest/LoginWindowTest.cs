using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using System.Windows;

namespace ST10263496_PROG6212_PART2_V1.Tests
{
    [TestClass]
    public class LoginWindowTests
    {
        private LoginWindow _loginWindow;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize the LoginWindow before each test
            _loginWindow = new LoginWindow();
        }

        [TestMethod]
        public void TestLoginButton_Click_ValidLecturerCredentials_ShouldOpenMainWindow()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = "lUser"; // Valid Lecturer username
            _loginWindow.PasswordBox.Password = "lPass"; // Valid Lecturer password
            _loginWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Lecturer" };

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            Assert.IsInstanceOfType(Application.Current.Windows[1], typeof(MainWindow)); // MainWindow should be opened
        }

        [TestMethod]
        public void TestLoginButton_Click_ValidApproverCredentials_ShouldOpenClaimApprovalWindow()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = "apUser"; // Valid Approver username
            _loginWindow.PasswordBox.Password = "apPass"; // Valid Approver password
            _loginWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Approver" };

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            Assert.IsTrue(Application.Current.Windows.OfType<ClaimApprovalWindow>().Any()); // ClaimApprovalWindow should be opened
        }

        [TestMethod]
        public void TestLoginButton_Click_ValidAdminCredentials_ShouldOpenAdminWindow()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = "adUser"; // Valid Admin username
            _loginWindow.PasswordBox.Password = "adPass"; // Valid Admin password
            _loginWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Administrator" };

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            Assert.IsInstanceOfType(Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault(), typeof(AdminWindow)); // AdminWindow should be opened
        }

        [TestMethod]
        public void TestLoginButton_Click_InvalidCredentials_ShouldShowErrorMessage()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = "invalidUser"; // Invalid username
            _loginWindow.PasswordBox.Password = "invalidPass"; // Invalid password
            _loginWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Lecturer" };

            bool messageShown = false;
            MessageBox.Show = (msg) =>
            {
                messageShown = true; // Track if the message box was shown
                return string.Empty;
            };

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            Assert.IsTrue(messageShown); // An error message should be shown
        }
    }
}
