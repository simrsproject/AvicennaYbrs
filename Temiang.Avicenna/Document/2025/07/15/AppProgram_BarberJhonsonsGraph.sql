
INSERT INTO [dbo].[AppProgram]
           ([ProgramID]
           ,[ParentProgramID]
           ,[ProgramName]
           ,[TopLevelProgramID]
           ,[RootLevel]
           ,[RowIndex]
           ,[Note]
           ,[IsParentProgram]
           ,[IsProgram]
           ,[IsBeginGroup]
           ,[ProgramType]
           ,[IsProgramAddAble]
           ,[IsProgramEditAble]
           ,[IsProgramDeleteAble]
           ,[IsProgramViewAble]
           ,[IsProgramApprovalAble]
           ,[IsProgramUnApprovalAble]
           ,[IsProgramVoidAble]
           ,[IsProgramUnVoidAble]
           ,[IsProgramDirectVoid]
           ,[IsProgramPrintAble]
           ,[IsMenuAddVisible]
           ,[IsMenuHomeVisible]
           ,[IsVisible]
           ,[IsDiscontinue]
           ,[NavigateUrl]
           ,[HelpLinkID]
           ,[AssemblyName]
           ,[AssemblyClassName]
           ,[StoreProcedureName]
           ,[AccessKey]
           ,[IsUsingReportHeader]
           ,[IsDirectPrintEnable]
           ,[IsListLoadRecordOnInit]
           ,[IsListLoadRecordIfFiltered]
           ,[IsProgramRedirected]
           ,[ApplicationID]
           ,[ZplCommandTemplate]
           ,[IsProgramExportAble]
           ,[IsProgramCrossUnitAble]
           ,[IsProgramPowerUserAble])
     VALUES
           ('16.02.05'--<ProgramID, varchar(30),>
           ,'16'--<ParentProgramID, varchar(30),>
           ,'Barber Johnsons Graph'--<ProgramName, varchar(100),>
           ,'16'--<TopLevelProgramID, varchar(20),>
           ,0--<RootLevel, tinyint,>
           ,16025--<RowIndex, smallint,>
           ,''--<Note, varchar(1000),>
           ,0--<IsParentProgram, bit,>
           ,1--<IsProgram, bit,>
           ,0--<IsBeginGroup, bit,>
           ,'LNK'--<ProgramType, varchar(5),>
           ,0--<IsProgramAddAble, bit,>
           ,0--<IsProgramEditAble, bit,>
           ,0--<IsProgramDeleteAble, bit,>
           ,1--<IsProgramViewAble, bit,>
           ,0--<IsProgramApprovalAble, bit,>
           ,0--<IsProgramUnApprovalAble, bit,>
           ,0--<IsProgramVoidAble, bit,>
           ,0--<IsProgramUnVoidAble, bit,>
           ,0--<IsProgramDirectVoid, bit,>
           ,0--<IsProgramPrintAble, bit,>
           ,0--<IsMenuAddVisible, bit,>
           ,0--<IsMenuHomeVisible, bit,>
           ,1--<IsVisible, bit,>
           ,0--<IsDiscontinue, bit,>
           ,''--<NavigateUrl, varchar(1000),>
           ,NULL--<HelpLinkID, varchar(255),>
           ,'LNK'--<AssemblyName, varchar(50),>
           ,'fas fa-chart-bar nav-icon text-primary'--<AssemblyClassName, varchar(200),>
           ,'javascript: LoadPageBarberJGraph(''{0}'');'--<StoreProcedureName, varchar(200),>
           ,'1'--<AccessKey, char(1),>
           ,NULL--<IsUsingReportHeader, bit,>
           ,0--<IsDirectPrintEnable, bit,>
           ,0--<IsListLoadRecordOnInit, bit,>
           ,0--<IsListLoadRecordIfFiltered, bit,>
           ,0--<IsProgramRedirected, bit,>
           ,'HIS2015'--<ApplicationID, varchar(50),>
           ,NULL--<ZplCommandTemplate, varchar(max),>
           ,0--<IsProgramExportAble, bit,>
           ,0--<IsProgramCrossUnitAble, bit,>
           ,0--<IsProgramPowerUserAble, bit,>
		   )
GO
