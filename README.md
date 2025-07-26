# Microsoft Back-End Developer Professional Certificate Final Project

This repository contains **LogiTrack**, a .NET Web API developed using the **Clean Architecture** approach. The solution is organized into four main layers: **Domain**, **Application**, **Infrastructure**, and **Presentation (API)**. This project serves as the final requirement for earning the [Microsoft Back-End Developer Professional Certificate](https://www.coursera.org/professional-certificates/microsoft-back-end-developer).

## Project Overview

**LogiTrack** is designed to serve as a foundational logistics platform. Its core features include:

- **Order Management**
- **Inventory Item Management**
- **User Authentication and Authorization** (implemented using ASP.NET Core Identity and secured with JWT)
- **User Management**
- **OpenAPI (Swagger) Documentation**
- **Centralized Exception Handling (per request)**
- **System Logging**
- **In-Memory Caching**
- **Unit Testing**

## Architectural Decision

Although the original project proposal suggested a simple monolithic structure, I chose to implement a **Clean Architecture** pattern instead. This decision was made to ensure better separation of concerns, enhanced maintainability, and a more scalable structure. The layered architecture promotes modularity and testability by isolating business logic from infrastructure concerns.

This project not only fulfills the certification requirements but also demonstrates how a real-world logistics API can be thoughtfully designed and implemented using modern back-end development practices.
