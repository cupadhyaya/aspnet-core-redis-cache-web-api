# Redis Caching in ASP.NET Core Web API

A simple example demonstrating Redis Distributed Cache in ASP.NET Core Web API using the Cache-Aside pattern.

## Features

- ASP.NET Core Web API
- Redis Distributed Cache
- Cache-Aside Pattern
- Absolute Expiration
- JSON Serialization
- StackExchange.Redis

## Install Package

```bash
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
```

## Configure Redis

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "RedisCacheSettings": {
    "ConnectionString": "localhost:6379"
  }
}
```

## Register Redis Cache

### Program.cs

```csharp
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =
        builder.Configuration["RedisCacheSettings:ConnectionString"];

    options.InstanceName = "RedisCacheInstance";
});
```

## Cache Flow

```text
Client Request
      ↓
Check Redis Cache
      ↓
Cache Hit?
  Yes → Return Data

   No
    ↓
Fetch Data
    ↓
Store in Redis
    ↓
Return Response
```

This approach is known as the **Cache-Aside Pattern**.

## Cache Expiration Policies

### Absolute Expiration

```csharp
AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
```

### Sliding Expiration

```csharp
SlidingExpiration = TimeSpan.FromMinutes(5);
```

### Combination of Both

```csharp
new DistributedCacheEntryOptions
{
    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
    SlidingExpiration = TimeSpan.FromMinutes(10)
};
```

## API Endpoint

```http
GET /City
```

### Sample Response

```json
[
  "New York",
  "Los Angeles",
  "Chicago",
  "Houston",
  "Phoenix"
]
```

## Concepts Covered

- Redis Cache
- Distributed Cache
- Cache-Aside Pattern
- IDistributedCache
- Absolute Expiration
- Sliding Expiration
- JSON Serialization
- StackExchange.Redis
- Cache Eviction Policies (LRU, TTL)

## Technologies Used

- ASP.NET Core Web API
- C#
- Redis
- StackExchange.Redis

## Author

Chakrapani Upadhyaya
