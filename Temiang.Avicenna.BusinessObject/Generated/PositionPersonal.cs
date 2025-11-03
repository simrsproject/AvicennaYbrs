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
	abstract public class esPositionPersonalCollection : esEntityCollectionWAuditLog
	{
		public esPositionPersonalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionPersonalCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionPersonalQuery query)
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
			this.InitQuery(query as esPositionPersonalQuery);
		}
		#endregion
		
		virtual public PositionPersonal DetachEntity(PositionPersonal entity)
		{
			return base.DetachEntity(entity) as PositionPersonal;
		}
		
		virtual public PositionPersonal AttachEntity(PositionPersonal entity)
		{
			return base.AttachEntity(entity) as PositionPersonal;
		}
		
		virtual public void Combine(PositionPersonalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionPersonal this[int index]
		{
			get
			{
				return base[index] as PositionPersonal;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionPersonal);
		}
	}



	[Serializable]
	abstract public class esPositionPersonal : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionPersonalQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionPersonal()
		{

		}

		public esPositionPersonal(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionPersonalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionPersonalID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionPersonalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionPersonalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionPersonalID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionPersonalID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionPersonalID)
		{
			esPositionPersonalQuery query = this.GetDynamicQuery();
			query.Where(query.PositionPersonalID == positionPersonalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionPersonalID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionPersonalID",positionPersonalID);
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
						case "PositionPersonalID": this.str.PositionPersonalID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "MinimumAge": this.str.MinimumAge = (string)value; break;							
						case "MaxsimumAge": this.str.MaxsimumAge = (string)value; break;							
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;							
						case "SRGenderType": this.str.SRGenderType = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionPersonalID":
						
							if (value == null || value is System.Int32)
								this.PositionPersonalID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						
						case "MinimumAge":
						
							if (value == null || value is System.Int32)
								this.MinimumAge = (System.Int32?)value;
							break;
						
						case "MaxsimumAge":
						
							if (value == null || value is System.Int32)
								this.MaxsimumAge = (System.Int32?)value;
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
		/// Maps to PositionPersonal.PositionPersonalID
		/// </summary>
		virtual public System.Int32? PositionPersonalID
		{
			get
			{
				return base.GetSystemInt32(PositionPersonalMetadata.ColumnNames.PositionPersonalID);
			}
			
			set
			{
				base.SetSystemInt32(PositionPersonalMetadata.ColumnNames.PositionPersonalID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPersonal.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionPersonalMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(PositionPersonalMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPersonal.MinimumAge
		/// </summary>
		virtual public System.Int32? MinimumAge
		{
			get
			{
				return base.GetSystemInt32(PositionPersonalMetadata.ColumnNames.MinimumAge);
			}
			
			set
			{
				base.SetSystemInt32(PositionPersonalMetadata.ColumnNames.MinimumAge, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPersonal.MaxsimumAge
		/// </summary>
		virtual public System.Int32? MaxsimumAge
		{
			get
			{
				return base.GetSystemInt32(PositionPersonalMetadata.ColumnNames.MaxsimumAge);
			}
			
			set
			{
				base.SetSystemInt32(PositionPersonalMetadata.ColumnNames.MaxsimumAge, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPersonal.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(PositionPersonalMetadata.ColumnNames.SRMaritalStatus);
			}
			
			set
			{
				base.SetSystemString(PositionPersonalMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPersonal.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(PositionPersonalMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(PositionPersonalMetadata.ColumnNames.SRGenderType, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPersonal.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionPersonalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionPersonalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPersonal.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionPersonalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionPersonalMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionPersonal entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionPersonalID
			{
				get
				{
					System.Int32? data = entity.PositionPersonalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionPersonalID = null;
					else entity.PositionPersonalID = Convert.ToInt32(value);
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
				
			public System.String MinimumAge
			{
				get
				{
					System.Int32? data = entity.MinimumAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinimumAge = null;
					else entity.MinimumAge = Convert.ToInt32(value);
				}
			}
				
			public System.String MaxsimumAge
			{
				get
				{
					System.Int32? data = entity.MaxsimumAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxsimumAge = null;
					else entity.MaxsimumAge = Convert.ToInt32(value);
				}
			}
				
			public System.String SRMaritalStatus
			{
				get
				{
					System.String data = entity.SRMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
					else entity.SRMaritalStatus = Convert.ToString(value);
				}
			}
				
			public System.String SRGenderType
			{
				get
				{
					System.String data = entity.SRGenderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGenderType = null;
					else entity.SRGenderType = Convert.ToString(value);
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
			

			private esPositionPersonal entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionPersonalQuery query)
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
				throw new Exception("esPositionPersonal can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionPersonal : esPositionPersonal
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
	abstract public class esPositionPersonalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionPersonalMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionPersonalID
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.PositionPersonalID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MinimumAge
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.MinimumAge, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MaxsimumAge
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.MaxsimumAge, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionPersonalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionPersonalCollection")]
	public partial class PositionPersonalCollection : esPositionPersonalCollection, IEnumerable<PositionPersonal>
	{
		public PositionPersonalCollection()
		{

		}
		
		public static implicit operator List<PositionPersonal>(PositionPersonalCollection coll)
		{
			List<PositionPersonal> list = new List<PositionPersonal>();
			
			foreach (PositionPersonal emp in coll)
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
				return  PositionPersonalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionPersonalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionPersonal(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionPersonal();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionPersonalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionPersonalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionPersonalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionPersonal AddNew()
		{
			PositionPersonal entity = base.AddNewEntity() as PositionPersonal;
			
			return entity;
		}

		public PositionPersonal FindByPrimaryKey(System.Int32 positionPersonalID)
		{
			return base.FindByPrimaryKey(positionPersonalID) as PositionPersonal;
		}


		#region IEnumerable<PositionPersonal> Members

		IEnumerator<PositionPersonal> IEnumerable<PositionPersonal>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionPersonal;
			}
		}

		#endregion
		
		private PositionPersonalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionPersonal' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionPersonal ({PositionPersonalID})")]
	[Serializable]
	public partial class PositionPersonal : esPositionPersonal
	{
		public PositionPersonal()
		{

		}
	
		public PositionPersonal(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionPersonalMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionPersonalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionPersonalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionPersonalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionPersonalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionPersonalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionPersonalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionPersonalQuery : esPositionPersonalQuery
	{
		public PositionPersonalQuery()
		{

		}		
		
		public PositionPersonalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionPersonalQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionPersonalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionPersonalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.PositionPersonalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.PositionPersonalID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.MinimumAge, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.MinimumAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.MaxsimumAge, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.MaxsimumAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.SRMaritalStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.SRGenderType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPersonalMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPersonalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionPersonalMetadata Meta()
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
			 public const string PositionPersonalID = "PositionPersonalID";
			 public const string PositionID = "PositionID";
			 public const string MinimumAge = "MinimumAge";
			 public const string MaxsimumAge = "MaxsimumAge";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string SRGenderType = "SRGenderType";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionPersonalID = "PositionPersonalID";
			 public const string PositionID = "PositionID";
			 public const string MinimumAge = "MinimumAge";
			 public const string MaxsimumAge = "MaxsimumAge";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string SRGenderType = "SRGenderType";
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
			lock (typeof(PositionPersonalMetadata))
			{
				if(PositionPersonalMetadata.mapDelegates == null)
				{
					PositionPersonalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionPersonalMetadata.meta == null)
				{
					PositionPersonalMetadata.meta = new PositionPersonalMetadata();
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
				

				meta.AddTypeMap("PositionPersonalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MinimumAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MaxsimumAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionPersonal";
				meta.Destination = "PositionPersonal";
				
				meta.spInsert = "proc_PositionPersonalInsert";				
				meta.spUpdate = "proc_PositionPersonalUpdate";		
				meta.spDelete = "proc_PositionPersonalDelete";
				meta.spLoadAll = "proc_PositionPersonalLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionPersonalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionPersonalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
