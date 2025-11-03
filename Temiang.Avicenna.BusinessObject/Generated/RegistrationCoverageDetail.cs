/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/6/2017 1:10:13 AM
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
	abstract public class esRegistrationCoverageDetailCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationCoverageDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationCoverageDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationCoverageDetailQuery query)
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
			this.InitQuery(query as esRegistrationCoverageDetailQuery);
		}
		#endregion
		
		virtual public RegistrationCoverageDetail DetachEntity(RegistrationCoverageDetail entity)
		{
			return base.DetachEntity(entity) as RegistrationCoverageDetail;
		}
		
		virtual public RegistrationCoverageDetail AttachEntity(RegistrationCoverageDetail entity)
		{
			return base.AttachEntity(entity) as RegistrationCoverageDetail;
		}
		
		virtual public void Combine(RegistrationCoverageDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationCoverageDetail this[int index]
		{
			get
			{
				return base[index] as RegistrationCoverageDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationCoverageDetail);
		}
	}



	[Serializable]
	abstract public class esRegistrationCoverageDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationCoverageDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationCoverageDetail()
		{

		}

		public esRegistrationCoverageDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String classID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, classID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, classID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String classID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, classID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, classID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String classID)
		{
			esRegistrationCoverageDetailQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.ClassID == classID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String classID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("ClassID",classID);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "CoverageAmount": this.str.CoverageAmount = (string)value; break;							
						case "CalculatedAmount": this.str.CalculatedAmount = (string)value; break;							
						case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;							
						case "LastCreateUserID": this.str.LastCreateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CoverageAmount":
						
							if (value == null || value is System.Decimal)
								this.CoverageAmount = (System.Decimal?)value;
							break;
						
						case "CalculatedAmount":
						
							if (value == null || value is System.Decimal)
								this.CalculatedAmount = (System.Decimal?)value;
							break;
						
						case "LastCreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastCreateDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationCoverageDetail.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationCoverageDetailMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationCoverageDetailMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCoverageDetail.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(RegistrationCoverageDetailMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCoverageDetailMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCoverageDetail.CoverageAmount
		/// </summary>
		virtual public System.Decimal? CoverageAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationCoverageDetailMetadata.ColumnNames.CoverageAmount);
			}
			
			set
			{
				base.SetSystemDecimal(RegistrationCoverageDetailMetadata.ColumnNames.CoverageAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCoverageDetail.CalculatedAmount
		/// </summary>
		virtual public System.Decimal? CalculatedAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationCoverageDetailMetadata.ColumnNames.CalculatedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(RegistrationCoverageDetailMetadata.ColumnNames.CalculatedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCoverageDetail.LastCreateDateTime
		/// </summary>
		virtual public System.DateTime? LastCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationCoverageDetailMetadata.ColumnNames.LastCreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationCoverageDetailMetadata.ColumnNames.LastCreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCoverageDetail.LastCreateUserID
		/// </summary>
		virtual public System.String LastCreateUserID
		{
			get
			{
				return base.GetSystemString(RegistrationCoverageDetailMetadata.ColumnNames.LastCreateUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCoverageDetailMetadata.ColumnNames.LastCreateUserID, value);
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
			public esStrings(esRegistrationCoverageDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String CoverageAmount
			{
				get
				{
					System.Decimal? data = entity.CoverageAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageAmount = null;
					else entity.CoverageAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String CalculatedAmount
			{
				get
				{
					System.Decimal? data = entity.CalculatedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CalculatedAmount = null;
					else entity.CalculatedAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String LastCreateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
					else entity.LastCreateDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LastCreateUserID
			{
				get
				{
					System.String data = entity.LastCreateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateUserID = null;
					else entity.LastCreateUserID = Convert.ToString(value);
				}
			}
			

			private esRegistrationCoverageDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationCoverageDetailQuery query)
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
				throw new Exception("esRegistrationCoverageDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RegistrationCoverageDetail : esRegistrationCoverageDetail
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
	abstract public class esRegistrationCoverageDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCoverageDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationCoverageDetailMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, RegistrationCoverageDetailMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem CoverageAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationCoverageDetailMetadata.ColumnNames.CoverageAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CalculatedAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationCoverageDetailMetadata.ColumnNames.CalculatedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastCreateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationCoverageDetailMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastCreateUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationCoverageDetailMetadata.ColumnNames.LastCreateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationCoverageDetailCollection")]
	public partial class RegistrationCoverageDetailCollection : esRegistrationCoverageDetailCollection, IEnumerable<RegistrationCoverageDetail>
	{
		public RegistrationCoverageDetailCollection()
		{

		}
		
		public static implicit operator List<RegistrationCoverageDetail>(RegistrationCoverageDetailCollection coll)
		{
			List<RegistrationCoverageDetail> list = new List<RegistrationCoverageDetail>();
			
			foreach (RegistrationCoverageDetail emp in coll)
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
				return  RegistrationCoverageDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCoverageDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationCoverageDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationCoverageDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationCoverageDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCoverageDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationCoverageDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationCoverageDetail AddNew()
		{
			RegistrationCoverageDetail entity = base.AddNewEntity() as RegistrationCoverageDetail;
			
			return entity;
		}

		public RegistrationCoverageDetail FindByPrimaryKey(System.String registrationNo, System.String classID)
		{
			return base.FindByPrimaryKey(registrationNo, classID) as RegistrationCoverageDetail;
		}


		#region IEnumerable<RegistrationCoverageDetail> Members

		IEnumerator<RegistrationCoverageDetail> IEnumerable<RegistrationCoverageDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationCoverageDetail;
			}
		}

		#endregion
		
		private RegistrationCoverageDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationCoverageDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationCoverageDetail ({RegistrationNo},{ClassID})")]
	[Serializable]
	public partial class RegistrationCoverageDetail : esRegistrationCoverageDetail
	{
		public RegistrationCoverageDetail()
		{

		}
	
		public RegistrationCoverageDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCoverageDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationCoverageDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCoverageDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationCoverageDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCoverageDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationCoverageDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationCoverageDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationCoverageDetailQuery : esRegistrationCoverageDetailQuery
	{
		public RegistrationCoverageDetailQuery()
		{

		}		
		
		public RegistrationCoverageDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationCoverageDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationCoverageDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationCoverageDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationCoverageDetailMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCoverageDetailMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCoverageDetailMetadata.ColumnNames.ClassID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCoverageDetailMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCoverageDetailMetadata.ColumnNames.CoverageAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationCoverageDetailMetadata.PropertyNames.CoverageAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCoverageDetailMetadata.ColumnNames.CalculatedAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationCoverageDetailMetadata.PropertyNames.CalculatedAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCoverageDetailMetadata.ColumnNames.LastCreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationCoverageDetailMetadata.PropertyNames.LastCreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCoverageDetailMetadata.ColumnNames.LastCreateUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCoverageDetailMetadata.PropertyNames.LastCreateUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationCoverageDetailMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ClassID = "ClassID";
			 public const string CoverageAmount = "CoverageAmount";
			 public const string CalculatedAmount = "CalculatedAmount";
			 public const string LastCreateDateTime = "LastCreateDateTime";
			 public const string LastCreateUserID = "LastCreateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ClassID = "ClassID";
			 public const string CoverageAmount = "CoverageAmount";
			 public const string CalculatedAmount = "CalculatedAmount";
			 public const string LastCreateDateTime = "LastCreateDateTime";
			 public const string LastCreateUserID = "LastCreateUserID";
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
			lock (typeof(RegistrationCoverageDetailMetadata))
			{
				if(RegistrationCoverageDetailMetadata.mapDelegates == null)
				{
					RegistrationCoverageDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationCoverageDetailMetadata.meta == null)
				{
					RegistrationCoverageDetailMetadata.meta = new RegistrationCoverageDetailMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CalculatedAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCreateUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationCoverageDetail";
				meta.Destination = "RegistrationCoverageDetail";
				
				meta.spInsert = "proc_RegistrationCoverageDetailInsert";				
				meta.spUpdate = "proc_RegistrationCoverageDetailUpdate";		
				meta.spDelete = "proc_RegistrationCoverageDetailDelete";
				meta.spLoadAll = "proc_RegistrationCoverageDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationCoverageDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationCoverageDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
