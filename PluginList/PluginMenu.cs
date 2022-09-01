using System.Collections.Immutable;
using System.Text;
using Nyan;

namespace PluginList;

public class PluginMenu : NyanPlugin
{
    private ImmutableArray<NyanPlugin> _plugins;

    public PluginMenu()
    {
        ID = "com.Catcd.pluginMenu";
        Name = "Plugin Menu";
        Author = "Catcd";
        Description = "A plugin that adds a menu of all the plugins in nyan";
        Version = new Version(0, 0, 1);
        ReleaseVersion = ReleaseVersion.Development;
    }

    protected override Task OnRegister(NyanBot nyan)
    {
        _plugins = nyan.Plugins;
        ConsoleApplication.Instance?.commands.RegisterCommand("pluginMenu", ShowList);


        return Task.CompletedTask;
    }

    private Task ShowList(string args, Response response)
    {
        var sb = new StringBuilder();
        foreach (var nyanPlugin in _plugins)
        {
            sb.AppendLine("Id: " + nyanPlugin.ID);
            sb.AppendLine("Plugin: " + nyanPlugin.Name);
            sb.AppendLine("Description: " + nyanPlugin.Description);
            sb.AppendLine("Author: " + nyanPlugin.Author);
            sb.AppendLine("Version: " + nyanPlugin.VersionString);
            sb.AppendLine(" ");
        }

        response(sb.ToString());
        return Task.CompletedTask;
    }
}