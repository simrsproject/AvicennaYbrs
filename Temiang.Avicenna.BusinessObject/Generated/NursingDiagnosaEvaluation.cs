/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/11/2015 6:05:45 AM
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
	abstract public class esNursingDiagnosaEvaluationCollection : esEntityCollectionWAuditLog
	{
		public esNursingDiagnosaEvaluationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NursingDiagnosaEvaluationCollection";
		}

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaEvaluationQuery query)
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
			this.InitQuery(query as esNursingDiagnosaEvaluationQuery);
		}
		#endregion
		
		virtual public NursingDiagnosaEvaluation DetachEntity(NursingDiagnosaEvaluation entity)
		{
			return base.DetachEntity(entity) as NursingDiagnosaEvaluation;
		}
		
		virtual public NursingDiagnosaEvaluation AttachEntity(NursingDiagnosaEvaluation entity)
		{
			return base.AttachEntity(entity) as NursingDiagnosaEvaluation;
		}
		
		virtual public void Combine(NursingDiagnosaEvaluationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingDiagnosaEvaluation this[int index]
		{
			get
			{
				return base[index] as NursingDiagnosaEvaluation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingDiagnosaEvaluation);
		}
	}



	[Serializable]
	abstract public class esNursingDiagnosaEvaluation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingDiagnosaEvaluationQuery GetDynamicQuery()
		{
			return null;
		}

		public esNursingDiagnosaEvaluation()
		{

		}

		public esNursingDiagnosaEvaluation(DataRow row)
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
			esNursingDiagnosaEvaluationQuery query = this.GetDynamicQuery();
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
						case "EvaluationID": this.str.EvaluationID = (string)value; break;							
						case "InterventionID": this.str.InterventionID = (string)value; break;							
						case "NursingInterventionID": this.str.NursingInterventionID = (string)value; break;							
						case "SRNursingCarePlanning": this.str.SRNursingCarePlanning = (string)value; break;							
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;							
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
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
						
						case "EvaluationID":
						
							if (value == null || value is System.Int64)
								this.EvaluationID = (System.Int64?)value;
							break;
						
						case "InterventionID":
						
							if (value == null || value is System.Int64)
								this.InterventionID = (System.Int64?)value;
							break;
						
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to NursingDiagnosaEvaluation.ID
		/// </summary>
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(NursingDiagnosaEvaluationMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt64(NursingDiagnosaEvaluationMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingDiagnosaEvaluation.EvaluationID
		/// </summary>
		virtual public System.Int64? EvaluationID
		{
			get
			{
				return base.GetSystemInt64(NursingDiagnosaEvaluationMetadata.ColumnNames.EvaluationID);
			}
			
			set
			{
				base.SetSystemInt64(NursingDiagnosaEvaluationMetadata.ColumnNames.EvaluationID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingDiagnosaEvaluation.InterventionID
		/// </summary>
		virtual public System.Int64? InterventionID
		{
			get
			{
				return base.GetSystemInt64(NursingDiagnosaEvaluationMetadata.ColumnNames.InterventionID);
			}
			
			set
			{
				base.SetSystemInt64(NursingDiagnosaEvaluationMetadata.ColumnNames.InterventionID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingDiagnosaEvaluation.NursingInterventionID
		/// </summary>
		virtual public System.String NursingInterventionID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaEvaluationMetadata.ColumnNames.NursingInterventionID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaEvaluationMetadata.ColumnNames.NursingInterventionID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingDiagnosaEvaluation.SRNursingCarePlanning
		/// </summary>
		virtual public System.String SRNursingCarePlanning
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaEvaluationMetadata.ColumnNames.SRNursingCarePlanning);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaEvaluationMetadata.ColumnNames.SRNursingCarePlanning, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingDiagnosaEvaluation.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaEvaluationMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaEvaluationMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingDiagnosaEvaluation.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingDiagnosaEvaluationMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingDiagnosaEvaluationMetadata.ColumnNames.CreateDateTime, value);
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
			public esStrings(esNursingDiagnosaEvaluation entity)
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
				
			public System.String EvaluationID
			{
				get
				{
					System.Int64? data = entity.EvaluationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluationID = null;
					else entity.EvaluationID = Convert.ToInt64(value);
				}
			}
				
			public System.String InterventionID
			{
				get
				{
					System.Int64? data = entity.InterventionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InterventionID = null;
					else entity.InterventionID = Convert.ToInt64(value);
				}
			}
				
			public System.String NursingInterventionID
			{
				get
				{
					System.String data = entity.NursingInterventionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingInterventionID = null;
					else entity.NursingInterventionID = Convert.ToString(value);
				}
			}
				
			public System.String SRNursingCarePlanning
			{
				get
				{
					System.String data = entity.SRNursingCarePlanning;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNursingCarePlanning = null;
					else entity.SRNursingCarePlanning = Convert.ToString(value);
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
			

			private esNursingDiagnosaEvaluation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaEvaluationQuery query)
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
				throw new Exception("esNursingDiagnosaEvaluation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esNursingDiagnosaEvaluationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaEvaluationMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaEvaluationMetadata.ColumnNames.Id, esSystemType.Int64);
			}
		} 
		
		public esQueryItem EvaluationID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaEvaluationMetadata.ColumnNames.EvaluationID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem InterventionID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaEvaluationMetadata.ColumnNames.InterventionID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem NursingInterventionID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaEvaluationMetadata.ColumnNames.NursingInterventionID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRNursingCarePlanning
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaEvaluationMetadata.ColumnNames.SRNursingCarePlanning, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaEvaluationMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaEvaluationMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingDiagnosaEvaluationCollection")]
	public partial class NursingDiagnosaEvaluationCollection : esNursingDiagnosaEvaluationCollection, IEnumerable<NursingDiagnosaEvaluation>
	{
		public NursingDiagnosaEvaluationCollection()
		{

		}
		
		public static implicit operator List<NursingDiagnosaEvaluation>(NursingDiagnosaEvaluationCollection coll)
		{
			List<NursingDiagnosaEvaluation> list = new List<NursingDiagnosaEvaluation>();
			
			foreach (NursingDiagnosaEvaluation emp in coll)
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
				return  NursingDiagnosaEvaluationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaEvaluationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingDiagnosaEvaluation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingDiagnosaEvaluation();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NursingDiagnosaEvaluationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaEvaluationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NursingDiagnosaEvaluationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NursingDiagnosaEvaluation AddNew()
		{
			NursingDiagnosaEvaluation entity = base.AddNewEntity() as NursingDiagnosaEvaluation;
			
			return entity;
		}

		public NursingDiagnosaEvaluation FindByPrimaryKey(System.Int64 id)
		{
			return base.FindByPrimaryKey(id) as NursingDiagnosaEvaluation;
		}


		#region IEnumerable<NursingDiagnosaEvaluation> Members

		IEnumerator<NursingDiagnosaEvaluation> IEnumerable<NursingDiagnosaEvaluation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingDiagnosaEvaluation;
			}
		}

		#endregion
		
		private NursingDiagnosaEvaluationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingDiagnosaEvaluation' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NursingDiagnosaEvaluation ({Id})")]
	[Serializable]
	public partial class NursingDiagnosaEvaluation : esNursingDiagnosaEvaluation
	{
		public NursingDiagnosaEvaluation()
		{

		}
	
		public NursingDiagnosaEvaluation(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaEvaluationMetadata.Meta();
			}
		}
		
		
		
		override protected esNursingDiagnosaEvaluationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaEvaluationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NursingDiagnosaEvaluationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaEvaluationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NursingDiagnosaEvaluationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NursingDiagnosaEvaluationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NursingDiagnosaEvaluationQuery : esNursingDiagnosaEvaluationQuery
	{
		public NursingDiagnosaEvaluationQuery()
		{

		}		
		
		public NursingDiagnosaEvaluationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NursingDiagnosaEvaluationQuery";
        }
		
			
	}


	[Serializable]
	public partial class NursingDiagnosaEvaluationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingDiagnosaEvaluationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NursingDiagnosaEvaluationMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingDiagnosaEvaluationMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingDiagnosaEvaluationMetadata.ColumnNames.EvaluationID, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingDiagnosaEvaluationMetadata.PropertyNames.EvaluationID;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingDiagnosaEvaluationMetadata.ColumnNames.InterventionID, 2, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingDiagnosaEvaluationMetadata.PropertyNames.InterventionID;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingDiagnosaEvaluationMetadata.ColumnNames.NursingInterventionID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaEvaluationMetadata.PropertyNames.NursingInterventionID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingDiagnosaEvaluationMetadata.ColumnNames.SRNursingCarePlanning, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaEvaluationMetadata.PropertyNames.SRNursingCarePlanning;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingDiagnosaEvaluationMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaEvaluationMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingDiagnosaEvaluationMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingDiagnosaEvaluationMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NursingDiagnosaEvaluationMetadata Meta()
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
			 public const string EvaluationID = "EvaluationID";
			 public const string InterventionID = "InterventionID";
			 public const string NursingInterventionID = "NursingInterventionID";
			 public const string SRNursingCarePlanning = "SRNursingCarePlanning";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string EvaluationID = "EvaluationID";
			 public const string InterventionID = "InterventionID";
			 public const string NursingInterventionID = "NursingInterventionID";
			 public const string SRNursingCarePlanning = "SRNursingCarePlanning";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
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
			lock (typeof(NursingDiagnosaEvaluationMetadata))
			{
				if(NursingDiagnosaEvaluationMetadata.mapDelegates == null)
				{
					NursingDiagnosaEvaluationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingDiagnosaEvaluationMetadata.meta == null)
				{
					NursingDiagnosaEvaluationMetadata.meta = new NursingDiagnosaEvaluationMetadata();
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
				meta.AddTypeMap("EvaluationID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("InterventionID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("NursingInterventionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNursingCarePlanning", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "NursingDiagnosaEvaluation";
				meta.Destination = "NursingDiagnosaEvaluation";
				
				meta.spInsert = "proc_NursingDiagnosaEvaluationInsert";				
				meta.spUpdate = "proc_NursingDiagnosaEvaluationUpdate";		
				meta.spDelete = "proc_NursingDiagnosaEvaluationDelete";
				meta.spLoadAll = "proc_NursingDiagnosaEvaluationLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingDiagnosaEvaluationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingDiagnosaEvaluationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
