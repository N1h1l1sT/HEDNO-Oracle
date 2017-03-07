/******************
**** View Erga ****
******************/

--!File must be encoded in UTF8!
--Always use "CREATE". If it is already created and you wish to alter it, just use the
--"Alter it" Radio Button from the programme and "CREATE" will dynamically become "ALTER".


CREATE VIEW [dbo].[v9FinalDataset]
AS
SELECT [Label] -- LABELLED DATA: Whether the Project was done or not
	  ,[ID] AS [ID_Erga] --Unique Identification for each row
	  ,[TimeSeriesDate] -- Which Quarter of which year the project began
	  ,[MONADA] AS [GrafioEktelesisErgou] --Described as: "Grafio ektelesis ergou" and also as "Geografiki perioxi ergou"
	  ,[Onoma_Polis] --The name of the city in which the project takes places
	  ,[GeoLocX] --Column Should be added to the Original Table as INT NULL (with no default value)
	  ,[GeoLocY] --Column Should be added to the Original Table as INT NULL (with no default value)
	  --,[Hmerominia] --Όποτε υπάρχει η Ημερ_Μελέτης, παίρνει αυτή την τιμή, αλλιώς παίρνει την τιμή της Ημερ_Αναγγελίας - Ουσιαστικά είναι και πάλι η ημερομονία μελέτης, αλλά με λιγότερα NULL
      --,[ΗΜΕΡ_ΜΕΛΕΤΗΣ] AS [Imer_Meletis] --ήρθε ο πελάτης με κάποιο αίτημα --Παίρνει τιμή την ημερομηνία που έχει τελειώσει η μελέτη
      --,[ΗΜΕΡ_ΑΝΑΓΓΕΛΙΑΣ] AS [Imer_Anagelias] --Στέλνουμε γράμμα που ισχύει τρεις μήνες και του λέμε το ποσό της συμμετοχής που πρέπει να πληρώσει για το έργο (σχεδόν πάντα, ίδια ημερομηνία με Ημερ_Μελέτης)
      --,[ΗΜΕΡ_ΥΠΟΓΡΑΦΗΣ] AS [Imer_Ypografis] --LABELED DATA: Είναι πεδίο που πρέπει να προβλέψουμε δηλ. αν θα γίνει η πληρωμεί και πότε. Από τη στιγμή που το πεδίο παίρνει τιμή (date) χρειαζόμαστε τα υλικά
      --,[ID2] --inconsequential
      --,[ID_ΠΡΟΤΑΣΗΣ] AS [ID_Protasis] --inconsequential
      --,[ΕΤΟΣ] AS [Etos] --Subsumed under the TimeSeriesDate paradigm --inconsequential
      --,[Α_Α] AS [A_A] --inconsequential
      --,[ΗΜΕΡ_ΚΑΤΑΧΩΡΗΣΗΣ] AS [Imer_Kataxorisis] --inconsequential
      --,[ΑΡΙΘΜΟΣ] AS [Arithmos] --inconsequential
      --,[ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ] --inconsequential --Completely Missing
      --,[ΣΚΟΠΟΣ_ΕΡΓΟΥ] --inconsequential --Completely Missing
      --,[ΕΤΟΣ_ΕΡΓΟΥ] --inconsequential --Completely Missing
      --,[ΑΡΙΘΜΟΣ_ΕΡΓΟΥ] --inconsequential --Completely Missing
      --,[ΑΚΥΡΩΘΕΝ] AS [Akyrothen] --Rows cannot be deleted, hence if the need arises due to an erroneous entry, it's reflected here
      ,[ΚΑΤΗΓΟΡΙΑ] AS [Katigoria] --Similar to [SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ] but more general
	  ,[Xaraktirismos_Ergou] --Main project's category
	  ,[Skopos_Ergou] --Project's Sub-category
      --,[ΦΟΠ_ΛΟΙΠΑ] AS [FOP_Lipa] --inconsequential
      --,[ΖΗΜΙΑ_ΠΑΡΑΛΑΓΗ] AS [Zimia_Paralagi] --inconsequential
      --,[ΟΜΑΔΑ] AS [Omada] --inconsequential
      --,[ΕΤΟΣ_ΜΕΛΕΤΗΣ] AS [Etos_Meletis] --inconsequential
      --,[ΑΡΙΘΜΟΣ_ΜΕΛΕΤΗΣ] AS [Arithmos_Meletis] --inconsequential
      --,[ΕΙΔΟΣ_ΕΞΥΠΗΡΕΤΗΣΗΣ0] AS [Eidos_Eksipiretisis0] --inconsequential
      --,[ΕΤΟΣ_ΚΑΤΑΣΚΕΥΗΣ] AS [Etos_Kataskevis] --inconsequential
      --,[ΑΡΙΘΜΟΣ_ΚΑΤΑΣΚΕΥΗΣ] AS [Arithmos_Kataskevis] --inconsequential
      --,[ΔΕΗ_ΠΕΛΑΤΗΣ] AS [DEH_Pelatis] -- 1=DEH, 0,2,NULL = Client --We ONLY care about the Clients, not DEH
      --,[ΑΡ_ΠΡΩΤΟΚΟΛΟΥ_ΠΕΛΑΤΗ] AS [Ar_Protokolou_Pelati]  --inconsequential --Mostly Missing --inconsequential
      --,[ΑΡ_ΠΡΩΤΟΚΟΛΟΥ_ΔΕΗ] AS [Ar_Protokolou_DEH] --inconsequential --Mostly Missing --inconsequential
      --,[ΝΕΟ_ΠΑΡΑΛΛΑΓΗ] --Mostly Missing --inconsequential
      --,[ΑΡ_ΠΑΡΟΧΗΣ] AS [Ar_Paroxis] --Mostly Missing --inconsequential
      --,[ΠΕΡΙΓΡΑΦΗ] --inconsequential
      --,[ΗΜΕΡ_ΑΙΤΗΣΗΣ] AS [Imer_Aitisis] --inconsequential
      --,[ΑΡΧΗ_ΠΑΡΑΤΗΡ_ΜΕΛΕΤΗΣ] --Mostly Missing --inconsequential
      --,[ΤΕΛΟΣ_ΠΑΡΑΤΗΡ_ΜΕΛΕΤΗΣ] --Mostly Missing --inconsequential
	  ,[MelClientDelay] --Whether there was a delay in the study caused by the client
	  ,[Mel_Kathisterisi_Pelati] --The delay, in days, caused by the client
      --,[ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] AS [Mel_Kathisterisi_Pelati] --
      --,[ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] AS [Mel_Xik_Kathisterisi_Pelati] --
      --,[ΜΕΛ_ΕΝΔ_ΚΑΘ_ΠΕΛΑΤΗ] AS [Mel_End_Kath_Pelati] --Αν κάποιος ξεκινήσει μια καθυστέρηση την ημερομηνία ΑΠΟ  και δεν καταχώρηση το ΕΩΣ το binary θα γίνει 1 αλλά δεν θα έχει σύνολο ημερών καθυστέρησης. 
	  ,[MelDEHDelay]
	  ,[Mel_Kathisterisi_DEH] --The delay, in days, caused by the DEDDHE
      --,[ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] AS [Mel_Kathisterisi_DEH] --Should facilitate feature engineer with "XYK" and "END"
      --,[ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] AS [Mel_Xyk_Kathisterisi_DEH] --Should facilitate feature engineer with "XYK" and "END"
      --,[ΜΕΛ_ΕΝΔ_ΚΑΘ_ΔΕΗ] AS [Mel_End_Kath_DEH] --Αν κάποιος ξεκινήσει μια καθυστέρηση την ημερομηνία ΑΠΟ  και δεν καταχώρηση το ΕΩΣ το binary θα γίνει 1 αλλά δεν θα έχει σύνολο ημερών καθυστέρησης. 
      ,[MelOthersDelay]
	  ,[Mel_Kathisterisi_Triton]
	  --,[ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] AS [Mel_Kathisterisi_Triton] --Should facilitate feature engineer with "XYK" and "END"
      --,[ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] AS [Mel_Xyk_Kathisterisi_Triton] --Should facilitate feature engineer with "XYK" and "END"
      --,[ΜΕΛ_ΕΝΔ_ΚΑΘ_ΤΡΙΤΩΝ] AS [Mel_End_Kath_Triton] --Αν κάποιος ξεκινήσει μια καθυστέρηση την ημερομηνία ΑΠΟ  και δεν καταχώρηση το ΕΩΣ το binary θα γίνει 1 αλλά δεν θα έχει σύνολο ημερών καθυστέρησης. 
      ,[ΗΜΕΡΕΣ_ΜΕΛΕΤΗΣ] AS [Meres_Meletis] --The number of days that the 'Study' part of the project lasted, including non-DEDEH delays
      --,[ΕΡΓ_ΗΜΕΡΕΣ_ΜΕΛΕΤΗΣ] AS [Erg_Meres_Meletis] --We already have the Days it took
      ,UPPER(LTRIM(RTRIM([ΣΥΝΕΡΓΕΙΟ_ΜΕΛΕΤΗΣ]))) AS [Sinergio_Meletis] --To sinergio, 3 Factor Levels
      --,UPPER(LTRIM(RTRIM([ΜΕΛΕΤΗΤΗΣ]))) AS [Meletitis] --1932 Meletites
      --,[ΑΩ_ΚΑΤΑΣΚΕΥΗΣ]
      --,[ΚΟΣΤΟΣ_ΜΕΛΕΤΗΤΗ] AS [Kostos_Meletiti] --Mostly Missing
      ,[ΚΟΣΤΟΣ_ΕΡΓΑΤΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] AS [Kostos_Ergatikon_Kataskevis]
      ,[ΚΟΣΤΟΣ_ΥΛΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] AS [Kostos_Ilikon_Kataskevis]
      ,[ΚΟΣΤΟΣ_ΚΑΤΑΣΚΕΥΗΣ] AS [Kostos_Kataskevis]
      ,[ΚΟΣΤΟΣ_ΕΡΓΟΛΑΒΙΚΩΝ_ΕΠΙΔΟΣΗΣ] AS [Kostos_Ergolavikon_Epidosis]
      --,[ΑΩ_ΜΕΛΕΤΗΣ] --inconsequential
      --,[ΗΜΕΡ_ΠΑΡΑΛΑΒΗΣ] --inconsequential --Πότε πήγε στον εργολάβο
      --,[ΕΙΔΟΣ_ΕΞΥΠΗΡΕΤΗΣΗΣ] AS [Idos_Eksipiretisis] --inconsequential
      --,[ΤΙΤΛΟΣ_ΕΡΓΟΥ] --inconsequential
      --,[ΣΥΜΒ_ΗΜΕΡ_ΕΝΑΡΞΗΣ] AS [Symb_Imer_Enarksis] --inconsequential --συμβατική ημερομηνία. Εσωτερικό πεδίο. Υπάρχει ένα ανώτερο όριο να το φτιάξει. Η σύμβαση λέει ότι πρέπει να φτιαχτεί σε 10 μέρες.
      --,[ΣΥΜΒ_ΗΜΕΡ_ΕΚΤΕΛΕΣΗΣ] AS [Symb_Imer_Ektelesis] --inconsequential --Αν αφαιρέσω αυτή την ημερομηνία από την πραγματική ημερομηνία εκτέλεσης, βλέπω την καθυστέρηση
      --,[ΑΡΧΗ_ΠΑΡΑΤΗΡ_ΕΚΤΕΛΕΣΗΣ] --inconsequential
      --,[ΤΕΛΟΣ_ΠΑΡΑΤΗΡ_ΕΚΤΕΛΕΣΗΣ] --inconsequential
	  --,[KatClientDelay]
	  --,[Kat_Kathisterisi_Pelati]
      --,[ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] AS [Kat_Kathisterisi_Pelati] --
      --,[ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] AS [Kat_Xyk_Kathisterisi_Pelati] --
      --,[ΚΑΤ_ΕΝΔ_ΚΑΘ_ΠΕΛΑΤΗ] AS [Kat_End_Kath_Pelati] --Αν κάποιος ξεκινήσει μια καθυστέρηση την ημερομηνία ΑΠΟ  και δεν καταχώρηση το ΕΩΣ το binary θα γίνει 1 αλλά δεν θα έχει σύνολο ημερών καθυστέρησης. 
      --,[KatDEHDelay]
	  --,[Kat_Kathisterisi_DEH]
	  --,[ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] AS [Kat_Kathisterisi_DEH] --
      --,[ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] AS [Kat_Xyk_Kathisterisi_DEH] --
      --,[ΚΑΤ_ΕΝΔ_ΚΑΘ_ΔΕΗ] AS [Kat_End_Kath_DEH] --Αν κάποιος ξεκινήσει μια καθυστέρηση την ημερομηνία ΑΠΟ  και δεν καταχώρηση το ΕΩΣ το binary θα γίνει 1 αλλά δεν θα έχει σύνολο ημερών καθυστέρησης. 
      --,[KatOthersDelay]
	  --,[Kat_Kathisterisi_Triton]
	  --,[ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] AS [Kat_Kathisterisi_Triton] --
      --,[ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] AS [Kat_Xyk_Kathisterisi_Triton] --
      --,[ΚΑΤ_ΕΝΔ_ΚΑΘ_ΤΡΙΤΩΝ] AS [Kat_End_Kath_Triton] --Αν κάποιος ξεκινήσει μια καθυστέρηση την ημερομηνία ΑΠΟ  και δεν καταχώρηση το ΕΩΣ το binary θα γίνει 1 αλλά δεν θα έχει σύνολο ημερών καθυστέρησης. 
      --,[ΗΜΕΡ_ΕΝΑΡΞΗΣ] AS [Imer_Enarksis] --inconsequential
      --,[ΗΜΕΡ_ΕΚΤΕΛΕΣΗΣ] AS [Imer_Ektelesis] --inconsequential --Πότε φτιάχτηκε
      --,[ΠΟΣ_ΕΚΤΕΛΕΣΗΣ] AS [Pos_Ektelesis] --inconsequential (αν πληρώθηκε ο εργολάβος ή όχι)
      --,[ΠΙΣΤΟΠΟΙΗΣΗ] AS [Pistopiisi] --inconsequential
      --,[ΗΜΕΡ_ΠΙΣΤΟΠΟΙΗΣΗΣ] AS [Imer_Pistopiisis] --inconsequential
      --,[ΗΜΕΡΕΣ_ΕΚΤΕΛΕΣΗΣ] AS [Meres_Ektelesis]
      --,[ΕΡΓ_ΗΜΕΡΕΣ_ΕΚΤΕΛΕΣΗΣ] AS [Erg_Meres_Ektelesis] 
      --,UPPER(LTRIM(RTRIM([ΣΥΝΕΡΓΕΙΟ_ΚΑΤΑΣΚΕΥΗΣ]))) AS [Sinergio_Kataskevis]
      --,UPPER(LTRIM(RTRIM([ΚΑΤΑΣΚΕΥΑΣΤΗΣ]))) AS [Kataskevastis]
      --,[ΑΠΟΛ_ΚΟΣΤΟΣ_ΕΡΓΑΤΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] AS [Apol_Kostos_Ergatikon_Kataskevis] --Mostly Missing
      --,[ΑΠΟΛ_ΚΟΣΤΟΣ_ΥΛΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] AS [Apol_Kostos_Ylikon_Kataskevis] --Mostly Missing
      --,[ΑΠΟΛ_ΚΟΣΤΟΣ_ΚΑΤΑΣΚΕΥΗΣ] AS [Apol_Kostos_Kataskevis] --Mostly Missing
      --,[ΕΚΤΥΠΩΣΗ_ΠΡΩΤΟΚΟΛΟΥ] AS [Ektiposi_Protokolou] --inconsequential
      --,[ΟΝΟΜΑΤΕΠΩΝΥΜΟ] --inconsequential
      --,[ΔΙΕΥΘΥΝΣΗ] --inconsequential
      --,[ΠΟΛΗ] AS [Poli] --Feature Engineered a new City field
      --,[ΠΟΛΗ_Υ_Σ] AS [Poli_Y_S] --Feature Engineered a new City field
      --,[Υ_Σ] AS [Y_S] --inconsequential (30,874 Factors)
      --,[ΤΗΛΕΦΩΝΟ] --inconsequential
      --,[ΠΑΡΑΤΗΡΗΣΕΙΣ] --inconsequential
      --,[ΠΑΡΑΤΗΡΗΣΕΙΣ2] --inconsequential
      ,[ΕΚΤΑΣΗ_ΕΡΓΟΥ] AS [Ektasi_Ergou]
      ,[ΑΝΑΓΚΗ_ΥΣ] AS [Anagi_YS]
	  --,[Diktio_Xt_Mt]
      --,[ΔΙΚΤΥΟ_ΧΤ_ΜΤ] AS [Diktio_Xt_Mt] --inconsequential --One of its 3 factors is a Blank - This is probably meant as a NULL but it is not, so it's featured engineered
      --,[ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] AS [Kod_Logariasmou] -- 41/D = ΕΠΕΝΔΥΣΗ, 42/M = ΕΚΜΕΤΑΛΕΥΣΗ. 219896 41, 105615 42, 18918  D, 21397  M, 11635 NULL
      --,[ΚΩΔ_ΑΝΑΛΥΣΗΣ] AS [Kod_Analysis] -- 1163 Factors, Ipokatigories Ergon
      --,[SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ] AS [SAP_Xaraktirismos_Ergou] --41/D = ΕΠΕΝΔΥΣΗ, 42/M = ΕΚΜΕΤΑΛΕΥΣΗ. 219896 41, 105615 42, 18918  D, 21397  M, 11635 NULL
      --,[SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ] AS [SAP_Skopos_Ergou] --Ipokatigories Ergon
      ,UPPER(LTRIM(RTRIM([SAP_ΤΥΠΟΣ_ΠΕΛΑΤΗ]))) AS [SAP_Typos_Pelati]
      ,UPPER(LTRIM(RTRIM([SAP_ΕΙΔΟΣ_ΑΙΤΗΜΑΤΟΣ]))) AS [SAP_Eidos_Aitimatos]
      --,[SAP_ΑΡΙΘΜΟΣ_ΕΡΓΟΥ] AS [SAP_Arith_Ergou] --inconsequential (89,051 Factors)
      --,[UserName] --inconsequential
      --,[UpDate] --inconsequential
      --,[Test] --inconsequential
	  ,[Kathisterisi_AitisisKataxorisis]
	  ,[Kathisterisi_Meletis]
	  ,[Kathisterisi_Anagelias]
	  ,[DayOfYearSine]
	  ,[DayOfYearCosine]
	  ,[DayOfYearCartesX]
	  ,[DayOfYearCartesY]
	  ,[MarkedForTest]
	  
  FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]
  
    --inconsequential
    --INNER JOIN (SELECT [ID] AS tmp_ID4,
				--	(SELECT CASE
				--		WHEN UPPER(LTRIM(RTRIM([ΔΙΚΤΥΟ_ΧΤ_ΜΤ]))) = 'ΧΤ' OR UPPER(LTRIM(RTRIM([ΔΙΚΤΥΟ_ΧΤ_ΜΤ]))) = 'ΜΤ' THEN
				--			(UPPER(LTRIM(RTRIM([ΔΙΚΤΥΟ_ΧΤ_ΜΤ]))))
				--		END AS [Diktio_Xt_Mt]
				--	)
				--AS [Diktio_Xt_Mt]
				--FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp4
				--ON tmp4.tmp_ID4 = [ΕΡΓΑ].ID
				
    INNER JOIN (SELECT [ID] AS tmp_ID3,
					(SELECT CASE
						WHEN [ΠΟΛΗ] IS NOT NULL THEN
							(UPPER(LTRIM(RTRIM([ΠΟΛΗ]))))
						WHEN [ΠΟΛΗ_Υ_Σ] IS NOT NULL THEN
							(UPPER(LTRIM(RTRIM([ΠΟΛΗ_Υ_Σ]))))
						END AS [Onoma_Polis]
					)
				AS [Onoma_Polis]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp3
				ON tmp3.tmp_ID3 = [ΕΡΓΑ].ID
				
    INNER JOIN (SELECT [ID] AS tmp_ID0,
					(SELECT CASE
						WHEN [ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41'
						OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D'
						OR [SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ] = '41'
						OR UPPER(LTRIM(RTRIM([SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ]))) = 'D'
						OR [ΚΑΤΗΓΟΡΙΑ] = '9300'
						OR [ΚΑΤΗΓΟΡΙΑ] = '9900'
						OR [ΚΑΤΗΓΟΡΙΑ] = '9500'
						OR [ΚΑΤΗΓΟΡΙΑ] = '9700'
						OR [ΚΑΤΗΓΟΡΙΑ] = '9600'
						THEN
							('EPENDISI')
						
						WHEN [ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42'
						OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M'
						OR [SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ] = '42'
						OR UPPER(LTRIM(RTRIM([SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ]))) = 'M'
						OR [ΚΑΤΗΓΟΡΙΑ] = '250'
						THEN
							('EKMETALEFSI')
						END AS [Xaraktirismos_Ergou]
					)
				AS [Xaraktirismos_Ergou]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp0
				ON tmp0.tmp_ID0 = [ΕΡΓΑ].ID
				
    INNER JOIN (SELECT [ID] AS tmp_ID1,
					(SELECT CASE
						WHEN UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'EA' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'EB' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'EC' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'ED' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'EE' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'EF' OR
						UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SA' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SB' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SC' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SD' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SE' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SF' OR
						UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SG' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SH' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SI' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SJ' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SK' OR UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))) = 'SL' THEN
							(UPPER(LTRIM(RTRIM([SAP_ΣΚΟΠΟΣ_ΕΡΓΟΥ]))))
						
						WHEN UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'EA' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'EB' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'EC' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'ED' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'EE' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'EF' OR
						 UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SA' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SB' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SC' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SD' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SE' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SF' OR
						  UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SG' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SH' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SI' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SJ' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SK' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))) = 'SL' THEN
							(UPPER(LTRIM(RTRIM([ΚΩΔ_ΑΝΑΛΥΣΗΣ]))))
						--Placement DOES matter hereinafter as some rules are more general than others and less general rules will never occur if they aren't placed first
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '324%' THEN
							'EB'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '32%' THEN
							'EA'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '336%' THEN
							'EF'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '33%' THEN
							'EC'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '316%' THEN
							'ED'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '34%' THEN
							'EE'
						WHEN [ΚΑΤΗΓΟΡΙΑ] = '9300' THEN
							'EA'
						WHEN [ΚΑΤΗΓΟΡΙΑ] = '9900' THEN
							'EB'
						WHEN [ΚΑΤΗΓΟΡΙΑ] = '9500' THEN
							'EC'
						WHEN [ΚΑΤΗΓΟΡΙΑ] = '9700' THEN
							'ED'
						WHEN [ΚΑΤΗΓΟΡΙΑ] = '9600' THEN
							'EF'
						WHEN [ΚΑΤΗΓΟΡΙΑ] = '9400' AND ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '41'
														OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'D'
														OR [SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ] = '41'
														OR UPPER(LTRIM(RTRIM([SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ]))) = 'D') THEN
							'EE'


						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '321%' THEN
							'SA'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '322%' THEN
							'SB'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '33%' THEN
							'SC'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '36%' THEN
							'SF'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '325%' THEN
							'SG'
						WHEN ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42' OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M') AND [ΚΩΔ_ΑΝΑΛΥΣΗΣ] LIKE '326%' THEN
							'SH'
						WHEN [ΚΑΤΗΓΟΡΙΑ] = '9400' AND ([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ] = '42'
														OR UPPER(LTRIM(RTRIM([ΚΩΔ_ΛΟΓΑΡΙΑΣΜΟΥ]))) = 'M'
														OR [SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ] = '42'
														OR UPPER(LTRIM(RTRIM([SAP_ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΕΡΓΟΥ]))) = 'M') THEN
							'SF'
						END AS [Skopos_Ergou]
					)
				AS [Skopos_Ergou]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp1
				ON tmp1.tmp_ID1 = [ΕΡΓΑ].ID
				
	--INNER JOIN (SELECT ID as tmpID7, (SELECT CASE
	--											WHEN [ΗΜΕΡ_ΜΕΛΕΤΗΣ] IS NOT NULL THEN
	--												[ΗΜΕΡ_ΜΕΛΕΤΗΣ]
	--											ELSE
	--												[ΗΜΕΡ_ΑΝΑΓΓΕΛΙΑΣ]
	--											END AS [Hmerominia]
	--											) [Hmerominia]
	--			FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp7
	--			ON tmp7.tmpID7 = [ΕΡΓΑ].ID

	INNER JOIN (SELECT ID AS tmpID, (convert(varchar(4), YEAR([ΗΜΕΡ_ΑΙΤΗΣΗΣ])) +  (SELECT CASE 
																						WHEN MONTH([ΗΜΕΡ_ΑΙΤΗΣΗΣ]) < 4 THEN
																							(' Q1')
																						WHEN MONTH([ΗΜΕΡ_ΑΙΤΗΣΗΣ]) < 7 THEN
																							(' Q2')
																						WHEN MONTH([ΗΜΕΡ_ΑΙΤΗΣΗΣ]) < 10 THEN
																							(' Q3')
																						ELSE
																							(' Q4')
																						END AS [TimeSeriesDate]
																					)
								        ) AS [TimeSeriesDate]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp
				ON tmp.tmpID = [ΕΡΓΑ].ID

	INNER JOIN (SELECT [ID] AS tmpID2,
					(SELECT CASE
						WHEN [ΗΜΕΡ_ΥΠΟΓΡΑΦΗΣ] IS NULL THEN
							(0)
						ELSE
							(1)
						END AS [Label]
					) AS [Label]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp2
				ON tmp2.tmpID2 = [ΕΡΓΑ].ID

	INNER JOIN (SELECT [ID] AS tmpID6,
					(SELECT CASE
						WHEN (([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] IS NULL OR [ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] = 0)
						AND ([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] IS NULL OR [ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] = 0)
						AND [ΜΕΛ_ΕΝΔ_ΚΑΘ_ΠΕΛΑΤΗ] = 0) THEN
							(0)
						ELSE
							(1)
						END AS [MelClientDelay]
					) AS [MelClientDelay]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp6
				ON tmp6.tmpID6 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT [ID] AS tmpID5,
					(SELECT CASE
						WHEN (([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] IS NULL OR [ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] = 0)
						AND ([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] IS NULL OR [ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] = 0)
						AND [ΜΕΛ_ΕΝΔ_ΚΑΘ_ΔΕΗ] = 0) THEN
							(0)
						ELSE
							(1)
						END AS [MelDEHDelay]
					) AS [MelDEHDelay]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp5
				ON tmp5.tmpID5 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT [ID] AS tmpID8,
					(SELECT CASE
						WHEN (([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] IS NULL OR [ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] = 0)
						AND ([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] IS NULL OR [ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] = 0)
						AND [ΜΕΛ_ΕΝΔ_ΚΑΘ_ΤΡΙΤΩΝ] = 0) THEN
							(0)
						ELSE
							(1)
						END AS [MelOthersDelay]
					) AS [MelOthersDelay]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp8
				ON tmp8.tmpID8 = [ΕΡΓΑ].ID
				
	--INNER JOIN (SELECT [ID] AS tmpID9,
	--				(SELECT CASE
	--					WHEN (([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] IS NULL OR [ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] = 0)
	--					AND ([ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] IS NULL OR [ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] = 0)
	--					AND [ΚΑΤ_ΕΝΔ_ΚΑΘ_ΠΕΛΑΤΗ] = 0) THEN
	--						(0)
	--					ELSE
	--						(1)
	--					END AS [KatClientDelay]
	--				) AS [KatClientDelay]
	--			FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp9
	--			ON tmp9.tmpID9 = [ΕΡΓΑ].ID
				
	--INNER JOIN (SELECT [ID] AS tmpID10,
	--				(SELECT CASE
	--					WHEN (([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] IS NULL OR [ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] = 0)
	--					AND ([ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] IS NULL OR [ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] = 0)
	--					AND [ΚΑΤ_ΕΝΔ_ΚΑΘ_ΔΕΗ] = 0) THEN
	--						(0)
	--					ELSE
	--						(1)
	--					END AS [KatDEHDelay]
	--				) AS [KatDEHDelay]
	--			FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp10
	--			ON tmp10.tmpID10 = [ΕΡΓΑ].ID
				
	--INNER JOIN (SELECT [ID] AS tmpID11,
	--				(SELECT CASE
	--					WHEN (([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] IS NULL OR [ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] = 0)
	--					AND ([ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] IS NULL OR [ΚΑΤ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] = 0)
	--					AND [ΚΑΤ_ΕΝΔ_ΚΑΘ_ΤΡΙΤΩΝ] = 0) THEN
	--						(0)
	--					ELSE
	--						(1)
	--					END AS [KatOthersDelay]
	--				) AS [KatOthersDelay]
	--			FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp11
	--			ON tmp11.tmpID11 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT [ID] AS tmpID12,
					(SELECT CASE
						WHEN ([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] IS NOT NULL) THEN
							([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ])
						WHEN ([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] IS NOT NULL) THEN
							([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ])
						ELSE
							(0)
						END AS [Mel_Kathisterisi_Pelati]
					) AS [Mel_Kathisterisi_Pelati]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp12
				ON tmp12.tmpID12 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT [ID] AS tmpID13,
					(SELECT CASE
						WHEN ([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] IS NOT NULL) THEN
							([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ])
						WHEN ([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] IS NOT NULL) THEN
							([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ])
						ELSE
							(0)
						END AS [Mel_Kathisterisi_DEH]
					) AS [Mel_Kathisterisi_DEH]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp13
				ON tmp13.tmpID13 = [ΕΡΓΑ].ID

	INNER JOIN (SELECT [ID] AS tmpID14,
					(SELECT CASE
						WHEN ([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] IS NOT NULL) THEN
							([ΜΕΛ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ])
						WHEN ([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] IS NOT NULL) THEN
							([ΜΕΛ_ΧΥΚ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ])
						ELSE
							(0)
						END AS [Mel_Kathisterisi_Triton]
					) AS [Mel_Kathisterisi_Triton]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp14
				ON tmp14.tmpID14 = [ΕΡΓΑ].ID
				
	--INNER JOIN (SELECT [ID] AS tmpID15,
	--				(SELECT CASE
	--					WHEN ([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ] IS NOT NULL) THEN
	--						([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΠΕΛΑΤΗ])
	--					ELSE
	--						(0)
	--					END AS [Kat_Kathisterisi_Pelati]
	--				) AS [Kat_Kathisterisi_Pelati]
	--			FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp15
	--			ON tmp15.tmpID15 = [ΕΡΓΑ].ID
				
	--INNER JOIN (SELECT [ID] AS tmpID16,
	--				(SELECT CASE
	--					WHEN ([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ] IS NOT NULL) THEN
	--						([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΔΕΗ])
	--					ELSE
	--						(0)
	--					END AS [Kat_Kathisterisi_DEH]
	--				) AS [Kat_Kathisterisi_DEH]
	--			FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp16
	--			ON tmp16.tmpID16 = [ΕΡΓΑ].ID

	--INNER JOIN (SELECT [ID] AS tmpID17,
	--				(SELECT CASE
	--					WHEN ([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ] IS NOT NULL) THEN
	--						([ΚΑΤ_ΚΑΘΥΣΤΕΡΗΣΗ_ΤΡΙΤΩΝ])
	--					ELSE
	--						(0)
	--					END AS [Kat_Kathisterisi_Triton]
	--				) AS [Kat_Kathisterisi_Triton]
	--			FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp17
	--			ON tmp17.tmpID17 = [ΕΡΓΑ].ID
					
	INNER JOIN (SELECT [ID] AS tmpID24, DATEDIFF(day, [ΗΜΕΡ_ΑΙΤΗΣΗΣ], [ΗΜΕΡ_ΚΑΤΑΧΩΡΗΣΗΣ]) AS [Kathisterisi_AitisisKataxorisis]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp24
				ON tmp24.tmpID24 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT [ID] AS tmpID25, DATEDIFF(day, [ΗΜΕΡ_ΚΑΤΑΧΩΡΗΣΗΣ], [ΗΜΕΡ_ΜΕΛΕΤΗΣ]) AS [Kathisterisi_Meletis]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp25
				ON tmp25.tmpID25 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT [ID] AS tmpID22, DATEDIFF(day, [ΗΜΕΡ_ΜΕΛΕΤΗΣ], [ΗΜΕΡ_ΑΝΑΓΓΕΛΙΑΣ]) AS [Kathisterisi_Anagelias]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp22
				ON tmp22.tmpID22 = [ΕΡΓΑ].ID

	INNER JOIN (SELECT ID AS tmpID26, SIN(((datepart(dayofyear, [ΗΜΕΡ_ΑΙΤΗΣΗΣ])) * 360) / 366) AS [DayOfYearSine]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp26
				ON tmp26.tmpID26 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT ID AS tmpID27, COS(((datepart(dayofyear, [ΗΜΕΡ_ΑΙΤΗΣΗΣ])) * 360) / 366) AS [DayOfYearCosine]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp27
				ON tmp27.tmpID27 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT ID AS tmpID28, (datepart(year, [ΗΜΕΡ_ΑΙΤΗΣΗΣ]) * COS(((datepart(dayofyear, [ΗΜΕΡ_ΑΙΤΗΣΗΣ])) * 360) / 366)) AS [DayOfYearCartesX]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp28
				ON tmp28.tmpID28 = [ΕΡΓΑ].ID

	INNER JOIN (SELECT ID AS tmpID29, (datepart(year, [ΗΜΕΡ_ΑΙΤΗΣΗΣ]) * SIN(((datepart(dayofyear, [ΗΜΕΡ_ΑΙΤΗΣΗΣ])) * 360) / 366)) AS [DayOfYearCartesY]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp29
				ON tmp29.tmpID29 = [ΕΡΓΑ].ID
				
	INNER JOIN (SELECT [ID] AS tmpID30,
		(SELECT CASE
			WHEN ([Label] = 0 AND DATEADD(MONTH,2,[ΗΜΕΡ_ΑΙΤΗΣΗΣ]) >= '2016-07-06') THEN
				(1)
			ELSE
				(0)
			END AS [MarkedForTest]
		) AS [MarkedForTest]
	FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]
		INNER JOIN (SELECT [ID] AS tmpID31,
					(SELECT CASE
						WHEN [ΗΜΕΡ_ΥΠΟΓΡΑΦΗΣ] IS NULL THEN
							(0)
						ELSE
							(1)
						END AS [Label]
					) AS [Label]
				FROM [YLIKA_KOSTOL].[dbo].[ΕΡΓΑ]) tmp31
				ON tmp31.tmpID31 = [ΕΡΓΑ].ID) tmp30
	ON tmp30.tmpID30 = [ΕΡΓΑ].ID
															
  WHERE (([ΔΕΗ_ΠΕΛΑΤΗΣ] <> 1 OR [ΔΕΗ_ΠΕΛΑΤΗΣ] IS NULL) AND  --Getting only Clients --1=DEH
		 ([ΑΚΥΡΩΘΕΝ] = 0 OR [ΑΚΥΡΩΘΕΝ] IS NULL) AND  --Akyrothen <> 0 = false record, ergo non needed
		 [Onoma_Polis] IS NOT NULL AND --216171 --Needed for the Clustering
		 --([Hmerominia] IS NOT NULL) AND --NOT Null so that we can have an accurate Label --The difference between those dates is negligible, so if the one is NULL, the other is used
		 
		 --For the Real-data algorithm, we need the whole thing to pass through Clustering, and this clause will be applied afterwards programmatically
		 --([Label] = 0 AND DATEADD(MONTH,2,[ΗΜΕΡ_ΑΙΤΗΣΗΣ]) >= '2016-07-06') AND --[Instead of '2016-07-06', it should be GETDATE() on the real HEDNO's server] --Getting the as of yet undecided projects
		 
		 [Xaraktirismos_Ergou] IS NOT NULL AND --The database encompasses a wide range of things but we only care for projects. Those are the 41,D,42,M. The NOT NULL means that that's all the query retrieves
		 [Skopos_Ergou] IS NOT NULL AND --The feature's been engineered so that only rows with a value on this are relevant
		 
		 [ΗΜΕΡΕΣ_ΜΕΛΕΤΗΣ] >= 0 AND --A study cannot have ended before it even began, hence erroneous data (noise) are being cleaned
		 [ΚΟΣΤΟΣ_ΕΡΓΑΤΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] IS NOT NULL AND --Cost is a critical variable, so it must be filled in
		 [ΚΟΣΤΟΣ_ΥΛΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] IS NOT NULL AND --Cost is a critical variable, so it must be filled in
		 [ΚΟΣΤΟΣ_ΚΑΤΑΣΚΕΥΗΣ] IS NOT NULL AND --Cost is a critical variable, so it must be filled in
		 [ΚΟΣΤΟΣ_ΕΡΓΟΛΑΒΙΚΩΝ_ΕΠΙΔΟΣΗΣ] IS NOT NULL AND --Cost is a critical variable, so it must be filled in

		 [ΚΟΣΤΟΣ_ΕΡΓΑΤΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] > 0 AND --A negative cost points to a dismantlement, which are irrelevant projects
		 [ΚΟΣΤΟΣ_ΥΛΙΚΩΝ_ΚΑΤΑΣΚΕΥΗΣ] > 0 AND --A negative cost points to a dismantlement, which are irrelevant projects
		 [ΚΟΣΤΟΣ_ΚΑΤΑΣΚΕΥΗΣ] > 0 AND --A negative cost points to a dismantlement, which are irrelevant projects
		 [ΚΟΣΤΟΣ_ΕΡΓΟΛΑΒΙΚΩΝ_ΕΠΙΔΟΣΗΣ] > 0 AND --A negative cost points to a dismantlement, which are irrelevant projects

		 [MONADA] IS NOT NULL AND
		 [ΚΑΤΗΓΟΡΙΑ] IS NOT NULL AND
		 [ΗΜΕΡΕΣ_ΜΕΛΕΤΗΣ] IS NOT NULL AND
		 [ΣΥΝΕΡΓΕΙΟ_ΜΕΛΕΤΗΣ] IS NOT NULL AND
		 [ΕΚΤΑΣΗ_ΕΡΓΟΥ] IS NOT NULL AND
		 [ΑΝΑΓΚΗ_ΥΣ] IS NOT NULL AND

		 [ΗΜΕΡ_ΑΙΤΗΣΗΣ] IS NOT NULL AND
		 [ΗΜΕΡ_ΚΑΤΑΧΩΡΗΣΗΣ] IS NOT NULL AND
		 [ΗΜΕΡ_ΑΝΑΓΓΕΛΙΑΣ] IS NOT NULL AND
		 [ΗΜΕΡ_ΜΕΛΕΤΗΣ] IS NOT NULL --AND

		 --[SAP_ΤΥΠΟΣ_ΠΕΛΑΤΗ] IS NOT NULL AND
		 --[SAP_ΕΙΔΟΣ_ΑΙΤΗΜΑΤΟΣ] IS NOT NULL --AND
		 )