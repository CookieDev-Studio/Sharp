# Sharp

## Table of contents
* [Introduction](#introduction)
* [Technologies](#technologies)
* [Setup](#setup)
* [Notes](#some-notes)
* [Upcoming](#upcoming)

## Introduction
Sharp is a moderation bot for Discord. Sharp includes functionality for assigning strikes to unruly users and customizing what prefix your commands use (ex. '-' or '/').
	
## Technologies
You will need the following tech installed on your machine to run this program locally.

* .NET Core: 3.1
* PostgreSQL: 12.3
* Visual Studio 2019 Community or VSCode
* pgAdmin if you want a GUI for the database (latest version should be fine).
	
## Setup

1. Create a fresh PostgreSQL database locally.
2. Use the database backup in the repo to create the schema for your local database.
3. Open up the Sharp solution in Visual Studio. Alternatively, run `$ code .` in the solution directory if you want to use VSCode.
4. In Sharp.Data, add your database connection info to the connection string in `SQLConnectionString.txt`.
5. Create your Discord bot on https://discord.com/developers/applications/
6. Run the application (`dotnet run` in the `Sharp` project if you're using the CLI). It will ask you for your Discord bot token information and whatnot in a console. Input it and a `token.txt` file will be created for you storing the information. If you want to keep it private, ensure it's in the `.gitignore` file.
7. You should now be up and running! Make sure to invite your bot to a server, and then you should be able to send it commands! Try `-help` to see what you can do!

## Some Notes

The application follows a standard N-tier architecture. There is a Sharp.Service project and a Sharp.Data project that handle their usual responsibilites. The Sharp project is the actual Bot code that listens for commands in a Discord server and calls into the Service for the command. The upcoming Sharp.WebApi and Sharp.Web projects will handle the web portion of the application, and Sharp.WebApi will also leverage Sharp.Service.

We are using Dapper, not Entity Framework.

Sharp.Service includes a lot of parallel synchronous and asynchronous methods. Use whichever you prefer (but try not to create blocking code!).

If you like what you see, please consider [donating to us on Patreon](https://www.patreon.com/cookiedevstudio)!

## Upcoming

Cookie Dev is developing a message-logging feature for Sharp. It will allow servers to opt-in to message tracking. Every message sent in enabled servers will be saved to a database regardless of if the message was edited or deleted, allowing moderators to view existing conversations if someone was breaking rules (and had subsequently altered their erroneous messages). Moderators will be able to log into a dashboard application on the web where they can view existing messages.
