using DocumentsApproval.Commands;
using DocumentsApproval.Model;
using EventLite.Streams.StreamManager;
using System.Threading.Tasks;

namespace DocumentsApproval
{
    public class CommandHandler
    {
        private readonly IEventStreamReader _eventStreamReader;

        public CommandHandler(IEventStreamReader eventStreamReader)
        {
            _eventStreamReader = eventStreamReader;
        }

        public async Task HandleApproveDocument(ApproveDocument command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var resultedEvent = aggregate.ApproveDocument(command);
            await aggregate.Save(resultedEvent);
        }

        public async Task HandleCreateDocument(CreateDocument command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var resultedEvent = aggregate.CreateDocument(command);
            await aggregate.Save(resultedEvent);
        }

        public async Task HandleDeleteDocument(DeleteDocument command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var @event = aggregate.DeleteDocument(command);
            await aggregate.Save(@event);
        }

        public async Task HandleRejectDocument(RejectDocument command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var @event = aggregate.RejectDocument(command);
            await aggregate.Save(@event);
        }

        public async Task HandleRenameDocument(RenameDocument command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var @event = aggregate.RenameDocument(command);
            await aggregate.Save(@event);
        }

        public async Task HandleUpdateArtifacts(UpdateArtifacts command)
        {
            var aggregate = await _eventStreamReader.GetAggregate<DocumentAggregate>(command.StreamId);
            var @event = aggregate.UpdateArtifacts(command);
            await aggregate.Save(@event);
        }
    }
}