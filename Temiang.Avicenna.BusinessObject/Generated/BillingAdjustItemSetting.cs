/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/24/2017 6:26:03 PM
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
	abstract public class esBillingAdjustItemSettingCollection : esEntityCollectionWAuditLog
	{
		public esBillingAdjustItemSettingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BillingAdjustItemSettingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBillingAdjustItemSettingQuery query)
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
			this.InitQuery(query as esBillingAdjustItemSettingQuery);
		}
		#endregion
			
		virtual public BillingAdjustItemSetting DetachEntity(BillingAdjustItemSetting entity)
		{
			return base.DetachEntity(entity) as BillingAdjustItemSetting;
		}
		
		virtual public BillingAdjustItemSetting AttachEntity(BillingAdjustItemSetting entity)
		{
			return base.AttachEntity(entity) as BillingAdjustItemSetting;
		}
		
		virtual public void Combine(BillingAdjustItemSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BillingAdjustItemSetting this[int index]
		{
			get
			{
				return base[index] as BillingAdjustItemSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BillingAdjustItemSetting);
		}
	}

	[Serializable]
	abstract public class esBillingAdjustItemSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBillingAdjustItemSettingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBillingAdjustItemSetting()
		{
		}
	
		public esBillingAdjustItemSetting(DataRow row)
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
			esBillingAdjustItemSettingQuery query = this.GetDynamicQuery();
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "IsFeeValueInPercent": this.str.IsFeeValueInPercent = (string)value; break;
						case "FeeValue": this.str.FeeValue = (string)value; break;
						case "ItemGroupIDsReplacement": this.str.ItemGroupIDsReplacement = (string)value; break;
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
		/// Maps to BillingAdjustItemSetting.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(BillingAdjustItemSettingMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(BillingAdjustItemSettingMetadata.ColumnNames.Id, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.SRSpecialty
		/// </summary>
		virtual public System.String SRSpecialty
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.SRSpecialty);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.SRSpecialty, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.SRTariffType);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.IsFeeValueInPercent
		/// </summary>
		virtual public System.Boolean? IsFeeValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(BillingAdjustItemSettingMetadata.ColumnNames.IsFeeValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(BillingAdjustItemSettingMetadata.ColumnNames.IsFeeValueInPercent, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.FeeValue
		/// </summary>
		virtual public System.Decimal? FeeValue
		{
			get
			{
				return base.GetSystemDecimal(BillingAdjustItemSettingMetadata.ColumnNames.FeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(BillingAdjustItemSettingMetadata.ColumnNames.FeeValue, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.ItemGroupIDsReplacement
		/// </summary>
		virtual public System.String ItemGroupIDsReplacement
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ItemGroupIDsReplacement);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.ItemGroupIDsReplacement, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BillingAdjustItemSettingMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BillingAdjustItemSettingMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esBillingAdjustItemSetting entity)
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
			public System.String ItemGroupIDsReplacement
			{
				get
				{
					System.String data = entity.ItemGroupIDsReplacement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupIDsReplacement = null;
					else entity.ItemGroupIDsReplacement = Convert.ToString(value);
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
			private esBillingAdjustItemSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBillingAdjustItemSettingQuery query)
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
				throw new Exception("esBillingAdjustItemSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BillingAdjustItemSetting : esBillingAdjustItemSetting
	{	
	}

	[Serializable]
	abstract public class esBillingAdjustItemSettingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BillingAdjustItemSettingMetadata.Meta();
			}
		}	
			
		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRSpecialty
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.SRSpecialty, esSystemType.String);
			}
		} 
			
		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsFeeValueInPercent
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.IsFeeValueInPercent, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem FeeValue
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.FeeValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ItemGroupIDsReplacement
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.ItemGroupIDsReplacement, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BillingAdjustItemSettingCollection")]
	public partial class BillingAdjustItemSettingCollection : esBillingAdjustItemSettingCollection, IEnumerable< BillingAdjustItemSetting>
	{
		public BillingAdjustItemSettingCollection()
		{

		}	
		
		public static implicit operator List< BillingAdjustItemSetting>(BillingAdjustItemSettingCollection coll)
		{
			List< BillingAdjustItemSetting> list = new List< BillingAdjustItemSetting>();
			
			foreach (BillingAdjustItemSetting emp in coll)
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
				return  BillingAdjustItemSettingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BillingAdjustItemSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BillingAdjustItemSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BillingAdjustItemSetting();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BillingAdjustItemSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BillingAdjustItemSettingQuery();
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
		public bool Load(BillingAdjustItemSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BillingAdjustItemSetting AddNew()
		{
			BillingAdjustItemSetting entity = base.AddNewEntity() as BillingAdjustItemSetting;
			
			return entity;		
		}
		public BillingAdjustItemSetting FindByPrimaryKey(Int32 id)
		{
			return base.FindByPrimaryKey(id) as BillingAdjustItemSetting;
		}

		#region IEnumerable< BillingAdjustItemSetting> Members

		IEnumerator< BillingAdjustItemSetting> IEnumerable< BillingAdjustItemSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BillingAdjustItemSetting;
			}
		}

		#endregion
		
		private BillingAdjustItemSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BillingAdjustItemSetting' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BillingAdjustItemSetting ({Id})")]
	[Serializable]
	public partial class BillingAdjustItemSetting : esBillingAdjustItemSetting
	{
		public BillingAdjustItemSetting()
		{
		}	
	
		public BillingAdjustItemSetting(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BillingAdjustItemSettingMetadata.Meta();
			}
		}	
	
		override protected esBillingAdjustItemSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BillingAdjustItemSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BillingAdjustItemSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BillingAdjustItemSettingQuery();
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
		public bool Load(BillingAdjustItemSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BillingAdjustItemSettingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BillingAdjustItemSettingQuery : esBillingAdjustItemSettingQuery
	{
		public BillingAdjustItemSettingQuery()
		{

		}		
		
		public BillingAdjustItemSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BillingAdjustItemSettingQuery";
        }
	}

	[Serializable]
	public partial class BillingAdjustItemSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BillingAdjustItemSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.SRSpecialty, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.SRSpecialty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.SRTariffType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.SRTariffType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.GuarantorID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.SRRegistrationType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.ServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.ItemID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.ClassID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.TariffComponentID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.IsFeeValueInPercent, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.IsFeeValueInPercent;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.FeeValue, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.FeeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.ItemGroupIDsReplacement, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.ItemGroupIDsReplacement;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.CreateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.CreateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemSettingMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BillingAdjustItemSettingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BillingAdjustItemSettingMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string TariffComponentID = "TariffComponentID";
			public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			public const string FeeValue = "FeeValue";
			public const string ItemGroupIDsReplacement = "ItemGroupIDsReplacement";
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
			public const string ParamedicID = "ParamedicID";
			public const string SRSpecialty = "SRSpecialty";
			public const string SRTariffType = "SRTariffType";
			public const string GuarantorID = "GuarantorID";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string TariffComponentID = "TariffComponentID";
			public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			public const string FeeValue = "FeeValue";
			public const string ItemGroupIDsReplacement = "ItemGroupIDsReplacement";
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
			lock (typeof(BillingAdjustItemSettingMetadata))
			{
				if(BillingAdjustItemSettingMetadata.mapDelegates == null)
				{
					BillingAdjustItemSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BillingAdjustItemSettingMetadata.meta == null)
				{
					BillingAdjustItemSettingMetadata.meta = new BillingAdjustItemSettingMetadata();
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
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFeeValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ItemGroupIDsReplacement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "BillingAdjustItemSetting";
				meta.Destination = "BillingAdjustItemSetting";
				meta.spInsert = "proc_BillingAdjustItemSettingInsert";				
				meta.spUpdate = "proc_BillingAdjustItemSettingUpdate";		
				meta.spDelete = "proc_BillingAdjustItemSettingDelete";
				meta.spLoadAll = "proc_BillingAdjustItemSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_BillingAdjustItemSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BillingAdjustItemSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
