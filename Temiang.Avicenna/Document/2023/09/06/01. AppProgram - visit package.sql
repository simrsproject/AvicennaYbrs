INSERT INTO AppProgram
(ProgramID,ParentProgramID,ProgramName,TopLevelProgramID,RootLevel,RowIndex,Note,IsParentProgram,IsProgram,IsBeginGroup,ProgramType,IsProgramAddAble,
	IsProgramEditAble,IsProgramDeleteAble,IsProgramViewAble,IsProgramApprovalAble,IsProgramUnApprovalAble,IsProgramVoidAble,IsProgramUnVoidAble,
	IsProgramDirectVoid,IsProgramPrintAble,IsMenuAddVisible,IsMenuHomeVisible,IsVisible,IsDiscontinue,NavigateUrl,HelpLinkID,AssemblyName,
	AssemblyClassName,StoreProcedureName,AccessKey,IsUsingReportHeader,IsDirectPrintEnable,IsListLoadRecordOnInit,IsListLoadRecordIfFiltered,
	IsProgramRedirected,ApplicationID,ZplCommandTemplate,IsProgramExportAble,IsProgramCrossUnitAble,IsProgramPowerUserAble,SRProgramCategory)
VALUES
('05.09.40','05.09C','Item Visit Package Template','05','3',5922,'',0,1,0,'PRG',1,1,0,1,0,0,0,0,0,0,1,0,1,0,
'~/Module/Finance/Master/VisitPackage/VisitPackageList.aspx',NULL,NULL,NULL,NULL,NULL,NULL,1,1,0,NULL,'HIS2015',NULL,0,0,0,NULL)

INSERT INTO AppUserGroupProgram
(UserGroupID,ProgramID,IsUserGroupAddAble,IsUserGroupEditAble,IsUserGroupDeleteAble,IsUserGroupApprovalAble,IsUserGroupUnApprovalAble,IsUserGroupVoidAble,
	IsUserGroupUnVoidAble,IsUserGroupExportAble,IsUserGroupCrossUnitAble,IsUserGroupPowerUserAble,LastUpdateDateTime,LastUpdateByUserID)
VALUES
('ADMIN','05.09.40',1,1,0,0,0,0,0,0,0,0,GETDATE(),'sci')