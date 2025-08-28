
-- Get Id OF Role (Admin)
SELECT Id FROM AspNetRoles WHERE Name = 'Admin';


-- Delete Student Role from user
DELETE FROM AspNetUserRoles
WHERE UserId = 'userid'
  AND RoleId = (SELECT Id FROM AspNetRoles WHERE Name = 'Student');


  -- Add Admin Role for User
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('UserId', 'AdminId');

-- Confirm Email for a specific user
UPDATE [Educational-Platform].[dbo].[AspNetUsers]
SET EmailConfirmed = 1
WHERE Id = 'Userid';


-- Get All User With Roles (To check)
SELECT u.Id AS UserId,
       u.UserName,
       u.Email,
       r.Name AS RoleName
FROM AspNetUsers u
JOIN AspNetUserRoles ur ON u.Id = ur.UserId
JOIN AspNetRoles r ON ur.RoleId = r.Id
ORDER BY u.UserName;

