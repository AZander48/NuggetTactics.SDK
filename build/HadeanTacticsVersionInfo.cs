using Nuke.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace _build;

[TypeConverter(typeof(Converter))]
public record HadeanTacticsVersionInfo
{
    public const uint STEAM_APPID = 1260590;
    public const uint STEAM_DEPOT_ID_WINDOWS = 1260591;
    public const uint STEAM_DEPOT_ID_MAC = 1260593;
    public const uint STEAM_DEPOT_ID_LINUX = 1260592;

    public static IEnumerable<HadeanTacticsVersionInfo> AllVersions => [
        .. typeof(HadeanTacticsVersionInfo).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.FieldType == typeof(HadeanTacticsVersionInfo))
            .Select(x => (HadeanTacticsVersionInfo)x.GetValue(null))
    ];
    public static IEnumerable<string> AllVersionStrings => AllVersions.Select(x => x.Version);

    public required string Version { get; init; }
    public required ulong WindowsManifestId { get; init; }
    public required ulong MacManifestId { get; init; }
    public required ulong LinuxManifestId { get; init; }

    public static readonly HadeanTacticsVersionInfo _2_1_15 = new()
    {
        Version = "2.1.15",
        WindowsManifestId = 1218875717814362851,
        MacManifestId = 2735488591912107437,
        LinuxManifestId = 5372835591775659255
    };

    public sealed override string ToString() => Version;
    public string ToDetailedString()
    {
        StringBuilder sb = new();
        sb.Append("{ ");
        PrintMembers(sb);
        sb.Append(" }");
        return sb.ToString();
    }
    public static implicit operator string(HadeanTacticsVersionInfo v) => v.Version;

    public class Converter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string str)
            {
                List<HadeanTacticsVersionInfo> matches = [.. AllVersions.Where(v => v.Version == str)];
                Assert.HasSingleItem(matches);
                return matches[0];
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
