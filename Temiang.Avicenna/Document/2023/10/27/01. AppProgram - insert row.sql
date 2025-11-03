IF EXISTS (SELECT TOP 1 0 FROM AppProgram AS ap WHERE ap.TopLevelProgramID = '07' AND ap.ProgramType = 'PRG' AND ap.IsVisible = 1)
BEGIN
	INSERT INTO AppProgram
	(ProgramID,ParentProgramID,ProgramName,TopLevelProgramID,RootLevel,RowIndex,Note,IsParentProgram,IsProgram,IsBeginGroup,ProgramType,IsProgramAddAble,
		IsProgramEditAble,IsProgramDeleteAble,IsProgramViewAble,IsProgramApprovalAble,IsProgramUnApprovalAble,IsProgramVoidAble,IsProgramUnVoidAble,
		IsProgramDirectVoid,IsProgramPrintAble,IsMenuAddVisible,IsMenuHomeVisible,IsVisible,IsDiscontinue,NavigateUrl,HelpLinkID,AssemblyName,
		AssemblyClassName,StoreProcedureName,AccessKey,IsUsingReportHeader,IsDirectPrintEnable,IsListLoadRecordOnInit,IsListLoadRecordIfFiltered,
		IsProgramRedirected,ApplicationID,ZplCommandTemplate,IsProgramExportAble,IsProgramCrossUnitAble,IsProgramPowerUserAble,SRProgramCategory)
	SELECT '16.05' ProgramID,ParentProgramID,'Human Resources'ProgramName,TopLevelProgramID,RootLevel,16050 RowIndex,Note,IsParentProgram,IsProgram,IsBeginGroup,ProgramType,IsProgramAddAble,
		IsProgramEditAble,IsProgramDeleteAble,IsProgramViewAble,IsProgramApprovalAble,IsProgramUnApprovalAble,IsProgramVoidAble,IsProgramUnVoidAble,
		IsProgramDirectVoid,IsProgramPrintAble,IsMenuAddVisible,IsMenuHomeVisible,IsVisible,IsDiscontinue,NavigateUrl,HelpLinkID,AssemblyName,
		'fas fa-users nav-icon text-primary' AssemblyClassName,REPLACE(StoreProcedureName, '16_04', '16_05') ,AccessKey,IsUsingReportHeader,IsDirectPrintEnable,IsListLoadRecordOnInit,IsListLoadRecordIfFiltered,
		IsProgramRedirected,ApplicationID,ZplCommandTemplate,IsProgramExportAble,IsProgramCrossUnitAble,IsProgramPowerUserAble,SRProgramCategory 
	FROM AppProgram AS ap WHERE ap.ProgramID = '16.04'

	INSERT INTO AppProgram
	(ProgramID,ParentProgramID,ProgramName,TopLevelProgramID,RootLevel,RowIndex,Note,IsParentProgram,IsProgram,IsBeginGroup,ProgramType,IsProgramAddAble,
		IsProgramEditAble,IsProgramDeleteAble,IsProgramViewAble,IsProgramApprovalAble,IsProgramUnApprovalAble,IsProgramVoidAble,IsProgramUnVoidAble,
		IsProgramDirectVoid,IsProgramPrintAble,IsMenuAddVisible,IsMenuHomeVisible,IsVisible,IsDiscontinue,NavigateUrl,HelpLinkID,AssemblyName,
		AssemblyClassName,StoreProcedureName,AccessKey,IsUsingReportHeader,IsDirectPrintEnable,IsListLoadRecordOnInit,IsListLoadRecordIfFiltered,
		IsProgramRedirected,ApplicationID,ZplCommandTemplate,IsProgramExportAble,IsProgramCrossUnitAble,IsProgramPowerUserAble,SRProgramCategory)
	VALUES
	('16.05.01','16.05','Employee By Employment','16',0,16051,'',0,1,0,'LNK',0,0,0,0,0,0,0,0,0,0,0,0,1,0,'',NULL,'LNK','col-sm-12 col-md-6 col-lg-4 text-center',
		NULL,1,NULL,0,1,0,NULL,'HIS2015',NULL,0,0,0,NULL),
	('16.05.02','16.05','Employee By Type','16',0,16052,'',0,1,0,'LNK',0,0,0,0,0,0,0,0,0,0,0,0,1,0,'',NULL,'LNK','col-sm-12 col-md-6 col-lg-4 text-center',
		NULL,1,NULL,0,1,0,NULL,'HIS2015',NULL,0,0,0,NULL),
	('16.05.03','16.05','Employee By Age','16',0,16053,'',0,1,0,'LNK',0,0,0,0,0,0,0,0,0,0,0,0,1,0,'',NULL,'LNK','col-sm-12 col-md-6 col-lg-4 text-center',
		NULL,1,NULL,0,1,0,NULL,'HIS2015',NULL,0,0,0,NULL),
	('16.05.04','16.05','License','16',0,16054,'',0,1,0,'LNK',0,0,0,0,0,0,0,0,0,0,0,0,1,0,'',NULL,'LNK','col-sm-12 col-md-6 col-lg-4 text-center',
		NULL,1,NULL,0,1,0,NULL,'HIS2015',NULL,0,0,0,NULL),
	('16.05.05','16.05','Employee By Type of Labor','16',0,16055,'',0,1,0,'LNK',0,0,0,0,0,0,0,0,0,0,0,0,1,0,'',NULL,'LNK','col-sm-12 col-md-6 col-lg-4 text-center',
		NULL,1,NULL,0,1,0,NULL,'HIS2015',NULL,0,0,0,NULL),
	('16.05.06','16.05','Employee By Division','16',0,16056,'',0,1,0,'LNK',0,0,0,0,0,0,0,0,0,0,0,0,1,0,'',NULL,'LNK','col-sm-12 col-md-6 col-lg-4 text-center',
		NULL,1,NULL,0,1,0,NULL,'HIS2015',NULL,0,0,0,NULL)
		
	INSERT INTO AppUserGroupProgram
	(
		UserGroupID,
		ProgramID,
		IsUserGroupAddAble,
		IsUserGroupEditAble,
		IsUserGroupDeleteAble,
		IsUserGroupApprovalAble,
		IsUserGroupUnApprovalAble,
		IsUserGroupVoidAble,
		IsUserGroupUnVoidAble,
		IsUserGroupExportAble,
		IsUserGroupCrossUnitAble,
		IsUserGroupPowerUserAble,
		LastUpdateDateTime,
		LastUpdateByUserID
	)
	SELECT 'ADMIN' UserGroupID,
		ap.ProgramID,
		0 IsUserGroupAddAble,
		0 IsUserGroupEditAble,
		0 IsUserGroupDeleteAble,
		0 IsUserGroupApprovalAble,
		0 IsUserGroupUnApprovalAble,
		0 IsUserGroupVoidAble,
		0 IsUserGroupUnVoidAble,
		0 IsUserGroupExportAble,
		0 IsUserGroupCrossUnitAble,
		0 IsUserGroupPowerUserAble,
		GETDATE() LastUpdateDateTime,
		'sci' LastUpdateByUserID 
	FROM AppProgram AS ap WHERE ap.ProgramID LIKE '16.05.%'
END

