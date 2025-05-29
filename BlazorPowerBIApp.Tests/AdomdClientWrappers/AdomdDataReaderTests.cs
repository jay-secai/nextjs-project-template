using BlazorPowerBIApp.Services.AdomdClientWrappers;
using System;
using System.Data;
using Xunit;

namespace BlazorPowerBIApp.Tests.AdomdClientWrappers
{
    public class AdomdDataReaderTests
    {
        [Fact]
        public void Constructor_WithNullReader_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdomdDataReader(null));
        }

        [Fact]
        public void Dispose_MultipleCalls_DoesNotThrow()
        {
            // Arrange
            var mockReader = new MockAdomdDataReader();
            var reader = new AdomdDataReader(mockReader);

            // Act & Assert
            reader.Dispose();
            reader.Dispose(); // Second dispose should not throw
        }

        [Fact]
        public void Read_AfterDispose_ThrowsObjectDisposedException()
        {
            // Arrange
            var mockReader = new MockAdomdDataReader();
            var reader = new AdomdDataReader(mockReader);
            reader.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => reader.Read());
        }

        [Fact]
        public void GetValue_AfterDispose_ThrowsObjectDisposedException()
        {
            // Arrange
            var mockReader = new MockAdomdDataReader();
            var reader = new AdomdDataReader(mockReader);
            reader.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => reader.GetValue(0));
        }

        // Helper mock class for testing
        private class MockAdomdDataReader : Microsoft.AnalysisServices.AdomdClient.AdomdDataReader
        {
            public override int Depth => 0;
            public override bool IsClosed => false;
            public override int RecordsAffected => 0;
            public override int FieldCount => 1;
            public override void Close() { }
            public override bool GetBoolean(int i) => false;
            public override byte GetByte(int i) => 0;
            public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length) => 0;
            public override char GetChar(int i) => 'a';
            public override long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length) => 0;
            public override string GetDataTypeName(int i) => "string";
            public override DateTime GetDateTime(int i) => DateTime.Now;
            public override decimal GetDecimal(int i) => 0;
            public override double GetDouble(int i) => 0;
            public override Type GetFieldType(int i) => typeof(string);
            public override float GetFloat(int i) => 0;
            public override Guid GetGuid(int i) => Guid.Empty;
            public override short GetInt16(int i) => 0;
            public override int GetInt32(int i) => 0;
            public override long GetInt64(int i) => 0;
            public override string GetName(int i) => "Column1";
            public override int GetOrdinal(string name) => 0;
            public override string GetString(int i) => "";
            public override object GetValue(int i) => null;
            public override int GetValues(object[] values) => 0;
            public override bool IsDBNull(int i) => true;
            public override bool NextResult() => false;
            public override bool Read() => false;
            public override DataTable GetSchemaTable() => new DataTable();
        }
    }
}
