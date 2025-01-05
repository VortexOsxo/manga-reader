# MangaReader

MangaReader is a WPF application for reading manga, with a server backend to manage user authentication, manga data, and user preferences.

## Table of Contents

- Features
- Installation
- License

## Features

- User authentication (login and signup)
- Browse and read manga
- Bookmark and favorite manga
- Auto-scroll feature for reading manga
- Profile page to view reading history and favorites

## Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Python 3.10](https://www.python.org/downloads/release/python-3100/)

### Client Setup

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/MangaReader.git
    cd MangaReader/client
    ```

2. Restore the .NET dependencies:
    ```sh
    dotnet restore
    ```

3. Build the project:
    ```sh
    dotnet build
    ```

4. Run the application:
    ```sh
    dotnet run --project MangaReader.csproj
    ```

### Server Setup

1. Navigate to the server directory:
    ```sh
    cd ../server
    ```

2. Create a virtual environment and activate it:
    ```sh
    python -m venv venv
    source venv/bin/activate  # On Windows use `venv\Scripts\activate`
    ```

3. Install the Python dependencies:
    ```sh
    pip install -r requirements.txt
    ```

4. Run the server in development mode:
    ```sh
    flask --app app run --host=0.0.0.0
    ```

## License

This project is licensed under the MIT License.