using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;


class FolderCreateToolTests
{
    private FolderCreateTool _folderCreateTool;
    private string _testPath = "Assets/FolderCreateToolTest";
    private string  _testFile = "test.txt";

    [SetUp]
    public void SetUp()
    {
        _folderCreateTool = new FolderCreateTool();
        FolderCreateTool.CreateDirectory(_testPath);
        
        using (StreamWriter writer = new StreamWriter(Path.Join(_testPath, _testFile)))
        {
            writer.WriteLine("Hello World! this is a test file.");
        }

        AssetDatabase.Refresh();
    }

    [Test]
    public void CreatedFoldersCorrectly()
    { 
        var testFiles = new Object[1]{ AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Join(_testPath, _testFile)) };
        _folderCreateTool.Start(testFiles, _testPath, "");
    }

    [Test]
    public void NoObjectsFoundThrowsExecption()
    {
        Assert.Throws<System.Exception>(() => _folderCreateTool.Start(null, _testPath, ""));
    }

    [Test]
    public void NoOutpathGivenThrowsExecption()
    {
        var testFiles = new Object[1]{ AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Join(_testPath, _testFile)) };
        Assert.Throws<System.Exception>(() => _folderCreateTool.Start(testFiles, null, ""));
    }

    [TearDown]
    public void TearDown()
    {
        Directory.Delete(_testPath, true);
    }
}