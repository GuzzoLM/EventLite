using System.Threading.Tasks;
using EventLite.Streams.StreamManager;
using POC.Documents.Commands;

namespace POC.Documents
{
    public class CommandHandler
    {
        private readonly IEventStreamReader _eventStreamReader;

        public CommandHandler(IEventStreamReader eventStreamReader)
        {
            _eventStreamReader = eventStreamReader;
        }

        public async Task HandleCreateDocument(CreateDocument command)
        {
            //var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            //var @event = aggregate.CreateDocument(command);
            //await aggregate.Save(@event);
        }

        public async Task HandleUpdateDocument(RenameDocument command)
        {
            //var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            //var @event = aggregate.UpdateDocument(command);
            //await aggregate.Save(@event);
        }

        public async Task HandleDeleteDocument(DeleteDocument command)
        {
            //var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            //var @event = aggregate.DeleteDocument(command);
            //await aggregate.Save(@event);
        }
    }
}