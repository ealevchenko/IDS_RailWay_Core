USE [KRR-PA-CNT-Railway]
GO

/****** Object:  UserDefinedFunction [IDS].[get_view_operation_parent_next]    Script Date: 24.02.2026 9:02:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [IDS].[get_view_operation_parent_next]
 (
	@id_wim bigint
 )
		RETURNS 
		@view TABLE  (
			[id] [bigint] NOT NULL,
			[id_filing] [bigint] NULL,
			[wio_parent_id] [bigint] NULL,
			[wio_parent_id_operation] [int] NULL,
			[wio_parent_operation_start] [datetime] NULL,
			[wio_parent_operation_end] [datetime] NULL,
			[wio_id] [bigint] NULL,
			[wio_id_operation] [int] NULL,
			[wio_operation_start] [datetime] NULL,
			[wio_operation_end] [datetime] NULL,
			[wio_next_id] [bigint] NULL,
			[wio_next_id_operation] [int] NULL,
			[wio_next_operation_start] [datetime] NULL,
			[wio_next_operation_end] [datetime] NULL
		)
		AS
	BEGIN

	INSERT @view
	SELECT 
	   wim.[id]
      ,wim.[id_filing]
	  --
      ,wio.[parent_id] as wio_parent_id
	  ,wio_parent.id_operation as wio_parent_id_operation
	  ,wio_parent.[operation_start] as wio_parent_operation_start
      ,wio_parent.[operation_end] as wio_parent_operation_end
	  --
	  ,wio.[id] as wio_id
	  ,wio.id_operation as wio_id_operation
      ,wio.[operation_start] as wio_operation_start
      ,wio.[operation_end] as wio_operation_end
	  --
      ,wio_next.id as wio_next_id  
      ,wio_next.id_operation as wio_next_id_operation  
	  ,wio_next.[operation_start] as wio_next_operation_start
      ,wio_next.[operation_end] as wio_next_operation_end
	  --into wio_operation
  FROM [IDS].[WagonInternalMovement]  as wim
  		--> Операция подачи		
		LEFT JOIN IDS.WagonInternalOperation as wio ON wio.id = wim.[id_wio]
  		--> Операция подачи		
		LEFT JOIN IDS.WagonInternalOperation as wio_parent ON wio_parent.id = wio.[parent_id]
  		--> Операция подачи		
		LEFT JOIN IDS.WagonInternalOperation as wio_next ON wio_next.[parent_id] = wio.id

  where wim.id = @id_wim
	RETURN
 END

GO


