using System;
namespace Rapido.Application.Infrastructure.Commands
{
    public interface ICommandProcessor
    {
        void Execute(string commandStrng);
    }
}
