using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

namespace ChatBot_Demo
{
    class Program
    {
        //credentials (suppressed for privacy)
        private static string login_name = "<LOGIN_NAME>";
        private static string token = Environment.GetEnvironmentVariable("Token");  //Token should be stored in a safe place
        private static List<string> channels_to_join = new List<string>(new string[] { "<CHANNEL_1>", "<CHANNEL_2>" });

        //main function
        static void Main(string[] args)
        {
            //Testing writing to line
            Console.WriteLine("Hello World!");

            //New up a List of TwitchChatBot objects
            List<TwitchChatBot> chatBots = new List<TwitchChatBot>();

            //add each channel to the list
            for (int i = 0; i < channels_to_join.Count; i++)
            {
                chatBots.Add(new TwitchChatBot(login_name, token, channels_to_join[i]));
            }

            //for each chatBot...
            for (int i = 0; i < chatBots.Count; i++)
            {
                //this chatBot
                TwitchChatBot chatBot = chatBots[i];

                //Connect to Twitch IRC
                chatBot.Connect();

                //Start Pinger
                Pinger pinger = new Pinger(chatBot);
                pinger.Start();
            }

            //Read message until we quit
            while (true)
            {
                //for each chatBot...
                for (int i = 0; i < chatBots.Count; i++)
                {
                    //this chatbot
                    TwitchChatBot chatBot = chatBots[i];

                    //if we get disconnected, reconnect
                    if (!chatBot.Client.Connected)
                    {
                        chatBot.Connect();
                    }
                    //else we're connected
                    else
                    {
                        //get the message that just came through
                        string msg = chatBot.ReadMessage();

                        //did we receive a message?
                        if (msg != "" && msg != null)
                        {
                            //write the raw message to the console
                            Console.WriteLine(msg);

                            //response string
                            string toRespond = "";

                            //If PING respond with PONG
                            if (msg.Length >= 4 && msg.Substring(0, 4) == "PING")
                                chatBot.SendPong();

                            //Trim the message to just the chat message piece
                            string msgTrimmed = trimMessage(msg);

                            //Handling commands
                            if (msgTrimmed.Length >= 6 && msgTrimmed.Substring(0, 6) == "!8ball")
                                toRespond = chatBot.Command_MagicEightBall();
                            else if (msgTrimmed == "!age")
                                toRespond = chatBot.Command_Age();
                            else if (msgTrimmed == "!discord")
                                toRespond = chatBot.Command_Discord();

                            //Write response to console and send message to chat
                            Console.WriteLine(toRespond);

                            //Send the message to chat
                            chatBot.SendMessage(toRespond);
                        }

                    }
                }

            }

        }

        #region Helper methods
        /// <summary>
        /// Trims an IRC message from chat to just the message that was sent in the chat
        /// </summary>
        /// <param name="message"></param>
        /// <returns>string</returns>
        public static string trimMessage(string message)
        {
            int indexOfSecondColon = getNthIndex(message, ':', 2);
            var result = message.Substring(indexOfSecondColon + 1);
            return result;
        }

        /// <summary>
        /// Gets the second colon, which is the seperator before the chat message
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        /// <returns>string</returns>
        public static int getNthIndex(string s, char t, int n)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion


    }
}
