using System;
using System.Text;

namespace ABC_Messaging_Application
{
    class Message
    {
        public DateTime TimeStamp { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public int MessageID;
        private string messageContents;
        public int characterLimit = 4000;


        public string MessageContents
        {
            get
            {
                return messageContents;
            }
            set
            {
                if (value.Length <= 0 || value.Length > characterLimit)
                {
                    throw new ArgumentOutOfRangeException("MessageContents", "Entry is outside allowed character limits");
                }

                messageContents = value;
            }
        }

        public Message()
        { }

        public Message(int messageID, DateTime timeStamp, string senderUsername, string receiverUsername)
        {
            MessageID = messageID;
            TimeStamp = Convert.ToDateTime(timeStamp);
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
        }

        public override string ToString()
        {
            StringBuilder messageDetails = new StringBuilder();
            messageDetails
                .Append($"MessageID: {MessageID}")
                .Append($"TimeStamp: {TimeStamp}\t")
                .Append($"Sender: {SenderUsername}\t")
                .Append($"Receiver: {ReceiverUsername}\t")
                .Append($"MessageData: {MessageContents}");


            return messageDetails.ToString();
        }


        public static void CreateMessage(string Username)
        {
            try
            {
                SQLAccess.SQLConnectionOpen();
                Message message = new Message();

                SQLAccess.ChooseRecipient(Username);
                Console.WriteLine("Select the Recipient");
                message.ReceiverUsername = Console.ReadLine();

                SQLAccess.ExistentSenderInMessages(Username, message.ReceiverUsername);

                message.MessageID = new Random().Next(1000, 9999);
                message.TimeStamp = DateTime.Now;
                message.ReceiverUsername = Username;
                
                
                Console.WriteLine("Write your Message: ");
                message.MessageContents = Console.ReadLine();


                SQLAccess.MessageToSQL(message.MessageID, message.TimeStamp, message.SenderUsername, message.ReceiverUsername, message.MessageContents);

            }
            catch (Exception badLength)
            {
                Console.WriteLine(badLength.Message);
            }
            finally
            {
                SQLAccess.SQLConnectionClose();
                SQLAccess.SQLConnectionDispose();
            }
        }

        public static void ViewMessage(string Username)
        {
            try
            {
                SQLAccess.SQLConnectionOpen();

                SQLAccess.ViewMessage(Username);

            }
            catch (Exception badLength)
            {
                Console.WriteLine(badLength.Message);
            }
            finally
            {
                SQLAccess.SQLConnectionClose();
                SQLAccess.SQLConnectionDispose();
            }
        }

        public static void ViewAllMessages()
        {
            try
            {
                SQLAccess.SQLConnectionOpen();
                SQLAccess.ViewAllMessages();
            }
            catch (Exception badLength)
            {
                Console.WriteLine(badLength.Message);
            }
            finally
            {
                SQLAccess.SQLConnectionClose();
                SQLAccess.SQLConnectionDispose();
            }
        }

        public static void EditMessage(string SenderUsername)
        {
            try
            {
                SQLAccess.SQLConnectionOpen();

                Console.Write("Enter the recipient's username whose message is to be edited: ");
                string ReceiverUsername = Console.ReadLine();

                Console.Write("Enter the date the message was sent (YYYY-MM-DD): ");
                DateTime timeStamp = DateTime.Parse(Console.ReadLine());

                Console.Write("New message: ");
                string newMessage = Console.ReadLine();

                SQLAccess.ExistentRecipientInMessages(SenderUsername, ReceiverUsername);

                SQLAccess.UpdateMessage(SenderUsername, ReceiverUsername, timeStamp, newMessage);
            }
            catch (Exception badLength)
            {
                Console.WriteLine(badLength.Message);
            }
            finally
            {
                SQLAccess.SQLConnectionClose();
                SQLAccess.SQLConnectionDispose();
            }
        }

        public static void EditAllMessages()
        {
            try
            {
                SQLAccess.SQLConnectionOpen();


                Console.Write("Enter the sender's username whose message is to be edited: ");
                string SenderUsername = Console.ReadLine();

                Console.Write("Enter the recipient's username whose message is to be edited: ");
                string ReceiverUsername = Console.ReadLine();


                Console.Write("Enter the date the message was sent (YYYY-MM-DD): ");
                var timeStamp = DateTime.Parse(Console.ReadLine());



                Console.Write("New message: ");
                string newMessage = Console.ReadLine();

                SQLAccess.ExistentRecipientInMessages(SenderUsername, ReceiverUsername);



                SQLAccess.UpdateMessage(SenderUsername, ReceiverUsername, timeStamp, newMessage);
            }
            catch (Exception badLength)
            {
                Console.WriteLine(badLength.Message);
            }
            finally
            {
                SQLAccess.SQLConnectionClose();
                SQLAccess.SQLConnectionDispose();
            }
        }

        public static void DeleteMessage(string username)
        {
            try
            {
                SQLAccess.SQLConnectionOpen();

                Console.Write("Enter the recipient's username whose message is to be deleted: ");
                string ReceiverUsername = Console.ReadLine();

                Console.Write("Enter the date the message was sent (YYYY-MM-DD): ");
                DateTime timeStamp = DateTime.Parse(Console.ReadLine());

                SQLAccess.DeleteMessage(username, ReceiverUsername, timeStamp);
            }
            catch (Exception badLength)
            {
                Console.WriteLine(badLength.Message);
            }
            finally
            {
                SQLAccess.SQLConnectionClose();
                SQLAccess.SQLConnectionDispose();
            }
        }

        public static void DeleteAllMessages()
        {
            try
            {
                SQLAccess.SQLConnectionOpen();

                Console.Write("Enter the sender's username whose message is to be deleted: ");
                string SenderUsername = Console.ReadLine();

                Console.Write("Enter the recipient's username whose message is to be deleted: ");
                string ReceiverUsername = Console.ReadLine();

                Console.Write("Enter the date the message was sent (YYYY-MM-DD): ");
                var timeStamp = DateTime.Parse(Console.ReadLine());

                SQLAccess.DeleteMessage(SenderUsername, ReceiverUsername, timeStamp);
            }
            catch (Exception badLength)
            {
                Console.WriteLine(badLength.Message);
            }
            finally
            {
                SQLAccess.SQLConnectionClose();
                SQLAccess.SQLConnectionDispose();
            }
        }
    }
}


