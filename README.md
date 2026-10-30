# 🎯 TrustPath – Lead Management Web Console

**TrustPath** is a web-based Lead Management System designed to simplify the process of capturing, organizing, tracking, and managing business leads.

It provides a centralized platform for managing lead information, assigning leads to team members, tracking follow-ups, monitoring lead status, and improving the overall sales management process.

## 🚀 Key Features

- **Dashboard:** Get an overview of leads, activities, and business performance.
- **Lead Management:** Create, update, view, and manage leads.
- **Lead Tracking:** Track lead status throughout the sales process.
- **Follow-Up Management:** Schedule and manage lead follow-up activities.
- **Customer Management:** Maintain customer and prospect information.
- **Lead Assignment:** Assign leads to relevant team members.
- **Sales Pipeline:** Monitor leads through different sales stages.
- **Task Management:** Organize lead-related activities and tasks.
- **Communication History:** Maintain lead interaction records.
- **Reports & Analytics:** Analyze lead performance and conversion statistics.
- **Search & Filters:** Quickly find leads using search and filtering options.
- **User Management:** Manage application users and access permissions.
- **Role-Based Access:** Control access based on user roles and permissions.
- **Responsive Interface:** Access the application across different screen sizes.

## 🛠️ Technology Stack

- ASP.NET / .NET
- C#
- SQL Server (if configured)
- Entity Framework (as configured)
- HTML5
- CSS3
- JavaScript
- jQuery
- Bootstrap
- RESTful API Integration (if configured)

## 🏗️ Project Architecture

The application follows a layered architecture to separate application responsibilities and improve maintainability.

```text
TrustPath-Web-Console/
│
├── TrustPathWebConsole.Domain/
│
├── TrustPathWebConsole.Infrastructure/
│
├── TrustPathWebConsole.Service/
│
├── TrustPathWebConsole/
│
└── TrustPathWebConsole.sln
```

### Layer Responsibilities

| Layer          | Responsibility                                                |
| -------------- | ------------------------------------------------------------- |
| Domain         | Contains application models and domain entities.              |
| Infrastructure | Handles infrastructure-related functionality.                 |
| Service        | Contains business logic and service operations.               |
| WebConsole     | Handles controllers, views, and user interface functionality. |

## ⚙️ Getting Started

### Prerequisites

- Visual Studio
- Compatible .NET SDK / Targeting Pack
- SQL Server (if required)
- NuGet Package Manager
- Required API services (if applicable)

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/drashti-shastra-soft/TrustPath-Web-Console.git
```

**2. Navigate to the project directory**

```bash
cd TrustPath-Web-Console
```

**3. Open the solution**

Open the solution file in Visual Studio:

```text
TrustPathWebConsole.sln
```

**4. Restore NuGet Packages**

Restore all required NuGet packages.

**5. Configure Application Settings**

Configure the required database connection string, API URLs, and other application settings.

**6. Build and Run**

Build the solution and run the Web Console using Visual Studio.

## 🔐 Security

- Secure user authentication.
- Role-based access control (if configured).
- Permission-based feature access.
- Protected application functionality.
- Secure configuration management.

## 🎯 Project Objective

The main objective of TrustPath is to provide businesses with an organized and efficient lead management platform.

It helps teams manage their leads, maintain follow-up activities, track sales progress, improve customer relationships, and streamline the overall lead management workflow.
