/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/18/2016 10:13:06 AM
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
	abstract public class esClinicalExamResultsCollection : esEntityCollectionWAuditLog
	{
		public esClinicalExamResultsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ClinicalExamResultsCollection";
		}

		#region Query Logic
		protected void InitQuery(esClinicalExamResultsQuery query)
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
			this.InitQuery(query as esClinicalExamResultsQuery);
		}
		#endregion
		
		virtual public ClinicalExamResults DetachEntity(ClinicalExamResults entity)
		{
			return base.DetachEntity(entity) as ClinicalExamResults;
		}
		
		virtual public ClinicalExamResults AttachEntity(ClinicalExamResults entity)
		{
			return base.AttachEntity(entity) as ClinicalExamResults;
		}
		
		virtual public void Combine(ClinicalExamResultsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ClinicalExamResults this[int index]
		{
			get
			{
				return base[index] as ClinicalExamResults;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClinicalExamResults);
		}
	}



	[Serializable]
	abstract public class esClinicalExamResults : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClinicalExamResultsQuery GetDynamicQuery()
		{
			return null;
		}

		public esClinicalExamResults()
		{

		}

		public esClinicalExamResults(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paramedicID, System.String registrationNo, System.String title)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, registrationNo, title);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, registrationNo, title);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paramedicID, System.String registrationNo, System.String title)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, registrationNo, title);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, registrationNo, title);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paramedicID, System.String registrationNo, System.String title)
		{
			esClinicalExamResultsQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID == paramedicID, query.RegistrationNo == registrationNo, query.Title == title);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paramedicID, System.String registrationNo, System.String title)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID",paramedicID);			parms.Add("RegistrationNo",registrationNo);			parms.Add("Title",title);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "Title": this.str.Title = (string)value; break;							
						case "Result": this.str.Result = (string)value; break;							
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
		/// Maps to ClinicalExamResults.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ClinicalExamResultsMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ClinicalExamResultsMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ClinicalExamResults.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ClinicalExamResultsMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ClinicalExamResultsMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ClinicalExamResults.Title
		/// </summary>
		virtual public System.String Title
		{
			get
			{
				return base.GetSystemString(ClinicalExamResultsMetadata.ColumnNames.Title);
			}
			
			set
			{
				base.SetSystemString(ClinicalExamResultsMetadata.ColumnNames.Title, value);
			}
		}
		
		/// <summary>
		/// Maps to ClinicalExamResults.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(ClinicalExamResultsMetadata.ColumnNames.Result);
			}
			
			set
			{
				base.SetSystemString(ClinicalExamResultsMetadata.ColumnNames.Result, value);
			}
		}
		
		/// <summary>
		/// Maps to ClinicalExamResults.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalExamResultsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ClinicalExamResultsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ClinicalExamResults.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalExamResultsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ClinicalExamResultsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClinicalExamResults entity)
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
				
			public System.String Title
			{
				get
				{
					System.String data = entity.Title;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Title = null;
					else entity.Title = Convert.ToString(value);
				}
			}
				
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
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
			

			private esClinicalExamResults entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClinicalExamResultsQuery query)
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
				throw new Exception("esClinicalExamResults can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ClinicalExamResults : esClinicalExamResults
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
	abstract public class esClinicalExamResultsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ClinicalExamResultsMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ClinicalExamResultsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ClinicalExamResultsMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem Title
		{
			get
			{
				return new esQueryItem(this, ClinicalExamResultsMetadata.ColumnNames.Title, esSystemType.String);
			}
		} 
		
		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, ClinicalExamResultsMetadata.ColumnNames.Result, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalExamResultsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalExamResultsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClinicalExamResultsCollection")]
	public partial class ClinicalExamResultsCollection : esClinicalExamResultsCollection, IEnumerable<ClinicalExamResults>
	{
		public ClinicalExamResultsCollection()
		{

		}
		
		public static implicit operator List<ClinicalExamResults>(ClinicalExamResultsCollection coll)
		{
			List<ClinicalExamResults> list = new List<ClinicalExamResults>();
			
			foreach (ClinicalExamResults emp in coll)
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
				return  ClinicalExamResultsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalExamResultsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClinicalExamResults(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClinicalExamResults();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ClinicalExamResultsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalExamResultsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ClinicalExamResultsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ClinicalExamResults AddNew()
		{
			ClinicalExamResults entity = base.AddNewEntity() as ClinicalExamResults;
			
			return entity;
		}

		public ClinicalExamResults FindByPrimaryKey(System.String paramedicID, System.String registrationNo, System.String title)
		{
			return base.FindByPrimaryKey(paramedicID, registrationNo, title) as ClinicalExamResults;
		}


		#region IEnumerable<ClinicalExamResults> Members

		IEnumerator<ClinicalExamResults> IEnumerable<ClinicalExamResults>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ClinicalExamResults;
			}
		}

		#endregion
		
		private ClinicalExamResultsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClinicalExamResults' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ClinicalExamResults ({RegistrationNo},{ParamedicID},{Title})")]
	[Serializable]
	public partial class ClinicalExamResults : esClinicalExamResults
	{
		public ClinicalExamResults()
		{

		}
	
		public ClinicalExamResults(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClinicalExamResultsMetadata.Meta();
			}
		}
		
		
		
		override protected esClinicalExamResultsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalExamResultsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ClinicalExamResultsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalExamResultsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ClinicalExamResultsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ClinicalExamResultsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ClinicalExamResultsQuery : esClinicalExamResultsQuery
	{
		public ClinicalExamResultsQuery()
		{

		}		
		
		public ClinicalExamResultsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ClinicalExamResultsQuery";
        }
		
			
	}


	[Serializable]
	public partial class ClinicalExamResultsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClinicalExamResultsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClinicalExamResultsMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalExamResultsMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClinicalExamResultsMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalExamResultsMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClinicalExamResultsMetadata.ColumnNames.Title, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalExamResultsMetadata.PropertyNames.Title;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClinicalExamResultsMetadata.ColumnNames.Result, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalExamResultsMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClinicalExamResultsMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalExamResultsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClinicalExamResultsMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalExamResultsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ClinicalExamResultsMetadata Meta()
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
			 public const string ParamedicID = "ParamedicID";
			 public const string Title = "Title";
			 public const string Result = "Result";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ParamedicID = "ParamedicID";
			 public const string Title = "Title";
			 public const string Result = "Result";
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
			lock (typeof(ClinicalExamResultsMetadata))
			{
				if(ClinicalExamResultsMetadata.mapDelegates == null)
				{
					ClinicalExamResultsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ClinicalExamResultsMetadata.meta == null)
				{
					ClinicalExamResultsMetadata.meta = new ClinicalExamResultsMetadata();
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
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Title", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ClinicalExamResults";
				meta.Destination = "ClinicalExamResults";
				
				meta.spInsert = "proc_ClinicalExamResultsInsert";				
				meta.spUpdate = "proc_ClinicalExamResultsUpdate";		
				meta.spDelete = "proc_ClinicalExamResultsDelete";
				meta.spLoadAll = "proc_ClinicalExamResultsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClinicalExamResultsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClinicalExamResultsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
