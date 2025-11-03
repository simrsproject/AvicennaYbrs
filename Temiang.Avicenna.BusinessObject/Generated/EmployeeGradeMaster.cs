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
	abstract public class esEmployeeGradeMasterCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeGradeMasterCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeGradeMasterCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeGradeMasterQuery query)
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
			this.InitQuery(query as esEmployeeGradeMasterQuery);
		}
		#endregion
		
		virtual public EmployeeGradeMaster DetachEntity(EmployeeGradeMaster entity)
		{
			return base.DetachEntity(entity) as EmployeeGradeMaster;
		}
		
		virtual public EmployeeGradeMaster AttachEntity(EmployeeGradeMaster entity)
		{
			return base.AttachEntity(entity) as EmployeeGradeMaster;
		}
		
		virtual public void Combine(EmployeeGradeMasterCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeGradeMaster this[int index]
		{
			get
			{
				return base[index] as EmployeeGradeMaster;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeGradeMaster);
		}
	}



	[Serializable]
	abstract public class esEmployeeGradeMaster : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeGradeMasterQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeGradeMaster()
		{

		}

		public esEmployeeGradeMaster(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeGradeMasterID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeGradeMasterID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeGradeMasterID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeGradeMasterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeGradeMasterID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeGradeMasterID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeGradeMasterID)
		{
			esEmployeeGradeMasterQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeGradeMasterID == employeeGradeMasterID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeGradeMasterID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeGradeMasterID",employeeGradeMasterID);
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
						case "EmployeeGradeMasterID": this.str.EmployeeGradeMasterID = (string)value; break;							
						case "EmployeeGradeCode": this.str.EmployeeGradeCode = (string)value; break;							
						case "EmployeeGradeName": this.str.EmployeeGradeName = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "Rangking": this.str.Rangking = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeGradeMasterID":
						
							if (value == null || value is System.Int32)
								this.EmployeeGradeMasterID = (System.Int32?)value;
							break;
						
						case "Rangking":
						
							if (value == null || value is System.Int32)
								this.Rangking = (System.Int32?)value;
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
		/// Maps to EmployeeGradeMaster.EmployeeGradeMasterID
		/// </summary>
		virtual public System.Int32? EmployeeGradeMasterID
		{
			get
			{
				return base.GetSystemInt32(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeMasterID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeMasterID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGradeMaster.EmployeeGradeCode
		/// </summary>
		virtual public System.String EmployeeGradeCode
		{
			get
			{
				return base.GetSystemString(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeCode);
			}
			
			set
			{
				base.SetSystemString(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeCode, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGradeMaster.EmployeeGradeName
		/// </summary>
		virtual public System.String EmployeeGradeName
		{
			get
			{
				return base.GetSystemString(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeName);
			}
			
			set
			{
				base.SetSystemString(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeName, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGradeMaster.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(EmployeeGradeMasterMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(EmployeeGradeMasterMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGradeMaster.Rangking
		/// </summary>
		virtual public System.Int32? Rangking
		{
			get
			{
				return base.GetSystemInt32(EmployeeGradeMasterMetadata.ColumnNames.Rangking);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeGradeMasterMetadata.ColumnNames.Rangking, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGradeMaster.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeGradeMasterMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeGradeMasterMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGradeMaster.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeGradeMasterMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeGradeMasterMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeGradeMaster entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeGradeMasterID
			{
				get
				{
					System.Int32? data = entity.EmployeeGradeMasterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeGradeMasterID = null;
					else entity.EmployeeGradeMasterID = Convert.ToInt32(value);
				}
			}
				
			public System.String EmployeeGradeCode
			{
				get
				{
					System.String data = entity.EmployeeGradeCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeGradeCode = null;
					else entity.EmployeeGradeCode = Convert.ToString(value);
				}
			}
				
			public System.String EmployeeGradeName
			{
				get
				{
					System.String data = entity.EmployeeGradeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeGradeName = null;
					else entity.EmployeeGradeName = Convert.ToString(value);
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
				
			public System.String Rangking
			{
				get
				{
					System.Int32? data = entity.Rangking;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rangking = null;
					else entity.Rangking = Convert.ToInt32(value);
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
			

			private esEmployeeGradeMaster entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeGradeMasterQuery query)
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
				throw new Exception("esEmployeeGradeMaster can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeGradeMaster : esEmployeeGradeMaster
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
	abstract public class esEmployeeGradeMasterQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeGradeMasterMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeGradeMasterID
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeMasterID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EmployeeGradeCode
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeCode, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeGradeName
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeName, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMasterMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem Rangking
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMasterMetadata.ColumnNames.Rangking, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMasterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMasterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeGradeMasterCollection")]
	public partial class EmployeeGradeMasterCollection : esEmployeeGradeMasterCollection, IEnumerable<EmployeeGradeMaster>
	{
		public EmployeeGradeMasterCollection()
		{

		}
		
		public static implicit operator List<EmployeeGradeMaster>(EmployeeGradeMasterCollection coll)
		{
			List<EmployeeGradeMaster> list = new List<EmployeeGradeMaster>();
			
			foreach (EmployeeGradeMaster emp in coll)
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
				return  EmployeeGradeMasterMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeGradeMasterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeGradeMaster(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeGradeMaster();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeGradeMasterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeGradeMasterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeGradeMasterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeGradeMaster AddNew()
		{
			EmployeeGradeMaster entity = base.AddNewEntity() as EmployeeGradeMaster;
			
			return entity;
		}

		public EmployeeGradeMaster FindByPrimaryKey(System.Int32 employeeGradeMasterID)
		{
			return base.FindByPrimaryKey(employeeGradeMasterID) as EmployeeGradeMaster;
		}


		#region IEnumerable<EmployeeGradeMaster> Members

		IEnumerator<EmployeeGradeMaster> IEnumerable<EmployeeGradeMaster>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeGradeMaster;
			}
		}

		#endregion
		
		private EmployeeGradeMasterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeGradeMaster' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeGradeMaster ({EmployeeGradeMasterID})")]
	[Serializable]
	public partial class EmployeeGradeMaster : esEmployeeGradeMaster
	{
		public EmployeeGradeMaster()
		{

		}
	
		public EmployeeGradeMaster(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeGradeMasterMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeGradeMasterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeGradeMasterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeGradeMasterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeGradeMasterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeGradeMasterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeGradeMasterQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeGradeMasterQuery : esEmployeeGradeMasterQuery
	{
		public EmployeeGradeMasterQuery()
		{

		}		
		
		public EmployeeGradeMasterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeGradeMasterQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeGradeMasterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeGradeMasterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeMasterID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeGradeMasterMetadata.PropertyNames.EmployeeGradeMasterID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeGradeMasterMetadata.PropertyNames.EmployeeGradeCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMasterMetadata.ColumnNames.EmployeeGradeName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeGradeMasterMetadata.PropertyNames.EmployeeGradeName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMasterMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeGradeMasterMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMasterMetadata.ColumnNames.Rangking, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeGradeMasterMetadata.PropertyNames.Rangking;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMasterMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeGradeMasterMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMasterMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeGradeMasterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeGradeMasterMetadata Meta()
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
			 public const string EmployeeGradeMasterID = "EmployeeGradeMasterID";
			 public const string EmployeeGradeCode = "EmployeeGradeCode";
			 public const string EmployeeGradeName = "EmployeeGradeName";
			 public const string Description = "Description";
			 public const string Rangking = "Rangking";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeGradeMasterID = "EmployeeGradeMasterID";
			 public const string EmployeeGradeCode = "EmployeeGradeCode";
			 public const string EmployeeGradeName = "EmployeeGradeName";
			 public const string Description = "Description";
			 public const string Rangking = "Rangking";
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
			lock (typeof(EmployeeGradeMasterMetadata))
			{
				if(EmployeeGradeMasterMetadata.mapDelegates == null)
				{
					EmployeeGradeMasterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeGradeMasterMetadata.meta == null)
				{
					EmployeeGradeMasterMetadata.meta = new EmployeeGradeMasterMetadata();
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
				

				meta.AddTypeMap("EmployeeGradeMasterID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeGradeCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EmployeeGradeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Rangking", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeGradeMaster";
				meta.Destination = "EmployeeGradeMaster";
				
				meta.spInsert = "proc_EmployeeGradeMasterInsert";				
				meta.spUpdate = "proc_EmployeeGradeMasterUpdate";		
				meta.spDelete = "proc_EmployeeGradeMasterDelete";
				meta.spLoadAll = "proc_EmployeeGradeMasterLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeGradeMasterLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeGradeMasterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
