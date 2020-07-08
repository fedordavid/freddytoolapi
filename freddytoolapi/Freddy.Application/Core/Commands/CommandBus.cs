using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Freddy.Application.Core.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Handle<TCommand>(TCommand command) where TCommand : Command
        {
            var executor = _serviceProvider.GetService<IHandleCommands<TCommand>>();

            return executor.Handle(command);
        }
    }
}
