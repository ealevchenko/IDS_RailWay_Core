USE [KRR-PA-CNT-Railway]
GO
/****** Object:  Table [IDS].[Directory_GroupWagonOperations]    Script Date: 16.10.2024 10:31:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [IDS].[WagonInternalOperation] DROP CONSTRAINT [FK_WagonInternalOperation_Directory_WagonOperations]
GO

ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus] DROP CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonOperations]
GO

DROP TABLE [IDS].[Directory_WagonOperations]
GO
DROP TABLE [IDS].[Directory_GroupWagonOperations]
GO
DROP TABLE [IDS].[Directory_TypeDownTimeWagonOperations]
GO

CREATE TABLE [IDS].[Directory_GroupWagonOperations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[group_name_ru] [nvarchar](50) NOT NULL,
	[group_name_en] [nvarchar](50) NOT NULL,
	[create] [datetime] NOT NULL,
	[create_user] [nvarchar](50) NOT NULL,
	[change] [datetime] NULL,
	[change_user] [nvarchar](50) NULL,
 CONSTRAINT [PK_Directory_GroupWagonOperations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IDS].[Directory_TypeDownTimeWagonOperations]    Script Date: 16.10.2024 10:31:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IDS].[Directory_TypeDownTimeWagonOperations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type_downtime_ru] [nvarchar](20) NOT NULL,
	[type_downtime_en] [nvarchar](20) NOT NULL,
	[create] [datetime] NOT NULL,
	[create_user] [nvarchar](50) NOT NULL,
	[change] [datetime] NULL,
	[change_user] [nvarchar](50) NULL,
 CONSTRAINT [PK_Directory_TypeDownTimeWagonOperations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IDS].[Directory_WagonOperations]    Script Date: 16.10.2024 10:31:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IDS].[Directory_WagonOperations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[operation_name_ru] [nvarchar](50) NOT NULL,
	[operation_name_en] [nvarchar](50) NOT NULL,
	[operation_abbr_ru] [nvarchar](20) NOT NULL,
	[operation_abbr_en] [nvarchar](20) NOT NULL,
	[id_group] [int] NULL,
	[id_type_down_time] [int] NULL,
	[busy] [bit] NOT NULL,
	[create] [datetime] NOT NULL,
	[create_user] [nvarchar](50) NOT NULL,
	[change] [datetime] NULL,
	[change_user] [nvarchar](50) NULL,
 CONSTRAINT [PK_Directory_WagonOperations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [IDS].[Directory_GroupWagonOperations] ON 

INSERT [IDS].[Directory_GroupWagonOperations] ([id], [group_name_ru], [group_name_en], [create], [create_user], [change], [change_user]) VALUES (1, N'Транспортные операции', N'Transport operations', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_GroupWagonOperations] ([id], [group_name_ru], [group_name_en], [create], [create_user], [change], [change_user]) VALUES (2, N'Технологические операции', N'Technological operations', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_GroupWagonOperations] ([id], [group_name_ru], [group_name_en], [create], [create_user], [change], [change_user]) VALUES (3, N'Ожидание технологических операций', N'Waiting for technological operations', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_GroupWagonOperations] ([id], [group_name_ru], [group_name_en], [create], [create_user], [change], [change_user]) VALUES (4, N'Ожидание погрузочно-разгрузочных операций', N'Waiting for loading and unloading operations', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_GroupWagonOperations] ([id], [group_name_ru], [group_name_en], [create], [create_user], [change], [change_user]) VALUES (5, N'Погрузочно-разгрузочные операции', N'Loading and unloading operations', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
SET IDENTITY_INSERT [IDS].[Directory_GroupWagonOperations] OFF
GO
SET IDENTITY_INSERT [IDS].[Directory_TypeDownTimeWagonOperations] ON 

INSERT [IDS].[Directory_TypeDownTimeWagonOperations] ([id], [type_downtime_ru], [type_downtime_en], [create], [create_user], [change], [change_user]) VALUES (1, N'Производственный', N'Production', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_TypeDownTimeWagonOperations] ([id], [type_downtime_ru], [type_downtime_en], [create], [create_user], [change], [change_user]) VALUES (2, N'Непроизводственный', N'Non-production', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
SET IDENTITY_INSERT [IDS].[Directory_TypeDownTimeWagonOperations] OFF
GO
SET IDENTITY_INSERT [IDS].[Directory_WagonOperations] ON 

INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (1, N'Прибытие с УЗ', N'Arrival from UZ', N'Прибытие с УЗ', N'Arrival from UZ', 1, 1, 0, CAST(N'2020-09-24T16:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (2, N'Отправление на УЗ', N'Departure to UZ', N'Отправление на УЗ', N'Departure to UZ', NULL, NULL, 0, CAST(N'2020-09-28T16:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (3, N'Дислокация', N'Dislocation', N'Дислокация', N'Dislocation', 1, 1, 0, CAST(N'2020-10-12T09:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (4, N'Роспуск', N'Dissolution', N'Роспуск', N'Dissolution', 1, 1, 0, CAST(N'2020-10-23T09:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (5, N'Отправление', N'Sending', N'Отправление', N'Sending', 1, 1, 0, CAST(N'2020-10-29T11:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (6, N'Прибытие', N'Arrival', N'Прибытие', N'Arrival', 1, 1, 0, CAST(N'2020-11-02T11:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (7, N'Транспортировка', N'Transportation', N'Транспортировка', N'Transportation', NULL, NULL, 0, CAST(N'2020-11-08T18:30:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (8, N'Ручная расстановка', N'Manual placement', N'Ручная расстановка', N'Manual placement', NULL, NULL, 0, CAST(N'2020-12-11T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (9, N'Предъявление на УЗ', N'Presentation for UZ', N'Предъявление на УЗ', N'Presentation for UZ', 1, 1, 0, CAST(N'2021-01-25T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (10, N'Возврат на УЗ', N'Return', N'Возврат на УЗ', N'Return', 1, 1, 0, CAST(N'2021-03-05T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (11, N'Возврат с перегона', N'Return outer way', N'Возврат с перегона', N'Return outer way', NULL, NULL, 0, CAST(N'2021-10-19T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (12, N'Отмена', N'Cancel', N'Отмена', N'Cancel', NULL, NULL, 0, CAST(N'2021-10-19T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (13, N'Выгрузка с УЗ', N'Unloading from UZ', N'Выгрузка с УЗ', N'Unloading from UZ', 5, 1, 0, CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (14, N'Выгрузка В/З', N'Unloading inside the factory', N'Выгрузка В/З', N'Unloading I/F', 5, 1, 0, CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (15, N'Погрузка на УЗ', N'Loading on UZ', N'Погрузка УЗ', N'Loading UZ', 5, 1, 0, CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (16, N'Погрузка В/З', N'Loading inside the factory', N'Погрузка В/З', N'Loading I/F', 5, 1, 0, CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (17, N'Очистка', N'Cleaning', N'Очистка', N'Cleaning', 2, 2, 0, CAST(N'2025-02-05T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
INSERT [IDS].[Directory_WagonOperations] ([id], [operation_name_ru], [operation_name_en], [operation_abbr_ru], [operation_abbr_en], [id_group], [id_type_down_time], [busy], [create], [create_user], [change], [change_user]) VALUES (18, N'Обработка', N'Processing', N'Обработка', N'Processing', 2, 1, 0, CAST(N'2025-02-05T00:00:00.000' AS DateTime), N'EUROPE\ealevchenko', NULL, NULL)
SET IDENTITY_INSERT [IDS].[Directory_WagonOperations] OFF
GO
ALTER TABLE [IDS].[Directory_WagonOperations]  WITH CHECK ADD  CONSTRAINT [FK_Directory_WagonOperations_Directory_GroupWagonOperations] FOREIGN KEY([id_group])
REFERENCES [IDS].[Directory_GroupWagonOperations] ([id])
GO
ALTER TABLE [IDS].[Directory_WagonOperations] CHECK CONSTRAINT [FK_Directory_WagonOperations_Directory_GroupWagonOperations]
GO
ALTER TABLE [IDS].[Directory_WagonOperations]  WITH CHECK ADD  CONSTRAINT [FK_Directory_WagonOperations_Directory_TypeDownTimeWagonOperations] FOREIGN KEY([id_type_down_time])
REFERENCES [IDS].[Directory_TypeDownTimeWagonOperations] ([id])
GO
ALTER TABLE [IDS].[Directory_WagonOperations] CHECK CONSTRAINT [FK_Directory_WagonOperations_Directory_TypeDownTimeWagonOperations]
GO

--ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus]  WITH CHECK ADD  CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonOperations] FOREIGN KEY([id_wagon_operations])
--REFERENCES [IDS].[Directory_WagonOperations] ([id])
--GO

--ALTER TABLE [IDS].[Directory_WagonOperationsLoadingStatus] CHECK CONSTRAINT [FK_Directory_WagonOperationsLoadingStatus_Directory_WagonOperations]
--GO

ALTER TABLE [IDS].[WagonInternalOperation]  WITH CHECK ADD  CONSTRAINT [FK_WagonInternalOperation_Directory_WagonOperations] FOREIGN KEY([id_operation])
REFERENCES [IDS].[Directory_WagonOperations] ([id])
GO

ALTER TABLE [IDS].[WagonInternalOperation] CHECK CONSTRAINT [FK_WagonInternalOperation_Directory_WagonOperations]
GO