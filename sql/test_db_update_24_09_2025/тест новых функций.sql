USE [KRR-PA-CNT-Railway]

select * from [IDS].[get_view_wagons_of_id_way](226)

--select top(1) id from [IDS].[InstructionalLettersWagon] as ilws where ilws.id_wir = 1129027 and ilws.status < 3

--select * from [IDS].[get_current_operation_wagon_of_id_wir](1132614) -- отмена
--select * from [IDS].[get_current_operation_wagon_of_id_wir](1132624)

select * from [IDS].[get_current_operation_wagon_of_num](68629583) -- отмена
--select * from [IDS].[get_current_operation_wagon_of_num](63524086)

--select * from [IDS].[get_view_incoming_cars_of_id_car](2029653)-- отмена
--select * from [IDS].[get_view_incoming_cars_of_id_car](2029663)
--select * from [IDS].[get_view_incoming_cars_of_id_car](2029705)-- не заходил

--select * from [IDS].[get_view_incoming_cars_of_id_sostav](399457)

