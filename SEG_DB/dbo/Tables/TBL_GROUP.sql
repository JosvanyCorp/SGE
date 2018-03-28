﻿CREATE TABLE [dbo].[TBL_GROUP]
(
	GRO_GUID_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	SEC_GUID_ID UNIQUEIDENTIFIER NOT NULL,
	PER_GUID_ID UNIQUEIDENTIFIER NOT NULL,
	MOD_GUID_ID UNIQUEIDENTIFIER NOT NULL,
	TYP_TUR_GUID_ID UNIQUEIDENTIFIER NOT NULL,
	GRO_ENABLED_DATE DATETIME NOT NULL,
	GRO_DISABLED_DATE DATETIME NOT NULL,

	CONSTRAINT [FK_TBL_SECTION_TBL_GROUP] FOREIGN KEY ([SEC_GUID_ID]) REFERENCES [dbo].[TBL_SECTION]([SEC_GUID_ID]),
	CONSTRAINT [FK_TBL_PERSONS_TBL_GROUP] FOREIGN KEY ([PER_GUID_ID]) REFERENCES [dbo].[TBL_PERSONS]([PER_GUID_ID]),
	CONSTRAINT [FK_TBL_MOD_TBL_GROUP] FOREIGN KEY ([MOD_GUID_ID]) REFERENCES [dbo].[TBL_MODALITY]([MOD_GUID_ID]),
	CONSTRAINT [FK_TBL_TYPE_TURN_TBL_GROUP] FOREIGN KEY([TYP_TUR_GUID_ID]) REFERENCES [dbo].[TBL_TYPE_TURN]([TYP_TUR_GUID_ID])
)