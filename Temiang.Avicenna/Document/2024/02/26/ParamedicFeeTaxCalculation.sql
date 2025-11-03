
alter table ParamedicFeeTaxCalculation add PeriodMonth smallint
GO

update p set p.PeriodMonth = MONTH(g.PaymentDate) 
from ParamedicFeeTaxCalculation as p
	inner join ParamedicFeePaymentGroup as g on p.PaymentGroupNo = g.PaymentGroupNo
where p.PeriodMonth is null 
GO
