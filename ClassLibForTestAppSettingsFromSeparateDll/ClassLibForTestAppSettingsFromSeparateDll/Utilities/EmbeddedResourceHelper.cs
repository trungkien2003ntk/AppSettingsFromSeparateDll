using System.Reflection;

namespace ClassLibForTestAppSettingsFromSeparateDll.Utilities;

public static class EmbeddedResourceHelper
{
    public static string GetEmbeddedResourceJson(Assembly assembly, string resourceFileName)
    {
        using var stream = assembly.GetManifestResourceStream(resourceFileName) 
            ?? throw new Exception($"Resource '{resourceFileName}' not found.");
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}
