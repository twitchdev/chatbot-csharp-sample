# CSharp-Chatbot-Sample
A sample IRC Twitch chatbot using written in C#. 

## What's in this sample
This sample demonstrates how to write a simple Twitch chatbot through IRC. The chatbot connects to a Twitch channel's chat via IRC, reads messages from chat, responds to a few simple commands, and maintains the connection by PINGing and PONGing as needed.

## How to edit this Bot and use it yourself
- Generate an OAuth Token per the instructions from the [Twitch IRC documentation](https://dev.twitch.tv/docs/irc/#building-the-bot)
- Create a new C# Console Application
- Download the TwitchChatBot.cs file
- Add the TwitchChatBot.cs file into your project
- Download the Pinger.cs file
- Add the Pinger.cs file into your project
- Download the Program.cs file
- Overwrite the existing Program.cs file in your project with the contents from this Program.cs file. 
- Update the namespace in your files as they need to match the name of your project rather than "Chatbot_Demo"
- Update the three variables in the Program.cs file with the credentials specific to you ("login_name" is the Twitch username of your bot, "token" is the token generated from step 1, and "channels_to_join" is the Twitch channel(s) chat you want your bot to join)
- Run the project. Open the channel you're testing chat with and talk to your bot
