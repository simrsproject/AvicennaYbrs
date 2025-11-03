/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:23 PM
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
	abstract public class esPositionWorkExperienceCollection : esEntityCollectionWAuditLog
	{
		public esPositionWorkExperienceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionWorkExperienceCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionWorkExperienceQuery query)
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
			this.InitQuery(query as esPositionWorkExperienceQuery);
		}
		#endregion
		
		virtual public PositionWorkExperience DetachEntity(PositionWorkExperience entity)
		{
			return base.DetachEntity(entity) as PositionWorkExperience;
		}
		
		virtual public PositionWorkExperience AttachEntity(PositionWorkExperience entity)
		{
			return base.AttachEntity(entity) as PositionWorkExperience;
		}
		
		virtual public void Combine(PositionWorkExperienceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionWorkExperience this[int index]
		{
			get
			{
				return base[index] as PositionWorkExperience;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionWorkExperience);
		}
	}



	[Serializable]
	abstract public class esPositionWorkExperience : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionWorkExperienceQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionWorkExperience()
		{

		}

		public esPositionWorkExperience(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionWorkExperienceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionWorkExperienceID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionWorkExperienceID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionWorkExperienceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionWorkExperienceID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionWorkExperienceID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionWorkExperienceID)
		{
			esPositionWorkExperienceQuery query = this.GetDynamicQuery();
			query.Where(query.PositionWorkExperienceID == positionWorkExperienceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionWorkExperienceID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionWorkExperienceID",positionWorkExperienceID);
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
						case "PositionWorkExperienceID": this.str.PositionWorkExperienceID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "SRRequirement": this.str.SRRequirement = (string)value; break;							
						case "SRLineBusiness": this.str.SRLineBusiness = (string)value; break;							
						case "YearExperience": this.str.YearExperience = (string)value; break;							
						case "WorkExperienceNotes": this.str.WorkExperienceNotes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionWorkExperienceID":
						
							if (value == null || value is System.Int32)
								this.PositionWorkExperienceID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						
						case "YearExperience":
						
							if (value == null || value is System.Int32)
								this.YearExperience = (System.Int32?)value;
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
		/// Maps to PositionWorkExperience.PositionWorkExperienceID
		/// </summary>
		virtual public System.Int32? PositionWorkExperienceID
		{
			get
			{
				return base.GetSystemInt32(PositionWorkExperienceMetadata.ColumnNames.PositionWorkExperienceID);
			}
			
			set
			{
				base.SetSystemInt32(PositionWorkExperienceMetadata.ColumnNames.PositionWorkExperienceID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionWorkExperience.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionWorkExperienceMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(PositionWorkExperienceMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionWorkExperience.SRRequirement
		/// </summary>
		virtual public System.String SRRequirement
		{
			get
			{
				return base.GetSystemString(PositionWorkExperienceMetadata.ColumnNames.SRRequirement);
			}
			
			set
			{
				base.SetSystemString(PositionWorkExperienceMetadata.ColumnNames.SRRequirement, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionWorkExperience.SRLineBusiness
		/// </summary>
		virtual public System.String SRLineBusiness
		{
			get
			{
				return base.GetSystemString(PositionWorkExperienceMetadata.ColumnNames.SRLineBusiness);
			}
			
			set
			{
				base.SetSystemString(PositionWorkExperienceMetadata.ColumnNames.SRLineBusiness, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionWorkExperience.YearExperience
		/// </summary>
		virtual public System.Int32? YearExperience
		{
			get
			{
				return base.GetSystemInt32(PositionWorkExperienceMetadata.ColumnNames.YearExperience);
			}
			
			set
			{
				base.SetSystemInt32(PositionWorkExperienceMetadata.ColumnNames.YearExperience, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionWorkExperience.WorkExperienceNotes
		/// </summary>
		virtual public System.String WorkExperienceNotes
		{
			get
			{
				return base.GetSystemString(PositionWorkExperienceMetadata.ColumnNames.WorkExperienceNotes);
			}
			
			set
			{
				base.SetSystemString(PositionWorkExperienceMetadata.ColumnNames.WorkExperienceNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionWorkExperience.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionWorkExperienceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionWorkExperience.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionWorkExperienceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionWorkExperience entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionWorkExperienceID
			{
				get
				{
					System.Int32? data = entity.PositionWorkExperienceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionWorkExperienceID = null;
					else entity.PositionWorkExperienceID = Convert.ToInt32(value);
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
				
			public System.String SRLineBusiness
			{
				get
				{
					System.String data = entity.SRLineBusiness;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLineBusiness = null;
					else entity.SRLineBusiness = Convert.ToString(value);
				}
			}
				
			public System.String YearExperience
			{
				get
				{
					System.Int32? data = entity.YearExperience;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearExperience = null;
					else entity.YearExperience = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkExperienceNotes
			{
				get
				{
					System.String data = entity.WorkExperienceNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkExperienceNotes = null;
					else entity.WorkExperienceNotes = Convert.ToString(value);
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
			

			private esPositionWorkExperience entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionWorkExperienceQuery query)
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
				throw new Exception("esPositionWorkExperience can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionWorkExperience : esPositionWorkExperience
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
	abstract public class esPositionWorkExperienceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionWorkExperienceMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionWorkExperienceID
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.PositionWorkExperienceID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRequirement
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.SRRequirement, esSystemType.String);
			}
		} 
		
		public esQueryItem SRLineBusiness
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.SRLineBusiness, esSystemType.String);
			}
		} 
		
		public esQueryItem YearExperience
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.YearExperience, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkExperienceNotes
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.WorkExperienceNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionWorkExperienceCollection")]
	public partial class PositionWorkExperienceCollection : esPositionWorkExperienceCollection, IEnumerable<PositionWorkExperience>
	{
		public PositionWorkExperienceCollection()
		{

		}
		
		public static implicit operator List<PositionWorkExperience>(PositionWorkExperienceCollection coll)
		{
			List<PositionWorkExperience> list = new List<PositionWorkExperience>();
			
			foreach (PositionWorkExperience emp in coll)
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
				return  PositionWorkExperienceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionWorkExperience(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionWorkExperience();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionWorkExperienceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionWorkExperience AddNew()
		{
			PositionWorkExperience entity = base.AddNewEntity() as PositionWorkExperience;
			
			return entity;
		}

		public PositionWorkExperience FindByPrimaryKey(System.Int32 positionWorkExperienceID)
		{
			return base.FindByPrimaryKey(positionWorkExperienceID) as PositionWorkExperience;
		}


		#region IEnumerable<PositionWorkExperience> Members

		IEnumerator<PositionWorkExperience> IEnumerable<PositionWorkExperience>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionWorkExperience;
			}
		}

		#endregion
		
		private PositionWorkExperienceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionWorkExperience' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionWorkExperience ({PositionWorkExperienceID})")]
	[Serializable]
	public partial class PositionWorkExperience : esPositionWorkExperience
	{
		public PositionWorkExperience()
		{

		}
	
		public PositionWorkExperience(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionWorkExperienceMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionWorkExperienceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionWorkExperienceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionWorkExperienceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionWorkExperienceQuery : esPositionWorkExperienceQuery
	{
		public PositionWorkExperienceQuery()
		{

		}		
		
		public PositionWorkExperienceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionWorkExperienceQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionWorkExperienceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionWorkExperienceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.PositionWorkExperienceID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.PositionWorkExperienceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.SRRequirement, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.SRRequirement;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.SRLineBusiness, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.SRLineBusiness;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.YearExperience, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.YearExperience;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.WorkExperienceNotes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.WorkExperienceNotes;
			c.CharacterMaxLength = 400;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionWorkExperienceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionWorkExperienceMetadata Meta()
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
			 public const string PositionWorkExperienceID = "PositionWorkExperienceID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string SRLineBusiness = "SRLineBusiness";
			 public const string YearExperience = "YearExperience";
			 public const string WorkExperienceNotes = "WorkExperienceNotes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionWorkExperienceID = "PositionWorkExperienceID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string SRLineBusiness = "SRLineBusiness";
			 public const string YearExperience = "YearExperience";
			 public const string WorkExperienceNotes = "WorkExperienceNotes";
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
			lock (typeof(PositionWorkExperienceMetadata))
			{
				if(PositionWorkExperienceMetadata.mapDelegates == null)
				{
					PositionWorkExperienceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionWorkExperienceMetadata.meta == null)
				{
					PositionWorkExperienceMetadata.meta = new PositionWorkExperienceMetadata();
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
				

				meta.AddTypeMap("PositionWorkExperienceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRequirement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLineBusiness", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("YearExperience", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkExperienceNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionWorkExperience";
				meta.Destination = "PositionWorkExperience";
				
				meta.spInsert = "proc_PositionWorkExperienceInsert";				
				meta.spUpdate = "proc_PositionWorkExperienceUpdate";		
				meta.spDelete = "proc_PositionWorkExperienceDelete";
				meta.spLoadAll = "proc_PositionWorkExperienceLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionWorkExperienceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionWorkExperienceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
