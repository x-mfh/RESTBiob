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
| `GET`   | /movies/{movieId} |
| `PATCH` | /movies/{movieId} |
| `DELETE`| /movies/{movieId} |
#### Genre sources
| Request | Resources                       |
|:--------|:--------------------------------|
| `GET`   | /movies/{movieId}/genres |
| `POST`  | /movies/{movieId}/genres |
| `GET`   | /movies/{movieId}/genres/{genreId} |
| `PATCH` | /movies/{movieId}/genres/{genreId} |
| `DELETE`| /movies/{movieId}/genres/{genreId} |
#### Showtime Resources
| Request | Resources                       |
|:--------|:--------------------------------|
| `GET`   | /movies/{movieId}/showtimes |
| `POST`  | /movies/{movieId}/showtimes |
| `GET`   | /movies/{movieId}/showtimes/{showtimeId} |
| `PATCH` | /movies/{movieId}/showtimes/{showtimeId} |
| `DELETE`| /movies/{movieId}/showtimes/{showtimeId} |
#### Ticket sources
| Request | Resources                       |
|:--------|:--------------------------------|
| `GET`   | /movies/{movieId}/showtimes/{showtimeId}/tickets |
| `POST`  | /movies/{movieId}/showtimes/{showtimeId}/tickets |
| `GET`   | /movies/{movieId}/showtimes/{showtimeId}/tickets/{ticketId} |
| `PATCH` | /movies/{movieId}/showtimes/{showtimeId}/tickets/{ticketId} |
| `DELETE`| /movies/{movieId}/showtimes/{showtimeId}/tickets/{ticketId} |
#### Hall sources
| Request | Resources                       |
|:--------|:--------------------------------|
| `GET`   | /halls |
| `POST`  | /halls |
| `GET`   | /halls/{hallId} |
| `PATCH` | /halls/{hallId} |
| `DELETE`| /halls/{hallId} |
#### Seat sources
| Request | Resources                       |
|:--------|:--------------------------------|
| `GET`   | /halls/{hallId}/seats |
| `POST`  | /halls/{hallId}/seats |
| `GET`   | /halls/{hallId}/seats/{seatId} |
| `PATCH` | /halls/{hallId}/seats/{seatId} |
| `DELETE`| /halls/{hallId}/seats/{seatId} |
## API string queries

## Authors
- BalenD - [BalenD](https://github.com/BalenD)
- Mikkel - [x-mfh](https://github.com/x-mfh)
- Jason - [Sabeniano](https://github.com/Sabeniano)
- Bjarke - [Bjarke22](https://github.com/Bjarke22)
