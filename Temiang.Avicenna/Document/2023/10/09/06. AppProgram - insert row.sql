DECLARE @hc VARCHAR(20) = (SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'HealthcareInitialAppsVersion')

IF (@hc = 'RSSTJ')
BEGIN
	INSERT INTO AppProgram
	(ProgramID,ParentProgramID,ProgramName,TopLevelProgramID,RootLevel,RowIndex,Note,IsParentProgram,IsProgram,IsBeginGroup,ProgramType,
		IsProgramAddAble,IsProgramEditAble,IsProgramDeleteAble,IsProgramViewAble,IsProgramApprovalAble,IsProgramUnApprovalAble,IsProgramVoidAble,
		IsProgramUnVoidAble,IsProgramDirectVoid,IsProgramPrintAble,IsMenuAddVisible,IsMenuHomeVisible,IsVisible,IsDiscontinue,
		NavigateUrl,HelpLinkID,AssemblyName,AssemblyClassName,StoreProcedureName,AccessKey,IsUsingReportHeader,IsDirectPrintEnable,IsListLoadRecordOnInit,
		IsListLoadRecordIfFiltered,IsProgramRedirected,ApplicationID,ZplCommandTemplate,IsProgramExportAble,IsProgramCrossUnitAble,IsProgramPowerUserAble,
		SRProgramCategory)
	VALUES
	('05.09.41','05.09C','Item Service Procedure','05',3,5922,'',0,1,0,'PRG',1,1,0,1,0,0,0,0,0,0,1,0,1,0,
	'~/Module/Finance/Master/ItemServiceProcedure/ItemServiceProcedureList.aspx',NULL,NULL,NULL,NULL,NULL,NULL,1,1,0,NULL,'HIS2015',NULL,0,0,0,NULL)
	
	INSERT INTO AppUserGroupProgram
	(UserGroupID,ProgramID,IsUserGroupAddAble,IsUserGroupEditAble,IsUserGroupDeleteAble,IsUserGroupApprovalAble,IsUserGroupUnApprovalAble,IsUserGroupVoidAble,
		IsUserGroupUnVoidAble,IsUserGroupExportAble,IsUserGroupCrossUnitAble,IsUserGroupPowerUserAble,LastUpdateDateTime,LastUpdateByUserID)
	VALUES
	('ADMIN','05.09.41',1,1,0,0,0,0,0,0,0,0,GETDATE(),'sci')
END
ELSE
BEGIN
	INSERT INTO AppProgram
	(ProgramID,ParentProgramID,ProgramName,TopLevelProgramID,RootLevel,RowIndex,Note,IsParentProgram,IsProgram,IsBeginGroup,ProgramType,
		IsProgramAddAble,IsProgramEditAble,IsProgramDeleteAble,IsProgramViewAble,IsProgramApprovalAble,IsProgramUnApprovalAble,IsProgramVoidAble,
		IsProgramUnVoidAble,IsProgramDirectVoid,IsProgramPrintAble,IsMenuAddVisible,IsMenuHomeVisible,IsVisible,IsDiscontinue,
		NavigateUrl,HelpLinkID,AssemblyName,AssemblyClassName,StoreProcedureName,AccessKey,IsUsingReportHeader,IsDirectPrintEnable,IsListLoadRecordOnInit,
		IsListLoadRecordIfFiltered,IsProgramRedirected,ApplicationID,ZplCommandTemplate,IsProgramExportAble,IsProgramCrossUnitAble,IsProgramPowerUserAble,
		SRProgramCategory)
	VALUES
	('05.09.41','05.09C','Item Service Procedure','05',3,5922,'',0,1,0,'PRG',1,1,0,1,0,0,0,0,0,0,1,0,0,0,
	'~/Module/Finance/Master/ItemServiceProcedure/ItemServiceProcedureList.aspx',NULL,NULL,NULL,NULL,NULL,NULL,1,1,0,NULL,'HIS2015',NULL,0,0,0,NULL)	
END

