/******************
**** Aggr Erga ****
******************/

--!File must be encoded in UTF8!
--Always use "CREATE". If it is already created and you wish to alter it, just use the
--"Alter it" Radio Button from the programme and "CREATE" will dynamically become "ALTER".

CREATE VIEW [dbo].[v7Aggr_Erga]
AS
SELECT *
FROM [dbo].[v4Erga]
  INNER JOIN (SELECT [v6Mel_Kostol_Aggr_Variantes].[Kodikos_Meletis_FromKostolVar], sum([v6Mel_Kostol_Aggr_Variantes].ItemPriceSum) as PriceSum 
              from [YLIKA_KOSTOL].[dbo].[v6Mel_Kostol_Aggr_Variantes]
			  group by [v6Mel_Kostol_Aggr_Variantes].[Kodikos_Meletis_FromKostolVar]
			  ) [v6Mel_Kostol_Aggr_Variantes]
  ON [v4Erga].[ID_Erga] = [v6Mel_Kostol_Aggr_Variantes].[Kodikos_Meletis_FromKostolVar]