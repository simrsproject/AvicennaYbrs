/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/1/2021 4:31:18 PM
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
	abstract public class esTransaksiBkuDetailCollection : esEntityCollectionWAuditLog
	{
		public esTransaksiBkuDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransaksiBkuDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransaksiBkuDetailQuery query)
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
			this.InitQuery(query as esTransaksiBkuDetailQuery);
		}
		#endregion
		
		virtual public TransaksiBkuDetail DetachEntity(TransaksiBkuDetail entity)
		{
			return base.DetachEntity(entity) as TransaksiBkuDetail;
		}
		
		virtual public TransaksiBkuDetail AttachEntity(TransaksiBkuDetail entity)
		{
			return base.AttachEntity(entity) as TransaksiBkuDetail;
		}
		
		virtual public void Combine(TransaksiBkuDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransaksiBkuDetail this[int index]
		{
			get
			{
				return base[index] as TransaksiBkuDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransaksiBkuDetail);
		}
	}



	[Serializable]
	abstract public class esTransaksiBkuDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransaksiBkuDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransaksiBkuDetail()
		{

		}

		public esTransaksiBkuDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 id)
		{
			esTransaksiBkuDetailQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id",id);
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
						case "Id": this.str.Id = (string)value; break;							
						case "Nomor": this.str.Nomor = (string)value; break;							
						case "KodeRekening": this.str.KodeRekening = (string)value; break;							
						case "KodeItem": this.str.KodeItem = (string)value; break;							
						case "Memo": this.str.Memo = (string)value; break;							
						case "Nominal": this.str.Nominal = (string)value; break;							
						case "Posisi": this.str.Posisi = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
							break;
						
						case "KodeRekening":
						
							if (value == null || value is System.Int32)
								this.KodeRekening = (System.Int32?)value;
							break;
						
						case "Nominal":
						
							if (value == null || value is System.Decimal)
								this.Nominal = (System.Decimal?)value;
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
		/// Maps to TransaksiBkuDetail.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(TransaksiBkuDetailMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(TransaksiBkuDetailMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.Nomor
		/// </summary>
		virtual public System.String Nomor
		{
			get
			{
				return base.GetSystemString(TransaksiBkuDetailMetadata.ColumnNames.Nomor);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuDetailMetadata.ColumnNames.Nomor, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.KodeRekening
		/// </summary>
		virtual public System.Int32? KodeRekening
		{
			get
			{
				return base.GetSystemInt32(TransaksiBkuDetailMetadata.ColumnNames.KodeRekening);
			}
			
			set
			{
				base.SetSystemInt32(TransaksiBkuDetailMetadata.ColumnNames.KodeRekening, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.KodeItem
		/// </summary>
		virtual public System.String KodeItem
		{
			get
			{
				return base.GetSystemString(TransaksiBkuDetailMetadata.ColumnNames.KodeItem);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuDetailMetadata.ColumnNames.KodeItem, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.Memo
		/// </summary>
		virtual public System.String Memo
		{
			get
			{
				return base.GetSystemString(TransaksiBkuDetailMetadata.ColumnNames.Memo);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuDetailMetadata.ColumnNames.Memo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.Nominal
		/// </summary>
		virtual public System.Decimal? Nominal
		{
			get
			{
				return base.GetSystemDecimal(TransaksiBkuDetailMetadata.ColumnNames.Nominal);
			}
			
			set
			{
				base.SetSystemDecimal(TransaksiBkuDetailMetadata.ColumnNames.Nominal, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.Posisi
		/// </summary>
		virtual public System.String Posisi
		{
			get
			{
				return base.GetSystemString(TransaksiBkuDetailMetadata.ColumnNames.Posisi);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuDetailMetadata.ColumnNames.Posisi, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransaksiBkuDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransaksiBkuDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBkuDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransaksiBkuDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTransaksiBkuDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
				}
			}
				
			public System.String Nomor
			{
				get
				{
					System.String data = entity.Nomor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nomor = null;
					else entity.Nomor = Convert.ToString(value);
				}
			}
				
			public System.String KodeRekening
			{
				get
				{
					System.Int32? data = entity.KodeRekening;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeRekening = null;
					else entity.KodeRekening = Convert.ToInt32(value);
				}
			}
				
			public System.String KodeItem
			{
				get
				{
					System.String data = entity.KodeItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeItem = null;
					else entity.KodeItem = Convert.ToString(value);
				}
			}
				
			public System.String Memo
			{
				get
				{
					System.String data = entity.Memo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Memo = null;
					else entity.Memo = Convert.ToString(value);
				}
			}
				
			public System.String Nominal
			{
				get
				{
					System.Decimal? data = entity.Nominal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nominal = null;
					else entity.Nominal = Convert.ToDecimal(value);
				}
			}
				
			public System.String Posisi
			{
				get
				{
					System.String data = entity.Posisi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Posisi = null;
					else entity.Posisi = Convert.ToString(value);
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
			

			private esTransaksiBkuDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransaksiBkuDetailQuery query)
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
				throw new Exception("esTransaksiBkuDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esTransaksiBkuDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransaksiBkuDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Nomor
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.Nomor, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeRekening
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.KodeRekening, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KodeItem
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.KodeItem, esSystemType.String);
			}
		} 
		
		public esQueryItem Memo
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.Memo, esSystemType.String);
			}
		} 
		
		public esQueryItem Nominal
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.Nominal, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Posisi
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.Posisi, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransaksiBkuDetailCollection")]
	public partial class TransaksiBkuDetailCollection : esTransaksiBkuDetailCollection, IEnumerable<TransaksiBkuDetail>
	{
		public TransaksiBkuDetailCollection()
		{

		}
		
		public static implicit operator List<TransaksiBkuDetail>(TransaksiBkuDetailCollection coll)
		{
			List<TransaksiBkuDetail> list = new List<TransaksiBkuDetail>();
			
			foreach (TransaksiBkuDetail emp in coll)
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
				return  TransaksiBkuDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransaksiBkuDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransaksiBkuDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransaksiBkuDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransaksiBkuDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransaksiBkuDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransaksiBkuDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransaksiBkuDetail AddNew()
		{
			TransaksiBkuDetail entity = base.AddNewEntity() as TransaksiBkuDetail;
			
			return entity;
		}

		public TransaksiBkuDetail FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as TransaksiBkuDetail;
		}


		#region IEnumerable<TransaksiBkuDetail> Members

		IEnumerator<TransaksiBkuDetail> IEnumerable<TransaksiBkuDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransaksiBkuDetail;
			}
		}

		#endregion
		
		private TransaksiBkuDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransaksiBkuDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransaksiBkuDetail ({Id})")]
	[Serializable]
	public partial class TransaksiBkuDetail : esTransaksiBkuDetail
	{
		public TransaksiBkuDetail()
		{

		}
	
		public TransaksiBkuDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransaksiBkuDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esTransaksiBkuDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransaksiBkuDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransaksiBkuDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransaksiBkuDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransaksiBkuDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransaksiBkuDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransaksiBkuDetailQuery : esTransaksiBkuDetailQuery
	{
		public TransaksiBkuDetailQuery()
		{

		}		
		
		public TransaksiBkuDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransaksiBkuDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransaksiBkuDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransaksiBkuDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.Nomor, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.Nomor;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.KodeRekening, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.KodeRekening;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.KodeItem, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.KodeItem;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.Memo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.Memo;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.Nominal, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.Nominal;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.Posisi, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.Posisi;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuDetailMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransaksiBkuDetailMetadata Meta()
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
			 public const string Id = "Id";
			 public const string Nomor = "Nomor";
			 public const string KodeRekening = "KodeRekening";
			 public const string KodeItem = "KodeItem";
			 public const string Memo = "Memo";
			 public const string Nominal = "Nominal";
			 public const string Posisi = "Posisi";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Nomor = "Nomor";
			 public const string KodeRekening = "KodeRekening";
			 public const string KodeItem = "KodeItem";
			 public const string Memo = "Memo";
			 public const string Nominal = "Nominal";
			 public const string Posisi = "Posisi";
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
			lock (typeof(TransaksiBkuDetailMetadata))
			{
				if(TransaksiBkuDetailMetadata.mapDelegates == null)
				{
					TransaksiBkuDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransaksiBkuDetailMetadata.meta == null)
				{
					TransaksiBkuDetailMetadata.meta = new TransaksiBkuDetailMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Nomor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeRekening", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KodeItem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Memo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nominal", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Posisi", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TransaksiBkuDetail";
				meta.Destination = "TransaksiBkuDetail";
				
				meta.spInsert = "proc_TransaksiBkuDetailInsert";				
				meta.spUpdate = "proc_TransaksiBkuDetailUpdate";		
				meta.spDelete = "proc_TransaksiBkuDetailDelete";
				meta.spLoadAll = "proc_TransaksiBkuDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransaksiBkuDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransaksiBkuDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
