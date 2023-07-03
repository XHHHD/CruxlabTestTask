using CruxlabTeatTask.BLL.Services;
using CruxlabTeatTask.DTO.ViewModels;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CruxlabTestTask.UTests.Tests.Services
{
    [TestClass]
    public class PasswordAuthenticatorServiceTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }


        [TestCleanup]
        public void TestCleanup()
        {
        }

        [DataRow(@"Inventory\PasswordAuthenticatorServiceInventory.txt", 3, 2)]
        [TestMethod]
        public void AuthenticateFile_ShouldReturnRightResult(string filePathInProject, int checksCount, int passedPasswords)
        {
            //ARRANGE
            var expectedResult = new PasswordAuthenticationResponseVM()
            {
                ChecksCount = checksCount,
                PassedPasswords = passedPasswords,
            };
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fullPath = Path.Combine(projectDirectory, filePathInProject);

            //ACT
            var result = PasswordAuthenticatorService.AuthenticateFile(fullPath);

            //ASSERT
            Assert.AreEqual(expectedResult.ChecksCount, result.ChecksCount);
            Assert.AreEqual(expectedResult.PassedPasswords, result.PassedPasswords);
        }
    }
}