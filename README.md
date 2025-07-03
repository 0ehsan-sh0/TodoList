# 📝 TodoList

A simple full-stack Todo List application with authentication and category management — built using **Angular** and **ASP.NET Core Web API**.

> ⚠️ **Note:** This project is under development and not yet feature-complete.

---

## ⚙️ Technologies Used

### Frontend:
- [Angular 17+](https://angular.io/)
- [TailwindCSS](https://tailwindcss.com/)
- [RxJS](https://rxjs.dev/)
- [NgModel Forms](https://angular.io/guide/forms-overview)

### Backend:
- [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/)
- [Dapper](https://dappertutorial.net/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- SQL Server
- JWT (JSON Web Tokens) for authentication

---

## 🚀 Getting Started

### 🔙 Backend Setup (ASP.NET Core)

1. Navigate to the backend project directory:

   ```bash
   cd TodoListApi
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Run database:

   You have to restore backup (TodoList.bak) on the SQL Server or you have to implement Store Procedures by your own.

4. Start the API:

   ```bash
   dotnet run
   ```

> API will be available at: `https://localhost:7157`

---

### 💻 Frontend Setup (Angular)

1. Navigate to the frontend project directory:

   ```bash
   cd todo-list-front
   ```

2. Install Node.js dependencies:

   ```bash
   npm install
   ```

3. Run the Angular development server:

   ```bash
   ng serve
   ```

> App will be served at: `http://localhost:4200`

---

## ✨ Features

### ✅ Current Features

- ✅ User Registration & Login
- ✅ JWT-based Authentication
- ✅ Secure category fetching
- ✅ Create / Edit / Delete categories
- ✅ TailwindCSS responsive UI
- ✅ Reusable Modal component
- ✅ Alert component with smooth animations

### 🔧 In Progress

- ⏳ Task (Todo) management
- ⏳ Pagination & Search
- ⏳ Admin features

---

## 📂 Project Structure

```
TodoList/
├── todo-list-front/           # Angular frontend
│   ├── app/
│   │   ├── components/
│   │   ├── services/
│   │   ├── models/
│   │   └── ...
│   └── ...
├── TodoListApi/               # ASP.NET Core backend
│   ├── Controllers/
│   ├── Models/
│   ├── Data/
│   └── ...
```

---

## 🛠 Notes

- CORS is enabled on the backend to allow frontend requests during development.
- JWT tokens are stored in `localStorage`.
- Angular standalone components are used for modular UI.
- The API returns structured validation errors using `ProblemDetails`.

---

## 🤝 Contribution

Contributions are welcome!  
Feel free to fork this repo, submit pull requests, or open issues for bugs or ideas.

---

## 📃 License

This project is licensed under the **MIT License**.

---

## 🙋‍♂️ Author

Created by [@0ehsan-sh0](https://github.com/0ehsan-sh0)