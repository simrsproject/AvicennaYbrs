/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/27/2019 6:44:12 PM
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
	abstract public class esParamedicFeeRemunCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeRemunCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeRemunCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunQuery query)
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
			this.InitQuery(query as esParamedicFeeRemunQuery);
		}
		#endregion
			
		virtual public ParamedicFeeRemun DetachEntity(ParamedicFeeRemun entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeRemun;
		}
		
		virtual public ParamedicFeeRemun AttachEntity(ParamedicFeeRemun entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeRemun;
		}
		
		virtual public void Combine(ParamedicFeeRemunCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeRemun this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeRemun;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeRemun);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeRemun : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeRemunQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeRemun()
		{
		}
	
		public esParamedicFeeRemun(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paramedicID, Int32 year, Int32 month)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, year, month);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, year, month);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paramedicID, Int32 year, Int32 month)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, year, month);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, year, month);
		}
	
		private bool LoadByPrimaryKeyDynamic(String paramedicID, Int32 year, Int32 month)
		{
			esParamedicFeeRemunQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID==paramedicID, query.Year==year, query.Month==month);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String paramedicID, Int32 year, Int32 month)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID",paramedicID);
			parms.Add("Year",year);
			parms.Add("Month",month);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "Year": this.str.Year = (string)value; break;
						case "Month": this.str.Month = (string)value; break;
						case "Claim": this.str.Claim = (string)value; break;
						case "PercentOfClaim": this.str.PercentOfClaim = (string)value; break;
						case "FeeClaim": this.str.FeeClaim = (string)value; break;
						case "Additional": this.str.Additional = (string)value; break;
						case "Deduction": this.str.Deduction = (string)value; break;
						case "DeductionConvertion": this.str.DeductionConvertion = (string)value; break;
						case "DeductionAnesthetic": this.str.DeductionAnesthetic = (string)value; break;
						case "DeductionResult": this.str.DeductionResult = (string)value; break;
						case "Performance": this.str.Performance = (string)value; break;
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastCalculatedByUserID": this.str.LastCalculatedByUserID = (string)value; break;
						case "LastCalculatedDateTime": this.str.LastCalculatedDateTime = (string)value; break;
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Year":
						
							if (value == null || value is System.Int32)
								this.Year = (System.Int32?)value;
							break;
						case "Month":
						
							if (value == null || value is System.Int32)
								this.Month = (System.Int32?)value;
							break;
						case "Claim":
						
							if (value == null || value is System.Decimal)
								this.Claim = (System.Decimal?)value;
							break;
						case "PercentOfClaim":
						
							if (value == null || value is System.Decimal)
								this.PercentOfClaim = (System.Decimal?)value;
							break;
						case "FeeClaim":
						
							if (value == null || value is System.Decimal)
								this.FeeClaim = (System.Decimal?)value;
							break;
						case "Additional":
						
							if (value == null || value is System.Decimal)
								this.Additional = (System.Decimal?)value;
							break;
						case "Deduction":
						
							if (value == null || value is System.Decimal)
								this.Deduction = (System.Decimal?)value;
							break;
						case "DeductionConvertion":
						
							if (value == null || value is System.Decimal)
								this.DeductionConvertion = (System.Decimal?)value;
							break;
						case "DeductionAnesthetic":
						
							if (value == null || value is System.Decimal)
								this.DeductionAnesthetic = (System.Decimal?)value;
							break;
						case "DeductionResult":
						
							if (value == null || value is System.Decimal)
								this.DeductionResult = (System.Decimal?)value;
							break;
						case "Performance":
						
							if (value == null || value is System.Decimal)
								this.Performance = (System.Decimal?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LastCalculatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastCalculatedDateTime = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeRemun.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.Year
		/// </summary>
		virtual public System.Int32? Year
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunMetadata.ColumnNames.Year, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.Month
		/// </summary>
		virtual public System.Int32? Month
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunMetadata.ColumnNames.Month, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.Claim
		/// </summary>
		virtual public System.Decimal? Claim
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Claim);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Claim, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.PercentOfClaim
		/// </summary>
		virtual public System.Decimal? PercentOfClaim
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.PercentOfClaim);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.PercentOfClaim, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.FeeClaim
		/// </summary>
		virtual public System.Decimal? FeeClaim
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.FeeClaim);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.FeeClaim, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.Additional
		/// </summary>
		virtual public System.Decimal? Additional
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Additional);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Additional, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.Deduction
		/// </summary>
		virtual public System.Decimal? Deduction
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Deduction);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Deduction, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.DeductionConvertion
		/// </summary>
		virtual public System.Decimal? DeductionConvertion
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.DeductionConvertion);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.DeductionConvertion, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.DeductionAnesthetic
		/// </summary>
		virtual public System.Decimal? DeductionAnesthetic
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.DeductionAnesthetic);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.DeductionAnesthetic, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.DeductionResult
		/// </summary>
		virtual public System.Decimal? DeductionResult
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.DeductionResult);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.DeductionResult, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.Performance
		/// </summary>
		virtual public System.Decimal? Performance
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Performance);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunMetadata.ColumnNames.Performance, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.LastCalculatedByUserID
		/// </summary>
		virtual public System.String LastCalculatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.LastCalculatedDateTime
		/// </summary>
		virtual public System.DateTime? LastCalculatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemun.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunMetadata.ColumnNames.PaymentGroupNo, value);
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
			public esStrings(esParamedicFeeRemun entity)
			{
				this.entity = entity;
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String Year
			{
				get
				{
					System.Int32? data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToInt32(value);
				}
			}
			public System.String Month
			{
				get
				{
					System.Int32? data = entity.Month;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month = null;
					else entity.Month = Convert.ToInt32(value);
				}
			}
			public System.String Claim
			{
				get
				{
					System.Decimal? data = entity.Claim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Claim = null;
					else entity.Claim = Convert.ToDecimal(value);
				}
			}
			public System.String PercentOfClaim
			{
				get
				{
					System.Decimal? data = entity.PercentOfClaim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentOfClaim = null;
					else entity.PercentOfClaim = Convert.ToDecimal(value);
				}
			}
			public System.String FeeClaim
			{
				get
				{
					System.Decimal? data = entity.FeeClaim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeClaim = null;
					else entity.FeeClaim = Convert.ToDecimal(value);
				}
			}
			public System.String Additional
			{
				get
				{
					System.Decimal? data = entity.Additional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Additional = null;
					else entity.Additional = Convert.ToDecimal(value);
				}
			}
			public System.String Deduction
			{
				get
				{
					System.Decimal? data = entity.Deduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Deduction = null;
					else entity.Deduction = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionConvertion
			{
				get
				{
					System.Decimal? data = entity.DeductionConvertion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionConvertion = null;
					else entity.DeductionConvertion = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionAnesthetic
			{
				get
				{
					System.Decimal? data = entity.DeductionAnesthetic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionAnesthetic = null;
					else entity.DeductionAnesthetic = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionResult
			{
				get
				{
					System.Decimal? data = entity.DeductionResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionResult = null;
					else entity.DeductionResult = Convert.ToDecimal(value);
				}
			}
			public System.String Performance
			{
				get
				{
					System.Decimal? data = entity.Performance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Performance = null;
					else entity.Performance = Convert.ToDecimal(value);
				}
			}
			public System.String VerificationNo
			{
				get
				{
					System.String data = entity.VerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNo = null;
					else entity.VerificationNo = Convert.ToString(value);
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
			public System.String LastCalculatedByUserID
			{
				get
				{
					System.String data = entity.LastCalculatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCalculatedByUserID = null;
					else entity.LastCalculatedByUserID = Convert.ToString(value);
				}
			}
			public System.String LastCalculatedDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCalculatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCalculatedDateTime = null;
					else entity.LastCalculatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String PaymentGroupNo
			{
				get
				{
					System.String data = entity.PaymentGroupNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentGroupNo = null;
					else entity.PaymentGroupNo = Convert.ToString(value);
				}
			}
			private esParamedicFeeRemun entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunQuery query)
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
				throw new Exception("esParamedicFeeRemun can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeRemun : esParamedicFeeRemun
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeRemunQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunMetadata.Meta();
			}
		}	
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.Year, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.Month, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Claim
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.Claim, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PercentOfClaim
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.PercentOfClaim, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeClaim
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.FeeClaim, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Additional
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.Additional, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Deduction
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.Deduction, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionConvertion
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.DeductionConvertion, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionAnesthetic
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.DeductionAnesthetic, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionResult
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.DeductionResult, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Performance
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.Performance, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastCalculatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastCalculatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeRemunCollection")]
	public partial class ParamedicFeeRemunCollection : esParamedicFeeRemunCollection, IEnumerable< ParamedicFeeRemun>
	{
		public ParamedicFeeRemunCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeRemun>(ParamedicFeeRemunCollection coll)
		{
			List< ParamedicFeeRemun> list = new List< ParamedicFeeRemun>();
			
			foreach (ParamedicFeeRemun emp in coll)
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
				return  ParamedicFeeRemunMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeRemun(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeRemun();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeRemunQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunQuery();
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
		public bool Load(ParamedicFeeRemunQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeRemun AddNew()
		{
			ParamedicFeeRemun entity = base.AddNewEntity() as ParamedicFeeRemun;
			
			return entity;		
		}
		public ParamedicFeeRemun FindByPrimaryKey(String paramedicID, Int32 year, Int32 month)
		{
			return base.FindByPrimaryKey(paramedicID, year, month) as ParamedicFeeRemun;
		}

		#region IEnumerable< ParamedicFeeRemun> Members

		IEnumerator< ParamedicFeeRemun> IEnumerable< ParamedicFeeRemun>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeRemun;
			}
		}

		#endregion
		
		private ParamedicFeeRemunQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeRemun' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeRemun ({ParamedicID, Year, Month})")]
	[Serializable]
	public partial class ParamedicFeeRemun : esParamedicFeeRemun
	{
		public ParamedicFeeRemun()
		{
		}	
	
		public ParamedicFeeRemun(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeRemunQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeRemunQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunQuery();
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
		public bool Load(ParamedicFeeRemunQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeRemunQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeRemunQuery : esParamedicFeeRemunQuery
	{
		public ParamedicFeeRemunQuery()
		{

		}		
		
		public ParamedicFeeRemunQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeRemunQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeRemunMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeRemunMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.Year, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.Year;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.Month, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.Month;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.Claim, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.Claim;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.PercentOfClaim, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.PercentOfClaim;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.FeeClaim, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.FeeClaim;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.Additional, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.Additional;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.Deduction, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.Deduction;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.DeductionConvertion, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.DeductionConvertion;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.DeductionAnesthetic, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.DeductionAnesthetic;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.DeductionResult, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.DeductionResult;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.Performance, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.Performance;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.VerificationNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.CreatedByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.CreatedDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.LastCalculatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.LastCalculatedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.LastCalculatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunMetadata.ColumnNames.PaymentGroupNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunMetadata.PropertyNames.PaymentGroupNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeRemunMetadata Meta()
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
			public const string ParamedicID = "ParamedicID";
			public const string Year = "Year";
			public const string Month = "Month";
			public const string Claim = "Claim";
			public const string PercentOfClaim = "PercentOfClaim";
			public const string FeeClaim = "FeeClaim";
			public const string Additional = "Additional";
			public const string Deduction = "Deduction";
			public const string DeductionConvertion = "DeductionConvertion";
			public const string DeductionAnesthetic = "DeductionAnesthetic";
			public const string DeductionResult = "DeductionResult";
			public const string Performance = "Performance";
			public const string VerificationNo = "VerificationNo";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			public const string PaymentGroupNo = "PaymentGroupNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ParamedicID = "ParamedicID";
			public const string Year = "Year";
			public const string Month = "Month";
			public const string Claim = "Claim";
			public const string PercentOfClaim = "PercentOfClaim";
			public const string FeeClaim = "FeeClaim";
			public const string Additional = "Additional";
			public const string Deduction = "Deduction";
			public const string DeductionConvertion = "DeductionConvertion";
			public const string DeductionAnesthetic = "DeductionAnesthetic";
			public const string DeductionResult = "DeductionResult";
			public const string Performance = "Performance";
			public const string VerificationNo = "VerificationNo";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			public const string PaymentGroupNo = "PaymentGroupNo";
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
			lock (typeof(ParamedicFeeRemunMetadata))
			{
				if(ParamedicFeeRemunMetadata.mapDelegates == null)
				{
					ParamedicFeeRemunMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeRemunMetadata.meta == null)
				{
					ParamedicFeeRemunMetadata.meta = new ParamedicFeeRemunMetadata();
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
				
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Year", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Month", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Claim", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PercentOfClaim", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FeeClaim", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Additional", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Deduction", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionConvertion", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionAnesthetic", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionResult", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Performance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCalculatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCalculatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ParamedicFeeRemun";
				meta.Destination = "ParamedicFeeRemun";
				meta.spInsert = "proc_ParamedicFeeRemunInsert";				
				meta.spUpdate = "proc_ParamedicFeeRemunUpdate";		
				meta.spDelete = "proc_ParamedicFeeRemunDelete";
				meta.spLoadAll = "proc_ParamedicFeeRemunLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeRemunLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeRemunMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
