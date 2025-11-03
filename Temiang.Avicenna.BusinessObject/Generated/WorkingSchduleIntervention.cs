/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/19/2021 8:59:25 AM
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
	abstract public class esWorkingSchduleInterventionCollection : esEntityCollectionWAuditLog
	{
		public esWorkingSchduleInterventionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WorkingSchduleInterventionCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingSchduleInterventionQuery query)
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
			this.InitQuery(query as esWorkingSchduleInterventionQuery);
		}
		#endregion
		
		virtual public WorkingSchduleIntervention DetachEntity(WorkingSchduleIntervention entity)
		{
			return base.DetachEntity(entity) as WorkingSchduleIntervention;
		}
		
		virtual public WorkingSchduleIntervention AttachEntity(WorkingSchduleIntervention entity)
		{
			return base.AttachEntity(entity) as WorkingSchduleIntervention;
		}
		
		virtual public void Combine(WorkingSchduleInterventionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WorkingSchduleIntervention this[int index]
		{
			get
			{
				return base[index] as WorkingSchduleIntervention;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingSchduleIntervention);
		}
	}



	[Serializable]
	abstract public class esWorkingSchduleIntervention : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingSchduleInterventionQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingSchduleIntervention()
		{

		}

		public esWorkingSchduleIntervention(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 workingSchduleInterventionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingSchduleInterventionID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingSchduleInterventionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 workingSchduleInterventionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingSchduleInterventionID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingSchduleInterventionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 workingSchduleInterventionID)
		{
			esWorkingSchduleInterventionQuery query = this.GetDynamicQuery();
			query.Where(query.WorkingSchduleInterventionID == workingSchduleInterventionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 workingSchduleInterventionID)
		{
			esParameters parms = new esParameters();
			parms.Add("WorkingSchduleInterventionID",workingSchduleInterventionID);
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
						case "WorkingSchduleInterventionID": this.str.WorkingSchduleInterventionID = (string)value; break;							
						case "WorkingScheduleID": this.str.WorkingScheduleID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "WorkingSchduleInterventionID":
						
							if (value == null || value is System.Int32)
								this.WorkingSchduleInterventionID = (System.Int32?)value;
							break;
						
						case "WorkingScheduleID":
						
							if (value == null || value is System.Int32)
								this.WorkingScheduleID = (System.Int32?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to WorkingSchduleIntervention.WorkingSchduleInterventionID
		/// </summary>
		virtual public System.Int32? WorkingSchduleInterventionID
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionMetadata.ColumnNames.WorkingSchduleInterventionID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionMetadata.ColumnNames.WorkingSchduleInterventionID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleIntervention.WorkingScheduleID
		/// </summary>
		virtual public System.Int32? WorkingScheduleID
		{
			get
			{
				return base.GetSystemInt32(WorkingSchduleInterventionMetadata.ColumnNames.WorkingScheduleID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingSchduleInterventionMetadata.ColumnNames.WorkingScheduleID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleIntervention.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(WorkingSchduleInterventionMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(WorkingSchduleInterventionMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleIntervention.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(WorkingSchduleInterventionMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(WorkingSchduleInterventionMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleIntervention.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(WorkingSchduleInterventionMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(WorkingSchduleInterventionMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleIntervention.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchduleIntervention.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateUserID);
			}
			
			set
			{
				base.SetSystemString(WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateUserID, value);
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
			public esStrings(esWorkingSchduleIntervention entity)
			{
				this.entity = entity;
			}
			
	
			public System.String WorkingSchduleInterventionID
			{
				get
				{
					System.Int32? data = entity.WorkingSchduleInterventionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingSchduleInterventionID = null;
					else entity.WorkingSchduleInterventionID = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingScheduleID
			{
				get
				{
					System.Int32? data = entity.WorkingScheduleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingScheduleID = null;
					else entity.WorkingScheduleID = Convert.ToInt32(value);
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
				
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
				
			public System.String LastUpdateUserID
			{
				get
				{
					System.String data = entity.LastUpdateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateUserID = null;
					else entity.LastUpdateUserID = Convert.ToString(value);
				}
			}
			

			private esWorkingSchduleIntervention entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingSchduleInterventionQuery query)
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
				throw new Exception("esWorkingSchduleIntervention can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esWorkingSchduleInterventionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WorkingSchduleInterventionMetadata.Meta();
			}
		}	
		

		public esQueryItem WorkingSchduleInterventionID
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionMetadata.ColumnNames.WorkingSchduleInterventionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingScheduleID
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionMetadata.ColumnNames.WorkingScheduleID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingSchduleInterventionCollection")]
	public partial class WorkingSchduleInterventionCollection : esWorkingSchduleInterventionCollection, IEnumerable<WorkingSchduleIntervention>
	{
		public WorkingSchduleInterventionCollection()
		{

		}
		
		public static implicit operator List<WorkingSchduleIntervention>(WorkingSchduleInterventionCollection coll)
		{
			List<WorkingSchduleIntervention> list = new List<WorkingSchduleIntervention>();
			
			foreach (WorkingSchduleIntervention emp in coll)
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
				return  WorkingSchduleInterventionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingSchduleInterventionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingSchduleIntervention(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingSchduleIntervention();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WorkingSchduleInterventionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingSchduleInterventionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WorkingSchduleInterventionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WorkingSchduleIntervention AddNew()
		{
			WorkingSchduleIntervention entity = base.AddNewEntity() as WorkingSchduleIntervention;
			
			return entity;
		}

		public WorkingSchduleIntervention FindByPrimaryKey(System.Int32 workingSchduleInterventionID)
		{
			return base.FindByPrimaryKey(workingSchduleInterventionID) as WorkingSchduleIntervention;
		}


		#region IEnumerable<WorkingSchduleIntervention> Members

		IEnumerator<WorkingSchduleIntervention> IEnumerable<WorkingSchduleIntervention>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WorkingSchduleIntervention;
			}
		}

		#endregion
		
		private WorkingSchduleInterventionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingSchduleIntervention' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WorkingSchduleIntervention ({WorkingSchduleInterventionID})")]
	[Serializable]
	public partial class WorkingSchduleIntervention : esWorkingSchduleIntervention
	{
		public WorkingSchduleIntervention()
		{

		}
	
		public WorkingSchduleIntervention(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingSchduleInterventionMetadata.Meta();
			}
		}
		
		
		
		override protected esWorkingSchduleInterventionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingSchduleInterventionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WorkingSchduleInterventionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingSchduleInterventionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WorkingSchduleInterventionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WorkingSchduleInterventionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WorkingSchduleInterventionQuery : esWorkingSchduleInterventionQuery
	{
		public WorkingSchduleInterventionQuery()
		{

		}		
		
		public WorkingSchduleInterventionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WorkingSchduleInterventionQuery";
        }
		
			
	}


	[Serializable]
	public partial class WorkingSchduleInterventionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingSchduleInterventionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingSchduleInterventionMetadata.ColumnNames.WorkingSchduleInterventionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionMetadata.PropertyNames.WorkingSchduleInterventionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionMetadata.ColumnNames.WorkingScheduleID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingSchduleInterventionMetadata.PropertyNames.WorkingScheduleID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingSchduleInterventionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionMetadata.ColumnNames.IsApproved, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingSchduleInterventionMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionMetadata.ColumnNames.IsVoid, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingSchduleInterventionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingSchduleInterventionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingSchduleInterventionMetadata.ColumnNames.LastUpdateUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingSchduleInterventionMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WorkingSchduleInterventionMetadata Meta()
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
			 public const string WorkingSchduleInterventionID = "WorkingSchduleInterventionID";
			 public const string WorkingScheduleID = "WorkingScheduleID";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WorkingSchduleInterventionID = "WorkingSchduleInterventionID";
			 public const string WorkingScheduleID = "WorkingScheduleID";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
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
			lock (typeof(WorkingSchduleInterventionMetadata))
			{
				if(WorkingSchduleInterventionMetadata.mapDelegates == null)
				{
					WorkingSchduleInterventionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WorkingSchduleInterventionMetadata.meta == null)
				{
					WorkingSchduleInterventionMetadata.meta = new WorkingSchduleInterventionMetadata();
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
				

				meta.AddTypeMap("WorkingSchduleInterventionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingScheduleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				meta.Source = "WorkingSchduleIntervention";
				meta.Destination = "WorkingSchduleIntervention";
				
				meta.spInsert = "proc_WorkingSchduleInterventionInsert";				
				meta.spUpdate = "proc_WorkingSchduleInterventionUpdate";		
				meta.spDelete = "proc_WorkingSchduleInterventionDelete";
				meta.spLoadAll = "proc_WorkingSchduleInterventionLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingSchduleInterventionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingSchduleInterventionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
