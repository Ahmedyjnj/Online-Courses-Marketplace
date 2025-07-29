# Online Courses Marketplace

This project is a complete **ASP.NET Core MVC** application that simulates an online course marketplace where students can browse, purchase, and access courses, while instructors can manage their content and earnings.

---

# 🎓 Online Courses Marketplace

A full-featured online course platform built using **ASP.NET Core MVC**, offering students and instructors a seamless experience for managing, purchasing, and viewing educational video content.

---

## 📌 Features

* 🔐 Secure user authentication & authorization (Student / Instructor roles)
* 📈 Course listing with filtering, search, and categories
* 💳 Online payment system integrated with **Paymob** API
* 🎥 Protected video streaming using **HLS** and access control
* 📅 Instructor and student dashboards with profile editing
* 🔍 Searchable course catalog and detail pages
* 💬 Real-time support chat via **SignalR**
* ☁️ Cloud uploads for videos and images (Cloudinary or custom uploader)
* 🛒 Basket system with Redis-based caching

---

## 📊 Technologies Used

* **ASP.NET Core 8 MVC**
* **Entity Framework Core** + PostgreSQL
* **SignalR** for real-time communication
* **Paymob API** for secure payments
* **Redis** for basket/session caching
* **Cloudinary** or local FFmpeg-based HLS streaming
* **Bootstrap 5**, **Animate.css** for modern UI


### 🎓 Students

* Register & log in
* View course listings and search
* Pay for a course via **Paymob** integration
* Access course videos after payment
* View/edit their profile

### 👨‍🏫 Instructors

* Register & log in
* Create and manage courses
* Upload images and videos
* View their earnings

### 💳 Payments

* Paymob integration using iframe URL redirect
* Payment confirmation handling
* Student/instructor payment tracking

### ✨ UI

* Responsive Bootstrap 5 layout
* Role-based views
* Reusable layout with navbar and footer
* SignalR-based live support chat (optional)

---

## 🏃 Project Structure

### Core

* **Domain.Models**: Contains all domain entities

  * `Course`, `CourseImage`, `CourseVideo`
  * `Instructor`, `Student`, `ApplicationUser`
  * `Payment`, `InstructorPayment`, `StudentPayment`
* **Abstraction**: Interfaces for services

### Services

* Contains business logic for each module (e.g., CourseService, PaymentService)

### Infrastructure

* Data access layer with **EF Core** and PostgreSQL
* Configurations & context setup

### Web (Presentation)

* MVC controllers:

  * `HomeController`, `CourseController`, `PaymentController`, etc.
* Views for student, instructor, and admin roles

---

## 💵 Payment Flow

1. Student clicks **Pay Now** button
2. `PaymentController.StartPayment` (GET) fills in view model with course & user data
3. Submits to `StartPayment` (POST) which calls `PaymentService.StartPayment`
4. Redirects to Paymob iframe URL
5. Upon success, webhook or return confirms payment and logs `StudentPayment`

---

## 🚧 Security

* JWT or Identity authentication (uses ASP.NET Identity)
* Role-based authorization
* Secret scanning protected

---

## 🔧 How to Run

1. Clone the repo

```bash
git clone https://github.com/yourusername/Online-Courses-Marketplace.git
```

2. Configure database in `appsettings.json`

3. Apply migrations

```bash
dotnet ef database update
```

4. Run the app

```bash
dotnet run
```

---

## 📚 Learning Outcome

This project demonstrates:

* Building scalable layered architecture
* Clean Domain-Driven Design with interfaces and services
* Payment system integration
* Entity Framework and data modeling
* ASP.NET Identity customization

---

## ✨ Contribution

If you're interested in contributing, open an issue or pull request. All contributions are welcome.

---

## 🎉 Author

Ahmed mahmoud – [GitHub](https://github.com/Ahmedyjnj)

---

## 📍 License

[MIT License](LICENSE)
