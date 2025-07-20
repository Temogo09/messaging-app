-- Insert sample users
INSERT INTO tblUserAccount (UserName, PhoneNumber, Passwordd, IsOnline)
VALUES 
('john_doe', '+12345678901', 'password123', 1),
('jane_smith', '+19876543210', 'securepass', 0),
('mike_jones', '+11223344556', 'mikepass', 1),
('sarah_lee', '+15556667777', 'sarah123', 0),
('alex_wong', '+18889990000', 'alexpass', 1);

-- Insert contacts (john knows jane and mike; jane knows sarah, etc.)
INSERT INTO tblUserContact (UserID, ContactUserID, ContactName)
VALUES
(1, 2, 'Jane (Work)'),
(1, 3, 'Mike'),
(2, 1, 'John Doe'),
(2, 4, 'Sarah'),
(3, 1, 'John'),
(4, 2, 'Jane Smith'),
(5, 1, 'John D.');

-- Insert groups
INSERT INTO tblGroupChat (GroupName, AdminID)
VALUES
('Work Team', 1),
('Friends Circle', 2),
('Project Alpha', 5);

-- Add members to groups
INSERT INTO tblGroupMember (GroupID, UserID, IsAdmin)
VALUES
(1, 1, 1),  -- John is admin of Work Team
(1, 2, 0),  -- Jane is member
(1, 5, 0),  -- Alex is member
(2, 2, 1),  -- Jane is admin of Friends Circle
(2, 1, 0),  -- John is member
(2, 4, 0),  -- Sarah is member
(3, 5, 1),  -- Alex is admin of Project Alpha
(3, 1, 1),  -- John is also admin
(3, 3, 0);  -- Mike is member

-- Insert base messages
INSERT INTO tblMessage (SenderID, MessageText, SentAt, IsRead, MessageStatus)
VALUES
(1, 'Hey Jane, how are you?', DATEADD(MINUTE, -30, GETDATE()), 1, 2),
(2, 'I''m good John! Working on the project.', DATEADD(MINUTE, -25, GETDATE()), 1, 2),
(1, 'Can we meet tomorrow?', DATEADD(MINUTE, -20, GETDATE()), 1, 2),
(2, 'Sure, 2 PM works?', DATEADD(MINUTE, -15, GETDATE()), 1, 2),
(5, 'Team, we need to discuss the deadline', DATEADD(MINUTE, -10, GETDATE()), 0, 1),
(2, 'Everyone, party at my place Friday!', DATEADD(MINUTE, -5, GETDATE()), 0, 1);

-- Link messages to private conversations
INSERT INTO tblPrivateMessage (MessageID, ReceiverID)
VALUES
(1, 2),  -- John to Jane
(2, 1),  -- Jane to John
(3, 2),  -- John to Jane
(4, 1);  -- Jane to John

-- Link messages to groups
INSERT INTO tblGroupMessage (MessageID, GroupID)
VALUES
(5, 1),  -- Alex to Work Team
(6, 2);  -- Jane to Friends Circle