
create table Students(
personid int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
firstName varchar(50) NOT NULL,
middleInitial varchar(5),
lastName varchar(50) NOT NULL,
section varchar(10) NOT NULL,
RemainingTime varchar(50),

CONSTRAINT UQ_StudentTermEnrolled UNIQUE(studentId, section)
);

go

create table currentSession(
sessionId int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
Date varchar(15) NOT NULL,
TimeIn varchar(15) NOT NULL,
TimeOut varchar(15),
RemainingTime varchar(50),
personid int NOT NULL,

CONSTRAINT FK_currentStudent FOREIGN KEY(personid)
REFERENCES Students(personid)
);

create table SessionLogs(
logId int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
Date varchar(15) NOT NULL,
TimeIn time NOT NULL,
TimeOut time,
TimeUsed varchar(50),
personid int NOT NULL,

CONSTRAINT FK_LogStudent FOREIGN KEY(personid)
REFERENCES Students(personid)
);




--Select currentSession.Date, Students.studentId, students.firstName, students.middleInitial, Students.lastName, currentSession.TimeIn, currentSession.TimeOut
--FROM Students INNER JOIN currentSession
--ON students.personid = currentsession.personid


--SELECT cs.Date, s.studentId, s.firstName, s.middleInitial, s.lastname, s.section, cs.TimeIn, cs.timeout FROM students s JOIN currentSession cs on s.studentId = cs.studentId

--select * from SessionLogs


--SELECT sl.Date, s.studentId, s.firstName, s.middleInitial ,s.lastname, s.section, sl.TimeIn, sl.timeout FROM students s JOIN sessionLogs sl on s.studentid = sl.studentid where concat(sl.studentid, s.firstName, s.middleInitial ,s.lastname, s.section) like '%ez%'



--select * from SessionLogs




--UPDATE sessionLogs SET timeOut = '5:20:04 PM' where studentid = '21-2001265' and date = ' 06/13/2023'

update SessionLogs set TimeUsed =  concat (datediff(second,TimeIn,TimeOut)/3600,' hours ',(datediff(second,TimeIn,TimeOut)%3600)/60,' minutes') where studentID ='21-2001265'

select * from SessionLogs

UPDATE sessionLogs SET timeOut = '15:28:40.0000000', TimeUsed =  concat (datediff(second,TimeIn,TimeOut)/3600,' hours ',(datediff(second,TimeIn,TimeOut)%3600)/60,' minutes') where studentid = '21-2001265' and date = ' 06/14/2023'


SELECT * FROM SessionLogs