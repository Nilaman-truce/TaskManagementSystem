CREATE DATABASE TaskManagement;

USE TaskManagement;

CREATE TABLE Status (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL
);

CREATE TABLE Priority (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL
);

CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME NULL,
    StatusId INT NOT NULL,
    PriorityId INT NOT NULL
    
);

INSERT INTO Status (Title) VALUES ('Pending');
INSERT INTO Status (Title) VALUES ('In Progress');
INSERT INTO Status (Title) VALUES ('Completed');

INSERT INTO Priority (Title) VALUES ('High');
INSERT INTO Priority (Title) VALUES ('Medium');
INSERT INTO Priority (Title) VALUES ('Low');

INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) 
VALUES ('Add Report Section', 'Add Report Section', '2025-02-20', 1, 1);

INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) 
VALUES ('CRUD Operation for tasks', 'CRUD Operation for tasks', '2025-03-05', 2, 2);


INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) 
VALUES ('Add Report Section', 'Add Report Section', '2025-02-28', 1, 2);

INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) 
VALUES ('CRUD Operation for tasks', 'CRUD Operation for tasks', '2025-03-05', 3, 1);

INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) 
VALUES ('Show required Field in Create Task Modal', 'Show required Field in Create Task Modal', '2025-03-05', 3, 2);


INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) 
VALUES ('Fix Text alignment ', 'Fix Text alignment ', '2025-03-05', 1, 3);


INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) 
VALUES ('Add DatePicker ', 'Add DatePicker', '2025-03-05', 2, 3);
