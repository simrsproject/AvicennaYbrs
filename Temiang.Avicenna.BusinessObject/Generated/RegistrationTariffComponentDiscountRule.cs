/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/24/2014 9:46:07 AM
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
	abstract public class esRegistrationTariffComponentDiscountRuleCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationTariffComponentDiscountRuleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationTariffComponentDiscountRuleCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationTariffComponentDiscountRuleQuery query)
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
			this.InitQuery(query as esRegistrationTariffComponentDiscountRuleQuery);
		}
		#endregion
		
		virtual public RegistrationTariffComponentDiscountRule DetachEntity(RegistrationTariffComponentDiscountRule entity)
		{
			return base.DetachEntity(entity) as RegistrationTariffComponentDiscountRule;
		}
		
		virtual public RegistrationTariffComponentDiscountRule AttachEntity(RegistrationTariffComponentDiscountRule entity)
		{
			return base.AttachEntity(entity) as RegistrationTariffComponentDiscountRule;
		}
		
		virtual public void Combine(RegistrationTariffComponentDiscountRuleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationTariffComponentDiscountRule this[int index]
		{
			get
			{
				return base[index] as RegistrationTariffComponentDiscountRule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationTariffComponentDiscountRule);
		}
	}



	[Serializable]
	abstract public class esRegistrationTariffComponentDiscountRule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationTariffComponentDiscountRuleQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationTariffComponentDiscountRule()
		{

		}

		public esRegistrationTariffComponentDiscountRule(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String tariffComponentID)
		{
			esRegistrationTariffComponentDiscountRuleQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "IsDiscountInPercentage": this.str.IsDiscountInPercentage = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsDiscountInPercentage":
						
							if (value == null || value is System.Boolean)
								this.IsDiscountInPercentage = (System.Boolean?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
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
		/// Maps to RegistrationTariffComponentDiscountRule.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationTariffComponentDiscountRule.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationTariffComponentDiscountRule.IsDiscountInPercentage
		/// </summary>
		virtual public System.Boolean? IsDiscountInPercentage
		{
			get
			{
				return base.GetSystemBoolean(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.IsDiscountInPercentage);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.IsDiscountInPercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationTariffComponentDiscountRule.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationTariffComponentDiscountRule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationTariffComponentDiscountRule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationTariffComponentDiscountRule entity)
			{
				this.entity = entity;
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
				
			public System.String IsDiscountInPercentage
			{
				get
				{
					System.Boolean? data = entity.IsDiscountInPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscountInPercentage = null;
					else entity.IsDiscountInPercentage = Convert.ToBoolean(value);
				}
			}
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
			

			private esRegistrationTariffComponentDiscountRule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationTariffComponentDiscountRuleQuery query)
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
				throw new Exception("esRegistrationTariffComponentDiscountRule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RegistrationTariffComponentDiscountRule : esRegistrationTariffComponentDiscountRule
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esRegistrationTariffComponentDiscountRuleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationTariffComponentDiscountRuleMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsDiscountInPercentage
		{
			get
			{
				return new esQueryItem(this, RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.IsDiscountInPercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationTariffComponentDiscountRuleCollection")]
	public partial class RegistrationTariffComponentDiscountRuleCollection : esRegistrationTariffComponentDiscountRuleCollection, IEnumerable<RegistrationTariffComponentDiscountRule>
	{
		public RegistrationTariffComponentDiscountRuleCollection()
		{

		}
		
		public static implicit operator List<RegistrationTariffComponentDiscountRule>(RegistrationTariffComponentDiscountRuleCollection coll)
		{
			List<RegistrationTariffComponentDiscountRule> list = new List<RegistrationTariffComponentDiscountRule>();
			
			foreach (RegistrationTariffComponentDiscountRule emp in coll)
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
				return  RegistrationTariffComponentDiscountRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationTariffComponentDiscountRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationTariffComponentDiscountRule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationTariffComponentDiscountRule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationTariffComponentDiscountRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationTariffComponentDiscountRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationTariffComponentDiscountRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationTariffComponentDiscountRule AddNew()
		{
			RegistrationTariffComponentDiscountRule entity = base.AddNewEntity() as RegistrationTariffComponentDiscountRule;
			
			return entity;
		}

		public RegistrationTariffComponentDiscountRule FindByPrimaryKey(System.String registrationNo, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(registrationNo, tariffComponentID) as RegistrationTariffComponentDiscountRule;
		}


		#region IEnumerable<RegistrationTariffComponentDiscountRule> Members

		IEnumerator<RegistrationTariffComponentDiscountRule> IEnumerable<RegistrationTariffComponentDiscountRule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationTariffComponentDiscountRule;
			}
		}

		#endregion
		
		private RegistrationTariffComponentDiscountRuleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationTariffComponentDiscountRule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationTariffComponentDiscountRule ({RegistrationNo},{TariffComponentID})")]
	[Serializable]
	public partial class RegistrationTariffComponentDiscountRule : esRegistrationTariffComponentDiscountRule
	{
		public RegistrationTariffComponentDiscountRule()
		{

		}
	
		public RegistrationTariffComponentDiscountRule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationTariffComponentDiscountRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationTariffComponentDiscountRuleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationTariffComponentDiscountRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationTariffComponentDiscountRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationTariffComponentDiscountRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationTariffComponentDiscountRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationTariffComponentDiscountRuleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationTariffComponentDiscountRuleQuery : esRegistrationTariffComponentDiscountRuleQuery
	{
		public RegistrationTariffComponentDiscountRuleQuery()
		{

		}		
		
		public RegistrationTariffComponentDiscountRuleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationTariffComponentDiscountRuleQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationTariffComponentDiscountRuleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationTariffComponentDiscountRuleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationTariffComponentDiscountRuleMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.TariffComponentID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationTariffComponentDiscountRuleMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.IsDiscountInPercentage, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationTariffComponentDiscountRuleMetadata.PropertyNames.IsDiscountInPercentage;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.Amount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationTariffComponentDiscountRuleMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationTariffComponentDiscountRuleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationTariffComponentDiscountRuleMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationTariffComponentDiscountRuleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationTariffComponentDiscountRuleMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsDiscountInPercentage = "IsDiscountInPercentage";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsDiscountInPercentage = "IsDiscountInPercentage";
			 public const string Amount = "Amount";
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
			lock (typeof(RegistrationTariffComponentDiscountRuleMetadata))
			{
				if(RegistrationTariffComponentDiscountRuleMetadata.mapDelegates == null)
				{
					RegistrationTariffComponentDiscountRuleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationTariffComponentDiscountRuleMetadata.meta == null)
				{
					RegistrationTariffComponentDiscountRuleMetadata.meta = new RegistrationTariffComponentDiscountRuleMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDiscountInPercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationTariffComponentDiscountRule";
				meta.Destination = "RegistrationTariffComponentDiscountRule";
				
				meta.spInsert = "proc_RegistrationTariffComponentDiscountRuleInsert";				
				meta.spUpdate = "proc_RegistrationTariffComponentDiscountRuleUpdate";		
				meta.spDelete = "proc_RegistrationTariffComponentDiscountRuleDelete";
				meta.spLoadAll = "proc_RegistrationTariffComponentDiscountRuleLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationTariffComponentDiscountRuleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationTariffComponentDiscountRuleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
