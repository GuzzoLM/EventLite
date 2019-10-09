using System.Threading.Tasks;
using EventLite.Exceptions;
using EventLite.Streams.StreamManager;
using POC.Documents.Commands;
using POC.Documents.Model;

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
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var @event = aggregate.CreateDocument(command);

            await SaveEvent(aggregate, @event);
        }

        public async Task HandleUpdateDocument(UpdateDocument command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var @event = aggregate.UpdateDocument(command);

            await SaveEvent(aggregate, @event);
        }

        public async Task HandleDeleteDocument(DeleteDocument command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var @event = aggregate.DeleteDocument(command);

            await SaveEvent(aggregate, @event);
        }

        private async Task SaveEvent<T>(DocumentAggregate aggregate, T @event)
        {
            try
            {
                await aggregate.Save(@event);
            }
            catch (EventStreamConcurrencyException)
            {
                await SaveEvent(aggregate, @event);
            }
        }
    }
}