using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Application.Infrastructure.Commands
{
  
    public class CommandListProcessor : ICommandProcessor
    {

        public CommandListProcessor()
        {
        }

        public void Execute(string commandStrng)
        {
            Console.WriteLine("db create [Rapido]");
            Console.WriteLine("db gen [create_users_table]");
            Console.WriteLine("db run [version]");
            Console.WriteLine("db migrate");
            Console.WriteLine("db rollback");
            Console.WriteLine("db migrate [version]");
            Console.WriteLine("db rollback [version]");
            Console.WriteLine("db version");          
            Console.WriteLine("config gen");
        }
    }
}
