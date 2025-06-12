using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

class RenameToolTests
{
    // private RenameToolSettings settings;
    private RenameTool _renameTool;
    private string _testPath = "Assets/RenameTool";
    private string _testFile = "test.txt";

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        RenameToolSettings settings = new RenameToolSettings("key", "value", "prefix", "suffix", true, true, true);
        _renameTool = new RenameTool(settings, "prefix", "suffix");

        FolderCreateTool.CreateDirectory(_testPath);
        yield return null;
    }

    [UnityTest]
    public IEnumerator RenamedCorrectly()
    {
        using (StreamWriter writer = new StreamWriter(Path.Join(_testPath, _testFile), true))
        {
            writer.WriteLine("Hello World! this is a test file.");
        }

        AssetDatabase.ImportAsset(Path.Join(_testPath, _testFile));
        yield return null;

        AssetDatabase.Refresh();
        yield return null;

        var o = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Join(_testPath, _testFile).Replace("\\", "/"));
        var testFile = new Object[1]{ o };
        _renameTool.Start(testFile);
    }

    [UnityTest]
    public IEnumerator NoObjectsSelectedThrowsException()
    {
        Assert.Throws<System.Exception>(() => _renameTool.Start(null));
        yield return null;
    }


    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (File.Exists(Path.Join(_testPath, _testFile)))
        {
            File.Delete(Path.Join(_testPath, _testFile));
        }

        Directory.Delete(_testPath, true);
        yield return null;
    }
}