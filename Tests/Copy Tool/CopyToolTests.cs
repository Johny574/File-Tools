using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class CopyToolTests
{
    private CopyTool _copyTool;

    private string _testPath = "Assets/CopyToolTest";
    private string _outPath = "/outPath";
    private string _testFile = "/test.txt";

    [SetUp]
    public void SetUp()
    { 
        _copyTool = new CopyTool();
        
        // Create test directory.
        FolderCreateTool.CreateDirectory(_testPath);

        // Create outpath.
        FolderCreateTool.CreateDirectory(Path.Join(_testPath, _outPath));
     
        // Create testfile.
        using (StreamWriter writer = new StreamWriter(Path.Join(_testPath, _testFile), false))
        {
            writer.WriteLine("Hello, world! This is a test file.");
        }

        AssetDatabase.Refresh();
    }

    [Test]
    public void AssetsCopiedCorrectly()
    { 
        var outPath = new Object[1]{AssetDatabase.LoadAssetAtPath<Object>(Path.Join(_testPath, _outPath))};

        var testFile = new Object[1]{ AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Join(_testPath, _testFile)) };

        _copyTool.Start(outPath, testFile);

        Assert.IsTrue(File.Exists(Path.Join(_testPath, "/test.txt")));
    }

    [Test]
    public void NoObjectGivenThrowsException()
    {
        Assert.Throws<System.Exception>(() => _copyTool.Start(new Object[1]{AssetDatabase.LoadAssetAtPath(_testPath, typeof(UnityEngine.Object))}, null));
    }

    [Test]
    public void NoDirectoryGivenThrowsException()
    {
        Assert.Throws<System.Exception>(() => _copyTool.Start(null, new Object[1]{ new Object() }));
    }

    [TearDown]
    public void TearDown()
    {
        // Remove testFile.
        if (File.Exists(Path.Join(_testPath, _testFile)))
        {
            File.Delete(Path.Join(_testPath, _testFile));
        }    
        
        // Remove outPath.
        Directory.Delete(Path.Join(_testPath, _outPath), true);

        // Remove testPath.
        Directory.Delete(_testPath, true);
    }
}