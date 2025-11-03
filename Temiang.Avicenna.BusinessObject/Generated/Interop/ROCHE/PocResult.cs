/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/26/2021 6:19:20 AM
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



namespace Temiang.Avicenna.BusinessObject.Interop.ROCHE
{

	[Serializable]
	abstract public class esPocResultCollection : esEntityCollectionWAuditLog
	{
		public esPocResultCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PocResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esPocResultQuery query)
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
			this.InitQuery(query as esPocResultQuery);
		}
		#endregion
		
		virtual public PocResult DetachEntity(PocResult entity)
		{
			return base.DetachEntity(entity) as PocResult;
		}
		
		virtual public PocResult AttachEntity(PocResult entity)
		{
			return base.AttachEntity(entity) as PocResult;
		}
		
		virtual public void Combine(PocResultCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PocResult this[int index]
		{
			get
			{
				return base[index] as PocResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PocResult);
		}
	}



	[Serializable]
	abstract public class esPocResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPocResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esPocResult()
		{

		}

		public esPocResult(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 id)
		{
			esPocResultQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 id)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",id);
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
						case "VisitNum": this.str.VisitNum = (string)value; break;							
						case "Pid": this.str.Pid = (string)value; break;							
						case "ResultDt": this.str.ResultDt = (string)value; break;							
						case "TestCode": this.str.TestCode = (string)value; break;							
						case "TestName": this.str.TestName = (string)value; break;							
						case "Result": this.str.Result = (string)value; break;							
						case "Flag": this.str.Flag = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
							break;
						
						case "ResultDt":
						
							if (value == null || value is System.DateTime)
								this.ResultDt = (System.DateTime?)value;
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
		/// Maps to POC_RESULT.ID
		/// </summary>
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(PocResultMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt64(PocResultMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_RESULT.VISIT_NUM
		/// </summary>
		virtual public System.String VisitNum
		{
			get
			{
				return base.GetSystemString(PocResultMetadata.ColumnNames.VisitNum);
			}
			
			set
			{
				base.SetSystemString(PocResultMetadata.ColumnNames.VisitNum, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_RESULT.PID
		/// </summary>
		virtual public System.String Pid
		{
			get
			{
				return base.GetSystemString(PocResultMetadata.ColumnNames.Pid);
			}
			
			set
			{
				base.SetSystemString(PocResultMetadata.ColumnNames.Pid, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_RESULT.RESULT_DT
		/// </summary>
		virtual public System.DateTime? ResultDt
		{
			get
			{
				return base.GetSystemDateTime(PocResultMetadata.ColumnNames.ResultDt);
			}
			
			set
			{
				base.SetSystemDateTime(PocResultMetadata.ColumnNames.ResultDt, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_RESULT.TEST_CODE
		/// </summary>
		virtual public System.String TestCode
		{
			get
			{
				return base.GetSystemString(PocResultMetadata.ColumnNames.TestCode);
			}
			
			set
			{
				base.SetSystemString(PocResultMetadata.ColumnNames.TestCode, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_RESULT.TEST_NAME
		/// </summary>
		virtual public System.String TestName
		{
			get
			{
				return base.GetSystemString(PocResultMetadata.ColumnNames.TestName);
			}
			
			set
			{
				base.SetSystemString(PocResultMetadata.ColumnNames.TestName, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_RESULT.RESULT
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(PocResultMetadata.ColumnNames.Result);
			}
			
			set
			{
				base.SetSystemString(PocResultMetadata.ColumnNames.Result, value);
			}
		}
		
		/// <summary>
		/// Normalcy status of the result. Supported flags: o Empty (blank) = normal range o < = result below instrument displayable range o > = result above instrument displayable range o A = out of normal range o B = better o D = significant change down o H = result above instrument normal range o HH = result above instrument critical range o L = result below instrument normal range o LL = result below instrument critical range o N = normal range o S = susceptible (for microbiology results only) o U = significant change up o W = worse
		/// </summary>
		virtual public System.String Flag
		{
			get
			{
				return base.GetSystemString(PocResultMetadata.ColumnNames.Flag);
			}
			
			set
			{
				base.SetSystemString(PocResultMetadata.ColumnNames.Flag, value);
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
			public esStrings(esPocResult entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int64? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt64(value);
				}
			}
				
			public System.String VisitNum
			{
				get
				{
					System.String data = entity.VisitNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitNum = null;
					else entity.VisitNum = Convert.ToString(value);
				}
			}
				
			public System.String Pid
			{
				get
				{
					System.String data = entity.Pid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pid = null;
					else entity.Pid = Convert.ToString(value);
				}
			}
				
			public System.String ResultDt
			{
				get
				{
					System.DateTime? data = entity.ResultDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultDt = null;
					else entity.ResultDt = Convert.ToDateTime(value);
				}
			}
				
			public System.String TestCode
			{
				get
				{
					System.String data = entity.TestCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestCode = null;
					else entity.TestCode = Convert.ToString(value);
				}
			}
				
			public System.String TestName
			{
				get
				{
					System.String data = entity.TestName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestName = null;
					else entity.TestName = Convert.ToString(value);
				}
			}
				
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
				
			public System.String Flag
			{
				get
				{
					System.String data = entity.Flag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Flag = null;
					else entity.Flag = Convert.ToString(value);
				}
			}
			

			private esPocResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPocResultQuery query)
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
				throw new Exception("esPocResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esPocResultQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PocResultMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.Id, esSystemType.Int64);
			}
		} 
		
		public esQueryItem VisitNum
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.VisitNum, esSystemType.String);
			}
		} 
		
		public esQueryItem Pid
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.Pid, esSystemType.String);
			}
		} 
		
		public esQueryItem ResultDt
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.ResultDt, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TestCode
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.TestCode, esSystemType.String);
			}
		} 
		
		public esQueryItem TestName
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.TestName, esSystemType.String);
			}
		} 
		
		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.Result, esSystemType.String);
			}
		} 
		
		public esQueryItem Flag
		{
			get
			{
				return new esQueryItem(this, PocResultMetadata.ColumnNames.Flag, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PocResultCollection")]
	public partial class PocResultCollection : esPocResultCollection, IEnumerable<PocResult>
	{
		public PocResultCollection()
		{

		}
		
		public static implicit operator List<PocResult>(PocResultCollection coll)
		{
			List<PocResult> list = new List<PocResult>();
			
			foreach (PocResult emp in coll)
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
				return  PocResultMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PocResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PocResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PocResult();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PocResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PocResultQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PocResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PocResult AddNew()
		{
			PocResult entity = base.AddNewEntity() as PocResult;
			
			return entity;
		}

		public PocResult FindByPrimaryKey(System.Int64 id)
		{
			return base.FindByPrimaryKey(id) as PocResult;
		}


		#region IEnumerable<PocResult> Members

		IEnumerator<PocResult> IEnumerable<PocResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PocResult;
			}
		}

		#endregion
		
		private PocResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'POC_RESULT' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PocResult ({Id})")]
	[Serializable]
	public partial class PocResult : esPocResult
	{
		public PocResult()
		{

		}
	
		public PocResult(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PocResultMetadata.Meta();
			}
		}
		
		
		
		override protected esPocResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PocResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PocResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PocResultQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PocResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PocResultQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PocResultQuery : esPocResultQuery
	{
		public PocResultQuery()
		{

		}		
		
		public PocResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PocResultQuery";
        }
		
			
	}


	[Serializable]
	public partial class PocResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PocResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PocResultMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PocResultMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocResultMetadata.ColumnNames.VisitNum, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PocResultMetadata.PropertyNames.VisitNum;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocResultMetadata.ColumnNames.Pid, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PocResultMetadata.PropertyNames.Pid;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocResultMetadata.ColumnNames.ResultDt, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PocResultMetadata.PropertyNames.ResultDt;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocResultMetadata.ColumnNames.TestCode, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PocResultMetadata.PropertyNames.TestCode;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocResultMetadata.ColumnNames.TestName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PocResultMetadata.PropertyNames.TestName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocResultMetadata.ColumnNames.Result, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PocResultMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocResultMetadata.ColumnNames.Flag, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PocResultMetadata.PropertyNames.Flag;
			c.CharacterMaxLength = 1;
			c.Description = "Normalcy status of the result. Supported flags: o Empty (blank) = normal range o < = result below instrument displayable range o > = result above instrument displayable range o A = out of normal range o B = better o D = significant change down o H = result above instrument normal range o HH = result above instrument critical range o L = result below instrument normal range o LL = result below instrument critical range o N = normal range o S = susceptible (for microbiology results only) o U = significant change up o W = worse";
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PocResultMetadata Meta()
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
			 public const string Id = "ID";
			 public const string VisitNum = "VISIT_NUM";
			 public const string Pid = "PID";
			 public const string ResultDt = "RESULT_DT";
			 public const string TestCode = "TEST_CODE";
			 public const string TestName = "TEST_NAME";
			 public const string Result = "RESULT";
			 public const string Flag = "FLAG";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string VisitNum = "VisitNum";
			 public const string Pid = "Pid";
			 public const string ResultDt = "ResultDt";
			 public const string TestCode = "TestCode";
			 public const string TestName = "TestName";
			 public const string Result = "Result";
			 public const string Flag = "Flag";
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
			lock (typeof(PocResultMetadata))
			{
				if(PocResultMetadata.mapDelegates == null)
				{
					PocResultMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PocResultMetadata.meta == null)
				{
					PocResultMetadata.meta = new PocResultMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("VisitNum", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultDt", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TestCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Flag", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "POC_RESULT";
				meta.Destination = "POC_RESULT";
				
				meta.spInsert = "proc_POC_RESULTInsert";				
				meta.spUpdate = "proc_POC_RESULTUpdate";		
				meta.spDelete = "proc_POC_RESULTDelete";
				meta.spLoadAll = "proc_POC_RESULTLoadAll";
				meta.spLoadByPrimaryKey = "proc_POC_RESULTLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PocResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
