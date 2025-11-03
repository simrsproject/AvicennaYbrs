/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 11:00:41 AM
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
	abstract public class esRL4EducationCollection : esEntityCollectionWAuditLog
	{
		public esRL4EducationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RL4EducationCollection";
		}

		#region Query Logic
		protected void InitQuery(esRL4EducationQuery query)
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
			this.InitQuery(query as esRL4EducationQuery);
		}
		#endregion
		
		virtual public RL4Education DetachEntity(RL4Education entity)
		{
			return base.DetachEntity(entity) as RL4Education;
		}
		
		virtual public RL4Education AttachEntity(RL4Education entity)
		{
			return base.AttachEntity(entity) as RL4Education;
		}
		
		virtual public void Combine(RL4EducationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RL4Education this[int index]
		{
			get
			{
				return base[index] as RL4Education;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RL4Education);
		}
	}



	[Serializable]
	abstract public class esRL4Education : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRL4EducationQuery GetDynamicQuery()
		{
			return null;
		}

		public esRL4Education()
		{

		}

		public esRL4Education(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 rL4EducationID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rL4EducationID);
			else
				return LoadByPrimaryKeyStoredProcedure(rL4EducationID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 rL4EducationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rL4EducationID);
			else
				return LoadByPrimaryKeyStoredProcedure(rL4EducationID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 rL4EducationID)
		{
			esRL4EducationQuery query = this.GetDynamicQuery();
			query.Where(query.RL4EducationID == rL4EducationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 rL4EducationID)
		{
			esParameters parms = new esParameters();
			parms.Add("RL4EducationID",rL4EducationID);
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
						case "RL4EducationID": this.str.RL4EducationID = (string)value; break;							
						case "RL4EducationCode": this.str.RL4EducationCode = (string)value; break;							
						case "RL4EducationName": this.str.RL4EducationName = (string)value; break;							
						case "AcademicTitle": this.str.AcademicTitle = (string)value; break;							
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;							
						case "SRFieldLabor": this.str.SRFieldLabor = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RL4EducationID":
						
							if (value == null || value is System.Int32)
								this.RL4EducationID = (System.Int32?)value;
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
		/// Maps to RL4Education.RL4EducationID
		/// </summary>
		virtual public System.Int32? RL4EducationID
		{
			get
			{
				return base.GetSystemInt32(RL4EducationMetadata.ColumnNames.RL4EducationID);
			}
			
			set
			{
				base.SetSystemInt32(RL4EducationMetadata.ColumnNames.RL4EducationID, value);
			}
		}
		
		/// <summary>
		/// Maps to RL4Education.RL4EducationCode
		/// </summary>
		virtual public System.String RL4EducationCode
		{
			get
			{
				return base.GetSystemString(RL4EducationMetadata.ColumnNames.RL4EducationCode);
			}
			
			set
			{
				base.SetSystemString(RL4EducationMetadata.ColumnNames.RL4EducationCode, value);
			}
		}
		
		/// <summary>
		/// Maps to RL4Education.RL4EducationName
		/// </summary>
		virtual public System.String RL4EducationName
		{
			get
			{
				return base.GetSystemString(RL4EducationMetadata.ColumnNames.RL4EducationName);
			}
			
			set
			{
				base.SetSystemString(RL4EducationMetadata.ColumnNames.RL4EducationName, value);
			}
		}
		
		/// <summary>
		/// Maps to RL4Education.AcademicTitle
		/// </summary>
		virtual public System.String AcademicTitle
		{
			get
			{
				return base.GetSystemString(RL4EducationMetadata.ColumnNames.AcademicTitle);
			}
			
			set
			{
				base.SetSystemString(RL4EducationMetadata.ColumnNames.AcademicTitle, value);
			}
		}
		
		/// <summary>
		/// Maps to RL4Education.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(RL4EducationMetadata.ColumnNames.SREducationLevel);
			}
			
			set
			{
				base.SetSystemString(RL4EducationMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to RL4Education.SRFieldLabor
		/// </summary>
		virtual public System.String SRFieldLabor
		{
			get
			{
				return base.GetSystemString(RL4EducationMetadata.ColumnNames.SRFieldLabor);
			}
			
			set
			{
				base.SetSystemString(RL4EducationMetadata.ColumnNames.SRFieldLabor, value);
			}
		}
		
		/// <summary>
		/// Maps to RL4Education.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RL4EducationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RL4EducationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RL4Education.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RL4EducationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RL4EducationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRL4Education entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RL4EducationID
			{
				get
				{
					System.Int32? data = entity.RL4EducationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RL4EducationID = null;
					else entity.RL4EducationID = Convert.ToInt32(value);
				}
			}
				
			public System.String RL4EducationCode
			{
				get
				{
					System.String data = entity.RL4EducationCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RL4EducationCode = null;
					else entity.RL4EducationCode = Convert.ToString(value);
				}
			}
				
			public System.String RL4EducationName
			{
				get
				{
					System.String data = entity.RL4EducationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RL4EducationName = null;
					else entity.RL4EducationName = Convert.ToString(value);
				}
			}
				
			public System.String AcademicTitle
			{
				get
				{
					System.String data = entity.AcademicTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcademicTitle = null;
					else entity.AcademicTitle = Convert.ToString(value);
				}
			}
				
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
				}
			}
				
			public System.String SRFieldLabor
			{
				get
				{
					System.String data = entity.SRFieldLabor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFieldLabor = null;
					else entity.SRFieldLabor = Convert.ToString(value);
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
			

			private esRL4Education entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRL4EducationQuery query)
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
				throw new Exception("esRL4Education can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RL4Education : esRL4Education
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
	abstract public class esRL4EducationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RL4EducationMetadata.Meta();
			}
		}	
		

		public esQueryItem RL4EducationID
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.RL4EducationID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RL4EducationCode
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.RL4EducationCode, esSystemType.String);
			}
		} 
		
		public esQueryItem RL4EducationName
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.RL4EducationName, esSystemType.String);
			}
		} 
		
		public esQueryItem AcademicTitle
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.AcademicTitle, esSystemType.String);
			}
		} 
		
		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		} 
		
		public esQueryItem SRFieldLabor
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.SRFieldLabor, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RL4EducationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RL4EducationCollection")]
	public partial class RL4EducationCollection : esRL4EducationCollection, IEnumerable<RL4Education>
	{
		public RL4EducationCollection()
		{

		}
		
		public static implicit operator List<RL4Education>(RL4EducationCollection coll)
		{
			List<RL4Education> list = new List<RL4Education>();
			
			foreach (RL4Education emp in coll)
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
				return  RL4EducationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RL4EducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RL4Education(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RL4Education();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RL4EducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RL4EducationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RL4EducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RL4Education AddNew()
		{
			RL4Education entity = base.AddNewEntity() as RL4Education;
			
			return entity;
		}

		public RL4Education FindByPrimaryKey(System.Int32 rL4EducationID)
		{
			return base.FindByPrimaryKey(rL4EducationID) as RL4Education;
		}


		#region IEnumerable<RL4Education> Members

		IEnumerator<RL4Education> IEnumerable<RL4Education>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RL4Education;
			}
		}

		#endregion
		
		private RL4EducationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RL4Education' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RL4Education ({RL4EducationID})")]
	[Serializable]
	public partial class RL4Education : esRL4Education
	{
		public RL4Education()
		{

		}
	
		public RL4Education(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RL4EducationMetadata.Meta();
			}
		}
		
		
		
		override protected esRL4EducationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RL4EducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RL4EducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RL4EducationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RL4EducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RL4EducationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RL4EducationQuery : esRL4EducationQuery
	{
		public RL4EducationQuery()
		{

		}		
		
		public RL4EducationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RL4EducationQuery";
        }
		
			
	}


	[Serializable]
	public partial class RL4EducationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RL4EducationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.RL4EducationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RL4EducationMetadata.PropertyNames.RL4EducationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.RL4EducationCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RL4EducationMetadata.PropertyNames.RL4EducationCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.RL4EducationName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RL4EducationMetadata.PropertyNames.RL4EducationName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.AcademicTitle, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RL4EducationMetadata.PropertyNames.AcademicTitle;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.SREducationLevel, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RL4EducationMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.SRFieldLabor, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RL4EducationMetadata.PropertyNames.SRFieldLabor;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RL4EducationMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(RL4EducationMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RL4EducationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RL4EducationMetadata Meta()
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
			 public const string RL4EducationID = "RL4EducationID";
			 public const string RL4EducationCode = "RL4EducationCode";
			 public const string RL4EducationName = "RL4EducationName";
			 public const string AcademicTitle = "AcademicTitle";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string SRFieldLabor = "SRFieldLabor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RL4EducationID = "RL4EducationID";
			 public const string RL4EducationCode = "RL4EducationCode";
			 public const string RL4EducationName = "RL4EducationName";
			 public const string AcademicTitle = "AcademicTitle";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string SRFieldLabor = "SRFieldLabor";
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
			lock (typeof(RL4EducationMetadata))
			{
				if(RL4EducationMetadata.mapDelegates == null)
				{
					RL4EducationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RL4EducationMetadata.meta == null)
				{
					RL4EducationMetadata.meta = new RL4EducationMetadata();
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
				

				meta.AddTypeMap("RL4EducationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RL4EducationCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("RL4EducationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AcademicTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFieldLabor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RL4Education";
				meta.Destination = "RL4Education";
				
				meta.spInsert = "proc_RL4EducationInsert";				
				meta.spUpdate = "proc_RL4EducationUpdate";		
				meta.spDelete = "proc_RL4EducationDelete";
				meta.spLoadAll = "proc_RL4EducationLoadAll";
				meta.spLoadByPrimaryKey = "proc_RL4EducationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RL4EducationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
