using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10263496_PROG6212_PART2_V1;
using System.Windows.Controls;

namespace ST10263496_PROG6212_PART2_V1.Tests
{
    [TestClass]
    public class LoginWindowTests
    {
        private LoginWindow _loginWindow;

        [TestInitialize]
        public void Setup()
        {
            _loginWindow = new LoginWindow();
        }

        [TestMethod]
        public void LoginButton_Click_EmptyFields_ShouldShowErrorMessage()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = ""; // Empty username
            _loginWindow.PasswordBox.Password = ""; // Empty password
            _loginWindow.RoleComboBox.SelectedItem = null; // No role selected

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            Assert.AreEqual(1, MessageBoxHelper.ShowMessageBoxCount);
            Assert.AreEqual("Please fill in all fields and select a role.", MessageBoxHelper.LastMessageBoxText);
        }

        [TestMethod]
        public void LoginButton_Click_InvalidCredentials_ShouldShowErrorMessage()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = "invalidUser";
            _loginWindow.PasswordBox.Password = "invalidPass";
            _loginWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Lecturer" };

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            Assert.AreEqual(1, MessageBoxHelper.ShowMessageBoxCount);
            Assert.AreEqual("Invalid credentials. Please try again.", MessageBoxHelper.LastMessageBoxText);
        }

        [TestMethod]
        public void LoginButton_Click_ValidLecturerCredentials_ShouldOpenMainWindow()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = "lUser";
            _loginWindow.PasswordBox.Password = "lPass";
            _loginWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Lecturer" };

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            // Check if the MainWindow was opened (you may need a way to verify that)
            Assert.IsTrue(_loginWindow.IsClosed);
        }

        [TestMethod]
        public void LoginButton_Click_ValidApproverCredentials_ShouldOpenClaimApprovalWindow()
        {
            // Arrange
            _loginWindow.UsernameTextBox.Text = "apUser";
            _loginWindow.PasswordBox.Password = "apPass";
            _loginWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Approver" };

            // Act
            _loginWindow.LoginButton_Click(null, null);

            // Assert
            Assert.IsTrue(_loginWindow.IsClosed);
            // Additional checks to confirm the ClaimApprovalWindow is opened
        }

        [TestMethod]
        public void VerifyCredentials_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            string username = "lUser";
            string password = "lPass";
            string role = "Lecturer";

            // Act
            bool result = _loginWindow.VerifyCredentials(username, password, role);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyCredentials_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            string username = "invalidUser";
            string password = "invalidPass";
            string role = "Lecturer";

            // Act
            bool result = _loginWindow.VerifyCredentials(username, password, role);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
