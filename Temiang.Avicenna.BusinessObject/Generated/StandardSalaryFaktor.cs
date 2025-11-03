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
	abstract public class esStandardSalaryFaktorCollection : esEntityCollectionWAuditLog
	{
		public esStandardSalaryFaktorCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "StandardSalaryFaktorCollection";
		}

		#region Query Logic
		protected void InitQuery(esStandardSalaryFaktorQuery query)
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
			this.InitQuery(query as esStandardSalaryFaktorQuery);
		}
		#endregion
		
		virtual public StandardSalaryFaktor DetachEntity(StandardSalaryFaktor entity)
		{
			return base.DetachEntity(entity) as StandardSalaryFaktor;
		}
		
		virtual public StandardSalaryFaktor AttachEntity(StandardSalaryFaktor entity)
		{
			return base.AttachEntity(entity) as StandardSalaryFaktor;
		}
		
		virtual public void Combine(StandardSalaryFaktorCollection collection)
		{
			base.Combine(collection);
		}
		
		new public StandardSalaryFaktor this[int index]
		{
			get
			{
				return base[index] as StandardSalaryFaktor;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(StandardSalaryFaktor);
		}
	}



	[Serializable]
	abstract public class esStandardSalaryFaktor : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esStandardSalaryFaktorQuery GetDynamicQuery()
		{
			return null;
		}

		public esStandardSalaryFaktor()
		{

		}

		public esStandardSalaryFaktor(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 standardSalaryFaktorID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardSalaryFaktorID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardSalaryFaktorID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 standardSalaryFaktorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardSalaryFaktorID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardSalaryFaktorID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 standardSalaryFaktorID)
		{
			esStandardSalaryFaktorQuery query = this.GetDynamicQuery();
			query.Where(query.StandardSalaryFaktorID == standardSalaryFaktorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 standardSalaryFaktorID)
		{
			esParameters parms = new esParameters();
			parms.Add("StandardSalaryFaktorID",standardSalaryFaktorID);
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
						case "StandardSalaryFaktorID": this.str.StandardSalaryFaktorID = (string)value; break;							
						case "StandardSalaryID": this.str.StandardSalaryID = (string)value; break;							
						case "GradeServiceYear": this.str.GradeServiceYear = (string)value; break;							
						case "AmountSalary": this.str.AmountSalary = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StandardSalaryFaktorID":
						
							if (value == null || value is System.Int32)
								this.StandardSalaryFaktorID = (System.Int32?)value;
							break;
						
						case "StandardSalaryID":
						
							if (value == null || value is System.Int32)
								this.StandardSalaryID = (System.Int32?)value;
							break;
						
						case "GradeServiceYear":
						
							if (value == null || value is System.Int32)
								this.GradeServiceYear = (System.Int32?)value;
							break;
						
						case "AmountSalary":
						
							if (value == null || value is System.Decimal)
								this.AmountSalary = (System.Decimal?)value;
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
		/// Maps to StandardSalaryFaktor.StandardSalaryFaktorID
		/// </summary>
		virtual public System.Int32? StandardSalaryFaktorID
		{
			get
			{
				return base.GetSystemInt32(StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryFaktorID);
			}
			
			set
			{
				base.SetSystemInt32(StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryFaktorID, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalaryFaktor.StandardSalaryID
		/// </summary>
		virtual public System.Int32? StandardSalaryID
		{
			get
			{
				return base.GetSystemInt32(StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryID);
			}
			
			set
			{
				base.SetSystemInt32(StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryID, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalaryFaktor.GradeServiceYear
		/// </summary>
		virtual public System.Int32? GradeServiceYear
		{
			get
			{
				return base.GetSystemInt32(StandardSalaryFaktorMetadata.ColumnNames.GradeServiceYear);
			}
			
			set
			{
				base.SetSystemInt32(StandardSalaryFaktorMetadata.ColumnNames.GradeServiceYear, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalaryFaktor.AmountSalary
		/// </summary>
		virtual public System.Decimal? AmountSalary
		{
			get
			{
				return base.GetSystemDecimal(StandardSalaryFaktorMetadata.ColumnNames.AmountSalary);
			}
			
			set
			{
				base.SetSystemDecimal(StandardSalaryFaktorMetadata.ColumnNames.AmountSalary, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalaryFaktor.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(StandardSalaryFaktorMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(StandardSalaryFaktorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to StandardSalaryFaktor.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(StandardSalaryFaktorMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(StandardSalaryFaktorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esStandardSalaryFaktor entity)
			{
				this.entity = entity;
			}
			
	
			public System.String StandardSalaryFaktorID
			{
				get
				{
					System.Int32? data = entity.StandardSalaryFaktorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardSalaryFaktorID = null;
					else entity.StandardSalaryFaktorID = Convert.ToInt32(value);
				}
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
				
			public System.String GradeServiceYear
			{
				get
				{
					System.Int32? data = entity.GradeServiceYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GradeServiceYear = null;
					else entity.GradeServiceYear = Convert.ToInt32(value);
				}
			}
				
			public System.String AmountSalary
			{
				get
				{
					System.Decimal? data = entity.AmountSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountSalary = null;
					else entity.AmountSalary = Convert.ToDecimal(value);
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
			

			private esStandardSalaryFaktor entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esStandardSalaryFaktorQuery query)
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
				throw new Exception("esStandardSalaryFaktor can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class StandardSalaryFaktor : esStandardSalaryFaktor
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
	abstract public class esStandardSalaryFaktorQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StandardSalaryFaktorMetadata.Meta();
			}
		}	
		

		public esQueryItem StandardSalaryFaktorID
		{
			get
			{
				return new esQueryItem(this, StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryFaktorID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem StandardSalaryID
		{
			get
			{
				return new esQueryItem(this, StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem GradeServiceYear
		{
			get
			{
				return new esQueryItem(this, StandardSalaryFaktorMetadata.ColumnNames.GradeServiceYear, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AmountSalary
		{
			get
			{
				return new esQueryItem(this, StandardSalaryFaktorMetadata.ColumnNames.AmountSalary, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, StandardSalaryFaktorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, StandardSalaryFaktorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("StandardSalaryFaktorCollection")]
	public partial class StandardSalaryFaktorCollection : esStandardSalaryFaktorCollection, IEnumerable<StandardSalaryFaktor>
	{
		public StandardSalaryFaktorCollection()
		{

		}
		
		public static implicit operator List<StandardSalaryFaktor>(StandardSalaryFaktorCollection coll)
		{
			List<StandardSalaryFaktor> list = new List<StandardSalaryFaktor>();
			
			foreach (StandardSalaryFaktor emp in coll)
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
				return  StandardSalaryFaktorMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StandardSalaryFaktorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new StandardSalaryFaktor(row);
		}

		override protected esEntity CreateEntity()
		{
			return new StandardSalaryFaktor();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public StandardSalaryFaktorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StandardSalaryFaktorQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(StandardSalaryFaktorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public StandardSalaryFaktor AddNew()
		{
			StandardSalaryFaktor entity = base.AddNewEntity() as StandardSalaryFaktor;
			
			return entity;
		}

		public StandardSalaryFaktor FindByPrimaryKey(System.Int32 standardSalaryFaktorID)
		{
			return base.FindByPrimaryKey(standardSalaryFaktorID) as StandardSalaryFaktor;
		}


		#region IEnumerable<StandardSalaryFaktor> Members

		IEnumerator<StandardSalaryFaktor> IEnumerable<StandardSalaryFaktor>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as StandardSalaryFaktor;
			}
		}

		#endregion
		
		private StandardSalaryFaktorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'StandardSalaryFaktor' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("StandardSalaryFaktor ({StandardSalaryFaktorID})")]
	[Serializable]
	public partial class StandardSalaryFaktor : esStandardSalaryFaktor
	{
		public StandardSalaryFaktor()
		{

		}
	
		public StandardSalaryFaktor(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StandardSalaryFaktorMetadata.Meta();
			}
		}
		
		
		
		override protected esStandardSalaryFaktorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StandardSalaryFaktorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public StandardSalaryFaktorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StandardSalaryFaktorQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(StandardSalaryFaktorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private StandardSalaryFaktorQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class StandardSalaryFaktorQuery : esStandardSalaryFaktorQuery
	{
		public StandardSalaryFaktorQuery()
		{

		}		
		
		public StandardSalaryFaktorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "StandardSalaryFaktorQuery";
        }
		
			
	}


	[Serializable]
	public partial class StandardSalaryFaktorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StandardSalaryFaktorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryFaktorID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StandardSalaryFaktorMetadata.PropertyNames.StandardSalaryFaktorID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StandardSalaryFaktorMetadata.PropertyNames.StandardSalaryID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryFaktorMetadata.ColumnNames.GradeServiceYear, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StandardSalaryFaktorMetadata.PropertyNames.GradeServiceYear;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryFaktorMetadata.ColumnNames.AmountSalary, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = StandardSalaryFaktorMetadata.PropertyNames.AmountSalary;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryFaktorMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = StandardSalaryFaktorMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandardSalaryFaktorMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = StandardSalaryFaktorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public StandardSalaryFaktorMetadata Meta()
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
			 public const string StandardSalaryFaktorID = "StandardSalaryFaktorID";
			 public const string StandardSalaryID = "StandardSalaryID";
			 public const string GradeServiceYear = "GradeServiceYear";
			 public const string AmountSalary = "AmountSalary";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StandardSalaryFaktorID = "StandardSalaryFaktorID";
			 public const string StandardSalaryID = "StandardSalaryID";
			 public const string GradeServiceYear = "GradeServiceYear";
			 public const string AmountSalary = "AmountSalary";
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
			lock (typeof(StandardSalaryFaktorMetadata))
			{
				if(StandardSalaryFaktorMetadata.mapDelegates == null)
				{
					StandardSalaryFaktorMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StandardSalaryFaktorMetadata.meta == null)
				{
					StandardSalaryFaktorMetadata.meta = new StandardSalaryFaktorMetadata();
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
				

				meta.AddTypeMap("StandardSalaryFaktorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StandardSalaryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GradeServiceYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AmountSalary", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "StandardSalaryFaktor";
				meta.Destination = "StandardSalaryFaktor";
				
				meta.spInsert = "proc_StandardSalaryFaktorInsert";				
				meta.spUpdate = "proc_StandardSalaryFaktorUpdate";		
				meta.spDelete = "proc_StandardSalaryFaktorDelete";
				meta.spLoadAll = "proc_StandardSalaryFaktorLoadAll";
				meta.spLoadByPrimaryKey = "proc_StandardSalaryFaktorLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StandardSalaryFaktorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
