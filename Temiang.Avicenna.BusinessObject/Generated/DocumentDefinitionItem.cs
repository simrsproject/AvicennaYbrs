/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:14 PM
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
	abstract public class esDocumentDefinitionItemCollection : esEntityCollectionWAuditLog
	{
		public esDocumentDefinitionItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DocumentDefinitionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esDocumentDefinitionItemQuery query)
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
			this.InitQuery(query as esDocumentDefinitionItemQuery);
		}
		#endregion
		
		virtual public DocumentDefinitionItem DetachEntity(DocumentDefinitionItem entity)
		{
			return base.DetachEntity(entity) as DocumentDefinitionItem;
		}
		
		virtual public DocumentDefinitionItem AttachEntity(DocumentDefinitionItem entity)
		{
			return base.AttachEntity(entity) as DocumentDefinitionItem;
		}
		
		virtual public void Combine(DocumentDefinitionItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DocumentDefinitionItem this[int index]
		{
			get
			{
				return base[index] as DocumentDefinitionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DocumentDefinitionItem);
		}
	}



	[Serializable]
	abstract public class esDocumentDefinitionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDocumentDefinitionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esDocumentDefinitionItem()
		{

		}

		public esDocumentDefinitionItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 documentDefinitionID, System.Int32 documentFilesID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentDefinitionID, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentDefinitionID, documentFilesID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 documentDefinitionID, System.Int32 documentFilesID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentDefinitionID, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentDefinitionID, documentFilesID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 documentDefinitionID, System.Int32 documentFilesID)
		{
			esDocumentDefinitionItemQuery query = this.GetDynamicQuery();
			query.Where(query.DocumentDefinitionID == documentDefinitionID, query.DocumentFilesID == documentFilesID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 documentDefinitionID, System.Int32 documentFilesID)
		{
			esParameters parms = new esParameters();
			parms.Add("DocumentDefinitionID",documentDefinitionID);			parms.Add("DocumentFilesID",documentFilesID);
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
						case "DocumentDefinitionID": this.str.DocumentDefinitionID = (string)value; break;							
						case "DocumentFilesID": this.str.DocumentFilesID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DocumentDefinitionID":
						
							if (value == null || value is System.Int32)
								this.DocumentDefinitionID = (System.Int32?)value;
							break;
						
						case "DocumentFilesID":
						
							if (value == null || value is System.Int32)
								this.DocumentFilesID = (System.Int32?)value;
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
		/// Maps to DocumentDefinitionItem.DocumentDefinitionID
		/// </summary>
		virtual public System.Int32? DocumentDefinitionID
		{
			get
			{
				return base.GetSystemInt32(DocumentDefinitionItemMetadata.ColumnNames.DocumentDefinitionID);
			}
			
			set
			{
				base.SetSystemInt32(DocumentDefinitionItemMetadata.ColumnNames.DocumentDefinitionID, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinitionItem.DocumentFilesID
		/// </summary>
		virtual public System.Int32? DocumentFilesID
		{
			get
			{
				return base.GetSystemInt32(DocumentDefinitionItemMetadata.ColumnNames.DocumentFilesID);
			}
			
			set
			{
				base.SetSystemInt32(DocumentDefinitionItemMetadata.ColumnNames.DocumentFilesID, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinitionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DocumentDefinitionItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DocumentDefinitionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinitionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DocumentDefinitionItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DocumentDefinitionItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDocumentDefinitionItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DocumentDefinitionID
			{
				get
				{
					System.Int32? data = entity.DocumentDefinitionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentDefinitionID = null;
					else entity.DocumentDefinitionID = Convert.ToInt32(value);
				}
			}
				
			public System.String DocumentFilesID
			{
				get
				{
					System.Int32? data = entity.DocumentFilesID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentFilesID = null;
					else entity.DocumentFilesID = Convert.ToInt32(value);
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
			

			private esDocumentDefinitionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDocumentDefinitionItemQuery query)
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
				throw new Exception("esDocumentDefinitionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class DocumentDefinitionItem : esDocumentDefinitionItem
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
	abstract public class esDocumentDefinitionItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DocumentDefinitionItemMetadata.Meta();
			}
		}	
		

		public esQueryItem DocumentDefinitionID
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionItemMetadata.ColumnNames.DocumentDefinitionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DocumentFilesID
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionItemMetadata.ColumnNames.DocumentFilesID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DocumentDefinitionItemCollection")]
	public partial class DocumentDefinitionItemCollection : esDocumentDefinitionItemCollection, IEnumerable<DocumentDefinitionItem>
	{
		public DocumentDefinitionItemCollection()
		{

		}
		
		public static implicit operator List<DocumentDefinitionItem>(DocumentDefinitionItemCollection coll)
		{
			List<DocumentDefinitionItem> list = new List<DocumentDefinitionItem>();
			
			foreach (DocumentDefinitionItem emp in coll)
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
				return  DocumentDefinitionItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentDefinitionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DocumentDefinitionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DocumentDefinitionItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DocumentDefinitionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentDefinitionItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DocumentDefinitionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DocumentDefinitionItem AddNew()
		{
			DocumentDefinitionItem entity = base.AddNewEntity() as DocumentDefinitionItem;
			
			return entity;
		}

		public DocumentDefinitionItem FindByPrimaryKey(System.Int32 documentDefinitionID, System.Int32 documentFilesID)
		{
			return base.FindByPrimaryKey(documentDefinitionID, documentFilesID) as DocumentDefinitionItem;
		}


		#region IEnumerable<DocumentDefinitionItem> Members

		IEnumerator<DocumentDefinitionItem> IEnumerable<DocumentDefinitionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DocumentDefinitionItem;
			}
		}

		#endregion
		
		private DocumentDefinitionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DocumentDefinitionItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DocumentDefinitionItem ({DocumentDefinitionID},{DocumentFilesID})")]
	[Serializable]
	public partial class DocumentDefinitionItem : esDocumentDefinitionItem
	{
		public DocumentDefinitionItem()
		{

		}
	
		public DocumentDefinitionItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DocumentDefinitionItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDocumentDefinitionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentDefinitionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DocumentDefinitionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentDefinitionItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DocumentDefinitionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DocumentDefinitionItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DocumentDefinitionItemQuery : esDocumentDefinitionItemQuery
	{
		public DocumentDefinitionItemQuery()
		{

		}		
		
		public DocumentDefinitionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DocumentDefinitionItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class DocumentDefinitionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DocumentDefinitionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DocumentDefinitionItemMetadata.ColumnNames.DocumentDefinitionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DocumentDefinitionItemMetadata.PropertyNames.DocumentDefinitionID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionItemMetadata.ColumnNames.DocumentFilesID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DocumentDefinitionItemMetadata.PropertyNames.DocumentFilesID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionItemMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DocumentDefinitionItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionItemMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentDefinitionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DocumentDefinitionItemMetadata Meta()
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
			 public const string DocumentDefinitionID = "DocumentDefinitionID";
			 public const string DocumentFilesID = "DocumentFilesID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DocumentDefinitionID = "DocumentDefinitionID";
			 public const string DocumentFilesID = "DocumentFilesID";
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
			lock (typeof(DocumentDefinitionItemMetadata))
			{
				if(DocumentDefinitionItemMetadata.mapDelegates == null)
				{
					DocumentDefinitionItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DocumentDefinitionItemMetadata.meta == null)
				{
					DocumentDefinitionItemMetadata.meta = new DocumentDefinitionItemMetadata();
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
				

				meta.AddTypeMap("DocumentDefinitionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DocumentFilesID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DocumentDefinitionItem";
				meta.Destination = "DocumentDefinitionItem";
				
				meta.spInsert = "proc_DocumentDefinitionItemInsert";				
				meta.spUpdate = "proc_DocumentDefinitionItemUpdate";		
				meta.spDelete = "proc_DocumentDefinitionItemDelete";
				meta.spLoadAll = "proc_DocumentDefinitionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_DocumentDefinitionItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DocumentDefinitionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
