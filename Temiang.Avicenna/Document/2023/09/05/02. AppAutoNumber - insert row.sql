INSERT INTO AppAutoNumber
(SRAutoNumber,EffectiveDate,Prefik,SeparatorAfterPrefik,IsUsedDepartment,SeparatorAfterDept,IsUsedYear,YearDigit,SeparatorAfterYear,IsUsedMonth,
	IsMonthInRomawi,SeparatorAfterMonth,IsUsedDay,SeparatorAfterDay,NumberLength,NumberGroupLength,NumberGroupSeparator,NumberFormat,
	SeparatorAfterNumber,IsUsedYearToDateOrder,LastUpdateDateTime,LastUpdateByUserID)
VALUES
('CssdStockOpnameNo','8/1/2023','SO','/','','',1,'4','',1,0,'',1,'-',4,0,0,'','',1,GETDATE(),'sci')
