USE [KRR-PA-CNT-Railway]
GO

/****** Object:  UserDefinedFunction [IDS].[get_view_next_filing_of_id_filing]    Script Date: 16.04.2026 9:39:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [IDS].[get_view_next_filing_of_id_filing]
 (
	@id_filing bigint
 )
		RETURNS 
@view_filing TABLE (
	[id_wim] [bigint] NOT NULL,
	[id_wir] [bigint] NULL,
	[num] [int] NULL,
	[id_filing] [bigint] NULL,
	[num_filing] [nvarchar](50) NULL,
	[type_filing] [int] NULL,
	[id_division_filing] [int] NULL,
	[filing_name_division_ru] [nvarchar](250) NULL,
	[filing_name_division_en] [nvarchar](250) NULL,
	[filing_division_abbr_ru] [nvarchar](50) NULL,
	[filing_division_abbr_en] [nvarchar](50) NULL,
	[filing_note] [nvarchar](250) NULL,
	[start_filing] [datetime] NULL,
	[end_filing] [datetime] NULL,
	[filing_id_station_from_amkr] [int] NULL,
	[filing_station_name_ru] [nvarchar](50) NULL,
	[filing_station_name_en] [nvarchar](50) NULL,
	[filing_station_abbr_ru] [nvarchar](50) NULL,
	[filing_station_abbr_en] [nvarchar](50) NULL,
	[id_wim_next] [bigint] NULL,
	[id_filing_next] [bigint] NULL,
	[num_filing_next] [nvarchar](50) NULL,
	[type_filing_next] [int] NULL,
	[id_division_filing_next] [int] NULL,
	[filing_next_name_division_ru] [nvarchar](250) NULL,
	[filing_next_name_division_en] [nvarchar](250) NULL,
	[filing_next_division_abbr_ru] [nvarchar](50) NULL,
	[filing_next_division_abbr_en] [nvarchar](50) NULL,
	[filing_next_note] [nvarchar](250) NULL,
	[filing_next_start_filing] [datetime] NULL,
	[filing_next_end_filing] [datetime] NULL,
	[filing_next_id_station_from_amkr] [int] NULL,
	[filing_next_station_name_ru] [nvarchar](50) NULL,
	[filing_next_station_name_en] [nvarchar](50) NULL,
	[filing_next_station_abbr_ru] [nvarchar](50) NULL,
	[filing_next_station_abbr_en] [nvarchar](50) NULL,
	[wio_old_id_operation] [int] NULL,
	[wio_old_loading_status] [int] NULL
	)
    AS
    BEGIN

	insert @view_filing
	SELECT wim.[id] as id_wim
	  ,wir.id as id_wir
	  ,wir.num
	  ,wf.[id] as id_filing
      ,wf.[num_filing]
      ,wf.[type_filing]
      ,wf.[id_division] as id_division_filing
	  ,dir_div.name_division_ru as filing_name_division_ru
	  ,dir_div.name_division_en as filing_name_division_en
	  ,dir_div.division_abbr_ru as filing_division_abbr_ru
	  ,dir_div.division_abbr_en as filing_division_abbr_en
      ,wf.[note] as filing_note
      ,wf.[start_filing]
      ,wf.[end_filing]
      ,wimc.[id_station_from_amkr] as filing_id_station_from_amkr
	  ,dir_station.station_name_ru as filing_station_name_ru
	  ,dir_station.station_name_en as filing_station_name_en
	  ,dir_station.station_abbr_ru as filing_station_abbr_ru
      ,dir_station.station_abbr_en as filing_station_abbr_en
	  ,wim_next_wf.[id] as id_wim_next
	  ,wf_next.[id] as id_filing_next
      ,wf_next.[num_filing] as num_filing_next
      ,wf_next.[type_filing] as type_filing_next
      ,wf_next.[id_division] as id_division_filing_next
	  ,dir_div_next.name_division_ru as filing_next_name_division_ru
	  ,dir_div_next.name_division_en as filing_next_name_division_en
	  ,dir_div_next.division_abbr_ru as filing_next_division_abbr_ru
	  ,dir_div_next.division_abbr_en as filing_next_division_abbr_en
      ,wf_next.[note] as filing_next_note
      ,wf_next.[start_filing] as filing_next_start_filing
      ,wf_next.[end_filing] as filing_next_end_filing
	  ,wimc.[id_station_from_amkr] as filing_next_id_station_from_amkr
	  ,dir_station_next.station_name_ru as filing_next_station_name_ru
	  ,dir_station_next.station_name_en as filing_next_station_name_en
	  ,dir_station_next.station_abbr_ru as filing_next_station_abbr_ru
      ,dir_station_next.station_abbr_en as filing_next_station_abbr_en
	  ,wio_old.id_operation as wio_old_id_operation
	  ,wio_old.id_loading_status as wio_old_loading_status
  FROM [IDS].[WagonInternalMovement] as wim
  Left JOIN [IDS].[WagonInternalRoutes] as wir ON wir.id = wim.id_wagon_internal_routes
  Left JOIN [IDS].[WagonFiling] as wf ON wf.id = wim.id_filing
  Left JOIN IDS.WagonInternalMovement as wim_next_wf ON wim_next_wf.id = (SELECT top(1) [id] FROM [IDS].[WagonInternalMovement] where [id_wagon_internal_routes] = wim.id_wagon_internal_routes and [id_filing] is not null and id > wim.id order by [id_filing] )
  Left JOIN [IDS].[WagonFiling] as wf_next ON wf_next.id = wim_next_wf.id_filing
  Left JOIN IDS.WagonInternalMovement as wim_old_wf ON wim_old_wf.id = (SELECT top(1) [id] FROM [IDS].[WagonInternalMovement] where [id_wagon_internal_routes] = wim.id_wagon_internal_routes and [id_filing] is not null and id < wim.id order by [id_filing] desc)
  --Left JOIN [IDS].[WagonFiling] as wf_old ON wf_old.id = wim_old_wf.id_filing
  Left JOIN [IDS].[WagonInternalOperation] as wio_old ON wio_old.id = wim_old_wf.id_wio
  Left JOIN [IDS].[Directory_Divisions] as dir_div ON dir_div.id = wf.id_division
  Left JOIN [IDS].[Directory_Divisions] as dir_div_next ON dir_div_next.id = wf_next.id_division
  Left JOIN [IDS].[WagonInternalMoveCargo] as wimc ON wimc.id_wim_load = wim.id
  Left JOIN [IDS].[WagonInternalMoveCargo] as wimc_next ON wimc_next.id_wim_load = wim_next_wf.id
  Left JOIN [IDS].[Directory_Station] as dir_station ON dir_station.id = wimc.id_division_from
  Left JOIN [IDS].[Directory_Station] as dir_station_next ON dir_station_next.id = wimc_next.id_division_from
  where wim.[id_filing] = @id_filing
	RETURN
 END

GO


