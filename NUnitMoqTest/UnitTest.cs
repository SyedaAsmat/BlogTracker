using DALTestLayer;
using Moq;
using NUnitMoqTest;


namespace TestProject
{
    public class Tests
    {
        [TestFixture]
        public class TestClass
        {
            [Test]
            public void AddBlogTest()
            {
                //Arrange 
                var blog = new BlogInfo { Title = "Test", Subject = "TestSubject" };

                //Act
                //var blogIdTest = blogid.BlogInfoId;
                var blogTitleTest = blog.Title;
                var blogSubjTest = blog.Subject;

                //Assert
                //Assert.AreEqual(1, blogIdTest);
                Assert.AreEqual("Test", blogTitleTest);
                Assert.AreEqual("TestSubject", blogSubjTest);
            }

            [Test]
            public void AddEmpTest()
            {
                //Arrange
                var emp = new EmpInfo { Name = "Employee" };

                //Act
                var empNameTest = emp.Name;

                //Assert
                //Assert.AreEqual(1, empIdTest);
                Assert.AreEqual("Employee", empNameTest);
            }

            [Test]
            public void ValidateUser_WhenCredentialsAreValid()
            {
                // Arrange
                var loginServiceMock = new Mock<ILoginService>();
                loginServiceMock.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns(true);

                var loginForm = new LoginForm(loginServiceMock.Object);

                // Act
                var result = loginForm.ValidateUser("validUsername", "validPassword");

                // Assert
                Assert.IsTrue(result);
                loginServiceMock.Verify(x => x.ValidateUser("validUsername", "validPassword"), Times.Once);
            }

            [Test]
            public void ValidateUser_WhenCredentialsAreInvalid()
            {
                // Arrange
                var loginServiceMock = new Mock<ILoginService>();
                loginServiceMock.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns(false);

                var loginForm = new LoginForm(loginServiceMock.Object);

                // Act
                var result = loginForm.ValidateUser("invalidUsername", "invalidPassword");

                // Assert
                Assert.IsFalse(result);
                loginServiceMock.Verify(x => x.ValidateUser("invalidUsername", "invalidPassword"), Times.Once);
            }
        }
    }
}