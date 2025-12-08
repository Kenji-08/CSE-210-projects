using System;
using System.Collections.Generic;
using System.IO;

class IniFile
{
    private Dictionary<string, Dictionary<string, string>> data = new();

    public IniFile(string path)
    {
        Load(path);
    }

    public void Load(string path)
    {
        data.Clear();
        string currentSection = "";

        foreach (string line in File.ReadAllLines(path))
        {
            string trimmed = line.Trim();

            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                continue;

            if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
            {
                currentSection = trimmed[1..^1].Trim();
                data[currentSection] = new Dictionary<string, string>();
            }
            else if (trimmed.Contains("="))
            {
                string[] split = trimmed.Split('=', 2);
                string key = split[0].Trim();
                string value = split[1].Trim();

                if (currentSection != "")
                    data[currentSection][key] = value;
            }
        }
    }

    public string Get(string section, string key, string defaultValue = "")
    {
        if (data.ContainsKey(section) && data[section].ContainsKey(key))
            return data[section][key];
        return defaultValue;
    }

    public void Set(string section, string key, string value)
    {
        if (!data.ContainsKey(section))
            data[section] = new Dictionary<string, string>();
        data[section][key] = value;
    }

    public void Save(string path)
    {
        using StreamWriter writer = new(path);
        foreach (KeyValuePair<string, Dictionary<string, string>> section in data)
        {
            writer.WriteLine($"[{section.Key}]");
            foreach (KeyValuePair<string, string> kv in section.Value)
                writer.WriteLine($"{kv.Key}={kv.Value}");
            writer.WriteLine();
        }
    }

    public Dictionary<string, string> GetSection(string section)
    {
        if (data.ContainsKey(section))
            return data[section];

        return new Dictionary<string, string>();
    }

}
