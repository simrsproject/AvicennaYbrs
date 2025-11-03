/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/7/2021 3:02:26 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esAssetWorkOrderCollection : esEntityCollectionWAuditLog
	{
		public esAssetWorkOrderCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AssetWorkOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetWorkOrderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntityCollection)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			this.PopulateCollection(table);
			return (this.RowCount > 0) ? true : false;
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery(query as esAssetWorkOrderQuery);
		}
		#endregion

		virtual public AssetWorkOrder DetachEntity(AssetWorkOrder entity)
		{
			return base.DetachEntity(entity) as AssetWorkOrder;
		}

		virtual public AssetWorkOrder AttachEntity(AssetWorkOrder entity)
		{
			return base.AttachEntity(entity) as AssetWorkOrder;
		}

		virtual public void Combine(AssetWorkOrderCollection collection)
		{
			base.Combine(collection);
		}

		new public AssetWorkOrder this[int index]
		{
			get
			{
				return base[index] as AssetWorkOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetWorkOrder);
		}
	}

	[Serializable]
	abstract public class esAssetWorkOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetWorkOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetWorkOrder()
		{
		}

		public esAssetWorkOrder(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String orderNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
		}

		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
		}

		private bool LoadByPrimaryKeyDynamic(String orderNo)
		{
			esAssetWorkOrderQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String orderNo)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo", orderNo);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion

		#region Properties

		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}

		public override void SetProperty(string name, object value)
		{
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "OrderDate": this.str.OrderDate = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "AssetID": this.str.AssetID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "ProblemDescription": this.str.ProblemDescription = (string)value; break;
						case "SRWorkStatus": this.str.SRWorkStatus = (string)value; break;
						case "SRWorkType": this.str.SRWorkType = (string)value; break;
						case "SRWorkPriority": this.str.SRWorkPriority = (string)value; break;
						case "SRWorkTrade": this.str.SRWorkTrade = (string)value; break;
						case "RequiredDate": this.str.RequiredDate = (string)value; break;
						case "RequestByUserID": this.str.RequestByUserID = (string)value; break;
						case "ReceivedDateTime": this.str.ReceivedDateTime = (string)value; break;
						case "ReceivedByUserID": this.str.ReceivedByUserID = (string)value; break;
						case "SRFailureCode": this.str.SRFailureCode = (string)value; break;
						case "FailureCauseDescription": this.str.FailureCauseDescription = (string)value; break;
						case "ActionTaken": this.str.ActionTaken = (string)value; break;
						case "PreventionTaken": this.str.PreventionTaken = (string)value; break;
						case "CostEstimation": this.str.CostEstimation = (string)value; break;
						case "LastRealizationByUserID": this.str.LastRealizationByUserID = (string)value; break;
						case "LastRealizationDateTime": this.str.LastRealizationDateTime = (string)value; break;
						case "AcceptedByUserID": this.str.AcceptedByUserID = (string)value; break;
						case "AcceptedDateTime": this.str.AcceptedDateTime = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "IsProceed": this.str.IsProceed = (string)value; break;
						case "IsPreventiveMaintenance": this.str.IsPreventiveMaintenance = (string)value; break;
						case "PMNo": this.str.PMNo = (string)value; break;
						case "IsGeneratePrDr": this.str.IsGeneratePrDr = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ImplementedBy": this.str.ImplementedBy = (string)value; break;
						case "AcceptedBy": this.str.AcceptedBy = (string)value; break;
						case "SentToThirdPartiesByUserID": this.str.SentToThirdPartiesByUserID = (string)value; break;
						case "SentToThirdPartiesDateTime": this.str.SentToThirdPartiesDateTime = (string)value; break;
						case "ReceivedFromThirdPartiesByUserID": this.str.ReceivedFromThirdPartiesByUserID = (string)value; break;
						case "ReceivedFromThirdPartiesDateTime": this.str.ReceivedFromThirdPartiesDateTime = (string)value; break;
						case "ReceivedFromLogisticsByUserID": this.str.ReceivedFromLogisticsByUserID = (string)value; break;
						case "ReceivedFromLogisticsDateTime": this.str.ReceivedFromLogisticsDateTime = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "LetterNo": this.str.LetterNo = (string)value; break;
						case "SupplierID": this.str.SupplierID = (string)value; break;
						case "FirstResponseDateTime": this.str.FirstResponseDateTime = (string)value; break;
						case "FirstResponseByUserID": this.str.FirstResponseByUserID = (string)value; break;
						case "SRWorkTradeItem": this.str.SRWorkTradeItem = (string)value; break;
						case "IsSanitation": this.str.IsSanitation = (string)value; break;
						case "SRWorkOrderPoint": this.str.SRWorkOrderPoint = (string)value; break;
						case "WorkOrderPoint": this.str.WorkOrderPoint = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "OrderDate":

							if (value == null || value is System.DateTime)
								this.OrderDate = (System.DateTime?)value;
							break;
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "RequiredDate":

							if (value == null || value is System.DateTime)
								this.RequiredDate = (System.DateTime?)value;
							break;
						case "ReceivedDateTime":

							if (value == null || value is System.DateTime)
								this.ReceivedDateTime = (System.DateTime?)value;
							break;
						case "CostEstimation":

							if (value == null || value is System.Decimal)
								this.CostEstimation = (System.Decimal?)value;
							break;
						case "LastRealizationDateTime":

							if (value == null || value is System.DateTime)
								this.LastRealizationDateTime = (System.DateTime?)value;
							break;
						case "AcceptedDateTime":

							if (value == null || value is System.DateTime)
								this.AcceptedDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "IsProceed":

							if (value == null || value is System.Boolean)
								this.IsProceed = (System.Boolean?)value;
							break;
						case "IsPreventiveMaintenance":

							if (value == null || value is System.Boolean)
								this.IsPreventiveMaintenance = (System.Boolean?)value;
							break;
						case "IsGeneratePrDr":

							if (value == null || value is System.Boolean)
								this.IsGeneratePrDr = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "SentToThirdPartiesDateTime":

							if (value == null || value is System.DateTime)
								this.SentToThirdPartiesDateTime = (System.DateTime?)value;
							break;
						case "ReceivedFromThirdPartiesDateTime":

							if (value == null || value is System.DateTime)
								this.ReceivedFromThirdPartiesDateTime = (System.DateTime?)value;
							break;
						case "ReceivedFromLogisticsDateTime":

							if (value == null || value is System.DateTime)
								this.ReceivedFromLogisticsDateTime = (System.DateTime?)value;
							break;
						case "FirstResponseDateTime":

							if (value == null || value is System.DateTime)
								this.FirstResponseDateTime = (System.DateTime?)value;
							break;
						case "IsSanitation":

							if (value == null || value is System.Boolean)
								this.IsSanitation = (System.Boolean?)value;
							break;
						case "WorkOrderPoint":

							if (value == null || value is System.Decimal)
								this.WorkOrderPoint = (System.Decimal?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to AssetWorkOrder.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.OrderNo);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.OrderDate
		/// </summary>
		virtual public System.DateTime? OrderDate
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.OrderDate);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.OrderDate, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.AssetID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.AssetID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ProblemDescription
		/// </summary>
		virtual public System.String ProblemDescription
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ProblemDescription);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ProblemDescription, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SRWorkStatus
		/// </summary>
		virtual public System.String SRWorkStatus
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkStatus);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkStatus, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SRWorkType
		/// </summary>
		virtual public System.String SRWorkType
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkType);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkType, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SRWorkPriority
		/// </summary>
		virtual public System.String SRWorkPriority
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkPriority);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkPriority, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SRWorkTrade
		/// </summary>
		virtual public System.String SRWorkTrade
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkTrade);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkTrade, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.RequiredDate
		/// </summary>
		virtual public System.DateTime? RequiredDate
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.RequiredDate);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.RequiredDate, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.RequestByUserID
		/// </summary>
		virtual public System.String RequestByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.RequestByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.RequestByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ReceivedDateTime
		/// </summary>
		virtual public System.DateTime? ReceivedDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ReceivedDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ReceivedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ReceivedByUserID
		/// </summary>
		virtual public System.String ReceivedByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ReceivedByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ReceivedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SRFailureCode
		/// </summary>
		virtual public System.String SRFailureCode
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SRFailureCode);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SRFailureCode, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.FailureCauseDescription
		/// </summary>
		virtual public System.String FailureCauseDescription
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.FailureCauseDescription);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.FailureCauseDescription, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ActionTaken
		/// </summary>
		virtual public System.String ActionTaken
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ActionTaken);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ActionTaken, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.PreventionTaken
		/// </summary>
		virtual public System.String PreventionTaken
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.PreventionTaken);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.PreventionTaken, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.CostEstimation
		/// </summary>
		virtual public System.Decimal? CostEstimation
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderMetadata.ColumnNames.CostEstimation);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderMetadata.ColumnNames.CostEstimation, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.LastRealizationByUserID
		/// </summary>
		virtual public System.String LastRealizationByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.LastRealizationByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.LastRealizationByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.LastRealizationDateTime
		/// </summary>
		virtual public System.DateTime? LastRealizationDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.LastRealizationDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.LastRealizationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.AcceptedByUserID
		/// </summary>
		virtual public System.String AcceptedByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.AcceptedByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.AcceptedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.AcceptedDateTime
		/// </summary>
		virtual public System.DateTime? AcceptedDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.AcceptedDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.AcceptedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.IsProceed
		/// </summary>
		virtual public System.Boolean? IsProceed
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsProceed);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsProceed, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.IsPreventiveMaintenance
		/// </summary>
		virtual public System.Boolean? IsPreventiveMaintenance
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsPreventiveMaintenance);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsPreventiveMaintenance, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.PMNo
		/// </summary>
		virtual public System.String PMNo
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.PMNo);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.PMNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.IsGeneratePrDr
		/// </summary>
		virtual public System.Boolean? IsGeneratePrDr
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsGeneratePrDr);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsGeneratePrDr, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ImplementedBy
		/// </summary>
		virtual public System.String ImplementedBy
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ImplementedBy);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ImplementedBy, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.AcceptedBy
		/// </summary>
		virtual public System.String AcceptedBy
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.AcceptedBy);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.AcceptedBy, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SentToThirdPartiesByUserID
		/// </summary>
		virtual public System.String SentToThirdPartiesByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SentToThirdPartiesDateTime
		/// </summary>
		virtual public System.DateTime? SentToThirdPartiesDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ReceivedFromThirdPartiesByUserID
		/// </summary>
		virtual public System.String ReceivedFromThirdPartiesByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ReceivedFromThirdPartiesDateTime
		/// </summary>
		virtual public System.DateTime? ReceivedFromThirdPartiesDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ReceivedFromLogisticsByUserID
		/// </summary>
		virtual public System.String ReceivedFromLogisticsByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ReceivedFromLogisticsDateTime
		/// </summary>
		virtual public System.DateTime? ReceivedFromLogisticsDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.LetterNo
		/// </summary>
		virtual public System.String LetterNo
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.LetterNo);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.LetterNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SupplierID
		/// </summary>
		virtual public System.String SupplierID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SupplierID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SupplierID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.FirstResponseDateTime
		/// </summary>
		virtual public System.DateTime? FirstResponseDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.FirstResponseDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderMetadata.ColumnNames.FirstResponseDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.FirstResponseByUserID
		/// </summary>
		virtual public System.String FirstResponseByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.FirstResponseByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.FirstResponseByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SRWorkTradeItem
		/// </summary>
		virtual public System.String SRWorkTradeItem
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkTradeItem);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkTradeItem, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.IsSanitation
		/// </summary>
		virtual public System.Boolean? IsSanitation
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsSanitation);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderMetadata.ColumnNames.IsSanitation, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.SRWorkOrderPoint
		/// </summary>
		virtual public System.String SRWorkOrderPoint
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkOrderPoint);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderMetadata.ColumnNames.SRWorkOrderPoint, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrder.WorkOrderPoint
		/// </summary>
		virtual public System.Decimal? WorkOrderPoint
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderMetadata.ColumnNames.WorkOrderPoint);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderMetadata.ColumnNames.WorkOrderPoint, value);
			}
		}

		#endregion

		#region String Properties

		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
		[BrowsableAttribute(false)]
		public esStrings str
		{
			get
			{
				if (esstrings == null)
				{
					esstrings = new esStrings(this);
				}
				return esstrings;
			}
		}

		[Serializable]
		sealed public class esStrings
		{
			public esStrings(esAssetWorkOrder entity)
			{
				this.entity = entity;
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String OrderDate
			{
				get
				{
					System.DateTime? data = entity.OrderDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDate = null;
					else entity.OrderDate = Convert.ToDateTime(value);
				}
			}
			public System.String FromServiceUnitID
			{
				get
				{
					System.String data = entity.FromServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
					else entity.FromServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
			public System.String ProblemDescription
			{
				get
				{
					System.String data = entity.ProblemDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProblemDescription = null;
					else entity.ProblemDescription = Convert.ToString(value);
				}
			}
			public System.String SRWorkStatus
			{
				get
				{
					System.String data = entity.SRWorkStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkStatus = null;
					else entity.SRWorkStatus = Convert.ToString(value);
				}
			}
			public System.String SRWorkType
			{
				get
				{
					System.String data = entity.SRWorkType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkType = null;
					else entity.SRWorkType = Convert.ToString(value);
				}
			}
			public System.String SRWorkPriority
			{
				get
				{
					System.String data = entity.SRWorkPriority;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkPriority = null;
					else entity.SRWorkPriority = Convert.ToString(value);
				}
			}
			public System.String SRWorkTrade
			{
				get
				{
					System.String data = entity.SRWorkTrade;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkTrade = null;
					else entity.SRWorkTrade = Convert.ToString(value);
				}
			}
			public System.String RequiredDate
			{
				get
				{
					System.DateTime? data = entity.RequiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequiredDate = null;
					else entity.RequiredDate = Convert.ToDateTime(value);
				}
			}
			public System.String RequestByUserID
			{
				get
				{
					System.String data = entity.RequestByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestByUserID = null;
					else entity.RequestByUserID = Convert.ToString(value);
				}
			}
			public System.String ReceivedDateTime
			{
				get
				{
					System.DateTime? data = entity.ReceivedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedDateTime = null;
					else entity.ReceivedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReceivedByUserID
			{
				get
				{
					System.String data = entity.ReceivedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedByUserID = null;
					else entity.ReceivedByUserID = Convert.ToString(value);
				}
			}
			public System.String SRFailureCode
			{
				get
				{
					System.String data = entity.SRFailureCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFailureCode = null;
					else entity.SRFailureCode = Convert.ToString(value);
				}
			}
			public System.String FailureCauseDescription
			{
				get
				{
					System.String data = entity.FailureCauseDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FailureCauseDescription = null;
					else entity.FailureCauseDescription = Convert.ToString(value);
				}
			}
			public System.String ActionTaken
			{
				get
				{
					System.String data = entity.ActionTaken;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionTaken = null;
					else entity.ActionTaken = Convert.ToString(value);
				}
			}
			public System.String PreventionTaken
			{
				get
				{
					System.String data = entity.PreventionTaken;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreventionTaken = null;
					else entity.PreventionTaken = Convert.ToString(value);
				}
			}
			public System.String CostEstimation
			{
				get
				{
					System.Decimal? data = entity.CostEstimation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostEstimation = null;
					else entity.CostEstimation = Convert.ToDecimal(value);
				}
			}
			public System.String LastRealizationByUserID
			{
				get
				{
					System.String data = entity.LastRealizationByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRealizationByUserID = null;
					else entity.LastRealizationByUserID = Convert.ToString(value);
				}
			}
			public System.String LastRealizationDateTime
			{
				get
				{
					System.DateTime? data = entity.LastRealizationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRealizationDateTime = null;
					else entity.LastRealizationDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String AcceptedByUserID
			{
				get
				{
					System.String data = entity.AcceptedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcceptedByUserID = null;
					else entity.AcceptedByUserID = Convert.ToString(value);
				}
			}
			public System.String AcceptedDateTime
			{
				get
				{
					System.DateTime? data = entity.AcceptedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcceptedDateTime = null;
					else entity.AcceptedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String IsProceed
			{
				get
				{
					System.Boolean? data = entity.IsProceed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProceed = null;
					else entity.IsProceed = Convert.ToBoolean(value);
				}
			}
			public System.String IsPreventiveMaintenance
			{
				get
				{
					System.Boolean? data = entity.IsPreventiveMaintenance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPreventiveMaintenance = null;
					else entity.IsPreventiveMaintenance = Convert.ToBoolean(value);
				}
			}
			public System.String PMNo
			{
				get
				{
					System.String data = entity.PMNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PMNo = null;
					else entity.PMNo = Convert.ToString(value);
				}
			}
			public System.String IsGeneratePrDr
			{
				get
				{
					System.Boolean? data = entity.IsGeneratePrDr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGeneratePrDr = null;
					else entity.IsGeneratePrDr = Convert.ToBoolean(value);
				}
			}
			public System.String LastUpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastUpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
					else entity.LastUpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastUpdateByUserID
			{
				get
				{
					System.String data = entity.LastUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
					else entity.LastUpdateByUserID = Convert.ToString(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ImplementedBy
			{
				get
				{
					System.String data = entity.ImplementedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImplementedBy = null;
					else entity.ImplementedBy = Convert.ToString(value);
				}
			}
			public System.String AcceptedBy
			{
				get
				{
					System.String data = entity.AcceptedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcceptedBy = null;
					else entity.AcceptedBy = Convert.ToString(value);
				}
			}
			public System.String SentToThirdPartiesByUserID
			{
				get
				{
					System.String data = entity.SentToThirdPartiesByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SentToThirdPartiesByUserID = null;
					else entity.SentToThirdPartiesByUserID = Convert.ToString(value);
				}
			}
			public System.String SentToThirdPartiesDateTime
			{
				get
				{
					System.DateTime? data = entity.SentToThirdPartiesDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SentToThirdPartiesDateTime = null;
					else entity.SentToThirdPartiesDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReceivedFromThirdPartiesByUserID
			{
				get
				{
					System.String data = entity.ReceivedFromThirdPartiesByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedFromThirdPartiesByUserID = null;
					else entity.ReceivedFromThirdPartiesByUserID = Convert.ToString(value);
				}
			}
			public System.String ReceivedFromThirdPartiesDateTime
			{
				get
				{
					System.DateTime? data = entity.ReceivedFromThirdPartiesDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedFromThirdPartiesDateTime = null;
					else entity.ReceivedFromThirdPartiesDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReceivedFromLogisticsByUserID
			{
				get
				{
					System.String data = entity.ReceivedFromLogisticsByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedFromLogisticsByUserID = null;
					else entity.ReceivedFromLogisticsByUserID = Convert.ToString(value);
				}
			}
			public System.String ReceivedFromLogisticsDateTime
			{
				get
				{
					System.DateTime? data = entity.ReceivedFromLogisticsDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedFromLogisticsDateTime = null;
					else entity.ReceivedFromLogisticsDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			public System.String LetterNo
			{
				get
				{
					System.String data = entity.LetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterNo = null;
					else entity.LetterNo = Convert.ToString(value);
				}
			}
			public System.String SupplierID
			{
				get
				{
					System.String data = entity.SupplierID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupplierID = null;
					else entity.SupplierID = Convert.ToString(value);
				}
			}
			public System.String FirstResponseDateTime
			{
				get
				{
					System.DateTime? data = entity.FirstResponseDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FirstResponseDateTime = null;
					else entity.FirstResponseDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String FirstResponseByUserID
			{
				get
				{
					System.String data = entity.FirstResponseByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FirstResponseByUserID = null;
					else entity.FirstResponseByUserID = Convert.ToString(value);
				}
			}
			public System.String SRWorkTradeItem
			{
				get
				{
					System.String data = entity.SRWorkTradeItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkTradeItem = null;
					else entity.SRWorkTradeItem = Convert.ToString(value);
				}
			}
			public System.String IsSanitation
			{
				get
				{
					System.Boolean? data = entity.IsSanitation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSanitation = null;
					else entity.IsSanitation = Convert.ToBoolean(value);
				}
			}
			public System.String SRWorkOrderPoint
			{
				get
				{
					System.String data = entity.SRWorkOrderPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkOrderPoint = null;
					else entity.SRWorkOrderPoint = Convert.ToString(value);
				}
			}
			public System.String WorkOrderPoint
			{
				get
				{
					System.Decimal? data = entity.WorkOrderPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkOrderPoint = null;
					else entity.WorkOrderPoint = Convert.ToDecimal(value);
				}
			}
			private esAssetWorkOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetWorkOrderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntity)this).Connection;
		}

		[System.Diagnostics.DebuggerNonUserCode]
		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esAssetWorkOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AssetWorkOrder : esAssetWorkOrder
	{
	}

	[Serializable]
	abstract public class esAssetWorkOrderQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AssetWorkOrderMetadata.Meta();
			}
		}

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		}

		public esQueryItem OrderDate
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.OrderDate, esSystemType.DateTime);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem ProblemDescription
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ProblemDescription, esSystemType.String);
			}
		}

		public esQueryItem SRWorkStatus
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SRWorkStatus, esSystemType.String);
			}
		}

		public esQueryItem SRWorkType
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SRWorkType, esSystemType.String);
			}
		}

		public esQueryItem SRWorkPriority
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SRWorkPriority, esSystemType.String);
			}
		}

		public esQueryItem SRWorkTrade
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SRWorkTrade, esSystemType.String);
			}
		}

		public esQueryItem RequiredDate
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.RequiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RequestByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.RequestByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReceivedDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ReceivedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ReceivedByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ReceivedByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRFailureCode
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SRFailureCode, esSystemType.String);
			}
		}

		public esQueryItem FailureCauseDescription
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.FailureCauseDescription, esSystemType.String);
			}
		}

		public esQueryItem ActionTaken
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ActionTaken, esSystemType.String);
			}
		}

		public esQueryItem PreventionTaken
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.PreventionTaken, esSystemType.String);
			}
		}

		public esQueryItem CostEstimation
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.CostEstimation, esSystemType.Decimal);
			}
		}

		public esQueryItem LastRealizationByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.LastRealizationByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastRealizationDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.LastRealizationDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem AcceptedByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.AcceptedByUserID, esSystemType.String);
			}
		}

		public esQueryItem AcceptedDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.AcceptedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem IsProceed
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.IsProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPreventiveMaintenance
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.IsPreventiveMaintenance, esSystemType.Boolean);
			}
		}

		public esQueryItem PMNo
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.PMNo, esSystemType.String);
			}
		}

		public esQueryItem IsGeneratePrDr
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.IsGeneratePrDr, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ImplementedBy
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ImplementedBy, esSystemType.String);
			}
		}

		public esQueryItem AcceptedBy
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.AcceptedBy, esSystemType.String);
			}
		}

		public esQueryItem SentToThirdPartiesByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesByUserID, esSystemType.String);
			}
		}

		public esQueryItem SentToThirdPartiesDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ReceivedFromThirdPartiesByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReceivedFromThirdPartiesDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ReceivedFromLogisticsByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReceivedFromLogisticsDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem LetterNo
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.LetterNo, esSystemType.String);
			}
		}

		public esQueryItem SupplierID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SupplierID, esSystemType.String);
			}
		}

		public esQueryItem FirstResponseDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.FirstResponseDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem FirstResponseByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.FirstResponseByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRWorkTradeItem
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SRWorkTradeItem, esSystemType.String);
			}
		}

		public esQueryItem IsSanitation
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.IsSanitation, esSystemType.Boolean);
			}
		}

		public esQueryItem SRWorkOrderPoint
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.SRWorkOrderPoint, esSystemType.String);
			}
		}

		public esQueryItem WorkOrderPoint
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderMetadata.ColumnNames.WorkOrderPoint, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetWorkOrderCollection")]
	public partial class AssetWorkOrderCollection : esAssetWorkOrderCollection, IEnumerable<AssetWorkOrder>
	{
		public AssetWorkOrderCollection()
		{

		}

		public static implicit operator List<AssetWorkOrder>(AssetWorkOrderCollection coll)
		{
			List<AssetWorkOrder> list = new List<AssetWorkOrder>();

			foreach (AssetWorkOrder emp in coll)
			{
				list.Add(emp);
			}

			return list;
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetWorkOrderMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetWorkOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetWorkOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetWorkOrder();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AssetWorkOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetWorkOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(AssetWorkOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AssetWorkOrder AddNew()
		{
			AssetWorkOrder entity = base.AddNewEntity() as AssetWorkOrder;

			return entity;
		}
		public AssetWorkOrder FindByPrimaryKey(String orderNo)
		{
			return base.FindByPrimaryKey(orderNo) as AssetWorkOrder;
		}

		#region IEnumerable< AssetWorkOrder> Members

		IEnumerator<AssetWorkOrder> IEnumerable<AssetWorkOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AssetWorkOrder;
			}
		}

		#endregion

		private AssetWorkOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetWorkOrder' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AssetWorkOrder ({OrderNo})")]
	[Serializable]
	public partial class AssetWorkOrder : esAssetWorkOrder
	{
		public AssetWorkOrder()
		{
		}

		public AssetWorkOrder(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetWorkOrderMetadata.Meta();
			}
		}

		override protected esAssetWorkOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetWorkOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AssetWorkOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetWorkOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(AssetWorkOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AssetWorkOrderQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AssetWorkOrderQuery : esAssetWorkOrderQuery
	{
		public AssetWorkOrderQuery()
		{

		}

		public AssetWorkOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AssetWorkOrderQuery";
		}
	}

	[Serializable]
	public partial class AssetWorkOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetWorkOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.OrderDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.OrderDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.FromServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ToServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.AssetID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ItemID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.Qty, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ProblemDescription, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ProblemDescription;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SRWorkStatus, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SRWorkStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SRWorkType, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SRWorkType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SRWorkPriority, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SRWorkPriority;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SRWorkTrade, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SRWorkTrade;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.RequiredDate, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.RequiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.RequestByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.RequestByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ReceivedDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ReceivedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ReceivedByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ReceivedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SRFailureCode, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SRFailureCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.FailureCauseDescription, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.FailureCauseDescription;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ActionTaken, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ActionTaken;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.PreventionTaken, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.PreventionTaken;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.CostEstimation, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.CostEstimation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.LastRealizationByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.LastRealizationByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.LastRealizationDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.LastRealizationDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.AcceptedByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.AcceptedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.AcceptedDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.AcceptedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.IsApproved, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.IsVoid, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.IsProceed, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.IsProceed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.IsPreventiveMaintenance, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.IsPreventiveMaintenance;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.PMNo, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.PMNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.IsGeneratePrDr, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.IsGeneratePrDr;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.LastUpdateDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.LastUpdateByUserID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ApprovedDateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ImplementedBy, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ImplementedBy;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.AcceptedBy, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.AcceptedBy;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesByUserID, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SentToThirdPartiesByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SentToThirdPartiesDateTime, 37, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SentToThirdPartiesDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesByUserID, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ReceivedFromThirdPartiesByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ReceivedFromThirdPartiesDateTime, 39, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ReceivedFromThirdPartiesDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsByUserID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ReceivedFromLogisticsByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ReceivedFromLogisticsDateTime, 41, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ReceivedFromLogisticsDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.ReferenceNo, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.LetterNo, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.LetterNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SupplierID, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SupplierID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.FirstResponseDateTime, 45, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.FirstResponseDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.FirstResponseByUserID, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.FirstResponseByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SRWorkTradeItem, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SRWorkTradeItem;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.IsSanitation, 48, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.IsSanitation;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.SRWorkOrderPoint, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.SRWorkOrderPoint;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderMetadata.ColumnNames.WorkOrderPoint, 50, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderMetadata.PropertyNames.WorkOrderPoint;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AssetWorkOrderMetadata Meta()
		{
			return meta;
		}

		public Guid DataID
		{
			get { return base._dataID; }
		}

		public bool MultiProviderMode
		{
			get { return false; }
		}

		public esColumnMetadataCollection Columns
		{
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string OrderNo = "OrderNo";
			public const string OrderDate = "OrderDate";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string AssetID = "AssetID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string ProblemDescription = "ProblemDescription";
			public const string SRWorkStatus = "SRWorkStatus";
			public const string SRWorkType = "SRWorkType";
			public const string SRWorkPriority = "SRWorkPriority";
			public const string SRWorkTrade = "SRWorkTrade";
			public const string RequiredDate = "RequiredDate";
			public const string RequestByUserID = "RequestByUserID";
			public const string ReceivedDateTime = "ReceivedDateTime";
			public const string ReceivedByUserID = "ReceivedByUserID";
			public const string SRFailureCode = "SRFailureCode";
			public const string FailureCauseDescription = "FailureCauseDescription";
			public const string ActionTaken = "ActionTaken";
			public const string PreventionTaken = "PreventionTaken";
			public const string CostEstimation = "CostEstimation";
			public const string LastRealizationByUserID = "LastRealizationByUserID";
			public const string LastRealizationDateTime = "LastRealizationDateTime";
			public const string AcceptedByUserID = "AcceptedByUserID";
			public const string AcceptedDateTime = "AcceptedDateTime";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string IsProceed = "IsProceed";
			public const string IsPreventiveMaintenance = "IsPreventiveMaintenance";
			public const string PMNo = "PMNo";
			public const string IsGeneratePrDr = "IsGeneratePrDr";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ImplementedBy = "ImplementedBy";
			public const string AcceptedBy = "AcceptedBy";
			public const string SentToThirdPartiesByUserID = "SentToThirdPartiesByUserID";
			public const string SentToThirdPartiesDateTime = "SentToThirdPartiesDateTime";
			public const string ReceivedFromThirdPartiesByUserID = "ReceivedFromThirdPartiesByUserID";
			public const string ReceivedFromThirdPartiesDateTime = "ReceivedFromThirdPartiesDateTime";
			public const string ReceivedFromLogisticsByUserID = "ReceivedFromLogisticsByUserID";
			public const string ReceivedFromLogisticsDateTime = "ReceivedFromLogisticsDateTime";
			public const string ReferenceNo = "ReferenceNo";
			public const string LetterNo = "LetterNo";
			public const string SupplierID = "SupplierID";
			public const string FirstResponseDateTime = "FirstResponseDateTime";
			public const string FirstResponseByUserID = "FirstResponseByUserID";
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string IsSanitation = "IsSanitation";
			public const string SRWorkOrderPoint = "SRWorkOrderPoint";
			public const string WorkOrderPoint = "WorkOrderPoint";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string OrderNo = "OrderNo";
			public const string OrderDate = "OrderDate";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string AssetID = "AssetID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string ProblemDescription = "ProblemDescription";
			public const string SRWorkStatus = "SRWorkStatus";
			public const string SRWorkType = "SRWorkType";
			public const string SRWorkPriority = "SRWorkPriority";
			public const string SRWorkTrade = "SRWorkTrade";
			public const string RequiredDate = "RequiredDate";
			public const string RequestByUserID = "RequestByUserID";
			public const string ReceivedDateTime = "ReceivedDateTime";
			public const string ReceivedByUserID = "ReceivedByUserID";
			public const string SRFailureCode = "SRFailureCode";
			public const string FailureCauseDescription = "FailureCauseDescription";
			public const string ActionTaken = "ActionTaken";
			public const string PreventionTaken = "PreventionTaken";
			public const string CostEstimation = "CostEstimation";
			public const string LastRealizationByUserID = "LastRealizationByUserID";
			public const string LastRealizationDateTime = "LastRealizationDateTime";
			public const string AcceptedByUserID = "AcceptedByUserID";
			public const string AcceptedDateTime = "AcceptedDateTime";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string IsProceed = "IsProceed";
			public const string IsPreventiveMaintenance = "IsPreventiveMaintenance";
			public const string PMNo = "PMNo";
			public const string IsGeneratePrDr = "IsGeneratePrDr";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ImplementedBy = "ImplementedBy";
			public const string AcceptedBy = "AcceptedBy";
			public const string SentToThirdPartiesByUserID = "SentToThirdPartiesByUserID";
			public const string SentToThirdPartiesDateTime = "SentToThirdPartiesDateTime";
			public const string ReceivedFromThirdPartiesByUserID = "ReceivedFromThirdPartiesByUserID";
			public const string ReceivedFromThirdPartiesDateTime = "ReceivedFromThirdPartiesDateTime";
			public const string ReceivedFromLogisticsByUserID = "ReceivedFromLogisticsByUserID";
			public const string ReceivedFromLogisticsDateTime = "ReceivedFromLogisticsDateTime";
			public const string ReferenceNo = "ReferenceNo";
			public const string LetterNo = "LetterNo";
			public const string SupplierID = "SupplierID";
			public const string FirstResponseDateTime = "FirstResponseDateTime";
			public const string FirstResponseByUserID = "FirstResponseByUserID";
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string IsSanitation = "IsSanitation";
			public const string SRWorkOrderPoint = "SRWorkOrderPoint";
			public const string WorkOrderPoint = "WorkOrderPoint";
		}
		#endregion

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}

		#region MAP esDefault

		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(AssetWorkOrderMetadata))
			{
				if (AssetWorkOrderMetadata.mapDelegates == null)
				{
					AssetWorkOrderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AssetWorkOrderMetadata.meta == null)
				{
					AssetWorkOrderMetadata.meta = new AssetWorkOrderMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ProblemDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWorkStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWorkType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWorkPriority", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWorkTrade", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RequestByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReceivedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFailureCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FailureCauseDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionTaken", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreventionTaken", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CostEstimation", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastRealizationByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastRealizationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AcceptedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AcceptedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPreventiveMaintenance", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PMNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGeneratePrDr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ImplementedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AcceptedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SentToThirdPartiesByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SentToThirdPartiesDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReceivedFromThirdPartiesByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedFromThirdPartiesDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReceivedFromLogisticsByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedFromLogisticsDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FirstResponseDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FirstResponseByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWorkTradeItem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSanitation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRWorkOrderPoint", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkOrderPoint", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "AssetWorkOrder";
				meta.Destination = "AssetWorkOrder";
				meta.spInsert = "proc_AssetWorkOrderInsert";
				meta.spUpdate = "proc_AssetWorkOrderUpdate";
				meta.spDelete = "proc_AssetWorkOrderDelete";
				meta.spLoadAll = "proc_AssetWorkOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetWorkOrderLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetWorkOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
