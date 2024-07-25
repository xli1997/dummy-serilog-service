<PackageReference Include="xunit" Version="2.4.1" />
<PackageReference Include="Moq" Version="4.16.1" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.7.1" />

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Moq;
using Xunit;

public class Record
{
    public string SubscriptionSymbol { get; set; }
    public string Symbol { get; set; }
    public string Factor { get; set; }
    public string OxInternalSymbol { get; set; }
}

public class YourClass
{
    private List<Record> MappingList = new List<Record>();
    private string FilePath;
    private bool IsVIV2Split;
    private DateTime LastUpdateDateTime;
    private readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

    public void WriteToCacheDataFile()
    {
        Lock.EnterWriteLock();
        try
        {
            if (IsVIV2Split)
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(AppContext.BaseDirectory, FilePath)))
                {
                    writer.WriteLine($"{LastUpdateDateTime}");
                    foreach (Record item in MappingList)
                    {
                        writer.WriteLine($"{item.SubscriptionSymbol},{item.Symbol},{item.Factor},{item.OxInternalSymbol}");
                    }
                }

                using (StreamWriter writer = new StreamWriter(Path.Combine(AppContext.BaseDirectory, FilePath.Replace(".dat", "_V1.dat"))))
                {
                    writer.WriteLine($"{LastUpdateDateTime}");
                    foreach (Record item in MappingList)
                    {
                        writer.WriteLine($"{item.SubscriptionSymbol},{item.Symbol},{item.Factor}");
                    }
                }

                using (StreamWriter writer = new StreamWriter(Path.Combine(AppContext.BaseDirectory, FilePath.Replace(".dat", "_V2.dat"))))
                {
                    writer.WriteLine($"{LastUpdateDateTime}");
                    foreach (Record item in MappingList)
                    {
                        writer.WriteLine($"{item.OxInternalSymbol},{item.Symbol},{item.Factor}");
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.WriteLine($"{LastUpdateDateTime}");
                    foreach (Record item in MappingList)
                    {
                        writer.WriteLine($"{item.SubscriptionSymbol},{item.Symbol},{item.Factor}");
                    }
                }
            }
        }
        finally
        {
            Lock.ExitWriteLock();
        }
    }
}

public class YourClassTests
{
    [Fact]
    public void WriteToCacheDataFile_WithVIV2Split_WritesCorrectly()
    {
        // Arrange
        var mockAppContext = new Mock<AppContextBase>();
        var testFilePath = "test.dat";
        var testRecords = new List<Record>
        {
            new Record { SubscriptionSymbol = "Sub1", Symbol = "Sym1", Factor = "Fac1", OxInternalSymbol = "OxInt1" },
            new Record { SubscriptionSymbol = "Sub2", Symbol = "Sym2", Factor = "Fac2", OxInternalSymbol = "OxInt2" }
        };
        var lastUpdateDateTime = DateTime.Now;
        
        var yourClass = new YourClass
        {
            MappingList = testRecords,
            FilePath = testFilePath,
            IsVIV2Split = true,
            LastUpdateDateTime = lastUpdateDateTime
        };

        var baseDirectory = AppContext.BaseDirectory;
        var v1FilePath = Path.Combine(baseDirectory, testFilePath.Replace(".dat", "_V1.dat"));
        var v2FilePath = Path.Combine(baseDirectory, testFilePath.Replace(".dat", "_V2.dat"));

        // Act
        yourClass.WriteToCacheDataFile();

        // Assert
        AssertFileContents(Path.Combine(baseDirectory, testFilePath), lastUpdateDateTime, testRecords, true);
        AssertFileContents(v1FilePath, lastUpdateDateTime, testRecords, false, false);
        AssertFileContents(v2FilePath, lastUpdateDateTime, testRecords, false, true);
    }

    [Fact]
    public void WriteToCacheDataFile_WithoutVIV2Split_WritesCorrectly()
    {
        // Arrange
        var mockAppContext = new Mock<AppContextBase>();
        var testFilePath = "test.dat";
        var testRecords = new List<Record>
        {
            new Record { SubscriptionSymbol = "Sub1", Symbol = "Sym1", Factor = "Fac1", OxInternalSymbol = "OxInt1" },
            new Record { SubscriptionSymbol = "Sub2", Symbol = "Sym2", Factor = "Fac2", OxInternalSymbol = "OxInt2" }
        };
        var lastUpdateDateTime = DateTime.Now;

        var yourClass = new YourClass
        {
            MappingList = testRecords,
            FilePath = testFilePath,
            IsVIV2Split = false,
            LastUpdateDateTime = lastUpdateDateTime
        };

        // Act
        yourClass.WriteToCacheDataFile();

        // Assert
        AssertFileContents(testFilePath, lastUpdateDateTime, testRecords, true);
    }

    private void AssertFileContents(string filePath, DateTime lastUpdateDateTime, List<Record> records, bool includeOxInternalSymbol, bool onlyOxInternalSymbol = false)
    {
        var expectedLines = new List<string> { lastUpdateDateTime.ToString() };
        foreach (var record in records)
        {
            if (onlyOxInternalSymbol)
            {
                expectedLines.Add($"{record.OxInternalSymbol},{record.Symbol},{record.Factor}");
            }
            else if (includeOxInternalSymbol)
            {
                expectedLines.Add($"{record.SubscriptionSymbol},{record.Symbol},{record.Factor},{record.OxInternalSymbol}");
            }
            else
            {
                expectedLines.Add($"{record.SubscriptionSymbol},{record.Symbol},{record.Factor}");
            }
        }

        var actualLines = File.ReadAllLines(filePath);
        Assert.Equal(expectedLines, actualLines);
    }
}