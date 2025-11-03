CREATE TABLE [dbo].[NccIDRG]
(
    [RegistrationNo]        varchar(20)  NOT NULL,
    [MedicalNo]             varchar(20)  NULL,
    [SEP]                   varchar(20)  NULL, 
    [ClaimDataRequest]      varchar(MAX) NULL,
    [ClaimDataResponse]     varchar(MAX) NULL,
    [IdrgDiagnosaSetReq]    varchar(MAX) NULL,
    [IdrgDiagnosaSetRes]    varchar(MAX) NULL,
    [IdrgDiagnosaGetReq]    varchar(MAX) NULL,
    [IdrgDiagnosaGetRes]    varchar(MAX) NULL,
    [IdrgProcedureSetReq]   varchar(MAX) NULL,
    [IdrgProcedureSetRes]   varchar(MAX) NULL,
    [IdrgProcedureGetReq]   varchar(MAX) NULL,
    [IdrgProcedureGetRes]   varchar(MAX) NULL,
    [GroupingIdrgReq]       varchar(MAX) NULL,
    [GroupingIdrgRes]       varchar(MAX) NULL,
    [FinalIdrgReq]          varchar(MAX) NULL,
    [FinalIdrgRes]          varchar(MAX) NULL,
    [ReEditIdrgReq]         varchar(MAX) NULL,
    [ReEditIdrgRes]         varchar(MAX) NULL,
    [IdrgToInacbgImportReq] varchar(MAX) NULL,
    [IdrgToInacbgImportRes] varchar(MAX) NULL,
    [InacbgDiagnosaGetReq]  varchar(MAX) NULL,
    [InacbgDiagnosaGetRes]  varchar(MAX) NULL,
    [InacbgDiagnosaSetReq]  varchar(MAX) NULL,
    [InacbgDiagnosaSetRes]  varchar(MAX) NULL,
    [InacbgProcedureSetReq] varchar(MAX) NULL,
    [InacbgProcedureSetRes] varchar(MAX) NULL,
    [InacbgProcedureGetReq] varchar(MAX) NULL,
    [InacbgProcedureGetRes] varchar(MAX) NULL,
    [GroupingInacbgStage1Req] varchar(MAX) NULL,
    [GroupingInacbgStage1Res] varchar(MAX) NULL,
    [GroupingInacbgStage2Req] varchar(MAX) NULL,
    [GroupingInacbgStage2Res] varchar(MAX) NULL,
    [FinalInacbgReq]        varchar(MAX) NULL,
    [FinalInacbgRes]        varchar(MAX) NULL,
    [ReEditInacbgReq]       varchar(MAX) NULL,
    [ReEditInacbgRes]       varchar(MAX) NULL,
    [ClaimFinalReq]         varchar(MAX) NULL,
    [ClaimFinalRes]         varchar(MAX) NULL,
    [ClaimReEditReq]        varchar(MAX) NULL,
    [ClaimReEditRes]        varchar(MAX) NULL,
    [ClaimSendReq]          varchar(MAX) NULL,
    [ClaimSendRes]          varchar(MAX) NULL,
    [GetClaimDataReq]       varchar(MAX) NULL,
    [GetClaimDataRes]       varchar(MAX) NULL,
    [LastUpdateDateTime]    datetime     NULL,
    [LastUpdateByUserID]    varchar(10)  NULL,

    CONSTRAINT [PK_NccIDRG] PRIMARY KEY CLUSTERED ([RegistrationNo] ASC)
);
GO

CREATE NONCLUSTERED INDEX IX_NccIDRG_MedicalNo ON dbo.NccIDRG (MedicalNo);
CREATE NONCLUSTERED INDEX IX_NccIDRG_SEP       ON dbo.NccIDRG (SEP);
GO

