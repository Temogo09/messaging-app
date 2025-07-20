CREATE TABLE tblUserAccount
(
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(30) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(15) UNIQUE NOT NULL,
    ProfilePicture VARBINARY(MAX) NULL,
    IsOnline BIT DEFAULT 0,
);

CREATE TABLE tblUserContact
(
    ContactID INT IDENTITY(1,1) PRIMARY KEY,
    UserAccountID INT NOT NULL, 
    ContactUserID INT NOT NULL,
    ContactName VARCHAR(50) NULL,  
    FOREIGN KEY (UserAccountID) REFERENCES tblUserAccount(UserID),
    FOREIGN KEY (ContactUserID) REFERENCES tblUserAccount(UserID),
    CONSTRAINT UC_UserContact UNIQUE (UserAccountID, ContactUserID)
);

CREATE TABLE tblGroupChat
(
    GroupID INT IDENTITY(1,1) PRIMARY KEY,
    GroupName VARCHAR(50) NOT NULL,
    AdminID INT NOT NULL,
    GroupPicture VARBINARY(MAX) NULL,
    FOREIGN KEY (AdminID) REFERENCES tblUserAccount(UserID)
);

CREATE TABLE tblGroupMember
(
    GroupID INT NOT NULL,
    UserID INT NOT NULL,
    IsAdmin BIT DEFAULT 0,
    PRIMARY KEY (GroupID, UserID),
    FOREIGN KEY (GroupID) REFERENCES tblGroupChat(GroupID),
    FOREIGN KEY (UserID) REFERENCES tblUserAccount(UserID)
);

CREATE TABLE tblMessage
(
    MessageID INT IDENTITY(1,1) PRIMARY KEY,
    SenderID INT NOT NULL,                        
    MessageText NVARCHAR(MAX) NOT NULL,
    SentAt DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0,
    MessageStatus TINYINT DEFAULT 0, -- 0=sent, 1=delivered, 2=read
    FOREIGN KEY (SenderID) REFERENCES tblUserAccount(UserID)
);


CREATE TABLE tblPrivateMessage (
    MessageID INT PRIMARY KEY,
    ReceiverID INT NOT NULL,
    FOREIGN KEY (MessageID) REFERENCES tblMessage(MessageID),
    FOREIGN KEY (ReceiverID) REFERENCES tblUserAccount(UserID)
);

CREATE TABLE tblGroupMessage (
    MessageID INT PRIMARY KEY,
    GroupID INT NOT NULL,
    FOREIGN KEY (MessageID) REFERENCES tblMessage(MessageID),
    FOREIGN KEY (GroupID) REFERENCES tblGroupChat(GroupID)
);


