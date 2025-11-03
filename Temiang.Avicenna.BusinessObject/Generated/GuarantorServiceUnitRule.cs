/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/7/2014 2:53:52 PM
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
	abstract public class esGuarantorServiceUnitRuleCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorServiceUnitRuleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorServiceUnitRuleCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorServiceUnitRuleQuery query)
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
			this.InitQuery(query as esGuarantorServiceUnitRuleQuery);
		}
		#endregion
		
		virtual public GuarantorServiceUnitRule DetachEntity(GuarantorServiceUnitRule entity)
		{
			return base.DetachEntity(entity) as GuarantorServiceUnitRule;
		}
		
		virtual public GuarantorServiceUnitRule AttachEntity(GuarantorServiceUnitRule entity)
		{
			return base.AttachEntity(entity) as GuarantorServiceUnitRule;
		}
		
		virtual public void Combine(GuarantorServiceUnitRuleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorServiceUnitRule this[int index]
		{
			get
			{
				return base[index] as GuarantorServiceUnitRule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorServiceUnitRule);
		}
	}



	[Serializable]
	abstract public class esGuarantorServiceUnitRule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorServiceUnitRuleQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorServiceUnitRule()
		{

		}

		public esGuarantorServiceUnitRule(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID, System.String serviceUnitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, serviceUnitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID, System.String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, serviceUnitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID, System.String serviceUnitID)
		{
			esGuarantorServiceUnitRuleQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.ServiceUnitID == serviceUnitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID, System.String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);			parms.Add("ServiceUnitID",serviceUnitID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "IsCovered": this.str.IsCovered = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsCovered":
						
							if (value == null || value is System.Boolean)
								this.IsCovered = (System.Boolean?)value;
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
		/// Maps to GuarantorServiceUnitRule.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorServiceUnitRuleMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorServiceUnitRuleMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorServiceUnitRule.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(GuarantorServiceUnitRuleMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(GuarantorServiceUnitRuleMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorServiceUnitRule.IsCovered
		/// </summary>
		virtual public System.Boolean? IsCovered
		{
			get
			{
				return base.GetSystemBoolean(GuarantorServiceUnitRuleMetadata.ColumnNames.IsCovered);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorServiceUnitRuleMetadata.ColumnNames.IsCovered, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorServiceUnitRule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorServiceUnitRule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorServiceUnitRule entity)
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
				
			public System.String IsCovered
			{
				get
				{
					System.Boolean? data = entity.IsCovered;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCovered = null;
					else entity.IsCovered = Convert.ToBoolean(value);
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
			

			private esGuarantorServiceUnitRule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorServiceUnitRuleQuery query)
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
				throw new Exception("esGuarantorServiceUnitRule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorServiceUnitRule : esGuarantorServiceUnitRule
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
	abstract public class esGuarantorServiceUnitRuleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorServiceUnitRuleMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitRuleMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitRuleMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsCovered
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitRuleMetadata.ColumnNames.IsCovered, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorServiceUnitRuleCollection")]
	public partial class GuarantorServiceUnitRuleCollection : esGuarantorServiceUnitRuleCollection, IEnumerable<GuarantorServiceUnitRule>
	{
		public GuarantorServiceUnitRuleCollection()
		{

		}
		
		public static implicit operator List<GuarantorServiceUnitRule>(GuarantorServiceUnitRuleCollection coll)
		{
			List<GuarantorServiceUnitRule> list = new List<GuarantorServiceUnitRule>();
			
			foreach (GuarantorServiceUnitRule emp in coll)
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
				return  GuarantorServiceUnitRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorServiceUnitRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorServiceUnitRule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorServiceUnitRule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorServiceUnitRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorServiceUnitRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorServiceUnitRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorServiceUnitRule AddNew()
		{
			GuarantorServiceUnitRule entity = base.AddNewEntity() as GuarantorServiceUnitRule;
			
			return entity;
		}

		public GuarantorServiceUnitRule FindByPrimaryKey(System.String guarantorID, System.String serviceUnitID)
		{
			return base.FindByPrimaryKey(guarantorID, serviceUnitID) as GuarantorServiceUnitRule;
		}


		#region IEnumerable<GuarantorServiceUnitRule> Members

		IEnumerator<GuarantorServiceUnitRule> IEnumerable<GuarantorServiceUnitRule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorServiceUnitRule;
			}
		}

		#endregion
		
		private GuarantorServiceUnitRuleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorServiceUnitRule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorServiceUnitRule ({GuarantorID},{ServiceUnitID})")]
	[Serializable]
	public partial class GuarantorServiceUnitRule : esGuarantorServiceUnitRule
	{
		public GuarantorServiceUnitRule()
		{

		}
	
		public GuarantorServiceUnitRule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorServiceUnitRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorServiceUnitRuleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorServiceUnitRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorServiceUnitRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorServiceUnitRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorServiceUnitRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorServiceUnitRuleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorServiceUnitRuleQuery : esGuarantorServiceUnitRuleQuery
	{
		public GuarantorServiceUnitRuleQuery()
		{

		}		
		
		public GuarantorServiceUnitRuleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorServiceUnitRuleQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorServiceUnitRuleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorServiceUnitRuleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorServiceUnitRuleMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorServiceUnitRuleMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorServiceUnitRuleMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorServiceUnitRuleMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorServiceUnitRuleMetadata.ColumnNames.IsCovered, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorServiceUnitRuleMetadata.PropertyNames.IsCovered;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorServiceUnitRuleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorServiceUnitRuleMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorServiceUnitRuleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorServiceUnitRuleMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string IsCovered = "IsCovered";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string IsCovered = "IsCovered";
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
			lock (typeof(GuarantorServiceUnitRuleMetadata))
			{
				if(GuarantorServiceUnitRuleMetadata.mapDelegates == null)
				{
					GuarantorServiceUnitRuleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorServiceUnitRuleMetadata.meta == null)
				{
					GuarantorServiceUnitRuleMetadata.meta = new GuarantorServiceUnitRuleMetadata();
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
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCovered", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "GuarantorServiceUnitRule";
				meta.Destination = "GuarantorServiceUnitRule";
				
				meta.spInsert = "proc_GuarantorServiceUnitRuleInsert";				
				meta.spUpdate = "proc_GuarantorServiceUnitRuleUpdate";		
				meta.spDelete = "proc_GuarantorServiceUnitRuleDelete";
				meta.spLoadAll = "proc_GuarantorServiceUnitRuleLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorServiceUnitRuleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorServiceUnitRuleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
