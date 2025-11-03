ALTER TABLE dbo.CasemixCoveredDetail ADD
	IsUsingGlobalSetting BIT NULL,
	
	QtyIpr NUMERIC(18,2) NULL,
	QtyOpr NUMERIC(18,2) NULL,
	QtyEmr NUMERIC(18,2) NULL,
	
	IsNeedCasemixValidateIpr BIT NULL,
	IsAllowedToOrderIpr BIT NULL,
	IsNeedCasemixValidateOpr BIT NULL,
	IsAllowedToOrderOpr BIT NULL,
	IsNeedCasemixValidateEmr BIT NULL,
	IsAllowedToOrderEmr BIT NULL
GO

UPDATE CasemixCoveredDetail SET 
	IsUsingGlobalSetting = 1,
	QtyIpr = Qty,
	QtyOpr = Qty,
	QtyEmr = Qty,
	IsNeedCasemixValidateIpr = IsNeedCasemixValidate,
	IsAllowedToOrderIpr = IsAllowedToOrder,
	IsNeedCasemixValidateOpr = IsNeedCasemixValidate,
	IsAllowedToOrderOpr = IsAllowedToOrder,
	IsNeedCasemixValidateEmr = IsNeedCasemixValidate,
	IsAllowedToOrderEmr = IsAllowedToOrder
GO
