# YouTubeTools | .NET & C# Edition

A powerful web application built with **.NET 8** and **ASP.NET Core MVC** designed to help YouTube content creators optimize their SEO and manage thumbnails efficiently.

## 🚀 Key Features

- **SEO Tag Generator**: Analyze top-performing search results for any keyword/title to extract the most effective tags.
- **Metadata Retriever**: Fetch complete video information (Title, Description, Channel Info, Publication Date, and Tags) using just a URL or Video ID.
- **Thumbnail Management**: Extract and download high-resolution (1280x720) thumbnails from any public YouTube video.
- **Responsive Design**: Premium UI built with **Tailwind CSS**, featuring:
  - Full **Dark Mode** support.
  - One-click **Clipboard Integration** for tags.
  - Seamless mobile responsiveness.

## 🛠️ Technology Stack

- **Backend**: .NET 8, C#, ASP.NET Core MVC
- **API Integration**: YouTube Data API V3
- **Frontend**: Razor Pages, Tailwind CSS, Bootstrap Icons
- **HTTP Client**: Typed HttpClient for efficient API communication
- **Asynchronous Patterns**: full `async/await` implementation for non-blocking operations

## 📦 How to Use

1. **Clone the repository**:
   ```bash
   git clone https://github.com/MayuriN2024/YtRapidTags.git
   ```
2. **Setup API Key**:
   Open `appsettings.json` and ensure your `YouTube:ApiKey` is set.
3. **Run the project**:
   ```bash
   dotnet run
   ```
4. **Access the Web UI**:
   Open your browser to the URL provided in the terminal (usually `https://localhost:5001`).

## 📄 API Integration Details
The application uses the `YouTube Data API v3` to perform search queries and retrieve details. It extracts:
- `Snippet` (Title, Description, Channel, Date)
- `Tags` (Full SEO keyword list)
- `Thumbnails` (Max resolution URL)

---
*Developed as a modern migration from Java/Spring Boot to the .NET ecosystem.*
