/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:27 PM
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
	abstract public class esUserHostPrinterCollection : esEntityCollectionWAuditLog
	{
		public esUserHostPrinterCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "UserHostPrinterCollection";
		}

		#region Query Logic
		protected void InitQuery(esUserHostPrinterQuery query)
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
			this.InitQuery(query as esUserHostPrinterQuery);
		}
		#endregion
		
		virtual public UserHostPrinter DetachEntity(UserHostPrinter entity)
		{
			return base.DetachEntity(entity) as UserHostPrinter;
		}
		
		virtual public UserHostPrinter AttachEntity(UserHostPrinter entity)
		{
			return base.AttachEntity(entity) as UserHostPrinter;
		}
		
		virtual public void Combine(UserHostPrinterCollection collection)
		{
			base.Combine(collection);
		}
		
		new public UserHostPrinter this[int index]
		{
			get
			{
				return base[index] as UserHostPrinter;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(UserHostPrinter);
		}
	}



	[Serializable]
	abstract public class esUserHostPrinter : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esUserHostPrinterQuery GetDynamicQuery()
		{
			return null;
		}

		public esUserHostPrinter()
		{

		}

		public esUserHostPrinter(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String userHostName)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userHostName);
			else
				return LoadByPrimaryKeyStoredProcedure(userHostName);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String userHostName)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userHostName);
			else
				return LoadByPrimaryKeyStoredProcedure(userHostName);
		}

		private bool LoadByPrimaryKeyDynamic(System.String userHostName)
		{
			esUserHostPrinterQuery query = this.GetDynamicQuery();
			query.Where(query.UserHostName == userHostName);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String userHostName)
		{
			esParameters parms = new esParameters();
			parms.Add("UserHostName",userHostName);
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
						case "UserHostName": this.str.UserHostName = (string)value; break;							
						case "PrinterID": this.str.PrinterID = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to UserHostPrinter.UserHostName
		/// </summary>
		virtual public System.String UserHostName
		{
			get
			{
				return base.GetSystemString(UserHostPrinterMetadata.ColumnNames.UserHostName);
			}
			
			set
			{
				base.SetSystemString(UserHostPrinterMetadata.ColumnNames.UserHostName, value);
			}
		}
		
		/// <summary>
		/// Maps to UserHostPrinter.PrinterID
		/// </summary>
		virtual public System.String PrinterID
		{
			get
			{
				return base.GetSystemString(UserHostPrinterMetadata.ColumnNames.PrinterID);
			}
			
			set
			{
				base.SetSystemString(UserHostPrinterMetadata.ColumnNames.PrinterID, value);
			}
		}
		
		/// <summary>
		/// Maps to UserHostPrinter.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(UserHostPrinterMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(UserHostPrinterMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to UserHostPrinter.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserHostPrinterMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(UserHostPrinterMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to UserHostPrinter.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(UserHostPrinterMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(UserHostPrinterMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esUserHostPrinter entity)
			{
				this.entity = entity;
			}
			
	
			public System.String UserHostName
			{
				get
				{
					System.String data = entity.UserHostName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserHostName = null;
					else entity.UserHostName = Convert.ToString(value);
				}
			}
				
			public System.String PrinterID
			{
				get
				{
					System.String data = entity.PrinterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrinterID = null;
					else entity.PrinterID = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
			

			private esUserHostPrinter entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esUserHostPrinterQuery query)
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
				throw new Exception("esUserHostPrinter can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class UserHostPrinter : esUserHostPrinter
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
	abstract public class esUserHostPrinterQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return UserHostPrinterMetadata.Meta();
			}
		}	
		

		public esQueryItem UserHostName
		{
			get
			{
				return new esQueryItem(this, UserHostPrinterMetadata.ColumnNames.UserHostName, esSystemType.String);
			}
		} 
		
		public esQueryItem PrinterID
		{
			get
			{
				return new esQueryItem(this, UserHostPrinterMetadata.ColumnNames.PrinterID, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, UserHostPrinterMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, UserHostPrinterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, UserHostPrinterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("UserHostPrinterCollection")]
	public partial class UserHostPrinterCollection : esUserHostPrinterCollection, IEnumerable<UserHostPrinter>
	{
		public UserHostPrinterCollection()
		{

		}
		
		public static implicit operator List<UserHostPrinter>(UserHostPrinterCollection coll)
		{
			List<UserHostPrinter> list = new List<UserHostPrinter>();
			
			foreach (UserHostPrinter emp in coll)
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
				return  UserHostPrinterMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UserHostPrinterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new UserHostPrinter(row);
		}

		override protected esEntity CreateEntity()
		{
			return new UserHostPrinter();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public UserHostPrinterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserHostPrinterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(UserHostPrinterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public UserHostPrinter AddNew()
		{
			UserHostPrinter entity = base.AddNewEntity() as UserHostPrinter;
			
			return entity;
		}

		public UserHostPrinter FindByPrimaryKey(System.String userHostName)
		{
			return base.FindByPrimaryKey(userHostName) as UserHostPrinter;
		}


		#region IEnumerable<UserHostPrinter> Members

		IEnumerator<UserHostPrinter> IEnumerable<UserHostPrinter>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as UserHostPrinter;
			}
		}

		#endregion
		
		private UserHostPrinterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'UserHostPrinter' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("UserHostPrinter ({UserHostName})")]
	[Serializable]
	public partial class UserHostPrinter : esUserHostPrinter
	{
		public UserHostPrinter()
		{

		}
	
		public UserHostPrinter(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserHostPrinterMetadata.Meta();
			}
		}
		
		
		
		override protected esUserHostPrinterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UserHostPrinterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public UserHostPrinterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserHostPrinterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(UserHostPrinterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private UserHostPrinterQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class UserHostPrinterQuery : esUserHostPrinterQuery
	{
		public UserHostPrinterQuery()
		{

		}		
		
		public UserHostPrinterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "UserHostPrinterQuery";
        }
		
			
	}


	[Serializable]
	public partial class UserHostPrinterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserHostPrinterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(UserHostPrinterMetadata.ColumnNames.UserHostName, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = UserHostPrinterMetadata.PropertyNames.UserHostName;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(UserHostPrinterMetadata.ColumnNames.PrinterID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = UserHostPrinterMetadata.PropertyNames.PrinterID;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(UserHostPrinterMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = UserHostPrinterMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(UserHostPrinterMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserHostPrinterMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(UserHostPrinterMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = UserHostPrinterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public UserHostPrinterMetadata Meta()
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
			 public const string UserHostName = "UserHostName";
			 public const string PrinterID = "PrinterID";
			 public const string Description = "Description";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string UserHostName = "UserHostName";
			 public const string PrinterID = "PrinterID";
			 public const string Description = "Description";
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
			lock (typeof(UserHostPrinterMetadata))
			{
				if(UserHostPrinterMetadata.mapDelegates == null)
				{
					UserHostPrinterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (UserHostPrinterMetadata.meta == null)
				{
					UserHostPrinterMetadata.meta = new UserHostPrinterMetadata();
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
				

				meta.AddTypeMap("UserHostName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrinterID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "UserHostPrinter";
				meta.Destination = "UserHostPrinter";
				
				meta.spInsert = "proc_UserHostPrinterInsert";				
				meta.spUpdate = "proc_UserHostPrinterUpdate";		
				meta.spDelete = "proc_UserHostPrinterDelete";
				meta.spLoadAll = "proc_UserHostPrinterLoadAll";
				meta.spLoadByPrimaryKey = "proc_UserHostPrinterLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserHostPrinterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
