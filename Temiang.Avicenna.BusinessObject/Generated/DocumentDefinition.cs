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
	abstract public class esDocumentDefinitionCollection : esEntityCollectionWAuditLog
	{
		public esDocumentDefinitionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DocumentDefinitionCollection";
		}

		#region Query Logic
		protected void InitQuery(esDocumentDefinitionQuery query)
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
			this.InitQuery(query as esDocumentDefinitionQuery);
		}
		#endregion
		
		virtual public DocumentDefinition DetachEntity(DocumentDefinition entity)
		{
			return base.DetachEntity(entity) as DocumentDefinition;
		}
		
		virtual public DocumentDefinition AttachEntity(DocumentDefinition entity)
		{
			return base.AttachEntity(entity) as DocumentDefinition;
		}
		
		virtual public void Combine(DocumentDefinitionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DocumentDefinition this[int index]
		{
			get
			{
				return base[index] as DocumentDefinition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DocumentDefinition);
		}
	}



	[Serializable]
	abstract public class esDocumentDefinition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDocumentDefinitionQuery GetDynamicQuery()
		{
			return null;
		}

		public esDocumentDefinition()
		{

		}

		public esDocumentDefinition(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 documentDefinitionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentDefinitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentDefinitionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 documentDefinitionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentDefinitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentDefinitionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 documentDefinitionID)
		{
			esDocumentDefinitionQuery query = this.GetDynamicQuery();
			query.Where(query.DocumentDefinitionID == documentDefinitionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 documentDefinitionID)
		{
			esParameters parms = new esParameters();
			parms.Add("DocumentDefinitionID",documentDefinitionID);
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
						case "DepartmentID": this.str.DepartmentID = (string)value; break;							
						case "SRFilesAnalysis": this.str.SRFilesAnalysis = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
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
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to DocumentDefinition.DocumentDefinitionID
		/// </summary>
		virtual public System.Int32? DocumentDefinitionID
		{
			get
			{
				return base.GetSystemInt32(DocumentDefinitionMetadata.ColumnNames.DocumentDefinitionID);
			}
			
			set
			{
				base.SetSystemInt32(DocumentDefinitionMetadata.ColumnNames.DocumentDefinitionID, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinition.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(DocumentDefinitionMetadata.ColumnNames.DepartmentID);
			}
			
			set
			{
				base.SetSystemString(DocumentDefinitionMetadata.ColumnNames.DepartmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinition.SRFilesAnalysis
		/// </summary>
		virtual public System.String SRFilesAnalysis
		{
			get
			{
				return base.GetSystemString(DocumentDefinitionMetadata.ColumnNames.SRFilesAnalysis);
			}
			
			set
			{
				base.SetSystemString(DocumentDefinitionMetadata.ColumnNames.SRFilesAnalysis, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinition.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(DocumentDefinitionMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(DocumentDefinitionMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DocumentDefinitionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DocumentDefinitionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentDefinition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DocumentDefinitionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DocumentDefinitionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDocumentDefinition entity)
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
				
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
				}
			}
				
			public System.String SRFilesAnalysis
			{
				get
				{
					System.String data = entity.SRFilesAnalysis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFilesAnalysis = null;
					else entity.SRFilesAnalysis = Convert.ToString(value);
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
			

			private esDocumentDefinition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDocumentDefinitionQuery query)
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
				throw new Exception("esDocumentDefinition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class DocumentDefinition : esDocumentDefinition
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
	abstract public class esDocumentDefinitionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DocumentDefinitionMetadata.Meta();
			}
		}	
		

		public esQueryItem DocumentDefinitionID
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionMetadata.ColumnNames.DocumentDefinitionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRFilesAnalysis
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionMetadata.ColumnNames.SRFilesAnalysis, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DocumentDefinitionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DocumentDefinitionCollection")]
	public partial class DocumentDefinitionCollection : esDocumentDefinitionCollection, IEnumerable<DocumentDefinition>
	{
		public DocumentDefinitionCollection()
		{

		}
		
		public static implicit operator List<DocumentDefinition>(DocumentDefinitionCollection coll)
		{
			List<DocumentDefinition> list = new List<DocumentDefinition>();
			
			foreach (DocumentDefinition emp in coll)
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
				return  DocumentDefinitionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentDefinitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DocumentDefinition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DocumentDefinition();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DocumentDefinitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentDefinitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DocumentDefinitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DocumentDefinition AddNew()
		{
			DocumentDefinition entity = base.AddNewEntity() as DocumentDefinition;
			
			return entity;
		}

		public DocumentDefinition FindByPrimaryKey(System.Int32 documentDefinitionID)
		{
			return base.FindByPrimaryKey(documentDefinitionID) as DocumentDefinition;
		}


		#region IEnumerable<DocumentDefinition> Members

		IEnumerator<DocumentDefinition> IEnumerable<DocumentDefinition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DocumentDefinition;
			}
		}

		#endregion
		
		private DocumentDefinitionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DocumentDefinition' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DocumentDefinition ({DocumentDefinitionID})")]
	[Serializable]
	public partial class DocumentDefinition : esDocumentDefinition
	{
		public DocumentDefinition()
		{

		}
	
		public DocumentDefinition(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DocumentDefinitionMetadata.Meta();
			}
		}
		
		
		
		override protected esDocumentDefinitionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentDefinitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DocumentDefinitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentDefinitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DocumentDefinitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DocumentDefinitionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DocumentDefinitionQuery : esDocumentDefinitionQuery
	{
		public DocumentDefinitionQuery()
		{

		}		
		
		public DocumentDefinitionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DocumentDefinitionQuery";
        }
		
			
	}


	[Serializable]
	public partial class DocumentDefinitionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DocumentDefinitionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DocumentDefinitionMetadata.ColumnNames.DocumentDefinitionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DocumentDefinitionMetadata.PropertyNames.DocumentDefinitionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionMetadata.ColumnNames.DepartmentID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentDefinitionMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionMetadata.ColumnNames.SRFilesAnalysis, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentDefinitionMetadata.PropertyNames.SRFilesAnalysis;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DocumentDefinitionMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DocumentDefinitionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentDefinitionMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentDefinitionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DocumentDefinitionMetadata Meta()
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
			 public const string DepartmentID = "DepartmentID";
			 public const string SRFilesAnalysis = "SRFilesAnalysis";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DocumentDefinitionID = "DocumentDefinitionID";
			 public const string DepartmentID = "DepartmentID";
			 public const string SRFilesAnalysis = "SRFilesAnalysis";
			 public const string IsActive = "IsActive";
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
			lock (typeof(DocumentDefinitionMetadata))
			{
				if(DocumentDefinitionMetadata.mapDelegates == null)
				{
					DocumentDefinitionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DocumentDefinitionMetadata.meta == null)
				{
					DocumentDefinitionMetadata.meta = new DocumentDefinitionMetadata();
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
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFilesAnalysis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DocumentDefinition";
				meta.Destination = "DocumentDefinition";
				
				meta.spInsert = "proc_DocumentDefinitionInsert";				
				meta.spUpdate = "proc_DocumentDefinitionUpdate";		
				meta.spDelete = "proc_DocumentDefinitionDelete";
				meta.spLoadAll = "proc_DocumentDefinitionLoadAll";
				meta.spLoadByPrimaryKey = "proc_DocumentDefinitionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DocumentDefinitionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
