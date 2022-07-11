using System.Text.Json;

namespace CDoc.Config;

public class Settings
{
    private const string _settingsFile = "/etc/cdoc.json";
    private CDocConfig? _config;

    public CDocConfig GetConfig()
    {
        _config ??= JsonSerializer.Deserialize<CDocConfig>(File.ReadAllText(_settingsFile));
        if (_config == null)
        {
            throw new Exception($"Unable to load configuration file in {_settingsFile}");
        }
        return _config;
    } 
}