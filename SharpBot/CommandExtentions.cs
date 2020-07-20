using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommandExtentions
{
    readonly CommandService _commands;

    public CommandExtentions(CommandService commandService)
    {
        _commands = commandService;
    }

    public Task<ReadOnlyCollection<CommandInfo>> GetAllCommands()
    {
        foreach (var command in _commands.Commands)
            Console.WriteLine(command.Module.Name);

        return Task.Run(() => _commands.Commands.ToList().AsReadOnly());
    }
    public Task<ReadOnlyCollection<CommandInfo>> GetCommands(string module)
    {
        return Task.Run(() => _commands.Commands.Where(x => x.Module.Name == module).ToList().AsReadOnly());
    }
}
