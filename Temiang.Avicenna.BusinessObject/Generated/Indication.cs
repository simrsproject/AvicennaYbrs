/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/2/2014 2:03:19 PM
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
	abstract public class esIndicationCollection : esEntityCollection
	{
		public esIndicationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "IndicationCollection";
		}

		#region Query Logic
		protected void InitQuery(esIndicationQuery query)
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
			this.InitQuery(query as esIndicationQuery);
		}
		#endregion
		
		virtual public Indication DetachEntity(Indication entity)
		{
			return base.DetachEntity(entity) as Indication;
		}
		
		virtual public Indication AttachEntity(Indication entity)
		{
			return base.AttachEntity(entity) as Indication;
		}
		
		virtual public void Combine(IndicationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Indication this[int index]
		{
			get
			{
				return base[index] as Indication;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Indication);
		}
	}



	[Serializable]
	abstract public class esIndication : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esIndicationQuery GetDynamicQuery()
		{
			return null;
		}

		public esIndication()
		{

		}

		public esIndication(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String indicationID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(indicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(indicationID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String indicationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(indicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(indicationID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String indicationID)
		{
			esIndicationQuery query = this.GetDynamicQuery();
			query.Where(query.IndicationID == indicationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String indicationID)
		{
			esParameters parms = new esParameters();
			parms.Add("IndicationID",indicationID);
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
						case "IndicationID": this.str.IndicationID = (string)value; break;							
						case "IndicationName": this.str.IndicationName = (string)value; break;							
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
		/// Maps to Indication.IndicationID
		/// </summary>
		virtual public System.String IndicationID
		{
			get
			{
				return base.GetSystemString(IndicationMetadata.ColumnNames.IndicationID);
			}
			
			set
			{
				base.SetSystemString(IndicationMetadata.ColumnNames.IndicationID, value);
			}
		}
		
		/// <summary>
		/// Maps to Indication.IndicationName
		/// </summary>
		virtual public System.String IndicationName
		{
			get
			{
				return base.GetSystemString(IndicationMetadata.ColumnNames.IndicationName);
			}
			
			set
			{
				base.SetSystemString(IndicationMetadata.ColumnNames.IndicationName, value);
			}
		}
		
		/// <summary>
		/// Maps to Indication.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(IndicationMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(IndicationMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Indication.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(IndicationMetadata.ColumnNames.InsertDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(IndicationMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Indication.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(IndicationMetadata.ColumnNames.InsertByUserID);
			}
			
			set
			{
				base.SetSystemString(IndicationMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Indication.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(IndicationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(IndicationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Indication.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(IndicationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(IndicationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esIndication entity)
			{
				this.entity = entity;
			}
			
	
			public System.String IndicationID
			{
				get
				{
					System.String data = entity.IndicationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndicationID = null;
					else entity.IndicationID = Convert.ToString(value);
				}
			}
				
			public System.String IndicationName
			{
				get
				{
					System.String data = entity.IndicationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndicationName = null;
					else entity.IndicationName = Convert.ToString(value);
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
			

			private esIndication entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esIndicationQuery query)
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
				throw new Exception("esIndication can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esIndicationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return IndicationMetadata.Meta();
			}
		}	
		

		public esQueryItem IndicationID
		{
			get
			{
				return new esQueryItem(this, IndicationMetadata.ColumnNames.IndicationID, esSystemType.String);
			}
		} 
		
		public esQueryItem IndicationName
		{
			get
			{
				return new esQueryItem(this, IndicationMetadata.ColumnNames.IndicationName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, IndicationMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, IndicationMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, IndicationMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, IndicationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, IndicationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("IndicationCollection")]
	public partial class IndicationCollection : esIndicationCollection, IEnumerable<Indication>
	{
		public IndicationCollection()
		{

		}
		
		public static implicit operator List<Indication>(IndicationCollection coll)
		{
			List<Indication> list = new List<Indication>();
			
			foreach (Indication emp in coll)
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
				return  IndicationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IndicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Indication(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Indication();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public IndicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IndicationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(IndicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Indication AddNew()
		{
			Indication entity = base.AddNewEntity() as Indication;
			
			return entity;
		}

		public Indication FindByPrimaryKey(System.String indicationID)
		{
			return base.FindByPrimaryKey(indicationID) as Indication;
		}


		#region IEnumerable<Indication> Members

		IEnumerator<Indication> IEnumerable<Indication>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Indication;
			}
		}

		#endregion
		
		private IndicationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Indication' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Indication ({IndicationID})")]
	[Serializable]
	public partial class Indication : esIndication
	{
		public Indication()
		{

		}
	
		public Indication(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return IndicationMetadata.Meta();
			}
		}
		
		
		
		override protected esIndicationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IndicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public IndicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IndicationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(IndicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private IndicationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class IndicationQuery : esIndicationQuery
	{
		public IndicationQuery()
		{

		}		
		
		public IndicationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "IndicationQuery";
        }
		
			
	}


	[Serializable]
	public partial class IndicationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected IndicationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(IndicationMetadata.ColumnNames.IndicationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = IndicationMetadata.PropertyNames.IndicationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(IndicationMetadata.ColumnNames.IndicationName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = IndicationMetadata.PropertyNames.IndicationName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(IndicationMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = IndicationMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(IndicationMetadata.ColumnNames.InsertDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IndicationMetadata.PropertyNames.InsertDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(IndicationMetadata.ColumnNames.InsertByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = IndicationMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(IndicationMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IndicationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(IndicationMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = IndicationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public IndicationMetadata Meta()
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
			 public const string IndicationID = "IndicationID";
			 public const string IndicationName = "IndicationName";
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
			 public const string IndicationID = "IndicationID";
			 public const string IndicationName = "IndicationName";
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
			lock (typeof(IndicationMetadata))
			{
				if(IndicationMetadata.mapDelegates == null)
				{
					IndicationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (IndicationMetadata.meta == null)
				{
					IndicationMetadata.meta = new IndicationMetadata();
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
				

				meta.AddTypeMap("IndicationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IndicationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Indication";
				meta.Destination = "Indication";
				
				meta.spInsert = "proc_IndicationInsert";				
				meta.spUpdate = "proc_IndicationUpdate";		
				meta.spDelete = "proc_IndicationDelete";
				meta.spLoadAll = "proc_IndicationLoadAll";
				meta.spLoadByPrimaryKey = "proc_IndicationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private IndicationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
