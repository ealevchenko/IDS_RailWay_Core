USE [KRR-PA-CNT-Railway]
GO

/****** Object:  UserDefinedFunction [IDS].[get_count_all_wagons_of_way]    Script Date: 11.11.2024 9:44:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [IDS].[is_wagon_moving_of_id_wim](@id_wim bigint) 
	RETURNS bit
	AS
	BEGIN 
		
	declare @id_wir bigint;
	declare @id_station int;
	declare @id_way int;
	declare @result bit;
	SELECT @id_station = [id_station], @id_way= [id_way], @id_wir = id_wagon_internal_routes FROM [IDS].[WagonInternalMovement] where id=@id_wim;
		
	declare @count int = (SELECT count([id]) FROM [IDS].[WagonInternalMovement] where [id_wagon_internal_routes] = @id_wir and [id] > @id_wim and (id_station <> @id_station or id_way <> @id_way or id_outer_way is not null));
	
	if(@count>0) set @result = 1; else set @result = 0;
	RETURN @result;
	END
GO


