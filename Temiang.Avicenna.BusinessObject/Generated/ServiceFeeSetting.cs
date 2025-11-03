/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/6/2022 6:10:56 PM
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
	abstract public class esServiceFeeSettingCollection : esEntityCollectionWAuditLog
	{
		public esServiceFeeSettingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceFeeSettingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceFeeSettingQuery query)
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
			this.InitQuery(query as esServiceFeeSettingQuery);
		}
		#endregion
			
		virtual public ServiceFeeSetting DetachEntity(ServiceFeeSetting entity)
		{
			return base.DetachEntity(entity) as ServiceFeeSetting;
		}
		
		virtual public ServiceFeeSetting AttachEntity(ServiceFeeSetting entity)
		{
			return base.AttachEntity(entity) as ServiceFeeSetting;
		}
		
		virtual public void Combine(ServiceFeeSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceFeeSetting this[int index]
		{
			get
			{
				return base[index] as ServiceFeeSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceFeeSetting);
		}
	}

	[Serializable]
	abstract public class esServiceFeeSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceFeeSettingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceFeeSetting()
		{
		}
	
		public esServiceFeeSetting(DataRow row)
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
			esServiceFeeSettingQuery query = this.GetDynamicQuery();
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
						case "Level": this.str.Level = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "SRSpecialty": this.str.SRSpecialty = (string)value; break;
						case "SRTariffType": this.str.SRTariffType = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "SRProcedure": this.str.SRProcedure = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "SRGuarantorType": this.str.SRGuarantorType = (string)value; break;
						case "SRParamedicStatus": this.str.SRParamedicStatus = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "SRItemConditionRuleType": this.str.SRItemConditionRuleType = (string)value; break;
						case "ItemConditionRuleID": this.str.ItemConditionRuleID = (string)value; break;
						case "FormulaDirektur": this.str.FormulaDirektur = (string)value; break;
						case "FormulaStruktural": this.str.FormulaStruktural = (string)value; break;
						case "FormulaMedis": this.str.FormulaMedis = (string)value; break;
						case "FormulaUnit": this.str.FormulaUnit = (string)value; break;
						case "FormulaPemerataan": this.str.FormulaPemerataan = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
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
						case "Level":
						
							if (value == null || value is System.Int32)
								this.Level = (System.Int32?)value;
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
		/// Maps to ServiceFeeSetting.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeSettingMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeSettingMetadata.ColumnNames.Id, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.Level
		/// </summary>
		virtual public System.Int32? Level
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeSettingMetadata.ColumnNames.Level);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeSettingMetadata.ColumnNames.Level, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.SRSpecialty
		/// </summary>
		virtual public System.String SRSpecialty
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRSpecialty);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRSpecialty, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRTariffType);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.ItemGroupID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.SRProcedure
		/// </summary>
		virtual public System.String SRProcedure
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRProcedure);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRProcedure, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.SRGuarantorType
		/// </summary>
		virtual public System.String SRGuarantorType
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRGuarantorType);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRGuarantorType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.SRParamedicStatus
		/// </summary>
		virtual public System.String SRParamedicStatus
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRParamedicStatus);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRParamedicStatus, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.SRItemConditionRuleType
		/// </summary>
		virtual public System.String SRItemConditionRuleType
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRItemConditionRuleType);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.SRItemConditionRuleType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.ItemConditionRuleID
		/// </summary>
		virtual public System.String ItemConditionRuleID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.ItemConditionRuleID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.ItemConditionRuleID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.FormulaDirektur
		/// </summary>
		virtual public System.String FormulaDirektur
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaDirektur);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaDirektur, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.FormulaStruktural
		/// </summary>
		virtual public System.String FormulaStruktural
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaStruktural);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaStruktural, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.FormulaMedis
		/// </summary>
		virtual public System.String FormulaMedis
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaMedis);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaMedis, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.FormulaUnit
		/// </summary>
		virtual public System.String FormulaUnit
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaUnit);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaUnit, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.FormulaPemerataan
		/// </summary>
		virtual public System.String FormulaPemerataan
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaPemerataan);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.FormulaPemerataan, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeSettingMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeSettingMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeSettingMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esServiceFeeSetting entity)
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
			public System.String FormulaDirektur
			{
				get
				{
					System.String data = entity.FormulaDirektur;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaDirektur = null;
					else entity.FormulaDirektur = Convert.ToString(value);
				}
			}
			public System.String FormulaStruktural
			{
				get
				{
					System.String data = entity.FormulaStruktural;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaStruktural = null;
					else entity.FormulaStruktural = Convert.ToString(value);
				}
			}
			public System.String FormulaMedis
			{
				get
				{
					System.String data = entity.FormulaMedis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaMedis = null;
					else entity.FormulaMedis = Convert.ToString(value);
				}
			}
			public System.String FormulaUnit
			{
				get
				{
					System.String data = entity.FormulaUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaUnit = null;
					else entity.FormulaUnit = Convert.ToString(value);
				}
			}
			public System.String FormulaPemerataan
			{
				get
				{
					System.String data = entity.FormulaPemerataan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaPemerataan = null;
					else entity.FormulaPemerataan = Convert.ToString(value);
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
			private esServiceFeeSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceFeeSettingQuery query)
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
				throw new Exception("esServiceFeeSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceFeeSetting : esServiceFeeSetting
	{	
	}

	[Serializable]
	abstract public class esServiceFeeSettingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeSettingMetadata.Meta();
			}
		}	
			
		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Level
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.Level, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRSpecialty
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.SRSpecialty, esSystemType.String);
			}
		} 
			
		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRProcedure
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.SRProcedure, esSystemType.String);
			}
		} 
			
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRGuarantorType
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.SRGuarantorType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicStatus
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.SRParamedicStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem SRItemConditionRuleType
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.SRItemConditionRuleType, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemConditionRuleID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.ItemConditionRuleID, esSystemType.String);
			}
		} 
			
		public esQueryItem FormulaDirektur
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.FormulaDirektur, esSystemType.String);
			}
		} 
			
		public esQueryItem FormulaStruktural
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.FormulaStruktural, esSystemType.String);
			}
		} 
			
		public esQueryItem FormulaMedis
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.FormulaMedis, esSystemType.String);
			}
		} 
			
		public esQueryItem FormulaUnit
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.FormulaUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem FormulaPemerataan
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.FormulaPemerataan, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceFeeSettingCollection")]
	public partial class ServiceFeeSettingCollection : esServiceFeeSettingCollection, IEnumerable< ServiceFeeSetting>
	{
		public ServiceFeeSettingCollection()
		{

		}	
		
		public static implicit operator List< ServiceFeeSetting>(ServiceFeeSettingCollection coll)
		{
			List< ServiceFeeSetting> list = new List< ServiceFeeSetting>();
			
			foreach (ServiceFeeSetting emp in coll)
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
				return  ServiceFeeSettingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceFeeSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceFeeSetting();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceFeeSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeSettingQuery();
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
		public bool Load(ServiceFeeSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceFeeSetting AddNew()
		{
			ServiceFeeSetting entity = base.AddNewEntity() as ServiceFeeSetting;
			
			return entity;		
		}
		public ServiceFeeSetting FindByPrimaryKey(Int32 id)
		{
			return base.FindByPrimaryKey(id) as ServiceFeeSetting;
		}

		#region IEnumerable< ServiceFeeSetting> Members

		IEnumerator< ServiceFeeSetting> IEnumerable< ServiceFeeSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceFeeSetting;
			}
		}

		#endregion
		
		private ServiceFeeSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceFeeSetting' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceFeeSetting ({Id})")]
	[Serializable]
	public partial class ServiceFeeSetting : esServiceFeeSetting
	{
		public ServiceFeeSetting()
		{
		}	
	
		public ServiceFeeSetting(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeSettingMetadata.Meta();
			}
		}	
	
		override protected esServiceFeeSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceFeeSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeSettingQuery();
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
		public bool Load(ServiceFeeSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceFeeSettingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceFeeSettingQuery : esServiceFeeSettingQuery
	{
		public ServiceFeeSettingQuery()
		{

		}		
		
		public ServiceFeeSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceFeeSettingQuery";
        }
	}

	[Serializable]
	public partial class ServiceFeeSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceFeeSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.Level, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.Level;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.SRSpecialty, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.SRSpecialty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.SRTariffType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.SRTariffType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.GuarantorID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.SRRegistrationType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.ServiceUnitID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.ItemGroupID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.ItemID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.TariffComponentID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.SRProcedure, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.SRProcedure;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.ClassID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.SRGuarantorType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.SRGuarantorType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.SRParamedicStatus, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.SRParamedicStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.Notes, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.SRItemConditionRuleType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.SRItemConditionRuleType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.ItemConditionRuleID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.ItemConditionRuleID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.FormulaDirektur, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.FormulaDirektur;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.FormulaStruktural, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.FormulaStruktural;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.FormulaMedis, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.FormulaMedis;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.FormulaUnit, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.FormulaUnit;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.FormulaPemerataan, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.FormulaPemerataan;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.CreateByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.CreateDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.LastUpdateByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeSettingMetadata.ColumnNames.LastUpdateDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeSettingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceFeeSettingMetadata Meta()
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
			public const string Level = "Level";
			public const string ParamedicID = "ParamedicID";
			public const string SRSpecialty = "SRSpecialty";
			public const string SRTariffType = "SRTariffType";
			public const string GuarantorID = "GuarantorID";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemID = "ItemID";
			public const string TariffComponentID = "TariffComponentID";
			public const string SRProcedure = "SRProcedure";
			public const string ClassID = "ClassID";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string SRParamedicStatus = "SRParamedicStatus";
			public const string Notes = "Notes";
			public const string SRItemConditionRuleType = "SRItemConditionRuleType";
			public const string ItemConditionRuleID = "ItemConditionRuleID";
			public const string FormulaDirektur = "FormulaDirektur";
			public const string FormulaStruktural = "FormulaStruktural";
			public const string FormulaMedis = "FormulaMedis";
			public const string FormulaUnit = "FormulaUnit";
			public const string FormulaPemerataan = "FormulaPemerataan";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string Id = "Id";
			public const string Level = "Level";
			public const string ParamedicID = "ParamedicID";
			public const string SRSpecialty = "SRSpecialty";
			public const string SRTariffType = "SRTariffType";
			public const string GuarantorID = "GuarantorID";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemID = "ItemID";
			public const string TariffComponentID = "TariffComponentID";
			public const string SRProcedure = "SRProcedure";
			public const string ClassID = "ClassID";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string SRParamedicStatus = "SRParamedicStatus";
			public const string Notes = "Notes";
			public const string SRItemConditionRuleType = "SRItemConditionRuleType";
			public const string ItemConditionRuleID = "ItemConditionRuleID";
			public const string FormulaDirektur = "FormulaDirektur";
			public const string FormulaStruktural = "FormulaStruktural";
			public const string FormulaMedis = "FormulaMedis";
			public const string FormulaUnit = "FormulaUnit";
			public const string FormulaPemerataan = "FormulaPemerataan";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(ServiceFeeSettingMetadata))
			{
				if(ServiceFeeSettingMetadata.mapDelegates == null)
				{
					ServiceFeeSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceFeeSettingMetadata.meta == null)
				{
					ServiceFeeSettingMetadata.meta = new ServiceFeeSettingMetadata();
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
				meta.AddTypeMap("Level", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSpecialty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcedure", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGuarantorType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemConditionRuleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemConditionRuleID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaDirektur", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaStruktural", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaMedis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaPemerataan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ServiceFeeSetting";
				meta.Destination = "ServiceFeeSetting";
				meta.spInsert = "proc_ServiceFeeSettingInsert";				
				meta.spUpdate = "proc_ServiceFeeSettingUpdate";		
				meta.spDelete = "proc_ServiceFeeSettingDelete";
				meta.spLoadAll = "proc_ServiceFeeSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceFeeSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceFeeSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
