using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace ShaedyRCON;

public class ShaedyRcon : BasePlugin
{
    public override string ModuleName => "shaedy RCON";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "shaedy";

    // Hardcoded prefix
    private string PluginPrefix => $"{ChatColors.White}[{ChatColors.Green}shaedy-RCON{ChatColors.White}] ";

    public override void Load(bool hotReload)
    {
        Console.WriteLine("[ShaedyRcon] Plugin loaded.");
    }

    [ConsoleCommand("css_rcon", "Executes a server command")]
    [CommandHelper(minArgs: 1, usage: "<command>", whoCanExecute: CommandUsage.CLIENT_ONLY)]
    [RequiresPermissions("@css/root")]
    public void OnRconCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null || !player.IsValid) return;

        string commandToExecute = command.ArgString;

        if (string.IsNullOrWhiteSpace(commandToExecute))
        {
            player.PrintToChat($"{PluginPrefix} {ChatColors.Red}Please provide a command.");
            return;
        }

        try
        {
            Server.ExecuteCommand(commandToExecute);
            player.PrintToChat($"{PluginPrefix} Executed: {ChatColors.Green}{commandToExecute}");
            Console.WriteLine($"[ShaedyRcon] Admin {player.PlayerName} executed: {commandToExecute}");
        }
        catch (Exception ex)
        {
            player.PrintToChat($"{PluginPrefix} {ChatColors.Red}Error: {ex.Message}");
        }
    }
}