using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

class RenameToolTests
{
    // private RenameToolSettings settings;
    private RenameTool _renameTool;
    private string _testPath = "Assets/RenameTool";
    private string _testFile = "test.txt";

    [SetUp]
    public void SetUp()
    { 
        RenameToolSettings settings = new RenameToolSettings("key", "value", "prefix", "suffix", true, true, true);
        _renameTool = new RenameTool(settings, "prefix", "suffix");

        FolderCreateTool.CreateDirectory(_testPath);   

        using (StreamWriter writer = new StreamWriter(Path.Join(_testPath, _testFile), true))
        {
            writer.WriteLine("Hello World! this is a test file.");
        }

        AssetDatabase.Refresh();
    }

    [Test]
    public void RenamedCorrectly()
    {
        var testFile = new Object[1]{ AssetDatabase.LoadAssetAtPath<TextAsset>(_testPath + _testFile) };
        _renameTool.Start(testFile);
    }

    [Test]
    public void NoObjectsSelectedThrowsException()
    {
        Assert.Throws<System.Exception>(() => _renameTool.Start(null));
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