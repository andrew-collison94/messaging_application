﻿using System;

namespace ABC_Messaging_Application
{
    class Program
    {
        public static void Main()
        {
            Agent agent = new Agent();
            agent = Login.LoginAttempt();
            Display.MainMenu(agent);

        }
    }
}

        









