# 🤖 Universal E-Commerce Web Scraper (.NET 8 / C#)

A modular, enterprise-grade web scraper built in C# (.NET 8) using **AngleSharp**. Designed with Clean Architecture and the **Strategy Pattern**, allowing seamless scraping of any e-commerce platform (books, electronics, retail) with zero modifications to the core engine.

---

## ✨ Features

- **AngleSharp Engine:** High-performance HTML parsing via standard CSS selectors.
- **Strategy Pattern (`IScraperStrategy`):** Decoupled web-parsing rules. Easily add target sites (e.g., electronics stores, book catalogs) by implementing a single interface.
- **LINQ Analytics & Summary:** Automated generation of dataset analytics (average price, price ranges, stock status, rating distribution).
- **Flexible Exporters:** Built-in JSON and CSV export engines via clean abstractions (`IDataExporter`).
- **Filtering System:** In-memory LINQ filtering by maximum price and minimum star rating.
- **WPF & Web Ready:** Architecture fully prepared for WPF MVVM binding or Dependency Injection container integration.

---

## 🏗️ Architecture Overview

```text
Core/
├── Abstractions/    --> Contracts for Engine, Strategies, Exporters
└── Models/          --> Immutable DTOs (ScrapedItemDto, SummaryDto)
Infrastructure/
├── Engine/          --> AngleSharp non-blocking HTTP scraper engine
├── Strategies/      --> Site-specific parsers (BooksToScrapeStrategy)
└── Exporters/       --> Data serialization (JSON, CSV)
Services/            --> Domain logic and LINQ statistics
```

---

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or newe

### Installation & Run

1. Clone the repository:
```bash
git clone [https://github.com/AdriansRepos/BooksToScrape.git](https://github.com/AdriansRepos/BooksToScrape.git)
```

2. Navigate to the project directory:
```bash
cd BookScraper
```

3. Run the application:
```bash
dotnet run
```

---

## 📊 Sample Output (JSON Export)

```json
[
  {
    "Title": "A Light in the Attic",
    "Price": 51.77,
    "Currency": "GBP",
    "RatingStars": 3,
    "IsInStock": true,
    "ProductUrl": "catalogue/a-light-in-the-attic_1000/index.html"
  }
]
```