using DocumentsApproval.Commands;
using EventLite.Streams.StreamManager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentsApproval
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IoC.RegisterServices();
            var commandHandler = new CommandHandler((IEventStreamReader)IoC.ServiceProvider.GetService(typeof(IEventStreamReader)));

            var streamId = Guid.Parse("e8512419-0183-42c9-b6df-0c4ea603ad00");
            Guid documentId;

            var quit = false;
            while (!quit)
            {
                var key = UI();

                switch (key)
                {
                    case "1":
                        var command = new CreateDocument
                        {
                            StreamId = streamId,
                            Artifacts = new Dictionary<string, string>(),
                            Name = Guid.NewGuid().ToString()
                        };
                        await commandHandler.HandleCreateDocument(command);
                        Console.WriteLine();
                        Console.WriteLine("Document created.");
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.WriteLine();
                        Console.WriteLine("Please, inform the new name of the dcocument:");
                        var name = Console.ReadLine();
                        var commandRename = new RenameDocument
                        {
                            StreamId = streamId,
                            Name = name
                        };
                        await commandHandler.HandleRenameDocument(commandRename);
                        Console.WriteLine();
                        Console.WriteLine($"Document renamed");
                        Console.WriteLine();
                        break;

                    case "3":
                        Console.WriteLine();
                        var commandUpdate = new UpdateArtifacts
                        {
                            StreamId = streamId,
                            AddArtifacts = new Dictionary<string, string>
                            {
                                { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
                            },
                            RemoveArtifacts = new List<string>()
                        };
                        await commandHandler.HandleUpdateArtifacts(commandUpdate);
                        Console.WriteLine();
                        Console.WriteLine($"Artifacts Updated!");
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.WriteLine();
                        Console.WriteLine("Please, inform the approver name");
                        var approverName = Console.ReadLine();
                        var commandApprove = new ApproveDocument
                        {
                            StreamId = streamId,
                            Approver = approverName
                        };
                        await commandHandler.HandleApproveDocument(commandApprove);
                        Console.WriteLine();
                        Console.WriteLine($"Document approved");
                        Console.WriteLine();
                        break;

                    case "5":
                        Console.WriteLine();
                        Console.WriteLine("Please, inform the approver name");
                        var rejecterName = Console.ReadLine();
                        var commandReject = new RejectDocument
                        {
                            StreamId = streamId,
                            Rejecter = rejecterName
                        };
                        await commandHandler.HandleRejectDocument(commandReject);
                        Console.WriteLine();
                        Console.WriteLine($"Document rejected");
                        Console.WriteLine();
                        break;

                    case "6":
                        Console.WriteLine();
                        var commandDelete = new DeleteDocument
                        {
                            StreamId = streamId
                        };
                        await commandHandler.HandleDeleteDocument(commandDelete);
                        Console.WriteLine();
                        Console.WriteLine($"Document deleted");
                        Console.WriteLine();
                        break;

                    case "Q":
                        quit = true;
                        break;

                    case "q":
                        quit = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid option, chose a valid command");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private static string UI()
        {
            Console.WriteLine("Wich command you want to send?");
            Console.WriteLine("1 - Create Document");
            Console.WriteLine("2 - Rename Document");
            Console.WriteLine("3 - Update Artifacts");
            Console.WriteLine("4 - Approve Document");
            Console.WriteLine("5 - Reject Document");
            Console.WriteLine("6 - Delete Document");
            Console.WriteLine("Q - Quit");
            Console.WriteLine();
            return Console.ReadLine();
        }
    }
}