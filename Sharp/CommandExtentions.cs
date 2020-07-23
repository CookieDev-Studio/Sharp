using Discord.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

public class CommandExtentions
{
    readonly CommandService _commands;

    public CommandExtentions(CommandService commandService)
    {
        _commands = commandService;
    }

    public ReadOnlyCollection<CommandInfo> GetAllCommands()
    {
        return _commands.Commands.ToList().AsReadOnly();
    }
    public ReadOnlyCollection<CommandInfo> GetCommands(string module)
    {
        return _commands.Commands.Where(x => x.Module.Name == module).ToList().AsReadOnly();
    }
}
