USE [KRR-PA-CNT-Railway-Archive]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER TABLE [IDS].[WagonInternalMovement] DROP CONSTRAINT [FK_WagonInternalMovement_WagonFiling]
GO

drop table [IDS].[WagonFiling]
go

CREATE TABLE [IDS].[WagonFiling](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[num_filing] [nvarchar](50) NOT NULL,
	[id_wio] [bigint] NOT NULL,
	[id_devision] [int] NOT NULL,
	[note] [nvarchar](250) NULL,
	[start_filing] [datetime] NOT NULL,
	[end_filing] [datetime] NULL,
	[create] [datetime] NOT NULL,
	[create_user] [nvarchar](50) NOT NULL,
	[change] [datetime] NULL,
	[change_user] [nvarchar](50) NULL,
	[close] [datetime] NULL,
	[close_user] [nvarchar](50) NULL,
 CONSTRAINT [PK_WagonFiling] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [IDS].[WagonFiling]  WITH CHECK ADD  CONSTRAINT [FK_WagonFiling_WagonInternalOperation] FOREIGN KEY([id_wio])
REFERENCES [IDS].[WagonInternalOperation] ([id])
GO

ALTER TABLE [IDS].[WagonFiling] CHECK CONSTRAINT [FK_WagonFiling_WagonInternalOperation]
GO


ALTER TABLE [IDS].[WagonInternalMovement]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalMovement_WagonFiling] FOREIGN KEY([id_filing])
REFERENCES [IDS].[WagonFiling] ([id])
GO

ALTER TABLE [IDS].[WagonInternalMovement] CHECK CONSTRAINT [FK_WagonInternalMovement_WagonFiling]
GO


