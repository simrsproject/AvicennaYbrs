/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:22 PM
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
	abstract public class esPositionEducationCollection : esEntityCollectionWAuditLog
	{
		public esPositionEducationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionEducationCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionEducationQuery query)
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
			this.InitQuery(query as esPositionEducationQuery);
		}
		#endregion
		
		virtual public PositionEducation DetachEntity(PositionEducation entity)
		{
			return base.DetachEntity(entity) as PositionEducation;
		}
		
		virtual public PositionEducation AttachEntity(PositionEducation entity)
		{
			return base.AttachEntity(entity) as PositionEducation;
		}
		
		virtual public void Combine(PositionEducationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionEducation this[int index]
		{
			get
			{
				return base[index] as PositionEducation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionEducation);
		}
	}



	[Serializable]
	abstract public class esPositionEducation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionEducationQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionEducation()
		{

		}

		public esPositionEducation(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionEducationID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionEducationID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionEducationID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionEducationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionEducationID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionEducationID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionEducationID)
		{
			esPositionEducationQuery query = this.GetDynamicQuery();
			query.Where(query.PositionEducationID == positionEducationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionEducationID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionEducationID",positionEducationID);
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
						case "PositionEducationID": this.str.PositionEducationID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "SRRequirement": this.str.SRRequirement = (string)value; break;							
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;							
						case "SREducationField": this.str.SREducationField = (string)value; break;							
						case "EducationNotes": this.str.EducationNotes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionEducationID":
						
							if (value == null || value is System.Int32)
								this.PositionEducationID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
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
		/// Maps to PositionEducation.PositionEducationID
		/// </summary>
		virtual public System.Int32? PositionEducationID
		{
			get
			{
				return base.GetSystemInt32(PositionEducationMetadata.ColumnNames.PositionEducationID);
			}
			
			set
			{
				base.SetSystemInt32(PositionEducationMetadata.ColumnNames.PositionEducationID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEducation.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionEducationMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(PositionEducationMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEducation.SRRequirement
		/// </summary>
		virtual public System.String SRRequirement
		{
			get
			{
				return base.GetSystemString(PositionEducationMetadata.ColumnNames.SRRequirement);
			}
			
			set
			{
				base.SetSystemString(PositionEducationMetadata.ColumnNames.SRRequirement, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEducation.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(PositionEducationMetadata.ColumnNames.SREducationLevel);
			}
			
			set
			{
				base.SetSystemString(PositionEducationMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEducation.SREducationField
		/// </summary>
		virtual public System.String SREducationField
		{
			get
			{
				return base.GetSystemString(PositionEducationMetadata.ColumnNames.SREducationField);
			}
			
			set
			{
				base.SetSystemString(PositionEducationMetadata.ColumnNames.SREducationField, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEducation.EducationNotes
		/// </summary>
		virtual public System.String EducationNotes
		{
			get
			{
				return base.GetSystemString(PositionEducationMetadata.ColumnNames.EducationNotes);
			}
			
			set
			{
				base.SetSystemString(PositionEducationMetadata.ColumnNames.EducationNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEducation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionEducationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionEducationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEducation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionEducationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionEducationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionEducation entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionEducationID
			{
				get
				{
					System.Int32? data = entity.PositionEducationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionEducationID = null;
					else entity.PositionEducationID = Convert.ToInt32(value);
				}
			}
				
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRRequirement
			{
				get
				{
					System.String data = entity.SRRequirement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRequirement = null;
					else entity.SRRequirement = Convert.ToString(value);
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
				
			public System.String SREducationField
			{
				get
				{
					System.String data = entity.SREducationField;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationField = null;
					else entity.SREducationField = Convert.ToString(value);
				}
			}
				
			public System.String EducationNotes
			{
				get
				{
					System.String data = entity.EducationNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EducationNotes = null;
					else entity.EducationNotes = Convert.ToString(value);
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
			

			private esPositionEducation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionEducationQuery query)
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
				throw new Exception("esPositionEducation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionEducation : esPositionEducation
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
	abstract public class esPositionEducationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionEducationMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionEducationID
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.PositionEducationID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRequirement
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.SRRequirement, esSystemType.String);
			}
		} 
		
		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		} 
		
		public esQueryItem SREducationField
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.SREducationField, esSystemType.String);
			}
		} 
		
		public esQueryItem EducationNotes
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.EducationNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionEducationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionEducationCollection")]
	public partial class PositionEducationCollection : esPositionEducationCollection, IEnumerable<PositionEducation>
	{
		public PositionEducationCollection()
		{

		}
		
		public static implicit operator List<PositionEducation>(PositionEducationCollection coll)
		{
			List<PositionEducation> list = new List<PositionEducation>();
			
			foreach (PositionEducation emp in coll)
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
				return  PositionEducationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionEducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionEducation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionEducation();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionEducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionEducationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionEducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionEducation AddNew()
		{
			PositionEducation entity = base.AddNewEntity() as PositionEducation;
			
			return entity;
		}

		public PositionEducation FindByPrimaryKey(System.Int32 positionEducationID)
		{
			return base.FindByPrimaryKey(positionEducationID) as PositionEducation;
		}


		#region IEnumerable<PositionEducation> Members

		IEnumerator<PositionEducation> IEnumerable<PositionEducation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionEducation;
			}
		}

		#endregion
		
		private PositionEducationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionEducation' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionEducation ({PositionEducationID})")]
	[Serializable]
	public partial class PositionEducation : esPositionEducation
	{
		public PositionEducation()
		{

		}
	
		public PositionEducation(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionEducationMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionEducationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionEducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionEducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionEducationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionEducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionEducationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionEducationQuery : esPositionEducationQuery
	{
		public PositionEducationQuery()
		{

		}		
		
		public PositionEducationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionEducationQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionEducationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionEducationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.PositionEducationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionEducationMetadata.PropertyNames.PositionEducationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionEducationMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.SRRequirement, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEducationMetadata.PropertyNames.SRRequirement;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.SREducationLevel, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEducationMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.SREducationField, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEducationMetadata.PropertyNames.SREducationField;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.EducationNotes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEducationMetadata.PropertyNames.EducationNotes;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionEducationMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEducationMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEducationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionEducationMetadata Meta()
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
			 public const string PositionEducationID = "PositionEducationID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string SREducationField = "SREducationField";
			 public const string EducationNotes = "EducationNotes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionEducationID = "PositionEducationID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string SREducationField = "SREducationField";
			 public const string EducationNotes = "EducationNotes";
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
			lock (typeof(PositionEducationMetadata))
			{
				if(PositionEducationMetadata.mapDelegates == null)
				{
					PositionEducationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionEducationMetadata.meta == null)
				{
					PositionEducationMetadata.meta = new PositionEducationMetadata();
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
				

				meta.AddTypeMap("PositionEducationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRequirement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationField", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EducationNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionEducation";
				meta.Destination = "PositionEducation";
				
				meta.spInsert = "proc_PositionEducationInsert";				
				meta.spUpdate = "proc_PositionEducationUpdate";		
				meta.spDelete = "proc_PositionEducationDelete";
				meta.spLoadAll = "proc_PositionEducationLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionEducationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionEducationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
