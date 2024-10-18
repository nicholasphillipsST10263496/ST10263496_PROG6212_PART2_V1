using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10263496_PROG6212_PART2_V1;
using ST10263496_PROG6212_PART2_V1.Models;
using System.Windows.Controls;
using System.Windows;

namespace ST10263496_PROG6212_PART2_V1.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        private MainWindow _mainWindow;
        private ClaimApprovalWindow _mockClaimApprovalWindow;

        [TestInitialize]
        public void Setup()
        {
            _mockClaimApprovalWindow = new ClaimApprovalWindow();
            _mainWindow = new MainWindow(_mockClaimApprovalWindow);
        }

        [TestMethod]
        public void ValidateInput_ValidInput_ReturnsTrue()
        {
            // Arrange
            _mainWindow.LecturerNameTextBox.Text = "John Doe";
            _mainWindow.HoursWorkedTextBox.Text = "10";
            _mainWindow.HourlyRateTextBox.Text = "25.50";
            _mainWindow.DocumentPathTextBox.Text = "C:\\validpath\\document.pdf"; // Make sure this path is valid

            // Act
            bool result = _mainWindow.ValidateInput(out int hoursWorked, out decimal hourlyRate);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(10, hoursWorked);
            Assert.AreEqual(25.50m, hourlyRate);
        }

        [TestMethod]
        public void ValidateInput_InvalidLecturerName_ReturnsFalse()
        {
            // Arrange
            _mainWindow.LecturerNameTextBox.Text = "";
            _mainWindow.HoursWorkedTextBox.Text = "10";
            _mainWindow.HourlyRateTextBox.Text = "25.50";
            _mainWindow.DocumentPathTextBox.Text = "C:\\validpath\\document.pdf";

            // Act
            bool result = _mainWindow.ValidateInput(out int hoursWorked, out decimal hourlyRate);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_InvalidHoursWorked_ReturnsFalse()
        {
            // Arrange
            _mainWindow.LecturerNameTextBox.Text = "John Doe";
            _mainWindow.HoursWorkedTextBox.Text = "25"; // Invalid: out of range
            _mainWindow.HourlyRateTextBox.Text = "25.50";
            _mainWindow.DocumentPathTextBox.Text = "C:\\validpath\\document.pdf";

            // Act
            bool result = _mainWindow.ValidateInput(out int hoursWorked, out decimal hourlyRate);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SubmitClaimButton_Click_ValidInput_SavesClaim()
        {
            // Arrange
            _mainWindow.LecturerNameTextBox.Text = "John Doe";
            _mainWindow.HoursWorkedTextBox.Text = "10";
            _mainWindow.HourlyRateTextBox.Text = "25.50";
            _mainWindow.DocumentPathTextBox.Text = "C:\\validpath\\document.pdf"; // Assume this is a valid path

            // Act
            _mainWindow.SubmitClaimButton_Click(null, null);

            // Assert
            // Here you should check the database or in-memory list to ensure the claim was added
            // Depending on how ClaimDbContext is implemented, this will vary.
            // e.g., Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void ClearInputFields_ShouldClearAllFields()
        {
            // Arrange
            _mainWindow.LecturerNameTextBox.Text = "John Doe";
            _mainWindow.HoursWorkedTextBox.Text = "10";
            _mainWindow.HourlyRateTextBox.Text = "25.50";
            _mainWindow.DocumentPathTextBox.Text = "C:\\validpath\\document.pdf";

            // Act
            _mainWindow.ClearInputFields();

            // Assert
            Assert.AreEqual(string.Empty, _mainWindow.LecturerNameTextBox.Text);
            Assert.AreEqual(string.Empty, _mainWindow.HoursWorkedTextBox.Text);
            Assert.AreEqual(string.Empty, _mainWindow.HourlyRateTextBox.Text);
            Assert.AreEqual(string.Empty, _mainWindow.DocumentPathTextBox.Text);
        }
    }
}
