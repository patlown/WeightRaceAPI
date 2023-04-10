using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using Moq;
using WeightRaceAPI.Controllers;
using WeightRaceAPI.Data;
using WeightRaceAPI.Models;

namespace WeightRaceAPI.Tests.Controllers
{
    public class WeightControllerTests {
        private readonly List<User> _users;
        private readonly List<Weight> _weights;
        private readonly Mock<WeightRaceContext> _fakeContext;
        private readonly User _userJack;
        private readonly Weight _weightOne;
        private readonly Weight _weightTwo;
        private readonly WeightController _targetController;

        public WeightControllerTests()
        {
            _weightOne = new Weight{ WeightId = 1, UserId=1, LogDate = new DateTime(), Value = 204.5 };
            _weightTwo = new Weight{ WeightId = 2, UserId=1, LogDate = new DateTime(), Value = 189.8 };
            _userJack = new User{UserId = 1, UserUid= "jdid", FirstName = "Jack", Weights = new List<Weight>(){_weightOne}};
            _users = new List<User>() { _userJack};
            _weights = new List<Weight>() { _weightOne, _weightTwo};
            _fakeContext = new Mock<WeightRaceContext>(new DbContextOptions<WeightRaceContext>());
            _fakeContext.Setup(x => x.Weights).ReturnsDbSet(_weights);            
            _fakeContext.Setup(x => x.Users).ReturnsDbSet(_users);            
            _targetController = new WeightController(_fakeContext.Object);
        } 

        [Fact]
        public async Task UserController_GetWeights_ReturnSuccess() {
            // Arrange

            // Act
            var results = await _targetController.GetWeights();

            // Assert
            Assert.NotNull(results.Value);
            Assert.Equal(results.Value, _weights);
        } 

        [Fact]
        public async Task WeightController_GetWeightById_ReturnSuccess() {
            // Arrange
            _fakeContext.Setup(x => x.Weights.FindAsync(1)).Returns(new ValueTask<Weight?>(_weightOne));            

            // Act
            var results = await _targetController.GetWeight(1);

            // Assert
            Assert.NotNull(results.Value);
            Assert.Equal(results.Value, _weightOne);
        } 

        [Fact]
        public async Task WeightController_GetWeightsForUser_ReturnSuccess() {
            // Arrange
           
            // Act
            var results = await _targetController.GetUserWeights(1);

            // Assert
            Assert.NotNull(results.Value);
            Assert.Equal(results.Value, _weights);
        } 

        [Fact]
        public async Task WeightController_PutWeight_UpdatesSuccessfully() {
            // Arrange
            var updatedWeight = new Weight{ WeightId = 1, UserId=1, LogDate = new DateTime(), Value = 14.5 };

            _fakeContext.Setup(c => c.SetModified(updatedWeight));
            
            // Act
            var resultOfUpdate = await _targetController.PutWeight(_weightOne.WeightId, updatedWeight);
            
            // Assert
            _fakeContext.Verify(x => x.SetModified(updatedWeight), Times.Once());
            _fakeContext.Verify(x => 
                    x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Fact]
        public async Task WeightController_PostWeight_CallsCreate() {
            // Arrange 
            var newWeight = new Weight{ WeightId = 3, UserId=1, LogDate = new DateTime(), Value = 104.5 };

            // Act
            var results = await _targetController.PostWeight(newWeight);

            // Assert
            _fakeContext.Verify(x => 
                    x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                    Times.Once());
        }

        [Fact]
        public async Task WeightController_DeleteWeight_CallsDelete() {
            // Arrange 
            _fakeContext.Setup(c => c.Weights.FindAsync(1)).Returns(new ValueTask<Weight?>(_weightOne));

            // Act
            var results = await _targetController.DeleteWeight(1);

            // Assert
            _fakeContext.Verify(x => 
                    x.Weights.FindAsync(1), Times.Once());
            _fakeContext.Verify(x => 
                    x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                    Times.Once());
        }
    }
}
