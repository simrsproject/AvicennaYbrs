/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/30/2021 8:15:22 PM
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
	abstract public class esMealAttendanceCollection : esEntityCollectionWAuditLog
	{
		public esMealAttendanceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MealAttendanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esMealAttendanceQuery query)
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
			this.InitQuery(query as esMealAttendanceQuery);
		}
		#endregion
		
		virtual public MealAttendance DetachEntity(MealAttendance entity)
		{
			return base.DetachEntity(entity) as MealAttendance;
		}
		
		virtual public MealAttendance AttachEntity(MealAttendance entity)
		{
			return base.AttachEntity(entity) as MealAttendance;
		}
		
		virtual public void Combine(MealAttendanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MealAttendance this[int index]
		{
			get
			{
				return base[index] as MealAttendance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MealAttendance);
		}
	}



	[Serializable]
	abstract public class esMealAttendance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMealAttendanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esMealAttendance()
		{

		}

		public esMealAttendance(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 mealAttendanceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(mealAttendanceID);
			else
				return LoadByPrimaryKeyStoredProcedure(mealAttendanceID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 mealAttendanceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(mealAttendanceID);
			else
				return LoadByPrimaryKeyStoredProcedure(mealAttendanceID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 mealAttendanceID)
		{
			esMealAttendanceQuery query = this.GetDynamicQuery();
			query.Where(query.MealAttendanceID == mealAttendanceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 mealAttendanceID)
		{
			esParameters parms = new esParameters();
			parms.Add("MealAttendanceID",mealAttendanceID);
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
						case "MealAttendanceID": this.str.MealAttendanceID = (string)value; break;							
						case "OpenDatetime": this.str.OpenDatetime = (string)value; break;							
						case "CloseDatetime": this.str.CloseDatetime = (string)value; break;							
						case "Status": this.str.Status = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MealAttendanceID":
						
							if (value == null || value is System.Int32)
								this.MealAttendanceID = (System.Int32?)value;
							break;
						
						case "OpenDatetime":
						
							if (value == null || value is System.DateTime)
								this.OpenDatetime = (System.DateTime?)value;
							break;
						
						case "CloseDatetime":
						
							if (value == null || value is System.DateTime)
								this.CloseDatetime = (System.DateTime?)value;
							break;
						
						case "Status":
						
							if (value == null || value is System.Byte)
								this.Status = (System.Byte?)value;
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
		/// Maps to MealAttendance.MealAttendanceID
		/// </summary>
		virtual public System.Int32? MealAttendanceID
		{
			get
			{
				return base.GetSystemInt32(MealAttendanceMetadata.ColumnNames.MealAttendanceID);
			}
			
			set
			{
				base.SetSystemInt32(MealAttendanceMetadata.ColumnNames.MealAttendanceID, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendance.OpenDatetime
		/// </summary>
		virtual public System.DateTime? OpenDatetime
		{
			get
			{
				return base.GetSystemDateTime(MealAttendanceMetadata.ColumnNames.OpenDatetime);
			}
			
			set
			{
				base.SetSystemDateTime(MealAttendanceMetadata.ColumnNames.OpenDatetime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendance.CloseDatetime
		/// </summary>
		virtual public System.DateTime? CloseDatetime
		{
			get
			{
				return base.GetSystemDateTime(MealAttendanceMetadata.ColumnNames.CloseDatetime);
			}
			
			set
			{
				base.SetSystemDateTime(MealAttendanceMetadata.ColumnNames.CloseDatetime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendance.Status
		/// </summary>
		virtual public System.Byte? Status
		{
			get
			{
				return base.GetSystemByte(MealAttendanceMetadata.ColumnNames.Status);
			}
			
			set
			{
				base.SetSystemByte(MealAttendanceMetadata.ColumnNames.Status, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendance.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(MealAttendanceMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(MealAttendanceMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MealAttendanceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MealAttendanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealAttendance.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(MealAttendanceMetadata.ColumnNames.LastUpdateUserID);
			}
			
			set
			{
				base.SetSystemString(MealAttendanceMetadata.ColumnNames.LastUpdateUserID, value);
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
			public esStrings(esMealAttendance entity)
			{
				this.entity = entity;
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
				
			public System.String OpenDatetime
			{
				get
				{
					System.DateTime? data = entity.OpenDatetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenDatetime = null;
					else entity.OpenDatetime = Convert.ToDateTime(value);
				}
			}
				
			public System.String CloseDatetime
			{
				get
				{
					System.DateTime? data = entity.CloseDatetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CloseDatetime = null;
					else entity.CloseDatetime = Convert.ToDateTime(value);
				}
			}
				
			public System.String Status
			{
				get
				{
					System.Byte? data = entity.Status;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Status = null;
					else entity.Status = Convert.ToByte(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			

			private esMealAttendance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMealAttendanceQuery query)
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
				throw new Exception("esMealAttendance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esMealAttendanceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MealAttendanceMetadata.Meta();
			}
		}	
		

		public esQueryItem MealAttendanceID
		{
			get
			{
				return new esQueryItem(this, MealAttendanceMetadata.ColumnNames.MealAttendanceID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OpenDatetime
		{
			get
			{
				return new esQueryItem(this, MealAttendanceMetadata.ColumnNames.OpenDatetime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CloseDatetime
		{
			get
			{
				return new esQueryItem(this, MealAttendanceMetadata.ColumnNames.CloseDatetime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Status
		{
			get
			{
				return new esQueryItem(this, MealAttendanceMetadata.ColumnNames.Status, esSystemType.Byte);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, MealAttendanceMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MealAttendanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, MealAttendanceMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MealAttendanceCollection")]
	public partial class MealAttendanceCollection : esMealAttendanceCollection, IEnumerable<MealAttendance>
	{
		public MealAttendanceCollection()
		{

		}
		
		public static implicit operator List<MealAttendance>(MealAttendanceCollection coll)
		{
			List<MealAttendance> list = new List<MealAttendance>();
			
			foreach (MealAttendance emp in coll)
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
				return  MealAttendanceMetadata.Meta();
			}
		}
		
		
		override protected string GetConnectionName()
		{
			return "Temiang.Avicenna.BusinessObject";
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealAttendanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MealAttendance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MealAttendance();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MealAttendanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealAttendanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MealAttendanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MealAttendance AddNew()
		{
			MealAttendance entity = base.AddNewEntity() as MealAttendance;
			
			return entity;
		}

		public MealAttendance FindByPrimaryKey(System.Int32 mealAttendanceID)
		{
			return base.FindByPrimaryKey(mealAttendanceID) as MealAttendance;
		}


		#region IEnumerable<MealAttendance> Members

		IEnumerator<MealAttendance> IEnumerable<MealAttendance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MealAttendance;
			}
		}

		#endregion
		
		private MealAttendanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MealAttendance' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MealAttendance ({MealAttendanceID})")]
	[Serializable]
	public partial class MealAttendance : esMealAttendance
	{
		public MealAttendance()
		{

		}
	
		public MealAttendance(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MealAttendanceMetadata.Meta();
			}
		}
		
		
		override protected string GetConnectionName()
		{
			return "Temiang.Avicenna.BusinessObject";
		}
		
		override protected esMealAttendanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealAttendanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MealAttendanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealAttendanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MealAttendanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MealAttendanceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MealAttendanceQuery : esMealAttendanceQuery
	{
		public MealAttendanceQuery()
		{

		}		
		
		public MealAttendanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MealAttendanceQuery";
        }
		
		
		override protected string GetConnectionName()
		{
			return "Temiang.Avicenna.BusinessObject";
		}	
	}


	[Serializable]
	public partial class MealAttendanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MealAttendanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MealAttendanceMetadata.ColumnNames.MealAttendanceID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MealAttendanceMetadata.PropertyNames.MealAttendanceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceMetadata.ColumnNames.OpenDatetime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealAttendanceMetadata.PropertyNames.OpenDatetime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceMetadata.ColumnNames.CloseDatetime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealAttendanceMetadata.PropertyNames.CloseDatetime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceMetadata.ColumnNames.Status, 3, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = MealAttendanceMetadata.PropertyNames.Status;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MealAttendanceMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealAttendanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealAttendanceMetadata.ColumnNames.LastUpdateUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MealAttendanceMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MealAttendanceMetadata Meta()
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
			 public const string MealAttendanceID = "MealAttendanceID";
			 public const string OpenDatetime = "OpenDatetime";
			 public const string CloseDatetime = "CloseDatetime";
			 public const string Status = "Status";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MealAttendanceID = "MealAttendanceID";
			 public const string OpenDatetime = "OpenDatetime";
			 public const string CloseDatetime = "CloseDatetime";
			 public const string Status = "Status";
			 public const string Notes = "Notes";
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
			lock (typeof(MealAttendanceMetadata))
			{
				if(MealAttendanceMetadata.mapDelegates == null)
				{
					MealAttendanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MealAttendanceMetadata.meta == null)
				{
					MealAttendanceMetadata.meta = new MealAttendanceMetadata();
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
				

				meta.AddTypeMap("MealAttendanceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OpenDatetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CloseDatetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Status", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				meta.Source = "MealAttendance";
				meta.Destination = "MealAttendance";
				
				meta.spInsert = "proc_MealAttendanceInsert";				
				meta.spUpdate = "proc_MealAttendanceUpdate";		
				meta.spDelete = "proc_MealAttendanceDelete";
				meta.spLoadAll = "proc_MealAttendanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_MealAttendanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MealAttendanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
