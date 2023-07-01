# ABC Messaging Application

## Overview

The ABC Messaging Application is a C#-based messaging platform that allows users to manage messages and contacts. The application provides different functionalities depending on the user role - agent or admin. Users need to login to the system to access these features.

## Features

### Login

- **Default Username:** kgiorgione0
- **Default Password:** 4nixvyHM

Users can log in using their username and password. The system provides a default username and password for initial login.

### Common Features for Agents and Admins

- **Send Message:** Allows users to send a new message. The message content is limited to 4000 characters.
- **Open Inbox:** Displays all the messages received by the user.
- **Edit Message:** Gives the option to edit any message sent by the user.
- **Delete Message:** Enables the user to delete any of their messages.

### Additional Features for Admins

- **View Any Message:** Provides an overview of all the messages in the system.
- **Edit Any Message:** Grants the ability to edit any message in the system.
- **Delete Any Message:** Allows for the deletion of any message in the system.
- **Add Contact:** Enables the addition of a new user profile with complete information.
- **Edit Contact:** Provides an option to update any user information.

## Installation

To install and run this application, you'll need [dotnet](https://dotnet.microsoft.com/download) installed on your computer. From your command line:

```bash
# Clone this repository
$ git clone https://github.com/user/repo
# Go into the repository
$ cd repo
# Install dependencies
$ dotnet restore
# Run the app
$ dotnet run
