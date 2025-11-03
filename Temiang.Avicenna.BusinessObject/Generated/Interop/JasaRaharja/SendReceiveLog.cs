/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/28/2015 1:58:55 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.JasaRaharja
{

	[Serializable]
	abstract public class esSendReceiveLogCollection : esEntityCollectionWAuditLog
	{
		public esSendReceiveLogCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SendReceiveLogCollection";
		}

		#region Query Logic
		protected void InitQuery(esSendReceiveLogQuery query)
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
			this.InitQuery(query as esSendReceiveLogQuery);
		}
		#endregion
		
		virtual public SendReceiveLog DetachEntity(SendReceiveLog entity)
		{
			return base.DetachEntity(entity) as SendReceiveLog;
		}
		
		virtual public SendReceiveLog AttachEntity(SendReceiveLog entity)
		{
			return base.AttachEntity(entity) as SendReceiveLog;
		}
		
		virtual public void Combine(SendReceiveLogCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SendReceiveLog this[int index]
		{
			get
			{
				return base[index] as SendReceiveLog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SendReceiveLog);
		}
	}



	[Serializable]
	abstract public class esSendReceiveLog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSendReceiveLogQuery GetDynamicQuery()
		{
			return null;
		}

		public esSendReceiveLog()
		{

		}

		public esSendReceiveLog(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 logId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(logId);
			else
				return LoadByPrimaryKeyStoredProcedure(logId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 logId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(logId);
			else
				return LoadByPrimaryKeyStoredProcedure(logId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 logId)
		{
			esSendReceiveLogQuery query = this.GetDynamicQuery();
			query.Where(query.LogId == logId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 logId)
		{
			esParameters parms = new esParameters();
			parms.Add("LogId",logId);
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
						case "LogId": this.str.LogId = (string)value; break;							
						case "OperationType": this.str.OperationType = (string)value; break;							
						case "SendDateTime": this.str.SendDateTime = (string)value; break;							
						case "SendParameter": this.str.SendParameter = (string)value; break;							
						case "ReceiveResult": this.str.ReceiveResult = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "IsOperationSuccess": this.str.IsOperationSuccess = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LogId":
						
							if (value == null || value is System.Int32)
								this.LogId = (System.Int32?)value;
							break;
						
						case "OperationType":
						
							if (value == null || value is System.Byte)
								this.OperationType = (System.Byte?)value;
							break;
						
						case "SendDateTime":
						
							if (value == null || value is System.DateTime)
								this.SendDateTime = (System.DateTime?)value;
							break;
						
						case "IsOperationSuccess":
						
							if (value == null || value is System.Boolean)
								this.IsOperationSuccess = (System.Boolean?)value;
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
		/// Maps to SendReceiveLog.LogId
		/// </summary>
		virtual public System.Int32? LogId
		{
			get
			{
				return base.GetSystemInt32(SendReceiveLogMetadata.ColumnNames.LogId);
			}
			
			set
			{
				base.SetSystemInt32(SendReceiveLogMetadata.ColumnNames.LogId, value);
			}
		}
		
		/// <summary>
		/// Maps to SendReceiveLog.OperationType
		/// </summary>
		virtual public System.Byte? OperationType
		{
			get
			{
				return base.GetSystemByte(SendReceiveLogMetadata.ColumnNames.OperationType);
			}
			
			set
			{
				base.SetSystemByte(SendReceiveLogMetadata.ColumnNames.OperationType, value);
			}
		}
		
		/// <summary>
		/// Maps to SendReceiveLog.SendDateTime
		/// </summary>
		virtual public System.DateTime? SendDateTime
		{
			get
			{
				return base.GetSystemDateTime(SendReceiveLogMetadata.ColumnNames.SendDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SendReceiveLogMetadata.ColumnNames.SendDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SendReceiveLog.SendParameter
		/// </summary>
		virtual public System.String SendParameter
		{
			get
			{
				return base.GetSystemString(SendReceiveLogMetadata.ColumnNames.SendParameter);
			}
			
			set
			{
				base.SetSystemString(SendReceiveLogMetadata.ColumnNames.SendParameter, value);
			}
		}
		
		/// <summary>
		/// Maps to SendReceiveLog.ReceiveResult
		/// </summary>
		virtual public System.String ReceiveResult
		{
			get
			{
				return base.GetSystemString(SendReceiveLogMetadata.ColumnNames.ReceiveResult);
			}
			
			set
			{
				base.SetSystemString(SendReceiveLogMetadata.ColumnNames.ReceiveResult, value);
			}
		}
		
		/// <summary>
		/// Maps to SendReceiveLog.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(SendReceiveLogMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(SendReceiveLogMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to SendReceiveLog.IsOperationSuccess
		/// </summary>
		virtual public System.Boolean? IsOperationSuccess
		{
			get
			{
				return base.GetSystemBoolean(SendReceiveLogMetadata.ColumnNames.IsOperationSuccess);
			}
			
			set
			{
				base.SetSystemBoolean(SendReceiveLogMetadata.ColumnNames.IsOperationSuccess, value);
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
			public esStrings(esSendReceiveLog entity)
			{
				this.entity = entity;
			}
			
	
			public System.String LogId
			{
				get
				{
					System.Int32? data = entity.LogId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LogId = null;
					else entity.LogId = Convert.ToInt32(value);
				}
			}
				
			public System.String OperationType
			{
				get
				{
					System.Byte? data = entity.OperationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationType = null;
					else entity.OperationType = Convert.ToByte(value);
				}
			}
				
			public System.String SendDateTime
			{
				get
				{
					System.DateTime? data = entity.SendDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SendDateTime = null;
					else entity.SendDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String SendParameter
			{
				get
				{
					System.String data = entity.SendParameter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SendParameter = null;
					else entity.SendParameter = Convert.ToString(value);
				}
			}
				
			public System.String ReceiveResult
			{
				get
				{
					System.String data = entity.ReceiveResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceiveResult = null;
					else entity.ReceiveResult = Convert.ToString(value);
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
				
			public System.String IsOperationSuccess
			{
				get
				{
					System.Boolean? data = entity.IsOperationSuccess;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOperationSuccess = null;
					else entity.IsOperationSuccess = Convert.ToBoolean(value);
				}
			}
			

			private esSendReceiveLog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSendReceiveLogQuery query)
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
				throw new Exception("esSendReceiveLog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esSendReceiveLogQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SendReceiveLogMetadata.Meta();
			}
		}	
		

		public esQueryItem LogId
		{
			get
			{
				return new esQueryItem(this, SendReceiveLogMetadata.ColumnNames.LogId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OperationType
		{
			get
			{
				return new esQueryItem(this, SendReceiveLogMetadata.ColumnNames.OperationType, esSystemType.Byte);
			}
		} 
		
		public esQueryItem SendDateTime
		{
			get
			{
				return new esQueryItem(this, SendReceiveLogMetadata.ColumnNames.SendDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SendParameter
		{
			get
			{
				return new esQueryItem(this, SendReceiveLogMetadata.ColumnNames.SendParameter, esSystemType.String);
			}
		} 
		
		public esQueryItem ReceiveResult
		{
			get
			{
				return new esQueryItem(this, SendReceiveLogMetadata.ColumnNames.ReceiveResult, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, SendReceiveLogMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsOperationSuccess
		{
			get
			{
				return new esQueryItem(this, SendReceiveLogMetadata.ColumnNames.IsOperationSuccess, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SendReceiveLogCollection")]
	public partial class SendReceiveLogCollection : esSendReceiveLogCollection, IEnumerable<SendReceiveLog>
	{
		public SendReceiveLogCollection()
		{

		}
		
		public static implicit operator List<SendReceiveLog>(SendReceiveLogCollection coll)
		{
			List<SendReceiveLog> list = new List<SendReceiveLog>();
			
			foreach (SendReceiveLog emp in coll)
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
				return  SendReceiveLogMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SendReceiveLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SendReceiveLog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SendReceiveLog();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SendReceiveLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SendReceiveLogQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SendReceiveLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SendReceiveLog AddNew()
		{
			SendReceiveLog entity = base.AddNewEntity() as SendReceiveLog;
			
			return entity;
		}

		public SendReceiveLog FindByPrimaryKey(System.Int32 logId)
		{
			return base.FindByPrimaryKey(logId) as SendReceiveLog;
		}


		#region IEnumerable<SendReceiveLog> Members

		IEnumerator<SendReceiveLog> IEnumerable<SendReceiveLog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SendReceiveLog;
			}
		}

		#endregion
		
		private SendReceiveLogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SendReceiveLog' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SendReceiveLog ({LogId})")]
	[Serializable]
	public partial class SendReceiveLog : esSendReceiveLog
	{
		public SendReceiveLog()
		{

		}
	
		public SendReceiveLog(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SendReceiveLogMetadata.Meta();
			}
		}
		
		
		
		override protected esSendReceiveLogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SendReceiveLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SendReceiveLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SendReceiveLogQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SendReceiveLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SendReceiveLogQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SendReceiveLogQuery : esSendReceiveLogQuery
	{
		public SendReceiveLogQuery()
		{

		}		
		
		public SendReceiveLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SendReceiveLogQuery";
        }
		
			
	}


	[Serializable]
	public partial class SendReceiveLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SendReceiveLogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SendReceiveLogMetadata.ColumnNames.LogId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SendReceiveLogMetadata.PropertyNames.LogId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SendReceiveLogMetadata.ColumnNames.OperationType, 1, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = SendReceiveLogMetadata.PropertyNames.OperationType;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SendReceiveLogMetadata.ColumnNames.SendDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SendReceiveLogMetadata.PropertyNames.SendDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SendReceiveLogMetadata.ColumnNames.SendParameter, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SendReceiveLogMetadata.PropertyNames.SendParameter;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SendReceiveLogMetadata.ColumnNames.ReceiveResult, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SendReceiveLogMetadata.PropertyNames.ReceiveResult;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SendReceiveLogMetadata.ColumnNames.RegistrationNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SendReceiveLogMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SendReceiveLogMetadata.ColumnNames.IsOperationSuccess, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SendReceiveLogMetadata.PropertyNames.IsOperationSuccess;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SendReceiveLogMetadata Meta()
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
			 public const string LogId = "LogId";
			 public const string OperationType = "OperationType";
			 public const string SendDateTime = "SendDateTime";
			 public const string SendParameter = "SendParameter";
			 public const string ReceiveResult = "ReceiveResult";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string IsOperationSuccess = "IsOperationSuccess";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string LogId = "LogId";
			 public const string OperationType = "OperationType";
			 public const string SendDateTime = "SendDateTime";
			 public const string SendParameter = "SendParameter";
			 public const string ReceiveResult = "ReceiveResult";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string IsOperationSuccess = "IsOperationSuccess";
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
			lock (typeof(SendReceiveLogMetadata))
			{
				if(SendReceiveLogMetadata.mapDelegates == null)
				{
					SendReceiveLogMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SendReceiveLogMetadata.meta == null)
				{
					SendReceiveLogMetadata.meta = new SendReceiveLogMetadata();
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
				

				meta.AddTypeMap("LogId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OperationType", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("SendDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SendParameter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceiveResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOperationSuccess", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "SendReceiveLog";
				meta.Destination = "SendReceiveLog";
				
				meta.spInsert = "proc_SendReceiveLogInsert";				
				meta.spUpdate = "proc_SendReceiveLogUpdate";		
				meta.spDelete = "proc_SendReceiveLogDelete";
				meta.spLoadAll = "proc_SendReceiveLogLoadAll";
				meta.spLoadByPrimaryKey = "proc_SendReceiveLogLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SendReceiveLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
