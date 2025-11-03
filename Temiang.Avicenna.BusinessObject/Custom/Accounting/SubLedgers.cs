/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 3/3/2010 4:56:16 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class SubLedgers : esSubLedgers
	{
        public static SubLedgersCollection Get()
        {
            SubLedgersCollection coll = new SubLedgersCollection();
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static SubLedgersCollection GetByGroupId(int subLedgerGroupId)
        {
            SubLedgersCollection coll = new SubLedgersCollection();
            coll.Query.Where(coll.Query.GroupId == subLedgerGroupId);
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static SubLedgersCollection GetByGroupId(int subLedgerGroupId, string filter)
        {
            SubLedgersCollection coll = new SubLedgersCollection();
            coll.Query.Where(coll.Query.GroupId == subLedgerGroupId);
            if (!string.IsNullOrEmpty(filter))
            {
                string searchTextContain = string.Format("%{0}%", filter);
                coll.Query.Where(coll.Query.Description.Like(searchTextContain));
                //coll.Query.Where(coll.Query.Description.Like("%" + filter + "%"));
            }
                
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static SubLedgers Get(int subLedgerGroupId, int subLedgerId)
        {
            SubLedgers entity = new SubLedgers();
            entity.Query.Where(entity.Query.GroupId == subLedgerGroupId, entity.Query.SubLedgerId == subLedgerId);
            entity.Query.es.WithNoLock = true;
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }

        public static SubLedgers Get(int subLedgerId)
        {
            SubLedgers entity = new SubLedgers();
            entity.Query.Where(entity.Query.SubLedgerId == subLedgerId);
            entity.Query.es.WithNoLock = true;
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }
	}
}
