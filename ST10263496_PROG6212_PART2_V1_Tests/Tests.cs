using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ST10263496_PROG6212_PART2_V1;
using ST10263496_PROG6212_PART2_V1.Models;
using ST10263496_PROG6212_PART2_V1.Data;

namespace ST10263496_PROG6212_PART2_V1.Tests
{
    [TestClass]
    public class LoginWindowTests
    {
        // Test to verify Lecturer login opens the correct window.
        [TestMethod]
        public void Login_Lecturer_ValidCredentials_OpensMainWindow()
        {
            // Arrange
            var loginWindow = new LoginWindow();
            var username = "lUser";
            var password = "lPass";
            var role = "Lecturer";

            // Act
            var result = loginWindow.VerifyCredentials(username, password, role);

            // Assert
            Assert.IsTrue(result, "Lecturer credentials should be valid.");
        }

        // Test for invalid credentials
        [TestMethod]
        public void Login_InvalidCredentials_ShowsErrorMessage()
        {
            // Arrange
            var loginWindow = new LoginWindow();
            var username = "invalidUser";
            var password = "invalidPass";
            var role = "Lecturer";

            // Act
            var result = loginWindow.VerifyCredentials(username, password, role);

            // Assert
            Assert.IsFalse(result, "Invalid credentials should not be valid.");
        }

        // Test for Approver role
        [TestMethod]
        public void Login_Approver_ValidCredentials_OpensClaimApprovalWindow()
        {
            // Arrange
            var loginWindow = new LoginWindow();
            var username = "apUser";
            var password = "apPass";
            var role = "Approver";

            // Act
            var result = loginWindow.VerifyCredentials(username, password, role);

            // Assert
            Assert.IsTrue(result, "Approver credentials should be valid.");
        }

        // Test for Admin role
        [TestMethod]
        public void Login_Admin_ValidCredentials_OpensAdminWindow()
        {
            // Arrange
            var loginWindow = new LoginWindow();
            var username = "adUser";
            var password = "adPass";
            var role = "Administrator";

            // Act
            var result = loginWindow.VerifyCredentials(username, password, role);

            // Assert
            Assert.IsTrue(result, "Administrator credentials should be valid.");
        }
    }

    [TestClass]
    public class ClaimApprovalWindowTests
    {
        private Mock<ClaimDbContext> _mockContext;
        private List<Claim> _mockClaimData;

        // Setup mock data for each test
        [TestInitialize]
        public void Setup()
        {
            // Initialize mock claims data
            _mockClaimData = new List<Claim>
            {
                new Claim { LecturerName = "John Doe", Status = "Pending" },
                new Claim { LecturerName = "Jane Smith", Status = "Pending" }
            };

            // Create a mock DbSet using Moq
            var mockClaimDbSet = _mockClaimData.AsQueryable().BuildMockDbSet();

            // Create a mock context
            _mockContext = new Mock<ClaimDbContext>();
            _mockContext.Setup(c => c.Claims).Returns(mockClaimDbSet.Object);
        }

        // Test for loading claims from the database
        [TestMethod]
        public void LoadClaims_ShouldLoadPendingClaims()
        {
            // Arrange
            var claimApprovalWindow = new ClaimApprovalWindow();

            // Act
            claimApprovalWindow.LoadClaims();

            // Assert
            Assert.AreEqual(2, claimApprovalWindow.Claims.Count); // Assuming 2 claims are loaded
        }

        // Test for approving a claim
        [TestMethod]
        public void ApproveClaim_ChangesClaimStatusToApproved()
        {
            // Arrange
            var claimApprovalWindow = new ClaimApprovalWindow();
            var claimToApprove = _mockClaimData[0]; // Selecting first claim

            // Act
            claimApprovalWindow.ApproveClaimButton_Click(claimToApprove);

            // Assert
            Assert.AreEqual("Approved", claimToApprove.Status, "Claim status should be updated to Approved.");
        }

        // Test for rejecting a claim
        [TestMethod]
        public void RejectClaim_ChangesClaimStatusToRejected()
        {
            // Arrange
            var claimApprovalWindow = new ClaimApprovalWindow();
            var claimToReject = _mockClaimData[1]; // Selecting second claim

            // Act
            claimApprovalWindow.RejectClaimButton_Click(claimToReject);

            // Assert
            Assert.AreEqual("Rejected", claimToReject.Status, "Claim status should be updated to Rejected.");
        }

        // Test for adding a new claim
        [TestMethod]
        public void AddNewClaim_ShouldAddClaimToCollection()
        {
            // Arrange
            var claimApprovalWindow = new ClaimApprovalWindow();
            var newClaim = new Claim { LecturerName = "New Lecturer", Status = "Pending" };

            // Act
            claimApprovalWindow.AddNewClaim(newClaim);

            // Assert
            Assert.IsTrue(claimApprovalWindow.Claims.Contains(newClaim), "New claim should be added to the list.");
        }
    }

    [TestClass]
    public class AdminWindowTests
    {
        private Mock<ClaimDbContext> _mockContext;
        private List<Claim> _mockClaimData;

        // Setup mock data for each test
        [TestInitialize]
        public void Setup()
        {
            // Initialize mock claims data
            _mockClaimData = new List<Claim>
            {
                new Claim { LecturerName = "John Doe", Status = "Approved" },
                new Claim { LecturerName = "Jane Smith", Status = "Rejected" }
            };

            // Create a mock DbSet using Moq
            var mockClaimDbSet = _mockClaimData.AsQueryable().BuildMockDbSet();

            // Create a mock context
            _mockContext = new Mock<ClaimDbContext>();
            _mockContext.Setup(c => c.Claims).Returns(mockClaimDbSet.Object);
        }

        // Test for loading claims from the database
        [TestMethod]
        public void LoadClaims_ShouldLoadAllClaims()
        {
            // Arrange
            var adminWindow = new AdminWindow();

            // Act
            adminWindow.LoadClaims();

            // Assert
            Assert.AreEqual(2, adminWindow.Claims.Count, "All claims should be loaded from the database.");
        }

        // Test for refreshing claims data
        [TestMethod]
        public void RefreshButton_Click_ShouldReloadClaims()
        {
            // Arrange
            var adminWindow = new AdminWindow();

            // Act
            adminWindow.RefreshButton_Click(null, null); // Simulate button click

            // Assert
            _mockContext.Verify(c => c.Claims.ToList(), Times.Once, "Claims should be reloaded from the database.");
        }
    }
}
