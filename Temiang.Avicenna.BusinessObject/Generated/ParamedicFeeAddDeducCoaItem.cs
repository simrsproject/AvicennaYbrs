/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/12/2017 3:43:18 AM
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
	abstract public class esParamedicFeeAddDeducCoaItemCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeAddDeducCoaItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeAddDeducCoaItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeAddDeducCoaItemQuery query)
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
			this.InitQuery(query as esParamedicFeeAddDeducCoaItemQuery);
		}
		#endregion
		
		virtual public ParamedicFeeAddDeducCoaItem DetachEntity(ParamedicFeeAddDeducCoaItem entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeAddDeducCoaItem;
		}
		
		virtual public ParamedicFeeAddDeducCoaItem AttachEntity(ParamedicFeeAddDeducCoaItem entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeAddDeducCoaItem;
		}
		
		virtual public void Combine(ParamedicFeeAddDeducCoaItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeAddDeducCoaItem this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeAddDeducCoaItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeAddDeducCoaItem);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeAddDeducCoaItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeAddDeducCoaItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeAddDeducCoaItem()
		{

		}

		public esParamedicFeeAddDeducCoaItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 listItemId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(listItemId);
			else
				return LoadByPrimaryKeyStoredProcedure(listItemId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 listItemId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(listItemId);
			else
				return LoadByPrimaryKeyStoredProcedure(listItemId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 listItemId)
		{
			esParamedicFeeAddDeducCoaItemQuery query = this.GetDynamicQuery();
			query.Where(query.ListItemId == listItemId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 listItemId)
		{
			esParameters parms = new esParameters();
			parms.Add("ListItemId",listItemId);
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
						case "ListItemId": this.str.ListItemId = (string)value; break;							
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;							
						case "SubledgerId": this.str.SubledgerId = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserId": this.str.LastUpdateByUserId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ListItemId":
						
							if (value == null || value is System.Int32)
								this.ListItemId = (System.Int32?)value;
							break;
						
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						
						case "SubledgerId":
						
							if (value == null || value is System.Int32)
								this.SubledgerId = (System.Int32?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeAddDeducCoaItem.ListItemId
		/// </summary>
		virtual public System.Int32? ListItemId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ListItemId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ListItemId, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeAddDeducCoaItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeAddDeducCoaItem.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeAddDeducCoaItem.SubledgerId
		/// </summary>
		virtual public System.Int32? SubledgerId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.SubledgerId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.SubledgerId, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeAddDeducCoaItem.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeAddDeducCoaItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeAddDeducCoaItem.LastUpdateByUserId
		/// </summary>
		virtual public System.String LastUpdateByUserId
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateByUserId);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateByUserId, value);
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
			public esStrings(esParamedicFeeAddDeducCoaItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ListItemId
			{
				get
				{
					System.Int32? data = entity.ListItemId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ListItemId = null;
					else entity.ListItemId = Convert.ToInt32(value);
				}
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
				
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
				
			public System.String SubledgerId
			{
				get
				{
					System.Int32? data = entity.SubledgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerId = null;
					else entity.SubledgerId = Convert.ToInt32(value);
				}
			}
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
				
			public System.String LastUpdateByUserId
			{
				get
				{
					System.String data = entity.LastUpdateByUserId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserId = null;
					else entity.LastUpdateByUserId = Convert.ToString(value);
				}
			}
			

			private esParamedicFeeAddDeducCoaItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeAddDeducCoaItemQuery query)
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
				throw new Exception("esParamedicFeeAddDeducCoaItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esParamedicFeeAddDeducCoaItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeAddDeducCoaItemMetadata.Meta();
			}
		}	
		

		public esQueryItem ListItemId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ListItemId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.SubledgerId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateByUserId, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeAddDeducCoaItemCollection")]
	public partial class ParamedicFeeAddDeducCoaItemCollection : esParamedicFeeAddDeducCoaItemCollection, IEnumerable<ParamedicFeeAddDeducCoaItem>
	{
		public ParamedicFeeAddDeducCoaItemCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeAddDeducCoaItem>(ParamedicFeeAddDeducCoaItemCollection coll)
		{
			List<ParamedicFeeAddDeducCoaItem> list = new List<ParamedicFeeAddDeducCoaItem>();
			
			foreach (ParamedicFeeAddDeducCoaItem emp in coll)
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
				return  ParamedicFeeAddDeducCoaItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeAddDeducCoaItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeAddDeducCoaItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeAddDeducCoaItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeAddDeducCoaItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeAddDeducCoaItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeAddDeducCoaItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeAddDeducCoaItem AddNew()
		{
			ParamedicFeeAddDeducCoaItem entity = base.AddNewEntity() as ParamedicFeeAddDeducCoaItem;
			
			return entity;
		}

		public ParamedicFeeAddDeducCoaItem FindByPrimaryKey(System.Int32 listItemId)
		{
			return base.FindByPrimaryKey(listItemId) as ParamedicFeeAddDeducCoaItem;
		}


		#region IEnumerable<ParamedicFeeAddDeducCoaItem> Members

		IEnumerator<ParamedicFeeAddDeducCoaItem> IEnumerable<ParamedicFeeAddDeducCoaItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeAddDeducCoaItem;
			}
		}

		#endregion
		
		private ParamedicFeeAddDeducCoaItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeAddDeducCoaItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeAddDeducCoaItem ({ListItemId})")]
	[Serializable]
	public partial class ParamedicFeeAddDeducCoaItem : esParamedicFeeAddDeducCoaItem
	{
		public ParamedicFeeAddDeducCoaItem()
		{

		}
	
		public ParamedicFeeAddDeducCoaItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeAddDeducCoaItemMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeAddDeducCoaItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeAddDeducCoaItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeAddDeducCoaItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeAddDeducCoaItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeAddDeducCoaItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeAddDeducCoaItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeAddDeducCoaItemQuery : esParamedicFeeAddDeducCoaItemQuery
	{
		public ParamedicFeeAddDeducCoaItemQuery()
		{

		}		
		
		public ParamedicFeeAddDeducCoaItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeAddDeducCoaItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeAddDeducCoaItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeAddDeducCoaItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ListItemId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeAddDeducCoaItemMetadata.PropertyNames.ListItemId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducCoaItemMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeAddDeducCoaItemMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.SubledgerId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeAddDeducCoaItemMetadata.PropertyNames.SubledgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeAddDeducCoaItemMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeAddDeducCoaItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeAddDeducCoaItemMetadata.ColumnNames.LastUpdateByUserId, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducCoaItemMetadata.PropertyNames.LastUpdateByUserId;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeAddDeducCoaItemMetadata Meta()
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
			 public const string ListItemId = "ListItemId";
			 public const string TransactionNo = "TransactionNo";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubledgerId = "SubledgerId";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserId = "LastUpdateByUserId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ListItemId = "ListItemId";
			 public const string TransactionNo = "TransactionNo";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubledgerId = "SubledgerId";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserId = "LastUpdateByUserId";
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
			lock (typeof(ParamedicFeeAddDeducCoaItemMetadata))
			{
				if(ParamedicFeeAddDeducCoaItemMetadata.mapDelegates == null)
				{
					ParamedicFeeAddDeducCoaItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeAddDeducCoaItemMetadata.meta == null)
				{
					ParamedicFeeAddDeducCoaItemMetadata.meta = new ParamedicFeeAddDeducCoaItemMetadata();
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
				

				meta.AddTypeMap("ListItemId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserId", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeAddDeducCoaItem";
				meta.Destination = "ParamedicFeeAddDeducCoaItem";
				
				meta.spInsert = "proc_ParamedicFeeAddDeducCoaItemInsert";				
				meta.spUpdate = "proc_ParamedicFeeAddDeducCoaItemUpdate";		
				meta.spDelete = "proc_ParamedicFeeAddDeducCoaItemDelete";
				meta.spLoadAll = "proc_ParamedicFeeAddDeducCoaItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeAddDeducCoaItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeAddDeducCoaItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
