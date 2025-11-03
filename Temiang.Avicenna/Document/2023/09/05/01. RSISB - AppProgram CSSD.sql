/*
* ATTENTION... !!!!
* script hanya dijalankan di rs indriati
*/

DECLARE @hc VARCHAR(20) = (SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'HealthcareInitialAppsVersion')
IF (@hc = 'RSISB')
BEGIN
	INSERT INTO AppProgram
	(ProgramID,ParentProgramID,ProgramName,TopLevelProgramID,RootLevel,RowIndex,Note,IsParentProgram,IsProgram,IsBeginGroup,ProgramType,IsProgramAddAble,IsProgramEditAble,
		IsProgramDeleteAble,IsProgramViewAble,IsProgramApprovalAble,IsProgramUnApprovalAble,IsProgramVoidAble,IsProgramUnVoidAble,IsProgramDirectVoid,
		IsProgramPrintAble,IsMenuAddVisible,IsMenuHomeVisible,IsVisible,IsDiscontinue,NavigateUrl,HelpLinkID,AssemblyName,AssemblyClassName,StoreProcedureName,
		AccessKey,IsUsingReportHeader,IsDirectPrintEnable,IsListLoadRecordOnInit,IsListLoadRecordIfFiltered,IsProgramRedirected,ApplicationID,
		ZplCommandTemplate,IsProgramExportAble,IsProgramCrossUnitAble,IsProgramPowerUserAble,SRProgramCategory)
	VALUES
	('14.06.12','14','Stock Opname','14','1',14069,'',0,1,0,'PRG',1,1,0,1,1,0,1,0,0,1,1,0,1,0,
	'~/Module/Cssd/Transaction/StockOpname/StockOpnameList.aspx',NULL,NULL,NULL,NULL,NULL,NULL,0,1,0,NULL,'HIS2015',NULL,0,0,0,NULL)

	INSERT INTO AppUserGroupProgram
	(UserGroupID,ProgramID,IsUserGroupAddAble,IsUserGroupEditAble,IsUserGroupDeleteAble,IsUserGroupApprovalAble,IsUserGroupUnApprovalAble,IsUserGroupVoidAble,
		IsUserGroupUnVoidAble,IsUserGroupExportAble,IsUserGroupCrossUnitAble,IsUserGroupPowerUserAble,LastUpdateDateTime,LastUpdateByUserID)
	VALUES
	('ADMIN','14.06.12',1,1,0,1,0,1,0,0,0,0,GETDATE(),'sci')
END

