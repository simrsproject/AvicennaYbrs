/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:21 PM
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
	abstract public class esPaymentMethodCollection : esEntityCollectionWAuditLog
	{
		public esPaymentMethodCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PaymentMethodCollection";
		}

		#region Query Logic
		protected void InitQuery(esPaymentMethodQuery query)
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
			this.InitQuery(query as esPaymentMethodQuery);
		}
		#endregion
		
		virtual public PaymentMethod DetachEntity(PaymentMethod entity)
		{
			return base.DetachEntity(entity) as PaymentMethod;
		}
		
		virtual public PaymentMethod AttachEntity(PaymentMethod entity)
		{
			return base.AttachEntity(entity) as PaymentMethod;
		}
		
		virtual public void Combine(PaymentMethodCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PaymentMethod this[int index]
		{
			get
			{
				return base[index] as PaymentMethod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PaymentMethod);
		}
	}



	[Serializable]
	abstract public class esPaymentMethod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPaymentMethodQuery GetDynamicQuery()
		{
			return null;
		}

		public esPaymentMethod()
		{

		}

		public esPaymentMethod(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRPaymentTypeID, System.String sRPaymentMethodID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRPaymentTypeID, sRPaymentMethodID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRPaymentTypeID, sRPaymentMethodID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRPaymentTypeID, System.String sRPaymentMethodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRPaymentTypeID, sRPaymentMethodID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRPaymentTypeID, sRPaymentMethodID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRPaymentTypeID, System.String sRPaymentMethodID)
		{
			esPaymentMethodQuery query = this.GetDynamicQuery();
			query.Where(query.SRPaymentTypeID == sRPaymentTypeID, query.SRPaymentMethodID == sRPaymentMethodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRPaymentTypeID, System.String sRPaymentMethodID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRPaymentTypeID",sRPaymentTypeID);			parms.Add("SRPaymentMethodID",sRPaymentMethodID);
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
						case "SRPaymentTypeID": this.str.SRPaymentTypeID = (string)value; break;							
						case "SRPaymentMethodID": this.str.SRPaymentMethodID = (string)value; break;							
						case "PaymentMethodName": this.str.PaymentMethodName = (string)value; break;							
						case "ChartOfAccountID": this.str.ChartOfAccountID = (string)value; break;							
						case "SubledgerID": this.str.SubledgerID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ChartOfAccountID":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountID = (System.Int32?)value;
							break;
						
						case "SubledgerID":
						
							if (value == null || value is System.Int32)
								this.SubledgerID = (System.Int32?)value;
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
		/// Maps to PaymentMethod.SRPaymentTypeID
		/// </summary>
		virtual public System.String SRPaymentTypeID
		{
			get
			{
				return base.GetSystemString(PaymentMethodMetadata.ColumnNames.SRPaymentTypeID);
			}
			
			set
			{
				base.SetSystemString(PaymentMethodMetadata.ColumnNames.SRPaymentTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to PaymentMethod.SRPaymentMethodID
		/// </summary>
		virtual public System.String SRPaymentMethodID
		{
			get
			{
				return base.GetSystemString(PaymentMethodMetadata.ColumnNames.SRPaymentMethodID);
			}
			
			set
			{
				base.SetSystemString(PaymentMethodMetadata.ColumnNames.SRPaymentMethodID, value);
			}
		}
		
		/// <summary>
		/// Maps to PaymentMethod.PaymentMethodName
		/// </summary>
		virtual public System.String PaymentMethodName
		{
			get
			{
				return base.GetSystemString(PaymentMethodMetadata.ColumnNames.PaymentMethodName);
			}
			
			set
			{
				base.SetSystemString(PaymentMethodMetadata.ColumnNames.PaymentMethodName, value);
			}
		}
		
		/// <summary>
		/// Maps to PaymentMethod.ChartOfAccountID
		/// </summary>
		virtual public System.Int32? ChartOfAccountID
		{
			get
			{
				return base.GetSystemInt32(PaymentMethodMetadata.ColumnNames.ChartOfAccountID);
			}
			
			set
			{
				base.SetSystemInt32(PaymentMethodMetadata.ColumnNames.ChartOfAccountID, value);
			}
		}
		
		/// <summary>
		/// Maps to PaymentMethod.SubledgerID
		/// </summary>
		virtual public System.Int32? SubledgerID
		{
			get
			{
				return base.GetSystemInt32(PaymentMethodMetadata.ColumnNames.SubledgerID);
			}
			
			set
			{
				base.SetSystemInt32(PaymentMethodMetadata.ColumnNames.SubledgerID, value);
			}
		}
		
		/// <summary>
		/// Maps to PaymentMethod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PaymentMethodMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PaymentMethodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PaymentMethod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PaymentMethodMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PaymentMethodMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPaymentMethod entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRPaymentTypeID
			{
				get
				{
					System.String data = entity.SRPaymentTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentTypeID = null;
					else entity.SRPaymentTypeID = Convert.ToString(value);
				}
			}
				
			public System.String SRPaymentMethodID
			{
				get
				{
					System.String data = entity.SRPaymentMethodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentMethodID = null;
					else entity.SRPaymentMethodID = Convert.ToString(value);
				}
			}
				
			public System.String PaymentMethodName
			{
				get
				{
					System.String data = entity.PaymentMethodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentMethodName = null;
					else entity.PaymentMethodName = Convert.ToString(value);
				}
			}
				
			public System.String ChartOfAccountID
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountID = null;
					else entity.ChartOfAccountID = Convert.ToInt32(value);
				}
			}
				
			public System.String SubledgerID
			{
				get
				{
					System.Int32? data = entity.SubledgerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerID = null;
					else entity.SubledgerID = Convert.ToInt32(value);
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
			

			private esPaymentMethod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPaymentMethodQuery query)
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
				throw new Exception("esPaymentMethod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PaymentMethod : esPaymentMethod
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
	abstract public class esPaymentMethodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PaymentMethodMetadata.Meta();
			}
		}	
		

		public esQueryItem SRPaymentTypeID
		{
			get
			{
				return new esQueryItem(this, PaymentMethodMetadata.ColumnNames.SRPaymentTypeID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPaymentMethodID
		{
			get
			{
				return new esQueryItem(this, PaymentMethodMetadata.ColumnNames.SRPaymentMethodID, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentMethodName
		{
			get
			{
				return new esQueryItem(this, PaymentMethodMetadata.ColumnNames.PaymentMethodName, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountID
		{
			get
			{
				return new esQueryItem(this, PaymentMethodMetadata.ColumnNames.ChartOfAccountID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerID
		{
			get
			{
				return new esQueryItem(this, PaymentMethodMetadata.ColumnNames.SubledgerID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PaymentMethodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PaymentMethodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PaymentMethodCollection")]
	public partial class PaymentMethodCollection : esPaymentMethodCollection, IEnumerable<PaymentMethod>
	{
		public PaymentMethodCollection()
		{

		}
		
		public static implicit operator List<PaymentMethod>(PaymentMethodCollection coll)
		{
			List<PaymentMethod> list = new List<PaymentMethod>();
			
			foreach (PaymentMethod emp in coll)
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
				return  PaymentMethodMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PaymentMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PaymentMethod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PaymentMethod();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PaymentMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentMethodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PaymentMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PaymentMethod AddNew()
		{
			PaymentMethod entity = base.AddNewEntity() as PaymentMethod;
			
			return entity;
		}

		public PaymentMethod FindByPrimaryKey(System.String sRPaymentTypeID, System.String sRPaymentMethodID)
		{
			return base.FindByPrimaryKey(sRPaymentTypeID, sRPaymentMethodID) as PaymentMethod;
		}


		#region IEnumerable<PaymentMethod> Members

		IEnumerator<PaymentMethod> IEnumerable<PaymentMethod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PaymentMethod;
			}
		}

		#endregion
		
		private PaymentMethodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PaymentMethod' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PaymentMethod ({SRPaymentTypeID},{SRPaymentMethodID})")]
	[Serializable]
	public partial class PaymentMethod : esPaymentMethod
	{
		public PaymentMethod()
		{

		}
	
		public PaymentMethod(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PaymentMethodMetadata.Meta();
			}
		}
		
		
		
		override protected esPaymentMethodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PaymentMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PaymentMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentMethodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PaymentMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PaymentMethodQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PaymentMethodQuery : esPaymentMethodQuery
	{
		public PaymentMethodQuery()
		{

		}		
		
		public PaymentMethodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PaymentMethodQuery";
        }
		
			
	}


	[Serializable]
	public partial class PaymentMethodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PaymentMethodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PaymentMethodMetadata.ColumnNames.SRPaymentTypeID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentMethodMetadata.PropertyNames.SRPaymentTypeID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PaymentMethodMetadata.ColumnNames.SRPaymentMethodID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentMethodMetadata.PropertyNames.SRPaymentMethodID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PaymentMethodMetadata.ColumnNames.PaymentMethodName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentMethodMetadata.PropertyNames.PaymentMethodName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(PaymentMethodMetadata.ColumnNames.ChartOfAccountID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PaymentMethodMetadata.PropertyNames.ChartOfAccountID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PaymentMethodMetadata.ColumnNames.SubledgerID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PaymentMethodMetadata.PropertyNames.SubledgerID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PaymentMethodMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PaymentMethodMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PaymentMethodMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentMethodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PaymentMethodMetadata Meta()
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
			 public const string SRPaymentTypeID = "SRPaymentTypeID";
			 public const string SRPaymentMethodID = "SRPaymentMethodID";
			 public const string PaymentMethodName = "PaymentMethodName";
			 public const string ChartOfAccountID = "ChartOfAccountID";
			 public const string SubledgerID = "SubledgerID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRPaymentTypeID = "SRPaymentTypeID";
			 public const string SRPaymentMethodID = "SRPaymentMethodID";
			 public const string PaymentMethodName = "PaymentMethodName";
			 public const string ChartOfAccountID = "ChartOfAccountID";
			 public const string SubledgerID = "SubledgerID";
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
			lock (typeof(PaymentMethodMetadata))
			{
				if(PaymentMethodMetadata.mapDelegates == null)
				{
					PaymentMethodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PaymentMethodMetadata.meta == null)
				{
					PaymentMethodMetadata.meta = new PaymentMethodMetadata();
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
				

				meta.AddTypeMap("SRPaymentTypeID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethodID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("PaymentMethodName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PaymentMethod";
				meta.Destination = "PaymentMethod";
				
				meta.spInsert = "proc_PaymentMethodInsert";				
				meta.spUpdate = "proc_PaymentMethodUpdate";		
				meta.spDelete = "proc_PaymentMethodDelete";
				meta.spLoadAll = "proc_PaymentMethodLoadAll";
				meta.spLoadByPrimaryKey = "proc_PaymentMethodLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PaymentMethodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
