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
	abstract public class esPositionEmploymentCompanyCollection : esEntityCollectionWAuditLog
	{
		public esPositionEmploymentCompanyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionEmploymentCompanyCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionEmploymentCompanyQuery query)
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
			this.InitQuery(query as esPositionEmploymentCompanyQuery);
		}
		#endregion
		
		virtual public PositionEmploymentCompany DetachEntity(PositionEmploymentCompany entity)
		{
			return base.DetachEntity(entity) as PositionEmploymentCompany;
		}
		
		virtual public PositionEmploymentCompany AttachEntity(PositionEmploymentCompany entity)
		{
			return base.AttachEntity(entity) as PositionEmploymentCompany;
		}
		
		virtual public void Combine(PositionEmploymentCompanyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionEmploymentCompany this[int index]
		{
			get
			{
				return base[index] as PositionEmploymentCompany;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionEmploymentCompany);
		}
	}



	[Serializable]
	abstract public class esPositionEmploymentCompany : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionEmploymentCompanyQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionEmploymentCompany()
		{

		}

		public esPositionEmploymentCompany(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionEmploymentCompanyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionEmploymentCompanyID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionEmploymentCompanyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionEmploymentCompanyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionEmploymentCompanyID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionEmploymentCompanyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionEmploymentCompanyID)
		{
			esPositionEmploymentCompanyQuery query = this.GetDynamicQuery();
			query.Where(query.PositionEmploymentCompanyID == positionEmploymentCompanyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionEmploymentCompanyID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionEmploymentCompanyID",positionEmploymentCompanyID);
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
						case "PositionEmploymentCompanyID": this.str.PositionEmploymentCompanyID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "SRRequirement": this.str.SRRequirement = (string)value; break;							
						case "YearOfService": this.str.YearOfService = (string)value; break;							
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionEmploymentCompanyID":
						
							if (value == null || value is System.Int32)
								this.PositionEmploymentCompanyID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						
						case "YearOfService":
						
							if (value == null || value is System.Int32)
								this.YearOfService = (System.Int32?)value;
							break;
						
						case "PositionGradeID":
						
							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
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
		/// Maps to PositionEmploymentCompany.PositionEmploymentCompanyID
		/// </summary>
		virtual public System.Int32? PositionEmploymentCompanyID
		{
			get
			{
				return base.GetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.PositionEmploymentCompanyID);
			}
			
			set
			{
				base.SetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.PositionEmploymentCompanyID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEmploymentCompany.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEmploymentCompany.SRRequirement
		/// </summary>
		virtual public System.String SRRequirement
		{
			get
			{
				return base.GetSystemString(PositionEmploymentCompanyMetadata.ColumnNames.SRRequirement);
			}
			
			set
			{
				base.SetSystemString(PositionEmploymentCompanyMetadata.ColumnNames.SRRequirement, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEmploymentCompany.YearOfService
		/// </summary>
		virtual public System.Int32? YearOfService
		{
			get
			{
				return base.GetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.YearOfService);
			}
			
			set
			{
				base.SetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.YearOfService, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEmploymentCompany.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.PositionGradeID);
			}
			
			set
			{
				base.SetSystemInt32(PositionEmploymentCompanyMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEmploymentCompany.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PositionEmploymentCompanyMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(PositionEmploymentCompanyMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEmploymentCompany.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionEmploymentCompany.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionEmploymentCompany entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionEmploymentCompanyID
			{
				get
				{
					System.Int32? data = entity.PositionEmploymentCompanyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionEmploymentCompanyID = null;
					else entity.PositionEmploymentCompanyID = Convert.ToInt32(value);
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
				
			public System.String YearOfService
			{
				get
				{
					System.Int32? data = entity.YearOfService;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearOfService = null;
					else entity.YearOfService = Convert.ToInt32(value);
				}
			}
				
			public System.String PositionGradeID
			{
				get
				{
					System.Int32? data = entity.PositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeID = null;
					else entity.PositionGradeID = Convert.ToInt32(value);
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
			

			private esPositionEmploymentCompany entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionEmploymentCompanyQuery query)
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
				throw new Exception("esPositionEmploymentCompany can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionEmploymentCompany : esPositionEmploymentCompany
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
	abstract public class esPositionEmploymentCompanyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionEmploymentCompanyMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionEmploymentCompanyID
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.PositionEmploymentCompanyID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRequirement
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.SRRequirement, esSystemType.String);
			}
		} 
		
		public esQueryItem YearOfService
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.YearOfService, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionEmploymentCompanyCollection")]
	public partial class PositionEmploymentCompanyCollection : esPositionEmploymentCompanyCollection, IEnumerable<PositionEmploymentCompany>
	{
		public PositionEmploymentCompanyCollection()
		{

		}
		
		public static implicit operator List<PositionEmploymentCompany>(PositionEmploymentCompanyCollection coll)
		{
			List<PositionEmploymentCompany> list = new List<PositionEmploymentCompany>();
			
			foreach (PositionEmploymentCompany emp in coll)
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
				return  PositionEmploymentCompanyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionEmploymentCompanyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionEmploymentCompany(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionEmploymentCompany();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionEmploymentCompanyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionEmploymentCompanyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionEmploymentCompanyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionEmploymentCompany AddNew()
		{
			PositionEmploymentCompany entity = base.AddNewEntity() as PositionEmploymentCompany;
			
			return entity;
		}

		public PositionEmploymentCompany FindByPrimaryKey(System.Int32 positionEmploymentCompanyID)
		{
			return base.FindByPrimaryKey(positionEmploymentCompanyID) as PositionEmploymentCompany;
		}


		#region IEnumerable<PositionEmploymentCompany> Members

		IEnumerator<PositionEmploymentCompany> IEnumerable<PositionEmploymentCompany>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionEmploymentCompany;
			}
		}

		#endregion
		
		private PositionEmploymentCompanyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionEmploymentCompany' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionEmploymentCompany ({PositionEmploymentCompanyID})")]
	[Serializable]
	public partial class PositionEmploymentCompany : esPositionEmploymentCompany
	{
		public PositionEmploymentCompany()
		{

		}
	
		public PositionEmploymentCompany(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionEmploymentCompanyMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionEmploymentCompanyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionEmploymentCompanyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionEmploymentCompanyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionEmploymentCompanyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionEmploymentCompanyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionEmploymentCompanyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionEmploymentCompanyQuery : esPositionEmploymentCompanyQuery
	{
		public PositionEmploymentCompanyQuery()
		{

		}		
		
		public PositionEmploymentCompanyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionEmploymentCompanyQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionEmploymentCompanyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionEmploymentCompanyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.PositionEmploymentCompanyID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.PositionEmploymentCompanyID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.SRRequirement, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.SRRequirement;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.YearOfService, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.YearOfService;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.PositionGradeID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionEmploymentCompanyMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionEmploymentCompanyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionEmploymentCompanyMetadata Meta()
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
			 public const string PositionEmploymentCompanyID = "PositionEmploymentCompanyID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string YearOfService = "YearOfService";
			 public const string PositionGradeID = "PositionGradeID";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionEmploymentCompanyID = "PositionEmploymentCompanyID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string YearOfService = "YearOfService";
			 public const string PositionGradeID = "PositionGradeID";
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
			lock (typeof(PositionEmploymentCompanyMetadata))
			{
				if(PositionEmploymentCompanyMetadata.mapDelegates == null)
				{
					PositionEmploymentCompanyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionEmploymentCompanyMetadata.meta == null)
				{
					PositionEmploymentCompanyMetadata.meta = new PositionEmploymentCompanyMetadata();
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
				

				meta.AddTypeMap("PositionEmploymentCompanyID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRequirement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("YearOfService", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionEmploymentCompany";
				meta.Destination = "PositionEmploymentCompany";
				
				meta.spInsert = "proc_PositionEmploymentCompanyInsert";				
				meta.spUpdate = "proc_PositionEmploymentCompanyUpdate";		
				meta.spDelete = "proc_PositionEmploymentCompanyDelete";
				meta.spLoadAll = "proc_PositionEmploymentCompanyLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionEmploymentCompanyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionEmploymentCompanyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
