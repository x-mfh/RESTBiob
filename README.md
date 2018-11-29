<h1 align="center">
  <br>
 Restful API
  <br>
</h1>

<p align="center">
  <a href="#about">About</a> •
  <a href="#getting-started">Getting Started</a> •
  <a href="#api-endpoints">API Endpoints</a> •
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

If you however want the API, you will need your own local DB
// MANGLER

## API endpoints
Example on URL startpoint
https://localhost:PORT/api/v1/movies/{movieId}/
#### Movie Resources
| Request | Resources                       |
|:--------|:--------------------------------|
| `GET`   | /movies |
| `POST`  | /movies |
| `GET`   | /movies/:id |
| `PATCH` | /movies/:id |
| `DELETE`| /movies/:id |
| `DELETE`| /movies/:id |
#### Genre sources
- <code>GET</code> movies/:id/showtimes/:id
- <code>POST</code> movies/:id/showtimes/:id
- <code>GET</code> movies/:id/showtimes/:id
- <code>PATCH</code> movies/:id/showtimes/:id
- <code>DELETE</code> movies/:id/showtimes/:id
#### Showtime Resources
- <code>GET</code> movies/:id/showtimes
- <code>POST</code> movies/:id/showtimes
- <code>GET</code> movies/:id/showtimes/:id
- <code>PATCH</code> movies/:id/showtimes/:id
- <code>DELETE</code> movies/:id/showtimes/:id
#### Ticket sources
- <code>GET</code> movies/:id/showtimes/:id/tickets
- <code>POST</code> movies/:id/showtimes/id/tickets
- <code>GET</code> movies/:id/showtimes/:id/tickets/:id
- <code>PATCH</code> movies/:id/showtimes/:id/tickets/:id
- <code>DELETE</code> movies/:id/showtimes/:id/tickets/:id
#### Hall sources
- <code>GET</code> halls/
- <code>POST</code> halls/
- <code>GET</code> halls/:id
- <code>PATCH</code> halls/:id
- <code>DELETE</code> halls/:id
#### Seat sources
- <code>GET</code> halls/:id/seats
- <code>POST</code> halls/:id/seats
- <code>GET</code> halls/:id/seats/:id
- <code>PATCH</code> halls/:id/seats/:id
- <code>DELETE</code> halls/:id/seats/:id
## API string queries

## Authors
- BalenD - [BalenD](https://github.com/BalenD)
- Mikkel - [x-mfh](https://github.com/x-mfh)
- Jason - [Sabeniano](https://github.com/Sabeniano)
- Bjarke - [Bjarke22](https://github.com/Bjarke22)
