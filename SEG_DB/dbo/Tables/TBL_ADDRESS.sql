﻿CREATE TABLE [dbo].[TBL_ADDRESS] (
    [ADD_GUID_ID] UNIQUEIDENTIFIER NOT NULL,
    [MUN_GUID_ID] UNIQUEIDENTIFIER NOT NULL,
    [ADD_NAME]    VARCHAR (300)    NULL,
    CONSTRAINT [PK__TBL_ADDR__5115928FDC2D4A5B] PRIMARY KEY CLUSTERED ([ADD_GUID_ID] ASC),
    CONSTRAINT [FK_TBL_MUNICIPALITY] FOREIGN KEY ([MUN_GUID_ID]) REFERENCES [dbo].[TBL_MUNICIPALITY] ([MUN_GUID_ID])
);

