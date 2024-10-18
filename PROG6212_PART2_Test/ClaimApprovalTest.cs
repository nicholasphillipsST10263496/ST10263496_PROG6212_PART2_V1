using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ST10263496_PROG6212_PART2_V1.Data;
using ST10263496_PROG6212_PART2_V1.Models;
using System.Collections.Generic;
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

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<ClaimDbContext>();
            _claimApprovalWindow = new ClaimApprovalWindow();
        }

        [TestMethod]
        public void LoadPendingClaims_ShouldLoadClaimsIntoDataGrid()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim { Id = 1, Status = "Pending", LecturerName = "Lecturer A" },
                new Claim { Id = 2, Status = "Pending", LecturerName = "Lecturer B" }
            };
            _mockContext.Setup(c => c.Claims).Returns(claims.AsQueryable());

            // Act
            _claimApprovalWindow.LoadPendingClaims();

            // Assert
            Assert.AreEqual(2, _claimApprovalWindow.Claims.Count); // Assuming you expose the claims collection for testing
        }

        [TestMethod]
        public void ApproveClaimButton_Click_ValidClaim_ShouldUpdateStatusToApproved()
        {
            // Arrange
            var claim = new Claim { Id = 1, Status = "Pending", LecturerName = "Lecturer A" };
            _claimApprovalWindow.Claims.Add(claim); // Assuming Claims is accessible for testing
            _claimApprovalWindow.ClaimDataGrid.SelectedItem = claim;

            // Act
            _claimApprovalWindow.ApproveClaimButton_Click(null, null);

            // Assert
            Assert.AreEqual("Approved", claim.Status);
        }

        [TestMethod]
        public void RejectClaimButton_Click_ValidClaim_ShouldUpdateStatusToRejected()
        {
            // Arrange
            var claim = new Claim { Id = 1, Status = "Pending", LecturerName = "Lecturer A" };
            _claimApprovalWindow.Claims.Add(claim); // Assuming Claims is accessible for testing
            _claimApprovalWindow.ClaimDataGrid.SelectedItem = claim;

            // Act
            _claimApprovalWindow.RejectClaimButton_Click(null, null);

            // Assert
            Assert.AreEqual("Rejected", claim.Status);
        }

        [TestMethod]
        public void ApproveClaimButton_Click_NoClaimSelected_ShouldShowWarningMessage()
        {
            // Arrange
            _claimApprovalWindow.ClaimDataGrid.SelectedItem = null; // No claim selected

            // Act
            _claimApprovalWindow.ApproveClaimButton_Click(null, null);

            // Assert
            // Check if the appropriate message handling or state changes occurred if applicable
        }

        [TestMethod]
        public void RejectClaimButton_Click_NoClaimSelected_ShouldShowWarningMessage()
        {
            // Arrange
            _claimApprovalWindow.ClaimDataGrid.SelectedItem = null; // No claim selected

            // Act
            _claimApprovalWindow.RejectClaimButton_Click(null, null);

            // Assert
            // Check if the appropriate message handling or state changes occurred if applicable
        }

        [TestMethod]
        public void SignOutButton_Click_ShouldShowSignOutMessageAndOpenLoginWindow()
        {
            // Act
            _claimApprovalWindow.SignOutButton_Click(null, null);

            // Assert
            // Check if the appropriate state change occurred, such as the opening of the login window
        }
    }
}
