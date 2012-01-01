using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Application.Infrastructure.Commands
{
    public class DbCommandProcessor : ICommandProcessor
    {
        Dictionary<string, ICommandProcessor> Commands = new Dictionary<string, ICommandProcessor>();

        public DbCommandProcessor()
        {
            //Commands["migrate:"] = new MigrationCommandProcessor();
        }

        public void Execute(string commandStrng)
        {
            CommandProcessor.ExecuteCommand(commandStrng, Commands);
        }
    }
}
