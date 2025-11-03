/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:16 PM
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
	abstract public class esGuarantorItemTypeRuleCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorItemTypeRuleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorItemTypeRuleCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorItemTypeRuleQuery query)
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
			this.InitQuery(query as esGuarantorItemTypeRuleQuery);
		}
		#endregion
		
		virtual public GuarantorItemTypeRule DetachEntity(GuarantorItemTypeRule entity)
		{
			return base.DetachEntity(entity) as GuarantorItemTypeRule;
		}
		
		virtual public GuarantorItemTypeRule AttachEntity(GuarantorItemTypeRule entity)
		{
			return base.AttachEntity(entity) as GuarantorItemTypeRule;
		}
		
		virtual public void Combine(GuarantorItemTypeRuleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorItemTypeRule this[int index]
		{
			get
			{
				return base[index] as GuarantorItemTypeRule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorItemTypeRule);
		}
	}



	[Serializable]
	abstract public class esGuarantorItemTypeRule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorItemTypeRuleQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorItemTypeRule()
		{

		}

		public esGuarantorItemTypeRule(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID, System.String sRItemType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, sRItemType);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, sRItemType);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID, System.String sRItemType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, sRItemType);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, sRItemType);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID, System.String sRItemType)
		{
			esGuarantorItemTypeRuleQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.SRItemType == sRItemType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID, System.String sRItemType)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);			parms.Add("SRItemType",sRItemType);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "SRItemType": this.str.SRItemType = (string)value; break;							
						case "IsToGuarantor": this.str.IsToGuarantor = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsToGuarantor":
						
							if (value == null || value is System.Boolean)
								this.IsToGuarantor = (System.Boolean?)value;
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
		/// Maps to GuarantorItemTypeRule.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorItemTypeRuleMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemTypeRuleMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemTypeRule.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(GuarantorItemTypeRuleMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemTypeRuleMetadata.ColumnNames.SRItemType, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemTypeRule.IsToGuarantor
		/// </summary>
		virtual public System.Boolean? IsToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemTypeRuleMetadata.ColumnNames.IsToGuarantor);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemTypeRuleMetadata.ColumnNames.IsToGuarantor, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemTypeRule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemTypeRule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorItemTypeRule entity)
			{
				this.entity = entity;
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
				
			public System.String SRItemType
			{
				get
				{
					System.String data = entity.SRItemType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemType = null;
					else entity.SRItemType = Convert.ToString(value);
				}
			}
				
			public System.String IsToGuarantor
			{
				get
				{
					System.Boolean? data = entity.IsToGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsToGuarantor = null;
					else entity.IsToGuarantor = Convert.ToBoolean(value);
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
			

			private esGuarantorItemTypeRule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorItemTypeRuleQuery query)
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
				throw new Exception("esGuarantorItemTypeRule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorItemTypeRule : esGuarantorItemTypeRule
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
	abstract public class esGuarantorItemTypeRuleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemTypeRuleMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemTypeRuleMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, GuarantorItemTypeRuleMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsToGuarantor
		{
			get
			{
				return new esQueryItem(this, GuarantorItemTypeRuleMetadata.ColumnNames.IsToGuarantor, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorItemTypeRuleCollection")]
	public partial class GuarantorItemTypeRuleCollection : esGuarantorItemTypeRuleCollection, IEnumerable<GuarantorItemTypeRule>
	{
		public GuarantorItemTypeRuleCollection()
		{

		}
		
		public static implicit operator List<GuarantorItemTypeRule>(GuarantorItemTypeRuleCollection coll)
		{
			List<GuarantorItemTypeRule> list = new List<GuarantorItemTypeRule>();
			
			foreach (GuarantorItemTypeRule emp in coll)
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
				return  GuarantorItemTypeRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemTypeRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorItemTypeRule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorItemTypeRule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorItemTypeRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemTypeRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorItemTypeRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorItemTypeRule AddNew()
		{
			GuarantorItemTypeRule entity = base.AddNewEntity() as GuarantorItemTypeRule;
			
			return entity;
		}

		public GuarantorItemTypeRule FindByPrimaryKey(System.String guarantorID, System.String sRItemType)
		{
			return base.FindByPrimaryKey(guarantorID, sRItemType) as GuarantorItemTypeRule;
		}


		#region IEnumerable<GuarantorItemTypeRule> Members

		IEnumerator<GuarantorItemTypeRule> IEnumerable<GuarantorItemTypeRule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorItemTypeRule;
			}
		}

		#endregion
		
		private GuarantorItemTypeRuleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorItemTypeRule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorItemTypeRule ({GuarantorID},{SRItemType})")]
	[Serializable]
	public partial class GuarantorItemTypeRule : esGuarantorItemTypeRule
	{
		public GuarantorItemTypeRule()
		{

		}
	
		public GuarantorItemTypeRule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemTypeRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorItemTypeRuleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemTypeRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorItemTypeRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemTypeRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorItemTypeRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorItemTypeRuleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorItemTypeRuleQuery : esGuarantorItemTypeRuleQuery
	{
		public GuarantorItemTypeRuleQuery()
		{

		}		
		
		public GuarantorItemTypeRuleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorItemTypeRuleQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorItemTypeRuleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorItemTypeRuleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorItemTypeRuleMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemTypeRuleMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemTypeRuleMetadata.ColumnNames.SRItemType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemTypeRuleMetadata.PropertyNames.SRItemType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemTypeRuleMetadata.ColumnNames.IsToGuarantor, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemTypeRuleMetadata.PropertyNames.IsToGuarantor;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorItemTypeRuleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemTypeRuleMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemTypeRuleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorItemTypeRuleMetadata Meta()
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
			 public const string GuarantorID = "GuarantorID";
			 public const string SRItemType = "SRItemType";
			 public const string IsToGuarantor = "IsToGuarantor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string SRItemType = "SRItemType";
			 public const string IsToGuarantor = "IsToGuarantor";
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
			lock (typeof(GuarantorItemTypeRuleMetadata))
			{
				if(GuarantorItemTypeRuleMetadata.mapDelegates == null)
				{
					GuarantorItemTypeRuleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorItemTypeRuleMetadata.meta == null)
				{
					GuarantorItemTypeRuleMetadata.meta = new GuarantorItemTypeRuleMetadata();
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
				

				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "GuarantorItemTypeRule";
				meta.Destination = "GuarantorItemTypeRule";
				
				meta.spInsert = "proc_GuarantorItemTypeRuleInsert";				
				meta.spUpdate = "proc_GuarantorItemTypeRuleUpdate";		
				meta.spDelete = "proc_GuarantorItemTypeRuleDelete";
				meta.spLoadAll = "proc_GuarantorItemTypeRuleLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorItemTypeRuleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorItemTypeRuleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
