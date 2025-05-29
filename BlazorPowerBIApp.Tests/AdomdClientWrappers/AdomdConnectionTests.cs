using BlazorPowerBIApp.Services.AdomdClientWrappers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace BlazorPowerBIApp.Tests.AdomdClientWrappers
{
    public class AdomdConnectionTests
    {
        private readonly Mock<ILogger<AdomdConnection>> _loggerMock;
        private const string TestConnectionString = "Data Source=localhost;Initial Catalog=TestDB";

        public AdomdConnectionTests()
        {
            _loggerMock = new Mock<ILogger<AdomdConnection>>();
        }

        [Fact]
        public void Constructor_WithNullLogger_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdomdConnection(logger: null));
        }

        [Fact]
        public void Constructor_WithValidLogger_CreatesInstance()
        {
            // Act
            var connection = new AdomdConnection(_loggerMock.Object);

            // Assert
            Assert.NotNull(connection);
        }

        [Fact]
        public void Constructor_WithConnectionString_SetsConnectionString()
        {
            // Act
            var connection = new AdomdConnection(TestConnectionString, _loggerMock.Object);

            // Assert
            Assert.Equal(TestConnectionString, connection.ConnectionString);
        }

        [Fact]
        public void Constructor_WithNullConnectionString_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new AdomdConnection(null, _loggerMock.Object));
        }

        [Fact]
        public void ConnectionString_SetAndGet_WorksCorrectly()
        {
            // Arrange
            var connection = new AdomdConnection(_loggerMock.Object);

            // Act
            connection.ConnectionString = TestConnectionString;

            // Assert
            Assert.Equal(TestConnectionString, connection.ConnectionString);
        }

        [Fact]
        public void CreateCommand_ReturnsValidCommand()
        {
            // Arrange
            var connection = new AdomdConnection(_loggerMock.Object);

            // Act
            var command = connection.CreateCommand();

            // Assert
            Assert.NotNull(command);
        }

        [Fact]
        public void Dispose_MultipleCalls_DoesNotThrow()
        {
            // Arrange
            var connection = new AdomdConnection(_loggerMock.Object);

            // Act & Assert
            connection.Dispose();
            connection.Dispose(); // Second dispose should not throw
        }

        [Fact]
        public void ConnectionString_AfterDispose_ThrowsObjectDisposedException()
        {
            // Arrange
            var connection = new AdomdConnection(_loggerMock.Object);
            connection.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => connection.ConnectionString = TestConnectionString);
        }

        [Fact]
        public void State_InitialState_IsClosed()
        {
            // Arrange
            var connection = new AdomdConnection(_loggerMock.Object);

            // Assert
            Assert.Equal(ConnectionState.Closed, connection.State);
        }

        [Fact]
        public void Close_WhenDisposed_ThrowsObjectDisposedException()
        {
            // Arrange
            var connection = new AdomdConnection(_loggerMock.Object);
            connection.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => connection.Close());
        }

        [Fact]
        public void CreateCommand_WhenDisposed_ThrowsObjectDisposedException()
        {
            // Arrange
            var connection = new AdomdConnection(_loggerMock.Object);
            connection.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => connection.CreateCommand());
        }
    }
}
