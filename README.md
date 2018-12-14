<h1 align="center">
  <br>
 Restful API
  <br>
</h1>

<p align="center">
  <a href="#about">About</a> •
  <a href="#getting-started">Getting Started</a> •
  <a href="#authors">Authors</a>
</p>

## About
This document provides simple information regarding our API.

The API is mainly used for our project regarding Bio B, and is supposed to handle CRUD operations to a "imaginary" cinema.

___This is a school project and is mainly made for us to gain knowlegde in the various technologies, while also being effective in best practices.___

## Getting Started
To clone and run the API, you'll need [Visual Studio](https://visualstudio.microsoft.com/vs/community/) installed on your computer.

From your command line:

```bash
# Clone this repository
$ git clone https://github.com/BalenD/Restbiob
```

if you want to run the api you will also need to
1. open the solution
2. open the package manager console
3. set "Default Project" on PMC to Biob.data.data
4. run the command

```powershell
# Update local db to match migrations
PM> update-database
```

in order to run the react (without the asp.net core server rendering)
1. navigate to Biob.Web.Client project
2. navigate to ClientApp
2,5. and from here on you can open that folder in VS code
3. in order to start run the command

```bash
# Start the frontend client
$ npm start
```

NOTE: remember to start the API and Oauth2 server at once in order for everything to work

## Authors
- BalenD - [BalenD](https://github.com/BalenD)
- Mikkel - [x-mfh](https://github.com/x-mfh)
- Jason - [Sabeniano](https://github.com/Sabeniano)
- Bjarke - [Bjarke22](https://github.com/Bjarke22)
