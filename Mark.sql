CREATE TABLE SemestersAdded(
	semester VARCHAR(33) PRIMARY KEY NOT NULL,
	startOfSem DATE,
	endOfSem DATE
);
GO


SELECT startOfSem FROM SemestersAdded WHERE semester = 'SECOND SEM 2022 - 2023'
GO

DROP TABLE SemestersAdded 
GO


INSERT INTO SemestersAdded(semester, startOfSem, endOfSem)
VALUES ('SECOND SEM 2022 - 2023', '01/16/2023', '06/24/2023')
GO