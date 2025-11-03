/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 3/3/2010 4:56:15 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class ChartOfAccountsCollection : esChartOfAccountsCollection
	{
        public bool LoadByIdIncChilds(int coaid) {
            var coa = new ChartOfAccounts();
            if (coa.LoadByPrimaryKey(coaid))
            {
                this.AttachEntity(coa);

                var coaColl = new ChartOfAccountsCollection();
                if (coaColl.LoadChildById(coaid, true)) {
                    foreach (var c in coaColl) {
                        this.AttachEntity(c);
                    }
                }

                return this.Count() > 0;
            }
            return false;
        }

        public bool LoadChildById(int coaid, bool incSubChilds) {
            var c1 = new ChartOfAccountsQuery("c1");
            var c2 = new ChartOfAccountsQuery("c2");

            c1.InnerJoin(c2).On(c1.GeneralAccount == c2.ChartOfAccountCode)
                .Where(c2.ChartOfAccountId == coaid)
                .Select(c1);

            if (this.Load(c1))
            {
                if (incSubChilds) {
                    var ids = this.Where(c => !(c.IsDetail ?? true)).Select(c => c.ChartOfAccountId).ToArray();
                    foreach (var id in ids) {
                        var coa = this.Where(c => c.ChartOfAccountId == id).First();
                        if (!(coa.IsDetail ?? true)) {
                            var coaColl = new ChartOfAccountsCollection();
                            if (coaColl.LoadChildById(coa.ChartOfAccountId.Value, incSubChilds)) {
                                foreach (var cc in coaColl) {
                                    this.AttachEntity(cc);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            else {
                return false;
            }
        }
	}
}
