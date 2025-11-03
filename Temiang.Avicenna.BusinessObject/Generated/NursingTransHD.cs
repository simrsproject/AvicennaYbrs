/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/31/2015 7:48:39 AM
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
	abstract public class esNursingTransHDCollection : esEntityCollection
	{
		public esNursingTransHDCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NursingTransHDCollection";
		}

		#region Query Logic
		protected void InitQuery(esNursingTransHDQuery query)
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
			this.InitQuery(query as esNursingTransHDQuery);
		}
		#endregion
		
		virtual public NursingTransHD DetachEntity(NursingTransHD entity)
		{
			return base.DetachEntity(entity) as NursingTransHD;
		}
		
		virtual public NursingTransHD AttachEntity(NursingTransHD entity)
		{
			return base.AttachEntity(entity) as NursingTransHD;
		}
		
		virtual public void Combine(NursingTransHDCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingTransHD this[int index]
		{
			get
			{
				return base[index] as NursingTransHD;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingTransHD);
		}
	}



	[Serializable]
	abstract public class esNursingTransHD : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingTransHDQuery GetDynamicQuery()
		{
			return null;
		}

		public esNursingTransHD()
		{

		}

		public esNursingTransHD(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
		{
			esNursingTransHDQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "NursingTransDateTime": this.str.NursingTransDateTime = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;							
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "NursingTransDateTime":
						
							if (value == null || value is System.DateTime)
								this.NursingTransDateTime = (System.DateTime?)value;
							break;
						
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to NursingTransHD.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(NursingTransHDMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(NursingTransHDMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingTransHD.NursingTransDateTime
		/// </summary>
		virtual public System.DateTime? NursingTransDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingTransHDMetadata.ColumnNames.NursingTransDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingTransHDMetadata.ColumnNames.NursingTransDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingTransHD.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NursingTransHDMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NursingTransHDMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingTransHD.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingTransHDMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingTransHDMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingTransHD.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingTransHDMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingTransHDMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingTransHD.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingTransHDMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingTransHDMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingTransHD.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingTransHDMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingTransHDMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esNursingTransHD entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String NursingTransDateTime
			{
				get
				{
					System.DateTime? data = entity.NursingTransDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingTransDateTime = null;
					else entity.NursingTransDateTime = Convert.ToDateTime(value);
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
				
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
				}
			}
				
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
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
			

			private esNursingTransHD entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingTransHDQuery query)
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
				throw new Exception("esNursingTransHD can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esNursingTransHDQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NursingTransHDMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, NursingTransHDMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem NursingTransDateTime
		{
			get
			{
				return new esQueryItem(this, NursingTransHDMetadata.ColumnNames.NursingTransDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NursingTransHDMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingTransHDMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingTransHDMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingTransHDMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingTransHDMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingTransHDCollection")]
	public partial class NursingTransHDCollection : esNursingTransHDCollection, IEnumerable<NursingTransHD>
	{
		public NursingTransHDCollection()
		{

		}
		
		public static implicit operator List<NursingTransHD>(NursingTransHDCollection coll)
		{
			List<NursingTransHD> list = new List<NursingTransHD>();
			
			foreach (NursingTransHD emp in coll)
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
				return  NursingTransHDMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingTransHDQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingTransHD(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingTransHD();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NursingTransHDQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingTransHDQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NursingTransHDQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NursingTransHD AddNew()
		{
			NursingTransHD entity = base.AddNewEntity() as NursingTransHD;
			
			return entity;
		}

		public NursingTransHD FindByPrimaryKey(System.String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as NursingTransHD;
		}


		#region IEnumerable<NursingTransHD> Members

		IEnumerator<NursingTransHD> IEnumerable<NursingTransHD>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingTransHD;
			}
		}

		#endregion
		
		private NursingTransHDQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingTransHD' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NursingTransHD ({TransactionNo})")]
	[Serializable]
	public partial class NursingTransHD : esNursingTransHD
	{
		public NursingTransHD()
		{

		}
	
		public NursingTransHD(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingTransHDMetadata.Meta();
			}
		}
		
		
		
		override protected esNursingTransHDQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingTransHDQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NursingTransHDQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingTransHDQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NursingTransHDQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NursingTransHDQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NursingTransHDQuery : esNursingTransHDQuery
	{
		public NursingTransHDQuery()
		{

		}		
		
		public NursingTransHDQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NursingTransHDQuery";
        }
		
			
	}


	[Serializable]
	public partial class NursingTransHDMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingTransHDMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NursingTransHDMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingTransHDMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingTransHDMetadata.ColumnNames.NursingTransDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingTransHDMetadata.PropertyNames.NursingTransDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingTransHDMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingTransHDMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingTransHDMetadata.ColumnNames.CreateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingTransHDMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingTransHDMetadata.ColumnNames.CreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingTransHDMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingTransHDMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingTransHDMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingTransHDMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingTransHDMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NursingTransHDMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string NursingTransDateTime = "NursingTransDateTime";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string NursingTransDateTime = "NursingTransDateTime";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(NursingTransHDMetadata))
			{
				if(NursingTransHDMetadata.mapDelegates == null)
				{
					NursingTransHDMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingTransHDMetadata.meta == null)
				{
					NursingTransHDMetadata.meta = new NursingTransHDMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingTransDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "NursingTransHD";
				meta.Destination = "NursingTransHD";
				
				meta.spInsert = "proc_NursingTransHDInsert";				
				meta.spUpdate = "proc_NursingTransHDUpdate";		
				meta.spDelete = "proc_NursingTransHDDelete";
				meta.spLoadAll = "proc_NursingTransHDLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingTransHDLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingTransHDMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
