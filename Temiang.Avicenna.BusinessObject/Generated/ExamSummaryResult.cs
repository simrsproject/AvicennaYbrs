/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/17/2013 3:25:51 PM
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
	abstract public class esExamSummaryResultCollection : esEntityCollectionWAuditLog
	{
		public esExamSummaryResultCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ExamSummaryResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esExamSummaryResultQuery query)
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
			this.InitQuery(query as esExamSummaryResultQuery);
		}
		#endregion
		
		virtual public ExamSummaryResult DetachEntity(ExamSummaryResult entity)
		{
			return base.DetachEntity(entity) as ExamSummaryResult;
		}
		
		virtual public ExamSummaryResult AttachEntity(ExamSummaryResult entity)
		{
			return base.AttachEntity(entity) as ExamSummaryResult;
		}
		
		virtual public void Combine(ExamSummaryResultCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ExamSummaryResult this[int index]
		{
			get
			{
				return base[index] as ExamSummaryResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ExamSummaryResult);
		}
	}



	[Serializable]
	abstract public class esExamSummaryResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esExamSummaryResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esExamSummaryResult()
		{

		}

		public esExamSummaryResult(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String sequenceNo)
		{
			esExamSummaryResultQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "SRExamSummaryType": this.str.SRExamSummaryType = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "OrderIndex": this.str.OrderIndex = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderIndex":
						
							if (value == null || value is System.Int32)
								this.OrderIndex = (System.Int32?)value;
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
		/// Maps to ExamSummaryResult.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ExamSummaryResultMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryResultMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummaryResult.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ExamSummaryResultMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryResultMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummaryResult.SRExamSummaryType
		/// </summary>
		virtual public System.String SRExamSummaryType
		{
			get
			{
				return base.GetSystemString(ExamSummaryResultMetadata.ColumnNames.SRExamSummaryType);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryResultMetadata.ColumnNames.SRExamSummaryType, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummaryResult.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(ExamSummaryResultMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryResultMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummaryResult.OrderIndex
		/// </summary>
		virtual public System.Int32? OrderIndex
		{
			get
			{
				return base.GetSystemInt32(ExamSummaryResultMetadata.ColumnNames.OrderIndex);
			}
			
			set
			{
				base.SetSystemInt32(ExamSummaryResultMetadata.ColumnNames.OrderIndex, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummaryResult.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ExamSummaryResultMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ExamSummaryResultMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ExamSummaryResult.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ExamSummaryResultMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ExamSummaryResultMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esExamSummaryResult entity)
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
				
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
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
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String OrderIndex
			{
				get
				{
					System.Int32? data = entity.OrderIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderIndex = null;
					else entity.OrderIndex = Convert.ToInt32(value);
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
			

			private esExamSummaryResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esExamSummaryResultQuery query)
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
				throw new Exception("esExamSummaryResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ExamSummaryResult : esExamSummaryResult
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
	abstract public class esExamSummaryResultQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ExamSummaryResultMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ExamSummaryResultMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ExamSummaryResultMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRExamSummaryType
		{
			get
			{
				return new esQueryItem(this, ExamSummaryResultMetadata.ColumnNames.SRExamSummaryType, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, ExamSummaryResultMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderIndex
		{
			get
			{
				return new esQueryItem(this, ExamSummaryResultMetadata.ColumnNames.OrderIndex, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ExamSummaryResultMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ExamSummaryResultMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ExamSummaryResultCollection")]
	public partial class ExamSummaryResultCollection : esExamSummaryResultCollection, IEnumerable<ExamSummaryResult>
	{
		public ExamSummaryResultCollection()
		{

		}
		
		public static implicit operator List<ExamSummaryResult>(ExamSummaryResultCollection coll)
		{
			List<ExamSummaryResult> list = new List<ExamSummaryResult>();
			
			foreach (ExamSummaryResult emp in coll)
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
				return  ExamSummaryResultMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ExamSummaryResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ExamSummaryResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ExamSummaryResult();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ExamSummaryResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ExamSummaryResultQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ExamSummaryResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ExamSummaryResult AddNew()
		{
			ExamSummaryResult entity = base.AddNewEntity() as ExamSummaryResult;
			
			return entity;
		}

		public ExamSummaryResult FindByPrimaryKey(System.String registrationNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo) as ExamSummaryResult;
		}


		#region IEnumerable<ExamSummaryResult> Members

		IEnumerator<ExamSummaryResult> IEnumerable<ExamSummaryResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ExamSummaryResult;
			}
		}

		#endregion
		
		private ExamSummaryResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ExamSummaryResult' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ExamSummaryResult ({RegistrationNo},{SequenceNo})")]
	[Serializable]
	public partial class ExamSummaryResult : esExamSummaryResult
	{
		public ExamSummaryResult()
		{

		}
	
		public ExamSummaryResult(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ExamSummaryResultMetadata.Meta();
			}
		}
		
		
		
		override protected esExamSummaryResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ExamSummaryResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ExamSummaryResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ExamSummaryResultQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ExamSummaryResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ExamSummaryResultQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ExamSummaryResultQuery : esExamSummaryResultQuery
	{
		public ExamSummaryResultQuery()
		{

		}		
		
		public ExamSummaryResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ExamSummaryResultQuery";
        }
		
			
	}


	[Serializable]
	public partial class ExamSummaryResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ExamSummaryResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ExamSummaryResultMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryResultMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryResultMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryResultMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryResultMetadata.ColumnNames.SRExamSummaryType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryResultMetadata.PropertyNames.SRExamSummaryType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryResultMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryResultMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryResultMetadata.ColumnNames.OrderIndex, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ExamSummaryResultMetadata.PropertyNames.OrderIndex;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryResultMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ExamSummaryResultMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ExamSummaryResultMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ExamSummaryResultMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ExamSummaryResultMetadata Meta()
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
			 public const string SequenceNo = "SequenceNo";
			 public const string SRExamSummaryType = "SRExamSummaryType";
			 public const string Description = "Description";
			 public const string OrderIndex = "OrderIndex";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string SRExamSummaryType = "SRExamSummaryType";
			 public const string Description = "Description";
			 public const string OrderIndex = "OrderIndex";
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
			lock (typeof(ExamSummaryResultMetadata))
			{
				if(ExamSummaryResultMetadata.mapDelegates == null)
				{
					ExamSummaryResultMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ExamSummaryResultMetadata.meta == null)
				{
					ExamSummaryResultMetadata.meta = new ExamSummaryResultMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRExamSummaryType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderIndex", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ExamSummaryResult";
				meta.Destination = "ExamSummaryResult";
				
				meta.spInsert = "proc_ExamSummaryResultInsert";				
				meta.spUpdate = "proc_ExamSummaryResultUpdate";		
				meta.spDelete = "proc_ExamSummaryResultDelete";
				meta.spLoadAll = "proc_ExamSummaryResultLoadAll";
				meta.spLoadByPrimaryKey = "proc_ExamSummaryResultLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ExamSummaryResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
