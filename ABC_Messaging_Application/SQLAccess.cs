using System;
using System.Data.SqlClient;
using System.Text;

namespace ABC_Messaging_Application
{
    class SQLAccess
    {
        private static string connectionString =
            "Data Source=localhost,1401;" +
            "Initial Catalog=master;" +
            "User id=sa;" +
            "Password=Mormoloc5;";
        private static SqlConnection sqlConnection;

        public static void SQLConnectionOpen()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public static void SQLConnectionClose()
        {
            sqlConnection.Close();
        }

        public static void SQLConnectionDispose()
        {
            sqlConnection.Dispose();
        }

        public static void AgentToSQL(int AgentID, string Name, string Lastname, string Telephone, string Email, string Username, string Password, string Role, string Region)
        { 
            SqlCommand cmdInsert = new SqlCommand($"INSERT INTO agents (AgentID, Name, Lastname, Telephone, Email, Role, Region, Username, Password) VALUES('{AgentID}', '{Name}', '{Lastname}', '{Telephone}', '{Email}', '{Role}', '{Region}', '{Username}', '{Password}')", sqlConnection);
            int rowsInserted = cmdInsert.ExecuteNonQuery();
            if (rowsInserted > 0)
            { 
                Console.WriteLine("Agent added successfully");
            }
        }

        public static void MessageToSQL(int MessageID, DateTime TimeStamp, string SenderUsername, string ReceiverUsername, string MessageContents)
        {
            SqlCommand cmdInsert = new SqlCommand($"INSERT INTO messages (MessageID, TimeStamp, SenderUsername, ReceiverUsername, MessageContents) VALUES('{MessageID}','{TimeStamp}', '{SenderUsername}', '{ReceiverUsername}', '{MessageContents}')", sqlConnection);
            int rowsInserted = cmdInsert.ExecuteNonQuery();
            if (rowsInserted > 0)
            {
                Console.WriteLine("Message Sent");
            }
        }

        public static void EditContactInfo(string updateInfo, int AgentID, string newUpdateInfo)
        {
            SqlCommand cmdUpdate = new SqlCommand($"UPDATE agents SET {updateInfo} = '{newUpdateInfo}' WHERE ID = '{AgentID}'", sqlConnection);
            int rowsUpdated = cmdUpdate.ExecuteNonQuery();
            if (rowsUpdated > 0)
            {
                Console.WriteLine("Contact Edit Saved");
            }
        }

        public static void UpdateMessage(string SenderUsername, string ReceiverUsername, DateTime TimeStamp, string newMessageContents)
        {
            SqlCommand cmdUpdate = new SqlCommand($"UPDATE messages SET MessageContents = '{newMessageContents}' WHERE TimeStamp = '{TimeStamp}' AND SenderUsername = '{SenderUsername}' AND ReceiverUsername = '{ReceiverUsername}'", sqlConnection);

            int rowsUpdated = cmdUpdate.ExecuteNonQuery();
            if (rowsUpdated > 0)
            {
                Console.WriteLine("Message Edit Saved");
            }
        }

        public static void DeleteAgent(int IDForDelete)
        {
            SqlCommand cmdDelete = new SqlCommand($"DELETE FROM agents WHERE AgentID = '{IDForDelete}'", sqlConnection);
            int rowsDeleted = cmdDelete.ExecuteNonQuery();
            if (rowsDeleted > 0)
            {
                Console.WriteLine("Agent Deleted");
            }
        }

        public static void DeleteMessage(string SenderUsername, string ReceiverUsername, DateTime TimeStamp)
        {
            SqlCommand cmdDelete = new SqlCommand($"DELETE FROM messages WHERE SenderUsername = '{SenderUsername}' AND ReceiverUsername = '{ReceiverUsername}' AND TimeStamp = '{TimeStamp}'", sqlConnection);
            int rowsDeleted = cmdDelete.ExecuteNonQuery();
            if (rowsDeleted > 0)
            {
                Console.WriteLine("Message Deleted");
            }
        }

        public static void ChooseRecipient(string Username) //Displays list of all potential recipients of a message
        {
            SqlCommand cmdSelect = new SqlCommand($"SELECT Name, Lastname, Username FROM agents WHERE Username != '{Username}'", sqlConnection);
            SqlDataReader reader = cmdSelect.ExecuteReader();

            while (reader.Read())
            {
                Agent agent = new Agent
                {
                    Name = reader.GetString(0),
                    Lastname = reader.GetString(1),
                    Username = reader.GetString(2)
                };

                StringBuilder sb = new StringBuilder();
                sb
                    .AppendLine($"Name: {agent.Name}")
                    .AppendLine($"Lastname: {agent.Lastname}")
                    .AppendLine($"Username: {agent.Username}");

                Console.WriteLine(sb);
            }
            reader.Close();
        }

        public static void ExistentSenderInMessages(string SenderUsername, string Username)
        {
            SqlCommand cmdSelect = new SqlCommand($"SELECT COUNT(*) FROM agents WHERE Username = @Username", sqlConnection);
            cmdSelect.Parameters.AddWithValue("@Username", $"{Username}");
            int result = (int)cmdSelect.ExecuteScalar();

            if (result > 0)
            {
                Console.WriteLine("Agent Found");
            }
            else
            {
                Console.WriteLine("New Message");
                Message.CreateMessage(SenderUsername);
            }
        }

        public static void ExistentRecipientInMessages(string SenderUsername, string ReceiverUsername)
        {
            SqlCommand cmdSelect = new SqlCommand($"SELECT COUNT(*) FROM messages WHERE ReceiverUsername = @ReceiverUsername", sqlConnection);
            cmdSelect.Parameters.AddWithValue("@Username", $"{ReceiverUsername}");
            int result = (int)cmdSelect.ExecuteScalar();

            if (result > 0)
            {
                Console.WriteLine("Message Found");
            }
            else
            {
                Console.WriteLine("Message Not Found");
                Message.EditMessage(SenderUsername);
            }
        }

        public static Agent LoginToSQL (string Username, string Password)
        {
            SqlCommand cmdLogin = new SqlCommand($"SELECT COUNT(*) FROM agents WHERE Username = @Username AND Password = @Password", sqlConnection);
            cmdLogin.Parameters.AddWithValue("@Username", $"{Username}");
            cmdLogin.Parameters.AddWithValue("@password", $"{Password}");
            int result = (int)cmdLogin.ExecuteScalar();
            Agent agent = new Agent();
            if (result > 0)
            {
                Console.WriteLine("Success");
                agent = PopulateUserInfo(Username);
            }
            else
            {
                Console.WriteLine("Attempt Failed");
                agent = Login.LoginAttempt();
            }
            return agent;
        }

        public static Agent PopulateUserInfo(string Username)
        {
            SqlCommand cmdSelect = new SqlCommand($"SELECT AgentID, Name, Lastname, Region, Telephone, Email, Role, Username FROM agents WHERE Username = @Username", sqlConnection);
            cmdSelect.Parameters.AddWithValue("@Username", $"{Username}");

            SqlDataReader reader = cmdSelect.ExecuteReader();

            Agent agent = new Agent();

            while (reader.Read())
            {
                agent.AgentID = reader.GetInt32(0);
                agent.Name = reader.GetString(1);
                agent.Lastname = reader.GetString(2);
                agent.Region = reader.GetString(3);
                agent.Telephone = reader.GetString(4);
                agent.Email = reader.GetString(5);
                agent.Role = reader.GetString(6);
                agent.Username = reader.GetString(7);

                Console.WriteLine(agent);
            }
            reader.Close();
            return agent;
        }

        public static void ViewMessage(string Username)
        {
            SqlCommand cmdView = new SqlCommand($"SELECT MessageID, TimeStamp, SenderUsername, ReceiverUsername, MessageContents FROM messages WHERE RecieverUsername = '{Username}'", sqlConnection);
            SqlDataReader reader = cmdView.ExecuteReader();
            while (reader.Read())
            {
                Message message = new Message();
                message.MessageID = reader.GetInt32(0);
                message.TimeStamp = reader.GetDateTime(1);
                message.SenderUsername = reader.GetString(2);
                message.ReceiverUsername = reader.GetString(3);
                message.MessageContents = reader.GetString(4);

                Console.WriteLine(message);
            }
            reader.Close();
        }

        public static void ViewAllMessages()
        {
            SqlCommand cmdView = new SqlCommand($"SELECT MessageID, TimeStamp, SenderUsername, ReceiverUsername, MessageContents FROM messages", sqlConnection);
            SqlDataReader reader = cmdView.ExecuteReader();
            while (reader.Read())
            {
                Message message = new Message();
                message.MessageID = reader.GetInt32(0);
                message.TimeStamp = reader.GetDateTime(1);
                message.SenderUsername = reader.GetString(2);
                message.ReceiverUsername = reader.GetString(3);
                message.MessageContents = reader.GetString(4);

                Console.WriteLine(message);
            }
            reader.Close();
        }

        public static void ViewAllAgents()
        {
            SqlCommand cmdSelect = new SqlCommand($"SELECT AgentID, Name, Lastname, Region, Telephone, Email, Role, Username FROM agents", sqlConnection);
            SqlDataReader reader = cmdSelect.ExecuteReader();
            while (reader.Read())
            {
                Agent agent = new Agent();

                agent.AgentID = reader.GetInt32(0);
                agent.Name = reader.GetString(1);
                agent.Lastname = reader.GetString(2);
                agent.Region = reader.GetString(3);
                agent.Telephone = reader.GetString(4);
                agent.Email = reader.GetString(5);
                agent.Role = reader.GetString(6);
                agent.Username = reader.GetString(7);

                Console.WriteLine(agent);
            }
            reader.Close();
        }
    }
}

