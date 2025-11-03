/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esAnalysisDocumentCollection : esEntityCollectionWAuditLog
	{
		public esAnalysisDocumentCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AnalysisDocumentCollection";
		}

		#region Query Logic
		protected void InitQuery(esAnalysisDocumentQuery query)
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
			this.InitQuery(query as esAnalysisDocumentQuery);
		}
		#endregion
		
		virtual public AnalysisDocument DetachEntity(AnalysisDocument entity)
		{
			return base.DetachEntity(entity) as AnalysisDocument;
		}
		
		virtual public AnalysisDocument AttachEntity(AnalysisDocument entity)
		{
			return base.AttachEntity(entity) as AnalysisDocument;
		}
		
		virtual public void Combine(AnalysisDocumentCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AnalysisDocument this[int index]
		{
			get
			{
				return base[index] as AnalysisDocument;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AnalysisDocument);
		}
	}



	[Serializable]
	abstract public class esAnalysisDocument : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAnalysisDocumentQuery GetDynamicQuery()
		{
			return null;
		}

		public esAnalysisDocument()
		{

		}

		public esAnalysisDocument(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo)
		{
			esAnalysisDocumentQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "SRFilesAnalysis": this.str.SRFilesAnalysis = (string)value; break;							
						case "FilesReceiveDate": this.str.FilesReceiveDate = (string)value; break;							
						case "FilesAcceptanceDate": this.str.FilesAcceptanceDate = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "SRCompleteStatusRM": this.str.SRCompleteStatusRM = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "FilesReceiveDate":
						
							if (value == null || value is System.DateTime)
								this.FilesReceiveDate = (System.DateTime?)value;
							break;
						
						case "FilesAcceptanceDate":
						
							if (value == null || value is System.DateTime)
								this.FilesAcceptanceDate = (System.DateTime?)value;
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
		/// Maps to AnalysisDocument.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.SRFilesAnalysis
		/// </summary>
		virtual public System.String SRFilesAnalysis
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentMetadata.ColumnNames.SRFilesAnalysis);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentMetadata.ColumnNames.SRFilesAnalysis, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.FilesReceiveDate
		/// </summary>
		virtual public System.DateTime? FilesReceiveDate
		{
			get
			{
				return base.GetSystemDateTime(AnalysisDocumentMetadata.ColumnNames.FilesReceiveDate);
			}
			
			set
			{
				base.SetSystemDateTime(AnalysisDocumentMetadata.ColumnNames.FilesReceiveDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.FilesAcceptanceDate
		/// </summary>
		virtual public System.DateTime? FilesAcceptanceDate
		{
			get
			{
				return base.GetSystemDateTime(AnalysisDocumentMetadata.ColumnNames.FilesAcceptanceDate);
			}
			
			set
			{
				base.SetSystemDateTime(AnalysisDocumentMetadata.ColumnNames.FilesAcceptanceDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.SRCompleteStatusRM
		/// </summary>
		virtual public System.String SRCompleteStatusRM
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentMetadata.ColumnNames.SRCompleteStatusRM);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentMetadata.ColumnNames.SRCompleteStatusRM, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AnalysisDocumentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AnalysisDocumentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocument.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAnalysisDocument entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
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
				
			public System.String FilesReceiveDate
			{
				get
				{
					System.DateTime? data = entity.FilesReceiveDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FilesReceiveDate = null;
					else entity.FilesReceiveDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String FilesAcceptanceDate
			{
				get
				{
					System.DateTime? data = entity.FilesAcceptanceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FilesAcceptanceDate = null;
					else entity.FilesAcceptanceDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
				
			public System.String SRCompleteStatusRM
			{
				get
				{
					System.String data = entity.SRCompleteStatusRM;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCompleteStatusRM = null;
					else entity.SRCompleteStatusRM = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			

			private esAnalysisDocument entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAnalysisDocumentQuery query)
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
				throw new Exception("esAnalysisDocument can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AnalysisDocument : esAnalysisDocument
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
	abstract public class esAnalysisDocumentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AnalysisDocumentMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRFilesAnalysis
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.SRFilesAnalysis, esSystemType.String);
			}
		} 
		
		public esQueryItem FilesReceiveDate
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.FilesReceiveDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem FilesAcceptanceDate
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.FilesAcceptanceDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCompleteStatusRM
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.SRCompleteStatusRM, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AnalysisDocumentCollection")]
	public partial class AnalysisDocumentCollection : esAnalysisDocumentCollection, IEnumerable<AnalysisDocument>
	{
		public AnalysisDocumentCollection()
		{

		}
		
		public static implicit operator List<AnalysisDocument>(AnalysisDocumentCollection coll)
		{
			List<AnalysisDocument> list = new List<AnalysisDocument>();
			
			foreach (AnalysisDocument emp in coll)
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
				return  AnalysisDocumentMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AnalysisDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AnalysisDocument(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AnalysisDocument();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AnalysisDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AnalysisDocumentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AnalysisDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AnalysisDocument AddNew()
		{
			AnalysisDocument entity = base.AddNewEntity() as AnalysisDocument;
			
			return entity;
		}

		public AnalysisDocument FindByPrimaryKey(System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as AnalysisDocument;
		}


		#region IEnumerable<AnalysisDocument> Members

		IEnumerator<AnalysisDocument> IEnumerable<AnalysisDocument>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AnalysisDocument;
			}
		}

		#endregion
		
		private AnalysisDocumentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AnalysisDocument' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AnalysisDocument ({RegistrationNo})")]
	[Serializable]
	public partial class AnalysisDocument : esAnalysisDocument
	{
		public AnalysisDocument()
		{

		}
	
		public AnalysisDocument(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AnalysisDocumentMetadata.Meta();
			}
		}
		
		
		
		override protected esAnalysisDocumentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AnalysisDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AnalysisDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AnalysisDocumentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AnalysisDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AnalysisDocumentQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AnalysisDocumentQuery : esAnalysisDocumentQuery
	{
		public AnalysisDocumentQuery()
		{

		}		
		
		public AnalysisDocumentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AnalysisDocumentQuery";
        }
		
			
	}


	[Serializable]
	public partial class AnalysisDocumentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AnalysisDocumentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.SRFilesAnalysis, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.SRFilesAnalysis;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.FilesReceiveDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.FilesReceiveDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.FilesAcceptanceDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.FilesAcceptanceDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.ParamedicID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.SRCompleteStatusRM, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.SRCompleteStatusRM;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AnalysisDocumentMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRFilesAnalysis = "SRFilesAnalysis";
			 public const string FilesReceiveDate = "FilesReceiveDate";
			 public const string FilesAcceptanceDate = "FilesAcceptanceDate";
			 public const string ParamedicID = "ParamedicID";
			 public const string SRCompleteStatusRM = "SRCompleteStatusRM";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRFilesAnalysis = "SRFilesAnalysis";
			 public const string FilesReceiveDate = "FilesReceiveDate";
			 public const string FilesAcceptanceDate = "FilesAcceptanceDate";
			 public const string ParamedicID = "ParamedicID";
			 public const string SRCompleteStatusRM = "SRCompleteStatusRM";
			 public const string Notes = "Notes";
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
			lock (typeof(AnalysisDocumentMetadata))
			{
				if(AnalysisDocumentMetadata.mapDelegates == null)
				{
					AnalysisDocumentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AnalysisDocumentMetadata.meta == null)
				{
					AnalysisDocumentMetadata.meta = new AnalysisDocumentMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFilesAnalysis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FilesReceiveDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FilesAcceptanceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCompleteStatusRM", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AnalysisDocument";
				meta.Destination = "AnalysisDocument";
				
				meta.spInsert = "proc_AnalysisDocumentInsert";				
				meta.spUpdate = "proc_AnalysisDocumentUpdate";		
				meta.spDelete = "proc_AnalysisDocumentDelete";
				meta.spLoadAll = "proc_AnalysisDocumentLoadAll";
				meta.spLoadByPrimaryKey = "proc_AnalysisDocumentLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AnalysisDocumentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
