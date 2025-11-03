/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/13/2022 5:20:10 PM
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
	abstract public class esServiceFeeRemunRsucdrCollection : esEntityCollectionWAuditLog
	{
		public esServiceFeeRemunRsucdrCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceFeeRemunRsucdrCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceFeeRemunRsucdrQuery query)
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
			this.InitQuery(query as esServiceFeeRemunRsucdrQuery);
		}
		#endregion
			
		virtual public ServiceFeeRemunRsucdr DetachEntity(ServiceFeeRemunRsucdr entity)
		{
			return base.DetachEntity(entity) as ServiceFeeRemunRsucdr;
		}
		
		virtual public ServiceFeeRemunRsucdr AttachEntity(ServiceFeeRemunRsucdr entity)
		{
			return base.AttachEntity(entity) as ServiceFeeRemunRsucdr;
		}
		
		virtual public void Combine(ServiceFeeRemunRsucdrCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceFeeRemunRsucdr this[int index]
		{
			get
			{
				return base[index] as ServiceFeeRemunRsucdr;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceFeeRemunRsucdr);
		}
	}

	[Serializable]
	abstract public class esServiceFeeRemunRsucdr : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceFeeRemunRsucdrQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceFeeRemunRsucdr()
		{
		}
	
		public esServiceFeeRemunRsucdr(DataRow row)
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
			esServiceFeeRemunRsucdrQuery query = this.GetDynamicQuery();
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
						case "IsBPJS": this.str.IsBPJS = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "DischargeDateFrom": this.str.DischargeDateFrom = (string)value; break;
						case "DischargeDateTo": this.str.DischargeDateTo = (string)value; break;
						case "InvoiceAmount": this.str.InvoiceAmount = (string)value; break;
						case "BudgetPercentage": this.str.BudgetPercentage = (string)value; break;
						case "BudgetAllocation": this.str.BudgetAllocation = (string)value; break;
						case "DeductionAmount": this.str.DeductionAmount = (string)value; break;
						case "BudgetAmount": this.str.BudgetAmount = (string)value; break;
						case "TotalFeeDirektur": this.str.TotalFeeDirektur = (string)value; break;
						case "TotalFeeStruktural": this.str.TotalFeeStruktural = (string)value; break;
						case "TotalFeeMedis": this.str.TotalFeeMedis = (string)value; break;
						case "TotalFeeMedisIgd": this.str.TotalFeeMedisIgd = (string)value; break;
						case "TotalFeeUnit": this.str.TotalFeeUnit = (string)value; break;
						case "TotalFeePemerataan": this.str.TotalFeePemerataan = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
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
						case "IsBPJS":
						
							if (value == null || value is System.Boolean)
								this.IsBPJS = (System.Boolean?)value;
							break;
						case "DischargeDateFrom":
						
							if (value == null || value is System.DateTime)
								this.DischargeDateFrom = (System.DateTime?)value;
							break;
						case "DischargeDateTo":
						
							if (value == null || value is System.DateTime)
								this.DischargeDateTo = (System.DateTime?)value;
							break;
						case "InvoiceAmount":
						
							if (value == null || value is System.Decimal)
								this.InvoiceAmount = (System.Decimal?)value;
							break;
						case "BudgetPercentage":
						
							if (value == null || value is System.Decimal)
								this.BudgetPercentage = (System.Decimal?)value;
							break;
						case "BudgetAllocation":
						
							if (value == null || value is System.Decimal)
								this.BudgetAllocation = (System.Decimal?)value;
							break;
						case "DeductionAmount":
						
							if (value == null || value is System.Decimal)
								this.DeductionAmount = (System.Decimal?)value;
							break;
						case "BudgetAmount":
						
							if (value == null || value is System.Decimal)
								this.BudgetAmount = (System.Decimal?)value;
							break;
						case "TotalFeeDirektur":
						
							if (value == null || value is System.Decimal)
								this.TotalFeeDirektur = (System.Decimal?)value;
							break;
						case "TotalFeeStruktural":
						
							if (value == null || value is System.Decimal)
								this.TotalFeeStruktural = (System.Decimal?)value;
							break;
						case "TotalFeeMedis":
						
							if (value == null || value is System.Decimal)
								this.TotalFeeMedis = (System.Decimal?)value;
							break;
						case "TotalFeeMedisIgd":
						
							if (value == null || value is System.Decimal)
								this.TotalFeeMedisIgd = (System.Decimal?)value;
							break;
						case "TotalFeeUnit":
						
							if (value == null || value is System.Decimal)
								this.TotalFeeUnit = (System.Decimal?)value;
							break;
						case "TotalFeePemerataan":
						
							if (value == null || value is System.Decimal)
								this.TotalFeePemerataan = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to ServiceFeeRemunRsucdr.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.RemunNo
		/// </summary>
		virtual public System.String RemunNo
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunNo);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.PeriodYear
		/// </summary>
		virtual public System.Int32? PeriodYear
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodYear);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.PeriodMonth
		/// </summary>
		virtual public System.Int32? PeriodMonth
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodMonth);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodMonth, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.IsBPJS
		/// </summary>
		virtual public System.Boolean? IsBPJS
		{
			get
			{
				return base.GetSystemBoolean(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsBPJS);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsBPJS, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.DischargeDateFrom
		/// </summary>
		virtual public System.DateTime? DischargeDateFrom
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateFrom);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateFrom, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.DischargeDateTo
		/// </summary>
		virtual public System.DateTime? DischargeDateTo
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateTo);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateTo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.InvoiceAmount
		/// </summary>
		virtual public System.Decimal? InvoiceAmount
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.InvoiceAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.InvoiceAmount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.BudgetPercentage
		/// </summary>
		virtual public System.Decimal? BudgetPercentage
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetPercentage);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.BudgetAllocation
		/// </summary>
		virtual public System.Decimal? BudgetAllocation
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAllocation);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAllocation, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.DeductionAmount
		/// </summary>
		virtual public System.Decimal? DeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.DeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.DeductionAmount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.BudgetAmount
		/// </summary>
		virtual public System.Decimal? BudgetAmount
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAmount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.TotalFeeDirektur
		/// </summary>
		virtual public System.Decimal? TotalFeeDirektur
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeDirektur);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeDirektur, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.TotalFeeStruktural
		/// </summary>
		virtual public System.Decimal? TotalFeeStruktural
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeStruktural);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeStruktural, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.TotalFeeMedis
		/// </summary>
		virtual public System.Decimal? TotalFeeMedis
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedis);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedis, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.TotalFeeMedisIgd
		/// </summary>
		virtual public System.Decimal? TotalFeeMedisIgd
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedisIgd);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedisIgd, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.TotalFeeUnit
		/// </summary>
		virtual public System.Decimal? TotalFeeUnit
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeUnit);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeUnit, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.TotalFeePemerataan
		/// </summary>
		virtual public System.Decimal? TotalFeePemerataan
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeePemerataan);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeePemerataan, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdr.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidDateTime, value);
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
			public esStrings(esServiceFeeRemunRsucdr entity)
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
			public System.String IsBPJS
			{
				get
				{
					System.Boolean? data = entity.IsBPJS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBPJS = null;
					else entity.IsBPJS = Convert.ToBoolean(value);
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
			public System.String DischargeDateFrom
			{
				get
				{
					System.DateTime? data = entity.DischargeDateFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDateFrom = null;
					else entity.DischargeDateFrom = Convert.ToDateTime(value);
				}
			}
			public System.String DischargeDateTo
			{
				get
				{
					System.DateTime? data = entity.DischargeDateTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDateTo = null;
					else entity.DischargeDateTo = Convert.ToDateTime(value);
				}
			}
			public System.String InvoiceAmount
			{
				get
				{
					System.Decimal? data = entity.InvoiceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceAmount = null;
					else entity.InvoiceAmount = Convert.ToDecimal(value);
				}
			}
			public System.String BudgetPercentage
			{
				get
				{
					System.Decimal? data = entity.BudgetPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetPercentage = null;
					else entity.BudgetPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String BudgetAllocation
			{
				get
				{
					System.Decimal? data = entity.BudgetAllocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetAllocation = null;
					else entity.BudgetAllocation = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionAmount
			{
				get
				{
					System.Decimal? data = entity.DeductionAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionAmount = null;
					else entity.DeductionAmount = Convert.ToDecimal(value);
				}
			}
			public System.String BudgetAmount
			{
				get
				{
					System.Decimal? data = entity.BudgetAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetAmount = null;
					else entity.BudgetAmount = Convert.ToDecimal(value);
				}
			}
			public System.String TotalFeeDirektur
			{
				get
				{
					System.Decimal? data = entity.TotalFeeDirektur;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalFeeDirektur = null;
					else entity.TotalFeeDirektur = Convert.ToDecimal(value);
				}
			}
			public System.String TotalFeeStruktural
			{
				get
				{
					System.Decimal? data = entity.TotalFeeStruktural;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalFeeStruktural = null;
					else entity.TotalFeeStruktural = Convert.ToDecimal(value);
				}
			}
			public System.String TotalFeeMedis
			{
				get
				{
					System.Decimal? data = entity.TotalFeeMedis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalFeeMedis = null;
					else entity.TotalFeeMedis = Convert.ToDecimal(value);
				}
			}
			public System.String TotalFeeMedisIgd
			{
				get
				{
					System.Decimal? data = entity.TotalFeeMedisIgd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalFeeMedisIgd = null;
					else entity.TotalFeeMedisIgd = Convert.ToDecimal(value);
				}
			}
			public System.String TotalFeeUnit
			{
				get
				{
					System.Decimal? data = entity.TotalFeeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalFeeUnit = null;
					else entity.TotalFeeUnit = Convert.ToDecimal(value);
				}
			}
			public System.String TotalFeePemerataan
			{
				get
				{
					System.Decimal? data = entity.TotalFeePemerataan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalFeePemerataan = null;
					else entity.TotalFeePemerataan = Convert.ToDecimal(value);
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
			private esServiceFeeRemunRsucdr entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceFeeRemunRsucdrQuery query)
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
				throw new Exception("esServiceFeeRemunRsucdr can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceFeeRemunRsucdr : esServiceFeeRemunRsucdr
	{	
	}

	[Serializable]
	abstract public class esServiceFeeRemunRsucdrQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRemunRsucdrMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RemunNo
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodYear, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PeriodMonth
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodMonth, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsBPJS
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.IsBPJS, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDateFrom
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateFrom, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem DischargeDateTo
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateTo, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem InvoiceAmount
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.InvoiceAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem BudgetPercentage
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetPercentage, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem BudgetAllocation
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAllocation, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionAmount
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.DeductionAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem BudgetAmount
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TotalFeeDirektur
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeDirektur, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TotalFeeStruktural
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeStruktural, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TotalFeeMedis
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedis, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TotalFeeMedisIgd
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedisIgd, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TotalFeeUnit
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeUnit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TotalFeePemerataan
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeePemerataan, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceFeeRemunRsucdrCollection")]
	public partial class ServiceFeeRemunRsucdrCollection : esServiceFeeRemunRsucdrCollection, IEnumerable< ServiceFeeRemunRsucdr>
	{
		public ServiceFeeRemunRsucdrCollection()
		{

		}	
		
		public static implicit operator List< ServiceFeeRemunRsucdr>(ServiceFeeRemunRsucdrCollection coll)
		{
			List< ServiceFeeRemunRsucdr> list = new List< ServiceFeeRemunRsucdr>();
			
			foreach (ServiceFeeRemunRsucdr emp in coll)
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
				return  ServiceFeeRemunRsucdrMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRemunRsucdrQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceFeeRemunRsucdr(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceFeeRemunRsucdr();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceFeeRemunRsucdrQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRemunRsucdrQuery();
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
		public bool Load(ServiceFeeRemunRsucdrQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceFeeRemunRsucdr AddNew()
		{
			ServiceFeeRemunRsucdr entity = base.AddNewEntity() as ServiceFeeRemunRsucdr;
			
			return entity;		
		}
		public ServiceFeeRemunRsucdr FindByPrimaryKey(Int32 remunID)
		{
			return base.FindByPrimaryKey(remunID) as ServiceFeeRemunRsucdr;
		}

		#region IEnumerable< ServiceFeeRemunRsucdr> Members

		IEnumerator< ServiceFeeRemunRsucdr> IEnumerable< ServiceFeeRemunRsucdr>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceFeeRemunRsucdr;
			}
		}

		#endregion
		
		private ServiceFeeRemunRsucdrQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceFeeRemunRsucdr' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceFeeRemunRsucdr ({RemunID})")]
	[Serializable]
	public partial class ServiceFeeRemunRsucdr : esServiceFeeRemunRsucdr
	{
		public ServiceFeeRemunRsucdr()
		{
		}	
	
		public ServiceFeeRemunRsucdr(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRemunRsucdrMetadata.Meta();
			}
		}	
	
		override protected esServiceFeeRemunRsucdrQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRemunRsucdrQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceFeeRemunRsucdrQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRemunRsucdrQuery();
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
		public bool Load(ServiceFeeRemunRsucdrQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceFeeRemunRsucdrQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceFeeRemunRsucdrQuery : esServiceFeeRemunRsucdrQuery
	{
		public ServiceFeeRemunRsucdrQuery()
		{

		}		
		
		public ServiceFeeRemunRsucdrQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceFeeRemunRsucdrQuery";
        }
	}

	[Serializable]
	public partial class ServiceFeeRemunRsucdrMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceFeeRemunRsucdrMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.RemunID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.RemunNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.RemunNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodYear, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.PeriodYear;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.PeriodMonth, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.PeriodMonth;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsBPJS, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.IsBPJS;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateFrom, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.DischargeDateFrom;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.DischargeDateTo, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.DischargeDateTo;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.InvoiceAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.InvoiceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetPercentage, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.BudgetPercentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAllocation, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.BudgetAllocation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.DeductionAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.DeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.BudgetAmount, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.BudgetAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeDirektur, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.TotalFeeDirektur;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeStruktural, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.TotalFeeStruktural;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedis, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.TotalFeeMedis;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeMedisIgd, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.TotalFeeMedisIgd;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeeUnit, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.TotalFeeUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.TotalFeePemerataan, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.TotalFeePemerataan;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.CreateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.LastUpdateDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsApproved, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.IsApproved;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedByUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.ApprovedDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.IsVoid, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrMetadata.ColumnNames.VoidDateTime, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceFeeRemunRsucdrMetadata Meta()
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
			public const string IsBPJS = "IsBPJS";
			public const string Notes = "Notes";
			public const string DischargeDateFrom = "DischargeDateFrom";
			public const string DischargeDateTo = "DischargeDateTo";
			public const string InvoiceAmount = "InvoiceAmount";
			public const string BudgetPercentage = "BudgetPercentage";
			public const string BudgetAllocation = "BudgetAllocation";
			public const string DeductionAmount = "DeductionAmount";
			public const string BudgetAmount = "BudgetAmount";
			public const string TotalFeeDirektur = "TotalFeeDirektur";
			public const string TotalFeeStruktural = "TotalFeeStruktural";
			public const string TotalFeeMedis = "TotalFeeMedis";
			public const string TotalFeeMedisIgd = "TotalFeeMedisIgd";
			public const string TotalFeeUnit = "TotalFeeUnit";
			public const string TotalFeePemerataan = "TotalFeePemerataan";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApproved = "IsApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RemunID = "RemunID";
			public const string RemunNo = "RemunNo";
			public const string PeriodYear = "PeriodYear";
			public const string PeriodMonth = "PeriodMonth";
			public const string IsBPJS = "IsBPJS";
			public const string Notes = "Notes";
			public const string DischargeDateFrom = "DischargeDateFrom";
			public const string DischargeDateTo = "DischargeDateTo";
			public const string InvoiceAmount = "InvoiceAmount";
			public const string BudgetPercentage = "BudgetPercentage";
			public const string BudgetAllocation = "BudgetAllocation";
			public const string DeductionAmount = "DeductionAmount";
			public const string BudgetAmount = "BudgetAmount";
			public const string TotalFeeDirektur = "TotalFeeDirektur";
			public const string TotalFeeStruktural = "TotalFeeStruktural";
			public const string TotalFeeMedis = "TotalFeeMedis";
			public const string TotalFeeMedisIgd = "TotalFeeMedisIgd";
			public const string TotalFeeUnit = "TotalFeeUnit";
			public const string TotalFeePemerataan = "TotalFeePemerataan";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApproved = "IsApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
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
			lock (typeof(ServiceFeeRemunRsucdrMetadata))
			{
				if(ServiceFeeRemunRsucdrMetadata.mapDelegates == null)
				{
					ServiceFeeRemunRsucdrMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceFeeRemunRsucdrMetadata.meta == null)
				{
					ServiceFeeRemunRsucdrMetadata.meta = new ServiceFeeRemunRsucdrMetadata();
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
				meta.AddTypeMap("IsBPJS", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDateFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DischargeDateTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InvoiceAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("BudgetPercentage", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("BudgetAllocation", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DeductionAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("BudgetAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalFeeDirektur", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalFeeStruktural", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalFeeMedis", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalFeeMedisIgd", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalFeeUnit", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalFeePemerataan", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ServiceFeeRemunRsucdr";
				meta.Destination = "ServiceFeeRemunRsucdr";
				meta.spInsert = "proc_ServiceFeeRemunRsucdrInsert";				
				meta.spUpdate = "proc_ServiceFeeRemunRsucdrUpdate";		
				meta.spDelete = "proc_ServiceFeeRemunRsucdrDelete";
				meta.spLoadAll = "proc_ServiceFeeRemunRsucdrLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceFeeRemunRsucdrLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceFeeRemunRsucdrMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
