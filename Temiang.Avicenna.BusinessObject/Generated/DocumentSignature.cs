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
	abstract public class esDocumentSignatureCollection : esEntityCollectionWAuditLog
	{
		public esDocumentSignatureCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DocumentSignatureCollection";
		}

		#region Query Logic
		protected void InitQuery(esDocumentSignatureQuery query)
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
			this.InitQuery(query as esDocumentSignatureQuery);
		}
		#endregion
		
		virtual public DocumentSignature DetachEntity(DocumentSignature entity)
		{
			return base.DetachEntity(entity) as DocumentSignature;
		}
		
		virtual public DocumentSignature AttachEntity(DocumentSignature entity)
		{
			return base.AttachEntity(entity) as DocumentSignature;
		}
		
		virtual public void Combine(DocumentSignatureCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DocumentSignature this[int index]
		{
			get
			{
				return base[index] as DocumentSignature;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DocumentSignature);
		}
	}



	[Serializable]
	abstract public class esDocumentSignature : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDocumentSignatureQuery GetDynamicQuery()
		{
			return null;
		}

		public esDocumentSignature()
		{

		}

		public esDocumentSignature(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
		{
			esDocumentSignatureQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SRTransactionCode": this.str.SRTransactionCode = (string)value; break;							
						case "SRItemType": this.str.SRItemType = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
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
		/// Maps to DocumentSignature.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(DocumentSignatureMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignature.SRTransactionCode
		/// </summary>
		virtual public System.String SRTransactionCode
		{
			get
			{
				return base.GetSystemString(DocumentSignatureMetadata.ColumnNames.SRTransactionCode);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureMetadata.ColumnNames.SRTransactionCode, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignature.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(DocumentSignatureMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureMetadata.ColumnNames.SRItemType, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignature.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DocumentSignatureMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignature.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DocumentSignatureMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DocumentSignatureMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esDocumentSignature entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String SRTransactionCode
			{
				get
				{
					System.String data = entity.SRTransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTransactionCode = null;
					else entity.SRTransactionCode = Convert.ToString(value);
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
			

			private esDocumentSignature entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDocumentSignatureQuery query)
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
				throw new Exception("esDocumentSignature can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class DocumentSignature : esDocumentSignature
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
	abstract public class esDocumentSignatureQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DocumentSignatureMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTransactionCode
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureMetadata.ColumnNames.SRTransactionCode, esSystemType.String);
			}
		} 
		
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DocumentSignatureCollection")]
	public partial class DocumentSignatureCollection : esDocumentSignatureCollection, IEnumerable<DocumentSignature>
	{
		public DocumentSignatureCollection()
		{

		}
		
		public static implicit operator List<DocumentSignature>(DocumentSignatureCollection coll)
		{
			List<DocumentSignature> list = new List<DocumentSignature>();
			
			foreach (DocumentSignature emp in coll)
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
				return  DocumentSignatureMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentSignatureQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DocumentSignature(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DocumentSignature();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DocumentSignatureQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentSignatureQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DocumentSignatureQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DocumentSignature AddNew()
		{
			DocumentSignature entity = base.AddNewEntity() as DocumentSignature;
			
			return entity;
		}

		public DocumentSignature FindByPrimaryKey(System.String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as DocumentSignature;
		}


		#region IEnumerable<DocumentSignature> Members

		IEnumerator<DocumentSignature> IEnumerable<DocumentSignature>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DocumentSignature;
			}
		}

		#endregion
		
		private DocumentSignatureQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DocumentSignature' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DocumentSignature ({TransactionNo})")]
	[Serializable]
	public partial class DocumentSignature : esDocumentSignature
	{
		public DocumentSignature()
		{

		}
	
		public DocumentSignature(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DocumentSignatureMetadata.Meta();
			}
		}
		
		
		
		override protected esDocumentSignatureQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentSignatureQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DocumentSignatureQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentSignatureQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DocumentSignatureQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DocumentSignatureQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DocumentSignatureQuery : esDocumentSignatureQuery
	{
		public DocumentSignatureQuery()
		{

		}		
		
		public DocumentSignatureQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DocumentSignatureQuery";
        }
		
			
	}


	[Serializable]
	public partial class DocumentSignatureMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DocumentSignatureMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DocumentSignatureMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureMetadata.ColumnNames.SRTransactionCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureMetadata.PropertyNames.SRTransactionCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureMetadata.ColumnNames.SRItemType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DocumentSignatureMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DocumentSignatureMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string SRTransactionCode = "SRTransactionCode";
			 public const string SRItemType = "SRItemType";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string SRTransactionCode = "SRTransactionCode";
			 public const string SRItemType = "SRItemType";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(DocumentSignatureMetadata))
			{
				if(DocumentSignatureMetadata.mapDelegates == null)
				{
					DocumentSignatureMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DocumentSignatureMetadata.meta == null)
				{
					DocumentSignatureMetadata.meta = new DocumentSignatureMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTransactionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "DocumentSignature";
				meta.Destination = "DocumentSignature";
				
				meta.spInsert = "proc_DocumentSignatureInsert";				
				meta.spUpdate = "proc_DocumentSignatureUpdate";		
				meta.spDelete = "proc_DocumentSignatureDelete";
				meta.spLoadAll = "proc_DocumentSignatureLoadAll";
				meta.spLoadByPrimaryKey = "proc_DocumentSignatureLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DocumentSignatureMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
