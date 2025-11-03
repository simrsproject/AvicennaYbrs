/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
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
	abstract public class esSubSpecialtyCollection : esEntityCollectionWAuditLog
	{
		public esSubSpecialtyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SubSpecialtyCollection";
		}

		#region Query Logic
		protected void InitQuery(esSubSpecialtyQuery query)
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
			this.InitQuery(query as esSubSpecialtyQuery);
		}
		#endregion
		
		virtual public SubSpecialty DetachEntity(SubSpecialty entity)
		{
			return base.DetachEntity(entity) as SubSpecialty;
		}
		
		virtual public SubSpecialty AttachEntity(SubSpecialty entity)
		{
			return base.AttachEntity(entity) as SubSpecialty;
		}
		
		virtual public void Combine(SubSpecialtyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SubSpecialty this[int index]
		{
			get
			{
				return base[index] as SubSpecialty;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SubSpecialty);
		}
	}



	[Serializable]
	abstract public class esSubSpecialty : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSubSpecialtyQuery GetDynamicQuery()
		{
			return null;
		}

		public esSubSpecialty()
		{

		}

		public esSubSpecialty(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String subSpecialtyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subSpecialtyID);
			else
				return LoadByPrimaryKeyStoredProcedure(subSpecialtyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String subSpecialtyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subSpecialtyID);
			else
				return LoadByPrimaryKeyStoredProcedure(subSpecialtyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String subSpecialtyID)
		{
			esSubSpecialtyQuery query = this.GetDynamicQuery();
			query.Where(query.SubSpecialtyID == subSpecialtyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String subSpecialtyID)
		{
			esParameters parms = new esParameters();
			parms.Add("SubSpecialtyID",subSpecialtyID);
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
						case "SubSpecialtyID": this.str.SubSpecialtyID = (string)value; break;							
						case "SRSpecialty": this.str.SRSpecialty = (string)value; break;							
						case "SubSpecialtyName": this.str.SubSpecialtyName = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to SubSpecialty.SubSpecialtyID
		/// </summary>
		virtual public System.String SubSpecialtyID
		{
			get
			{
				return base.GetSystemString(SubSpecialtyMetadata.ColumnNames.SubSpecialtyID);
			}
			
			set
			{
				base.SetSystemString(SubSpecialtyMetadata.ColumnNames.SubSpecialtyID, value);
			}
		}
		
		/// <summary>
		/// Maps to SubSpecialty.SRSpecialty
		/// </summary>
		virtual public System.String SRSpecialty
		{
			get
			{
				return base.GetSystemString(SubSpecialtyMetadata.ColumnNames.SRSpecialty);
			}
			
			set
			{
				base.SetSystemString(SubSpecialtyMetadata.ColumnNames.SRSpecialty, value);
			}
		}
		
		/// <summary>
		/// Maps to SubSpecialty.SubSpecialtyName
		/// </summary>
		virtual public System.String SubSpecialtyName
		{
			get
			{
				return base.GetSystemString(SubSpecialtyMetadata.ColumnNames.SubSpecialtyName);
			}
			
			set
			{
				base.SetSystemString(SubSpecialtyMetadata.ColumnNames.SubSpecialtyName, value);
			}
		}
		
		/// <summary>
		/// Maps to SubSpecialty.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SubSpecialtyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SubSpecialtyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SubSpecialty.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SubSpecialtyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SubSpecialtyMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSubSpecialty entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SubSpecialtyID
			{
				get
				{
					System.String data = entity.SubSpecialtyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubSpecialtyID = null;
					else entity.SubSpecialtyID = Convert.ToString(value);
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
				
			public System.String SubSpecialtyName
			{
				get
				{
					System.String data = entity.SubSpecialtyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubSpecialtyName = null;
					else entity.SubSpecialtyName = Convert.ToString(value);
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
			

			private esSubSpecialty entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSubSpecialtyQuery query)
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
				throw new Exception("esSubSpecialty can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SubSpecialty : esSubSpecialty
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
	abstract public class esSubSpecialtyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SubSpecialtyMetadata.Meta();
			}
		}	
		

		public esQueryItem SubSpecialtyID
		{
			get
			{
				return new esQueryItem(this, SubSpecialtyMetadata.ColumnNames.SubSpecialtyID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRSpecialty
		{
			get
			{
				return new esQueryItem(this, SubSpecialtyMetadata.ColumnNames.SRSpecialty, esSystemType.String);
			}
		} 
		
		public esQueryItem SubSpecialtyName
		{
			get
			{
				return new esQueryItem(this, SubSpecialtyMetadata.ColumnNames.SubSpecialtyName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SubSpecialtyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SubSpecialtyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SubSpecialtyCollection")]
	public partial class SubSpecialtyCollection : esSubSpecialtyCollection, IEnumerable<SubSpecialty>
	{
		public SubSpecialtyCollection()
		{

		}
		
		public static implicit operator List<SubSpecialty>(SubSpecialtyCollection coll)
		{
			List<SubSpecialty> list = new List<SubSpecialty>();
			
			foreach (SubSpecialty emp in coll)
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
				return  SubSpecialtyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubSpecialtyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SubSpecialty(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SubSpecialty();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SubSpecialtyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubSpecialtyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SubSpecialtyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SubSpecialty AddNew()
		{
			SubSpecialty entity = base.AddNewEntity() as SubSpecialty;
			
			return entity;
		}

		public SubSpecialty FindByPrimaryKey(System.String subSpecialtyID)
		{
			return base.FindByPrimaryKey(subSpecialtyID) as SubSpecialty;
		}


		#region IEnumerable<SubSpecialty> Members

		IEnumerator<SubSpecialty> IEnumerable<SubSpecialty>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SubSpecialty;
			}
		}

		#endregion
		
		private SubSpecialtyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SubSpecialty' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SubSpecialty ({SubSpecialtyID})")]
	[Serializable]
	public partial class SubSpecialty : esSubSpecialty
	{
		public SubSpecialty()
		{

		}
	
		public SubSpecialty(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SubSpecialtyMetadata.Meta();
			}
		}
		
		
		
		override protected esSubSpecialtyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubSpecialtyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SubSpecialtyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubSpecialtyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SubSpecialtyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SubSpecialtyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SubSpecialtyQuery : esSubSpecialtyQuery
	{
		public SubSpecialtyQuery()
		{

		}		
		
		public SubSpecialtyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SubSpecialtyQuery";
        }
		
			
	}


	[Serializable]
	public partial class SubSpecialtyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SubSpecialtyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SubSpecialtyMetadata.ColumnNames.SubSpecialtyID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SubSpecialtyMetadata.PropertyNames.SubSpecialtyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubSpecialtyMetadata.ColumnNames.SRSpecialty, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SubSpecialtyMetadata.PropertyNames.SRSpecialty;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubSpecialtyMetadata.ColumnNames.SubSpecialtyName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SubSpecialtyMetadata.PropertyNames.SubSpecialtyName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubSpecialtyMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SubSpecialtyMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubSpecialtyMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SubSpecialtyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SubSpecialtyMetadata Meta()
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
			 public const string SubSpecialtyID = "SubSpecialtyID";
			 public const string SRSpecialty = "SRSpecialty";
			 public const string SubSpecialtyName = "SubSpecialtyName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SubSpecialtyID = "SubSpecialtyID";
			 public const string SRSpecialty = "SRSpecialty";
			 public const string SubSpecialtyName = "SubSpecialtyName";
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
			lock (typeof(SubSpecialtyMetadata))
			{
				if(SubSpecialtyMetadata.mapDelegates == null)
				{
					SubSpecialtyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SubSpecialtyMetadata.meta == null)
				{
					SubSpecialtyMetadata.meta = new SubSpecialtyMetadata();
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
				

				meta.AddTypeMap("SubSpecialtyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSpecialty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubSpecialtyName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "SubSpecialty";
				meta.Destination = "SubSpecialty";
				
				meta.spInsert = "proc_SubSpecialtyInsert";				
				meta.spUpdate = "proc_SubSpecialtyUpdate";		
				meta.spDelete = "proc_SubSpecialtyDelete";
				meta.spLoadAll = "proc_SubSpecialtyLoadAll";
				meta.spLoadByPrimaryKey = "proc_SubSpecialtyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SubSpecialtyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
