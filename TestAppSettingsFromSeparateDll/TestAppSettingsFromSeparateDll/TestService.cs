using ClassLibForTestAppSettingsFromSeparateDll.Settings;
using Microsoft.Extensions.Options;

namespace TestAppSettingsFromSeparateDll;

public class TestService
{
    private readonly IOptions<AxSettings> _axSettingsOption;

    public TestService(IOptions<AxSettings> axSettingsOption)
    {
        _axSettingsOption = axSettingsOption;
    }

    public void Run()
    {
        var axSettings = _axSettingsOption.Value;
        Console.WriteLine($"AxSettings: {axSettings.Key}");
    }
}
