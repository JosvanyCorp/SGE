﻿CREATE TABLE [dbo].[TBL_STUDENTS]
(
	STD_GUID_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	STD_FIRST_NAME VARCHAR(50) NOT NULL,
	STD_SECOND_NAME VARCHAR(50) NULL,
	STD_SURNAME VARCHAR(50) NOT NULL,
	STD_SECOND_SURNAME VARCHAR(50) NULL,
	STD_EMAIL VARCHAR(50) NOT NULL,
	GEN_GUID_ID UNIQUEIDENTIFIER NOT NULL,
	ADD_GUID_ID UNIQUEIDENTIFIER NULL,
	STD_PHOTO VARBINARY(MAX) NOT NULL,
	STD_PHONE VARCHAR(8) NOT NULL,
	STD_BIRTHDATE DATETIME NOT NULL,
	STD_STATUS BIT NOT NULL,

	CONSTRAINT [FK_TBL_GENDER_STUDENT] FOREIGN KEY ([GEN_GUID_ID]) REFERENCES [dbo].[TBL_GENDER] ([GEN_GUID_ID]),
    CONSTRAINT [FK_TBL_STUDENT_TBL_ADDRESS] FOREIGN KEY ([ADD_GUID_ID]) REFERENCES [dbo].[TBL_ADDRESS] ([ADD_GUID_ID]),
)
