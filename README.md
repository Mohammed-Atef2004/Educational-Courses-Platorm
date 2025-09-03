# Educational Courses Platform - ASP.NET Core Web API

## 🎓 Overview

A comprehensive Educational Courses Platform built with ASP.NET Core Web API that provides a robust backend system for managing online courses, student enrollments, episodes, and user authentication. This platform supports course creators, students, and administrators with a full-featured learning management system.

## 🏗️ Architecture

The project follows a clean architecture pattern with clear separation of concerns:

- **Web API Layer**: RESTful API endpoints and controllers
- **Services Layer**: Business logic and service implementations  
- **Data Access Layer**: Entity Framework Core with Repository pattern
- **Entities Layer**: Domain models and DTOs
- **Database**: SQL Server with Entity Framework migrations

##  Database Schema
<img width="642" height="284" alt="image" src="https://github.com/user-attachments/assets/9d01e11d-19ca-4c0d-9057-07a94fd1a8b0" />

## 🚀 Features

### 👨‍🎓 User Management
- **User Registration & Authentication**: Secure user registration with JWT token-based authentication
- **Role-Based Authorization**: Support for different user roles (Admin, Instructor, Student)
- **Profile Management**: Complete user profile management with avatar support
- **Password Management**: Secure password reset functionality with email confirmation
- **User Login**: Secure login system with token generation

### 📚 Course Management
- **Course Creation**: Instructors can create comprehensive courses with detailed descriptions
- **Course Catalog**: Browse and search through available courses
- **Course Categories**: Organize courses by categories and topics
- **Course Publishing**: Draft and publish workflow for course content
- **Course Reviews**: Rating and review system for courses
- **Course Prerequisites**: Set up course prerequisites and learning paths

### 🎬 Episode Management  
- **Video Episodes**: Upload and manage video content for courses
- **Episode Sequencing**: Organize episodes in logical learning sequences
- **Progress Tracking**: Track student progress through episodes
- **Episode Resources**: Attach additional resources and materials
- **Episode Completion**: Mark episodes as completed and track learning milestones

### 📝 Enrollment System
- **Course Enrollment**: Students can enroll in courses
- **Enrollment Requests**: Handle enrollment requests and approvals
- **Enrollment History**: Track student enrollment history
- **Bulk Enrollment**: Administrative bulk enrollment capabilities
- **Enrollment Analytics**: Track enrollment statistics and trends

### 🛠️ Administrative Features
- **User Management**: Admin panel for managing all users
- **Course Moderation**: Review and approve course content
- **Analytics Dashboard**: Comprehensive analytics and reporting
- **System Configuration**: Platform settings and configuration management
- **Content Management**: Manage platform content and resources

### 📧 Communication Features
- **Email Notifications**: Automated email notifications for various events
- **Course Updates**: Notify students about course updates and new content
- **Enrollment Confirmations**: Email confirmations for enrollments
- **Password Reset Emails**: Secure password reset email system
- **Welcome Emails**: Onboarding emails for new users

### 🔒 Security Features
- **JWT Authentication**: Secure token-based authentication
- **Role-Based Access Control**: Granular permission system
- **Data Encryption**: Secure data storage and transmission
- **Input Validation**: Comprehensive input validation and sanitization
- **CORS Configuration**: Cross-origin resource sharing configuration

## 📸 Screenshots

### Account 
<img width="1342" height="294" alt="image" src="https://github.com/user-attachments/assets/efd09617-ec48-40d6-bed6-3a86876c899c" />

### Course  
<img width="1324" height="549" alt="image" src="https://github.com/user-attachments/assets/e75838fc-c81b-45ca-afb3-3c2b44c5b699" />

### Enrollment 
<img width="1338" height="281" alt="image" src="https://github.com/user-attachments/assets/491883aa-ce16-46ef-a6d9-1093f8542280" />

### Episode 
<img width="1332" height="230" alt="image" src="https://github.com/user-attachments/assets/0d2ea215-5a5c-4b03-9aa7-bb4aa34587a9" />

### RoleTest 
<img width="1326" height="63" alt="image" src="https://github.com/user-attachments/assets/ccfcbc03-7f3e-4427-acf8-dadb2ff3767e" />

## Code Snapshots
### Requesting Services
 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IEpisodeService, EpisodeService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IEnrollmentsRequestsService, EnrollmentsRequestsService>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();


## 🛠️ Technology Stack

### Backend Framework
- **ASP.NET Core 8.0**: Modern, cross-platform framework
- **Entity Framework Core**: Object-relational mapping
- **SQL Server**: Robust database management system

### Authentication & Security
- **JWT (JSON Web Tokens)**: Stateless authentication
- **ASP.NET Core Identity**: User management framework
- **Data Protection**: Built-in data protection APIs

### Development Tools
- **Visual Studio 2022**: Primary IDE
- **Swagger/OpenAPI**: API documentation
- 
## 📁 Project Structure

```
Educational-Courses-Platform/
├── 📁 Educational-Courses-Platform.DataAccess/
│   ├── 📁 Data/
│   │   └── 📄 ApplicationDbContext.cs
│   ├── 📁 Implementation/
│   │   ├── 📄 AdminRepository.cs
│   │   ├── 📄 CourseRepository.cs
│   │   ├── 📄 EnrollmentRequestsRepository.cs
│   │   ├── 📄 EpisodeRepository.cs
│   │   ├── 📄 RoleRepository.cs
│   │   └── 📄 UnitOfWork.cs
│   ├── 📁 Migrations/
│   └── 📁 Dependencies
│
├── 📁 Educational-Courses-Platform.Entities/
│   ├── 📁 Dto/
│   │   ├── 📄 CourseDto.cs
│   │   ├── 📄 CourseWithEpisodeDto.cs
│   │   ├── 📄 CourseWithIdDto.cs
│   │   ├── 📄 EpisodeCourseIdDto.cs
│   │   ├── 📄 EpisodeDto.cs
│   │   ├── 📄 ForgotPasswordDto.cs
│   │   ├── 📄 LoginUserDto.cs
│   │   ├── 📄 RegisterUserDto.cs
│   │   └── 📄 ResponseDto.cs
│   ├── 📁 Models/
│   │   ├── 📄 ApplicationUser.cs
│   │   ├── 📄 Course.cs
│   │   ├── 📄 EnrollmentsRequests.cs
│   │   └── 📄 Episode.cs
│   └── 📁 Repositories
│
├── 📁 Educational-Courses-Platform.Services/
│   ├── 📁 Implementation/
│   │   ├── 📄 CourseService.cs
│   │   ├── 📄 EmailSender.cs
│   │   ├── 📄 EnrollmentRequestsService.cs
│   │   ├── 📄 EpisodeService.cs
│   │   └── 📄 RoleService.cs
│   ├── 📁 Interfaces/
│   │   ├── 📄 ICourseService.cs
│   │   ├── 📄 IEmailSender.cs
│   │   ├── 📄 IEnrollmentRequestsService.cs
│   │   ├── 📄 IEpisodeService.cs
│   │   └── 📄 IRoleService.cs
│   └── 📄 Class1.cs
│
└── 📁 Educational-Courses-Platform.Web/
    ├── 📁 Controllers/
    │   ├── 📄 AccountController.cs
    │   ├── 📄 CourseController.cs
    │   ├── 📄 EnrollmentController.cs
    │   ├── 📄 EpisodeController.cs
    │   └── 📄 RolesTestController.cs
    ├── 📁 Templates/
    │   ├── 📄 ConfirmEmail.cshtml
    │   ├── 📄 ConfirmEmailSuccess.cshtml
    │   └── 📄 ForgotPasswordEmail.html
    ├── 📄 Program.cs
    └── 📁 wwwroot/
```

## 📋 API Endpoints

### 🔐 Authentication Endpoints
```
POST   /api/account/register           # User registration
POST   /api/account/login              # User login
POST   /api/account/forgot-password    # Password reset request
POST   /api/account/reset-password     # Password reset confirmation
GET    /api/account/confirm-email      # Email confirmation
```

### 📚 Course Management Endpoints
```
GET    /api/courses                    # Get all courses
GET    /api/courses/{id}              # Get specific course
POST   /api/courses                   # Create new course
PUT    /api/courses/{id}              # Update course
DELETE /api/courses/{id}              # Delete course
GET    /api/courses/{id}/episodes     # Get course episodes
```

### 🎬 Episode Management Endpoints
```
GET    /api/episodes                  # Get all episodes
GET    /api/episodes/{id}             # Get specific episode
POST   /api/episodes                  # Create new episode
PUT    /api/episodes/{id}             # Update episode
DELETE /api/episodes/{id}             # Delete episode
```

### 📝 Enrollment Endpoints
```
GET    /api/enrollment/requests       # Get enrollment requests
POST   /api/enrollment/request        # Submit enrollment request
PUT    /api/enrollment/approve/{id}   # Approve enrollment
PUT    /api/enrollment/reject/{id}    # Reject enrollment
GET    /api/enrollment/user-courses   # Get user's enrolled courses
```

### 👥 Role Management Endpoints
```
GET    /api/roles                     # Get all roles
POST   /api/roles                     # Create new role
PUT    /api/roles/{id}                # Update role
DELETE /api/roles/{id}                # Delete role
POST   /api/roles/assign              # Assign role to user
```

## ⚙️ Installation & Setup

### Prerequisites
- **.NET 8.0 SDK** or later
- **SQL Server** (LocalDB, Express, or Full version)
- **Visual Studio 2022** or **VS Code**
- **Git** for version control

### Step-by-Step Setup

1. **Clone the Repository**
   ```bash
   git clone [repository-url]
   cd Educational-Courses-Platform
   ```

2. **Database Configuration**
   - Update connection string in `appsettings.json`
   - Run Entity Framework migrations:
   ```bash
   dotnet ef database update
   ```

3. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Configure Email Settings**
   - Update SMTP settings in `appsettings.json`
   - Configure email templates in Templates folder

5. **Run the Application**
   ```bash
   dotnet run --project Educational-Courses-Platform.Web
   ```

6. **Access the API**
   - API Base URL: `https://localhost:7001`
   - Swagger Documentation: `https://localhost:7001/swagger`

## 📧 Email Configuration

Configure SMTP settings in `appsettings.json`:

```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password",
    "SenderName": "Educational Platform"
  }
}
```

## 🗄️ Database Schema

### Main Entities

#### Users (ApplicationUser)
- User authentication and profile information
- Role-based permissions
- Contact details and preferences

#### Courses
- Course metadata and descriptions
- Instructor information
- Publishing status and categories

#### Episodes
- Video content and resources
- Course association and sequencing
- Duration and completion tracking

#### Enrollment Requests
- Student enrollment applications
- Approval workflow
- Request status and timestamps

## 🧪 Testing

The project includes comprehensive testing capabilities:

- **Unit Tests**: Test individual components and services
- **Integration Tests**: Test API endpoints and database operations
- **Authentication Tests**: Verify security and authorization

Run tests using:
```bash
dotnet test
```

## 📊 Performance Features

- **Caching**: Redis caching for frequently accessed data
- **Pagination**: Efficient data pagination for large datasets
- **Lazy Loading**: Optimized database queries
- **Compression**: Response compression for better performance
- **Rate Limiting**: API rate limiting to prevent abuse

## 🔍 Monitoring & Logging

- **Structured Logging**: Comprehensive logging with Serilog
- **Health Checks**: API health monitoring endpoints
- **Error Tracking**: Centralized error handling and reporting
- **Performance Metrics**: Application performance monitoring

## 🚀 Deployment

### Production Deployment Checklist
- [ ] Update connection strings for production database
- [ ] Configure production email settings
- [ ] Set up SSL certificates
- [ ] Configure logging for production
- [ ] Set up monitoring and health checks
- [ ] Configure backup strategies


## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support and questions:
- Create an issue in the repository
- Check the documentation
- Review the API documentation at `/swagger`

## 🎯 Future Enhancements

- **Mobile App Integration**: REST API ready for mobile apps
- **Video Streaming**: Enhanced video streaming capabilities  
- **Discussion Forums**: Course-specific discussion boards
- **Certificates**: Automated certificate generation
- **Payment Integration**: Course payment processing
- **Advanced Analytics**: Detailed learning analytics
- **Mobile Push Notifications**: Real-time mobile notifications
- **Offline Content**: Downloadable course content for offline viewing

---

**Built with ❤️ using ASP.NET Core**
