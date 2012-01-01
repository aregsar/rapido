using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Commands
{
    

    public class CommandProcessor
    {
        public static string MigrationFilesPath;

        Dictionary<string, ICommandProcessor> Commands = new Dictionary<string, ICommandProcessor>();

        public CommandProcessor()
        {
            
            Commands["commands"] = new CommandListProcessor();//list available commands
            Commands["db"] = new DbCommandProcessor();
        }
        public void Execute(string commandStrng)
        {
             ExecuteCommand(commandStrng, Commands);
        }

        public void SetPaths(string migrationFiles)
        {
            MigrationFilesPath = migrationFiles;
        }

   

        public static void ExecuteCommand(string commandStrng,Dictionary<string, ICommandProcessor> Commands)
        {
              string firstSegment = commandStrng.Trim().Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

             ICommandProcessor command = Commands[firstSegment];

             if (command != null)
                Commands[firstSegment].Execute(commandStrng.Remove(0, firstSegment.Length).Trim());
             else
                Console.WriteLine("no such command");
        }
    }
}