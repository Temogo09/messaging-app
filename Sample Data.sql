-- Users
INSERT INTO tblUserAccount (UserName, PhoneNumber, ProfilePicture, IsOnline)
VALUES 
('Alice', '1234567890', NULL, 1),
('Bob', '2345678901', NULL, 0),
('Charlie', '3456789012', NULL, 1);

-- Contacts
INSERT INTO tblUserContact (UserAccountID, ContactUserID, ContactName)
VALUES
(1, 2, 'Bob'),
(1, 3, 'Charlie'),
(2, 1, 'Alice');

-- Group
INSERT INTO tblGroupChat (GroupName, AdminID, GroupPicture)
VALUES ('Study Group', 1, NULL);

-- Group Members
INSERT INTO tblGroupMember (GroupID, UserID, IsAdmin)
VALUES 
(1, 1, 1),
(1, 2, 0),
(1, 3, 0);

-- Messages
INSERT INTO tblMessage (SenderID, MessageText)
VALUES 
(1, 'Hello everyone!'),
(2, 'Hi Alice!'),
(3, 'Good morning!');

-- Private Messages
INSERT INTO tblPrivateMessage (MessageID, ReceiverID)
VALUES 
(1, 2),
(2, 1);

-- Group Message
INSERT INTO tblGroupMessage (MessageID, GroupID)
VALUES 
(3, 1);