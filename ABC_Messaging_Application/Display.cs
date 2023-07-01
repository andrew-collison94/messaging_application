using System;
namespace ABC_Messaging_Application
{
    public class Display
    {
        public static void MainMenu(Agent agent)
        { 
            Console.WriteLine("ABC's Messaging System");
            Console.WriteLine();
            Console.WriteLine("Enter 'agent' for Agent Menu");
            Console.WriteLine("Enter 'admin' Admin Menu");
            string agentOrAdmin = Console.ReadLine();
            if (agentOrAdmin == "agent")
            {
                Console.WriteLine("ABC's Messaging System");
                Console.WriteLine();
                Console.WriteLine("Select an Option");
                Console.WriteLine();
                Console.WriteLine("Send Message:       Press 1");
                Console.WriteLine("Open Inbox:         Press 2");
                Console.WriteLine("Edit Message:       Press 3");
                Console.WriteLine("Delete Message:     Press 4");
            }
            else if (agentOrAdmin == "admin")
            {
                Console.WriteLine("Send Message:       Press 1");
                Console.WriteLine("Open Inbox:         Press 2");
                Console.WriteLine("Edit Message:       Press 3");
                Console.WriteLine("Delete Message:     Press 4");
                Console.WriteLine("View Any Message:   Press 5");
                Console.WriteLine("Edit Any Message:   Press 6");
                Console.WriteLine("Delete Any Message: Press 7");
                Console.WriteLine("Add Contact:        Press 8");
                Console.WriteLine("Edit Contact:       Press 9");
            }
            else
            {
                Console.WriteLine("Invalid Selection");
            }

            ConsoleKeyInfo selection = Console.ReadKey();
            switch (selection.KeyChar)
            {
                case '1':
                    Message.CreateMessage(agent.Username);
                    break;

                case '2':
                    Message.ViewMessage(agent.Username);
                    break;

                case '3':
                    Message.ViewMessage(agent.Username);
                    Message.EditMessage(agent.Username);
                    break;

                case '4':
                    Message.ViewMessage(agent.Username);
                    Message.DeleteMessage(agent.Username);
                    break;

                case '5':
                    Message.ViewAllMessages();
                    break;

                case '6':
                    Message.ViewAllMessages();
                    Message.EditAllMessages();
                    break;

                case '7':
                    Message.ViewAllMessages();
                    Message.DeleteAllMessages();
                    break;

                case '8':
                    Agent.CreateUser();
                    break;

                case '9':
                    Agent.ViewAllUsers();
                    Agent.EditContact();
                    break;

                default:
                    Console.WriteLine("Invalid Selection");
                    MainMenu(agent);
                    break;
            }
        }
    }
}
 
