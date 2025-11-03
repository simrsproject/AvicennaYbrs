/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/12/2022 12:57:51 PM
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
	abstract public class esJsonBridgingValueTempCollection : esEntityCollectionWAuditLog
	{
		public esJsonBridgingValueTempCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "JsonBridgingValueTempCollection";
		}

		#region Query Logic
		protected void InitQuery(esJsonBridgingValueTempQuery query)
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
			this.InitQuery(query as esJsonBridgingValueTempQuery);
		}
		#endregion
		
		virtual public JsonBridgingValueTemp DetachEntity(JsonBridgingValueTemp entity)
		{
			return base.DetachEntity(entity) as JsonBridgingValueTemp;
		}
		
		virtual public JsonBridgingValueTemp AttachEntity(JsonBridgingValueTemp entity)
		{
			return base.AttachEntity(entity) as JsonBridgingValueTemp;
		}
		
		virtual public void Combine(JsonBridgingValueTempCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JsonBridgingValueTemp this[int index]
		{
			get
			{
				return base[index] as JsonBridgingValueTemp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JsonBridgingValueTemp);
		}
	}



	[Serializable]
	abstract public class esJsonBridgingValueTemp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJsonBridgingValueTempQuery GetDynamicQuery()
		{
			return null;
		}

		public esJsonBridgingValueTemp()
		{

		}

		public esJsonBridgingValueTemp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.String id)
		{
			esJsonBridgingValueTempQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String id)
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
						case "JsonValue": this.str.JsonValue = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{

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
		/// Maps to JsonBridgingValueTemp.Id
		/// </summary>
		virtual public System.String Id
		{
			get
			{
				return base.GetSystemString(JsonBridgingValueTempMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemString(JsonBridgingValueTempMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to JsonBridgingValueTemp.JsonValue
		/// </summary>
		virtual public System.String JsonValue
		{
			get
			{
				return base.GetSystemString(JsonBridgingValueTempMetadata.ColumnNames.JsonValue);
			}
			
			set
			{
				base.SetSystemString(JsonBridgingValueTempMetadata.ColumnNames.JsonValue, value);
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
			public esStrings(esJsonBridgingValueTemp entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.String data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToString(value);
				}
			}
				
			public System.String JsonValue
			{
				get
				{
					System.String data = entity.JsonValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JsonValue = null;
					else entity.JsonValue = Convert.ToString(value);
				}
			}
			

			private esJsonBridgingValueTemp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJsonBridgingValueTempQuery query)
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
				throw new Exception("esJsonBridgingValueTemp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esJsonBridgingValueTempQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return JsonBridgingValueTempMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, JsonBridgingValueTempMetadata.ColumnNames.Id, esSystemType.String);
			}
		} 
		
		public esQueryItem JsonValue
		{
			get
			{
				return new esQueryItem(this, JsonBridgingValueTempMetadata.ColumnNames.JsonValue, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JsonBridgingValueTempCollection")]
	public partial class JsonBridgingValueTempCollection : esJsonBridgingValueTempCollection, IEnumerable<JsonBridgingValueTemp>
	{
		public JsonBridgingValueTempCollection()
		{

		}
		
		public static implicit operator List<JsonBridgingValueTemp>(JsonBridgingValueTempCollection coll)
		{
			List<JsonBridgingValueTemp> list = new List<JsonBridgingValueTemp>();
			
			foreach (JsonBridgingValueTemp emp in coll)
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
				return  JsonBridgingValueTempMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JsonBridgingValueTempQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JsonBridgingValueTemp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JsonBridgingValueTemp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public JsonBridgingValueTempQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JsonBridgingValueTempQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(JsonBridgingValueTempQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public JsonBridgingValueTemp AddNew()
		{
			JsonBridgingValueTemp entity = base.AddNewEntity() as JsonBridgingValueTemp;
			
			return entity;
		}

		public JsonBridgingValueTemp FindByPrimaryKey(System.String id)
		{
			return base.FindByPrimaryKey(id) as JsonBridgingValueTemp;
		}


		#region IEnumerable<JsonBridgingValueTemp> Members

		IEnumerator<JsonBridgingValueTemp> IEnumerable<JsonBridgingValueTemp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JsonBridgingValueTemp;
			}
		}

		#endregion
		
		private JsonBridgingValueTempQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JsonBridgingValueTemp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("JsonBridgingValueTemp ({Id})")]
	[Serializable]
	public partial class JsonBridgingValueTemp : esJsonBridgingValueTemp
	{
		public JsonBridgingValueTemp()
		{

		}
	
		public JsonBridgingValueTemp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JsonBridgingValueTempMetadata.Meta();
			}
		}
		
		
		
		override protected esJsonBridgingValueTempQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JsonBridgingValueTempQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public JsonBridgingValueTempQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JsonBridgingValueTempQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(JsonBridgingValueTempQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private JsonBridgingValueTempQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class JsonBridgingValueTempQuery : esJsonBridgingValueTempQuery
	{
		public JsonBridgingValueTempQuery()
		{

		}		
		
		public JsonBridgingValueTempQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "JsonBridgingValueTempQuery";
        }
		
			
	}


	[Serializable]
	public partial class JsonBridgingValueTempMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JsonBridgingValueTempMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(JsonBridgingValueTempMetadata.ColumnNames.Id, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = JsonBridgingValueTempMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(JsonBridgingValueTempMetadata.ColumnNames.JsonValue, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = JsonBridgingValueTempMetadata.PropertyNames.JsonValue;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public JsonBridgingValueTempMetadata Meta()
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
			 public const string JsonValue = "JsonValue";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string JsonValue = "JsonValue";
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
			lock (typeof(JsonBridgingValueTempMetadata))
			{
				if(JsonBridgingValueTempMetadata.mapDelegates == null)
				{
					JsonBridgingValueTempMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JsonBridgingValueTempMetadata.meta == null)
				{
					JsonBridgingValueTempMetadata.meta = new JsonBridgingValueTempMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JsonValue", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "JsonBridgingValueTemp";
				meta.Destination = "JsonBridgingValueTemp";
				
				meta.spInsert = "proc_JsonBridgingValueTempInsert";				
				meta.spUpdate = "proc_JsonBridgingValueTempUpdate";		
				meta.spDelete = "proc_JsonBridgingValueTempDelete";
				meta.spLoadAll = "proc_JsonBridgingValueTempLoadAll";
				meta.spLoadByPrimaryKey = "proc_JsonBridgingValueTempLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JsonBridgingValueTempMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
