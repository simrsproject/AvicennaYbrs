/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:10 PM
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
	abstract public class esApplicantPhysicalCollection : esEntityCollectionWAuditLog
	{
		public esApplicantPhysicalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantPhysicalCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantPhysicalQuery query)
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
			this.InitQuery(query as esApplicantPhysicalQuery);
		}
		#endregion
		
		virtual public ApplicantPhysical DetachEntity(ApplicantPhysical entity)
		{
			return base.DetachEntity(entity) as ApplicantPhysical;
		}
		
		virtual public ApplicantPhysical AttachEntity(ApplicantPhysical entity)
		{
			return base.AttachEntity(entity) as ApplicantPhysical;
		}
		
		virtual public void Combine(ApplicantPhysicalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantPhysical this[int index]
		{
			get
			{
				return base[index] as ApplicantPhysical;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantPhysical);
		}
	}



	[Serializable]
	abstract public class esApplicantPhysical : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantPhysicalQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantPhysical()
		{

		}

		public esApplicantPhysical(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantPhysicalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantPhysicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantPhysicalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantPhysicalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantPhysicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantPhysicalID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantPhysicalID)
		{
			esApplicantPhysicalQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantPhysicalID == applicantPhysicalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantPhysicalID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantPhysicalID",applicantPhysicalID);
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
						case "ApplicantPhysicalID": this.str.ApplicantPhysicalID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "SRPhysicalCharacteristic": this.str.SRPhysicalCharacteristic = (string)value; break;							
						case "PhysicalValue": this.str.PhysicalValue = (string)value; break;							
						case "SRMeasurementCode": this.str.SRMeasurementCode = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantPhysicalID":
						
							if (value == null || value is System.Int32)
								this.ApplicantPhysicalID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
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
		/// Maps to ApplicantPhysical.ApplicantPhysicalID
		/// </summary>
		virtual public System.Int32? ApplicantPhysicalID
		{
			get
			{
				return base.GetSystemInt32(ApplicantPhysicalMetadata.ColumnNames.ApplicantPhysicalID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantPhysicalMetadata.ColumnNames.ApplicantPhysicalID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPhysical.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantPhysicalMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantPhysicalMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPhysical.SRPhysicalCharacteristic
		/// </summary>
		virtual public System.String SRPhysicalCharacteristic
		{
			get
			{
				return base.GetSystemString(ApplicantPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic);
			}
			
			set
			{
				base.SetSystemString(ApplicantPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPhysical.PhysicalValue
		/// </summary>
		virtual public System.String PhysicalValue
		{
			get
			{
				return base.GetSystemString(ApplicantPhysicalMetadata.ColumnNames.PhysicalValue);
			}
			
			set
			{
				base.SetSystemString(ApplicantPhysicalMetadata.ColumnNames.PhysicalValue, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPhysical.SRMeasurementCode
		/// </summary>
		virtual public System.String SRMeasurementCode
		{
			get
			{
				return base.GetSystemString(ApplicantPhysicalMetadata.ColumnNames.SRMeasurementCode);
			}
			
			set
			{
				base.SetSystemString(ApplicantPhysicalMetadata.ColumnNames.SRMeasurementCode, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPhysical.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantPhysicalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantPhysicalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPhysical.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantPhysicalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantPhysicalMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantPhysical entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantPhysicalID
			{
				get
				{
					System.Int32? data = entity.ApplicantPhysicalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantPhysicalID = null;
					else entity.ApplicantPhysicalID = Convert.ToInt32(value);
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
				
			public System.String SRPhysicalCharacteristic
			{
				get
				{
					System.String data = entity.SRPhysicalCharacteristic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicalCharacteristic = null;
					else entity.SRPhysicalCharacteristic = Convert.ToString(value);
				}
			}
				
			public System.String PhysicalValue
			{
				get
				{
					System.String data = entity.PhysicalValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicalValue = null;
					else entity.PhysicalValue = Convert.ToString(value);
				}
			}
				
			public System.String SRMeasurementCode
			{
				get
				{
					System.String data = entity.SRMeasurementCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMeasurementCode = null;
					else entity.SRMeasurementCode = Convert.ToString(value);
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
			

			private esApplicantPhysical entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantPhysicalQuery query)
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
				throw new Exception("esApplicantPhysical can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantPhysical : esApplicantPhysical
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
	abstract public class esApplicantPhysicalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantPhysicalMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantPhysicalID
		{
			get
			{
				return new esQueryItem(this, ApplicantPhysicalMetadata.ColumnNames.ApplicantPhysicalID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantPhysicalMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPhysicalCharacteristic
		{
			get
			{
				return new esQueryItem(this, ApplicantPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, esSystemType.String);
			}
		} 
		
		public esQueryItem PhysicalValue
		{
			get
			{
				return new esQueryItem(this, ApplicantPhysicalMetadata.ColumnNames.PhysicalValue, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMeasurementCode
		{
			get
			{
				return new esQueryItem(this, ApplicantPhysicalMetadata.ColumnNames.SRMeasurementCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantPhysicalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantPhysicalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantPhysicalCollection")]
	public partial class ApplicantPhysicalCollection : esApplicantPhysicalCollection, IEnumerable<ApplicantPhysical>
	{
		public ApplicantPhysicalCollection()
		{

		}
		
		public static implicit operator List<ApplicantPhysical>(ApplicantPhysicalCollection coll)
		{
			List<ApplicantPhysical> list = new List<ApplicantPhysical>();
			
			foreach (ApplicantPhysical emp in coll)
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
				return  ApplicantPhysicalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantPhysicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantPhysical(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantPhysical();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantPhysicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantPhysicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantPhysicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantPhysical AddNew()
		{
			ApplicantPhysical entity = base.AddNewEntity() as ApplicantPhysical;
			
			return entity;
		}

		public ApplicantPhysical FindByPrimaryKey(System.Int32 applicantPhysicalID)
		{
			return base.FindByPrimaryKey(applicantPhysicalID) as ApplicantPhysical;
		}


		#region IEnumerable<ApplicantPhysical> Members

		IEnumerator<ApplicantPhysical> IEnumerable<ApplicantPhysical>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantPhysical;
			}
		}

		#endregion
		
		private ApplicantPhysicalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantPhysical' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantPhysical ({ApplicantPhysicalID})")]
	[Serializable]
	public partial class ApplicantPhysical : esApplicantPhysical
	{
		public ApplicantPhysical()
		{

		}
	
		public ApplicantPhysical(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantPhysicalMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantPhysicalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantPhysicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantPhysicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantPhysicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantPhysicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantPhysicalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantPhysicalQuery : esApplicantPhysicalQuery
	{
		public ApplicantPhysicalQuery()
		{

		}		
		
		public ApplicantPhysicalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantPhysicalQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantPhysicalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantPhysicalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantPhysicalMetadata.ColumnNames.ApplicantPhysicalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantPhysicalMetadata.PropertyNames.ApplicantPhysicalID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPhysicalMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantPhysicalMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPhysicalMetadata.PropertyNames.SRPhysicalCharacteristic;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPhysicalMetadata.ColumnNames.PhysicalValue, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPhysicalMetadata.PropertyNames.PhysicalValue;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPhysicalMetadata.ColumnNames.SRMeasurementCode, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPhysicalMetadata.PropertyNames.SRMeasurementCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPhysicalMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantPhysicalMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPhysicalMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPhysicalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantPhysicalMetadata Meta()
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
			 public const string ApplicantPhysicalID = "ApplicantPhysicalID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRPhysicalCharacteristic = "SRPhysicalCharacteristic";
			 public const string PhysicalValue = "PhysicalValue";
			 public const string SRMeasurementCode = "SRMeasurementCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantPhysicalID = "ApplicantPhysicalID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRPhysicalCharacteristic = "SRPhysicalCharacteristic";
			 public const string PhysicalValue = "PhysicalValue";
			 public const string SRMeasurementCode = "SRMeasurementCode";
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
			lock (typeof(ApplicantPhysicalMetadata))
			{
				if(ApplicantPhysicalMetadata.mapDelegates == null)
				{
					ApplicantPhysicalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantPhysicalMetadata.meta == null)
				{
					ApplicantPhysicalMetadata.meta = new ApplicantPhysicalMetadata();
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
				

				meta.AddTypeMap("ApplicantPhysicalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPhysicalCharacteristic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicalValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMeasurementCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantPhysical";
				meta.Destination = "ApplicantPhysical";
				
				meta.spInsert = "proc_ApplicantPhysicalInsert";				
				meta.spUpdate = "proc_ApplicantPhysicalUpdate";		
				meta.spDelete = "proc_ApplicantPhysicalDelete";
				meta.spLoadAll = "proc_ApplicantPhysicalLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantPhysicalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantPhysicalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
