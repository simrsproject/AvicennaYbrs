/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/1/2023 11:49:09 AM
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
	abstract public class esParamedicFeeByFee4ServiceSettingCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeByFee4ServiceSettingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeByFee4ServiceSettingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeByFee4ServiceSettingQuery query)
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
			this.InitQuery(query as esParamedicFeeByFee4ServiceSettingQuery);
		}
		#endregion
			
		virtual public ParamedicFeeByFee4ServiceSetting DetachEntity(ParamedicFeeByFee4ServiceSetting entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeByFee4ServiceSetting;
		}
		
		virtual public ParamedicFeeByFee4ServiceSetting AttachEntity(ParamedicFeeByFee4ServiceSetting entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeByFee4ServiceSetting;
		}
		
		virtual public void Combine(ParamedicFeeByFee4ServiceSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeByFee4ServiceSetting this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeByFee4ServiceSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeByFee4ServiceSetting);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeByFee4ServiceSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeByFee4ServiceSettingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeByFee4ServiceSetting()
		{
		}
	
		public esParamedicFeeByFee4ServiceSetting(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 id)
		{
			esParamedicFeeByFee4ServiceSettingQuery query = this.GetDynamicQuery();
			query.Where(query.Id==id);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id",id);
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
						case "Id": this.str.Id = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "SRSpecialty": this.str.SRSpecialty = (string)value; break;
						case "SRTariffType": this.str.SRTariffType = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "IsFeeValueInPercent": this.str.IsFeeValueInPercent = (string)value; break;
						case "FeeValue": this.str.FeeValue = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "SRProcedure": this.str.SRProcedure = (string)value; break;
						case "IsFeeValueFromPlafon": this.str.IsFeeValueFromPlafon = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "IsFeeValueFromTariffPrice": this.str.IsFeeValueFromTariffPrice = (string)value; break;
						case "SRGuarantorType": this.str.SRGuarantorType = (string)value; break;
						case "SRParamedicStatus": this.str.SRParamedicStatus = (string)value; break;
						case "Formula": this.str.Formula = (string)value; break;
						case "IsUsingFormula": this.str.IsUsingFormula = (string)value; break;
						case "Level": this.str.Level = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "SRItemConditionRuleType": this.str.SRItemConditionRuleType = (string)value; break;
						case "ItemConditionRuleID": this.str.ItemConditionRuleID = (string)value; break;
						case "FormulaNetto": this.str.FormulaNetto = (string)value; break;
						case "SRRegistrationTypeMergeBilling": this.str.SRRegistrationTypeMergeBilling = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
							break;
						case "IsFeeValueInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsFeeValueInPercent = (System.Boolean?)value;
							break;
						case "FeeValue":
						
							if (value == null || value is System.Decimal)
								this.FeeValue = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsFeeValueFromPlafon":
						
							if (value == null || value is System.Boolean)
								this.IsFeeValueFromPlafon = (System.Boolean?)value;
							break;
						case "IsFeeValueFromTariffPrice":
						
							if (value == null || value is System.Boolean)
								this.IsFeeValueFromTariffPrice = (System.Boolean?)value;
							break;
						case "IsUsingFormula":
						
							if (value == null || value is System.Boolean)
								this.IsUsingFormula = (System.Boolean?)value;
							break;
						case "Level":
						
							if (value == null || value is System.Int32)
								this.Level = (System.Int32?)value;
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
		/// Maps to ParamedicFeeByFee4ServiceSetting.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Id, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRSpecialty
		/// </summary>
		virtual public System.String SRSpecialty
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRSpecialty);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRSpecialty, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRTariffType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemGroupID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.IsFeeValueInPercent
		/// </summary>
		virtual public System.Boolean? IsFeeValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.FeeValue
		/// </summary>
		virtual public System.Decimal? FeeValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FeeValue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRProcedure
		/// </summary>
		virtual public System.String SRProcedure
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRProcedure);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRProcedure, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.IsFeeValueFromPlafon
		/// </summary>
		virtual public System.Boolean? IsFeeValueFromPlafon
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromPlafon);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromPlafon, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.IsFeeValueFromTariffPrice
		/// </summary>
		virtual public System.Boolean? IsFeeValueFromTariffPrice
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromTariffPrice);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromTariffPrice, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRGuarantorType
		/// </summary>
		virtual public System.String SRGuarantorType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRGuarantorType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRGuarantorType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRParamedicStatus
		/// </summary>
		virtual public System.String SRParamedicStatus
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRParamedicStatus);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRParamedicStatus, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.Formula
		/// </summary>
		virtual public System.String Formula
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Formula);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Formula, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.IsUsingFormula
		/// </summary>
		virtual public System.Boolean? IsUsingFormula
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsUsingFormula);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsUsingFormula, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.Level
		/// </summary>
		virtual public System.Int32? Level
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Level);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Level, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRItemConditionRuleType
		/// </summary>
		virtual public System.String SRItemConditionRuleType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRItemConditionRuleType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRItemConditionRuleType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.ItemConditionRuleID
		/// </summary>
		virtual public System.String ItemConditionRuleID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemConditionRuleID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemConditionRuleID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.FormulaNetto
		/// </summary>
		virtual public System.String FormulaNetto
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FormulaNetto);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FormulaNetto, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByFee4ServiceSetting.SRRegistrationTypeMergeBilling
		/// </summary>
		virtual public System.String SRRegistrationTypeMergeBilling
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationTypeMergeBilling);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationTypeMergeBilling, value);
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
			public esStrings(esParamedicFeeByFee4ServiceSetting entity)
			{
				this.entity = entity;
			}
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
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
			public System.String SRSpecialty
			{
				get
				{
					System.String data = entity.SRSpecialty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSpecialty = null;
					else entity.SRSpecialty = Convert.ToString(value);
				}
			}
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
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
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
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
			public System.String IsFeeValueInPercent
			{
				get
				{
					System.Boolean? data = entity.IsFeeValueInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeeValueInPercent = null;
					else entity.IsFeeValueInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String FeeValue
			{
				get
				{
					System.Decimal? data = entity.FeeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeValue = null;
					else entity.FeeValue = Convert.ToDecimal(value);
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
			public System.String SRProcedure
			{
				get
				{
					System.String data = entity.SRProcedure;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedure = null;
					else entity.SRProcedure = Convert.ToString(value);
				}
			}
			public System.String IsFeeValueFromPlafon
			{
				get
				{
					System.Boolean? data = entity.IsFeeValueFromPlafon;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeeValueFromPlafon = null;
					else entity.IsFeeValueFromPlafon = Convert.ToBoolean(value);
				}
			}
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
			public System.String IsFeeValueFromTariffPrice
			{
				get
				{
					System.Boolean? data = entity.IsFeeValueFromTariffPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeeValueFromTariffPrice = null;
					else entity.IsFeeValueFromTariffPrice = Convert.ToBoolean(value);
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
			public System.String SRParamedicStatus
			{
				get
				{
					System.String data = entity.SRParamedicStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicStatus = null;
					else entity.SRParamedicStatus = Convert.ToString(value);
				}
			}
			public System.String Formula
			{
				get
				{
					System.String data = entity.Formula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Formula = null;
					else entity.Formula = Convert.ToString(value);
				}
			}
			public System.String IsUsingFormula
			{
				get
				{
					System.Boolean? data = entity.IsUsingFormula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingFormula = null;
					else entity.IsUsingFormula = Convert.ToBoolean(value);
				}
			}
			public System.String Level
			{
				get
				{
					System.Int32? data = entity.Level;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Level = null;
					else entity.Level = Convert.ToInt32(value);
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
			public System.String SRItemConditionRuleType
			{
				get
				{
					System.String data = entity.SRItemConditionRuleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemConditionRuleType = null;
					else entity.SRItemConditionRuleType = Convert.ToString(value);
				}
			}
			public System.String ItemConditionRuleID
			{
				get
				{
					System.String data = entity.ItemConditionRuleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemConditionRuleID = null;
					else entity.ItemConditionRuleID = Convert.ToString(value);
				}
			}
			public System.String FormulaNetto
			{
				get
				{
					System.String data = entity.FormulaNetto;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaNetto = null;
					else entity.FormulaNetto = Convert.ToString(value);
				}
			}
			public System.String SRRegistrationTypeMergeBilling
			{
				get
				{
					System.String data = entity.SRRegistrationTypeMergeBilling;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRegistrationTypeMergeBilling = null;
					else entity.SRRegistrationTypeMergeBilling = Convert.ToString(value);
				}
			}
			private esParamedicFeeByFee4ServiceSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeByFee4ServiceSettingQuery query)
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
				throw new Exception("esParamedicFeeByFee4ServiceSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeByFee4ServiceSetting : esParamedicFeeByFee4ServiceSetting
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeByFee4ServiceSettingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByFee4ServiceSettingMetadata.Meta();
			}
		}	
			
		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRSpecialty
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRSpecialty, esSystemType.String);
			}
		} 
			
		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsFeeValueInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueInPercent, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem FeeValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FeeValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRProcedure
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRProcedure, esSystemType.String);
			}
		} 
			
		public esQueryItem IsFeeValueFromPlafon
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromPlafon, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsFeeValueFromTariffPrice
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromTariffPrice, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRGuarantorType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRGuarantorType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicStatus
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRParamedicStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem Formula
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Formula, esSystemType.String);
			}
		} 
			
		public esQueryItem IsUsingFormula
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsUsingFormula, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Level
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Level, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem SRItemConditionRuleType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRItemConditionRuleType, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemConditionRuleID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemConditionRuleID, esSystemType.String);
			}
		} 
			
		public esQueryItem FormulaNetto
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FormulaNetto, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRegistrationTypeMergeBilling
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationTypeMergeBilling, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeByFee4ServiceSettingCollection")]
	public partial class ParamedicFeeByFee4ServiceSettingCollection : esParamedicFeeByFee4ServiceSettingCollection, IEnumerable< ParamedicFeeByFee4ServiceSetting>
	{
		public ParamedicFeeByFee4ServiceSettingCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeByFee4ServiceSetting>(ParamedicFeeByFee4ServiceSettingCollection coll)
		{
			List< ParamedicFeeByFee4ServiceSetting> list = new List< ParamedicFeeByFee4ServiceSetting>();
			
			foreach (ParamedicFeeByFee4ServiceSetting emp in coll)
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
				return  ParamedicFeeByFee4ServiceSettingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByFee4ServiceSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeByFee4ServiceSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeByFee4ServiceSetting();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeByFee4ServiceSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByFee4ServiceSettingQuery();
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
		public bool Load(ParamedicFeeByFee4ServiceSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeByFee4ServiceSetting AddNew()
		{
			ParamedicFeeByFee4ServiceSetting entity = base.AddNewEntity() as ParamedicFeeByFee4ServiceSetting;
			
			return entity;		
		}
		public ParamedicFeeByFee4ServiceSetting FindByPrimaryKey(Int32 id)
		{
			return base.FindByPrimaryKey(id) as ParamedicFeeByFee4ServiceSetting;
		}

		#region IEnumerable< ParamedicFeeByFee4ServiceSetting> Members

		IEnumerator< ParamedicFeeByFee4ServiceSetting> IEnumerable< ParamedicFeeByFee4ServiceSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeByFee4ServiceSetting;
			}
		}

		#endregion
		
		private ParamedicFeeByFee4ServiceSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeByFee4ServiceSetting' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeByFee4ServiceSetting ({Id})")]
	[Serializable]
	public partial class ParamedicFeeByFee4ServiceSetting : esParamedicFeeByFee4ServiceSetting
	{
		public ParamedicFeeByFee4ServiceSetting()
		{
		}	
	
		public ParamedicFeeByFee4ServiceSetting(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByFee4ServiceSettingMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeByFee4ServiceSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByFee4ServiceSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeByFee4ServiceSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByFee4ServiceSettingQuery();
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
		public bool Load(ParamedicFeeByFee4ServiceSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeByFee4ServiceSettingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeByFee4ServiceSettingQuery : esParamedicFeeByFee4ServiceSettingQuery
	{
		public ParamedicFeeByFee4ServiceSettingQuery()
		{

		}		
		
		public ParamedicFeeByFee4ServiceSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeByFee4ServiceSettingQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeByFee4ServiceSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeByFee4ServiceSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRSpecialty, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRSpecialty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRTariffType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRTariffType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.GuarantorID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemGroupID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.TariffComponentID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueInPercent, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.IsFeeValueInPercent;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FeeValue, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.FeeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.CreateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRProcedure, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRProcedure;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromPlafon, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.IsFeeValueFromPlafon;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ClassID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsFeeValueFromTariffPrice, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.IsFeeValueFromTariffPrice;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRGuarantorType, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRGuarantorType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRParamedicStatus, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRParamedicStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Formula, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.Formula;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.IsUsingFormula, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.IsUsingFormula;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Level, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.Level;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Notes, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRItemConditionRuleType, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRItemConditionRuleType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.ItemConditionRuleID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.ItemConditionRuleID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.FormulaNetto, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.FormulaNetto;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.SRRegistrationTypeMergeBilling, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByFee4ServiceSettingMetadata.PropertyNames.SRRegistrationTypeMergeBilling;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeByFee4ServiceSettingMetadata Meta()
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
			public const string Id = "Id";
			public const string ParamedicID = "ParamedicID";
			public const string SRSpecialty = "SRSpecialty";
			public const string SRTariffType = "SRTariffType";
			public const string GuarantorID = "GuarantorID";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemID = "ItemID";
			public const string TariffComponentID = "TariffComponentID";
			public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			public const string FeeValue = "FeeValue";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRProcedure = "SRProcedure";
			public const string IsFeeValueFromPlafon = "IsFeeValueFromPlafon";
			public const string ClassID = "ClassID";
			public const string IsFeeValueFromTariffPrice = "IsFeeValueFromTariffPrice";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string SRParamedicStatus = "SRParamedicStatus";
			public const string Formula = "Formula";
			public const string IsUsingFormula = "IsUsingFormula";
			public const string Level = "Level";
			public const string Notes = "Notes";
			public const string SRItemConditionRuleType = "SRItemConditionRuleType";
			public const string ItemConditionRuleID = "ItemConditionRuleID";
			public const string FormulaNetto = "FormulaNetto";
			public const string SRRegistrationTypeMergeBilling = "SRRegistrationTypeMergeBilling";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string Id = "Id";
			public const string ParamedicID = "ParamedicID";
			public const string SRSpecialty = "SRSpecialty";
			public const string SRTariffType = "SRTariffType";
			public const string GuarantorID = "GuarantorID";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemID = "ItemID";
			public const string TariffComponentID = "TariffComponentID";
			public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			public const string FeeValue = "FeeValue";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRProcedure = "SRProcedure";
			public const string IsFeeValueFromPlafon = "IsFeeValueFromPlafon";
			public const string ClassID = "ClassID";
			public const string IsFeeValueFromTariffPrice = "IsFeeValueFromTariffPrice";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string SRParamedicStatus = "SRParamedicStatus";
			public const string Formula = "Formula";
			public const string IsUsingFormula = "IsUsingFormula";
			public const string Level = "Level";
			public const string Notes = "Notes";
			public const string SRItemConditionRuleType = "SRItemConditionRuleType";
			public const string ItemConditionRuleID = "ItemConditionRuleID";
			public const string FormulaNetto = "FormulaNetto";
			public const string SRRegistrationTypeMergeBilling = "SRRegistrationTypeMergeBilling";
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
			lock (typeof(ParamedicFeeByFee4ServiceSettingMetadata))
			{
				if(ParamedicFeeByFee4ServiceSettingMetadata.mapDelegates == null)
				{
					ParamedicFeeByFee4ServiceSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeByFee4ServiceSettingMetadata.meta == null)
				{
					ParamedicFeeByFee4ServiceSettingMetadata.meta = new ParamedicFeeByFee4ServiceSettingMetadata();
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
				
				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSpecialty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFeeValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRProcedure", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFeeValueFromPlafon", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFeeValueFromTariffPrice", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRGuarantorType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Formula", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsingFormula", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Level", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemConditionRuleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemConditionRuleID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaNetto", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationTypeMergeBilling", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ParamedicFeeByFee4ServiceSetting";
				meta.Destination = "ParamedicFeeByFee4ServiceSetting";
				meta.spInsert = "proc_ParamedicFeeByFee4ServiceSettingInsert";				
				meta.spUpdate = "proc_ParamedicFeeByFee4ServiceSettingUpdate";		
				meta.spDelete = "proc_ParamedicFeeByFee4ServiceSettingDelete";
				meta.spLoadAll = "proc_ParamedicFeeByFee4ServiceSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeByFee4ServiceSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeByFee4ServiceSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
