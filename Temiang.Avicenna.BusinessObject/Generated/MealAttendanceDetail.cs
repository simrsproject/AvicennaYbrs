/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/31/2021 2:26:05 PM
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
	abstract public class esMealAttendanceDetailCollection : esEntityCollectionWAuditLog
	{
		public esMealAttendanceDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MealAttendanceDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esMealAttendanceDetailQuery query)
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
			this.InitQuery(query as esMealAttendanceDetailQuery);
		}
		#endregion
		
		virtual public MealAttendanceDetail DetachEntity(MealAttendanceDetail entity)
		{
			return base.DetachEntity(entity) as MealAttendanceDetail;
		}
		
		virtual public MealAttendanceDetail AttachEntity(MealAttendanceDetail entity)
		{
			return base.AttachEntity(entity) as MealAttendanceDetail;
		}
		
		virtual public void Combine(MealAttendanceDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MealAttendanceDetail this[int index]
		{
			get
			{
				return base[index] as MealAttendanceDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MealAttendanceDetail);
		}
	}



	[Serializable]
	abstract public class esMealAttendanceDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMealAttendanceDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esMealAttendanceDetail()
		{

		}

		public esMealAttendanceDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 mealAttendanceDetailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(mealAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(mealAttendanceDetailID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 mealAttendanceDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(mealAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(mealAttendanceDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 mealAttendanceDetailID)
		{
			esMealAttendanceDetailQuery query = this.GetDynamicQuery();
			query.Where(query.MealAttendanceDetailID == mealAttendanceDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 mealAttendanceDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("MealAttendanceDetailID",mealAttendanceDetailID);
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
						case "MealAttendanceDetailID": this.str.MealAttendanceDetailID = (string)value; break;							
						case "MealAttendanceID": this.str.MealAttendanceID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "Datetime": this.str.Datetime = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MealAttendanceDetailID":
						
							if (value == null || value is System.Int32)
								this.MealAttendanceDetailID = (System.Int32?)value;
							break;
						
						case "MealAttendanceID":
						
							if (value == null || value is System.Int32)
								this.MealAttendanceID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "Datetime":
						
							if (value == null || value is System.DateTime)
								this.Datetime = (System.DateTime?)value;
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
		/// Maps to MealAttendanceDetail.MealAttendanceDetailID
		/// </summary>
		virtual public System.Int32? MealAttendanceDetailID
		{
			get
			{
				return base.GetSystemInt32(MealAttendanceDetailMetadata.ColumnNames.MealAttendanceDetailID);
			}
			
			set
			{
				base.SetSystemInt32(MealAttendanceDetailMetadata.ColumnNames.MealAttendanceDetailID, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendanceDetail.MealAttendanceID
		/// </summary>
		virtual public System.Int32? MealAttendanceID
		{
			get
			{
				return base.GetSystemInt32(MealAttendanceDetailMetadata.ColumnNames.MealAttendanceID);
			}
			
			set
			{
				base.SetSystemInt32(MealAttendanceDetailMetadata.ColumnNames.MealAttendanceID, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendanceDetail.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MealAttendanceDetailMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(MealAttendanceDetailMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendanceDetail.Datetime
		/// </summary>
		virtual public System.DateTime? Datetime
		{
			get
			{
				return base.GetSystemDateTime(MealAttendanceDetailMetadata.ColumnNames.Datetime);
			}
			
			set
			{
				base.SetSystemDateTime(MealAttendanceDetailMetadata.ColumnNames.Datetime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendanceDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MealAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MealAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendanceDetail.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(MealAttendanceDetailMetadata.ColumnNames.LastUpdateUserID);
			}
			
			set
			{
				base.SetSystemString(MealAttendanceDetailMetadata.ColumnNames.LastUpdateUserID, value);
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
			public esStrings(esMealAttendanceDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MealAttendanceDetailID
			{
				get
				{
					System.Int32? data = entity.MealAttendanceDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MealAttendanceDetailID = null;
					else entity.MealAttendanceDetailID = Convert.ToInt32(value);
				}
			}
				
			public System.String MealAttendanceID
			{
				get
				{
					System.Int32? data = entity.MealAttendanceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MealAttendanceID = null;
					else entity.MealAttendanceID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String Datetime
			{
				get
				{
					System.DateTime? data = entity.Datetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Datetime = null;
					else entity.Datetime = Convert.ToDateTime(value);
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
				
			public System.String LastUpdateUserID
			{
				get
				{
					System.String data = entity.LastUpdateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateUserID = null;
					else entity.LastUpdateUserID = Convert.ToString(value);
				}
			}
			

			private esMealAttendanceDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMealAttendanceDetailQuery query)
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
				throw new Exception("esMealAttendanceDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esMealAttendanceDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MealAttendanceDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem MealAttendanceDetailID
		{
			get
			{
				return new esQueryItem(this, MealAttendanceDetailMetadata.ColumnNames.MealAttendanceDetailID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MealAttendanceID
		{
			get
			{
				return new esQueryItem(this, MealAttendanceDetailMetadata.ColumnNames.MealAttendanceID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MealAttendanceDetailMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Datetime
		{
			get
			{
				return new esQueryItem(this, MealAttendanceDetailMetadata.ColumnNames.Datetime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MealAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, MealAttendanceDetailMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MealAttendanceDetailCollection")]
	public partial class MealAttendanceDetailCollection : esMealAttendanceDetailCollection, IEnumerable<MealAttendanceDetail>
	{
		public MealAttendanceDetailCollection()
		{

		}
		
		public static implicit operator List<MealAttendanceDetail>(MealAttendanceDetailCollection coll)
		{
			List<MealAttendanceDetail> list = new List<MealAttendanceDetail>();
			
			foreach (MealAttendanceDetail emp in coll)
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
				return  MealAttendanceDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealAttendanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MealAttendanceDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MealAttendanceDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MealAttendanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealAttendanceDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MealAttendanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MealAttendanceDetail AddNew()
		{
			MealAttendanceDetail entity = base.AddNewEntity() as MealAttendanceDetail;
			
			return entity;
		}

		public MealAttendanceDetail FindByPrimaryKey(System.Int32 mealAttendanceDetailID)
		{
			return base.FindByPrimaryKey(mealAttendanceDetailID) as MealAttendanceDetail;
		}


		#region IEnumerable<MealAttendanceDetail> Members

		IEnumerator<MealAttendanceDetail> IEnumerable<MealAttendanceDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MealAttendanceDetail;
			}
		}

		#endregion
		
		private MealAttendanceDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MealAttendanceDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MealAttendanceDetail ({MealAttendanceDetailID})")]
	[Serializable]
	public partial class MealAttendanceDetail : esMealAttendanceDetail
	{
		public MealAttendanceDetail()
		{

		}
	
		public MealAttendanceDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MealAttendanceDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esMealAttendanceDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealAttendanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MealAttendanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealAttendanceDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MealAttendanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MealAttendanceDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MealAttendanceDetailQuery : esMealAttendanceDetailQuery
	{
		public MealAttendanceDetailQuery()
		{

		}		
		
		public MealAttendanceDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MealAttendanceDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class MealAttendanceDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MealAttendanceDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MealAttendanceDetailMetadata.ColumnNames.MealAttendanceDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MealAttendanceDetailMetadata.PropertyNames.MealAttendanceDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceDetailMetadata.ColumnNames.MealAttendanceID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MealAttendanceDetailMetadata.PropertyNames.MealAttendanceID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceDetailMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MealAttendanceDetailMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceDetailMetadata.ColumnNames.Datetime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealAttendanceDetailMetadata.PropertyNames.Datetime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealAttendanceDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceDetailMetadata.ColumnNames.LastUpdateUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MealAttendanceDetailMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MealAttendanceDetailMetadata Meta()
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
			 public const string MealAttendanceDetailID = "MealAttendanceDetailID";
			 public const string MealAttendanceID = "MealAttendanceID";
			 public const string PersonID = "PersonID";
			 public const string Datetime = "Datetime";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MealAttendanceDetailID = "MealAttendanceDetailID";
			 public const string MealAttendanceID = "MealAttendanceID";
			 public const string PersonID = "PersonID";
			 public const string Datetime = "Datetime";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
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
			lock (typeof(MealAttendanceDetailMetadata))
			{
				if(MealAttendanceDetailMetadata.mapDelegates == null)
				{
					MealAttendanceDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MealAttendanceDetailMetadata.meta == null)
				{
					MealAttendanceDetailMetadata.meta = new MealAttendanceDetailMetadata();
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
				

				meta.AddTypeMap("MealAttendanceDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MealAttendanceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Datetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				meta.Source = "MealAttendanceDetail";
				meta.Destination = "MealAttendanceDetail";
				
				meta.spInsert = "proc_MealAttendanceDetailInsert";				
				meta.spUpdate = "proc_MealAttendanceDetailUpdate";		
				meta.spDelete = "proc_MealAttendanceDetailDelete";
				meta.spLoadAll = "proc_MealAttendanceDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_MealAttendanceDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MealAttendanceDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
