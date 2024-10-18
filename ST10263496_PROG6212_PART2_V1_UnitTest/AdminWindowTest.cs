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
        public void TestInitialize()
        {
            // Initialize the AdminWindow and the mock database context
            _mockContext = new Mock<ClaimDbContext>();
            _adminWindow = new AdminWindow();
        }

        [TestMethod]
        public void LoadClaims_ShouldPopulateDataGridWithClaims()
        {
            // Arrange
            var mockClaims = new List<Claim>
            {
                new Claim { Id = 1, LecturerName = "John Doe", Status = "Approved" },
                new Claim { Id = 2, LecturerName = "Jane Smith", Status = "Rejected" }
            }.AsQueryable();

            // Mock the Claims property to return the mock claims
            _mockContext.Setup(c => c.Claims).Returns(mockClaims);

            // Act
            _adminWindow.LoadClaims(); // Call to load claims

            // Assert
            var claimsDataGridItemsSource = _adminWindow.ClaimsDataGrid.ItemsSource as List<Claim>;
            Assert.IsNotNull(claimsDataGridItemsSource); // Ensure that ItemsSource is not null
            Assert.AreEqual(2, claimsDataGridItemsSource.Count); // Check that the correct number of claims is loaded
            Assert.IsTrue(claimsDataGridItemsSource.All(c => c.Status == "Approved" || c.Status == "Rejected")); // Check statuses
        }

        [TestMethod]
        public void LoadClaims_ShouldHandleEmptyClaims()
        {
            // Arrange
            var emptyClaims = new List<Claim>().AsQueryable();

            // Mock the Claims property to return empty claims
            _mockContext.Setup(c => c.Claims).Returns(emptyClaims);

            // Act
            _adminWindow.LoadClaims(); // Call to load claims

            // Assert
            var claimsDataGridItemsSource = _adminWindow.ClaimsDataGrid.ItemsSource as List<Claim>;
            Assert.IsNotNull(claimsDataGridItemsSource); // Ensure that ItemsSource is not null
            Assert.AreEqual(0, claimsDataGridItemsSource.Count); // Check that no claims are loaded
        }
    }
}
