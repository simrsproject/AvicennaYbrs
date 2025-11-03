/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/3/2018 3:13:52 PM
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
	abstract public class esParamedicFeeDeductionSettingCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeDeductionSettingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeDeductionSettingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeDeductionSettingQuery query)
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
			this.InitQuery(query as esParamedicFeeDeductionSettingQuery);
		}
		#endregion
			
		virtual public ParamedicFeeDeductionSetting DetachEntity(ParamedicFeeDeductionSetting entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeDeductionSetting;
		}
		
		virtual public ParamedicFeeDeductionSetting AttachEntity(ParamedicFeeDeductionSetting entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeDeductionSetting;
		}
		
		virtual public void Combine(ParamedicFeeDeductionSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeDeductionSetting this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeDeductionSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeDeductionSetting);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeDeductionSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeDeductionSettingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeDeductionSetting()
		{
		}
	
		public esParamedicFeeDeductionSetting(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 deductionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(deductionID);
			else
				return LoadByPrimaryKeyStoredProcedure(deductionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 deductionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(deductionID);
			else
				return LoadByPrimaryKeyStoredProcedure(deductionID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 deductionID)
		{
			esParamedicFeeDeductionSettingQuery query = this.GetDynamicQuery();
			query.Where(query.DeductionID==deductionID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 deductionID)
		{
			esParameters parms = new esParameters();
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
						case "DeductionID": this.str.DeductionID = (string)value; break;
						case "SRParamedicFeeDeduction": this.str.SRParamedicFeeDeduction = (string)value; break;
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "SRParamedicFeeDeductionMethod": this.str.SRParamedicFeeDeductionMethod = (string)value; break;
						case "IsDeductionValueInPercent": this.str.IsDeductionValueInPercent = (string)value; break;
						case "DeductionValue": this.str.DeductionValue = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "SRGuarantorType": this.str.SRGuarantorType = (string)value; break;
						case "IsMainPhysicianOnly": this.str.IsMainPhysicianOnly = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
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
						case "IsDeductionValueInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsDeductionValueInPercent = (System.Boolean?)value;
							break;
						case "DeductionValue":
						
							if (value == null || value is System.Decimal)
								this.DeductionValue = (System.Decimal?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsMainPhysicianOnly":
						
							if (value == null || value is System.Boolean)
								this.IsMainPhysicianOnly = (System.Boolean?)value;
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
		/// Maps to ParamedicFeeDeductionSetting.DeductionID
		/// </summary>
		virtual public System.Int32? DeductionID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.SRParamedicFeeDeduction
		/// </summary>
		virtual public System.String SRParamedicFeeDeduction
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeduction);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeduction, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.SRParamedicFeeDeductionMethod
		/// </summary>
		virtual public System.String SRParamedicFeeDeductionMethod
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeductionMethod);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeductionMethod, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.IsDeductionValueInPercent
		/// </summary>
		virtual public System.Boolean? IsDeductionValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsDeductionValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsDeductionValueInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.DeductionValue
		/// </summary>
		virtual public System.Decimal? DeductionValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionValue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.SRGuarantorType
		/// </summary>
		virtual public System.String SRGuarantorType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRGuarantorType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRGuarantorType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.IsMainPhysicianOnly
		/// </summary>
		virtual public System.Boolean? IsMainPhysicianOnly
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsMainPhysicianOnly);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsMainPhysicianOnly, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeDeductionSettingMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeDeductionSetting.IsAfterTax
		/// </summary>
		virtual public System.Boolean? IsAfterTax
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsAfterTax);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsAfterTax, value);
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
			public esStrings(esParamedicFeeDeductionSetting entity)
			{
				this.entity = entity;
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
			public System.String SRParamedicFeeDeduction
			{
				get
				{
					System.String data = entity.SRParamedicFeeDeduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeDeduction = null;
					else entity.SRParamedicFeeDeduction = Convert.ToString(value);
				}
			}
			public System.String SRRegistrationType
			{
				get
				{
					System.String data = entity.SRRegistrationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRegistrationType = null;
					else entity.SRRegistrationType = Convert.ToString(value);
				}
			}
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
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
			public System.String SRParamedicFeeDeductionMethod
			{
				get
				{
					System.String data = entity.SRParamedicFeeDeductionMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeDeductionMethod = null;
					else entity.SRParamedicFeeDeductionMethod = Convert.ToString(value);
				}
			}
			public System.String IsDeductionValueInPercent
			{
				get
				{
					System.Boolean? data = entity.IsDeductionValueInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeductionValueInPercent = null;
					else entity.IsDeductionValueInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String DeductionValue
			{
				get
				{
					System.Decimal? data = entity.DeductionValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionValue = null;
					else entity.DeductionValue = Convert.ToDecimal(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			public System.String SRGuarantorType
			{
				get
				{
					System.String data = entity.SRGuarantorType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGuarantorType = null;
					else entity.SRGuarantorType = Convert.ToString(value);
				}
			}
			public System.String IsMainPhysicianOnly
			{
				get
				{
					System.Boolean? data = entity.IsMainPhysicianOnly;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMainPhysicianOnly = null;
					else entity.IsMainPhysicianOnly = Convert.ToBoolean(value);
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
			private esParamedicFeeDeductionSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeDeductionSettingQuery query)
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
				throw new Exception("esParamedicFeeDeductionSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeDeductionSetting : esParamedicFeeDeductionSetting
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeDeductionSettingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeDeductionSettingMetadata.Meta();
			}
		}	
			
		public esQueryItem DeductionID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRParamedicFeeDeduction
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeduction, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicFeeDeductionMethod
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeductionMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeductionValueInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.IsDeductionValueInPercent, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem DeductionValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRGuarantorType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.SRGuarantorType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsMainPhysicianOnly
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.IsMainPhysicianOnly, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAfterTax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeDeductionSettingMetadata.ColumnNames.IsAfterTax, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeDeductionSettingCollection")]
	public partial class ParamedicFeeDeductionSettingCollection : esParamedicFeeDeductionSettingCollection, IEnumerable< ParamedicFeeDeductionSetting>
	{
		public ParamedicFeeDeductionSettingCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeDeductionSetting>(ParamedicFeeDeductionSettingCollection coll)
		{
			List< ParamedicFeeDeductionSetting> list = new List< ParamedicFeeDeductionSetting>();
			
			foreach (ParamedicFeeDeductionSetting emp in coll)
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
				return  ParamedicFeeDeductionSettingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeDeductionSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeDeductionSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeDeductionSetting();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeDeductionSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeDeductionSettingQuery();
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
		public bool Load(ParamedicFeeDeductionSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeDeductionSetting AddNew()
		{
			ParamedicFeeDeductionSetting entity = base.AddNewEntity() as ParamedicFeeDeductionSetting;
			
			return entity;		
		}
		public ParamedicFeeDeductionSetting FindByPrimaryKey(Int32 deductionID)
		{
			return base.FindByPrimaryKey(deductionID) as ParamedicFeeDeductionSetting;
		}

		#region IEnumerable< ParamedicFeeDeductionSetting> Members

		IEnumerator< ParamedicFeeDeductionSetting> IEnumerable< ParamedicFeeDeductionSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeDeductionSetting;
			}
		}

		#endregion
		
		private ParamedicFeeDeductionSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeDeductionSetting' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeDeductionSetting ({DeductionID})")]
	[Serializable]
	public partial class ParamedicFeeDeductionSetting : esParamedicFeeDeductionSetting
	{
		public ParamedicFeeDeductionSetting()
		{
		}	
	
		public ParamedicFeeDeductionSetting(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeDeductionSettingMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeDeductionSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeDeductionSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeDeductionSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeDeductionSettingQuery();
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
		public bool Load(ParamedicFeeDeductionSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeDeductionSettingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeDeductionSettingQuery : esParamedicFeeDeductionSettingQuery
	{
		public ParamedicFeeDeductionSettingQuery()
		{

		}		
		
		public ParamedicFeeDeductionSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeDeductionSettingQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeDeductionSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeDeductionSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.DeductionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeduction, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.SRParamedicFeeDeduction;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRRegistrationType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.GuarantorID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRParamedicFeeDeductionMethod, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.SRParamedicFeeDeductionMethod;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsDeductionValueInPercent, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.IsDeductionValueInPercent;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionValue, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.DeductionValue;
			c.NumericPrecision = 12;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsActive, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.CreatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.SRGuarantorType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.SRGuarantorType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsMainPhysicianOnly, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.IsMainPhysicianOnly;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.TariffComponentID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeDeductionSettingMetadata.ColumnNames.IsAfterTax, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeDeductionSettingMetadata.PropertyNames.IsAfterTax;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeDeductionSettingMetadata Meta()
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
			public const string DeductionID = "DeductionID";
			public const string SRParamedicFeeDeduction = "SRParamedicFeeDeduction";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string GuarantorID = "GuarantorID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SRParamedicFeeDeductionMethod = "SRParamedicFeeDeductionMethod";
			public const string IsDeductionValueInPercent = "IsDeductionValueInPercent";
			public const string DeductionValue = "DeductionValue";
			public const string IsActive = "IsActive";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string IsMainPhysicianOnly = "IsMainPhysicianOnly";
			public const string TariffComponentID = "TariffComponentID";
			public const string IsAfterTax = "IsAfterTax";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string DeductionID = "DeductionID";
			public const string SRParamedicFeeDeduction = "SRParamedicFeeDeduction";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string GuarantorID = "GuarantorID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SRParamedicFeeDeductionMethod = "SRParamedicFeeDeductionMethod";
			public const string IsDeductionValueInPercent = "IsDeductionValueInPercent";
			public const string DeductionValue = "DeductionValue";
			public const string IsActive = "IsActive";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string IsMainPhysicianOnly = "IsMainPhysicianOnly";
			public const string TariffComponentID = "TariffComponentID";
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
			lock (typeof(ParamedicFeeDeductionSettingMetadata))
			{
				if(ParamedicFeeDeductionSettingMetadata.mapDelegates == null)
				{
					ParamedicFeeDeductionSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeDeductionSettingMetadata.meta == null)
				{
					ParamedicFeeDeductionSettingMetadata.meta = new ParamedicFeeDeductionSettingMetadata();
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
				
				meta.AddTypeMap("DeductionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRParamedicFeeDeduction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeDeductionMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeductionValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeductionValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRGuarantorType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMainPhysicianOnly", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAfterTax", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "ParamedicFeeDeductionSetting";
				meta.Destination = "ParamedicFeeDeductionSetting";
				meta.spInsert = "proc_ParamedicFeeDeductionSettingInsert";				
				meta.spUpdate = "proc_ParamedicFeeDeductionSettingUpdate";		
				meta.spDelete = "proc_ParamedicFeeDeductionSettingDelete";
				meta.spLoadAll = "proc_ParamedicFeeDeductionSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeDeductionSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeDeductionSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
