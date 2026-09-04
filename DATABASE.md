# 🗄️ Database Documentation

**District Police Management System** | Database Schema

<div align="center">

![Database](https://img.shields.io/badge/Database-MySQL%205.7%2B-blue?style=for-the-badge)
![Schema](https://img.shields.io/badge/Tables-8%20Core-gold?style=for-the-badge)
![Engine](https://img.shields.io/badge/Engine-InnoDB-0047AB?style=for-the-badge)

[📖 Back to README](./README.md) • [🏗️ Architecture](./ARCHITECTURE.md)

</div>

---

## TABLES OVERVIEW

The database contains 8 core tables:

1. **zones** - Geographic operational areas
2. **crime_categories** - Crime classification types
3. **accounts** - User accounts (Desks/Groups/Teams)
4. **cases** - Incident cases
5. **case_assignments** - Case allocation to groups
6. **investigation_updates** - Case progress updates
7. **evidence_records** - Evidence logging
8. **audit_logs** - Complete action audit trail

---

## TABLE DETAILS

### 1. ZONES

Stores geographic operational areas/districts.

```sql
CREATE TABLE zones (
    zone_id INT AUTO_INCREMENT PRIMARY KEY,
    zone_name VARCHAR(100) NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;
```

**Purpose**: Organize cases and personnel by geographic zones  
**Typical Records**: 5-20 zones per district  
**Example**: Downtown Central, East District, North Precinct

---

### 2. CRIME_CATEGORIES

Stores crime types for classification.

```sql
CREATE TABLE crime_categories (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;
```

**Purpose**: Classify cases by crime type  
**Typical Records**: 30-50 categories  
**Example**: Theft, Assault, Robbery, Burglary, Drug Possession

---

### 3. ACCOUNTS

User accounts for system access (organizational units, not individuals).

```sql
CREATE TABLE accounts (
    account_id INT AUTO_INCREMENT PRIMARY KEY,
    account_name VARCHAR(150) NOT NULL, 
    username VARCHAR(50) NOT NULL UNIQUE, 
    password_hash VARCHAR(255) NOT NULL,
    phone_number VARCHAR(20) NULL,
    role ENUM('ADMIN', 'INSPECTOR', 'OFFICER_GROUP') NOT NULL,
    zone_id INT NULL, 
    account_status ENUM('ACTIVE', 'INACTIVE') NOT NULL DEFAULT 'ACTIVE',
    
    CONSTRAINT fk_accounts_zone 
        FOREIGN KEY (zone_id) REFERENCES zones(zone_id) 
        ON UPDATE CASCADE ON DELETE SET NULL
) ENGINE=InnoDB;
```

**Purpose**: User authentication and role-based access  
**Roles**:
- ADMIN: Full system access
- INSPECTOR: Create and assign cases
- OFFICER_GROUP: Receive assignments, update progress

**Note**: Accounts represent teams/desks/groups, NOT individual officers

---

### 4. CASES

Core case/incident records.

```sql
CREATE TABLE cases (
    case_id INT AUTO_INCREMENT PRIMARY KEY,
    case_number VARCHAR(30) NOT NULL UNIQUE,
    case_title VARCHAR(150) NOT NULL,
    category_id INT NOT NULL,
    zone_id INT NOT NULL,
    incident_description TEXT NOT NULL,
    incident_date DATE NOT NULL,
    victim_name VARCHAR(150) NOT NULL,
    suspect_name VARCHAR(150),
    status ENUM('NEW', 'ASSIGNED', 'IN_PROGRESS', 'COMPLETED', 'CANCELLED') NOT NULL DEFAULT 'NEW',
    reported_by_account INT NOT NULL, 
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    CONSTRAINT fk_cases_category 
        FOREIGN KEY (category_id) REFERENCES crime_categories(category_id) 
        ON UPDATE CASCADE ON DELETE RESTRICT,

    CONSTRAINT fk_cases_zone 
        FOREIGN KEY (zone_id) REFERENCES zones(zone_id) 
        ON UPDATE CASCADE ON DELETE RESTRICT,
        
    CONSTRAINT fk_cases_inspector 
        FOREIGN KEY (reported_by_account) REFERENCES accounts(account_id) 
        ON UPDATE CASCADE
) ENGINE=InnoDB;
```

**Purpose**: Store all incident cases  
**Status Workflow**:
- NEW: Case created
- ASSIGNED: Case assigned to officer group
- IN_PROGRESS: Investigation active
- COMPLETED: Case closed
- CANCELLED: Case cancelled

**Typical Records**: 5,000-10,000 annually

---

### 5. CASE_ASSIGNMENTS

Tracks case assignments to officer groups.

```sql
CREATE TABLE case_assignments (
    assignment_id INT AUTO_INCREMENT PRIMARY KEY,
    case_id INT NOT NULL,
    assigned_to_group INT NOT NULL, 
    assigned_by_account INT NOT NULL, 
    assigned_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status ENUM('ACTIVE', 'REASSIGNED', 'COMPLETED') NOT NULL DEFAULT 'ACTIVE',
    
    CONSTRAINT fk_assign_case 
        FOREIGN KEY (case_id) REFERENCES cases(case_id) 
        ON UPDATE CASCADE ON DELETE RESTRICT,
        
    CONSTRAINT fk_assign_group 
        FOREIGN KEY (assigned_to_group) REFERENCES accounts(account_id) 
        ON UPDATE CASCADE,
        
    CONSTRAINT fk_assign_admin 
        FOREIGN KEY (assigned_by_account) REFERENCES accounts(account_id) 
        ON UPDATE CASCADE
) ENGINE=InnoDB;
```

**Purpose**: Record which group is assigned to which case  
**Status**:
- ACTIVE: Currently assigned
- REASSIGNED: Previously assigned (historical)
- COMPLETED: Assignment finished

**Notes**: 
- One active assignment per case
- Reassignment history preserved via status field
- Shows who assigned the case and when

---

### 6. INVESTIGATION_UPDATES

Progress updates for case investigations.

```sql
CREATE TABLE investigation_updates (
    update_id INT AUTO_INCREMENT PRIMARY KEY,
    case_id INT NOT NULL,
    updated_by_account INT NOT NULL, 
    description TEXT NOT NULL,
    update_date DATE NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    CONSTRAINT fk_investigation_case 
        FOREIGN KEY (case_id) REFERENCES cases(case_id) 
        ON UPDATE CASCADE,
        
    CONSTRAINT fk_investigation_account 
        FOREIGN KEY (updated_by_account) REFERENCES accounts(account_id) 
        ON UPDATE CASCADE
) ENGINE=InnoDB;
```

**Purpose**: Create investigation timeline for each case  
**Typical Records**: 3-10 updates per case  
**Example**: "Interview conducted", "Evidence collected", "Suspect identified"

---

### 7. EVIDENCE_RECORDS

Chain of custody documentation for evidence.

```sql
CREATE TABLE evidence_records (
    evidence_id INT AUTO_INCREMENT PRIMARY KEY,
    case_id INT NOT NULL,
    uploaded_by_account INT NOT NULL, 
    evidence_description TEXT NOT NULL,
    file_path VARCHAR(255) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    CONSTRAINT fk_evidence_case 
        FOREIGN KEY (case_id) REFERENCES cases(case_id) 
        ON UPDATE CASCADE ON DELETE RESTRICT,
        
    CONSTRAINT fk_evidence_account 
        FOREIGN KEY (uploaded_by_account) REFERENCES accounts(account_id) 
        ON UPDATE CASCADE
) ENGINE=InnoDB;
```

**Purpose**: Log and track all evidence  
**Typical Records**: 2-20 items per case  
**Tracks**: Who uploaded, when uploaded, file location

---

### 8. AUDIT_LOGS

Complete audit trail of all system changes (permanent record).

```sql
CREATE TABLE audit_logs (
    log_id INT AUTO_INCREMENT PRIMARY KEY,
    table_name VARCHAR(50) NOT NULL,
    record_id INT NOT NULL,
    action_type ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    old_values JSON NULL,
    new_values JSON NULL,
    changed_by_account INT NOT NULL,
    changed_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    CONSTRAINT fk_audit_account 
        FOREIGN KEY (changed_by_account) REFERENCES accounts(account_id) 
        ON UPDATE CASCADE
) ENGINE=InnoDB;
```

**Purpose**: Non-repudiation and compliance  
**Records**: Every INSERT, UPDATE, DELETE operation  
**Stores**:
- What table was changed
- What record was affected
- Before and after values (JSON)
- Who made the change
- When the change happened

**Note**: Audit logs are NEVER deleted (permanent record)

---

## RELATIONSHIPS

### Main Data Flow

```
Zone ←→ Crime_Category ←→ Accounts
  ↓           ↓              ↓
  └───→ Cases ←──────────────┘
        ↓
        ├─→ Case_Assignments → Accounts
        ├─→ Investigation_Updates → Accounts
        └─→ Evidence_Records → Accounts
                    
ALL CHANGES → Audit_Logs → Accounts
```

### Key Constraints

| Constraint | Tables | Type |
|-----------|--------|------|
| Zone → Accounts | zones.zone_id → accounts | SET NULL |
| Category → Cases | crime_categories → cases | RESTRICT |
| Zone → Cases | zones.zone_id → cases | RESTRICT |
| Case → Assignments | cases → case_assignments | RESTRICT |
| Accounts → Assignments | accounts → case_assignments | CASCADE |
| Case → Updates | cases → investigation_updates | CASCADE |
| Case → Evidence | cases → evidence_records | RESTRICT |
| Account → Changes | accounts → audit_logs | CASCADE |

---

<div align="center">

**Database Documentation v1.0** | District Police Management System

*Last Updated: January 2024*

</div>
