USE [KRR-PA-CNT-Railway]
GO
/****** Object:  Table [IDS].[Directory_WagonOperationsLoadingStatus]    Script Date: 11.11.2024 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

drop table [IDS].[Directory_WagonOperationsLoadingStatus]
GO

CREATE TABLE [IDS].[Directory_WagonOperationsLoadingStatus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_wagon_operations] [int] NOT NULL,
	[id_wagon_loading_status] [int] NOT NULL,
 CONSTRAINT [PK_Directory_WagonOperationsLoadingStatus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ON 

INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (1, 13, 0)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (2, 13, 1)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (3, 13, 3)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (4, 13, 4)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (5, 13, 5)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (6, 13, 6)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (7, 13, 7)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (8, 14, 0)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (9, 14, 2)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (10, 14, 3)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (11, 14, 4)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (12, 14, 5)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (13, 14, 7)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (14, 15, 6)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (15, 15, 0)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (16, 16, 2)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (17, 16, 0)
INSERT [IDS].[Directory_WagonOperationsLoadingStatus] ([id], [id_wagon_operations], [id_wagon_loading_status]) VALUES (18, 17, 8)
SET IDENTITY_INSERT [IDS].[Directory_WagonOperationsLoadingStatus] OFF
GO
ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus]  WITH CHECK ADD  CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonLoadingStatus] FOREIGN KEY([id_wagon_loading_status])
REFERENCES [IDS].[Directory_WagonLoadingStatus] ([id])
GO
ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus] CHECK CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonLoadingStatus]
GO
ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus]  WITH CHECK ADD  CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonOperations] FOREIGN KEY([id_wagon_operations])
REFERENCES [IDS].[Directory_WagonOperations] ([id])
GO
ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus] CHECK CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonOperations]
GO
