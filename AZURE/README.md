# Azure Cloud Architecture

Asynchronous data management and real-time queries, built with Function Apps, Service Bus, Cosmos DB and API Management.

## Overview

This project consists of designing and implementing a cloud architecture on Microsoft Azure, built to handle two operation types separately and efficiently: asynchronous data writing and synchronous data querying. The goal was to build a decoupled, scalable and resilient system, using a message-queue-based model for the write flow and direct synchronous calls for the read flow.

The base infrastructure consists of a **Web App** acting as the user interface, connected to an **API Management (APIM)** instance that acts as the central gateway for routing calls. From this gateway, two independent flows branch out: a queue-based write flow and a bidirectional, synchronous read (query) flow. This separation conceptually mirrors a **CQRS** (Command Query Responsibility Segregation) pattern.

## Tech Stack

| Component | Azure Service | Role |
|---|---|---|
| User interface | Web App | Entry point for the end user |
| Gateway | API Management (APIM) | Central routing of calls to the two flows |
| Write reception | Function App (HTTP Trigger) | Receives incoming data, publishes it to the queue |
| Messaging | Service Bus (Queue) | Decouples data reception from persistence |
| Data persistence | Function App (Service Bus Trigger) | Reads from the queue, writes to Cosmos DB |
| Data query | Function App (HTTP Trigger) | Reads from Cosmos DB, returns results |
| Database | Cosmos DB (Core/SQL API) | NoSQL store, queried with SQL-like syntax |

## Write Flow (Queue-Based)

1. APIM routes the write request to the first Function App (HTTP trigger).
2. This function publishes the data as a message on the Service Bus queue.
3. A second Function App (Service Bus trigger) listens continuously on the queue.
4. As soon as a message arrives, it writes/overwrites the corresponding data in Cosmos DB.

This absorbs spikes in incoming requests without overloading the database, since the queue acts as a buffer.

## Query Flow (Bidirectional)

1. APIM routes the request to a third Function App, dedicated solely to queries.
2. The function receives the query parameters from the gateway.
3. Data is retrieved from Cosmos DB via the Core (SQL) API.
4. The result is returned to the user through the same bidirectional channel.

## Notes

- Cosmos DB was provisioned in the cheapest available tier for development/testing.
- The Function Apps' boilerplate code was generated with AI assistance (Claude) to speed up development, then reviewed and adapted.
- Build order: Resource Group → Service Bus → 3 Function Apps → Cosmos DB → API Management → Web App.
