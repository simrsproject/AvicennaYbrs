/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/27/2011 11:29:31 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class Bank : esBank
	{
	    public string AccountName
	    {
            get { return string.Format("{0} - {1}", this.BankName, this.NoRek); }
	    }
        public static Bank Get(string bankId)
        {
            var entity = new Bank();
            entity.Query.Where(entity.Query.BankID == bankId);
            if (entity.Query.Load())
            {
                return entity;
            }
            return null;
        }

        public static BankCollection Get()
        {
            var coll = new BankCollection();
            coll.Query.Where(coll.Query.IsActive == true);
            coll.Query.OrderBy(coll.Query.BankID.Ascending);
            coll.Query.es.WithNoLock = true;
            if (coll.Query.Load())
                return coll;

            return null;
        }

        public static BankCollection GetLike(string bankName)
        {
            string searchTextContain = string.Format("%{0}%", bankName);
            var coll = new BankCollection();
            coll.Query.Where(coll.Query.BankName.Like(searchTextContain), coll.Query.IsActive == true);
            coll.Query.OrderBy(coll.Query.BankID.Ascending);
            coll.Query.es.WithNoLock = true;
            if (coll.Query.Load())
                return coll;
            
            return null;
        }

	}
}
