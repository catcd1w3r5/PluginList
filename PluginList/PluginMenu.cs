using System.Collections.Immutable;
using System.Text;
using Nyan;
using Nyan.Plugins;
using Nyan.Types;

namespace PluginList;

public class PluginMenu : NyanPlugin
{
    private ImmutableArray<NyanPlugin> _plugins;

    public PluginMenu() : base("com.Catcd.pluginMenu")
    {
        Name = "Plugin Menu";
        Author = "Catcd";
        Description = "A plugin that adds a menu of all the plugins in nyan";
        Version = "0.0.1 dev";
    }

    protected override Task OnRegister(NyanBot nyan)
    {
        _plugins = nyan.Plugins;
        BotInstance.Instance.commands.RegisterCommand("pluginList", ShowList);
        return Task.CompletedTask;
    }

    private Task ShowList(ReadOnlySpan<char> args, Response response)
    {
        var sb = new StringBuilder();
        foreach (var nyanPlugin in _plugins)
        {
            sb.AppendLine($"Id: {nyanPlugin.Id}");
            sb.AppendLine($"Plugin: {nyanPlugin.Name}");
            sb.AppendLine($"Description: {nyanPlugin.Description}");
            sb.AppendLine($"Author: {nyanPlugin.Author}");
            sb.AppendLine($"Version: {nyanPlugin.Version}");
            sb.AppendLine(" ");
        }

        response(sb.ToString());
        return Task.CompletedTask;
    }
}