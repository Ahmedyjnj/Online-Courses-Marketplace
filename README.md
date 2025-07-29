# Online Courses Marketplace

This project is a complete **ASP.NET Core MVC** application that simulates an online course marketplace where students can browse, purchase, and access courses, while instructors can manage their content and earnings.

---

## ✨ Features

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

## 🚀 Technologies Used

* **ASP.NET Core MVC 8**
* **Entity Framework Core 8**
* **PostgreSQL**
* **Redis** (optional caching)
* **SignalR** (live support)
* **Paymob API**
* **Bootstrap 5**

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
