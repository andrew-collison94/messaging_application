using System;
namespace ABC_Messaging_Application
{
    public class Login
    {
        public static Agent LoginAttempt()
        {
            Agent agent = new Agent();
            try
            {
                SQLAccess.SQLConnectionOpen();
                Console.WriteLine("Default Username = kgiorgione0");
                Console.WriteLine("Default Password = 4nixvyHM");
                Console.Write("Enter Username: ");
                string usernameInserted = Console.ReadLine();
                Console.Write("Enter Password: ");
                string passwordInserted = Console.ReadLine();
                agent = SQLAccess.LoginToSQL(usernameInserted, passwordInserted);
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
            return agent;
        }
    }
}
