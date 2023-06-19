﻿create table Students(
personid int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
firstName varchar(50) NOT NULL,
middleInitial varchar(5),
lastName varchar(50) NOT NULL,
section varchar(10) NOT NULL,
RemainingTime decimal(5, 3),

CONSTRAINT UQ_StudentTermEnrolled UNIQUE(studentId, section)
);

go

create table currentSession(
sessionId int identity(1,1) PRIMARY KEY NOT NULL,
studentId varchar(13) NOT NULL,
Date varchar(15) NOT NULL,
TimeIn varchar(15) NOT NULL,
TimeOut varchar(15),
RemainingTime decimal,
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
TimeUsed decimal(5, 3),
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

SELECT * 
SELECT * FROM SessionLogs

SELECT CONCAT(FLOOR((TIME_TO_SEC(currentTimeRemaining) - (SELECT TIME_TO_SEC(TimeUsed) FROM sessionLogs 
WHERE studentid = @studentid AND date = @dateNow))/3600), ' hours ', 
FLOOR(((TIME_TO_SEC(currentTimeRemaining) - (SELECT TIME_TO_SEC(TimeUsed) FROM sessionLogs 
WHERE studentid = @studentid AND date = @dateNow))%3600)/60), ' minutes') AS newTimeRemaining;

select * from sessionlogs



SELECT CONCAT(FLOOR(DATEDIFF(second, TimeIn, TimeOut)/3600), ' hours ', 
FLOOR((DATEDIFF(second, TimeIn, TimeOut)%3600)/60), ' minutes') AS TimeUsed
FROM sessionLogs
WHERE studentId = '21-2001265' AND Date = ' 06/15/2023';



select * from SessionLogs


SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'sessionlogs' 
  AND COLUMN_NAME IN ('TimeIn', 'TimeOut', 'TimeUsed')
  



UPDATE sessionlogs 
SET TimeUsed = DATEDIFF(second, CAST(TimeIn AS datetime), CAST(TimeOut AS datetime)) / 3600.0
WHERE studentid = '21-2002433' AND date = ' 06/19/2023'





--GETS THE TIMEUSED AFTER LOGOUT
UPDATE SessionLogs SET TimeUsed = (DATEDIFF(second, TimeIn, TimeOut) / 3600.0) WHERE studentID = '21-2002433';


--MINUS THE REMAINING TIME USING THE TIMEUSED FROM A LOG
UPDATE Students
SET remainingTime -= (SELECT CONVERT(DECIMAL(16, 6), SUM(TimeUsed))
FROM SessionLogs WHERE studentID = Students.studentID and Date = ' 06/19/2023')
WHERE studentID = '21-2002433';




--DATA CHANGES FOR TRIALS
UPDATE Students
SET remainingTime -= (SELECT CONVERT(DECIMAL(16, 6), SUM(TimeUsed))
FROM SessionLogs WHERE logid = 17)
WHERE studentID = '21-2001265';

UPDATE SessionLogs SET TimeUsed = (DATEDIFF(second, TimeIn, TimeOut) / 3600.0) WHERE logid = 17;




SELECT studentID, SUM(TimeUsed) AS TotalTimeUsed FROM SessionLogs WHERE studentID = '21-2001265' GROUP BY studentID HAVING SUM(TimeUsed) >= 1


select * from SessionLogs
SELECT COUNT(*) as countOfRows FROM SessionLogs;


drop table SessionLogs
drop table currentSession
drop table Students

