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

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class JournalCodes : esJournalCodes
	{

        public static JournalCodes Get(int journalCodeId)
        {
            JournalCodes entity = new JournalCodes();
            entity.Query.Where(entity.Query.JournalCodeId == journalCodeId);
            if (entity.Query.Load())
            {
                return entity;
            }
            return null;
        }

        public static JournalCodes Get(string journalCode)
        {
            JournalCodes entity = new JournalCodes();
            entity.Query.Where(entity.Query.JournalCode == journalCode);
            if (entity.Query.Load())
            {
                return entity;
            }
            return null;
        }

        public static JournalCodesCollection GetLike(string journalCode, bool visibleOnly)
        {
            JournalCodesCollection coll = new JournalCodesCollection();
            coll.Query.Where(coll.Query.IsEnabled == true, coll.Query.JournalCode.Like(string.Format("{0}%", journalCode)));
            if (visibleOnly) coll.Query.Where(coll.Query.IsVisible == true);
            coll.Query.OrderBy(coll.Query.JournalCode.Ascending);
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static string GenerateAndIncrementAutoNumber(string journalCode)
        {
            string retVal = string.Empty;
            JournalCodes entity = new JournalCodes();
            entity.Query.Where(entity.Query.JournalCode == journalCode, entity.Query.IsEnabled == true);
            entity.Query.OrderBy(entity.Query.JournalCodeId.Ascending);
            entity.Query.es.Top = 1; // ada bugs kode yang sama tercreate lebih dari 1, ambil kode yang pertama saja
            if (entity.Query.Load())
            {
                int seed = entity.NumberSeed.Value;
                int current = entity.CurrentNumber.Value;
                string numberFormat = entity.NumberFormat;
                int newNumber = current + seed;

                entity.CurrentNumber = newNumber;
                entity.Save();
                retVal = String.Format(numberFormat, newNumber);

            }
            return retVal;
        }

        public static JournalCodesCollection GetAllWithPaging(int pageNumber, int pageSize, string sortString)
        {
            JournalCodesCollection coll = new JournalCodesCollection();
            List<esComparison> prms = new List<esComparison>();

            coll.Query.OrderBy(coll.Query.JournalCode.Ascending);
            if (prms.Count > 0)
                coll.Query.Where(prms.ToArray());

            coll.Query.es.PageSize = pageSize;
            coll.Query.es.PageNumber = pageNumber + 1;
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static int TotalCount()
        {
            int retVal = 0;
            JournalCodes entity = new JournalCodes();
            List<esComparison> prms = new List<esComparison>();

            //prms.Add();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static string GetOrCreateAutoJournalCode(string prefix, DateTime trDate)
        {
            string year = trDate.Year.ToString().Substring(2, 2);
            string month = trDate.ToString("MM") ;
            string day = trDate.ToString("dd");
            string journalCode = string.Format("{0}{1}{2}{3}", prefix, year, month, day);
            if(prefix.Length >= 3)
                if(prefix.Substring(0, 3).Equals("CI.") || prefix.Substring(0, 3).Equals("CO.")) /*Cash In / Cash Out*/  
                    journalCode = string.Format("{0}{1}{2}", prefix, year, month);

            JournalCodes entity = entity = new JournalCodes();
            entity.Query.Where(entity.Query.JournalCode == journalCode);
            entity.Query.OrderBy(entity.Query.JournalCodeId.Ascending);
            entity.Query.es.Top = 1; // ada bugs kode yang sama tercreate lebih dari 1, ambil kode yang pertama saja
            entity.query.es.WithNoLock = true;
            if (entity.Query.Load())
                return entity.JournalCode;

            entity.AddNew();
            entity.JournalCode = journalCode;
            entity.Description = "JournalCode for Auto Journal Entry";
            entity.CurrentNumber = 0;
            entity.NumberFormat = "{0:00000}";
            entity.NumberSeed = 1;
            entity.IsEnabled = true;
            entity.IsAutoNumber = true;
            entity.Save();

            return entity.JournalCode;
        }

        public static string GetJournalCodeForCashEntry(string DK, string BankJournalCode) {
            return (DK.Equals("D") ? "CI" : "CO") + "." + BankJournalCode;
        }
	}
}
