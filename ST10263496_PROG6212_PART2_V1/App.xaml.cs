// References
// dotnet-bot (2024). Application Class (System.Windows). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.windows.application?view=netframework-4.8 [Accessed 18 Oct. 2024].
//-----------------------------------------------------------------------------------------------------------------------------------
using System.Windows;
namespace ST10263496_PROG6212_PART2_V1
{
    public partial class App : Application
    {
        // Event handler for the application startup
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Initialize the database by creating it if it doesn't exist
            using (var context = new Data.ClaimDbContext())
            {
                // Ensure that the database is created when the application starts
                context.Database.EnsureCreated();
                // This checks if the database exists, and if not, it creates the necessary tables
            }

            // Open the login window first to authenticate the user
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show(); // Display the login window to the user
        }
    }
}
//--------------------------------------------------------------END OF FILE ---------------------------------------------------------------