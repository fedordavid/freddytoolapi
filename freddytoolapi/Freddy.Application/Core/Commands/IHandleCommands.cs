using System.Threading.Tasks;

namespace Freddy.Application.Core.Commands
{
    public interface IHandleCommands<in TCommand> where TCommand : Command
    {
        Task Handle(TCommand command);
    }
}
