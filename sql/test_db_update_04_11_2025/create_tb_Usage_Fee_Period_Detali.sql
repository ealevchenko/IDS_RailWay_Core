USE [KRR-PA-CNT-Railway-Test]
GO

/****** Object:  Table [IDS].[Usage_Fee_Period_Detali]    Script Date: 13.11.2025 9:25:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [IDS].[Usage_Fee_Period_Detali](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usage_fee_period] [int] NULL,
	[code_stn_from] [int] NULL,
	[id_cargo_arrival] [int] NULL,
	[code_stn_to] [int] NULL,
	[id_cargo_outgoing] [int] NULL,
	[grace_time] [int] NULL,
	[id_currency] [int] NULL,
	[rate] [money] NULL,
	[end_unload] [bit] NULL,
	[start_load] [bit] NULL,
 CONSTRAINT [PK_Usage_Fee_Period_Detali] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali]  WITH CHECK ADD  CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_Cargo] FOREIGN KEY([id_cargo_arrival])
REFERENCES [IDS].[Directory_Cargo] ([id])
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali] CHECK CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_Cargo]
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali]  WITH CHECK ADD  CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_Cargo1] FOREIGN KEY([id_cargo_outgoing])
REFERENCES [IDS].[Directory_Cargo] ([id])
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali] CHECK CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_Cargo1]
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali]  WITH CHECK ADD  CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_ExternalStation] FOREIGN KEY([code_stn_from])
REFERENCES [IDS].[Directory_ExternalStation] ([code])
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali] CHECK CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_ExternalStation]
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali]  WITH CHECK ADD  CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_ExternalStation1] FOREIGN KEY([code_stn_to])
REFERENCES [IDS].[Directory_ExternalStation] ([code])
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali] CHECK CONSTRAINT [FK_Usage_Fee_Period_Detali_Directory_ExternalStation1]
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali]  WITH CHECK ADD  CONSTRAINT [FK_Usage_Fee_Period_Detali_Usage_Fee_Period] FOREIGN KEY([id_usage_fee_period])
REFERENCES [IDS].[Usage_Fee_Period] ([id])
GO

ALTER TABLE [IDS].[Usage_Fee_Period_Detali] CHECK CONSTRAINT [FK_Usage_Fee_Period_Detali_Usage_Fee_Period]
GO


