/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/17/2021 2:24:21 PM
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
	abstract public class esBudgetingHistoryCollection : esEntityCollectionWAuditLog
	{
		public esBudgetingHistoryCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BudgetingHistoryCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBudgetingHistoryQuery query)
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
			this.InitQuery(query as esBudgetingHistoryQuery);
		}
		#endregion
			
		virtual public BudgetingHistory DetachEntity(BudgetingHistory entity)
		{
			return base.DetachEntity(entity) as BudgetingHistory;
		}
		
		virtual public BudgetingHistory AttachEntity(BudgetingHistory entity)
		{
			return base.AttachEntity(entity) as BudgetingHistory;
		}
		
		virtual public void Combine(BudgetingHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BudgetingHistory this[int index]
		{
			get
			{
				return base[index] as BudgetingHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BudgetingHistory);
		}
	}

	[Serializable]
	abstract public class esBudgetingHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBudgetingHistoryQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBudgetingHistory()
		{
		}
	
		public esBudgetingHistory(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String budgetingNo, Int32 revision)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String budgetingNo, Int32 revision)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision);
		}
	
		private bool LoadByPrimaryKeyDynamic(String budgetingNo, Int32 revision)
		{
			esBudgetingHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.BudgetingNo==budgetingNo, query.Revision==revision);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String budgetingNo, Int32 revision)
		{
			esParameters parms = new esParameters();
			parms.Add("BudgetingNo",budgetingNo);
			parms.Add("Revision",revision);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "BudgetingNo": this.str.BudgetingNo = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "Periode": this.str.Periode = (string)value; break;
						case "Revision": this.str.Revision = (string)value; break;
						case "SumLimit": this.str.SumLimit = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsApprove": this.str.IsApprove = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidNotes": this.str.VoidNotes = (string)value; break;
						case "SRBudgetingVerifyStatus": this.str.SRBudgetingVerifyStatus = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "CorrectionNotes": this.str.CorrectionNotes = (string)value; break;
						case "IsByItem": this.str.IsByItem = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Periode":
						
							if (value == null || value is System.Int32)
								this.Periode = (System.Int32?)value;
							break;
						case "Revision":
						
							if (value == null || value is System.Int32)
								this.Revision = (System.Int32?)value;
							break;
						case "SumLimit":
						
							if (value == null || value is System.Decimal)
								this.SumLimit = (System.Decimal?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsApprove":
						
							if (value == null || value is System.Boolean)
								this.IsApprove = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":
						
							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "VerifiedDateTime":
						
							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "IsByItem":
						
							if (value == null || value is System.Boolean)
								this.IsByItem = (System.Boolean?)value;
							break;
					
						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to BudgetingHistory.BudgetingNo
		/// </summary>
		virtual public System.String BudgetingNo
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.BudgetingNo);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.BudgetingNo, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.Periode
		/// </summary>
		virtual public System.Int32? Periode
		{
			get
			{
				return base.GetSystemInt32(BudgetingHistoryMetadata.ColumnNames.Periode);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingHistoryMetadata.ColumnNames.Periode, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.Revision
		/// </summary>
		virtual public System.Int32? Revision
		{
			get
			{
				return base.GetSystemInt32(BudgetingHistoryMetadata.ColumnNames.Revision);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingHistoryMetadata.ColumnNames.Revision, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.SumLimit
		/// </summary>
		virtual public System.Decimal? SumLimit
		{
			get
			{
				return base.GetSystemDecimal(BudgetingHistoryMetadata.ColumnNames.SumLimit);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingHistoryMetadata.ColumnNames.SumLimit, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(BudgetingHistoryMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(BudgetingHistoryMetadata.ColumnNames.IsApprove, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(BudgetingHistoryMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(BudgetingHistoryMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.VoidNotes
		/// </summary>
		virtual public System.String VoidNotes
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.VoidNotes);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.VoidNotes, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.SRBudgetingVerifyStatus
		/// </summary>
		virtual public System.String SRBudgetingVerifyStatus
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.SRBudgetingVerifyStatus);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.SRBudgetingVerifyStatus, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.VerifiedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.VerifiedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingHistoryMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.CorrectionNotes
		/// </summary>
		virtual public System.String CorrectionNotes
		{
			get
			{
				return base.GetSystemString(BudgetingHistoryMetadata.ColumnNames.CorrectionNotes);
			}
			
			set
			{
				base.SetSystemString(BudgetingHistoryMetadata.ColumnNames.CorrectionNotes, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingHistory.IsByItem
		/// </summary>
		virtual public System.Boolean? IsByItem
		{
			get
			{
				return base.GetSystemBoolean(BudgetingHistoryMetadata.ColumnNames.IsByItem);
			}
			
			set
			{
				base.SetSystemBoolean(BudgetingHistoryMetadata.ColumnNames.IsByItem, value);
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
		[BrowsableAttribute( false )]		
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
			public esStrings(esBudgetingHistory entity)
			{
				this.entity = entity;
			}
			public System.String BudgetingNo
			{
				get
				{
					System.String data = entity.BudgetingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetingNo = null;
					else entity.BudgetingNo = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String Periode
			{
				get
				{
					System.Int32? data = entity.Periode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Periode = null;
					else entity.Periode = Convert.ToInt32(value);
				}
			}
			public System.String Revision
			{
				get
				{
					System.Int32? data = entity.Revision;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Revision = null;
					else entity.Revision = Convert.ToInt32(value);
				}
			}
			public System.String SumLimit
			{
				get
				{
					System.Decimal? data = entity.SumLimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SumLimit = null;
					else entity.SumLimit = Convert.ToDecimal(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			public System.String IsApprove
			{
				get
				{
					System.Boolean? data = entity.IsApprove;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApprove = null;
					else entity.IsApprove = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
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
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidNotes
			{
				get
				{
					System.String data = entity.VoidNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidNotes = null;
					else entity.VoidNotes = Convert.ToString(value);
				}
			}
			public System.String SRBudgetingVerifyStatus
			{
				get
				{
					System.String data = entity.SRBudgetingVerifyStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBudgetingVerifyStatus = null;
					else entity.SRBudgetingVerifyStatus = Convert.ToString(value);
				}
			}
			public System.String VerifiedByUserID
			{
				get
				{
					System.String data = entity.VerifiedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
					else entity.VerifiedByUserID = Convert.ToString(value);
				}
			}
			public System.String VerifiedDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
					else entity.VerifiedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			public System.String CorrectionNotes
			{
				get
				{
					System.String data = entity.CorrectionNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CorrectionNotes = null;
					else entity.CorrectionNotes = Convert.ToString(value);
				}
			}
			public System.String IsByItem
			{
				get
				{
					System.Boolean? data = entity.IsByItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsByItem = null;
					else entity.IsByItem = Convert.ToBoolean(value);
				}
			}
			private esBudgetingHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBudgetingHistoryQuery query)
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
				throw new Exception("esBudgetingHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BudgetingHistory : esBudgetingHistory
	{	
	}

	[Serializable]
	abstract public class esBudgetingHistoryQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingHistoryMetadata.Meta();
			}
		}	
			
		public esQueryItem BudgetingNo
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.BudgetingNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem Periode
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.Periode, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Revision
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.Revision, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SumLimit
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.SumLimit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidNotes
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.VoidNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBudgetingVerifyStatus
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.SRBudgetingVerifyStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem CorrectionNotes
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.CorrectionNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsByItem
		{
			get
			{
				return new esQueryItem(this, BudgetingHistoryMetadata.ColumnNames.IsByItem, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BudgetingHistoryCollection")]
	public partial class BudgetingHistoryCollection : esBudgetingHistoryCollection, IEnumerable< BudgetingHistory>
	{
		public BudgetingHistoryCollection()
		{

		}	
		
		public static implicit operator List< BudgetingHistory>(BudgetingHistoryCollection coll)
		{
			List< BudgetingHistory> list = new List< BudgetingHistory>();
			
			foreach (BudgetingHistory emp in coll)
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
				return  BudgetingHistoryMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BudgetingHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BudgetingHistory();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BudgetingHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingHistoryQuery();
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
		public bool Load(BudgetingHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BudgetingHistory AddNew()
		{
			BudgetingHistory entity = base.AddNewEntity() as BudgetingHistory;
			
			return entity;		
		}
		public BudgetingHistory FindByPrimaryKey(String budgetingNo, Int32 revision)
		{
			return base.FindByPrimaryKey(budgetingNo, revision) as BudgetingHistory;
		}

		#region IEnumerable< BudgetingHistory> Members

		IEnumerator< BudgetingHistory> IEnumerable< BudgetingHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BudgetingHistory;
			}
		}

		#endregion
		
		private BudgetingHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BudgetingHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BudgetingHistory ({BudgetingNo, Revision})")]
	[Serializable]
	public partial class BudgetingHistory : esBudgetingHistory
	{
		public BudgetingHistory()
		{
		}	
	
		public BudgetingHistory(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingHistoryMetadata.Meta();
			}
		}	
	
		override protected esBudgetingHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BudgetingHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingHistoryQuery();
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
		public bool Load(BudgetingHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BudgetingHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BudgetingHistoryQuery : esBudgetingHistoryQuery
	{
		public BudgetingHistoryQuery()
		{

		}		
		
		public BudgetingHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BudgetingHistoryQuery";
        }
	}

	[Serializable]
	public partial class BudgetingHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BudgetingHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.BudgetingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.BudgetingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.Periode, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.Periode;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.Revision, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.Revision;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.SumLimit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.SumLimit;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.CreatedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.IsApprove, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.IsApprove;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.ApprovedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.ApprovedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.VoidByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.VoidDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.VoidNotes, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.VoidNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.SRBudgetingVerifyStatus, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.SRBudgetingVerifyStatus;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.VerifiedByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.VerifiedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.Notes, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.CorrectionNotes, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.CorrectionNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingHistoryMetadata.ColumnNames.IsByItem, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingHistoryMetadata.PropertyNames.IsByItem;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BudgetingHistoryMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string BudgetingNo = "BudgetingNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string Periode = "Periode";
			public const string Revision = "Revision";
			public const string SumLimit = "SumLimit";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApprove = "IsApprove";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidNotes = "VoidNotes";
			public const string SRBudgetingVerifyStatus = "SRBudgetingVerifyStatus";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string Notes = "Notes";
			public const string CorrectionNotes = "CorrectionNotes";
			public const string IsByItem = "IsByItem";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string BudgetingNo = "BudgetingNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string Periode = "Periode";
			public const string Revision = "Revision";
			public const string SumLimit = "SumLimit";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApprove = "IsApprove";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidNotes = "VoidNotes";
			public const string SRBudgetingVerifyStatus = "SRBudgetingVerifyStatus";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string Notes = "Notes";
			public const string CorrectionNotes = "CorrectionNotes";
			public const string IsByItem = "IsByItem";
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
			lock (typeof(BudgetingHistoryMetadata))
			{
				if(BudgetingHistoryMetadata.mapDelegates == null)
				{
					BudgetingHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BudgetingHistoryMetadata.meta == null)
				{
					BudgetingHistoryMetadata.meta = new BudgetingHistoryMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("BudgetingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Periode", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Revision", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SumLimit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBudgetingVerifyStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CorrectionNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsByItem", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "BudgetingHistory";
				meta.Destination = "BudgetingHistory";
				meta.spInsert = "proc_BudgetingHistoryInsert";				
				meta.spUpdate = "proc_BudgetingHistoryUpdate";		
				meta.spDelete = "proc_BudgetingHistoryDelete";
				meta.spLoadAll = "proc_BudgetingHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_BudgetingHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BudgetingHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
