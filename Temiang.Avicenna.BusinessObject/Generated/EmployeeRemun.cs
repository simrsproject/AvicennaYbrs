/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/4/2022 8:00:14 AM
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
	abstract public class esEmployeeRemunCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeRemunCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "EmployeeRemunCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esEmployeeRemunQuery query)
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
			this.InitQuery(query as esEmployeeRemunQuery);
		}
		#endregion
			
		virtual public EmployeeRemun DetachEntity(EmployeeRemun entity)
		{
			return base.DetachEntity(entity) as EmployeeRemun;
		}
		
		virtual public EmployeeRemun AttachEntity(EmployeeRemun entity)
		{
			return base.AttachEntity(entity) as EmployeeRemun;
		}
		
		virtual public void Combine(EmployeeRemunCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeRemun this[int index]
		{
			get
			{
				return base[index] as EmployeeRemun;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeRemun);
		}
	}

	[Serializable]
	abstract public class esEmployeeRemun : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeRemunQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esEmployeeRemun()
		{
		}
	
		public esEmployeeRemun(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 remunID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 remunID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 remunID)
		{
			esEmployeeRemunQuery query = this.GetDynamicQuery();
			query.Where(query.RemunID==remunID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 remunID)
		{
			esParameters parms = new esParameters();
			parms.Add("RemunID",remunID);
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
						case "RemunID": this.str.RemunID = (string)value; break;
						case "RemunNo": this.str.RemunNo = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "PeriodMonth": this.str.PeriodMonth = (string)value; break;
						case "FundAllocProcedure": this.str.FundAllocProcedure = (string)value; break;
						case "FundAllocPosition": this.str.FundAllocPosition = (string)value; break;
						case "FundAllocInsetif": this.str.FundAllocInsetif = (string)value; break;
						case "KursPosition": this.str.KursPosition = (string)value; break;
						case "KursInsentif": this.str.KursInsentif = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RemunID":
						
							if (value == null || value is System.Int32)
								this.RemunID = (System.Int32?)value;
							break;
						case "PeriodYear":
						
							if (value == null || value is System.Int32)
								this.PeriodYear = (System.Int32?)value;
							break;
						case "PeriodMonth":
						
							if (value == null || value is System.Int32)
								this.PeriodMonth = (System.Int32?)value;
							break;
						case "FundAllocProcedure":
						
							if (value == null || value is System.Decimal)
								this.FundAllocProcedure = (System.Decimal?)value;
							break;
						case "FundAllocPosition":
						
							if (value == null || value is System.Decimal)
								this.FundAllocPosition = (System.Decimal?)value;
							break;
						case "FundAllocInsetif":
						
							if (value == null || value is System.Decimal)
								this.FundAllocInsetif = (System.Decimal?)value;
							break;
						case "KursPosition":
						
							if (value == null || value is System.Decimal)
								this.KursPosition = (System.Decimal?)value;
							break;
						case "KursInsentif":
						
							if (value == null || value is System.Decimal)
								this.KursInsentif = (System.Decimal?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
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
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeRemun.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.RemunNo
		/// </summary>
		virtual public System.String RemunNo
		{
			get
			{
				return base.GetSystemString(EmployeeRemunMetadata.ColumnNames.RemunNo);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunMetadata.ColumnNames.RemunNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.PeriodYear
		/// </summary>
		virtual public System.Int32? PeriodYear
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunMetadata.ColumnNames.PeriodYear);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.PeriodMonth
		/// </summary>
		virtual public System.Int32? PeriodMonth
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunMetadata.ColumnNames.PeriodMonth);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunMetadata.ColumnNames.PeriodMonth, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.FundAllocProcedure
		/// </summary>
		virtual public System.Decimal? FundAllocProcedure
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunMetadata.ColumnNames.FundAllocProcedure);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunMetadata.ColumnNames.FundAllocProcedure, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.FundAllocPosition
		/// </summary>
		virtual public System.Decimal? FundAllocPosition
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunMetadata.ColumnNames.FundAllocPosition);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunMetadata.ColumnNames.FundAllocPosition, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.FundAllocInsetif
		/// </summary>
		virtual public System.Decimal? FundAllocInsetif
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunMetadata.ColumnNames.FundAllocInsetif);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunMetadata.ColumnNames.FundAllocInsetif, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.KursPosition
		/// </summary>
		virtual public System.Decimal? KursPosition
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunMetadata.ColumnNames.KursPosition);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunMetadata.ColumnNames.KursPosition, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.KursInsentif
		/// </summary>
		virtual public System.Decimal? KursInsentif
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunMetadata.ColumnNames.KursInsentif);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunMetadata.ColumnNames.KursInsentif, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeRemunMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeRemunMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeRemunMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRemunMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeRemunMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeRemunMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeRemunMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeRemunMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRemunMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeRemunMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeRemunMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRemunMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeRemunMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeRemunMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRemunMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeRemunMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemun.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeRemunMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeRemun entity)
			{
				this.entity = entity;
			}
			public System.String RemunID
			{
				get
				{
					System.Int32? data = entity.RemunID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunID = null;
					else entity.RemunID = Convert.ToInt32(value);
				}
			}
			public System.String RemunNo
			{
				get
				{
					System.String data = entity.RemunNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunNo = null;
					else entity.RemunNo = Convert.ToString(value);
				}
			}
			public System.String PeriodYear
			{
				get
				{
					System.Int32? data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToInt32(value);
				}
			}
			public System.String PeriodMonth
			{
				get
				{
					System.Int32? data = entity.PeriodMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonth = null;
					else entity.PeriodMonth = Convert.ToInt32(value);
				}
			}
			public System.String FundAllocProcedure
			{
				get
				{
					System.Decimal? data = entity.FundAllocProcedure;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundAllocProcedure = null;
					else entity.FundAllocProcedure = Convert.ToDecimal(value);
				}
			}
			public System.String FundAllocPosition
			{
				get
				{
					System.Decimal? data = entity.FundAllocPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundAllocPosition = null;
					else entity.FundAllocPosition = Convert.ToDecimal(value);
				}
			}
			public System.String FundAllocInsetif
			{
				get
				{
					System.Decimal? data = entity.FundAllocInsetif;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundAllocInsetif = null;
					else entity.FundAllocInsetif = Convert.ToDecimal(value);
				}
			}
			public System.String KursPosition
			{
				get
				{
					System.Decimal? data = entity.KursPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KursPosition = null;
					else entity.KursPosition = Convert.ToDecimal(value);
				}
			}
			public System.String KursInsentif
			{
				get
				{
					System.Decimal? data = entity.KursInsentif;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KursInsentif = null;
					else entity.KursInsentif = Convert.ToDecimal(value);
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
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			private esEmployeeRemun entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeRemunQuery query)
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
				throw new Exception("esEmployeeRemun can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeRemun : esEmployeeRemun
	{	
	}

	[Serializable]
	abstract public class esEmployeeRemunQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeRemunMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RemunNo
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.RemunNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.PeriodYear, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PeriodMonth
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.PeriodMonth, esSystemType.Int32);
			}
		} 
			
		public esQueryItem FundAllocProcedure
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.FundAllocProcedure, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FundAllocPosition
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.FundAllocPosition, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FundAllocInsetif
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.FundAllocInsetif, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem KursPosition
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.KursPosition, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem KursInsentif
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.KursInsentif, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeRemunCollection")]
	public partial class EmployeeRemunCollection : esEmployeeRemunCollection, IEnumerable< EmployeeRemun>
	{
		public EmployeeRemunCollection()
		{

		}	
		
		public static implicit operator List< EmployeeRemun>(EmployeeRemunCollection coll)
		{
			List< EmployeeRemun> list = new List< EmployeeRemun>();
			
			foreach (EmployeeRemun emp in coll)
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
				return  EmployeeRemunMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeRemunQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeRemun(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeRemun();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public EmployeeRemunQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeRemunQuery();
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
		public bool Load(EmployeeRemunQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeRemun AddNew()
		{
			EmployeeRemun entity = base.AddNewEntity() as EmployeeRemun;
			
			return entity;		
		}
		public EmployeeRemun FindByPrimaryKey(Int32 remunID)
		{
			return base.FindByPrimaryKey(remunID) as EmployeeRemun;
		}

		#region IEnumerable< EmployeeRemun> Members

		IEnumerator< EmployeeRemun> IEnumerable< EmployeeRemun>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeRemun;
			}
		}

		#endregion
		
		private EmployeeRemunQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeRemun' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeRemun ({RemunID})")]
	[Serializable]
	public partial class EmployeeRemun : esEmployeeRemun
	{
		public EmployeeRemun()
		{
		}	
	
		public EmployeeRemun(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeRemunMetadata.Meta();
			}
		}	
	
		override protected esEmployeeRemunQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeRemunQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public EmployeeRemunQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeRemunQuery();
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
		public bool Load(EmployeeRemunQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private EmployeeRemunQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeRemunQuery : esEmployeeRemunQuery
	{
		public EmployeeRemunQuery()
		{

		}		
		
		public EmployeeRemunQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "EmployeeRemunQuery";
        }
	}

	[Serializable]
	public partial class EmployeeRemunMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeRemunMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.RemunID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.RemunID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.RemunNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.RemunNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.PeriodYear, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.PeriodYear;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.PeriodMonth, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.PeriodMonth;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.FundAllocProcedure, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.FundAllocProcedure;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.FundAllocPosition, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.FundAllocPosition;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.FundAllocInsetif, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.FundAllocInsetif;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.KursPosition, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.KursPosition;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.KursInsentif, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.KursInsentif;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.Notes, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.IsApproved, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.IsApproved;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.ApprovedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.ApprovedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.VoidDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.VoidByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.CreateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.CreateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public EmployeeRemunMetadata Meta()
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
			public const string RemunID = "RemunID";
			public const string RemunNo = "RemunNo";
			public const string PeriodYear = "PeriodYear";
			public const string PeriodMonth = "PeriodMonth";
			public const string FundAllocProcedure = "FundAllocProcedure";
			public const string FundAllocPosition = "FundAllocPosition";
			public const string FundAllocInsetif = "FundAllocInsetif";
			public const string KursPosition = "KursPosition";
			public const string KursInsentif = "KursInsentif";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RemunID = "RemunID";
			public const string RemunNo = "RemunNo";
			public const string PeriodYear = "PeriodYear";
			public const string PeriodMonth = "PeriodMonth";
			public const string FundAllocProcedure = "FundAllocProcedure";
			public const string FundAllocPosition = "FundAllocPosition";
			public const string FundAllocInsetif = "FundAllocInsetif";
			public const string KursPosition = "KursPosition";
			public const string KursInsentif = "KursInsentif";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(EmployeeRemunMetadata))
			{
				if(EmployeeRemunMetadata.mapDelegates == null)
				{
					EmployeeRemunMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeRemunMetadata.meta == null)
				{
					EmployeeRemunMetadata.meta = new EmployeeRemunMetadata();
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
				
				meta.AddTypeMap("RemunID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RemunNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PeriodMonth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FundAllocProcedure", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FundAllocPosition", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("FundAllocInsetif", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("KursPosition", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("KursInsentif", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "EmployeeRemun";
				meta.Destination = "EmployeeRemun";
				meta.spInsert = "proc_EmployeeRemunInsert";				
				meta.spUpdate = "proc_EmployeeRemunUpdate";		
				meta.spDelete = "proc_EmployeeRemunDelete";
				meta.spLoadAll = "proc_EmployeeRemunLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeRemunLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeRemunMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
