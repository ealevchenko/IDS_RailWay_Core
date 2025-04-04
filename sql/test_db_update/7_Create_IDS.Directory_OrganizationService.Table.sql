USE [KRR-PA-CNT-Railway]
GO

ALTER TABLE [IDS].[WagonInternalOperation] DROP CONSTRAINT [FK_WagonInternalOperation_Directory_OrganizationService]
GO

/****** Object:  Table [IDS].[Directory_OrganizationService]    Script Date: 06.02.2025 8:54:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[IDS].[Directory_OrganizationService]') AND type in (N'U'))
DROP TABLE [IDS].[Directory_OrganizationService]
GO
/****** Object:  Table [IDS].[Directory_OrganizationService]    Script Date: 06.02.2025 8:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IDS].[Directory_OrganizationService](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[organization_service_ru] [nvarchar](50) NOT NULL,
	[organization_service_en] [nvarchar](50) NOT NULL,
	[code_sap] [nvarchar](20) NULL,
	[create] [datetime] NOT NULL,
	[create_user] [nvarchar](50) NOT NULL,
	[change] [datetime] NULL,
	[change_user] [nvarchar](50) NULL,
	[delete] [datetime] NULL,
	[delete_user] [nvarchar](50) NULL,
 CONSTRAINT [PK_Directory_OrganizationService] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [IDS].[Directory_OrganizationService] ON 
INSERT [IDS].[Directory_OrganizationService] ([id], [organization_service_ru], [organization_service_en], [code_sap], [create], [create_user], [change], [change_user], [delete], [delete_user]) VALUES (0, N'АМКР', N'AMKR', NULL, CAST(N'2025-02-06T00:00:00.000' AS DateTime), N'europe\ealevchenko', NULL, NULL, NULL, NULL)
INSERT [IDS].[Directory_OrganizationService] ([id], [organization_service_ru], [organization_service_en], [code_sap], [create], [create_user], [change], [change_user], [delete], [delete_user]) VALUES (1, N'Макстайп', N'Макстайп', NULL, CAST(N'2025-02-06T00:00:00.000' AS DateTime), N'europe\ealevchenko', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [IDS].[Directory_OrganizationService] OFF
GO

ALTER TABLE [IDS].[WagonInternalOperation]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalOperation_Directory_OrganizationService] FOREIGN KEY([id_organization_service])
REFERENCES [IDS].[Directory_OrganizationService] ([id])
GO

ALTER TABLE [IDS].[WagonInternalOperation] CHECK CONSTRAINT [FK_WagonInternalOperation_Directory_OrganizationService]
GO
