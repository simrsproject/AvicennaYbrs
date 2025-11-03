/*
===============================================================================
                    Temiang.Dal(TM) 2008 by Temiang.Dal, LLC
             Persistence Layer and Business Objects for Microsoft .NET  
                          http://www.entityspaces.net
===============================================================================
                       Temiang.Dal Version # 2008.1.1110.0
                       CodeSmith Version    # 4.1.4.3592
                       Date Generated       : 01/12/2008 10:15:07
===============================================================================
*/

using System;
using System.Linq;
using System.Collections.Generic;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class IntermBillCollection
    {
        public void GetIbByNoRegWithMergeBilling(string[] registrationNoList) {
            var ibQ = new IntermBillQuery("ibq");
            ibQ.Where(ibQ.RegistrationNo.In(registrationNoList), ibQ.IsVoid == false);
            this.Load(ibQ);
        }
    }
}