using System;
using System.Threading.Tasks;
using POC.Documents.Commands;
using POC.Documents.Model;
using EventLite.Streams.StreamManager;
using System.Collections.Generic;

namespace POC.Documents
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IoC.RegisterServices();
            var commandHandler = new CommandHandler((IEventStreamReader)IoC.ServiceProvider.GetService(typeof(IEventStreamReader)));

            var streamId = Guid.Parse("e8512419-0183-42c9-b6df-0c4ea603ad00");
            Guid documentId;

            var quit = false;
            while (!quit)
            {
                var key = UI();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        documentId = Guid.NewGuid();
                        var command = new CreateDocument
                        {
                            StreamId = streamId,
                            Artifacts = new Dictionary<string,string>(),
                            Name = Guid.NewGuid().ToString()
                        };
                        await commandHandler.HandleCreateDocument(command);
                        Console.WriteLine();
                        Console.WriteLine($"Document created with ID: {documentId}");
                        Console.WriteLine();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine();
                        Console.WriteLine("Please, inform Id of the document to update:");
                        documentId = Guid.Parse(Console.ReadLine());
                        Console.WriteLine("Please, inform the new name of the dcocument:");
                        var name = Console.ReadLine();
                        var commandUpdate = new RenameDocument
                        {
                            StreamId = streamId,
                            Name = name
                        };
                        await commandHandler.HandleUpdateDocument(commandUpdate);
                        Console.WriteLine();
                        Console.WriteLine($"Document updated");
                        Console.WriteLine();
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine();
                        Console.WriteLine("Please, inform Id of the document to delete:");
                        documentId = Guid.Parse(Console.ReadLine());
                        var commandDelete = new DeleteDocument
                        {
                            StreamId = streamId,
                        };
                        Console.WriteLine();
                        Console.WriteLine($"Document deleted");
                        Console.WriteLine();
                        break;
                    case ConsoleKey.Q:
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

        private static ConsoleKeyInfo UI()
        {
            Console.WriteLine("Wich command you want to send?");
            Console.WriteLine("1 - Create Document");
            Console.WriteLine("2 - Update Document");
            Console.WriteLine("3 -Delete Document");
            Console.WriteLine("Q - Quit");
            Console.WriteLine();
            return Console.ReadKey();
        }
    }
}
