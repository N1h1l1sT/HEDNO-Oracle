/******************
**** Trimestre ****
******************/

--!File must be encoded in UTF8!
--Always use "CREATE". If it is already created and you wish to alter it, just use the
--"Alter it" Radio Button from the programme and "CREATE" will dynamically become "ALTER".

CREATE VIEW [dbo].[v8Price_Per_Trimestre]
AS
SELECT [TimeSeriesDate], sum([PriceSum]) as TrimestrePriceSum 
       FROM [YLIKA_KOSTOL].[dbo].[v7Aggr_Erga]
	   Group by [v7Aggr_Erga].[TimeSeriesDate]