/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/7/2013 3:37:41 PM
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
	abstract public class esExamSummaryCollection : esEntityCollectionWAuditLog
	{
		public esExamSummaryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ExamSummaryCollection";
		}

		#region Query Logic
		protected void InitQuery(esExamSummaryQuery query)
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
			this.InitQuery(query as esExamSummaryQuery);
		}
		#endregion
		
		virtual public ExamSummary DetachEntity(ExamSummary entity)
		{
			return base.DetachEntity(entity) as ExamSummary;
		}
		
		virtual public ExamSummary AttachEntity(ExamSummary entity)
		{
			return base.AttachEntity(entity) as ExamSummary;
		}
		
		virtual public void Combine(ExamSummaryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ExamSummary this[int index]
		{
			get
			{
				return base[index] as ExamSummary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ExamSummary);
		}
	}



	[Serializable]
	abstract public class esExamSummary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esExamSummaryQuery GetDynamicQuery()
		{
			return null;
		}

		public esExamSummary()
		{

		}

		public esExamSummary(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String examSummaryID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(examSummaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(examSummaryID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String examSummaryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(examSummaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(examSummaryID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String examSummaryID)
		{
			esExamSummaryQuery query = this.GetDynamicQuery();
			query.Where(query.ExamSummaryID == examSummaryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String examSummaryID)
		{
			esParameters parms = new esParameters();
			parms.Add("ExamSummaryID",examSummaryID);
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
						case "ExamSummaryID": this.str.ExamSummaryID = (string)value; break;							
						case "ExamSummaryName": this.str.ExamSummaryName = (string)value; break;							
						case "ExamSummaryNameEng": this.str.ExamSummaryNameEng = (string)value; break;							
						case "SRExamSummaryType": this.str.SRExamSummaryType = (string)value; break;							
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
		/// Maps to ExamSummary.ExamSummaryID
		/// </summary>
		virtual public System.String ExamSummaryID
		{
			get
			{
				return base.GetSystemString(ExamSummaryMetadata.ColumnNames.ExamSummaryID);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryMetadata.ColumnNames.ExamSummaryID, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummary.ExamSummaryName
		/// </summary>
		virtual public System.String ExamSummaryName
		{
			get
			{
				return base.GetSystemString(ExamSummaryMetadata.ColumnNames.ExamSummaryName);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryMetadata.ColumnNames.ExamSummaryName, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummary.ExamSummaryNameEng
		/// </summary>
		virtual public System.String ExamSummaryNameEng
		{
			get
			{
				return base.GetSystemString(ExamSummaryMetadata.ColumnNames.ExamSummaryNameEng);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryMetadata.ColumnNames.ExamSummaryNameEng, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummary.SRExamSummaryType
		/// </summary>
		virtual public System.String SRExamSummaryType
		{
			get
			{
				return base.GetSystemString(ExamSummaryMetadata.ColumnNames.SRExamSummaryType);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryMetadata.ColumnNames.SRExamSummaryType, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummary.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ExamSummaryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ExamSummaryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummary.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ExamSummaryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esExamSummary entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ExamSummaryID
			{
				get
				{
					System.String data = entity.ExamSummaryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExamSummaryID = null;
					else entity.ExamSummaryID = Convert.ToString(value);
				}
			}
				
			public System.String ExamSummaryName
			{
				get
				{
					System.String data = entity.ExamSummaryName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExamSummaryName = null;
					else entity.ExamSummaryName = Convert.ToString(value);
				}
			}
				
			public System.String ExamSummaryNameEng
			{
				get
				{
					System.String data = entity.ExamSummaryNameEng;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExamSummaryNameEng = null;
					else entity.ExamSummaryNameEng = Convert.ToString(value);
				}
			}
				
			public System.String SRExamSummaryType
			{
				get
				{
					System.String data = entity.SRExamSummaryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRExamSummaryType = null;
					else entity.SRExamSummaryType = Convert.ToString(value);
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
			

			private esExamSummary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esExamSummaryQuery query)
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
				throw new Exception("esExamSummary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ExamSummary : esExamSummary
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
	abstract public class esExamSummaryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ExamSummaryMetadata.Meta();
			}
		}	
		

		public esQueryItem ExamSummaryID
		{
			get
			{
				return new esQueryItem(this, ExamSummaryMetadata.ColumnNames.ExamSummaryID, esSystemType.String);
			}
		} 
		
		public esQueryItem ExamSummaryName
		{
			get
			{
				return new esQueryItem(this, ExamSummaryMetadata.ColumnNames.ExamSummaryName, esSystemType.String);
			}
		} 
		
		public esQueryItem ExamSummaryNameEng
		{
			get
			{
				return new esQueryItem(this, ExamSummaryMetadata.ColumnNames.ExamSummaryNameEng, esSystemType.String);
			}
		} 
		
		public esQueryItem SRExamSummaryType
		{
			get
			{
				return new esQueryItem(this, ExamSummaryMetadata.ColumnNames.SRExamSummaryType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ExamSummaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ExamSummaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ExamSummaryCollection")]
	public partial class ExamSummaryCollection : esExamSummaryCollection, IEnumerable<ExamSummary>
	{
		public ExamSummaryCollection()
		{

		}
		
		public static implicit operator List<ExamSummary>(ExamSummaryCollection coll)
		{
			List<ExamSummary> list = new List<ExamSummary>();
			
			foreach (ExamSummary emp in coll)
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
				return  ExamSummaryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ExamSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ExamSummary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ExamSummary();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ExamSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ExamSummaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ExamSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ExamSummary AddNew()
		{
			ExamSummary entity = base.AddNewEntity() as ExamSummary;
			
			return entity;
		}

		public ExamSummary FindByPrimaryKey(System.String examSummaryID)
		{
			return base.FindByPrimaryKey(examSummaryID) as ExamSummary;
		}


		#region IEnumerable<ExamSummary> Members

		IEnumerator<ExamSummary> IEnumerable<ExamSummary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ExamSummary;
			}
		}

		#endregion
		
		private ExamSummaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ExamSummary' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ExamSummary ({ExamSummaryID})")]
	[Serializable]
	public partial class ExamSummary : esExamSummary
	{
		public ExamSummary()
		{

		}
	
		public ExamSummary(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ExamSummaryMetadata.Meta();
			}
		}
		
		
		
		override protected esExamSummaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ExamSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ExamSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ExamSummaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ExamSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ExamSummaryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ExamSummaryQuery : esExamSummaryQuery
	{
		public ExamSummaryQuery()
		{

		}		
		
		public ExamSummaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ExamSummaryQuery";
        }
		
			
	}


	[Serializable]
	public partial class ExamSummaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ExamSummaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ExamSummaryMetadata.ColumnNames.ExamSummaryID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryMetadata.PropertyNames.ExamSummaryID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryMetadata.ColumnNames.ExamSummaryName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryMetadata.PropertyNames.ExamSummaryName;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryMetadata.ColumnNames.ExamSummaryNameEng, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryMetadata.PropertyNames.ExamSummaryNameEng;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryMetadata.ColumnNames.SRExamSummaryType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryMetadata.PropertyNames.SRExamSummaryType;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ExamSummaryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ExamSummaryMetadata Meta()
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
			 public const string ExamSummaryID = "ExamSummaryID";
			 public const string ExamSummaryName = "ExamSummaryName";
			 public const string ExamSummaryNameEng = "ExamSummaryNameEng";
			 public const string SRExamSummaryType = "SRExamSummaryType";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ExamSummaryID = "ExamSummaryID";
			 public const string ExamSummaryName = "ExamSummaryName";
			 public const string ExamSummaryNameEng = "ExamSummaryNameEng";
			 public const string SRExamSummaryType = "SRExamSummaryType";
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
			lock (typeof(ExamSummaryMetadata))
			{
				if(ExamSummaryMetadata.mapDelegates == null)
				{
					ExamSummaryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ExamSummaryMetadata.meta == null)
				{
					ExamSummaryMetadata.meta = new ExamSummaryMetadata();
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
				

				meta.AddTypeMap("ExamSummaryID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExamSummaryName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExamSummaryNameEng", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRExamSummaryType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ExamSummary";
				meta.Destination = "ExamSummary";
				
				meta.spInsert = "proc_ExamSummaryInsert";				
				meta.spUpdate = "proc_ExamSummaryUpdate";		
				meta.spDelete = "proc_ExamSummaryDelete";
				meta.spLoadAll = "proc_ExamSummaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_ExamSummaryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ExamSummaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
