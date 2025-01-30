using UnityEngine;

public class RenameToolSettings 
{
    public string Key = "";
    public string Value = "";

    public string Prefix = "";
    public string Suffix = "";

    public bool UseParentNameAsValue = false; 
    public bool AppendParentNameToPrefix = false; 
    public bool AppendParentNameToSuffix = false;

    public RenameToolSettings(string key, string value, string prefix, string suffix, bool useParentNameAsValue, bool appendParentNameToPrefix, bool appendParentNameToSuffix)
    {
        Key = key;
        Value = value;
        Prefix = prefix;
        Suffix = suffix;
        UseParentNameAsValue = useParentNameAsValue;
        AppendParentNameToPrefix = appendParentNameToPrefix;
        AppendParentNameToSuffix = appendParentNameToSuffix;
    }
}
