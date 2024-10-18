using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ST10263496_PROG6212_PART2_V1.Data;
using ST10263496_PROG6212_PART2_V1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ST10263496_PROG6212_PART2_V1.Tests
{
    [TestClass]
    public class AdminWindowTests
    {
        private AdminWindow _adminWindow;
        private Mock<ClaimDbContext> _mockContext;

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<ClaimDbContext>();
            _adminWindow = new AdminWindow();
        }

        [TestMethod]
        public void LoadClaims_ShouldLoadClaimsIntoDataGrid()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim { Id = 1, LecturerName = "Lecturer A", Status = "Approved" },
                new Claim { Id = 2, LecturerName = "Lecturer B", Status = "Rejected" }
            };
            _mockContext.Setup(c => c.Claims).Returns(claims.AsQueryable());

            // Act
            _adminWindow.LoadClaims();

            // Assert
            Assert.AreEqual(2, _adminWindow.ClaimsDataGrid.ItemsSource.Cast<Claim>().Count());
        }

        [TestMethod]
        public void RefreshButton_Click_ShouldReloadClaims()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim { Id = 1, LecturerName = "Lecturer A", Status = "Approved" }
            };
            _mockContext.Setup(c => c.Claims).Returns(claims.AsQueryable());
            _adminWindow.LoadClaims(); // Load initial claims

            // Act
            _adminWindow.RefreshButton_Click(null, null);

            // Assert
            Assert.AreEqual(1, _adminWindow.ClaimsDataGrid.ItemsSource.Cast<Claim>().Count()); // Check if claims are refreshed
        }

        [TestMethod]
        public void CloseButton_Click_ShouldCloseWindow()
        {
            // Act
            _adminWindow.CloseButton_Click(null, null);

            // Assert
            // Verify that the window closes (this might require integration testing in a real environment)
        }

        [TestMethod]
        public void SignOutButton_Click_ShouldCloseAdminWindowAndOpenLoginWindow()
        {
            // Act
            _adminWindow.SignOutButton_Click(null, null);

            // Assert
            // Check if the login window is opened (you may need a way to verify this)
        }
    }
}
