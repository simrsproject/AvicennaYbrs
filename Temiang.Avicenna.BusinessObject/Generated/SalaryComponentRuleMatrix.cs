/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:25 PM
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
	abstract public class esSalaryComponentRuleMatrixCollection : esEntityCollectionWAuditLog
	{
		public esSalaryComponentRuleMatrixCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SalaryComponentRuleMatrixCollection";
		}

		#region Query Logic
		protected void InitQuery(esSalaryComponentRuleMatrixQuery query)
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
			this.InitQuery(query as esSalaryComponentRuleMatrixQuery);
		}
		#endregion
		
		virtual public SalaryComponentRuleMatrix DetachEntity(SalaryComponentRuleMatrix entity)
		{
			return base.DetachEntity(entity) as SalaryComponentRuleMatrix;
		}
		
		virtual public SalaryComponentRuleMatrix AttachEntity(SalaryComponentRuleMatrix entity)
		{
			return base.AttachEntity(entity) as SalaryComponentRuleMatrix;
		}
		
		virtual public void Combine(SalaryComponentRuleMatrixCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SalaryComponentRuleMatrix this[int index]
		{
			get
			{
				return base[index] as SalaryComponentRuleMatrix;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SalaryComponentRuleMatrix);
		}
	}



	[Serializable]
	abstract public class esSalaryComponentRuleMatrix : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSalaryComponentRuleMatrixQuery GetDynamicQuery()
		{
			return null;
		}

		public esSalaryComponentRuleMatrix()
		{

		}

		public esSalaryComponentRuleMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 salaryComponentRuleMatrixID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentRuleMatrixID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentRuleMatrixID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 salaryComponentRuleMatrixID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentRuleMatrixID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentRuleMatrixID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 salaryComponentRuleMatrixID)
		{
			esSalaryComponentRuleMatrixQuery query = this.GetDynamicQuery();
			query.Where(query.SalaryComponentRuleMatrixID == salaryComponentRuleMatrixID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 salaryComponentRuleMatrixID)
		{
			esParameters parms = new esParameters();
			parms.Add("SalaryComponentRuleMatrixID",salaryComponentRuleMatrixID);
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
						case "SalaryComponentRuleMatrixID": this.str.SalaryComponentRuleMatrixID = (string)value; break;							
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;							
						case "SalaryRuleComponentID": this.str.SalaryRuleComponentID = (string)value; break;							
						case "SROperandType": this.str.SROperandType = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SalaryComponentRuleMatrixID":
						
							if (value == null || value is System.Int32)
								this.SalaryComponentRuleMatrixID = (System.Int32?)value;
							break;
						
						case "SalaryComponentID":
						
							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
							break;
						
						case "SalaryRuleComponentID":
						
							if (value == null || value is System.Int32)
								this.SalaryRuleComponentID = (System.Int32?)value;
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
		/// Maps to SalaryComponentRuleMatrix.SalaryComponentRuleMatrixID
		/// </summary>
		virtual public System.Int32? SalaryComponentRuleMatrixID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentRuleMatrixID);
			}
			
			set
			{
				base.SetSystemInt32(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentRuleMatrixID, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRuleMatrix.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentID);
			}
			
			set
			{
				base.SetSystemInt32(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRuleMatrix.SalaryRuleComponentID
		/// </summary>
		virtual public System.Int32? SalaryRuleComponentID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryRuleComponentID);
			}
			
			set
			{
				base.SetSystemInt32(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryRuleComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRuleMatrix.SROperandType
		/// </summary>
		virtual public System.String SROperandType
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleMatrixMetadata.ColumnNames.SROperandType);
			}
			
			set
			{
				base.SetSystemString(SalaryComponentRuleMatrixMetadata.ColumnNames.SROperandType, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRuleMatrix.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRuleMatrix.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSalaryComponentRuleMatrix entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SalaryComponentRuleMatrixID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentRuleMatrixID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentRuleMatrixID = null;
					else entity.SalaryComponentRuleMatrixID = Convert.ToInt32(value);
				}
			}
				
			public System.String SalaryComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentID = null;
					else entity.SalaryComponentID = Convert.ToInt32(value);
				}
			}
				
			public System.String SalaryRuleComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryRuleComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryRuleComponentID = null;
					else entity.SalaryRuleComponentID = Convert.ToInt32(value);
				}
			}
				
			public System.String SROperandType
			{
				get
				{
					System.String data = entity.SROperandType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROperandType = null;
					else entity.SROperandType = Convert.ToString(value);
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
			

			private esSalaryComponentRuleMatrix entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSalaryComponentRuleMatrixQuery query)
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
				throw new Exception("esSalaryComponentRuleMatrix can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SalaryComponentRuleMatrix : esSalaryComponentRuleMatrix
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
	abstract public class esSalaryComponentRuleMatrixQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentRuleMatrixMetadata.Meta();
			}
		}	
		

		public esQueryItem SalaryComponentRuleMatrixID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentRuleMatrixID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryRuleComponentID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryRuleComponentID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SROperandType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleMatrixMetadata.ColumnNames.SROperandType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SalaryComponentRuleMatrixCollection")]
	public partial class SalaryComponentRuleMatrixCollection : esSalaryComponentRuleMatrixCollection, IEnumerable<SalaryComponentRuleMatrix>
	{
		public SalaryComponentRuleMatrixCollection()
		{

		}
		
		public static implicit operator List<SalaryComponentRuleMatrix>(SalaryComponentRuleMatrixCollection coll)
		{
			List<SalaryComponentRuleMatrix> list = new List<SalaryComponentRuleMatrix>();
			
			foreach (SalaryComponentRuleMatrix emp in coll)
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
				return  SalaryComponentRuleMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentRuleMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SalaryComponentRuleMatrix(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SalaryComponentRuleMatrix();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SalaryComponentRuleMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentRuleMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SalaryComponentRuleMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SalaryComponentRuleMatrix AddNew()
		{
			SalaryComponentRuleMatrix entity = base.AddNewEntity() as SalaryComponentRuleMatrix;
			
			return entity;
		}

		public SalaryComponentRuleMatrix FindByPrimaryKey(System.Int32 salaryComponentRuleMatrixID)
		{
			return base.FindByPrimaryKey(salaryComponentRuleMatrixID) as SalaryComponentRuleMatrix;
		}


		#region IEnumerable<SalaryComponentRuleMatrix> Members

		IEnumerator<SalaryComponentRuleMatrix> IEnumerable<SalaryComponentRuleMatrix>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SalaryComponentRuleMatrix;
			}
		}

		#endregion
		
		private SalaryComponentRuleMatrixQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SalaryComponentRuleMatrix' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SalaryComponentRuleMatrix ({SalaryComponentRuleMatrixID})")]
	[Serializable]
	public partial class SalaryComponentRuleMatrix : esSalaryComponentRuleMatrix
	{
		public SalaryComponentRuleMatrix()
		{

		}
	
		public SalaryComponentRuleMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentRuleMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esSalaryComponentRuleMatrixQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentRuleMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SalaryComponentRuleMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentRuleMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SalaryComponentRuleMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SalaryComponentRuleMatrixQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SalaryComponentRuleMatrixQuery : esSalaryComponentRuleMatrixQuery
	{
		public SalaryComponentRuleMatrixQuery()
		{

		}		
		
		public SalaryComponentRuleMatrixQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SalaryComponentRuleMatrixQuery";
        }
		
			
	}


	[Serializable]
	public partial class SalaryComponentRuleMatrixMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SalaryComponentRuleMatrixMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentRuleMatrixID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleMatrixMetadata.PropertyNames.SalaryComponentRuleMatrixID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleMatrixMetadata.PropertyNames.SalaryComponentID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryRuleComponentID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleMatrixMetadata.PropertyNames.SalaryRuleComponentID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRuleMatrixMetadata.ColumnNames.SROperandType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleMatrixMetadata.PropertyNames.SROperandType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentRuleMatrixMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRuleMatrixMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleMatrixMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SalaryComponentRuleMatrixMetadata Meta()
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
			 public const string SalaryComponentRuleMatrixID = "SalaryComponentRuleMatrixID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string SalaryRuleComponentID = "SalaryRuleComponentID";
			 public const string SROperandType = "SROperandType";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SalaryComponentRuleMatrixID = "SalaryComponentRuleMatrixID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string SalaryRuleComponentID = "SalaryRuleComponentID";
			 public const string SROperandType = "SROperandType";
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
			lock (typeof(SalaryComponentRuleMatrixMetadata))
			{
				if(SalaryComponentRuleMatrixMetadata.mapDelegates == null)
				{
					SalaryComponentRuleMatrixMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SalaryComponentRuleMatrixMetadata.meta == null)
				{
					SalaryComponentRuleMatrixMetadata.meta = new SalaryComponentRuleMatrixMetadata();
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
				

				meta.AddTypeMap("SalaryComponentRuleMatrixID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryRuleComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SROperandType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "SalaryComponentRuleMatrix";
				meta.Destination = "SalaryComponentRuleMatrix";
				
				meta.spInsert = "proc_SalaryComponentRuleMatrixInsert";				
				meta.spUpdate = "proc_SalaryComponentRuleMatrixUpdate";		
				meta.spDelete = "proc_SalaryComponentRuleMatrixDelete";
				meta.spLoadAll = "proc_SalaryComponentRuleMatrixLoadAll";
				meta.spLoadByPrimaryKey = "proc_SalaryComponentRuleMatrixLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SalaryComponentRuleMatrixMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
