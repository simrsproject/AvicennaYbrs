/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/25/2021 9:10:02 AM
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
	abstract public class esBudgetingCollection : esEntityCollectionWAuditLog
	{
		public esBudgetingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BudgetingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBudgetingQuery query)
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
			this.InitQuery(query as esBudgetingQuery);
		}
		#endregion
			
		virtual public Budgeting DetachEntity(Budgeting entity)
		{
			return base.DetachEntity(entity) as Budgeting;
		}
		
		virtual public Budgeting AttachEntity(Budgeting entity)
		{
			return base.AttachEntity(entity) as Budgeting;
		}
		
		virtual public void Combine(BudgetingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Budgeting this[int index]
		{
			get
			{
				return base[index] as Budgeting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Budgeting);
		}
	}

	[Serializable]
	abstract public class esBudgeting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBudgetingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBudgeting()
		{
		}
	
		public esBudgeting(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String budgetingNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String budgetingNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String budgetingNo)
		{
			esBudgetingQuery query = this.GetDynamicQuery();
			query.Where(query.BudgetingNo==budgetingNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String budgetingNo)
		{
			esParameters parms = new esParameters();
			parms.Add("BudgetingNo",budgetingNo);
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
						case "SRBudgetingGroup": this.str.SRBudgetingGroup = (string)value; break;
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
		/// Maps to Budgeting.BudgetingNo
		/// </summary>
		virtual public System.String BudgetingNo
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.BudgetingNo);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.BudgetingNo, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.Periode
		/// </summary>
		virtual public System.Int32? Periode
		{
			get
			{
				return base.GetSystemInt32(BudgetingMetadata.ColumnNames.Periode);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingMetadata.ColumnNames.Periode, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.Revision
		/// </summary>
		virtual public System.Int32? Revision
		{
			get
			{
				return base.GetSystemInt32(BudgetingMetadata.ColumnNames.Revision);
			}
			
			set
			{
				base.SetSystemInt32(BudgetingMetadata.ColumnNames.Revision, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.SumLimit
		/// </summary>
		virtual public System.Decimal? SumLimit
		{
			get
			{
				return base.GetSystemDecimal(BudgetingMetadata.ColumnNames.SumLimit);
			}
			
			set
			{
				base.SetSystemDecimal(BudgetingMetadata.ColumnNames.SumLimit, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(BudgetingMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(BudgetingMetadata.ColumnNames.IsApprove, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(BudgetingMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(BudgetingMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.VoidNotes
		/// </summary>
		virtual public System.String VoidNotes
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.VoidNotes);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.VoidNotes, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.SRBudgetingVerifyStatus
		/// </summary>
		virtual public System.String SRBudgetingVerifyStatus
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.SRBudgetingVerifyStatus);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.SRBudgetingVerifyStatus, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.VerifiedByUserID);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingMetadata.ColumnNames.VerifiedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BudgetingMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.CorrectionNotes
		/// </summary>
		virtual public System.String CorrectionNotes
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.CorrectionNotes);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.CorrectionNotes, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.IsByItem
		/// </summary>
		virtual public System.Boolean? IsByItem
		{
			get
			{
				return base.GetSystemBoolean(BudgetingMetadata.ColumnNames.IsByItem);
			}
			
			set
			{
				base.SetSystemBoolean(BudgetingMetadata.ColumnNames.IsByItem, value);
			}
		}
		/// <summary>
		/// Maps to Budgeting.SRBudgetingGroup
		/// </summary>
		virtual public System.String SRBudgetingGroup
		{
			get
			{
				return base.GetSystemString(BudgetingMetadata.ColumnNames.SRBudgetingGroup);
			}
			
			set
			{
				base.SetSystemString(BudgetingMetadata.ColumnNames.SRBudgetingGroup, value);
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
			public esStrings(esBudgeting entity)
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
			public System.String SRBudgetingGroup
			{
				get
				{
					System.String data = entity.SRBudgetingGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBudgetingGroup = null;
					else entity.SRBudgetingGroup = Convert.ToString(value);
				}
			}
			private esBudgeting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBudgetingQuery query)
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
				throw new Exception("esBudgeting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Budgeting : esBudgeting
	{	
	}

	[Serializable]
	abstract public class esBudgetingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingMetadata.Meta();
			}
		}	
			
		public esQueryItem BudgetingNo
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.BudgetingNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem Periode
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.Periode, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Revision
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.Revision, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SumLimit
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.SumLimit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidNotes
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.VoidNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBudgetingVerifyStatus
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.SRBudgetingVerifyStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem CorrectionNotes
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.CorrectionNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsByItem
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.IsByItem, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRBudgetingGroup
		{
			get
			{
				return new esQueryItem(this, BudgetingMetadata.ColumnNames.SRBudgetingGroup, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BudgetingCollection")]
	public partial class BudgetingCollection : esBudgetingCollection, IEnumerable< Budgeting>
	{
		public BudgetingCollection()
		{

		}	
		
		public static implicit operator List< Budgeting>(BudgetingCollection coll)
		{
			List< Budgeting> list = new List< Budgeting>();
			
			foreach (Budgeting emp in coll)
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
				return  BudgetingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Budgeting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Budgeting();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BudgetingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingQuery();
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
		public bool Load(BudgetingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Budgeting AddNew()
		{
			Budgeting entity = base.AddNewEntity() as Budgeting;
			
			return entity;		
		}
		public Budgeting FindByPrimaryKey(String budgetingNo)
		{
			return base.FindByPrimaryKey(budgetingNo) as Budgeting;
		}

		#region IEnumerable< Budgeting> Members

		IEnumerator< Budgeting> IEnumerable< Budgeting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Budgeting;
			}
		}

		#endregion
		
		private BudgetingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Budgeting' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Budgeting ({BudgetingNo})")]
	[Serializable]
	public partial class Budgeting : esBudgeting
	{
		public Budgeting()
		{
		}	
	
		public Budgeting(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingMetadata.Meta();
			}
		}	
	
		override protected esBudgetingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BudgetingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingQuery();
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
		public bool Load(BudgetingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BudgetingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BudgetingQuery : esBudgetingQuery
	{
		public BudgetingQuery()
		{

		}		
		
		public BudgetingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BudgetingQuery";
        }
	}

	[Serializable]
	public partial class BudgetingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BudgetingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.BudgetingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.BudgetingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.Periode, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingMetadata.PropertyNames.Periode;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.Revision, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingMetadata.PropertyNames.Revision;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.SumLimit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingMetadata.PropertyNames.SumLimit;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.CreatedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.IsApprove, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingMetadata.PropertyNames.IsApprove;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.ApprovedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.ApprovedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.VoidByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.VoidDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.VoidNotes, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.VoidNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.SRBudgetingVerifyStatus, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.SRBudgetingVerifyStatus;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.VerifiedByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.VerifiedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.Notes, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.CorrectionNotes, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.CorrectionNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.IsByItem, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingMetadata.PropertyNames.IsByItem;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BudgetingMetadata.ColumnNames.SRBudgetingGroup, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingMetadata.PropertyNames.SRBudgetingGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BudgetingMetadata Meta()
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
			public const string SRBudgetingGroup = "SRBudgetingGroup";
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
			public const string SRBudgetingGroup = "SRBudgetingGroup";
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
			lock (typeof(BudgetingMetadata))
			{
				if(BudgetingMetadata.mapDelegates == null)
				{
					BudgetingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BudgetingMetadata.meta == null)
				{
					BudgetingMetadata.meta = new BudgetingMetadata();
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
				meta.AddTypeMap("SRBudgetingGroup", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "Budgeting";
				meta.Destination = "Budgeting";
				meta.spInsert = "proc_BudgetingInsert";				
				meta.spUpdate = "proc_BudgetingUpdate";		
				meta.spDelete = "proc_BudgetingDelete";
				meta.spLoadAll = "proc_BudgetingLoadAll";
				meta.spLoadByPrimaryKey = "proc_BudgetingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BudgetingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
