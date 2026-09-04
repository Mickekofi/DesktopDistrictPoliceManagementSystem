# 🗄️ Database Documentation

**District Police Management System** | Database Architecture & Design

<div align="center">

![Database](https://img.shields.io/badge/Database-MySQL%205.7%2B-blue?style=for-the-badge)
![Schema](https://img.shields.io/badge/Tables-8%20Core%20Tables-gold?style=for-the-badge)
![Engine](https://img.shields.io/badge/Engine-InnoDB-0047AB?style=for-the-badge)

[📖 Back to Main README](./README.md) • [🏗️ Architecture](./ARCHITECTURE.md)

</div>

---

## 📋 TABLE OF CONTENTS

1. [Database Overview](#database-overview)
2. [Schema Architecture](#schema-architecture)
3. [Core Tables](#core-tables)
4. [Entity Relationships](#entity-relationships)
5. [Capabilities](#capabilities)
6. [Limitations](#limitations)
7. [Design Decisions](#design-decisions)
8. [Data Flow](#data-flow)
9. [Optimization Tips](#optimization-tips)

---

## 🎯 DATABASE OVERVIEW

### Purpose

The DPMS database serves as the **single source of truth** for all police management operations, providing:

- **Case Tracking** - Complete case lifecycle from incident to resolution
- **Personnel Management** - Officer groups and administrative accounts
- **Evidence Management** - Chain-of-custody documentation
- **Zone Management** - Geographic operational units
- **Audit Compliance** - Non-repudiation through complete logging

### Key Characteristics

| Characteristic | Value |
|---|---|
| **Database Engine** | InnoDB (ACID compliant) |
| **Character Set** | UTF8MB4 (supports international characters) |
| **Default Storage** | ~500MB (expandable) |
| **Concurrent Connections** | 50+ users simultaneously |
| **Backup Strategy** | Daily automated backups |
| **Recovery Model** | Full + Incremental |

---

## 🏗️ SCHEMA ARCHITECTURE

### Core Architecture Principles

```
┌──────────────────────────────────────────────────────┐
│         MASTER REFERENCE TABLES (Independent)        │
├──────────────────────────────────────────────────────┤
│ • Zones                                              │
│ • Crime Categories                                   │
│                                                      │
├──────────────────────────────────────────────────────┤
│         ORGANIZATIONAL TABLES (Parent)               │
├──────────────────────────────────────────────────────┤
│ • Accounts (Desks/Groups - No Individuals)           │
│                                                      │
├──────────────────────────────────────────────────────┤
│         OPERATIONAL TABLES (Child)                   │
├──────────────────────────────────────────────────────┤
│ • Cases                                              │
│ • Case Assignments                                   │
│ • Investigation Updates                              │
│ • Evidence Records                                   │
│                                                      │
├──────────────────────────────────────────────────────┤
│         COMPLIANCE TABLES (Universal)                │
├──────────────────────────────────────────────────────┤
│ • Audit Logs (Non-Negotiable)                        │
└──────────────────────────────────────────────────────┘
```

---

## 🗂️ CORE TABLES

### 1. ZONES Table

**Purpose**: Geographic organization and operational boundaries

```sql
CREATE TABLE zones (
    zone_id INT AUTO_INCREMENT PRIMARY KEY,
    zone_name VARCHAR(100) NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;
```

**Details**:
- **Type**: Master Reference Table
- **Records Typical**: 5-20 zones per district
- **Growth**: Static or slow growth
- **Relationships**: Used by Accounts, Cases

**Example Data**:
```
┌─────────┬──────────────────┬─────────────────────────┐
│ zone_id │ zone_name        │ created_at              │
├─────────┼──────────────────┼─────────────────────────┤
│ 1       │ Downtown Central  │ 2024-01-01 09:00:00    │
│ 2       │ East District     │ 2024-01-01 09:00:00    │
│ 3       │ North Precinct    │ 2024-01-01 09:00:00    │
│ 4       │ West Division     │ 2024-01-01 09:00:00    │
└─────────┴──────────────────┴─────────────────────────┘
```

**Indexing**:
```sql
CREATE UNIQUE INDEX idx_zone_name ON zones(zone_name);
```

---

### 2. CRIME_CATEGORIES Table

**Purpose**: Classification system for all reported crimes

```sql
CREATE TABLE crime_categories (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;
```

**Details**:
- **Type**: Master Reference Table
- **Records Typical**: 30-50 categories
- **Growth**: Low growth (new categories rare)
- **Relationships**: FK from Cases table

**Example Categories**:
```
Violent Crimes
├─ Homicide
├─ Assault
├─ Sexual Assault
└─ Robbery

Property Crimes
├─ Burglary
├─ Theft
├─ Vehicle Theft
└─ Vandalism

White Collar Crimes
├─ Fraud
├─ Embezzlement
├─ Money Laundering
└─ Forgery

Public Order
├─ Disorderly Conduct
├─ Drug Possession
├─ Drunk & Disorderly
└─ Trespassing
```

**Indexing**:
```sql
CREATE UNIQUE INDEX idx_category_name ON crime_categories(category_name);
```

---

### 3. ACCOUNTS Table

**Purpose**: User accounts for organizational units (Desks/Groups, NO Individuals)

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

**Delete Strategy**: `SET NULL`
- If zone deleted, accounts.zone_id becomes NULL
- Account remains active (orphaned from zone)

**Details**:
- **Type**: Parent Organizational Table
- **Records Typical**: 50-100 accounts
- **Growth**: Moderate (new teams/desks added)
- **Key Design**: **NO individual officers** - only team/desk/group accounts
- **Relationships**: 1-to-Many with Cases, Case_Assignments, Investigation_Updates, Evidence_Records

**Role Hierarchy**:
```
ADMIN (System Administrator)
├─ Account Name: "System Administrator"
├─ Zone: NULL (All zones)
├─ Permissions: Full system access
└─ Responsibilities: Configuration, user management

INSPECTOR (Zone Inspector/Commander)
├─ Account Name: "Downtown Central Inspector"
├─ Zone: Specific zone
├─ Permissions: Create/assign cases in zone
└─ Responsibilities: Case management, supervision

OFFICER_GROUP (Team/Desk/Unit)
├─ Account Name: "Drug Enforcement Unit"
├─ Zone: Specific zone
├─ Permissions: Receive assignments, update progress
└─ Responsibilities: Investigation, evidence logging
```

**Example Data**:
```
┌────────────┬────────────────────────┬──────────┬───────────────┐
│ account_id │ account_name           │ role     │ zone_id       │
├────────────┼────────────────────────┼──────────┼───────────────┤
│ 1          │ System Administrator   │ ADMIN    │ NULL          │
│ 2          │ Downtown Central Insp. │ INSPECTOR│ 1             │
│ 3          │ Downtown Officer Team  │ OFFICER_GROUP │ 1        │
│ 4          │ East District Insp.    │ INSPECTOR│ 2             │
│ 5          │ East Officer Team      │ OFFICER_GROUP │ 2        │
└────────────┴────────────────────────┴──────────┴───────────────┘
```

**Indexing**:
```sql
CREATE UNIQUE INDEX idx_username ON accounts(username);
CREATE INDEX idx_zone_role ON accounts(zone_id, role);
CREATE INDEX idx_account_status ON accounts(account_status);
```

---

### 4. CASES Table

**Purpose**: Core case/incident records

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
    status ENUM('NEW', 'ASSIGNED', 'IN_PROGRESS', 'COMPLETED', 'CANCELLED') 
        NOT NULL DEFAULT 'NEW',
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

**Delete Strategy**:
- `RESTRICT` on category: Cannot delete crime category if cases exist
- `RESTRICT` on zone: Cannot delete zone if cases exist
- `CASCADE UPDATE`: Zone/Category updates cascade to cases

**Details**:
- **Type**: Core Operational Table
- **Records Typical**: 1000-5000+ cases annually
- **Growth**: High growth (continuous)
- **Storage**: Primary data storage (largest table)
- **Relationships**: Parent for Case_Assignments, Investigation_Updates, Evidence_Records

**Case Status Workflow**:
```
    ┌─────────┐
    │   NEW   │  Incident reported, awaiting assignment
    └────┬────┘
         │ [Assigned by Inspector]
    ┌────▼──────────┐
    │   ASSIGNED    │  Case allocated to officer group
    └────┬──────────┘
         │ [Officer begins investigation]
    ┌────▼───────────┐
    │  IN_PROGRESS   │  Active investigation ongoing
    └────┬───────────┘
         │ [Case resolved]
    ┌────▼──────────────────────┐
    │ COMPLETED / CANCELLED     │  Investigation finished
    └───────────────────────────┘
```

**Example Data**:
```
┌─────────┬──────────────┬────────────┬──────────┬────────────────┐
│case_id  │ case_number  │case_title  │category  │ zone_id        │
├─────────┼──────────────┼────────────┼──────────┼────────────────┤
│ 1       │ DPMS-2024-001│ Burglary   │ 7 (Theft)│ 1 (Downtown)   │
│ 2       │ DPMS-2024-002│ Assault    │ 2 (Assault)│ 2 (East)      │
│ 3       │ DPMS-2024-003│ Theft      │ 7 (Theft)│ 1 (Downtown)   │
└─────────┴──────────────┴────────────┴──────────┴────────────────┘
```

**Indexing**:
```sql
CREATE UNIQUE INDEX idx_case_number ON cases(case_number);
CREATE INDEX idx_case_status ON cases(status);
CREATE INDEX idx_case_zone ON cases(zone_id);
CREATE INDEX idx_case_category ON cases(category_id);
CREATE INDEX idx_case_created ON cases(created_at);
```

---

### 5. CASE_ASSIGNMENTS Table

**Purpose**: Single source of truth for case-to-group assignments (prevents duplicate assignments)

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

**Delete Strategy**:
- `RESTRICT` on case: Cannot delete case if assignments exist
- Prevents accidental case deletion with active assignments
- Assignment history preserved via `status` field

**Details**:
- **Type**: Operational Junction Table
- **Purpose**: Tracks case assignments with assignment history
- **Records Typical**: 1 per active case + historical records
- **Key Design**: Allows tracking **reassignments** via status field
- **Relationships**: Links Cases to Accounts (groups)

**Assignment Lifecycle**:
```
New Case Created
    ↓
Inspector Creates Assignment (status = 'ACTIVE')
    ↓
Officer Group Receives Case
    ↓
[OPTION A] Case Completed (status = 'COMPLETED')
[OPTION B] Case Reassigned to Another Group
    └─> Old Assignment (status = 'REASSIGNED')
    └─> New Assignment (status = 'ACTIVE')
```

**Example Data**:
```
┌───────────────┬─────────┬──────────────────┬──────────────┐
│ assignment_id │case_id  │assigned_to_group │ status       │
├───────────────┼─────────┼──────────────────┼──────────────┤
│ 1             │ 1       │ 3 (Officer Team) │ ACTIVE       │
│ 2             │ 1       │ 5 (Officer Team) │ REASSIGNED   │ (History)
│ 3             │ 2       │ 3 (Officer Team) │ ACTIVE       │
└───────────────┴─────────┴──────────────────┴──────────────┘
```

**Indexing**:
```sql
CREATE INDEX idx_assign_case ON case_assignments(case_id);
CREATE INDEX idx_assign_status ON case_assignments(status);
CREATE INDEX idx_assign_group ON case_assignments(assigned_to_group);
CREATE INDEX idx_assign_date ON case_assignments(assigned_date);
```

---

### 6. INVESTIGATION_UPDATES Table

**Purpose**: Progress tracking and investigation timeline

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

**Details**:
- **Type**: Operational Fact Table
- **Purpose**: Creates case investigation timeline
- **Records Typical**: 3-10 updates per case
- **Growth**: Continuous (one entry per investigation action)
- **Relationships**: Child of Cases

**Investigation Timeline Example**:
```
Case #DPMS-2024-001 Timeline:
│
├─ 2024-01-15 09:30 → Case Created (Burglary reported)
│
├─ 2024-01-15 14:00 → "Initial scene investigation completed.
│                      Evidence photographed."
│                      [Updated by: Downtown Central Inspector]
│
├─ 2024-01-16 10:15 → "Interviewed victim. Identified potential suspect.
│                      Security footage retrieved."
│                      [Updated by: Officer Team]
│
├─ 2024-01-17 13:45 → "Suspect located and interviewed. Inconsistencies noted.
│                      Further evidence review required."
│                      [Updated by: Officer Team]
│
└─ 2024-01-18 16:20 → "Case resolved. Suspect arrested. Evidence secured.
│                      Case ready for prosecution."
                       [Updated by: Downtown Central Inspector]
```

**Indexing**:
```sql
CREATE INDEX idx_investigation_case ON investigation_updates(case_id);
CREATE INDEX idx_investigation_date ON investigation_updates(update_date);
```

---

### 7. EVIDENCE_RECORDS Table

**Purpose**: Chain of custody documentation for physical/digital evidence

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

**Delete Strategy**:
- `RESTRICT` on case: Cannot delete case if evidence exists
- Ensures evidence chain of custody is preserved
- Prevents accidental deletion of critical investigative evidence

**Details**:
- **Type**: Operational Fact Table
- **Purpose**: Digital evidence logs for chain of custody
- **Records Typical**: 2-20 evidence items per case
- **Growth**: High (depends on case complexity)
- **Relationships**: Child of Cases

**Chain of Custody Example**:
```
Case #DPMS-2024-001 Evidence Log:
│
├─ Evidence #1
│  ├─ Description: Photograph of burglar tool marks on door
│  ├─ File Path: /evidence/2024/DPMS-2024-001/photo_001.jpg
│  ├─ Uploaded by: Officer Team (Downtown)
│  ├─ Timestamp: 2024-01-15 10:30:15
│  └─ Custody Status: Logged at scene
│
├─ Evidence #2
│  ├─ Description: Security footage - Main Street camera (1 hour)
│  ├─ File Path: /evidence/2024/DPMS-2024-001/footage_001.mp4
│  ├─ Uploaded by: Downtown Central Inspector
│  ├─ Timestamp: 2024-01-15 15:45:00
│  └─ Custody Status: Collected from security company
│
└─ Evidence #3
   ├─ Description: Fingerprint samples from entry point
   ├─ File Path: /evidence/2024/DPMS-2024-001/prints_001.pdf
   ├─ Uploaded by: Officer Team (Downtown)
   ├─ Timestamp: 2024-01-16 09:15:30
   └─ Custody Status: Lab analysis results
```

**Indexing**:
```sql
CREATE INDEX idx_evidence_case ON evidence_records(case_id);
CREATE INDEX idx_evidence_created ON evidence_records(created_at);
```

---

### 8. AUDIT_LOGS Table

**Purpose**: Non-negotiable compliance and audit trail (permanent record)

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

**Details**:
- **Type**: Compliance & Audit Table
- **Purpose**: Complete system audit trail (non-repudiation)
- **Records Typical**: 10-50 entries per case (high volume)
- **Growth**: Very high (every change logged)
- **Retention**: Permanent storage (audit logs NEVER deleted)
- **changed_by_account**: NOT NULL (required - who made the change)

**Key Characteristics**:
- ✅ Every action logged with timestamp
- ✅ User attribution mandatory
- ✅ JSON before/after values for detail
- ✅ Immutable audit trail
- ✅ Compliance-ready (non-repudiation)

**Audit Trail Example**:
```
Case #DPMS-2024-001 Audit Trail:
│
├─ Log #1001
│  ├─ Action: INSERT (Case Created)
│  ├─ Table: cases
│  ├─ Record: case_id = 1
│  ├─ New Values: {case_number: "DPMS-2024-001", status: "NEW", ...}
│  ├─ Old Values: NULL
│  ├─ Changed By: Downtown Central Inspector (account_id: 2)
│  └─ Timestamp: 2024-01-15 09:30:00
│
├─ Log #1002
│  ├─ Action: UPDATE (Status Changed)
│  ├─ Table: cases
│  ├─ Record: case_id = 1
│  ├─ Old Values: {status: "NEW"}
│  ├─ New Values: {status: "ASSIGNED"}
│  ├─ Changed By: Downtown Central Inspector (account_id: 2)
│  └─ Timestamp: 2024-01-15 14:00:00
│
└─ Log #1003
   ├─ Action: INSERT (Assignment Created)
   ├─ Table: case_assignments
   ├─ Record: assignment_id = 1
   ├─ New Values: {case_id: 1, assigned_to_group: 3, status: "ACTIVE"}
   ├─ Old Values: NULL
   ├─ Changed By: Downtown Central Inspector (account_id: 2)
   └─ Timestamp: 2024-01-15 14:00:30
```

**Indexing**:
```sql
CREATE INDEX idx_audit_table_record ON audit_logs(table_name, record_id);
CREATE INDEX idx_audit_timestamp ON audit_logs(changed_at);
CREATE INDEX idx_audit_account ON audit_logs(changed_by_account);
CREATE INDEX idx_audit_action ON audit_logs(action_type);
```

---

## 🔗 ENTITY RELATIONSHIPS

### Complete Entity-Relationship Diagram (Text Format)

```
┌─────────────────────────────────────────────────────────────────┐
│                     DATABASE RELATIONSHIPS                       │
└─────────────────────────────────────────────────────────────────┘

ZONES (Master Reference)
│   ├─ 1 zone_id ──┬─→ N in ACCOUNTS (zone_id)
│   │              └─→ N in CASES (zone_id)
│   └─ Represents: Geographic operational boundaries
└─────────────────────

CRIME_CATEGORIES (Master Reference)
│   └─ 1 category_id ──→ N in CASES (category_id)
└─ Represents: Crime classification system

ACCOUNTS (Parent Organizational)
│   ├─ 1 account_id ──┬─→ N in CASES (reported_by_account)
│   │                 ├─→ N in CASE_ASSIGNMENTS (assigned_to_group)
│   │                 ├─→ N in CASE_ASSIGNMENTS (assigned_by_account)
│   │                 ├─→ N in INVESTIGATION_UPDATES (updated_by_account)
│   │                 ├─→ N in EVIDENCE_RECORDS (uploaded_by_account)
│   │                 └─→ N in AUDIT_LOGS (changed_by_account)
│   │
│   ├─ 0..1 zone_id ──→ 1 in ZONES (zone_id)
│   └─ Represents: System users (Desks/Groups/Teams, NOT individuals)

CASES (Core Operational)
│   ├─ N cases ──→ 1 CRIME_CATEGORIES (category_id)
│   ├─ N cases ──→ 1 ZONES (zone_id)
│   ├─ N cases ──→ 1 ACCOUNTS (reported_by_account)
│   ├─ 1 case_id ──┬─→ N in CASE_ASSIGNMENTS (case_id)
│   │              ├─→ N in INVESTIGATION_UPDATES (case_id)
│   │              └─→ N in EVIDENCE_RECORDS (case_id)
│   └─ Represents: Incidents and cases

CASE_ASSIGNMENTS (Operational Junction)
│   ├─ N assignments ──→ 1 CASES (case_id)
│   ├─ N assignments ──→ 1 ACCOUNTS (assigned_to_group)
│   ├─ N assignments ──→ 1 ACCOUNTS (assigned_by_account)
│   └─ Represents: Case assignments with history

INVESTIGATION_UPDATES (Operational Fact)
│   ├─ N updates ──→ 1 CASES (case_id)
│   ├─ N updates ──→ 1 ACCOUNTS (updated_by_account)
│   └─ Represents: Case progress timeline

EVIDENCE_RECORDS (Operational Fact)
│   ├─ N evidence ──→ 1 CASES (case_id)
│   ├─ N evidence ──→ 1 ACCOUNTS (uploaded_by_account)
│   └─ Represents: Digital evidence chain of custody

AUDIT_LOGS (Compliance - Universal)
│   └─ N logs ──→ 1 ACCOUNTS (changed_by_account)
└─ Represents: Complete audit trail (never delete)
```

### Relationship Cardinality Summary

| Relationship | Cardinality | Type | Purpose |
|---|---|---|---|
| Zone → Accounts | 1:N | Organizational | Zone assignment for teams |
| Zone → Cases | 1:N | Operational | Geographic case distribution |
| Crime Category → Cases | 1:N | Operational | Crime classification |
| Accounts → Cases | 1:N | Operational | Case reporter tracking |
| Cases → Assignments | 1:N | Operational | Case allocation history |
| Accounts → Assignments (group) | 1:N | Operational | Group receives assignments |
| Accounts → Assignments (admin) | 1:N | Operational | Admin creates assignments |
| Cases → Updates | 1:N | Operational | Investigation timeline |
| Accounts → Updates | 1:N | Operational | Update author tracking |
| Cases → Evidence | 1:N | Operational | Evidence per case |
| Accounts → Evidence | 1:N | Operational | Evidence uploader |
| Accounts → Audit Logs | 1:N | Compliance | Action attribution |

---

## ✨ CAPABILITIES

### What the Database Can Do

#### 1. **Complete Case Lifecycle Management**
```
✓ Create cases with full incident details
✓ Automatically categorize by crime type
✓ Assign to geographic zones
✓ Track status through workflow (NEW → ASSIGNED → IN_PROGRESS → COMPLETED)
✓ Maintain assignment history (reassignments)
✓ Document investigation progress
✓ Log all changes in audit trail
```

#### 2. **Multi-Level Role Management**
```
✓ System Administrators (full access)
✓ Zone Inspectors (zone-level oversight)
✓ Officer Groups/Teams (case execution)
✓ Role-based query filtering
✓ Account activation/deactivation
```

#### 3. **Evidence Chain of Custody**
```
✓ Log evidence with timestamp & uploader
✓ Link evidence to specific cases
✓ Track who uploaded what and when
✓ Support for digital file references
✓ Complete audit trail of evidence handling
✓ Immutable evidence records
```

#### 4. **Investigation Timeline**
```
✓ Chronological case updates
✓ Multiple updates per case
✓ Author attribution (who, when, what)
✓ Full investigation progress history
✓ Queryable timeline reconstruction
```

#### 5. **Geographic Analytics**
```
✓ Crime distribution by zone
✓ Case load distribution
✓ Zone-specific crime categories
✓ Zone commander dashboards
✓ Cross-zone comparisons
```

#### 6. **Comprehensive Audit Logging**
```
✓ Log all INSERT operations
✓ Log all UPDATE operations (with old/new values)
✓ Log all DELETE operations
✓ JSON storage of complex changes
✓ Timestamp on every change
✓ Attribution to specific account
✓ Non-repudiation (cannot deny action)
✓ Regulatory compliance ready
```

#### 7. **Data Integrity Guarantees**
```
✓ Foreign Key constraints (referential integrity)
✓ Unique constraints (no duplicates)
✓ ON DELETE RESTRICT (prevent orphaned records)
✓ ON DELETE CASCADE (clean removal of related data)
✓ ACID compliance (InnoDB engine)
✓ Transaction support
✓ Concurrent access control
```

#### 8. **Query Performance**
```
✓ Indexed searches by case number
✓ Fast filtering by status
✓ Zone-based queries (optimized)
✓ Time-range queries (date indexes)
✓ Category searches (indexed)
✓ Support for 50+ concurrent users
✓ Sub-second response times (optimized)
```

---

## 🛡️ DELETE STRATEGY: RESTRICT (Protective Design)

### Why RESTRICT is Better for Law Enforcement

Our database uses **RESTRICT deletion** across all critical tables. This is intentional and beneficial:

```
RESTRICT Strategy in Action:

Scenario 1: Try to delete a crime category
┌──────────────────────────────────────────┐
│ DELETE FROM crime_categories             │
│ WHERE category_id = 5                    │
│                                          │
│ Result: ERROR! ❌                        │
│ "Cannot delete category with cases"      │
│ (10 cases linked to this category)       │
└──────────────────────────────────────────┘

Solution:
1. Archive/rename category instead
2. Or move cases to different category
3. Then delete category (safer!)

Scenario 2: Try to delete a case
┌──────────────────────────────────────────┐
│ DELETE FROM cases                        │
│ WHERE case_id = 1001                     │
│                                          │
│ Result: ERROR! ❌                        │
│ "Cannot delete case with assignments"    │
│ (3 assignments, 8 evidence items exist)  │
└──────────────────────────────────────────┘

Proper Procedure:
1. Mark case: status = 'ARCHIVED'
2. Delete evidence records (if needed)
3. Delete assignments (if needed)
4. Finally DELETE case
5. Audit trail preserved throughout!
```

### Comparison: RESTRICT vs CASCADE

| Aspect | RESTRICT | CASCADE |
|--------|----------|---------|
| **Accidental Deletion** | ✅ Prevented | ❌ Dangerous |
| **Data Preservation** | ✅ Forces explicit cleanup | ❌ Auto-deletes everything |
| **Audit Trail** | ✅ Complete history | ⚠️ May delete logs |
| **Recovery** | ✅ Easier (data still exists) | ❌ Harder (data gone) |
| **Governance** | ✅ Enforces procedures | ❌ No constraints |
| **Regulatory** | ✅ Evidence retention | ❌ Risky for compliance |

### What RESTRICT Prevents

```
✅ Prevents accidental category deletion
   └─ Database error instead of silent data loss

✅ Prevents accidental zone deletion
   └─ Keeps cases intact

✅ Prevents accidental case deletion with children
   └─ Forces proper archival workflow

✅ Prevents evidence loss
   └─ Cannot delete case while evidence exists

✅ Maintains referential integrity
   └─ No broken links in audit trail

✅ Forces proper procedures
   └─ Encourages archive status over deletion
```

---

## ⚠️ LIMITATIONS

### Design Constraints & Boundaries

#### 1. **No Individual Officer Records**
```
LIMITATION: Database stores ACCOUNTS (Desks/Groups/Teams), 
           NOT individual officers

WHY: Law enforcement operates as teams, not individuals
- Officers rotate between teams/shifts
- Cases assigned to units, not specific officers
- Maintains organizational focus over individuals

WORKAROUND: If individual tracking needed:
- Create additional OFFICERS table
- Link officers to ACCOUNTS groups via OFFICER_ASSIGNMENTS
- Track individual performance separately
- Maintain team structure as primary unit
```

#### 2. **RESTRICT Delete Creates Protection** (Design Feature)
```
CHARACTERISTIC: Not a limitation - this is a FEATURE!

RESTRICT on:
- Crime Categories: Cannot delete if cases exist
- Zones: Cannot delete if cases exist
- Cases: Cannot delete if assignments/evidence exist

BENEFIT: Prevents accidental data loss
- Requires archive status instead of deletion
- Maintains referential integrity
- Forces proper cleanup procedures

WORKAROUND if deletion needed:
- Change case status to 'ARCHIVED' instead of DELETE
- Create archival procedure before deletion
- Implement soft deletes (is_deleted flag)
```

#### 3. **Investigation Updates No Automatic History**
```
LIMITATION: No automatic version history tracking

Example: If investigation_updates.description is edited directly
         Change is not logged (unless application handles it)

WHY: Keeps data model focused on investigation timeline
     Historical changes handled via application layer

SOLUTION: Application should:
- Archive old updates to separate table
- Never modify update.description (append-only)
- Log modifications via audit_logs
```

#### 4. **Single Case Assignment at a Time**
```
CHARACTERISTIC: By design - one ACTIVE assignment per case

WHY: Clear responsibility (one group per case)
     Prevents ambiguous case ownership
     Reassignment history tracked via status = 'REASSIGNED'

WORKAROUND if multiple groups needed:
- Create case_assignment_groups junction table
- Change status to track concurrent assignments
- Implement conflict resolution logic
```

#### 5. **No Built-in Witness/Suspect Management**
```
LIMITATION: victim_name and suspect_name are TEXT fields
           No structured person/entity records

WHY: Keeps scope focused on case management
     Complex person management requires privacy/GDPR controls
     Can be extended later without schema breaking change

FUTURE ENHANCEMENT:
- Create PERSONS table for detailed tracking
- Link via person_id instead of text names
- Track contact info, history, relationships
- Implement privacy controls
```

#### 6. **File Storage External to Database**
```
LIMITATION: Evidence files stored on file system
           Database only stores file_path (reference)

WHY: Binary data in databases is inefficient
     File system more suitable for documents/media
     Enables CDN or cloud storage integration

RISK MITIGATION:
- Implement file integrity checks (checksums)
- Regular backup of file storage
- Document file management procedures
- Maintain file_path consistency

BEST PRACTICE:
- Use unique identifiers (evidence_id) in file paths
- Example: /evidence/2024/case_001/evidence_005_photo.jpg
- Enable file soft-deletes (move to trash instead of delete)
```

#### 7. **No Real-time Database Notifications**
```
LIMITATION: Database doesn't push updates to clients
           Application must poll or use messaging

WHY: SQL databases are passive (request-response)
     Real-time requires application layer handling

SOLUTION OPTIONS:
- Application implements polling every N seconds
- Use message queue (RabbitMQ, Kafka)
- SignalR for real-time WebSocket updates (future)
```

#### 8. **Scalability Ceiling (Single Instance)**
```
LIMITATION: Single MySQL instance
           ~50 concurrent users maximum
           ~5,000-10,000 cases annually

WHEN SCALING NEEDED:
- 100+ concurrent users → Read replicas
- 50,000+ annual cases → Partitioning by year/zone
- Geographic distribution → Multi-region setup

SCALING STRATEGY:
- Horizontal: Add read replicas for reporting
- Vertical: Upgrade server resources
- Partitioning: Split by case_year or zone_id
```

#### 9. **No Built-in Reporting Database**
```
LIMITATION: Analytics queries run against operational database
           Heavy reports may slow production performance

SOLUTION (Future):
- Implement nightly ETL to reporting database
- Keep operational DB lightweight (OLTP)
- Reporting DB optimized for analytics (OLAP)
- No impact on case creation/updates

CURRENT SOLUTION:
- Optimize queries with proper indexes
- Use database views for common reports
- Run heavy reports during off-hours
```

#### 10. **Limited Geospatial Features**
```
LIMITATION: No built-in geographic/mapping functions
           Zone management is text-based (zone_id, zone_name)

WHY: Adds complexity, optional for basic operations

FUTURE ENHANCEMENT:
- Add latitude/longitude to ZONES table
- Implement distance calculations
- Use PostGIS (if migrating to PostgreSQL)
- Integrate mapping API (Google Maps, Leaflet)
- Current solution suitable for district/precinct level
```

#### 11. **No Multi-Tenancy Support**
```
LIMITATION: Database designed for single district/agency
           Not multi-tenant out of box

WHY: Simplifies queries and reduces complexity
     Multi-tenancy adds significant overhead

FUTURE EXTENSION:
- Add tenant_id to all tables
- Implement row-level security
- Separate by schema or database
- Add tenant_id to audit_logs
```

#### 12. **Account Deletion Caution**
```
LIMITATION: Cannot fully delete account if it created audit logs
           changed_by_account is NOT NULL foreign key

WHY: Maintains audit trail integrity
     Preserves accountability

SOLUTION if account must be removed:
- Deactivate account: Set account_status = 'INACTIVE'
- Keep account record (still referenced in audit_logs)
- No longer allowed to login
- Historical actions remain attributed
```

---

## 🎯 DESIGN DECISIONS

### Why We Chose This Architecture

#### 1. **InnoDB Engine**
```
✓ ACID compliance (critical for police data)
✓ Foreign key constraints (referential integrity)
✓ Crash recovery (data safety)
✓ Transaction support (multi-step operations)
✓ Row-level locking (concurrent access)
```

#### 2. **UTF8MB4 Character Set**
```
✓ Support international characters (names, descriptions)
✓ Emoji support (if needed for future features)
✓ Backward compatible with UTF8
✓ Standard for web/modern applications
```

#### 3. **JSON for Audit Logs**
```
✓ Flexible schema (changing table structures)
✓ Human-readable before/after values
✓ No need to update audit schema
✓ Better than serialized data
```

#### 4. **Enum for Status Fields**
```
✓ Type safety (only valid values)
✓ Compact storage (smaller than VARCHAR)
✓ Faster queries than text lookups
✓ Clear valid states in schema
```

#### 5. **ON DELETE RESTRICT Strategy (Safety First)**
```
STRATEGIC CHOICE: RESTRICT on all critical data

ON DELETE RESTRICT used for:
├─ Crime Categories: Cannot delete category with cases
├─ Zones: Cannot delete zone with cases
├─ Cases: Cannot delete case with assignments/evidence
└─ Evidence: Cannot delete case with evidence

WHY RESTRICT (not CASCADE):
├─ Prevents accidental data loss
├─ Forces proper archive procedures
├─ Maintains data integrity
├─ Requires explicit deletion workflow
└─ Safer for law enforcement operations

DELETION WORKFLOW:
1. Mark case status = 'ARCHIVED' (soft delete)
2. Delete related records explicitly (if needed)
3. Finally DELETE case record
4. Audit trail remains permanent

BENEFIT: You cannot accidentally delete case + all history!
```

#### 6. **Timestamps on Every Table**
```
✓ created_at - When record created
✓ updated_at - When last modified
✓ Supports audit trail reconstruction
✓ Helps identify recent changes
```

---

## 📊 DATA FLOW

### Typical Data Flow Through the System

```
1. INCIDENT REPORTED
   └─→ Inspector creates CASE record
       • Enters incident details
       • Selects CRIME_CATEGORY
       • Selects ZONE
       • Audit log: INSERT into CASES

2. CASE ASSIGNMENT
   └─→ Inspector creates CASE_ASSIGNMENT
       • Links CASE to OFFICER_GROUP
       • Status = ACTIVE
       • Audit log: INSERT into CASE_ASSIGNMENTS
       • Case status updates to ASSIGNED
       • Audit log: UPDATE CASES

3. INVESTIGATION UNDERWAY
   └─→ Officer group posts INVESTIGATION_UPDATE
       • Adds timeline entry
       • Describes progress
       • Audit log: INSERT into INVESTIGATION_UPDATES

4. EVIDENCE LOGGED
   └─→ Officer uploads EVIDENCE_RECORD
       • Associates with CASE
       • Stores file path
       • Creates chain of custody entry
       • Audit log: INSERT into EVIDENCE_RECORDS

5. CASE REASSIGNMENT (if needed)
   └─→ Inspector reassigns case
       • Old assignment: status = REASSIGNED
       • Audit log: UPDATE CASE_ASSIGNMENTS
       • New assignment created: status = ACTIVE
       • Audit log: INSERT CASE_ASSIGNMENTS

6. CASE REASSIGNMENT (if needed)
   └─→ Inspector reassigns case
       • Old assignment: status = REASSIGNED
       • Audit log: UPDATE CASE_ASSIGNMENTS
       • New assignment created: status = ACTIVE
       • Audit log: INSERT CASE_ASSIGNMENTS

7. CASE CLOSURE
   └─→ Inspector closes case
       • Case status = COMPLETED
       • Assignment status = COMPLETED
       • Audit log: UPDATE CASES
       • Audit log: UPDATE CASE_ASSIGNMENTS

8. CASE ARCHIVAL (if needed)
   └─→ When case reaches final closure
       • Case status = 'ARCHIVED' (preferred over DELETE)
       • All child records remain intact
       • Audit log: UPDATE CASES
       • Full history preserved

9. AUDIT TRAIL PERMANENT
   └─→ Audit logs remain regardless
       • WHO made each change
       • WHAT changed (old/new values)
       • WHEN changes occurred
       • CONSTRAINT: All references valid (RESTRICT prevents orphans)
       • Non-repudiation maintained
```

---

## 🚀 OPTIMIZATION TIPS

### Query Optimization

#### High-Traffic Queries (Should Be Indexed)

```sql
-- Find all cases in a zone
SELECT * FROM cases 
WHERE zone_id = ? AND status != 'CANCELLED'
ORDER BY created_at DESC;
-- Index: idx_case_zone, idx_case_status

-- Find active assignments for a case
SELECT * FROM case_assignments 
WHERE case_id = ? AND status = 'ACTIVE';
-- Index: idx_assign_case, idx_assign_status

-- Get all updates for a case timeline
SELECT * FROM investigation_updates 
WHERE case_id = ? 
ORDER BY update_date ASC;
-- Index: idx_investigation_case

-- Find cases by category in zone
SELECT * FROM cases 
WHERE zone_id = ? AND category_id = ? 
ORDER BY created_at DESC;
-- Index: idx_case_zone, idx_case_category
```

#### Aggregate Queries (For Reporting)

```sql
-- Crime statistics by zone
SELECT 
    z.zone_name,
    COUNT(*) as total_cases,
    COUNT(CASE WHEN c.status = 'COMPLETED' THEN 1 END) as resolved
FROM zones z
LEFT JOIN cases c ON z.zone_id = c.zone_id
GROUP BY z.zone_id;

-- Case load distribution
SELECT 
    a.account_name,
    COUNT(*) as assigned_cases
FROM case_assignments ca
JOIN accounts a ON ca.assigned_to_group = a.account_id
WHERE ca.status = 'ACTIVE'
GROUP BY ca.assigned_to_group
ORDER BY assigned_cases DESC;
```

### Index Strategy

**Primary Indexes** (Essential):
```sql
-- Natural keys
CREATE UNIQUE INDEX idx_case_number ON cases(case_number);
CREATE UNIQUE INDEX idx_username ON accounts(username);
CREATE UNIQUE INDEX idx_zone_name ON zones(zone_name);

-- Foreign keys (for JOINs)
CREATE INDEX idx_cases_category ON cases(category_id);
CREATE INDEX idx_cases_zone ON cases(zone_id);
CREATE INDEX idx_assignments_case ON case_assignments(case_id);

-- Status filters (common WHERE clause)
CREATE INDEX idx_case_status ON cases(status);
CREATE INDEX idx_assign_status ON case_assignments(status);

-- Time-based queries
CREATE INDEX idx_case_created ON cases(created_at);
CREATE INDEX idx_audit_timestamp ON audit_logs(changed_at);
```

**Secondary Indexes** (For Performance):
```sql
-- Combined indexes for common filters
CREATE INDEX idx_case_zone_status ON cases(zone_id, status);
CREATE INDEX idx_audit_table_record ON audit_logs(table_name, record_id);
```

### Maintenance

```sql
-- Check index usage
SELECT * FROM INFORMATION_SCHEMA.STATISTICS 
WHERE TABLE_SCHEMA = 'district_police';

-- Rebuild fragmented indexes
OPTIMIZE TABLE cases;
OPTIMIZE TABLE case_assignments;
OPTIMIZE TABLE audit_logs;

-- Update table statistics
ANALYZE TABLE cases;
ANALYZE TABLE case_assignments;
```

---

## 📈 Capacity Planning

### Expected Growth

```
Year 1: 5,000 cases
├─ CASES: ~5,000 records
├─ CASE_ASSIGNMENTS: ~5,500 (with reassignments)
├─ INVESTIGATION_UPDATES: ~20,000 (4 updates/case avg)
├─ EVIDENCE_RECORDS: ~30,000 (6 evidence items/case avg)
└─ AUDIT_LOGS: ~100,000+ (20 audits per case)

Year 3: 15,000 cases
├─ Database size: ~1-2 GB
├─ Audit logs alone: ~300,000+ entries
├─ Requires periodic archival

Year 5: 25,000+ cases
├─ Database size: ~3-5 GB
├─ Recommend partitioning by year
├─ Possible migration to larger instance
```

### Storage Estimation

```
Per Case:
├─ Cases table: ~1 KB
├─ Assignments (avg 1.1): ~1.5 KB
├─ Updates (avg 4): ~2 KB
├─ Evidence (avg 6): ~3 KB
├─ Audit logs (avg 20): ~15 KB
└─ Total per case: ~22 KB

Example:
5,000 cases × 22 KB = ~110 MB (data)
+ Indexes (30% overhead) = ~143 MB
+ Backups/redundancy = ~300-400 MB
```

---

## 🔐 Backup & Recovery

### Backup Strategy

```bash
# Full backup (daily at 2 AM)
mysqldump -u root -p district_police > backup_$(date +%Y%m%d).sql

# Backup to cloud (daily)
aws s3 cp backup_*.sql s3://police-backups/

# Retention: Keep 30 days of daily + 12 monthly
```

### Recovery Procedures

```bash
# Restore from backup
mysql -u root -p district_police < backup_20240115.sql

# Point-in-time recovery (if binary logs enabled)
mysqlbinlog bin.000001 --stop-datetime='2024-01-15 10:00:00' | mysql
```

---

## 📚 Related Documentation

- **[README.md](./README.md)** - Project overview and quick start
- **[ARCHITECTURE.md](./ARCHITECTURE.md)** - Feature-based architecture
- **[USER_MANUAL.md](./docs/USER_MANUAL.md)** - User guide
- **[DEPLOYMENT.md](./docs/DEPLOYMENT.md)** - Production deployment

---

<div align="center">

**Database Documentation v1.0** | District Police Management System

*Last Updated: January 2024*

</div>
