/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/4/2016 1:15:55 PM
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
	abstract public class esParamedicFeeByNumberOfPatientsDetailCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeByNumberOfPatientsDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeByNumberOfPatientsDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeByNumberOfPatientsDetailQuery query)
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
			this.InitQuery(query as esParamedicFeeByNumberOfPatientsDetailQuery);
		}
		#endregion
		
		virtual public ParamedicFeeByNumberOfPatientsDetail DetachEntity(ParamedicFeeByNumberOfPatientsDetail entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeByNumberOfPatientsDetail;
		}
		
		virtual public ParamedicFeeByNumberOfPatientsDetail AttachEntity(ParamedicFeeByNumberOfPatientsDetail entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeByNumberOfPatientsDetail;
		}
		
		virtual public void Combine(ParamedicFeeByNumberOfPatientsDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeByNumberOfPatientsDetail this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeByNumberOfPatientsDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeByNumberOfPatientsDetail);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeByNumberOfPatientsDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeByNumberOfPatientsDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeByNumberOfPatientsDetail()
		{

		}

		public esParamedicFeeByNumberOfPatientsDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.DateTime registrationDate, System.String paramedicID, System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationDate, paramedicID, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationDate, paramedicID, registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime registrationDate, System.String paramedicID, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationDate, paramedicID, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationDate, paramedicID, registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.DateTime registrationDate, System.String paramedicID, System.String registrationNo)
		{
			esParamedicFeeByNumberOfPatientsDetailQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationDate == registrationDate, query.ParamedicID == paramedicID, query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.DateTime registrationDate, System.String paramedicID, System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationDate",registrationDate);			parms.Add("ParamedicID",paramedicID);			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationDate": this.str.RegistrationDate = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RegistrationDate":
						
							if (value == null || value is System.DateTime)
								this.RegistrationDate = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeByNumberOfPatientsDetail.RegistrationDate
		/// </summary>
		virtual public System.DateTime? RegistrationDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsDetail.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsDetail.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeByNumberOfPatientsDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationDate
			{
				get
				{
					System.DateTime? data = entity.RegistrationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationDate = null;
					else entity.RegistrationDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
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
			

			private esParamedicFeeByNumberOfPatientsDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeByNumberOfPatientsDetailQuery query)
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
				throw new Exception("esParamedicFeeByNumberOfPatientsDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeByNumberOfPatientsDetail : esParamedicFeeByNumberOfPatientsDetail
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
	abstract public class esParamedicFeeByNumberOfPatientsDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByNumberOfPatientsDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeByNumberOfPatientsDetailCollection")]
	public partial class ParamedicFeeByNumberOfPatientsDetailCollection : esParamedicFeeByNumberOfPatientsDetailCollection, IEnumerable<ParamedicFeeByNumberOfPatientsDetail>
	{
		public ParamedicFeeByNumberOfPatientsDetailCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeByNumberOfPatientsDetail>(ParamedicFeeByNumberOfPatientsDetailCollection coll)
		{
			List<ParamedicFeeByNumberOfPatientsDetail> list = new List<ParamedicFeeByNumberOfPatientsDetail>();
			
			foreach (ParamedicFeeByNumberOfPatientsDetail emp in coll)
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
				return  ParamedicFeeByNumberOfPatientsDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByNumberOfPatientsDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeByNumberOfPatientsDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeByNumberOfPatientsDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeByNumberOfPatientsDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByNumberOfPatientsDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeByNumberOfPatientsDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeByNumberOfPatientsDetail AddNew()
		{
			ParamedicFeeByNumberOfPatientsDetail entity = base.AddNewEntity() as ParamedicFeeByNumberOfPatientsDetail;
			
			return entity;
		}

		public ParamedicFeeByNumberOfPatientsDetail FindByPrimaryKey(System.DateTime registrationDate, System.String paramedicID, System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationDate, paramedicID, registrationNo) as ParamedicFeeByNumberOfPatientsDetail;
		}


		#region IEnumerable<ParamedicFeeByNumberOfPatientsDetail> Members

		IEnumerator<ParamedicFeeByNumberOfPatientsDetail> IEnumerable<ParamedicFeeByNumberOfPatientsDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeByNumberOfPatientsDetail;
			}
		}

		#endregion
		
		private ParamedicFeeByNumberOfPatientsDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeByNumberOfPatientsDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeByNumberOfPatientsDetail ({RegistrationDate},{ParamedicID},{RegistrationNo})")]
	[Serializable]
	public partial class ParamedicFeeByNumberOfPatientsDetail : esParamedicFeeByNumberOfPatientsDetail
	{
		public ParamedicFeeByNumberOfPatientsDetail()
		{

		}
	
		public ParamedicFeeByNumberOfPatientsDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByNumberOfPatientsDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeByNumberOfPatientsDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByNumberOfPatientsDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeByNumberOfPatientsDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByNumberOfPatientsDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeByNumberOfPatientsDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeByNumberOfPatientsDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeByNumberOfPatientsDetailQuery : esParamedicFeeByNumberOfPatientsDetailQuery
	{
		public ParamedicFeeByNumberOfPatientsDetailQuery()
		{

		}		
		
		public ParamedicFeeByNumberOfPatientsDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeByNumberOfPatientsDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeByNumberOfPatientsDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeByNumberOfPatientsDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationDate, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByNumberOfPatientsDetailMetadata.PropertyNames.RegistrationDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByNumberOfPatientsDetailMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByNumberOfPatientsDetailMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByNumberOfPatientsDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsDetailMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByNumberOfPatientsDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeByNumberOfPatientsDetailMetadata Meta()
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
			 public const string RegistrationDate = "RegistrationDate";
			 public const string ParamedicID = "ParamedicID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationDate = "RegistrationDate";
			 public const string ParamedicID = "ParamedicID";
			 public const string RegistrationNo = "RegistrationNo";
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
			lock (typeof(ParamedicFeeByNumberOfPatientsDetailMetadata))
			{
				if(ParamedicFeeByNumberOfPatientsDetailMetadata.mapDelegates == null)
				{
					ParamedicFeeByNumberOfPatientsDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeByNumberOfPatientsDetailMetadata.meta == null)
				{
					ParamedicFeeByNumberOfPatientsDetailMetadata.meta = new ParamedicFeeByNumberOfPatientsDetailMetadata();
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
				

				meta.AddTypeMap("RegistrationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeByNumberOfPatientsDetail";
				meta.Destination = "ParamedicFeeByNumberOfPatientsDetail";
				
				meta.spInsert = "proc_ParamedicFeeByNumberOfPatientsDetailInsert";				
				meta.spUpdate = "proc_ParamedicFeeByNumberOfPatientsDetailUpdate";		
				meta.spDelete = "proc_ParamedicFeeByNumberOfPatientsDetailDelete";
				meta.spLoadAll = "proc_ParamedicFeeByNumberOfPatientsDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeByNumberOfPatientsDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeByNumberOfPatientsDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
