using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using System.Data;
using NCalc;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeTaxCalculationCollection
    {
        public bool DeleteByPaymentGroupNo(string PaymentGroupNo)
        {
            var pars = new esParameters();
            var pPayGroupNo = new esParameter("PaymentGroupNo", PaymentGroupNo);
            pars.Add(pPayGroupNo);

            return this.Load(esQueryType.Text, "Delete From ParamedicFeeTaxCalculation where PaymentGroupNo = @PaymentGroupNo", pars);
        }
    }
}
