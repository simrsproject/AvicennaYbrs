/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/27/2019 6:41:13 PM
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
	abstract public class esParamedicFeeDeductionsCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeDeductionsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeDeductionsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeDeductionsQuery query)
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
			this.InitQuery(query as esParamedicFeeDeductionsQuery);
		}
		#endregion
			
		virtual public ParamedicFeeDeductions DetachEntity(ParamedicFeeDeductions entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeDeductions;
		}
		
		virtual public ParamedicFeeDeductions AttachEntity(ParamedicFeeDeductions entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeDeductions;
		}
		
		virtual public void Combine(ParamedicFeeDeductionsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeDeductions this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeDeductions;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeDeductions);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeDeductions : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeDeductionsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeDeductions()
		{
		}
	
		public esParamedicFeeDeductions(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID, Int32 deductionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID, deductionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID, deductionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String tariffComponentID, Int32 deductionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID, deductionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID, deductionID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String tariffComponentID, Int32 deductionID)
		{
			esParamedicFeeDeductionsQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.SequenceNo==sequenceNo, query.TariffComponentID==tariffComponentID, query.DeductionID==deductionID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String tariffComponentID, Int32 deductionID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("SequenceNo",sequenceNo);
			parms.Add("TariffComponentID",tariffComponentID);
			parms.Add("DeductionID",deductionID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "DeductionID": this.str.DeductionID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "RegistrationNoMergeTo": this.str.RegistrationNoMergeTo = (string)value; break;
						case "IsCalculatedInPercent": this.str.IsCalculatedInPercent = (string)value; break;
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "CalculatedAmount": this.str.CalculatedAmount = (string)value; break;
						case "DeductionAmount": this.str.DeductionAmount = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsAfterTax": this.str.IsAfterTax = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DeductionID":
						
							if (value == null || value is System.Int32)
								this.DeductionID = (System.Int32?)value;
							break;
						case "IsCalculatedInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsCalculatedInPercent = (System.Boolean?)value;
							break;
						case "CalculatedAmount":
						
							if (value == null || value is System.Decimal)
								this.CalculatedAmount = (System.Decimal?)value;
							break;
						case "DeductionAmount":
						
							if (value == null || value is System.Decimal)
								this.DeductionAmount = (System.Decimal?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsAfterTax":
						
							if (value == null || value is System.Boolean)
								this.IsAfterTax = (System.Boolean?)value;
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
		/// Maps to ParamedicFeeDeductions.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.DeductionID
		/// </summary>
		virtual public System.Int32? DeductionID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeDeductionsMetadata.ColumnNames.DeductionID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeDeductionsMetadata.ColumnNames.DeductionID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.RegistrationNoMergeTo
		/// </summary>
		virtual public System.String RegistrationNoMergeTo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNoMergeTo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNoMergeTo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.IsCalculatedInPercent
		/// </summary>
		virtual public System.Boolean? IsCalculatedInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeDeductionsMetadata.ColumnNames.IsCalculatedInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeDeductionsMetadata.ColumnNames.IsCalculatedInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.CalculatedAmount
		/// </summary>
		virtual public System.Decimal? CalculatedAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeDeductionsMetadata.ColumnNames.CalculatedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeDeductionsMetadata.ColumnNames.CalculatedAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.DeductionAmount
		/// </summary>
		virtual public System.Decimal? DeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeDeductionsMetadata.ColumnNames.DeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeDeductionsMetadata.ColumnNames.DeductionAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeDeductionsMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeDeductionsMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductions.IsAfterTax
		/// </summary>
		virtual public System.Boolean? IsAfterTax
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeDeductionsMetadata.ColumnNames.IsAfterTax);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeDeductionsMetadata.ColumnNames.IsAfterTax, value);
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
			public esStrings(esParamedicFeeDeductions entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
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
			public System.String DeductionID
			{
				get
				{
					System.Int32? data = entity.DeductionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionID = null;
					else entity.DeductionID = Convert.ToInt32(value);
				}
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String RegistrationNoMergeTo
			{
				get
				{
					System.String data = entity.RegistrationNoMergeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNoMergeTo = null;
					else entity.RegistrationNoMergeTo = Convert.ToString(value);
				}
			}
			public System.String IsCalculatedInPercent
			{
				get
				{
					System.Boolean? data = entity.IsCalculatedInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCalculatedInPercent = null;
					else entity.IsCalculatedInPercent = Convert.ToBoolean(value);
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
			public System.String CalculatedAmount
			{
				get
				{
					System.Decimal? data = entity.CalculatedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CalculatedAmount = null;
					else entity.CalculatedAmount = Convert.ToDecimal(value);
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
			public System.String IsAfterTax
			{
				get
				{
					System.Boolean? data = entity.IsAfterTax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAfterTax = null;
					else entity.IsAfterTax = Convert.ToBoolean(value);
				}
			}
			private esParamedicFeeDeductions entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeDeductionsQuery query)
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
				throw new Exception("esParamedicFeeDeductions can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeDeductions : esParamedicFeeDeductions
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeDeductionsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeDeductionsMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem DeductionID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.DeductionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNoMergeTo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNoMergeTo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsCalculatedInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.IsCalculatedInPercent, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem CalculatedAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.CalculatedAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.DeductionAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsAfterTax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionsMetadata.ColumnNames.IsAfterTax, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeDeductionsCollection")]
	public partial class ParamedicFeeDeductionsCollection : esParamedicFeeDeductionsCollection, IEnumerable< ParamedicFeeDeductions>
	{
		public ParamedicFeeDeductionsCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeDeductions>(ParamedicFeeDeductionsCollection coll)
		{
			List< ParamedicFeeDeductions> list = new List< ParamedicFeeDeductions>();
			
			foreach (ParamedicFeeDeductions emp in coll)
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
				return  ParamedicFeeDeductionsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeDeductionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeDeductions(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeDeductions();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeDeductionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeDeductionsQuery();
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
		public bool Load(ParamedicFeeDeductionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeDeductions AddNew()
		{
			ParamedicFeeDeductions entity = base.AddNewEntity() as ParamedicFeeDeductions;
			
			return entity;		
		}
		public ParamedicFeeDeductions FindByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID, Int32 deductionID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID, deductionID) as ParamedicFeeDeductions;
		}

		#region IEnumerable< ParamedicFeeDeductions> Members

		IEnumerator< ParamedicFeeDeductions> IEnumerable< ParamedicFeeDeductions>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeDeductions;
			}
		}

		#endregion
		
		private ParamedicFeeDeductionsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeDeductions' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeDeductions ({TransactionNo, SequenceNo, TariffComponentID, DeductionID})")]
	[Serializable]
	public partial class ParamedicFeeDeductions : esParamedicFeeDeductions
	{
		public ParamedicFeeDeductions()
		{
		}	
	
		public ParamedicFeeDeductions(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeDeductionsMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeDeductionsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeDeductionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeDeductionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeDeductionsQuery();
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
		public bool Load(ParamedicFeeDeductionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeDeductionsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeDeductionsQuery : esParamedicFeeDeductionsQuery
	{
		public ParamedicFeeDeductionsQuery()
		{

		}		
		
		public ParamedicFeeDeductionsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeDeductionsQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeDeductionsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeDeductionsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.ParamedicID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.DeductionID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.DeductionID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.RegistrationNoMergeTo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.RegistrationNoMergeTo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.IsCalculatedInPercent, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.IsCalculatedInPercent;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.VerificationNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.CalculatedAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.CalculatedAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.DeductionAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.DeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.CreatedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.CreatedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionsMetadata.ColumnNames.IsAfterTax, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeDeductionsMetadata.PropertyNames.IsAfterTax;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeDeductionsMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string ParamedicID = "ParamedicID";
			public const string DeductionID = "DeductionID";
			public const string RegistrationNo = "RegistrationNo";
			public const string RegistrationNoMergeTo = "RegistrationNoMergeTo";
			public const string IsCalculatedInPercent = "IsCalculatedInPercent";
			public const string VerificationNo = "VerificationNo";
			public const string CalculatedAmount = "CalculatedAmount";
			public const string DeductionAmount = "DeductionAmount";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsAfterTax = "IsAfterTax";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string ParamedicID = "ParamedicID";
			public const string DeductionID = "DeductionID";
			public const string RegistrationNo = "RegistrationNo";
			public const string RegistrationNoMergeTo = "RegistrationNoMergeTo";
			public const string IsCalculatedInPercent = "IsCalculatedInPercent";
			public const string VerificationNo = "VerificationNo";
			public const string CalculatedAmount = "CalculatedAmount";
			public const string DeductionAmount = "DeductionAmount";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsAfterTax = "IsAfterTax";
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
			lock (typeof(ParamedicFeeDeductionsMetadata))
			{
				if(ParamedicFeeDeductionsMetadata.mapDelegates == null)
				{
					ParamedicFeeDeductionsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeDeductionsMetadata.meta == null)
				{
					ParamedicFeeDeductionsMetadata.meta = new ParamedicFeeDeductionsMetadata();
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
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DeductionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNoMergeTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCalculatedInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CalculatedAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsAfterTax", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "ParamedicFeeDeductions";
				meta.Destination = "ParamedicFeeDeductions";
				meta.spInsert = "proc_ParamedicFeeDeductionsInsert";				
				meta.spUpdate = "proc_ParamedicFeeDeductionsUpdate";		
				meta.spDelete = "proc_ParamedicFeeDeductionsDelete";
				meta.spLoadAll = "proc_ParamedicFeeDeductionsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeDeductionsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeDeductionsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
