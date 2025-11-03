/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/13/2014 1:34:55 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esParamedicAutoBillItemCollection : esEntityCollectionWAuditLog
	{
		public esParamedicAutoBillItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicAutoBillItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicAutoBillItemQuery query)
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
			this.InitQuery(query as esParamedicAutoBillItemQuery);
		}
		#endregion
		
		virtual public ParamedicAutoBillItem DetachEntity(ParamedicAutoBillItem entity)
		{
			return base.DetachEntity(entity) as ParamedicAutoBillItem;
		}
		
		virtual public ParamedicAutoBillItem AttachEntity(ParamedicAutoBillItem entity)
		{
			return base.AttachEntity(entity) as ParamedicAutoBillItem;
		}
		
		virtual public void Combine(ParamedicAutoBillItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicAutoBillItem this[int index]
		{
			get
			{
				return base[index] as ParamedicAutoBillItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicAutoBillItem);
		}
	}



	[Serializable]
	abstract public class esParamedicAutoBillItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicAutoBillItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicAutoBillItem()
		{

		}

		public esParamedicAutoBillItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String paramedicID, System.String serviceUnitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, paramedicID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, paramedicID, serviceUnitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String paramedicID, System.String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, paramedicID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, paramedicID, serviceUnitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String paramedicID, System.String serviceUnitID)
		{
			esParamedicAutoBillItemQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.ParamedicID == paramedicID, query.ServiceUnitID == serviceUnitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String paramedicID, System.String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("ParamedicID",paramedicID);			parms.Add("ServiceUnitID",serviceUnitID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "Quantity": this.str.Quantity = (string)value; break;							
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "IsGenerateOnRegistration": this.str.IsGenerateOnRegistration = (string)value; break;							
						case "IsGenerateOnReferral": this.str.IsGenerateOnReferral = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Quantity":
						
							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "IsGenerateOnRegistration":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateOnRegistration = (System.Boolean?)value;
							break;
						
						case "IsGenerateOnReferral":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateOnReferral = (System.Boolean?)value;
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
		/// Maps to ParamedicAutoBillItem.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(ParamedicAutoBillItemMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicAutoBillItemMetadata.ColumnNames.Quantity, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ParamedicAutoBillItemMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicAutoBillItemMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.IsGenerateOnRegistration
		/// </summary>
		virtual public System.Boolean? IsGenerateOnRegistration
		{
			get
			{
				return base.GetSystemBoolean(ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.IsGenerateOnReferral
		/// </summary>
		virtual public System.Boolean? IsGenerateOnReferral
		{
			get
			{
				return base.GetSystemBoolean(ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicAutoBillItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esParamedicAutoBillItem entity)
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
				
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
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
				
			public System.String IsGenerateOnRegistration
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnRegistration = null;
					else entity.IsGenerateOnRegistration = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsGenerateOnReferral
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnReferral = null;
					else entity.IsGenerateOnReferral = Convert.ToBoolean(value);
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
			

			private esParamedicAutoBillItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicAutoBillItemQuery query)
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
				throw new Exception("esParamedicAutoBillItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esParamedicAutoBillItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicAutoBillItemMetadata.Meta();
			}
		}	
		

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsGenerateOnRegistration
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsGenerateOnReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicAutoBillItemCollection")]
	public partial class ParamedicAutoBillItemCollection : esParamedicAutoBillItemCollection, IEnumerable<ParamedicAutoBillItem>
	{
		public ParamedicAutoBillItemCollection()
		{

		}
		
		public static implicit operator List<ParamedicAutoBillItem>(ParamedicAutoBillItemCollection coll)
		{
			List<ParamedicAutoBillItem> list = new List<ParamedicAutoBillItem>();
			
			foreach (ParamedicAutoBillItem emp in coll)
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
				return  ParamedicAutoBillItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicAutoBillItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicAutoBillItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicAutoBillItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicAutoBillItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicAutoBillItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicAutoBillItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicAutoBillItem AddNew()
		{
			ParamedicAutoBillItem entity = base.AddNewEntity() as ParamedicAutoBillItem;
			
			return entity;
		}

		public ParamedicAutoBillItem FindByPrimaryKey(System.String itemID, System.String paramedicID, System.String serviceUnitID)
		{
			return base.FindByPrimaryKey(itemID, paramedicID, serviceUnitID) as ParamedicAutoBillItem;
		}


		#region IEnumerable<ParamedicAutoBillItem> Members

		IEnumerator<ParamedicAutoBillItem> IEnumerable<ParamedicAutoBillItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicAutoBillItem;
			}
		}

		#endregion
		
		private ParamedicAutoBillItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicAutoBillItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicAutoBillItem ({ParamedicID},{ServiceUnitID},{ItemID})")]
	[Serializable]
	public partial class ParamedicAutoBillItem : esParamedicAutoBillItem
	{
		public ParamedicAutoBillItem()
		{

		}
	
		public ParamedicAutoBillItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicAutoBillItemMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicAutoBillItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicAutoBillItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicAutoBillItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicAutoBillItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicAutoBillItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicAutoBillItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicAutoBillItemQuery : esParamedicAutoBillItemQuery
	{
		public ParamedicAutoBillItemQuery()
		{

		}		
		
		public ParamedicAutoBillItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicAutoBillItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicAutoBillItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicAutoBillItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.Quantity, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.IsGenerateOnRegistration;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.IsGenerateOnReferral;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicAutoBillItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicAutoBillItemMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ItemID = "ItemID";
			 public const string Quantity = "Quantity";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string IsActive = "IsActive";
			 public const string IsGenerateOnRegistration = "IsGenerateOnRegistration";
			 public const string IsGenerateOnReferral = "IsGenerateOnReferral";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ParamedicID = "ParamedicID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ItemID = "ItemID";
			 public const string Quantity = "Quantity";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string IsActive = "IsActive";
			 public const string IsGenerateOnRegistration = "IsGenerateOnRegistration";
			 public const string IsGenerateOnReferral = "IsGenerateOnReferral";
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
			lock (typeof(ParamedicAutoBillItemMetadata))
			{
				if(ParamedicAutoBillItemMetadata.mapDelegates == null)
				{
					ParamedicAutoBillItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicAutoBillItemMetadata.meta == null)
				{
					ParamedicAutoBillItemMetadata.meta = new ParamedicAutoBillItemMetadata();
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
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnReferral", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicAutoBillItem";
				meta.Destination = "ParamedicAutoBillItem";
				
				meta.spInsert = "proc_ParamedicAutoBillItemInsert";				
				meta.spUpdate = "proc_ParamedicAutoBillItemUpdate";		
				meta.spDelete = "proc_ParamedicAutoBillItemDelete";
				meta.spLoadAll = "proc_ParamedicAutoBillItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicAutoBillItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicAutoBillItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
