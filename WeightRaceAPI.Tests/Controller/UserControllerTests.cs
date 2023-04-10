using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using Moq;
using WeightRaceAPI.Controllers;
using WeightRaceAPI.Data;
using WeightRaceAPI.Models;

namespace WeightRaceAPI.Tests.Controllers
{
    public class UserControllerTests {
        private readonly List<User> _users;
        private readonly Mock<WeightRaceContext> _fakeContext;
        private readonly User _userJack;
        private readonly User _userPat;
        private readonly Weight _weightOne;
        private readonly Weight _weightTwo;
        private readonly UserController _targetController;

        public UserControllerTests()
        {
            _weightOne = new Weight{ WeightId = 1, LogDate = new DateTime(), Value = 204.5 };
            _weightTwo = new Weight{ WeightId = 2, LogDate = new DateTime(), Value = 189.8 };
            _userJack = new User{UserId = 1, UserUid= "jdid", FirstName = "Jack", Weights = new List<Weight>(){_weightOne}};
            _userPat = new User{UserId = 1, FirstName = "Pat"};
            _users = new List<User>() { _userJack, _userPat};
            _fakeContext = new Mock<WeightRaceContext>(new DbContextOptions<WeightRaceContext>());
            _fakeContext.Setup(x => x.Users).ReturnsDbSet(_users);            
            _targetController = new UserController(_fakeContext.Object);
        } 

        [Fact]
        public async Task UserController_GetUsers_ReturnSuccess() {
            // Arrange
            var userJackMissingWeight = new User{
                UserId = 1, 
                FirstName = "Jack"
            };

            var expectedUsers = new List<User>() {
                userJackMissingWeight, 
                _userPat
            };

            // Act
            var results = await _targetController.GetUsers();

            // Assert
            Assert.NotNull(results.Value);
             
            // Confirm that weight objects are included by comparing 
            // to a list with a user missing their weights
            Assert.NotEqual(results.Value, expectedUsers);
            Assert.Equal(results.Value, _users);


            // Below uses the library fluent assertions. May be worth exploring more down the road.
            // results.Value.Should().BeEquivalentTo(new List<User>() { new User{UserId = 1, FirstName = "Jack"}, new User{UserId = 2, FirstName = "Pat"}});
        } 

        [Fact]
        public async Task UserController_GetUserById_ReturnSuccess() {
            // Arrange

            // Act
            var results = await _targetController.GetUser(1);

            // Assert
            Assert.NotNull(results.Value);
            Assert.Equal(results.Value, _userJack);

            // Below uses the library fluent assertions. May be worth exploring more down the road.
            // results.Value.Should().BeEquivalentTo(new List<User>() { new User{UserId = 1, FirstName = "Jack"}, new User{UserId = 2, FirstName = "Pat"}});
        } 

        [Fact]
        public async Task UserController_GetUserByUID_ReturnSuccess() {
            // Arrange
           
            // Act
            var results = await _targetController.GetUserByUID("jdid");

            // Assert
            Assert.NotNull(results.Value);
            Assert.Equal(results.Value, _userJack);
        } 

        [Fact]
        public async Task UserController_PutUser_UpdatesSuccessfully() {
            // Arrange
            var updatedUser = new User{
                UserId = 1, 
                UserUid= "jdid", 
                FirstName = "Jack",
                LastName = "Davidson",
                Weights = new List<Weight>(){
                    _weightOne
                }
            };

            _fakeContext.Setup(c => c.SetModified(updatedUser));
            
            // Act
            var resultOfUpdate = await _targetController.PutUser(_userJack.UserId, updatedUser);
            
            // Assert
            _fakeContext.Verify(x => x.SetModified(updatedUser), Times.Once());
            _fakeContext.Verify(x => 
                    x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Fact]
        public async Task UserController_PostUser_CallsCreate() {
            // Arrange 
            var newUser = new User{
                UserId = 3, 
                UserUid= "Test", 
                FirstName = "Jack",
                LastName = "Davidson",
                Weights = new List<Weight>(){
                    _weightOne
                }
            };

            // Act
            var results = await _targetController.PostUser(newUser);

            // Assert
            _fakeContext.Verify(x => 
                    x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Fact]
        public async Task UserController_DeleteUser_CallsDelete() {
            // Arrange 
            _fakeContext.Setup(c => c.Users.FindAsync(1)).Returns(new ValueTask<User?>(_userJack));

            // Act
            var results = await _targetController.DeleteUser(1);

            // Assert
            _fakeContext.Verify(x => 
                    x.Users.FindAsync(1), Times.Once());
            _fakeContext.Verify(x => 
                    x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
