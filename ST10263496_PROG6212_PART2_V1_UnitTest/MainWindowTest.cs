using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10263496_PROG6212_PART2_V1;
using ST10263496_PROG6212_PART2_V1.Models;
using System.Windows;

namespace ST10263496_PROG6212_PART2_V1.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        private MainWindow _mainWindow;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create a ClaimApprovalWindow mock (or use a real instance if available)
            var claimApprovalWindow = new ClaimApprovalWindow(); 
            _mainWindow = new MainWindow(claimApprovalWindow);
        }

        [TestMethod]
        public void TestSetPlaceholderVisibility_WhenTextIsEmpty_ShouldShowPlaceholder()
        {
            // Arrange
            _mainWindow.LecturerNameTextBox.Clear();

            // Act
            _mainWindow.SetPlaceholderVisibility();

            // Assert
            Assert.AreEqual(Visibility.Visible, _mainWindow.LecturerNamePlaceholder.Visibility);
        }

        [TestMethod]
        public void TestSubmitClaimButton_Click_WhenFieldsAreEmpty_ShouldShowMessage()
        {
            // Arrange
            _mainWindow.LecturerNameTextBox.Clear();
            _mainWindow.HoursWorkedTextBox.Clear();
            _mainWindow.HourlyRateTextBox.Clear();
            _mainWindow.DocumentPathTextBox.Clear();

            // Act
            var messageBoxShown = false;
            MessageBox.Show = (msg) => { messageBoxShown = true; return string.Empty; };
            _mainWindow.SubmitClaimButton_Click(null, null);

            // Assert
            Assert.IsTrue(messageBoxShown);
        }

        [TestMethod]
        public void TestUploadDocumentButton_Click_ShouldSetDocumentPathTextBox()
        {
            // Arrange
            string testFilePath = @"C:\Test\document.pdf"; // Update the path for your test case
            var openFileDialogMock = new Mock<OpenFileDialog>();
            openFileDialogMock.Setup(dialog => dialog.ShowDialog()).Returns(true);
            openFileDialogMock.Setup(dialog => dialog.FileName).Returns(testFilePath);
            
            // Replace the OpenFileDialog with a mock or fake in your MainWindow

            // Act
            _mainWindow.UploadDocumentButton_Click(null, null);

            // Assert
            Assert.AreEqual(testFilePath, _mainWindow.DocumentPathTextBox.Text);
        }
    }
}
