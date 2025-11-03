/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/18/2017 1:50:11 PM
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
	abstract public class esJournalGroupUserCollection : esEntityCollectionWAuditLog
	{
		public esJournalGroupUserCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "JournalGroupUserCollection";
		}

		#region Query Logic
		protected void InitQuery(esJournalGroupUserQuery query)
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
			this.InitQuery(query as esJournalGroupUserQuery);
		}
		#endregion
		
		virtual public JournalGroupUser DetachEntity(JournalGroupUser entity)
		{
			return base.DetachEntity(entity) as JournalGroupUser;
		}
		
		virtual public JournalGroupUser AttachEntity(JournalGroupUser entity)
		{
			return base.AttachEntity(entity) as JournalGroupUser;
		}
		
		virtual public void Combine(JournalGroupUserCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JournalGroupUser this[int index]
		{
			get
			{
				return base[index] as JournalGroupUser;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JournalGroupUser);
		}
	}



	[Serializable]
	abstract public class esJournalGroupUser : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJournalGroupUserQuery GetDynamicQuery()
		{
			return null;
		}

		public esJournalGroupUser()
		{

		}

		public esJournalGroupUser(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 journalUserID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalUserID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalUserID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 journalUserID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalUserID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalUserID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 journalUserID)
		{
			esJournalGroupUserQuery query = this.GetDynamicQuery();
			query.Where(query.JournalUserID == journalUserID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 journalUserID)
		{
			esParameters parms = new esParameters();
			parms.Add("JournalUserID",journalUserID);
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
						case "JournalUserID": this.str.JournalUserID = (string)value; break;							
						case "JournalGroupID": this.str.JournalGroupID = (string)value; break;							
						case "UserID": this.str.UserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JournalUserID":
						
							if (value == null || value is System.Int32)
								this.JournalUserID = (System.Int32?)value;
							break;
						
						case "JournalGroupID":
						
							if (value == null || value is System.Int32)
								this.JournalGroupID = (System.Int32?)value;
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
		/// Maps to JournalGroupUser.JournalUserID
		/// </summary>
		virtual public System.Int32? JournalUserID
		{
			get
			{
				return base.GetSystemInt32(JournalGroupUserMetadata.ColumnNames.JournalUserID);
			}
			
			set
			{
				base.SetSystemInt32(JournalGroupUserMetadata.ColumnNames.JournalUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupUser.JournalGroupID
		/// </summary>
		virtual public System.Int32? JournalGroupID
		{
			get
			{
				return base.GetSystemInt32(JournalGroupUserMetadata.ColumnNames.JournalGroupID);
			}
			
			set
			{
				base.SetSystemInt32(JournalGroupUserMetadata.ColumnNames.JournalGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupUser.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(JournalGroupUserMetadata.ColumnNames.UserID);
			}
			
			set
			{
				base.SetSystemString(JournalGroupUserMetadata.ColumnNames.UserID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupUser.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(JournalGroupUserMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(JournalGroupUserMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupUser.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(JournalGroupUserMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(JournalGroupUserMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esJournalGroupUser entity)
			{
				this.entity = entity;
			}
			
	
			public System.String JournalUserID
			{
				get
				{
					System.Int32? data = entity.JournalUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalUserID = null;
					else entity.JournalUserID = Convert.ToInt32(value);
				}
			}
				
			public System.String JournalGroupID
			{
				get
				{
					System.Int32? data = entity.JournalGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalGroupID = null;
					else entity.JournalGroupID = Convert.ToInt32(value);
				}
			}
				
			public System.String UserID
			{
				get
				{
					System.String data = entity.UserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserID = null;
					else entity.UserID = Convert.ToString(value);
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
			

			private esJournalGroupUser entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJournalGroupUserQuery query)
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
				throw new Exception("esJournalGroupUser can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class JournalGroupUser : esJournalGroupUser
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
	abstract public class esJournalGroupUserQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return JournalGroupUserMetadata.Meta();
			}
		}	
		

		public esQueryItem JournalUserID
		{
			get
			{
				return new esQueryItem(this, JournalGroupUserMetadata.ColumnNames.JournalUserID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JournalGroupID
		{
			get
			{
				return new esQueryItem(this, JournalGroupUserMetadata.ColumnNames.JournalGroupID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, JournalGroupUserMetadata.ColumnNames.UserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, JournalGroupUserMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, JournalGroupUserMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JournalGroupUserCollection")]
	public partial class JournalGroupUserCollection : esJournalGroupUserCollection, IEnumerable<JournalGroupUser>
	{
		public JournalGroupUserCollection()
		{

		}
		
		public static implicit operator List<JournalGroupUser>(JournalGroupUserCollection coll)
		{
			List<JournalGroupUser> list = new List<JournalGroupUser>();
			
			foreach (JournalGroupUser emp in coll)
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
				return  JournalGroupUserMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalGroupUserQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JournalGroupUser(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JournalGroupUser();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public JournalGroupUserQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalGroupUserQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(JournalGroupUserQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public JournalGroupUser AddNew()
		{
			JournalGroupUser entity = base.AddNewEntity() as JournalGroupUser;
			
			return entity;
		}

		public JournalGroupUser FindByPrimaryKey(System.Int32 journalUserID)
		{
			return base.FindByPrimaryKey(journalUserID) as JournalGroupUser;
		}


		#region IEnumerable<JournalGroupUser> Members

		IEnumerator<JournalGroupUser> IEnumerable<JournalGroupUser>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JournalGroupUser;
			}
		}

		#endregion
		
		private JournalGroupUserQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JournalGroupUser' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("JournalGroupUser ({JournalUserID})")]
	[Serializable]
	public partial class JournalGroupUser : esJournalGroupUser
	{
		public JournalGroupUser()
		{

		}
	
		public JournalGroupUser(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JournalGroupUserMetadata.Meta();
			}
		}
		
		
		
		override protected esJournalGroupUserQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalGroupUserQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public JournalGroupUserQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalGroupUserQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(JournalGroupUserQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private JournalGroupUserQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class JournalGroupUserQuery : esJournalGroupUserQuery
	{
		public JournalGroupUserQuery()
		{

		}		
		
		public JournalGroupUserQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "JournalGroupUserQuery";
        }
		
			
	}


	[Serializable]
	public partial class JournalGroupUserMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JournalGroupUserMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(JournalGroupUserMetadata.ColumnNames.JournalUserID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalGroupUserMetadata.PropertyNames.JournalUserID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupUserMetadata.ColumnNames.JournalGroupID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalGroupUserMetadata.PropertyNames.JournalGroupID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupUserMetadata.ColumnNames.UserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalGroupUserMetadata.PropertyNames.UserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupUserMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalGroupUserMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupUserMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalGroupUserMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public JournalGroupUserMetadata Meta()
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
			 public const string JournalUserID = "JournalUserID";
			 public const string JournalGroupID = "JournalGroupID";
			 public const string UserID = "UserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string JournalUserID = "JournalUserID";
			 public const string JournalGroupID = "JournalGroupID";
			 public const string UserID = "UserID";
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
			lock (typeof(JournalGroupUserMetadata))
			{
				if(JournalGroupUserMetadata.mapDelegates == null)
				{
					JournalGroupUserMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JournalGroupUserMetadata.meta == null)
				{
					JournalGroupUserMetadata.meta = new JournalGroupUserMetadata();
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
				

				meta.AddTypeMap("JournalUserID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalGroupID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "JournalGroupUser";
				meta.Destination = "JournalGroupUser";
				
				meta.spInsert = "proc_JournalGroupUserInsert";				
				meta.spUpdate = "proc_JournalGroupUserUpdate";		
				meta.spDelete = "proc_JournalGroupUserDelete";
				meta.spLoadAll = "proc_JournalGroupUserLoadAll";
				meta.spLoadByPrimaryKey = "proc_JournalGroupUserLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JournalGroupUserMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
