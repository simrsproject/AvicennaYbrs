/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/7/2014 11:18:06 AM
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
	abstract public class esHealthRecordCollection : esEntityCollectionWAuditLog
	{
		public esHealthRecordCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HealthRecordCollection";
		}

		#region Query Logic
		protected void InitQuery(esHealthRecordQuery query)
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
			this.InitQuery(query as esHealthRecordQuery);
		}
		#endregion
		
		virtual public HealthRecord DetachEntity(HealthRecord entity)
		{
			return base.DetachEntity(entity) as HealthRecord;
		}
		
		virtual public HealthRecord AttachEntity(HealthRecord entity)
		{
			return base.AttachEntity(entity) as HealthRecord;
		}
		
		virtual public void Combine(HealthRecordCollection collection)
		{
			base.Combine(collection);
		}
		
		new public HealthRecord this[int index]
		{
			get
			{
				return base[index] as HealthRecord;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HealthRecord);
		}
	}



	[Serializable]
	abstract public class esHealthRecord : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHealthRecordQuery GetDynamicQuery()
		{
			return null;
		}

		public esHealthRecord()
		{

		}

		public esHealthRecord(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String patientID, System.String questionFormID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, questionFormID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String patientID, System.String questionFormID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, questionFormID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String patientID, System.String questionFormID)
		{
			esHealthRecordQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID, query.QuestionFormID == questionFormID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String patientID, System.String questionFormID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);			parms.Add("QuestionFormID",questionFormID);
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
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;							
						case "RecordDate": this.str.RecordDate = (string)value; break;							
						case "RecordTime": this.str.RecordTime = (string)value; break;							
						case "EmployeeID": this.str.EmployeeID = (string)value; break;							
						case "IsComplete": this.str.IsComplete = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RecordDate":
						
							if (value == null || value is System.DateTime)
								this.RecordDate = (System.DateTime?)value;
							break;
						
						case "IsComplete":
						
							if (value == null || value is System.Boolean)
								this.IsComplete = (System.Boolean?)value;
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
		/// Maps to HealthRecord.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(HealthRecordMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecord.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(HealthRecordMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecord.RecordDate
		/// </summary>
		virtual public System.DateTime? RecordDate
		{
			get
			{
				return base.GetSystemDateTime(HealthRecordMetadata.ColumnNames.RecordDate);
			}
			
			set
			{
				base.SetSystemDateTime(HealthRecordMetadata.ColumnNames.RecordDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecord.RecordTime
		/// </summary>
		virtual public System.String RecordTime
		{
			get
			{
				return base.GetSystemString(HealthRecordMetadata.ColumnNames.RecordTime);
			}
			
			set
			{
				base.SetSystemString(HealthRecordMetadata.ColumnNames.RecordTime, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecord.EmployeeID
		/// </summary>
		virtual public System.String EmployeeID
		{
			get
			{
				return base.GetSystemString(HealthRecordMetadata.ColumnNames.EmployeeID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordMetadata.ColumnNames.EmployeeID, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecord.IsComplete
		/// </summary>
		virtual public System.Boolean? IsComplete
		{
			get
			{
				return base.GetSystemBoolean(HealthRecordMetadata.ColumnNames.IsComplete);
			}
			
			set
			{
				base.SetSystemBoolean(HealthRecordMetadata.ColumnNames.IsComplete, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecord.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HealthRecordMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(HealthRecordMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to HealthRecord.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HealthRecordMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(HealthRecordMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esHealthRecord entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
				
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
				}
			}
				
			public System.String RecordDate
			{
				get
				{
					System.DateTime? data = entity.RecordDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecordDate = null;
					else entity.RecordDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String RecordTime
			{
				get
				{
					System.String data = entity.RecordTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecordTime = null;
					else entity.RecordTime = Convert.ToString(value);
				}
			}
				
			public System.String EmployeeID
			{
				get
				{
					System.String data = entity.EmployeeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeID = null;
					else entity.EmployeeID = Convert.ToString(value);
				}
			}
				
			public System.String IsComplete
			{
				get
				{
					System.Boolean? data = entity.IsComplete;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComplete = null;
					else entity.IsComplete = Convert.ToBoolean(value);
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
			

			private esHealthRecord entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHealthRecordQuery query)
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
				throw new Exception("esHealthRecord can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class HealthRecord : esHealthRecord
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
	abstract public class esHealthRecordQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HealthRecordMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
		
		public esQueryItem RecordDate
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.RecordDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem RecordTime
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.RecordTime, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeID
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.EmployeeID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsComplete
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.IsComplete, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HealthRecordMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HealthRecordCollection")]
	public partial class HealthRecordCollection : esHealthRecordCollection, IEnumerable<HealthRecord>
	{
		public HealthRecordCollection()
		{

		}
		
		public static implicit operator List<HealthRecord>(HealthRecordCollection coll)
		{
			List<HealthRecord> list = new List<HealthRecord>();
			
			foreach (HealthRecord emp in coll)
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
				return  HealthRecordMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HealthRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HealthRecord(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HealthRecord();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HealthRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HealthRecordQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HealthRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public HealthRecord AddNew()
		{
			HealthRecord entity = base.AddNewEntity() as HealthRecord;
			
			return entity;
		}

		public HealthRecord FindByPrimaryKey(System.String patientID, System.String questionFormID)
		{
			return base.FindByPrimaryKey(patientID, questionFormID) as HealthRecord;
		}


		#region IEnumerable<HealthRecord> Members

		IEnumerator<HealthRecord> IEnumerable<HealthRecord>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as HealthRecord;
			}
		}

		#endregion
		
		private HealthRecordQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HealthRecord' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("HealthRecord ({PatientID},{QuestionFormID})")]
	[Serializable]
	public partial class HealthRecord : esHealthRecord
	{
		public HealthRecord()
		{

		}
	
		public HealthRecord(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HealthRecordMetadata.Meta();
			}
		}
		
		
		
		override protected esHealthRecordQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HealthRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HealthRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HealthRecordQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HealthRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HealthRecordQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HealthRecordQuery : esHealthRecordQuery
	{
		public HealthRecordQuery()
		{

		}		
		
		public HealthRecordQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HealthRecordQuery";
        }
		
			
	}


	[Serializable]
	public partial class HealthRecordMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HealthRecordMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.RecordDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HealthRecordMetadata.PropertyNames.RecordDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.RecordTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordMetadata.PropertyNames.RecordTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.EmployeeID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordMetadata.PropertyNames.EmployeeID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.IsComplete, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HealthRecordMetadata.PropertyNames.IsComplete;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HealthRecordMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HealthRecordMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthRecordMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HealthRecordMetadata Meta()
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
			 public const string PatientID = "PatientID";
			 public const string QuestionFormID = "QuestionFormID";
			 public const string RecordDate = "RecordDate";
			 public const string RecordTime = "RecordTime";
			 public const string EmployeeID = "EmployeeID";
			 public const string IsComplete = "IsComplete";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientID = "PatientID";
			 public const string QuestionFormID = "QuestionFormID";
			 public const string RecordDate = "RecordDate";
			 public const string RecordTime = "RecordTime";
			 public const string EmployeeID = "EmployeeID";
			 public const string IsComplete = "IsComplete";
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
			lock (typeof(HealthRecordMetadata))
			{
				if(HealthRecordMetadata.mapDelegates == null)
				{
					HealthRecordMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HealthRecordMetadata.meta == null)
				{
					HealthRecordMetadata.meta = new HealthRecordMetadata();
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
				

				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecordDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RecordTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EmployeeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsComplete", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "HealthRecord";
				meta.Destination = "HealthRecord";
				
				meta.spInsert = "proc_HealthRecordInsert";				
				meta.spUpdate = "proc_HealthRecordUpdate";		
				meta.spDelete = "proc_HealthRecordDelete";
				meta.spLoadAll = "proc_HealthRecordLoadAll";
				meta.spLoadByPrimaryKey = "proc_HealthRecordLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HealthRecordMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
