using System.Collections.Immutable;
using System.Text;
using Nyan;

namespace PluginList;

public class PluginList : NyanPlugin
{
    private ImmutableArray<NyanPlugin> _plugins;

    public PluginList()
    {
        ID = "com.Catcd.pluginList";
        Name = "Plugin List";
        Author = "Catcd";
        Description = "A plugin that adds a menu of all the plugins in nyan";
        Version = new Version(0, 0, 1);
        ReleaseVersion = ReleaseVersion.Development;
    }

    protected override Task OnRegister(NyanBot nyan)
    {
        _plugins = nyan.Plugins;
        ConsoleApplication.Instance?.commands.RegisterCommand("pluginList", ShowList);
        nyan.Client.MessageCreated += (_, e) =>
        {
            if (e.Author.Id == nyan.Client.CurrentUser.Id || !e.Channel.IsPrivate || e.Message.Content != "Plugin List")
                return Task.CompletedTask;
            var str = GenerateList();
            e.Channel.SendMessageAsync(str);

            return Task.CompletedTask;
        };

        return Task.CompletedTask;
    }

    private Task ShowList(string args, Response response)
    {
        response(GenerateList());
        return Task.CompletedTask;
    }

    private string GenerateList()
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

        return sb.ToString();
    }
}