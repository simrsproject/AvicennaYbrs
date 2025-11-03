/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/14/2017 4:07:16 PM
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
	abstract public class esJournalGroupDetailCollection : esEntityCollectionWAuditLog
	{
		public esJournalGroupDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "JournalGroupDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esJournalGroupDetailQuery query)
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
			this.InitQuery(query as esJournalGroupDetailQuery);
		}
		#endregion
		
		virtual public JournalGroupDetail DetachEntity(JournalGroupDetail entity)
		{
			return base.DetachEntity(entity) as JournalGroupDetail;
		}
		
		virtual public JournalGroupDetail AttachEntity(JournalGroupDetail entity)
		{
			return base.AttachEntity(entity) as JournalGroupDetail;
		}
		
		virtual public void Combine(JournalGroupDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JournalGroupDetail this[int index]
		{
			get
			{
				return base[index] as JournalGroupDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JournalGroupDetail);
		}
	}



	[Serializable]
	abstract public class esJournalGroupDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJournalGroupDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esJournalGroupDetail()
		{

		}

		public esJournalGroupDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 journalDetailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalDetailID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 journalDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 journalDetailID)
		{
			esJournalGroupDetailQuery query = this.GetDynamicQuery();
			query.Where(query.JournalDetailID == journalDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 journalDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("JournalDetailID",journalDetailID);
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
						case "JournalDetailID": this.str.JournalDetailID = (string)value; break;							
						case "JournalGroupID": this.str.JournalGroupID = (string)value; break;							
						case "JournalJournalCode": this.str.JournalJournalCode = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JournalDetailID":
						
							if (value == null || value is System.Int32)
								this.JournalDetailID = (System.Int32?)value;
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
		/// Maps to JournalGroupDetail.JournalDetailID
		/// </summary>
		virtual public System.Int32? JournalDetailID
		{
			get
			{
				return base.GetSystemInt32(JournalGroupDetailMetadata.ColumnNames.JournalDetailID);
			}
			
			set
			{
				base.SetSystemInt32(JournalGroupDetailMetadata.ColumnNames.JournalDetailID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupDetail.JournalGroupID
		/// </summary>
		virtual public System.Int32? JournalGroupID
		{
			get
			{
				return base.GetSystemInt32(JournalGroupDetailMetadata.ColumnNames.JournalGroupID);
			}
			
			set
			{
				base.SetSystemInt32(JournalGroupDetailMetadata.ColumnNames.JournalGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupDetail.JournalJournalCode
		/// </summary>
		virtual public System.String JournalJournalCode
		{
			get
			{
				return base.GetSystemString(JournalGroupDetailMetadata.ColumnNames.JournalJournalCode);
			}
			
			set
			{
				base.SetSystemString(JournalGroupDetailMetadata.ColumnNames.JournalJournalCode, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(JournalGroupDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(JournalGroupDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroupDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(JournalGroupDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(JournalGroupDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esJournalGroupDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String JournalDetailID
			{
				get
				{
					System.Int32? data = entity.JournalDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalDetailID = null;
					else entity.JournalDetailID = Convert.ToInt32(value);
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
				
			public System.String JournalJournalCode
			{
				get
				{
					System.String data = entity.JournalJournalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalJournalCode = null;
					else entity.JournalJournalCode = Convert.ToString(value);
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
			

			private esJournalGroupDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJournalGroupDetailQuery query)
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
				throw new Exception("esJournalGroupDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class JournalGroupDetail : esJournalGroupDetail
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
	abstract public class esJournalGroupDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return JournalGroupDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem JournalDetailID
		{
			get
			{
				return new esQueryItem(this, JournalGroupDetailMetadata.ColumnNames.JournalDetailID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JournalGroupID
		{
			get
			{
				return new esQueryItem(this, JournalGroupDetailMetadata.ColumnNames.JournalGroupID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JournalJournalCode
		{
			get
			{
				return new esQueryItem(this, JournalGroupDetailMetadata.ColumnNames.JournalJournalCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, JournalGroupDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, JournalGroupDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JournalGroupDetailCollection")]
	public partial class JournalGroupDetailCollection : esJournalGroupDetailCollection, IEnumerable<JournalGroupDetail>
	{
		public JournalGroupDetailCollection()
		{

		}
		
		public static implicit operator List<JournalGroupDetail>(JournalGroupDetailCollection coll)
		{
			List<JournalGroupDetail> list = new List<JournalGroupDetail>();
			
			foreach (JournalGroupDetail emp in coll)
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
				return  JournalGroupDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalGroupDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JournalGroupDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JournalGroupDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public JournalGroupDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalGroupDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(JournalGroupDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public JournalGroupDetail AddNew()
		{
			JournalGroupDetail entity = base.AddNewEntity() as JournalGroupDetail;
			
			return entity;
		}

		public JournalGroupDetail FindByPrimaryKey(System.Int32 journalDetailID)
		{
			return base.FindByPrimaryKey(journalDetailID) as JournalGroupDetail;
		}


		#region IEnumerable<JournalGroupDetail> Members

		IEnumerator<JournalGroupDetail> IEnumerable<JournalGroupDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JournalGroupDetail;
			}
		}

		#endregion
		
		private JournalGroupDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JournalGroupDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("JournalGroupDetail ({JournalDetailID})")]
	[Serializable]
	public partial class JournalGroupDetail : esJournalGroupDetail
	{
		public JournalGroupDetail()
		{

		}
	
		public JournalGroupDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JournalGroupDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esJournalGroupDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalGroupDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public JournalGroupDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalGroupDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(JournalGroupDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private JournalGroupDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class JournalGroupDetailQuery : esJournalGroupDetailQuery
	{
		public JournalGroupDetailQuery()
		{

		}		
		
		public JournalGroupDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "JournalGroupDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class JournalGroupDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JournalGroupDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(JournalGroupDetailMetadata.ColumnNames.JournalDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalGroupDetailMetadata.PropertyNames.JournalDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupDetailMetadata.ColumnNames.JournalGroupID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalGroupDetailMetadata.PropertyNames.JournalGroupID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupDetailMetadata.ColumnNames.JournalJournalCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalGroupDetailMetadata.PropertyNames.JournalJournalCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupDetailMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalGroupDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupDetailMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalGroupDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public JournalGroupDetailMetadata Meta()
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
			 public const string JournalDetailID = "JournalDetailID";
			 public const string JournalGroupID = "JournalGroupID";
			 public const string JournalJournalCode = "JournalJournalCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string JournalDetailID = "JournalDetailID";
			 public const string JournalGroupID = "JournalGroupID";
			 public const string JournalJournalCode = "JournalJournalCode";
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
			lock (typeof(JournalGroupDetailMetadata))
			{
				if(JournalGroupDetailMetadata.mapDelegates == null)
				{
					JournalGroupDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JournalGroupDetailMetadata.meta == null)
				{
					JournalGroupDetailMetadata.meta = new JournalGroupDetailMetadata();
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
				

				meta.AddTypeMap("JournalDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalGroupID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalJournalCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "JournalGroupDetail";
				meta.Destination = "JournalGroupDetail";
				
				meta.spInsert = "proc_JournalGroupDetailInsert";				
				meta.spUpdate = "proc_JournalGroupDetailUpdate";		
				meta.spDelete = "proc_JournalGroupDetailDelete";
				meta.spLoadAll = "proc_JournalGroupDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_JournalGroupDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JournalGroupDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
