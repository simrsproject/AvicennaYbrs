/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
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
	abstract public class esStandardSalaryCollection : esEntityCollectionWAuditLog
	{
		public esStandardSalaryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "StandardSalaryCollection";
		}

		#region Query Logic
		protected void InitQuery(esStandardSalaryQuery query)
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
			this.InitQuery(query as esStandardSalaryQuery);
		}
		#endregion
		
		virtual public StandardSalary DetachEntity(StandardSalary entity)
		{
			return base.DetachEntity(entity) as StandardSalary;
		}
		
		virtual public StandardSalary AttachEntity(StandardSalary entity)
		{
			return base.AttachEntity(entity) as StandardSalary;
		}
		
		virtual public void Combine(StandardSalaryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public StandardSalary this[int index]
		{
			get
			{
				return base[index] as StandardSalary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(StandardSalary);
		}
	}



	[Serializable]
	abstract public class esStandardSalary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esStandardSalaryQuery GetDynamicQuery()
		{
			return null;
		}

		public esStandardSalary()
		{

		}

		public esStandardSalary(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 standardSalaryID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardSalaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardSalaryID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 standardSalaryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardSalaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardSalaryID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 standardSalaryID)
		{
			esStandardSalaryQuery query = this.GetDynamicQuery();
			query.Where(query.StandardSalaryID == standardSalaryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 standardSalaryID)
		{
			esParameters parms = new esParameters();
			parms.Add("StandardSalaryID",standardSalaryID);
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
						case "StandardSalaryID": this.str.StandardSalaryID = (string)value; break;							
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StandardSalaryID":
						
							if (value == null || value is System.Int32)
								this.StandardSalaryID = (System.Int32?)value;
							break;
						
						case "PositionGradeID":
						
							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to StandardSalary.StandardSalaryID
		/// </summary>
		virtual public System.Int32? StandardSalaryID
		{
			get
			{
				return base.GetSystemInt32(StandardSalaryMetadata.ColumnNames.StandardSalaryID);
			}
			
			set
			{
				base.SetSystemInt32(StandardSalaryMetadata.ColumnNames.StandardSalaryID, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalary.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(StandardSalaryMetadata.ColumnNames.PositionGradeID);
			}
			
			set
			{
				base.SetSystemInt32(StandardSalaryMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalary.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(StandardSalaryMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(StandardSalaryMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalary.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(StandardSalaryMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(StandardSalaryMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalary.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(StandardSalaryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(StandardSalaryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalary.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(StandardSalaryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(StandardSalaryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esStandardSalary entity)
			{
				this.entity = entity;
			}
			
	
			public System.String StandardSalaryID
			{
				get
				{
					System.Int32? data = entity.StandardSalaryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardSalaryID = null;
					else entity.StandardSalaryID = Convert.ToInt32(value);
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
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
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
			

			private esStandardSalary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esStandardSalaryQuery query)
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
				throw new Exception("esStandardSalary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class StandardSalary : esStandardSalary
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
	abstract public class esStandardSalaryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StandardSalaryMetadata.Meta();
			}
		}	
		

		public esQueryItem StandardSalaryID
		{
			get
			{
				return new esQueryItem(this, StandardSalaryMetadata.ColumnNames.StandardSalaryID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, StandardSalaryMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, StandardSalaryMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, StandardSalaryMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, StandardSalaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, StandardSalaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("StandardSalaryCollection")]
	public partial class StandardSalaryCollection : esStandardSalaryCollection, IEnumerable<StandardSalary>
	{
		public StandardSalaryCollection()
		{

		}
		
		public static implicit operator List<StandardSalary>(StandardSalaryCollection coll)
		{
			List<StandardSalary> list = new List<StandardSalary>();
			
			foreach (StandardSalary emp in coll)
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
				return  StandardSalaryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StandardSalaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new StandardSalary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new StandardSalary();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public StandardSalaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StandardSalaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(StandardSalaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public StandardSalary AddNew()
		{
			StandardSalary entity = base.AddNewEntity() as StandardSalary;
			
			return entity;
		}

		public StandardSalary FindByPrimaryKey(System.Int32 standardSalaryID)
		{
			return base.FindByPrimaryKey(standardSalaryID) as StandardSalary;
		}


		#region IEnumerable<StandardSalary> Members

		IEnumerator<StandardSalary> IEnumerable<StandardSalary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as StandardSalary;
			}
		}

		#endregion
		
		private StandardSalaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'StandardSalary' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("StandardSalary ({StandardSalaryID})")]
	[Serializable]
	public partial class StandardSalary : esStandardSalary
	{
		public StandardSalary()
		{

		}
	
		public StandardSalary(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StandardSalaryMetadata.Meta();
			}
		}
		
		
		
		override protected esStandardSalaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StandardSalaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public StandardSalaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StandardSalaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(StandardSalaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private StandardSalaryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class StandardSalaryQuery : esStandardSalaryQuery
	{
		public StandardSalaryQuery()
		{

		}		
		
		public StandardSalaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "StandardSalaryQuery";
        }
		
			
	}


	[Serializable]
	public partial class StandardSalaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StandardSalaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StandardSalaryMetadata.ColumnNames.StandardSalaryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StandardSalaryMetadata.PropertyNames.StandardSalaryID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryMetadata.ColumnNames.PositionGradeID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StandardSalaryMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryMetadata.ColumnNames.ValidFrom, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = StandardSalaryMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryMetadata.ColumnNames.ValidTo, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = StandardSalaryMetadata.PropertyNames.ValidTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = StandardSalaryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = StandardSalaryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public StandardSalaryMetadata Meta()
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
			 public const string StandardSalaryID = "StandardSalaryID";
			 public const string PositionGradeID = "PositionGradeID";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StandardSalaryID = "StandardSalaryID";
			 public const string PositionGradeID = "PositionGradeID";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
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
			lock (typeof(StandardSalaryMetadata))
			{
				if(StandardSalaryMetadata.mapDelegates == null)
				{
					StandardSalaryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StandardSalaryMetadata.meta == null)
				{
					StandardSalaryMetadata.meta = new StandardSalaryMetadata();
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
				

				meta.AddTypeMap("StandardSalaryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "StandardSalary";
				meta.Destination = "StandardSalary";
				
				meta.spInsert = "proc_StandardSalaryInsert";				
				meta.spUpdate = "proc_StandardSalaryUpdate";		
				meta.spDelete = "proc_StandardSalaryDelete";
				meta.spLoadAll = "proc_StandardSalaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_StandardSalaryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StandardSalaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
