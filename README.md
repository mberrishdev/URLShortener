# URL Shortener API

## Overview

The URL Shortener API is a RESTful service that allows users to shorten long URLs for easy sharing and management. This API can be integrated into various applications, providing a simple and effective way to generate short links that redirect to longer URLs.

## Features

- **Shorten URLs:** Generate a short link from a long URL.
- **Redirect:** Redirect users from a short link to the original long URL.
- **Analytics:** Track the number of clicks for each short URL.
- **Custom Short Links:** Optionally, users can create custom short links.
- **Delete Short Links:** Remove short links from the system.

## API Endpoints

### 1. Shorten URL

- **Endpoint:** `POST /v1/url`
- **Request Body:**
  ```json
  {
    "original": "https://example.com/some/long/url"
  }

- **Response Body:**
  ```json
  "https://example.com/some/long/url"

### 2. Redirect URL

- **Endpoint:** `GET /v1/url/{code}`
- **Description:** This endpoint redirects users from a short URL to the original long URL.
- **Path Parameter:**
  - `code`: The unique code for the shortened URL.
- **Response:** Redirects to the original long URL.

#### Example

- **Request:**
  ```http
  GET /v1/abc123 HTTP/1.1
  Host: yourdomain.com

- **Response:**
  - **Status Code:** `302 Found`
  - **Location:** `https://example.com/some/long/url`
  
- **Note:** If the short code does not exist, the API will return a `404 Not Found` status.

- ## Getting Started

### Prerequisites

- .NET SDK (version)
- A PostgreSQL or any other database (if applicable)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/mberrishdev/URLShortener.git
   
2. Navigate to the project directory:
   ```bash
   cd URLShortener

3. Restore dependencies:
   ```bash
   dotnet restore

4. Run the application:
   ```bash
   dotnet run

4. Open:
   ```bash
   [https://localhost:5000/swagger](https://localhost:5000/swagger/index.html)

### Usage

Use any HTTP client (like Postman or cURL) to interact with the API endpoints.

### Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue.

### Contact

For inquiries, please contact: [mikheil.berishvili@outlook.com]



