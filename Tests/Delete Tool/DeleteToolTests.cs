using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

class DeleteToolTests
{ 
    private DeleteTool _deleteTool;
    
    private string _testPath = "Assets/DeleteToolTest";
    private string _testFile = "/test.txt";

    [SetUp]
    public void SetUp()
    {
        _deleteTool = new();
        FolderCreateTool.CreateDirectory(_testPath);
        using ( StreamWriter writer = new StreamWriter(Path.Join(_testPath, _testFile), true))
        {
            writer.WriteLine("Hello world!, this is a test file.");
        }
        AssetDatabase.Refresh();
    }

    [Test]
    public void FilesDeletedCorrectly()
    {
        _deleteTool.Start("*test*", _testPath);
    }

    [Test]
    public void NoFilesFoundThrowsException()
    { 
        Assert.Throws<System.Exception>(() =>_deleteTool.Start("test", _testPath));
    }

    [Test]
    public void EmptySearchPatternThrowsException()
    { 
        Assert.Throws<System.Exception>(() =>_deleteTool.Start(null, _testPath));
    }

    [Test]
    public void EmptyDirectoryThrowsException()
    {
        Assert.Throws<System.Exception>(() =>_deleteTool.Start("", null));
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(Path.Join(_testPath, _testFile)))
        {
            File.Delete(Path.Join(_testPath, _testFile));
        }
        Directory.Delete(_testPath, true);
    }
}