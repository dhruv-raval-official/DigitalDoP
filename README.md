# Digital DoP

## Overview
**Digital DoP** is a web-based platform designed to enhance service transparency, accountability, and efficiency in the **Department of Posts (DoP)**. It allows admins and staff to manage postal services while enabling citizens to track services and provide feedback without requiring an account.

## Features
### **Admin**
- View **dashboard** with key performance indicators (KPIs) and graphs.
- Manage **staff** (Add, View, Edit with filters).
- Manage **services** (Add, View, Update with tracking and status change).
- View and filter **feedback** (Search by user, filter by office/rating).

### **Staff**
- Add and update **services** (Auto-detect office, update status).
- View and filter **feedback**.

### **Citizen (Public User - No Login Required)**
- **Track orders** using Tracking ID.
- **Submit feedback** (Office selection, rating, comments).
- Receive **notifications** on service status updates.

## Tech Stack
- **Frontend**: React.js, ShadCN, Tailwind CSS
- **Backend**: ASP.NET Core, C#
- **Database**: MS SQL
- **Authentication**: JWT (JSON Web Token)
- **Tools**: Visual Studio Code, Microsoft SQL Server Management Studio (SSMS), Edge, Generative AI

## Installation & Setup
### **Prerequisites**
- Node.js (For frontend development)
- .NET SDK (For backend development)
- MS SQL Server

### **Backend Setup**
```sh
# Clone the repository
git clone https://github.com/your-repo/digital-dop.git
cd digital-dop/backend

# Install dependencies
dotnet restore

# Run the application
dotnet run
```

### **Frontend Setup**
```sh
cd ../frontend

# Install dependencies
npm install

# Run the React app
npm start
```

## Database Configuration
- Update `appsettings.json` in the backend project with your **MS SQL Server** connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=DigitalDoP;User Id=your_user;Password=your_password;"
}
```

## API Endpoints (Sample)
- `POST /api/auth/login` → User Authentication
- `GET /api/services` → Get all services
- `POST /api/feedback` → Submit feedback
- `GET /api/tracking/{trackingId}` → Track service by ID

## License
This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

## Contributors
- **Dhruv Janardanbhai Raval** – Team Lead

## Contact
For any queries or contributions, please contact **dhruv.raval.official@gmail.com** or visit the **[GitHub Repository](https://github.com/dhruv-raval-official/DigitalDoP)**.

---
**Digital DoP** - A step towards transparent and efficient postal services!
