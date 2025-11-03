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
	abstract public class esParamedicLeaveDateCollection : esEntityCollectionWAuditLog
	{
		public esParamedicLeaveDateCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicLeaveDateCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicLeaveDateQuery query)
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
			this.InitQuery(query as esParamedicLeaveDateQuery);
		}
		#endregion
		
		virtual public ParamedicLeaveDate DetachEntity(ParamedicLeaveDate entity)
		{
			return base.DetachEntity(entity) as ParamedicLeaveDate;
		}
		
		virtual public ParamedicLeaveDate AttachEntity(ParamedicLeaveDate entity)
		{
			return base.AttachEntity(entity) as ParamedicLeaveDate;
		}
		
		virtual public void Combine(ParamedicLeaveDateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicLeaveDate this[int index]
		{
			get
			{
				return base[index] as ParamedicLeaveDate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicLeaveDate);
		}
	}



	[Serializable]
	abstract public class esParamedicLeaveDate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicLeaveDateQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicLeaveDate()
		{

		}

		public esParamedicLeaveDate(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.DateTime leaveDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, leaveDate);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, leaveDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.DateTime leaveDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, leaveDate);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, leaveDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.DateTime leaveDate)
		{
			esParamedicLeaveDateQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.LeaveDate == leaveDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.DateTime leaveDate)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("LeaveDate",leaveDate);
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
						case "LeaveDate": this.str.LeaveDate = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LeaveDate":
						
							if (value == null || value is System.DateTime)
								this.LeaveDate = (System.DateTime?)value;
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
		/// Maps to ParamedicLeaveDate.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveDateMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveDateMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeaveDate.LeaveDate
		/// </summary>
		virtual public System.DateTime? LeaveDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicLeaveDateMetadata.ColumnNames.LeaveDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicLeaveDateMetadata.ColumnNames.LeaveDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeaveDate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveDateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveDateMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeaveDate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicLeaveDateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicLeaveDateMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esParamedicLeaveDate entity)
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
				
			public System.String LeaveDate
			{
				get
				{
					System.DateTime? data = entity.LeaveDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveDate = null;
					else entity.LeaveDate = Convert.ToDateTime(value);
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
			

			private esParamedicLeaveDate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicLeaveDateQuery query)
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
				throw new Exception("esParamedicLeaveDate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicLeaveDate : esParamedicLeaveDate
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
	abstract public class esParamedicLeaveDateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicLeaveDateMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveDateMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LeaveDate
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveDateMetadata.ColumnNames.LeaveDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveDateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveDateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicLeaveDateCollection")]
	public partial class ParamedicLeaveDateCollection : esParamedicLeaveDateCollection, IEnumerable<ParamedicLeaveDate>
	{
		public ParamedicLeaveDateCollection()
		{

		}
		
		public static implicit operator List<ParamedicLeaveDate>(ParamedicLeaveDateCollection coll)
		{
			List<ParamedicLeaveDate> list = new List<ParamedicLeaveDate>();
			
			foreach (ParamedicLeaveDate emp in coll)
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
				return  ParamedicLeaveDateMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicLeaveDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicLeaveDate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicLeaveDate();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicLeaveDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicLeaveDateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicLeaveDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicLeaveDate AddNew()
		{
			ParamedicLeaveDate entity = base.AddNewEntity() as ParamedicLeaveDate;
			
			return entity;
		}

		public ParamedicLeaveDate FindByPrimaryKey(System.String transactionNo, System.DateTime leaveDate)
		{
			return base.FindByPrimaryKey(transactionNo, leaveDate) as ParamedicLeaveDate;
		}


		#region IEnumerable<ParamedicLeaveDate> Members

		IEnumerator<ParamedicLeaveDate> IEnumerable<ParamedicLeaveDate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicLeaveDate;
			}
		}

		#endregion
		
		private ParamedicLeaveDateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicLeaveDate' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicLeaveDate ({TransactionNo},{LeaveDate})")]
	[Serializable]
	public partial class ParamedicLeaveDate : esParamedicLeaveDate
	{
		public ParamedicLeaveDate()
		{

		}
	
		public ParamedicLeaveDate(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicLeaveDateMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicLeaveDateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicLeaveDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicLeaveDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicLeaveDateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicLeaveDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicLeaveDateQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicLeaveDateQuery : esParamedicLeaveDateQuery
	{
		public ParamedicLeaveDateQuery()
		{

		}		
		
		public ParamedicLeaveDateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicLeaveDateQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicLeaveDateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicLeaveDateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicLeaveDateMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveDateMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveDateMetadata.ColumnNames.LeaveDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicLeaveDateMetadata.PropertyNames.LeaveDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveDateMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveDateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveDateMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicLeaveDateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicLeaveDateMetadata Meta()
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
			 public const string LeaveDate = "LeaveDate";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string LeaveDate = "LeaveDate";
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
			lock (typeof(ParamedicLeaveDateMetadata))
			{
				if(ParamedicLeaveDateMetadata.mapDelegates == null)
				{
					ParamedicLeaveDateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicLeaveDateMetadata.meta == null)
				{
					ParamedicLeaveDateMetadata.meta = new ParamedicLeaveDateMetadata();
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
				meta.AddTypeMap("LeaveDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "ParamedicLeaveDate";
				meta.Destination = "ParamedicLeaveDate";
				
				meta.spInsert = "proc_ParamedicLeaveDateInsert";				
				meta.spUpdate = "proc_ParamedicLeaveDateUpdate";		
				meta.spDelete = "proc_ParamedicLeaveDateDelete";
				meta.spLoadAll = "proc_ParamedicLeaveDateLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicLeaveDateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicLeaveDateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
