/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/30/2015 12:47:44 AM
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
	abstract public class esVwJasaRaharjaLogCollection : esEntityCollectionWAuditLog
	{
		public esVwJasaRaharjaLogCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwJasaRaharjaLogCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwJasaRaharjaLogQuery query)
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
			this.InitQuery(query as esVwJasaRaharjaLogQuery);
		}
		#endregion
		
		virtual public VwJasaRaharjaLog DetachEntity(VwJasaRaharjaLog entity)
		{
			return base.DetachEntity(entity) as VwJasaRaharjaLog;
		}
		
		virtual public VwJasaRaharjaLog AttachEntity(VwJasaRaharjaLog entity)
		{
			return base.AttachEntity(entity) as VwJasaRaharjaLog;
		}
		
		virtual public void Combine(VwJasaRaharjaLogCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwJasaRaharjaLog this[int index]
		{
			get
			{
				return base[index] as VwJasaRaharjaLog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwJasaRaharjaLog);
		}
	}



	[Serializable]
	abstract public class esVwJasaRaharjaLog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwJasaRaharjaLogQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwJasaRaharjaLog()
		{

		}

		public esVwJasaRaharjaLog(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
						case "MedicalNo": this.str.MedicalNo = (string)value; break;
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
		/// Maps to vw_JasaRaharjaLog.LogId
		/// </summary>
		virtual public System.Int32? LogId
		{
			get
			{
				return base.GetSystemInt32(VwJasaRaharjaLogMetadata.ColumnNames.LogId);
			}
			
			set
			{
				base.SetSystemInt32(VwJasaRaharjaLogMetadata.ColumnNames.LogId, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_JasaRaharjaLog.OperationType
		/// </summary>
		virtual public System.Byte? OperationType
		{
			get
			{
				return base.GetSystemByte(VwJasaRaharjaLogMetadata.ColumnNames.OperationType);
			}
			
			set
			{
				base.SetSystemByte(VwJasaRaharjaLogMetadata.ColumnNames.OperationType, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_JasaRaharjaLog.SendDateTime
		/// </summary>
		virtual public System.DateTime? SendDateTime
		{
			get
			{
				return base.GetSystemDateTime(VwJasaRaharjaLogMetadata.ColumnNames.SendDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VwJasaRaharjaLogMetadata.ColumnNames.SendDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_JasaRaharjaLog.SendParameter
		/// </summary>
		virtual public System.String SendParameter
		{
			get
			{
				return base.GetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.SendParameter);
			}
			
			set
			{
				base.SetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.SendParameter, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_JasaRaharjaLog.ReceiveResult
		/// </summary>
		virtual public System.String ReceiveResult
		{
			get
			{
				return base.GetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.ReceiveResult);
			}
			
			set
			{
				base.SetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.ReceiveResult, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_JasaRaharjaLog.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_JasaRaharjaLog.IsOperationSuccess
		/// </summary>
		virtual public System.Boolean? IsOperationSuccess
		{
			get
			{
				return base.GetSystemBoolean(VwJasaRaharjaLogMetadata.ColumnNames.IsOperationSuccess);
			}
			
			set
			{
				base.SetSystemBoolean(VwJasaRaharjaLogMetadata.ColumnNames.IsOperationSuccess, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_JasaRaharjaLog.MedicalNo
		/// </summary>
		virtual public System.String MedicalNo
		{
			get
			{
				return base.GetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.MedicalNo);
			}
			
			set
			{
				base.SetSystemString(VwJasaRaharjaLogMetadata.ColumnNames.MedicalNo, value);
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
			public esStrings(esVwJasaRaharjaLog entity)
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
				
			public System.String MedicalNo
			{
				get
				{
					System.String data = entity.MedicalNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalNo = null;
					else entity.MedicalNo = Convert.ToString(value);
				}
			}
			

			private esVwJasaRaharjaLog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwJasaRaharjaLogQuery query)
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
				throw new Exception("esVwJasaRaharjaLog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwJasaRaharjaLogQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwJasaRaharjaLogMetadata.Meta();
			}
		}	
		

		public esQueryItem LogId
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.LogId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OperationType
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.OperationType, esSystemType.Byte);
			}
		} 
		
		public esQueryItem SendDateTime
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.SendDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SendParameter
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.SendParameter, esSystemType.String);
			}
		} 
		
		public esQueryItem ReceiveResult
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.ReceiveResult, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsOperationSuccess
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.IsOperationSuccess, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem MedicalNo
		{
			get
			{
				return new esQueryItem(this, VwJasaRaharjaLogMetadata.ColumnNames.MedicalNo, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwJasaRaharjaLogCollection")]
	public partial class VwJasaRaharjaLogCollection : esVwJasaRaharjaLogCollection, IEnumerable<VwJasaRaharjaLog>
	{
		public VwJasaRaharjaLogCollection()
		{

		}
		
		public static implicit operator List<VwJasaRaharjaLog>(VwJasaRaharjaLogCollection coll)
		{
			List<VwJasaRaharjaLog> list = new List<VwJasaRaharjaLog>();
			
			foreach (VwJasaRaharjaLog emp in coll)
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
				return  VwJasaRaharjaLogMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwJasaRaharjaLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwJasaRaharjaLog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwJasaRaharjaLog();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwJasaRaharjaLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwJasaRaharjaLogQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwJasaRaharjaLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwJasaRaharjaLog AddNew()
		{
			VwJasaRaharjaLog entity = base.AddNewEntity() as VwJasaRaharjaLog;
			
			return entity;
		}


		#region IEnumerable<VwJasaRaharjaLog> Members

		IEnumerator<VwJasaRaharjaLog> IEnumerable<VwJasaRaharjaLog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwJasaRaharjaLog;
			}
		}

		#endregion
		
		private VwJasaRaharjaLogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_JasaRaharjaLog' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwJasaRaharjaLog ()")]
	[Serializable]
	public partial class VwJasaRaharjaLog : esVwJasaRaharjaLog
	{
		public VwJasaRaharjaLog()
		{

		}
	
		public VwJasaRaharjaLog(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwJasaRaharjaLogMetadata.Meta();
			}
		}
		
		
		
		override protected esVwJasaRaharjaLogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwJasaRaharjaLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwJasaRaharjaLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwJasaRaharjaLogQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwJasaRaharjaLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwJasaRaharjaLogQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwJasaRaharjaLogQuery : esVwJasaRaharjaLogQuery
	{
		public VwJasaRaharjaLogQuery()
		{

		}		
		
		public VwJasaRaharjaLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwJasaRaharjaLogQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwJasaRaharjaLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwJasaRaharjaLogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.LogId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.LogId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.OperationType, 1, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.OperationType;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.SendDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.SendDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.SendParameter, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.SendParameter;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.ReceiveResult, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.ReceiveResult;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.RegistrationNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.IsOperationSuccess, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.IsOperationSuccess;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwJasaRaharjaLogMetadata.ColumnNames.MedicalNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwJasaRaharjaLogMetadata.PropertyNames.MedicalNo;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwJasaRaharjaLogMetadata Meta()
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
			 public const string MedicalNo = "MedicalNo";
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
			 public const string MedicalNo = "MedicalNo";
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
			lock (typeof(VwJasaRaharjaLogMetadata))
			{
				if(VwJasaRaharjaLogMetadata.mapDelegates == null)
				{
					VwJasaRaharjaLogMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwJasaRaharjaLogMetadata.meta == null)
				{
					VwJasaRaharjaLogMetadata.meta = new VwJasaRaharjaLogMetadata();
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
				meta.AddTypeMap("MedicalNo", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "vw_JasaRaharjaLog";
				meta.Destination = "vw_JasaRaharjaLog";
				
				meta.spInsert = "proc_vw_JasaRaharjaLogInsert";				
				meta.spUpdate = "proc_vw_JasaRaharjaLogUpdate";		
				meta.spDelete = "proc_vw_JasaRaharjaLogDelete";
				meta.spLoadAll = "proc_vw_JasaRaharjaLogLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_JasaRaharjaLogLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwJasaRaharjaLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
