using SitInMonitoring

create table Students(
personid int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
firstName varchar(50) NOT NULL,
middleInitial varchar(5),
lastName varchar(50) NOT NULL,
section varchar(10) NOT NULL,
remainingTime int NOT NULL,

CONSTRAINT UQ_StudentTermEnrolled UNIQUE(studentId, section)
);

go

create table currentSession(
sessionId int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
Date varchar(15) NOT NULL,
TimeIn varchar(15) NOT NULL,
TimeOut varchar(15),
RemainingTime varchar(15),
personid int NOT NULL,

CONSTRAINT FK_currentStudent FOREIGN KEY(personid)
REFERENCES Students(personid)
);

create table SessionLogs(
logId int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
Date varchar(15) NOT NULL,
TimeIn varchar(15) NOT NULL,
TimeOut varchar(15),
RemainingTime varchar(15),
personid int NOT NULL,

CONSTRAINT FK_LogStudent FOREIGN KEY(personid)
REFERENCES Students(personid)
);




Select currentSession.Date, Students.studentId, students.firstName, students.middleInitial, Students.lastName, currentSession.TimeIn, currentSession.TimeOut
FROM Students INNER JOIN currentSession
ON students.personid = currentsession.personid


SELECT cs.Date, s.studentId, s.firstName, s.middleInitial, s.lastname, s.section, cs.TimeIn, cs.timeout FROM students s JOIN currentSession cs on s.studentId = cs.studentId

select * from SessionLogs