USE [KRR-PA-CNT-Railway-Archive]--[KRR-PA-CNT-Railway]
GO

ALTER TABLE [IDS].[WagonInternalOperation] DROP CONSTRAINT [FK_WagonInternalOperation_Directory_WagonLoadingStatus]
GO

ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus] DROP CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonLoadingStatus]
GO


drop table [IDS].[Directory_WagonLoadingStatus]
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IDS].[Directory_WagonLoadingStatus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loading_status_ru] [nvarchar](30) NOT NULL,
	[loading_status_en] [nvarchar](30) NOT NULL,
	[create] [datetime] NOT NULL,
	[create_user] [nvarchar](50) NOT NULL,
	[change] [datetime] NULL,
	[change_user] [nvarchar](50) NULL,
 CONSTRAINT [PK_Directory_WagonLoadingStatus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [IDS].[Directory_WagonLoadingStatus] ON 

INSERT [IDS].[Directory_WagonLoadingStatus] ([id], [loading_status_ru], [loading_status_en], [create], [create_user], [change], [change_user]) VALUES (0, N'Порожний', N'Empty', CAST(N'2020-09-25T16:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonLoadingStatus] ([id], [loading_status_ru], [loading_status_en], [create], [create_user], [change], [change_user]) VALUES (1, N'Груженый ПРИБ', N'Loaded ARR', CAST(N'2020-09-25T16:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonLoadingStatus] ([id], [loading_status_ru], [loading_status_en], [create], [create_user], [change], [change_user]) VALUES (2, N'Груженый В/З', N'Loaded I/P', CAST(N'2020-09-25T16:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonLoadingStatus] ([id], [loading_status_ru], [loading_status_en], [create], [create_user], [change], [change_user]) VALUES (3, N'Грязный', N'Dirty', CAST(N'2020-09-25T16:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonLoadingStatus] ([id], [loading_status_ru], [loading_status_en], [create], [create_user], [change], [change_user]) VALUES (4, N'Мерзлый', N'Frozen', CAST(N'2024-11-06T08:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonLoadingStatus] ([id], [loading_status_ru], [loading_status_en], [create], [create_user], [change], [change_user]) VALUES (5, N'Тех. неисправность', N'Tech. malfunction', CAST(N'2024-11-06T08:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonLoadingStatus] ([id], [loading_status_ru], [loading_status_en], [create], [create_user], [change], [change_user]) VALUES (6, N'Груженый УЗ', N'Loaded UZ', CAST(N'2024-11-06T08:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)

SET IDENTITY_INSERT [IDS].[Directory_WagonLoadingStatus] OFF
GO


ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus]  WITH CHECK ADD  CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonLoadingStatus] FOREIGN KEY([id_wagon_loading_status])
REFERENCES [IDS].[Directory_WagonLoadingStatus] ([id])
GO

ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus] CHECK CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonLoadingStatus]
GO

ALTER TABLE [IDS].[WagonInternalOperation]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalOperation_Directory_WagonLoadingStatus] FOREIGN KEY([id_loading_status])
REFERENCES [IDS].[Directory_WagonLoadingStatus] ([id])
GO

ALTER TABLE [IDS].[WagonInternalOperation] CHECK CONSTRAINT [FK_WagonInternalOperation_Directory_WagonLoadingStatus]
GO


