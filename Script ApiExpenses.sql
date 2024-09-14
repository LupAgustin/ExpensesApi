-- Create Users table
CREATE TABLE Users (
    Id VARCHAR(36) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
	Role NVARCHAR(10) NOT NULL
);

-- Create Categories table
CREATE TABLE Categories (
    Id VARCHAR(36) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

-- Create Expenses table
CREATE TABLE Expenses (
    Id VARCHAR(36) PRIMARY KEY, -- GUID as VARCHAR
	UserId VARCHAR(36) NOT NULL, -- Foreign key from Users
    CategoryId VARCHAR(36) NOT NULL, -- Foreign key from Categories
    Amount DECIMAL(10, 2) NOT NULL,
    Date datetime NOT NULL,
    CONSTRAINT FK_Expenses_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_Expenses_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Insert two users with GUIDs for Id
INSERT INTO Users (Id, Username, Password, Role)
VALUES
(NEWID(), 'user1', 'hashedpassword1', 'User'), -- Replace 'hashedpassword1' with the actual hashed password
(NEWID(), 'user2', 'hashedpassword2', 'Admin'); -- Replace 'hashedpassword2' with the actual hashed password

-- Insert predefined categories into Categories table
INSERT INTO Categories (Id, Name)
VALUES 
    (NEWID(), 'Groceries'),
    (NEWID(), 'Leisure'),
    (NEWID(), 'Electronics'),
    (NEWID(), 'Utilities'),
    (NEWID(), 'Clothing'),
    (NEWID(), 'Health'),
    (NEWID(), 'Others');