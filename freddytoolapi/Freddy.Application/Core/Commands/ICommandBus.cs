using System.Threading.Tasks;

namespace Freddy.Application.Core.Commands
{
    public interface ICommandBus
    {
        Task Handle<TCommand>(TCommand command) where TCommand : Command;
    }
}