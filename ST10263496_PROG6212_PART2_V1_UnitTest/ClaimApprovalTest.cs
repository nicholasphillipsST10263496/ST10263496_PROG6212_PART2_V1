using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ST10263496_PROG6212_PART2_V1.Data;
using ST10263496_PROG6212_PART2_V1.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ST10263496_PROG6212_PART2_V1.Tests
{
    [TestClass]
    public class ClaimApprovalWindowTests
    {
        private ClaimApprovalWindow _claimApprovalWindow;
        private Mock<ClaimDbContext> _mockContext;
        private ObservableCollection<Claim> _mockClaims;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize the ClaimApprovalWindow and the mock database context
            _mockContext = new Mock<ClaimDbContext>();
            _mockClaims = new ObservableCollection<Claim>
            {
                new Claim { Id = 1, LecturerName = "John Doe", Status = "Pending" },
                new Claim { Id = 2, LecturerName = "Jane Smith", Status = "Pending" }
            };

            _mockContext.Setup(c => c.Claims).Returns(_mockClaims.AsQueryable());
            _claimApprovalWindow = new ClaimApprovalWindow
            {
                // Injecting mock context if needed or can be changed later
            };

            // This simulates the loading of claims from the database
            _claimApprovalWindow.AddNewClaim(_mockClaims[0]);
        }

        [TestMethod]
        public void TestLoadClaims_ShouldLoadPendingClaims()
        {
            // Act
            _claimApprovalWindow.LoadClaims(); // Call to load claims

            // Assert
            Assert.AreEqual(2, _claimApprovalWindow.Claims.Count); // Check that two claims are loaded
            Assert.IsTrue(_claimApprovalWindow.Claims.All(c => c.Status == "Pending")); // All claims should be pending
        }

        [TestMethod]
        public void TestApproveClaim_ShouldUpdateClaimStatus()
        {
            // Arrange
            var claimToApprove = new Claim { Id = 1, LecturerName = "John Doe", Status = "Pending" };
            _claimApprovalWindow.AddNewClaim(claimToApprove); // Add claim to the UI

            // Act
            _claimApprovalWindow.ApproveClaimButton_Click(null, null); // Approve the claim

            // Assert
            Assert.AreEqual("Approved", claimToApprove.Status); // Check that the status is updated
            Assert.IsFalse(_claimApprovalWindow.Claims.Contains(claimToApprove)); // Check that the claim is removed from the UI
        }

        [TestMethod]
        public void TestRejectClaim_ShouldUpdateClaimStatus()
        {
            // Arrange
            var claimToReject = new Claim { Id = 2, LecturerName = "Jane Smith", Status = "Pending" };
            _claimApprovalWindow.AddNewClaim(claimToReject); // Add claim to the UI

            // Act
            _claimApprovalWindow.RejectClaimButton_Click(null, null); // Reject the claim

            // Assert
            Assert.AreEqual("Rejected", claimToReject.Status); // Check that the status is updated
            Assert.IsFalse(_claimApprovalWindow.Claims.Contains(claimToReject)); // Check that the claim is removed from the UI
        }

        [TestMethod]
        public void TestAddNewClaim_ShouldAddClaimToList()
        {
            // Arrange
            var newClaim = new Claim { Id = 3, LecturerName = "Alice Johnson", Status = "Pending" };

            // Act
            _claimApprovalWindow.AddNewClaim(newClaim); // Add new claim

            // Assert
            Assert.IsTrue(_claimApprovalWindow.Claims.Contains(newClaim)); // Check that the claim is in the list
        }
    }
}
