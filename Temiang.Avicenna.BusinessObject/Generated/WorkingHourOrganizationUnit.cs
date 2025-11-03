/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/27/2022 1:54:38 PM
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
	abstract public class esWorkingHourOrganizationUnitCollection : esEntityCollectionWAuditLog
	{
		public esWorkingHourOrganizationUnitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WorkingHourOrganizationUnitCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingHourOrganizationUnitQuery query)
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
			this.InitQuery(query as esWorkingHourOrganizationUnitQuery);
		}
		#endregion
		
		virtual public WorkingHourOrganizationUnit DetachEntity(WorkingHourOrganizationUnit entity)
		{
			return base.DetachEntity(entity) as WorkingHourOrganizationUnit;
		}
		
		virtual public WorkingHourOrganizationUnit AttachEntity(WorkingHourOrganizationUnit entity)
		{
			return base.AttachEntity(entity) as WorkingHourOrganizationUnit;
		}
		
		virtual public void Combine(WorkingHourOrganizationUnitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WorkingHourOrganizationUnit this[int index]
		{
			get
			{
				return base[index] as WorkingHourOrganizationUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingHourOrganizationUnit);
		}
	}



	[Serializable]
	abstract public class esWorkingHourOrganizationUnit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingHourOrganizationUnitQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingHourOrganizationUnit()
		{

		}

		public esWorkingHourOrganizationUnit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 organizationUnitID, System.Int32 workingHourID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(organizationUnitID, workingHourID);
			else
				return LoadByPrimaryKeyStoredProcedure(organizationUnitID, workingHourID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 organizationUnitID, System.Int32 workingHourID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(organizationUnitID, workingHourID);
			else
				return LoadByPrimaryKeyStoredProcedure(organizationUnitID, workingHourID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 organizationUnitID, System.Int32 workingHourID)
		{
			esWorkingHourOrganizationUnitQuery query = this.GetDynamicQuery();
			query.Where(query.OrganizationUnitID == organizationUnitID, query.WorkingHourID == workingHourID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 organizationUnitID, System.Int32 workingHourID)
		{
			esParameters parms = new esParameters();
			parms.Add("OrganizationUnitID",organizationUnitID);			parms.Add("WorkingHourID",workingHourID);
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
						case "WorkingHourID": this.str.WorkingHourID = (string)value; break;							
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "WorkingHourID":
						
							if (value == null || value is System.Int32)
								this.WorkingHourID = (System.Int32?)value;
							break;
						
						case "OrganizationUnitID":
						
							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
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
		/// Maps to WorkingHourOrganizationUnit.WorkingHourID
		/// </summary>
		virtual public System.Int32? WorkingHourID
		{
			get
			{
				return base.GetSystemInt32(WorkingHourOrganizationUnitMetadata.ColumnNames.WorkingHourID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingHourOrganizationUnitMetadata.ColumnNames.WorkingHourID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingHourOrganizationUnit.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(WorkingHourOrganizationUnitMetadata.ColumnNames.OrganizationUnitID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingHourOrganizationUnitMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingHourOrganizationUnit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingHourOrganizationUnit.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateUserID);
			}
			
			set
			{
				base.SetSystemString(WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateUserID, value);
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
			public esStrings(esWorkingHourOrganizationUnit entity)
			{
				this.entity = entity;
			}
			
	
			public System.String WorkingHourID
			{
				get
				{
					System.Int32? data = entity.WorkingHourID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourID = null;
					else entity.WorkingHourID = Convert.ToInt32(value);
				}
			}
				
			public System.String OrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.OrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
					else entity.OrganizationUnitID = Convert.ToInt32(value);
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
			

			private esWorkingHourOrganizationUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingHourOrganizationUnitQuery query)
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
				throw new Exception("esWorkingHourOrganizationUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esWorkingHourOrganizationUnitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WorkingHourOrganizationUnitMetadata.Meta();
			}
		}	
		

		public esQueryItem WorkingHourID
		{
			get
			{
				return new esQueryItem(this, WorkingHourOrganizationUnitMetadata.ColumnNames.WorkingHourID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, WorkingHourOrganizationUnitMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingHourOrganizationUnitCollection")]
	public partial class WorkingHourOrganizationUnitCollection : esWorkingHourOrganizationUnitCollection, IEnumerable<WorkingHourOrganizationUnit>
	{
		public WorkingHourOrganizationUnitCollection()
		{

		}
		
		public static implicit operator List<WorkingHourOrganizationUnit>(WorkingHourOrganizationUnitCollection coll)
		{
			List<WorkingHourOrganizationUnit> list = new List<WorkingHourOrganizationUnit>();
			
			foreach (WorkingHourOrganizationUnit emp in coll)
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
				return  WorkingHourOrganizationUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingHourOrganizationUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingHourOrganizationUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingHourOrganizationUnit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WorkingHourOrganizationUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingHourOrganizationUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WorkingHourOrganizationUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WorkingHourOrganizationUnit AddNew()
		{
			WorkingHourOrganizationUnit entity = base.AddNewEntity() as WorkingHourOrganizationUnit;
			
			return entity;
		}

		public WorkingHourOrganizationUnit FindByPrimaryKey(System.Int32 organizationUnitID, System.Int32 workingHourID)
		{
			return base.FindByPrimaryKey(organizationUnitID, workingHourID) as WorkingHourOrganizationUnit;
		}


		#region IEnumerable<WorkingHourOrganizationUnit> Members

		IEnumerator<WorkingHourOrganizationUnit> IEnumerable<WorkingHourOrganizationUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WorkingHourOrganizationUnit;
			}
		}

		#endregion
		
		private WorkingHourOrganizationUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingHourOrganizationUnit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WorkingHourOrganizationUnit ({WorkingHourID},{OrganizationUnitID})")]
	[Serializable]
	public partial class WorkingHourOrganizationUnit : esWorkingHourOrganizationUnit
	{
		public WorkingHourOrganizationUnit()
		{

		}
	
		public WorkingHourOrganizationUnit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingHourOrganizationUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esWorkingHourOrganizationUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingHourOrganizationUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WorkingHourOrganizationUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingHourOrganizationUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WorkingHourOrganizationUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WorkingHourOrganizationUnitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WorkingHourOrganizationUnitQuery : esWorkingHourOrganizationUnitQuery
	{
		public WorkingHourOrganizationUnitQuery()
		{

		}		
		
		public WorkingHourOrganizationUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WorkingHourOrganizationUnitQuery";
        }
		
			
	}


	[Serializable]
	public partial class WorkingHourOrganizationUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingHourOrganizationUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingHourOrganizationUnitMetadata.ColumnNames.WorkingHourID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingHourOrganizationUnitMetadata.PropertyNames.WorkingHourID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingHourOrganizationUnitMetadata.ColumnNames.OrganizationUnitID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingHourOrganizationUnitMetadata.PropertyNames.OrganizationUnitID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingHourOrganizationUnitMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingHourOrganizationUnitMetadata.ColumnNames.LastUpdateUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourOrganizationUnitMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WorkingHourOrganizationUnitMetadata Meta()
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
			 public const string WorkingHourID = "WorkingHourID";
			 public const string OrganizationUnitID = "OrganizationUnitID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WorkingHourID = "WorkingHourID";
			 public const string OrganizationUnitID = "OrganizationUnitID";
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
			lock (typeof(WorkingHourOrganizationUnitMetadata))
			{
				if(WorkingHourOrganizationUnitMetadata.mapDelegates == null)
				{
					WorkingHourOrganizationUnitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WorkingHourOrganizationUnitMetadata.meta == null)
				{
					WorkingHourOrganizationUnitMetadata.meta = new WorkingHourOrganizationUnitMetadata();
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
				

				meta.AddTypeMap("WorkingHourID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "WorkingHourOrganizationUnit";
				meta.Destination = "WorkingHourOrganizationUnit";
				
				meta.spInsert = "proc_WorkingHourOrganizationUnitInsert";				
				meta.spUpdate = "proc_WorkingHourOrganizationUnitUpdate";		
				meta.spDelete = "proc_WorkingHourOrganizationUnitDelete";
				meta.spLoadAll = "proc_WorkingHourOrganizationUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingHourOrganizationUnitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingHourOrganizationUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
