﻿CREATE TABLE [dbo].[TBL_RELATION_SHIP_PLANS_SUBJETS]
(
	STD_PL_GUID_ID UNIQUEIDENTIFIER NOT NULL,
	SUB_GUID_ID UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT [FK_TBL_RELATION_TBL_PLAN] FOREIGN KEY ([STD_PL_GUID_ID]) REFERENCES [dbo].[TBL_STUDY_PLANS]([STD_PL_GUID_ID]),
	CONSTRAINT [FK_TBL_RELATION_TBL_SUBJETS] FOREIGN KEY ([SUB_GUID_ID]) REFERENCES [dbo].[TBL_SUBJECTS]([SUB_GUID_ID])
)
