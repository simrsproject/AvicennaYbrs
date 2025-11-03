/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 6/30/2014 11:20:41 AM
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
	abstract public class esLabellCollection : esEntityCollection
	{
		public esLabellCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LabellCollection";
		}

		#region Query Logic
		protected void InitQuery(esLabellQuery query)
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
			this.InitQuery(query as esLabellQuery);
		}
		#endregion
		
		virtual public Labell DetachEntity(Labell entity)
		{
			return base.DetachEntity(entity) as Labell;
		}
		
		virtual public Labell AttachEntity(Labell entity)
		{
			return base.AttachEntity(entity) as Labell;
		}
		
		virtual public void Combine(LabellCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Labell this[int index]
		{
			get
			{
				return base[index] as Labell;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Labell);
		}
	}



	[Serializable]
	abstract public class esLabell : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLabellQuery GetDynamicQuery()
		{
			return null;
		}

		public esLabell()
		{

		}

		public esLabell(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String labelID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(labelID);
			else
				return LoadByPrimaryKeyStoredProcedure(labelID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String labelID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(labelID);
			else
				return LoadByPrimaryKeyStoredProcedure(labelID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String labelID)
		{
			esLabellQuery query = this.GetDynamicQuery();
			query.Where(query.LabelID == labelID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String labelID)
		{
			esParameters parms = new esParameters();
			parms.Add("LabelID",labelID);
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
						case "LabelID": this.str.LabelID = (string)value; break;							
						case "LabelName": this.str.LabelName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "InsertDateTime": this.str.InsertDateTime = (string)value; break;							
						case "InsertByUserID": this.str.InsertByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "InsertDateTime":
						
							if (value == null || value is System.DateTime)
								this.InsertDateTime = (System.DateTime?)value;
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
		/// Maps to Labell.LabelID
		/// </summary>
		virtual public System.String LabelID
		{
			get
			{
				return base.GetSystemString(LabellMetadata.ColumnNames.LabelID);
			}
			
			set
			{
				base.SetSystemString(LabellMetadata.ColumnNames.LabelID, value);
			}
		}
		
		/// <summary>
		/// Maps to Labell.LabelName
		/// </summary>
		virtual public System.String LabelName
		{
			get
			{
				return base.GetSystemString(LabellMetadata.ColumnNames.LabelName);
			}
			
			set
			{
				base.SetSystemString(LabellMetadata.ColumnNames.LabelName, value);
			}
		}
		
		/// <summary>
		/// Maps to Labell.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(LabellMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(LabellMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Labell.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(LabellMetadata.ColumnNames.InsertDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LabellMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Labell.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(LabellMetadata.ColumnNames.InsertByUserID);
			}
			
			set
			{
				base.SetSystemString(LabellMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Labell.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LabellMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LabellMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Labell.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LabellMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LabellMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLabell entity)
			{
				this.entity = entity;
			}
			
	
			public System.String LabelID
			{
				get
				{
					System.String data = entity.LabelID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LabelID = null;
					else entity.LabelID = Convert.ToString(value);
				}
			}
				
			public System.String LabelName
			{
				get
				{
					System.String data = entity.LabelName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LabelName = null;
					else entity.LabelName = Convert.ToString(value);
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
				
			public System.String InsertDateTime
			{
				get
				{
					System.DateTime? data = entity.InsertDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertDateTime = null;
					else entity.InsertDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String InsertByUserID
			{
				get
				{
					System.String data = entity.InsertByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertByUserID = null;
					else entity.InsertByUserID = Convert.ToString(value);
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
			

			private esLabell entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLabellQuery query)
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
				throw new Exception("esLabell can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Labell : esLabell
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
	abstract public class esLabellQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LabellMetadata.Meta();
			}
		}	
		

		public esQueryItem LabelID
		{
			get
			{
				return new esQueryItem(this, LabellMetadata.ColumnNames.LabelID, esSystemType.String);
			}
		} 
		
		public esQueryItem LabelName
		{
			get
			{
				return new esQueryItem(this, LabellMetadata.ColumnNames.LabelName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, LabellMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, LabellMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, LabellMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LabellMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LabellMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LabellCollection")]
	public partial class LabellCollection : esLabellCollection, IEnumerable<Labell>
	{
		public LabellCollection()
		{

		}
		
		public static implicit operator List<Labell>(LabellCollection coll)
		{
			List<Labell> list = new List<Labell>();
			
			foreach (Labell emp in coll)
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
				return  LabellMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabellQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Labell(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Labell();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LabellQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabellQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LabellQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Labell AddNew()
		{
			Labell entity = base.AddNewEntity() as Labell;
			
			return entity;
		}

		public Labell FindByPrimaryKey(System.String labelID)
		{
			return base.FindByPrimaryKey(labelID) as Labell;
		}


		#region IEnumerable<Labell> Members

		IEnumerator<Labell> IEnumerable<Labell>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Labell;
			}
		}

		#endregion
		
		private LabellQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Labell' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Labell ({LabelID})")]
	[Serializable]
	public partial class Labell : esLabell
	{
		public Labell()
		{

		}
	
		public Labell(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LabellMetadata.Meta();
			}
		}
		
		
		
		override protected esLabellQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabellQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LabellQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabellQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LabellQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LabellQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LabellQuery : esLabellQuery
	{
		public LabellQuery()
		{

		}		
		
		public LabellQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LabellQuery";
        }
		
			
	}


	[Serializable]
	public partial class LabellMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LabellMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LabellMetadata.ColumnNames.LabelID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LabellMetadata.PropertyNames.LabelID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabellMetadata.ColumnNames.LabelName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LabellMetadata.PropertyNames.LabelName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabellMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LabellMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabellMetadata.ColumnNames.InsertDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabellMetadata.PropertyNames.InsertDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabellMetadata.ColumnNames.InsertByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LabellMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabellMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabellMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabellMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LabellMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LabellMetadata Meta()
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
			 public const string LabelID = "LabelID";
			 public const string LabelName = "LabelName";
			 public const string IsActive = "IsActive";
			 public const string InsertDateTime = "InsertDateTime";
			 public const string InsertByUserID = "InsertByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string LabelID = "LabelID";
			 public const string LabelName = "LabelName";
			 public const string IsActive = "IsActive";
			 public const string InsertDateTime = "InsertDateTime";
			 public const string InsertByUserID = "InsertByUserID";
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
			lock (typeof(LabellMetadata))
			{
				if(LabellMetadata.mapDelegates == null)
				{
					LabellMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LabellMetadata.meta == null)
				{
					LabellMetadata.meta = new LabellMetadata();
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
				

				meta.AddTypeMap("LabelID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LabelName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Labell";
				meta.Destination = "Labell";
				
				meta.spInsert = "proc_LabellInsert";				
				meta.spUpdate = "proc_LabellUpdate";		
				meta.spDelete = "proc_LabellDelete";
				meta.spLoadAll = "proc_LabellLoadAll";
				meta.spLoadByPrimaryKey = "proc_LabellLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LabellMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
