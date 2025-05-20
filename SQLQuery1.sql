--Tạo DataBase
CREATE DATABASE EduMasterDB;
GO
USE EduMasterDB;
GO

--Tạo Table:
--Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Teacher', 'Student')),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    vatarPath NVARCHAR(100),
    QueQuan NVARCHAR(50),
    SoDienThoai NVARCHAR(10),
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1  -- 1: Active, 0: Deactivated
);
ALTER TABLE Users 
ADD AvatarPath NVARCHAR(100), 
	QueQuan NVARCHAR(50), 
	SoDienThoai NVARCHAR(10);

--Courses
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseCode NVARCHAR(20) UNIQUE NOT NULL,
    CourseName NVARCHAR(200) NOT NULL,
    TeacherID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Description NVARCHAR(MAX),
    MaxEnrollment INT DEFAULT 50, -- Số lượng tối đa mặc định là 50
    RegistrationDeadline DATETIME NULL -- Hạn đăng ký
);
--Bảng Đăng Ký Khóa Học
CREATE TABLE CourseEnrollments (
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    CourseID INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseID),
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT UC_Enrollment UNIQUE (StudentID, CourseID)  -- Mỗi sinh viên chỉ đăng ký 1 lần
);
--Bảng Tài Liệu Khóa Học
CREATE TABLE CourseDocuments (
    DocumentID INT PRIMARY KEY IDENTITY(1,1),
    CourseID INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseID),
    Title NVARCHAR(200) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,  -- Lưu đường dẫn vật lý hoặc URL
    UploadedBy INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    UploadDate DATETIME DEFAULT GETDATE(),
    DocumentType NVARCHAR(50) CHECK (DocumentType IN ('PDF', 'Video', 'Slide', 'Other'))
);
--Bảng Bài Tập
CREATE TABLE Assignments (
    AssignmentID INT PRIMARY KEY IDENTITY(1,1),
    CourseID INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseID),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    DueDate DATETIME NOT NULL,
    MaxScore DECIMAL(5,2) DEFAULT 100,
    CreatedBy INT NOT NULL FOREIGN KEY REFERENCES Users(UserID)
);
--Bảng Bài Nộp
CREATE TABLE Submissions (
    SubmissionID INT PRIMARY KEY IDENTITY(1,1),
    AssignmentID INT NOT NULL FOREIGN KEY REFERENCES Assignments(AssignmentID),
    StudentID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    FilePath NVARCHAR(500) NOT NULL,
    SubmittedDate DATETIME DEFAULT GETDATE(),
    Score DECIMAL(5,2),
    Feedback NVARCHAR(MAX),
    CONSTRAINT UC_Submission UNIQUE (AssignmentID, StudentID)  -- Mỗi sinh viên chỉ nộp 1 lần/bài tập
);
--Bảng Phân Quyền
CREATE TABLE Permissions (
    PermissionID INT PRIMARY KEY IDENTITY(1,1),
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Teacher', 'Student')),
    CanCreateCourse BIT DEFAULT 0,
    CanDeleteCourse BIT DEFAULT 0,
    CanUploadMaterial BIT DEFAULT 0,
    CanGradeAssignment BIT DEFAULT 0,
    CanEnrollCourse BIT DEFAULT 0
);
CREATE NONCLUSTERED INDEX IDX_Users_Username ON Users(Username);
CREATE NONCLUSTERED INDEX IDX_Courses_TeacherID ON Courses(TeacherID);
CREATE NONCLUSTERED INDEX IDX_Submissions_AssignmentID ON Submissions(AssignmentID);
-- Đảm bảo TeacherID là NOT NULL
ALTER TABLE Courses
ALTER COLUMN TeacherID INT NOT NULL;

-- Thêm FOREIGN KEY với CASCADE
ALTER TABLE Courses
ADD CONSTRAINT FK_Courses_TeacherID
FOREIGN KEY (TeacherID) REFERENCES Users(UserID)
ON DELETE CASCADE;  -- Xóa Teacher → Xóa khóa học
GO

-- Tạo hàm check role
CREATE FUNCTION dbo.CheckUserRole(@UserID INT, @Role NVARCHAR(20))
RETURNS BIT
AS
BEGIN
    DECLARE @Result BIT = 0;
    SELECT @Result = 1 
    FROM Users 
    WHERE UserID = @UserID AND Role = @Role;
    RETURN @Result;
END;
GO

-- Thêm ràng buộc CHECK
ALTER TABLE Courses
ADD CONSTRAINT CHK_TeacherRole
CHECK (dbo.CheckUserRole(TeacherID, 'Teacher') = 1);


-- Bật IDENTITY_INSERT và chèn đủ dữ liệu
SET IDENTITY_INSERT Users ON;

INSERT INTO Users (
    UserID, 
    Username, 
    PasswordHash, 
    Role, 
    FullName, 
    Email  -- Các cột NOT NULL phải có giá trị
)
VALUES (
    1, 
    'teacher01', 
    'hashed_password',  -- Thay bằng giá trị thực tế
    'Teacher', 
    'Nguyễn Văn Giáo Viên', 
    'teacher@edumaster.edu.vn'
);

SET IDENTITY_INSERT Users OFF;

-- Thêm Admin
INSERT INTO Users (Username, PasswordHash, Role, FullName, Email)
VALUES (
    'admin01', 
    'hashed_password', 
    'Admin', 
    'Nguyễn Văn Admin', 
    'admin@edumaster.edu.vn'
);
-- Bước 1: Bật IDENTITY_INSERT
SET IDENTITY_INSERT Users ON;

-- Bước 2: Chèn dữ liệu với đầy đủ các cột NOT NULL
INSERT INTO Users (
    UserID, 
    Username, 
    PasswordHash, 
    Role, 
    FullName, 
    Email  -- Các cột NOT NULL phải được cung cấp
)
VALUES (
    3, 
    'teacher02', 
    'hashed_password1',  -- Thay bằng giá trị thực tế
    'Teacher', 
    'Nguyễn Văn Giáo Viên', 
    'teacher02@edumaster.edu.vn'
);

-- Bước 3: Tắt IDENTITY_INSERT
SET IDENTITY_INSERT Users OFF;

-- Thêm Sinh viên
INSERT INTO Users (Username, PasswordHash, Role, FullName, Email)
VALUES (
    'student01', 
    'hashed_password', 
    'Student', 
    'Lê Văn Sinh Viên', 
    'student@edumaster.edu.vn'
);

-- Thêm Khóa học
INSERT INTO Courses (CourseCode, CourseName, TeacherID, StartDate, EndDate, Description, MaxEnrollment, RegistrationDeadline)
VALUES 
('MATH101', N'Toán Cao Cấp', 1, '2024-06-01', '2024-08-31', N'Học phần Toán nâng cao cho sinh viên', 50, '2024-07-01'),
('PHIL101', N'Triết học Mác-Lênin', 2, '2024-06-01', '2024-08-31', N'Những nguyên lý cơ bản của chủ nghĩa Mác-Lênin', 40, '2024-07-01'),
('ENG101', N'Tiếng Anh Cơ Bản', 3, '2024-06-01', '2024-08-31', N'Kỹ năng tiếng Anh cơ bản cho sinh viên', 60, '2024-07-01');
GO

-- SP Đăng ký khóa học
CREATE PROCEDURE EnrollCourse
    @StudentID INT,
    @CourseID INT
AS
BEGIN
    INSERT INTO CourseEnrollments (StudentID, CourseID)
    VALUES (@StudentID, @CourseID);
END;
GO

-- SP Chấm điểm bài tập
CREATE PROCEDURE GradeAssignment
    @SubmissionID INT,
    @Score DECIMAL(5,2),
    @Feedback NVARCHAR(MAX)
AS
BEGIN
    UPDATE Submissions
    SET Score = @Score, Feedback = @Feedback
    WHERE SubmissionID = @SubmissionID;
END;
GO
-- Trigger tự động xóa bài nộp khi xóa bài tập
CREATE TRIGGER TR_DeleteSubmissions
ON Assignments
AFTER DELETE
AS
BEGIN
    DELETE FROM Submissions
    WHERE AssignmentID IN (SELECT AssignmentID FROM deleted);
END;
GO

-- Index cho các cột thường dùng để tìm kiếm
CREATE NONCLUSTERED INDEX IDX_Courses_StartDate ON Courses(StartDate);
CREATE NONCLUSTERED INDEX IDX_Submissions_StudentID ON Submissions(StudentID);

-- Tạo role cho từng loại user
CREATE ROLE AdminRole;
CREATE ROLE TeacherRole;
CREATE ROLE StudentRole;

-- Cấp quyền cho Admin
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Assignments TO AdminRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.CourseDocuments TO AdminRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.CourseEnrollments TO AdminRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Courses TO AdminRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO AdminRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Permissions TO AdminRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Submissions TO AdminRole;
-- Cấp quyền cho Giảng viên
GRANT SELECT, INSERT ON CourseDocuments TO TeacherRole;
GRANT EXECUTE ON GradeAssignment TO TeacherRole;

-- Cấp quyền cho Sinh viên
GRANT EXECUTE ON EnrollCourse TO StudentRole;

EXEC EnrollCourse @StudentID = 3, @CourseID = 3;  -- StudentID 3 đăng ký CourseID 1
SELECT * FROM Users;

SELECT 
    UserID AS StudentID,
    Username,
    FullName,
    Email
FROM Users
WHERE Role = 'Student';
-- Bước 1: Bật IDENTITY_INSERT
SET IDENTITY_INSERT Users ON;
-- Thêm sinh viên
INSERT INTO Users (UserID, Username, PasswordHash, Role, FullName, Email)
VALUES 
    (6,'student03', 'hash1', 'Student', 'Nguyễn Văn A', 'a@edu.vn'),
    (7,'student02', 'hash2', 'Student', 'Trần Thị B', 'b@edu.vn');

	SELECT 
    UserID AS StudentID,
    FullName
FROM Users
WHERE 
    UserID = 2 
    AND Role = 'Student';

	INSERT INTO CourseDocuments (CourseID, Title, FilePath, UploadedBy, DocumentType)
VALUES (
    3, 
    'Bài giảng Chương 1', 
    '/documents/math101_ch1.pdf', 
    1,  -- TeacherID
    'PDF'
);
Select * from Users
GO

CREATE FUNCTION dbo.CheckUploaderRole (@UserID INT)
RETURNS BIT
AS
BEGIN
    DECLARE @IsValid BIT = 0;
    SELECT @IsValid = 1
    FROM Users
    WHERE 
        UserID = @UserID 
        AND Role IN ('Teacher', 'Admin'); -- Chỉ Admin/Teacher được upload
    RETURN @IsValid;
END;
GO

CREATE TRIGGER TRG_CheckDocumentUploader
ON CourseDocuments
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @UploadedBy INT;
    SELECT @UploadedBy = UploadedBy FROM inserted;

    -- Nếu không phải Teacher/Admin, báo lỗi
    IF dbo.CheckUploaderRole(@UploadedBy) = 0
    BEGIN
        RAISERROR ('Chỉ Admin hoặc Teacher được phép upload tài liệu!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- Giả sử UserID 1 là Admin, UserID 2 là Teacher
INSERT INTO CourseDocuments (CourseID, Title, FilePath, UploadedBy, DocumentType)
VALUES 
    (3, 'Tài liệu Toán', '/docs/math.pdf', 1, 'PDF'), -- Admin upload
    (3, 'Bài giảng Video', '/videos/lecture.mp4', 3, 'Video'); -- Teacher upload
-- ✅ Thành công

-- Giả sử UserID 3 là Student
INSERT INTO CourseDocuments (CourseID, Title, FilePath, UploadedBy, DocumentType)
VALUES (3, 'Tài liệu Lý', '/docs/physics.pdf', 4, 'PDF');
-- ❌ Lỗi: "Chỉ Admin hoặc Teacher được phép upload tài liệu!"
Go
ALTER TRIGGER TRG_CheckDocumentUploader
ON CourseDocuments
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE dbo.CheckUploaderRole(i.UploadedBy) = 0
    )
    BEGIN
        RAISERROR ('Chỉ Admin hoặc Teacher được phép upload tài liệu!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- View danh sách sinh viên và khóa học đã đăng ký
CREATE VIEW StudentEnrollments AS
SELECT 
    u.FullName AS StudentName,
    c.CourseName,
    ce.EnrollmentDate
FROM Users u
JOIN CourseEnrollments ce ON u.UserID = ce.StudentID
JOIN Courses c ON ce.CourseID = c.CourseID;
GO

-- Sử dụng view
SELECT * FROM StudentEnrollments;
SET IDENTITY_INSERT Users ON;
INSERT INTO Users (UserID, Username, PasswordHash, Role, FullName, Email)
VALUES 
    (8,'admin02', '$2a$12$xyz123...', 'Admin', 'Nguyễn Văn C', 'c@edu.vn');

Select * from Users
ALTER TABLE CourseDocuments
ADD Documents NVARCHAR(20) CHECK (Documents IN ('Tài liệu', 'Bài tập')); -- Phân loại
ALTER TABLE CourseDocuments
ADD DueDate DATETIME NULL  -- Hạn nộp (nếu là bài tập);

SELECT Username, PasswordHash FROM Users 
INSERT INTO Users (
    UserID, 
    Username, 
    PasswordHash, 
    Role, 
    FullName, 
    Email  -- Các cột NOT NULL phải được cung cấp
)
VALUES (
    10, 
    'teacher08', 
    '123456',  -- Thay bằng giá trị thực tế
    'Teacher', 
    'Nguyễn Văn A', 
    'teacher08@edumaster.edu.vn'
);

UPDATE Users 
SET PasswordHash = '$2a$12$xyz123...' -- Thay bằng BCrypt hash thực tế
WHERE Username IN ('teacher08', 'student03', 'student02');

SELECT *
FROM Users 

ALTER TABLE Courses
ADD MaxEnrollment INT DEFAULT 50,
    RegistrationDeadline DATETIME NULL;


	Select * from Users
select * from Courses
select * from StudentCourse
select * from CourseEnrollments
ALTER TABLE Courses
DROP COLUMN RegistrationDeadline;


-- Giả sử TeacherID = 101 là một giảng viên hợp lệ
INSERT INTO Courses (CourseCode, CourseName, TeacherID, StartDate, EndDate, MaxEnrollment)
VALUES 
('CS101', N'Nhập môn Lập trình', 10, '2025-09-01', '2025-12-15', 50),
('CS201', N'Cấu trúc dữ liệu và giải thuật', 10, '2025-09-01', '2025-12-15', 50),
('DB301', N'Cơ sở dữ liệu', 3, '2025-09-01', '2025-12-15', 50),
('SE401', N'Phân tích và thiết kế hệ thống', 10, '2025-09-01', '2025-12-15', 50),
('AI501', N'Trí tuệ nhân tạo', 3, '2025-09-01', '2025-12-15', 50);

INSERT INTO Courses (CourseCode, CourseName, TeacherID, StartDate, EndDate, MaxEnrollment)
VALUES 
('CS102', N'Nhập môn Lập trình', 10, '2025-09-01', '2025-12-15', 50);


CREATE TABLE Grades (
    StudentID INT,
    CourseID INT,
    Score FLOAT,
    PRIMARY KEY (StudentID, CourseID),
    FOREIGN KEY (StudentID) REFERENCES Users(UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);


ALTER TABLE Grades
ADD TeacherID INT;

ALTER TABLE Grades
ADD CONSTRAINT FK_Grades_Teacher
FOREIGN KEY (TeacherID) REFERENCES Users(UserID);

CREATE TABLE Questions (
    QuestionID INT,
    AssignmentID INT,
    QuestionText NVARCHAR,
	OptionA NVARCHAR,
	OptionB NVARCHAR,
	OptionC NVARCHAR,
	OptionD NVARCHAR,
	CorrectAnswer NVARCHAR,
);
ALTER TABLE Questions
ALTER COLUMN QuestionID INT NOT NULL;

ALTER TABLE Questions
ADD CONSTRAINT PK_Questions PRIMARY KEY (QuestionID);

CREATE TABLE StudentAnswers (
    AnswerID INT IDENTITY(1,1) PRIMARY KEY,
    AssignmentID INT NOT NULL,
    StudentID INT NOT NULL,
    QuestionID INT NOT NULL,
    SelectedAnswer NVARCHAR(255),
    IsCorrect BIT,
);
CREATE TABLE StudentCourse (
    UserID INT NOT NULL,
    CourseID INT NOT NULL,
    EnrollmentDate DATETIME NOT NULL
);

CREATE TABLE AssignmentFiles (
    FileID INT IDENTITY(1,1) PRIMARY KEY,
    AssignmentID INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    UploadDate DATETIME NOT NULL
);

CREATE TABLE StudentSubmissions (
    SubmissionID INT PRIMARY KEY IDENTITY(1,1),
    AssignmentID INT NOT NULL,
    StudentID INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    SubmitDate DATETIME NOT NULL,
    Score FLOAT NULL,
);

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (UserID);

ALTER TABLE Courses
ADD CONSTRAINT PK_Courses PRIMARY KEY (CourseID);

ALTER TABLE Courses
ADD CONSTRAINT FK_Courses_Teacher FOREIGN KEY (TeacherID) REFERENCES Users(UserID);

ALTER TABLE Sessions
ADD CONSTRAINT PK_Sessions PRIMARY KEY (SessionID);

ALTER TABLE Sessions
ADD CONSTRAINT FK_Sessions_Course FOREIGN KEY (CourseID) REFERENCES Courses(CourseID);

ALTER TABLE Assignments
ADD CONSTRAINT PK_Assignments PRIMARY KEY (AssignmentID);

ALTER TABLE Assignments
ADD CONSTRAINT FK_Assignments_Course FOREIGN KEY (CourseID) REFERENCES Courses(CourseID);

ALTER TABLE Questions
ADD CONSTRAINT PK_Questions PRIMARY KEY (QuestionID);

ALTER TABLE Questions
ADD CONSTRAINT FK_Questions_Assignment FOREIGN KEY (AssignmentID) REFERENCES Assignments(AssignmentID);

ALTER TABLE AssignmentFiles
ADD CONSTRAINT PK_AssignmentFiles PRIMARY KEY (FileID);

ALTER TABLE AssignmentFiles
ADD CONSTRAINT FK_AssignmentFiles_Assignment FOREIGN KEY (AssignmentID) REFERENCES Assignments(AssignmentID);

ALTER TABLE StudentSubmissions
ADD CONSTRAINT PK_StudentSubmissions PRIMARY KEY (SubmissionID);

ALTER TABLE StudentSubmissions
ADD CONSTRAINT FK_StudentSubmissions_Assignment FOREIGN KEY (AssignmentID) REFERENCES Assignments(AssignmentID);

ALTER TABLE StudentSubmissions
ADD CONSTRAINT FK_StudentSubmissions_Student FOREIGN KEY (StudentID) REFERENCES Users(UserID);

ALTER TABLE StudentAnswers
ADD CONSTRAINT PK_StudentAnswers PRIMARY KEY (AnswerID);

ALTER TABLE StudentAnswers
ADD CONSTRAINT FK_StudentAnswers_Question FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID);

ALTER TABLE StudentAnswers
ADD CONSTRAINT FK_StudentAnswers_Student FOREIGN KEY (StudentID) REFERENCES Users(UserID);

ALTER TABLE CourseDocuments
ADD CONSTRAINT PK_CourseDocuments PRIMARY KEY (DocumentID);

ALTER TABLE CourseDocuments
ADD CONSTRAINT FK_CourseDocuments_Course FOREIGN KEY (CourseID) REFERENCES Courses(CourseID);

ALTER TABLE CourseEnrollments
ADD CONSTRAINT PK_CourseEnrollments PRIMARY KEY (EnrollmentID);

ALTER TABLE CourseEnrollments
ADD CONSTRAINT FK_CourseEnrollments_Course FOREIGN KEY (CourseID) REFERENCES Courses(CourseID);

ALTER TABLE CourseEnrollments
ADD CONSTRAINT FK_CourseEnrollments_Student FOREIGN KEY (StudentID) REFERENCES Users(UserID);

ALTER TABLE StudentCourse
ADD CONSTRAINT PK_StudentCourse PRIMARY KEY (UserID, CourseID);

ALTER TABLE StudentCourse
ADD CONSTRAINT FK_StudentCourse_User FOREIGN KEY (UserID) REFERENCES Users(UserID);

ALTER TABLE StudentCourse
ADD CONSTRAINT FK_StudentCourse_Course FOREIGN KEY (CourseID) REFERENCES Courses(CourseID);

ALTER TABLE Grades
ADD CONSTRAINT PK_Grades PRIMARY KEY (StudentID, CourseID);

ALTER TABLE Grades
ADD CONSTRAINT FK_Grades_Student FOREIGN KEY (StudentID) REFERENCES Users(UserID);

ALTER TABLE Grades
ADD CONSTRAINT FK_Grades_Course FOREIGN KEY (CourseID) REFERENCES Courses(CourseID);

ALTER TABLE Users 
ADD AvatarPath NVARCHAR(100), 
	QueQuan NVARCHAR(50), 
	SoDienThoai NVARCHAR(10);

ALTER TABLE Sessions
ALTER COLUMN CreatedBy INT NULL;

ALTER TABLE CourseDocuments
ALTER COLUMN UploadedBy INT NULL;