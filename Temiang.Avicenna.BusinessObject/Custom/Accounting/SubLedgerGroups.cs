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
	public partial class SubLedgerGroups : esSubLedgerGroups
	{
        public static SubLedgerGroupsCollection Get()
        {
            SubLedgerGroupsCollection coll = new SubLedgerGroupsCollection();
            coll.Query.Load();
            return coll;
        }

        public static SubLedgerGroups Get(int subLedgerGroupId)
        {
            SubLedgerGroups entity = new SubLedgerGroups();
            entity.Query.Where(entity.Query.SubLedgerGroupId == subLedgerGroupId);
            if (entity.Query.Load())
            {
                return entity;
            }
            return null;
        }

        public static SubLedgerGroups Get(string groupCode)
        {
            SubLedgerGroups entity = new SubLedgerGroups();
            entity.Query.Where(entity.Query.GroupCode == groupCode);
            if (entity.Query.Load())
            {
                return entity;
            }
            return null;
        }
	}
}
