/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:20 PM
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
	abstract public class esParamedicFeePaymentDtCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeePaymentDtCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeePaymentDtCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentDtQuery query)
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
			this.InitQuery(query as esParamedicFeePaymentDtQuery);
		}
		#endregion
		
		virtual public ParamedicFeePaymentDt DetachEntity(ParamedicFeePaymentDt entity)
		{
			return base.DetachEntity(entity) as ParamedicFeePaymentDt;
		}
		
		virtual public ParamedicFeePaymentDt AttachEntity(ParamedicFeePaymentDt entity)
		{
			return base.AttachEntity(entity) as ParamedicFeePaymentDt;
		}
		
		virtual public void Combine(ParamedicFeePaymentDtCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeePaymentDt this[int index]
		{
			get
			{
				return base[index] as ParamedicFeePaymentDt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeePaymentDt);
		}
	}



	[Serializable]
	abstract public class esParamedicFeePaymentDt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeePaymentDtQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeePaymentDt()
		{

		}

		public esParamedicFeePaymentDt(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paymentNo, System.String verificationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, verificationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, verificationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paymentNo, System.String verificationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, verificationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, verificationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paymentNo, System.String verificationNo)
		{
			esParamedicFeePaymentDtQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo, query.VerificationNo == verificationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paymentNo, System.String verificationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo",paymentNo);			parms.Add("VerificationNo",verificationNo);
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
						case "PaymentNo": this.str.PaymentNo = (string)value; break;							
						case "VerificationNo": this.str.VerificationNo = (string)value; break;							
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
		/// Maps to ParamedicFeePaymentDt.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentDtMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentDtMetadata.ColumnNames.PaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeePaymentDt.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentDtMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentDtMetadata.ColumnNames.VerificationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeePaymentDt.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeePaymentDt.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeePaymentDt entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
				}
			}
				
			public System.String VerificationNo
			{
				get
				{
					System.String data = entity.VerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNo = null;
					else entity.VerificationNo = Convert.ToString(value);
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
			

			private esParamedicFeePaymentDt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentDtQuery query)
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
				throw new Exception("esParamedicFeePaymentDt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeePaymentDt : esParamedicFeePaymentDt
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
	abstract public class esParamedicFeePaymentDtQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentDtMetadata.Meta();
			}
		}	
		

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentDtMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentDtMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeePaymentDtCollection")]
	public partial class ParamedicFeePaymentDtCollection : esParamedicFeePaymentDtCollection, IEnumerable<ParamedicFeePaymentDt>
	{
		public ParamedicFeePaymentDtCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeePaymentDt>(ParamedicFeePaymentDtCollection coll)
		{
			List<ParamedicFeePaymentDt> list = new List<ParamedicFeePaymentDt>();
			
			foreach (ParamedicFeePaymentDt emp in coll)
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
				return  ParamedicFeePaymentDtMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeePaymentDt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeePaymentDt();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeePaymentDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentDtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeePaymentDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeePaymentDt AddNew()
		{
			ParamedicFeePaymentDt entity = base.AddNewEntity() as ParamedicFeePaymentDt;
			
			return entity;
		}

		public ParamedicFeePaymentDt FindByPrimaryKey(System.String paymentNo, System.String verificationNo)
		{
			return base.FindByPrimaryKey(paymentNo, verificationNo) as ParamedicFeePaymentDt;
		}


		#region IEnumerable<ParamedicFeePaymentDt> Members

		IEnumerator<ParamedicFeePaymentDt> IEnumerable<ParamedicFeePaymentDt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeePaymentDt;
			}
		}

		#endregion
		
		private ParamedicFeePaymentDtQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeePaymentDt' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeePaymentDt ({PaymentNo},{VerificationNo})")]
	[Serializable]
	public partial class ParamedicFeePaymentDt : esParamedicFeePaymentDt
	{
		public ParamedicFeePaymentDt()
		{

		}
	
		public ParamedicFeePaymentDt(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentDtMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeePaymentDtQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeePaymentDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentDtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeePaymentDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeePaymentDtQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeePaymentDtQuery : esParamedicFeePaymentDtQuery
	{
		public ParamedicFeePaymentDtQuery()
		{

		}		
		
		public ParamedicFeePaymentDtQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeePaymentDtQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeePaymentDtMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeePaymentDtMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeePaymentDtMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentDtMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeePaymentDtMetadata.ColumnNames.VerificationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentDtMetadata.PropertyNames.VerificationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentDtMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeePaymentDtMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentDtMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeePaymentDtMetadata Meta()
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
			 public const string PaymentNo = "PaymentNo";
			 public const string VerificationNo = "VerificationNo";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PaymentNo = "PaymentNo";
			 public const string VerificationNo = "VerificationNo";
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
			lock (typeof(ParamedicFeePaymentDtMetadata))
			{
				if(ParamedicFeePaymentDtMetadata.mapDelegates == null)
				{
					ParamedicFeePaymentDtMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeePaymentDtMetadata.meta == null)
				{
					ParamedicFeePaymentDtMetadata.meta = new ParamedicFeePaymentDtMetadata();
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
				

				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeePaymentDt";
				meta.Destination = "ParamedicFeePaymentDt";
				
				meta.spInsert = "proc_ParamedicFeePaymentDtInsert";				
				meta.spUpdate = "proc_ParamedicFeePaymentDtUpdate";		
				meta.spDelete = "proc_ParamedicFeePaymentDtDelete";
				meta.spLoadAll = "proc_ParamedicFeePaymentDtLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeePaymentDtLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeePaymentDtMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
