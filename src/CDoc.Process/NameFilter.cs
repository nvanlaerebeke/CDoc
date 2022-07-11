namespace CDoc.Process;

internal static class NameFilter
{
    public static string Filter(string filename)
    {
        var s = Path.GetFileNameWithoutExtension(filename);
        if (string.IsNullOrEmpty(s))
        {
            return filename;
        }
        
        if (s.Length > 3 && int.TryParse(s[..2], out _) && s[2].Equals('_'))
        {
            return s[3..];
        }
        return s;
    }
}