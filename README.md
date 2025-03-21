# TravelAgent 🧳🌍

A full-stack web application for a custom Travel Agency, built as a university project. The platform allows users to browse existing travel offers or request personalized trips tailored to their needs. The project consists of both backend and frontend components, following a clean and modular structure.

## ✨ Features

- User authentication (Registration/Login) with two roles:
  - **Admin**: Can create travel offers and review users' trip requests.
  - **User**: Can browse existing offers, request custom trips, and manage their profile.
- Wishlist functionality: Logged-in users can add offers to their wishlist.
- Make reservations directly from the offer listing.
- Secure payment integration using **Stripe** (including webhook handling for payment confirmations).
- Custom trip request system for tailored offers.

## 🛠️ Tech Stack

### Backend
- **.NET Core** (C#)
- Follows MVC architecture with clear separation of:
  - Controllers
  - Models
  - DTOs
  - Services

### Frontend
- **Angular**
- Organized into:
  - Components
  - Services
  - Helpers
  - Models

### Payments
- **Stripe API** integration for credit card payments.

## 🚀 How to Run Locally

### Prerequisites:
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js + npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- A **Stripe** account and API keys for payment functionality

### Backend:
```bash
cd TravelAgent
dotnet build
dotnet run
```

### Frontend:
```bash
cd TravelAgent-Ng
npm install
ng serve
```
