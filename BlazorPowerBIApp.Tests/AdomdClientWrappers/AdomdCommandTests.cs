using BlazorPowerBIApp.Services.AdomdClientWrappers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Data;
using Xunit;

namespace BlazorPowerBIApp.Tests.AdomdClientWrappers
{
    public class AdomdCommandTests
    {
        private readonly Mock<ILogger<AdomdCommand>> _loggerMock;

        public AdomdCommandTests()
        {
            _loggerMock = new Mock<ILogger<AdomdCommand>>();
        }

        [Fact]
        public void Constructor_WithNullLogger_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdomdCommand(logger: null));
        }

        [Fact]
        public void Constructor_WithValidLogger_CreatesInstance()
        {
            // Act
            var command = new AdomdCommand(_loggerMock.Object);

            // Assert
            Assert.NotNull(command);
        }

        [Fact]
        public void AddParameter_WithNullParameter_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new AdomdCommand(_loggerMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => command.AddParameter(null));
        }

        [Fact]
        public void AddParameter_WithValidParameter_AddsSuccessfully()
        {
            // Arrange
            var command = new AdomdCommand(_loggerMock.Object);
            var parameter = new AdomdParameter("TestParam", "TestValue");

            // Act
            command.AddParameter(parameter);

            // Assert - No exception thrown
        }

        [Fact]
        public void Dispose_MultipleCalls_DoesNotThrow()
        {
            // Arrange
            var command = new AdomdCommand(_loggerMock.Object);

            // Act & Assert
            command.Dispose();
            command.Dispose(); // Second dispose should not throw
        }

        [Fact]
        public void CommandText_AfterDispose_ThrowsObjectDisposedException()
        {
            // Arrange
            var command = new AdomdCommand(_loggerMock.Object);
            command.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => command.CommandText = "SELECT * FROM Table");
        }

        [Fact]
        public void CommandType_SetAndGet_WorksCorrectly()
        {
            // Arrange
            var command = new AdomdCommand(_loggerMock.Object);

            // Act
            command.CommandType = CommandType.StoredProcedure;

            // Assert
            Assert.Equal(CommandType.StoredProcedure, command.CommandType);
        }

        [Fact]
        public void ClearParameters_AfterAddingParameters_ClearsSuccessfully()
        {
            // Arrange
            var command = new AdomdCommand(_loggerMock.Object);
            command.AddParameter(new AdomdParameter("Param1", "Value1"));
            command.AddParameter(new AdomdParameter("Param2", "Value2"));

            // Act
            command.ClearParameters();

            // Assert - No exception thrown
        }
    }
}
