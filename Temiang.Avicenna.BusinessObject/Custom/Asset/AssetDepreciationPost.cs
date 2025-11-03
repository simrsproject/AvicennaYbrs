/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/24/2011 11:56:42 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class AssetDepreciationPost : esAssetDepreciationPost
	{
        public static int TotalCount(string assetId)
        {
            if (string.IsNullOrEmpty(assetId))
                return 0;

            int retVal = 0;
            var entity = new AssetDepreciationPost();

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;

            entity.Query.Where(entity.Query.AssetID == assetId);
            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        public static AssetDepreciationPostCollection GetAllWithPaging(string assetId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(assetId))
                return new AssetDepreciationPostCollection();

            var q = new AssetDepreciationPostQuery("h");

            q.Select(q);
            q.Where(q.AssetID == assetId);
            q.OrderBy(q.PeriodNo.Ascending);

            q.es.PageSize = pageSize;
            q.es.PageNumber = pageNumber + 1;
            q.es.WithNoLock = true;

            var coll = new AssetDepreciationPostCollection();
            coll.Load(q);
            return coll;
        }

        public static int GenerateAssetDepreciation(string assetId, string editedBy)
        {
            var prms = new esParameters
                           {
                               {"AssetID", assetId, esParameterDirection.Input, DbType.String, 30},
                               {"LastUpdateByUserID", editedBy, esParameterDirection.Input, DbType.String, 40},
                               {"Return_Value", esParameterDirection.ReturnValue}
                           };

            var entity = new AssetDepreciationPost();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_AssetGenerateAssetDepreciationPost", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int GenerateDepreciationJournal(int postingId, bool chkIsPostingFinal, DateTime postingDate, string editedBy)
        {
            var isPosted = chkIsPostingFinal;
            var year = postingDate.Year.ToString();
            var month = (postingDate.Month >= 10 ? "" : "0") + postingDate.Month;
            
            AssetPostingStatus aps = null;
            if (postingId != 0)
            {
                aps = AssetPostingStatus.Get(postingId);
                if (aps.IsEnabled.Value)
                    return -3; // transaksi yang sudah di close tdk boleh diupdate lagi
            }

            var acc_AssetsStatusDepreciationJournal = "1,2,3";
            var app = new AppParameter();
            if (app.LoadByPrimaryKey("acc_AssetsStatusDepreciationJournal"))
                acc_AssetsStatusDepreciationJournal = app.ParameterValue;
            string[] acc_AssetsStatusDepreciationJournal_arr = acc_AssetsStatusDepreciationJournal.Split(',');

            var q = new AssetDepreciationPostViewQuery("v");
            var a = new AssetQuery("a");
            var b = new ChartOfAccountsQuery("b");
            var b2 = new ChartOfAccountsQuery("b2");

            q.InnerJoin(a).On(a.AssetID == q.AssetID);
            q.InnerJoin(b).On(b.ChartOfAccountId == q.AssetCostAccountId);
            q.InnerJoin(b2).On(b2.ChartOfAccountId == q.AssetAccumulationAccountId);

            q.Select(q);
            q.Where(q.Year == year, q.Month == month, a.SRAssetsStatus.In(acc_AssetsStatusDepreciationJournal_arr));
            q.OrderBy(q.AssetGroupId.Descending);

            var en = new AssetDepreciationPostViewCollection();

            if (en.Load(q))
            {
                // we will create journal per asset group
                var groups = (from p in en select p.AssetGroupId).Distinct();
                var info = new List<JournalInfo>();

                foreach (var groupId in groups)
                {
                    var id = groupId;
                    var entities = en.Where(p => p.AssetGroupId == id);
                    
                    JournalTransactions header = null;
                    if (postingId == 0)
                    {
                        header = GenerateJournalHeader(postingDate, editedBy);
                    }

                    // ----------------------------------------------------------------
                    // prepare detail journal
                    // ----------------------------------------------------------------
                    var debits = new List<JournalTransactionDetails>();
                    var credits = new List<JournalTransactionDetails>();
                    var assetList = new List<AssetDepreciationPostView>();
                    foreach (var e in entities)
                    {
                        var cost = new JournalTransactionDetails
                                       {
                                           ChartOfAccountId = e.AssetCostAccountId,
                                           SubLedgerId = e.AssetCostSubLedgerId == 0 ? e.SubLedgerId : e.AssetCostSubLedgerId,
                                           Debit = e.DepreciationAmount,
                                           Credit = 0,
                                           Description = string.Format("Depreciation {0}. {1} - {2}", e.GroupName, e.AssetID, e.AssetName),
                                           DocumentNumber = string.Empty
                                       };
                        debits.Add(cost);

                        var dep = new JournalTransactionDetails
                        {
                            ChartOfAccountId = e.AssetAccumulationAccountId,
                            SubLedgerId = e.AssetAccumulationSubLedgerId == 0 ? e.SubLedgerId : e.AssetAccumulationSubLedgerId,
                            Debit = 0,
                            Credit = e.DepreciationAmount,
                            Description = string.Format("Depreciation {0}. {1} - {2}", e.GroupName, e.AssetID, e.AssetName),
                            DocumentNumber = string.Empty
                        };
                        credits.Add(dep);
                        // list of asset depreciation, we'll have to update journal id for each asset later
                        assetList.Add(e);

                        if (header == null)
                        {
                            // use existing journal header, but if we cant find it create new one
                            header = JournalTransactions.Get(e.JournalId ?? 0) ?? GenerateJournalHeader(postingDate, editedBy);
                        }
                        // we need to show proper description
                        if (string.IsNullOrEmpty(header.Description))
                        {
                            header.Description = string.Format("Depreciation {0} {1}-{2}", e.GroupName, year, month);
                        }
                    }
                    // sort!
                    var details = debits.ToList();
                    details.AddRange(credits);
                    // ----------------------------------------------------------------
                    // end detail journal
                    // ----------------------------------------------------------------

                    info.Add(new JournalInfo { Header = header, Details = details, AssetList = assetList });
                }

                // ----------------------------------------------------------------
                // shoot in one go!
                // ----------------------------------------------------------------
                var utility = new esUtility();
                using (var trans = new esTransactionScope())
                {
                    // update posting status
                    if (postingId == 0)
                    {
                        aps = new AssetPostingStatus
                        {
                            Month = month,
                            Year = year,
                            IsEnabled = isPosted,
                            CreatedBy = editedBy,
                            LastUpdateByUserID = editedBy,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now
                        };
                        aps.AddNew();
                        aps.Save();
                    }
                    else
                    {
                        aps.IsEnabled = isPosted;
                        aps.LastUpdateDateTime = DateTime.Now;
                        aps.LastUpdateByUserID = editedBy;
                        aps.Save();
                    }

                    foreach (var j in info)
                    {
                        // create header jurnal
                        var header = j.Header;
                        if (header.es.IsAdded)
                        {
                            header.AddNew();
                        }
                        else
                        {
                            // kalo ud ada detilnya di hapus dulu lewat SP
                            if (header.JournalId.HasValue && header.JournalId.Value > 0)
                            {
                                var prms = new esParameters
                                           {
                                               {"JournalId", header.JournalId, esParameterDirection.Input, DbType.Int32, 0}
                                           };
                                utility.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_JournalTransactionsDeleteDetail", prms);    
                            }
                        }
                        header.IsPosted = isPosted;
                        header.Save();

                        // create detil jurnal, disini harusnya posisinya sudah urut debet/credit
                        foreach (var detail in j.Details)
                        {
                            detail.JournalId = header.JournalId;
                            detail.AddNew();
                            detail.Save();
                        }

                        // update depreciation post
                        foreach (var post in j.AssetList)
                        {
                            var prms = new esParameters
                               {
                                   {"AssetDepreciationPostId", post.AssetDepreciationPostId, esParameterDirection.Input, DbType.String, 30},
                                   {"PostingId", aps.PostingId, esParameterDirection.Input, DbType.Int32, 0},
                                   {"IsEnabled", aps.IsEnabled, esParameterDirection.Input, DbType.Boolean, 0}, 
                                   {"JournalId", header.JournalId, esParameterDirection.Input, DbType.Int32, 0},
                                   {"JournalNumber", string.Format("{0}-{1}", header.JournalCode, header.TransactionNumber), esParameterDirection.Input, DbType.String, 20},
                                   {"PostedDate", header.TransactionDate, esParameterDirection.Input, DbType.DateTime, 0},
                                   {"LastUpdateByUserID", editedBy, esParameterDirection.Input, DbType.String, 40}
                               };

                            utility.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_AssetUpdateDepreciationPostStatus", prms);
                        }
                    }

                    trans.Complete();
                }
            }
            return 0;
        }

	    private static JournalTransactions GenerateJournalHeader(DateTime postingDate, string editedBy)
	    {
	        JournalTransactions header;
	        var journalCode = JournalCodes.GetOrCreateAutoJournalCode("FA", postingDate);
	        var journalNumber = JournalCodes.GenerateAndIncrementAutoNumber(journalCode);

	        header = new JournalTransactions
	                     {
	                         JournalType = JournalType.FixAsset,
	                         JournalCode = journalCode,
	                         TransactionNumber = journalNumber,
	                         TransactionDate = postingDate,
	                         IsPosted = false,
	                         DateCreated = DateTime.Now,
	                         LastUpdateDateTime = DateTime.Now,
	                         CreatedBy = editedBy,
	                         LastUpdateByUserID = editedBy,
	                         RefferenceNumber = string.Empty
	                     };
	        return header;
	    }

	    private class JournalInfo
	    {
	        public JournalTransactions Header;
	        public List<JournalTransactionDetails> Details;
            public List<AssetDepreciationPostView> AssetList;
	    }
	}
}