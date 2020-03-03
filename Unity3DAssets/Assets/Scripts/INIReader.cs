using System;
using System.Collections.Generic;

internal class INIReader
{
    /*
     * File-Format:
     * _______________________
     * 
     * #Comment
     * [Section]
     * Key1=Value1
     *
     */

    private const string LINE_SEPARATOR = "\r\n";
    private const string KEY_VALUE_SEPARATOR = "=";

    private Dictionary<string, string> config = new Dictionary<string, string>();

    public INIReader(string pathToFile)
    {
        try
        {
            string configText = System.IO.File.ReadAllText(pathToFile);
            Read(configText);
        }
        catch
        {
            // File read error            
        }
    }

    public string getConfig(string key)
    {
        string value = "";
        try
        {
            value = config[key];
        }
        catch
        {

        }
        return value;
    }

    private void Read(string configText)
    {
        Dictionary<string, string> _s = new Dictionary<string, string>();
        string[] lines = configText.Split(new string[] { LINE_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var l in lines)
        {
            string t = l.Trim();
            if (t[0] == '#')
            {
                continue;
            }
            else
            {
                string[] kv = t.Split(new string[] { KEY_VALUE_SEPARATOR }, StringSplitOptions.None);
                if (kv.Length == 2)
                {
                    config.Add(kv[0].Trim(), kv[1].Trim());
                }
            }
        }
    }
}