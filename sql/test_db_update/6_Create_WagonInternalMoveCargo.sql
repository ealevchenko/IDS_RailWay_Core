USE [KRR-PA-CNT-Railway]
go

/****** Object:  Table [IDS].[WagonInternalMoveCargo]    Script Date: 24.02.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [IDS].[WagonInternalMoveCargo](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_wagon_internal_routes] [bigint] NOT NULL,
	[internal_doc_num] [nvarchar](20) NULL,
	[id_weighing_num] [int] NULL,
	[doc_received] [datetime] NULL,
	[id_cargo] [int] NULL,
	[id_internal_cargo] [int] NULL,
	[empty] [bit] NULL,
	[vesg] [int] NULL,
	[id_station_from_amkr] [int] NULL,
	[id_division_from] [int] NULL,
	[id_wim_load] [bigint] NULL,
	[id_wim_redirection] [bigint] NULL,
	[code_external_station] [int] NULL,
	[id_station_on_amkr] [int] NULL,
	[id_division_on] [int] NULL,
	[create] [datetime] NOT NULL,
	[create_user] [nvarchar](50) NOT NULL,
	[change] [datetime] NULL,
	[change_user] [nvarchar](50) NULL,
	[close] [datetime] NULL,
	[close_user] [nvarchar](50) NULL,
	[parent_id] [bigint] NULL,
 CONSTRAINT [PK_WagonInternalMoveCargo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Cargo] FOREIGN KEY([id_cargo])
REFERENCES [IDS].[Directory_Cargo] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Cargo]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Divisions] FOREIGN KEY([id_division_from])
REFERENCES [IDS].[Directory_Divisions] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Divisions]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Divisions1] FOREIGN KEY([id_division_on])
REFERENCES [IDS].[Directory_Divisions] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Divisions1]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_Directory_ExternalStation] FOREIGN KEY([code_external_station])
REFERENCES [IDS].[Directory_ExternalStation] ([code])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_Directory_ExternalStation]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_Directory_InternalCargo1] FOREIGN KEY([id_internal_cargo])
REFERENCES [IDS].[Directory_InternalCargo] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_Directory_InternalCargo1]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Station] FOREIGN KEY([id_station_from_amkr])
REFERENCES [IDS].[Directory_Station] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Station]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Station1] FOREIGN KEY([id_station_on_amkr])
REFERENCES [IDS].[Directory_Station] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_Directory_Station1]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalMoveCargo] FOREIGN KEY([parent_id])
REFERENCES [IDS].[WagonInternalMoveCargo] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalMoveCargo]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalMovement] FOREIGN KEY([id_wim_load])
REFERENCES [IDS].[WagonInternalMovement] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalMovement]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalMovement1] FOREIGN KEY([id_wim_redirection])
REFERENCES [IDS].[WagonInternalMovement] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalMovement1]
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalRoutes] FOREIGN KEY([id_wagon_internal_routes])
REFERENCES [IDS].[WagonInternalRoutes] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMoveCargo] CHECK CONSTRAINT [FK_WagonInternalMoveCargo_WagonInternalRoutes]
GO


