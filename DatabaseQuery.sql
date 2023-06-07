CREATE DATABASE monitoring

USE monitoring CREATE TABLE sit_in_table(
date_time DATE, 
student_id VARCHAR(13) UNIQUE, 
student_full_name VARCHAR(22),
time_in TIME,
time_out TIME,
PRIMARY KEY (student_id));

SELECT * FROM sit_in_table
DROP TABLE sit_in_table


USE monitoring CREATE TABLE records_table(
date_time DATE, 
student_id VARCHAR(13) UNIQUE, 
student_full_name VARCHAR(22),
time_in TIME,
time_out TIME,
PRIMARY KEY (student_id));
