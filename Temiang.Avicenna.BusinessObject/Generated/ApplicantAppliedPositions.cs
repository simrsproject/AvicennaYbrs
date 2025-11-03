/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esApplicantAppliedPositionsCollection : esEntityCollectionWAuditLog
	{
		public esApplicantAppliedPositionsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantAppliedPositionsCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantAppliedPositionsQuery query)
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
			this.InitQuery(query as esApplicantAppliedPositionsQuery);
		}
		#endregion
		
		virtual public ApplicantAppliedPositions DetachEntity(ApplicantAppliedPositions entity)
		{
			return base.DetachEntity(entity) as ApplicantAppliedPositions;
		}
		
		virtual public ApplicantAppliedPositions AttachEntity(ApplicantAppliedPositions entity)
		{
			return base.AttachEntity(entity) as ApplicantAppliedPositions;
		}
		
		virtual public void Combine(ApplicantAppliedPositionsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantAppliedPositions this[int index]
		{
			get
			{
				return base[index] as ApplicantAppliedPositions;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantAppliedPositions);
		}
	}



	[Serializable]
	abstract public class esApplicantAppliedPositions : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantAppliedPositionsQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantAppliedPositions()
		{

		}

		public esApplicantAppliedPositions(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantAppliedPositionsID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantAppliedPositionsID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantAppliedPositionsID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantAppliedPositionsID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantAppliedPositionsID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantAppliedPositionsID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantAppliedPositionsID)
		{
			esApplicantAppliedPositionsQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantAppliedPositionsID == applicantAppliedPositionsID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantAppliedPositionsID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantAppliedPositionsID",applicantAppliedPositionsID);
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
						case "ApplicantAppliedPositionsID": this.str.ApplicantAppliedPositionsID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantAppliedPositionsID":
						
							if (value == null || value is System.Int32)
								this.ApplicantAppliedPositionsID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
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
		/// Maps to ApplicantAppliedPositions.ApplicantAppliedPositionsID
		/// </summary>
		virtual public System.Int32? ApplicantAppliedPositionsID
		{
			get
			{
				return base.GetSystemInt32(ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantAppliedPositionsID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantAppliedPositionsID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantAppliedPositions.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantAppliedPositions.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(ApplicantAppliedPositionsMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantAppliedPositionsMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantAppliedPositions.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantAppliedPositions.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantAppliedPositions entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantAppliedPositionsID
			{
				get
				{
					System.Int32? data = entity.ApplicantAppliedPositionsID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantAppliedPositionsID = null;
					else entity.ApplicantAppliedPositionsID = Convert.ToInt32(value);
				}
			}
				
			public System.String ApplicantID
			{
				get
				{
					System.Int32? data = entity.ApplicantID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantID = null;
					else entity.ApplicantID = Convert.ToInt32(value);
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
			

			private esApplicantAppliedPositions entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantAppliedPositionsQuery query)
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
				throw new Exception("esApplicantAppliedPositions can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantAppliedPositions : esApplicantAppliedPositions
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
	abstract public class esApplicantAppliedPositionsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantAppliedPositionsMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantAppliedPositionsID
		{
			get
			{
				return new esQueryItem(this, ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantAppliedPositionsID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, ApplicantAppliedPositionsMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantAppliedPositionsCollection")]
	public partial class ApplicantAppliedPositionsCollection : esApplicantAppliedPositionsCollection, IEnumerable<ApplicantAppliedPositions>
	{
		public ApplicantAppliedPositionsCollection()
		{

		}
		
		public static implicit operator List<ApplicantAppliedPositions>(ApplicantAppliedPositionsCollection coll)
		{
			List<ApplicantAppliedPositions> list = new List<ApplicantAppliedPositions>();
			
			foreach (ApplicantAppliedPositions emp in coll)
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
				return  ApplicantAppliedPositionsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantAppliedPositionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantAppliedPositions(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantAppliedPositions();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantAppliedPositionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantAppliedPositionsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantAppliedPositionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantAppliedPositions AddNew()
		{
			ApplicantAppliedPositions entity = base.AddNewEntity() as ApplicantAppliedPositions;
			
			return entity;
		}

		public ApplicantAppliedPositions FindByPrimaryKey(System.Int32 applicantAppliedPositionsID)
		{
			return base.FindByPrimaryKey(applicantAppliedPositionsID) as ApplicantAppliedPositions;
		}


		#region IEnumerable<ApplicantAppliedPositions> Members

		IEnumerator<ApplicantAppliedPositions> IEnumerable<ApplicantAppliedPositions>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantAppliedPositions;
			}
		}

		#endregion
		
		private ApplicantAppliedPositionsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantAppliedPositions' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantAppliedPositions ({ApplicantAppliedPositionsID})")]
	[Serializable]
	public partial class ApplicantAppliedPositions : esApplicantAppliedPositions
	{
		public ApplicantAppliedPositions()
		{

		}
	
		public ApplicantAppliedPositions(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantAppliedPositionsMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantAppliedPositionsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantAppliedPositionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantAppliedPositionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantAppliedPositionsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantAppliedPositionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantAppliedPositionsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantAppliedPositionsQuery : esApplicantAppliedPositionsQuery
	{
		public ApplicantAppliedPositionsQuery()
		{

		}		
		
		public ApplicantAppliedPositionsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantAppliedPositionsQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantAppliedPositionsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantAppliedPositionsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantAppliedPositionsID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantAppliedPositionsMetadata.PropertyNames.ApplicantAppliedPositionsID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantAppliedPositionsMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantAppliedPositionsMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantAppliedPositionsMetadata.ColumnNames.PositionID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantAppliedPositionsMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantAppliedPositionsMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantAppliedPositionsMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantAppliedPositionsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantAppliedPositionsMetadata Meta()
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
			 public const string ApplicantAppliedPositionsID = "ApplicantAppliedPositionsID";
			 public const string ApplicantID = "ApplicantID";
			 public const string PositionID = "PositionID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantAppliedPositionsID = "ApplicantAppliedPositionsID";
			 public const string ApplicantID = "ApplicantID";
			 public const string PositionID = "PositionID";
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
			lock (typeof(ApplicantAppliedPositionsMetadata))
			{
				if(ApplicantAppliedPositionsMetadata.mapDelegates == null)
				{
					ApplicantAppliedPositionsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantAppliedPositionsMetadata.meta == null)
				{
					ApplicantAppliedPositionsMetadata.meta = new ApplicantAppliedPositionsMetadata();
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
				

				meta.AddTypeMap("ApplicantAppliedPositionsID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantAppliedPositions";
				meta.Destination = "ApplicantAppliedPositions";
				
				meta.spInsert = "proc_ApplicantAppliedPositionsInsert";				
				meta.spUpdate = "proc_ApplicantAppliedPositionsUpdate";		
				meta.spDelete = "proc_ApplicantAppliedPositionsDelete";
				meta.spLoadAll = "proc_ApplicantAppliedPositionsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantAppliedPositionsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantAppliedPositionsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
