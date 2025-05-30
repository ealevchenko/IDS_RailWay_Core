USE [KRR-PA-CNT-Railway-Archive]
GO
/****** Object:  UserDefinedFunction [IDS].[get_view_operators_send_of_id_station]    Script Date: 04.02.2025 10:46:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER FUNCTION [IDS].[get_view_operators_send_of_id_station]
 (
   @id_station int
 )
	RETURNS 
	@operators_of_outer_ways TABLE(
	[id] [int] NULL,
	[name_outer_way_ru] [nvarchar](150) NULL,
	[name_outer_way_en] [nvarchar](150) NULL,
	[id_operator] [int] NULL,
	[operator_abbr_ru] [nvarchar](20) NULL,
	[operator_abbr_en] [nvarchar](20) NULL,
	[operator_color] [nvarchar](10) NULL,
	[count_operators] [int] NULL
	)
	AS
	BEGIN
	insert into @operators_of_outer_ways
	SELECT 
		out_way.id
		,out_way.[name_outer_way_ru]
		,out_way.[name_outer_way_en]
		,dir_operator.[id] as id_operator 
		,dir_operator.[abbr_ru] as operator_abbr_ru
		,dir_operator.[abbr_en] as operator_abbr_en
		,dir_operator.color as operator_color
		,Count (wim.id) as count_operators
	FROM [IDS].[WagonInternalMovement] as wim
  		--> Текущее внетренее перемещение
		INNER JOIN IDS.WagonInternalRoutes as wir ON wim.id_wagon_internal_routes = wir.id
		--> Справочник аренд
		Left JOIN IDS.Directory_WagonsRent as dir_rent ON dir_rent.id = (SELECT top(1) [id] FROM [IDS].[Directory_WagonsRent] where [num] = wir.num and rent_end is null order by [id] desc)	
		--> Справочник группы Операторов вагона
		Left JOIN IDS.Directory_OperatorsWagonsGroup as dir_operator_group ON dir_rent.id_operator =  dir_operator_group.id_operator
		--> Справочник Оператор вагона
		Left JOIN IDS.Directory_OperatorsWagons as dir_operator ON dir_rent.id_operator =  dir_operator.id
		--> Справочник перегонов
		Left JOIN [IDS].[Directory_OuterWays] out_way ON wim.id_outer_way = out_way.id
	WHERE wim.id_station = @id_station and wim.[way_start] is not null and wim.[way_end] is not null and wim.[outer_way_start] is not null and wim.[outer_way_end] is null
	group by 
		out_way.id, 
		out_way.name_outer_way_ru,
		out_way.name_outer_way_en,
		dir_operator.[id], 
		dir_operator.[abbr_ru], 
		dir_operator.[abbr_en],
		dir_operator.color
	order by out_way.id, dir_operator.[id]
	RETURN
 END

