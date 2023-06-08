

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
);

create table SessionLogs(
logId int identity(1,1) PRIMARY KEY NOT NULL,
Date varchar(15) NOT NULL,
TimeIn varchar(15) NOT NULL,
TimeOut varchar(15),
RemainingTime varchar(15),
);


select * from Students
select * from currentSession
