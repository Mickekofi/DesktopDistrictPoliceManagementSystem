# 🏗️ System Architecture

**District Police Management System** | Feature-Based Architecture Design

<div align="center">

![Architecture](https://img.shields.io/badge/Pattern-Feature%20Based-0047AB?style=for-the-badge)
![Design](https://img.shields.io/badge/Framework-VB.NET%20WinForms-blue?style=for-the-badge)

[📖 Back to README](./README.md) • [🗄️ Database](./DATABASE.md)

</div>

---

## ARCHITECTURE PATTERN

### Feature-Based Architecture

The system is organized around **business features** rather than technical layers.

Each feature is self-contained with its own screens and logic:

```
Features/
│
├─ Authentication/     → User login & permissions
├─ Admin/             → System administration
├─ Inspector/         → Case management & assignment
└─ Officer/           → Case updates & evidence logging
```

### Why Feature-Based?

```
✓ Related code stays together
✓ Easy to find functionality
✓ Simple to add new features
✓ Clear ownership per feature
✓ Minimal cross-feature dependencies
```

---

## PROJECT STRUCTURE

```
district-police-management/
│
├── src/
│   ├── Core/
│   │   ├── database.txt          # Database queries & schema
│   │   └── doc.txt               # Database notes
│   │
│   ├── Shared/
│   │   ├── Database.vb           # Database connection & config
│   │   ├── DataGridViewHelper.vb # DataGrid UI utilities
│   │   ├── NavButtonStyles.vb    # Navigation button styling
│   │   ├── SessionManager.vb     # User session management
│   │   └── ShapeUtilities.vb     # UI shape drawing helpers
│   │
│   └── Features/
│       ├── Authentication/
│       │   └── Screens/          # Login forms
│       │
│       ├── Admin/
│       │   └── Screens/          # Admin UI forms
│       │
│       ├── Inspector/
│       │   └── Screens/          # Inspector UI forms
│       │
│       └── Officer/
│           └── Screens/          # Officer UI forms
│
├── database/
│   ├── schema.sql               # Database schema
│   └── sample_data.sql          # Sample data
│
├── docs/
│   ├── README.md                # Main documentation
│   ├── DATABASE.md              # Database documentation
│   └── ARCHITECTURE.md          # This file
│
├── App.config                   # Application configuration
├── DistrictPoliceManagement.sln # Solution file
├── LICENSE                      # MIT License
└── README.md                    # Project overview
```

---

## FOLDER ORGANIZATION

### Core/
Contains database setup and documentation.

```
Core/
├── database.txt     # Database queries and schema
└── doc.txt         # Database notes and documentation
```

### Shared/
Utilities used across all features.

```
Shared/
├── Database.vb              # Handles database connections
├── DataGridViewHelper.vb    # DataGrid formatting & utilities
├── NavButtonStyles.vb       # Navigation button styling
├── SessionManager.vb        # User session management
└── ShapeUtilities.vb        # UI shape drawing helpers
```

### Features/
Self-contained feature modules.

```
Features/
│
├── Authentication/
│   └── Screens/
│       └── LoginForm.vb     # User login screen
│
├── Admin/
│   └── Screens/
│       └── [Admin forms]    # Zone management, user management, etc.
│
├── Inspector/
│   └── Screens/
│       └── [Inspector forms] # Case creation, assignment, reporting
│
└── Officer/
    └── Screens/
        └── [Officer forms]  # Case updates, evidence logging
```

---

## FEATURES EXPLAINED

### Authentication Feature

**Purpose**: User login and session management

**Screens**:
- Login form

**Responsibilities**:
- Validate user credentials
- Create user session
- Assign user role

---

### Admin Feature

**Purpose**: System administration

**Screens**:
- Admin dashboard
- Zone management
- Crime category management
- User account management
- System settings

**Responsibilities**:
- Create/edit zones
- Create/edit crime categories
- Create/edit user accounts
- System-wide configuration

---

### Inspector Feature

**Purpose**: Case management and oversight

**Screens**:
- Inspector dashboard
- Case creation
- Case assignment
- Case tracking
- Report generation

**Responsibilities**:
- Create new cases
- Assign cases to officer groups
- Track investigation progress
- Generate reports
- View zone statistics

---

### Officer Feature

**Purpose**: Case investigation execution

**Screens**:
- Officer dashboard
- Assigned cases list
- Case details
- Investigation update form
- Evidence logging form

**Responsibilities**:
- View assigned cases
- Update investigation progress
- Log evidence
- Submit findings

---

## SHARED UTILITIES

### Database.vb
Manages database connections and queries.

### SessionManager.vb
Tracks logged-in user and their role.

### DataGridViewHelper.vb
Provides common DataGrid formatting and display utilities.

### NavButtonStyles.vb
Applies consistent navigation button styling across screens.

### ShapeUtilities.vb
Provides UI shape drawing and styling helpers.

---

## APPLICATION FLOW

```
Start Application
    ↓
Authentication Feature (Login)
    ↓
SessionManager stores: user, role, zone
    ↓
Based on role, show appropriate feature:
├─ ADMIN → Admin Feature
├─ INSPECTOR → Inspector Feature
└─ OFFICER_GROUP → Officer Feature
    ↓
User interacts with screens
    ↓
Screens use Shared utilities
    ↓
Screens interact with database
    ↓
All changes logged to audit_logs
```

---

<div align="center">

**Architecture Documentation v1.0** | District Police Management System

*Last Updated: September 4th*

</div>
