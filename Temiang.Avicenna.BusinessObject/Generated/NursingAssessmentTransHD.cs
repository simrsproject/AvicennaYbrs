/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2015 10:09:43 PM
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
	abstract public class esNursingAssessmentTransHDCollection : esEntityCollectionWAuditLog
	{
		public esNursingAssessmentTransHDCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NursingAssessmentTransHDCollection";
		}

		#region Query Logic
		protected void InitQuery(esNursingAssessmentTransHDQuery query)
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
			this.InitQuery(query as esNursingAssessmentTransHDQuery);
		}
		#endregion
		
		virtual public NursingAssessmentTransHD DetachEntity(NursingAssessmentTransHD entity)
		{
			return base.DetachEntity(entity) as NursingAssessmentTransHD;
		}
		
		virtual public NursingAssessmentTransHD AttachEntity(NursingAssessmentTransHD entity)
		{
			return base.AttachEntity(entity) as NursingAssessmentTransHD;
		}
		
		virtual public void Combine(NursingAssessmentTransHDCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingAssessmentTransHD this[int index]
		{
			get
			{
				return base[index] as NursingAssessmentTransHD;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingAssessmentTransHD);
		}
	}



	[Serializable]
	abstract public class esNursingAssessmentTransHD : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingAssessmentTransHDQuery GetDynamicQuery()
		{
			return null;
		}

		public esNursingAssessmentTransHD()
		{

		}

		public esNursingAssessmentTransHD(DataRow row)
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
			esNursingAssessmentTransHDQuery query = this.GetDynamicQuery();
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "AssessmentDateTime": this.str.AssessmentDateTime = (string)value; break;							
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;							
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "QuestionFormReference": this.str.QuestionFormReference = (string)value; break;
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
						
						case "AssessmentDateTime":
						
							if (value == null || value is System.DateTime)
								this.AssessmentDateTime = (System.DateTime?)value;
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
		/// Maps to NursingAssessmentTransHD.ID
		/// </summary>
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(NursingAssessmentTransHDMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt64(NursingAssessmentTransHDMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransHD.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransHD.AssessmentDateTime
		/// </summary>
		virtual public System.DateTime? AssessmentDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransHD.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransHD.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentTransHDMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentTransHDMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransHD.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransHD.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingAssessmentTransHD.QuestionFormReference
		/// </summary>
		virtual public System.String QuestionFormReference
		{
			get
			{
				return base.GetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.QuestionFormReference);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentTransHDMetadata.ColumnNames.QuestionFormReference, value);
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
			public esStrings(esNursingAssessmentTransHD entity)
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
				
			public System.String AssessmentDateTime
			{
				get
				{
					System.DateTime? data = entity.AssessmentDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssessmentDateTime = null;
					else entity.AssessmentDateTime = Convert.ToDateTime(value);
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
				
			public System.String QuestionFormReference
			{
				get
				{
					System.String data = entity.QuestionFormReference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormReference = null;
					else entity.QuestionFormReference = Convert.ToString(value);
				}
			}
			

			private esNursingAssessmentTransHD entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingAssessmentTransHDQuery query)
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
				throw new Exception("esNursingAssessmentTransHD can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esNursingAssessmentTransHDQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentTransHDMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.Id, esSystemType.Int64);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem AssessmentDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem QuestionFormReference
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentTransHDMetadata.ColumnNames.QuestionFormReference, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingAssessmentTransHDCollection")]
	public partial class NursingAssessmentTransHDCollection : esNursingAssessmentTransHDCollection, IEnumerable<NursingAssessmentTransHD>
	{
		public NursingAssessmentTransHDCollection()
		{

		}
		
		public static implicit operator List<NursingAssessmentTransHD>(NursingAssessmentTransHDCollection coll)
		{
			List<NursingAssessmentTransHD> list = new List<NursingAssessmentTransHD>();
			
			foreach (NursingAssessmentTransHD emp in coll)
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
				return  NursingAssessmentTransHDMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentTransHDQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingAssessmentTransHD(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingAssessmentTransHD();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NursingAssessmentTransHDQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentTransHDQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NursingAssessmentTransHDQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NursingAssessmentTransHD AddNew()
		{
			NursingAssessmentTransHD entity = base.AddNewEntity() as NursingAssessmentTransHD;
			
			return entity;
		}

		public NursingAssessmentTransHD FindByPrimaryKey(System.Int64 id)
		{
			return base.FindByPrimaryKey(id) as NursingAssessmentTransHD;
		}


		#region IEnumerable<NursingAssessmentTransHD> Members

		IEnumerator<NursingAssessmentTransHD> IEnumerable<NursingAssessmentTransHD>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingAssessmentTransHD;
			}
		}

		#endregion
		
		private NursingAssessmentTransHDQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingAssessmentTransHD' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NursingAssessmentTransHD ({Id})")]
	[Serializable]
	public partial class NursingAssessmentTransHD : esNursingAssessmentTransHD
	{
		public NursingAssessmentTransHD()
		{

		}
	
		public NursingAssessmentTransHD(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentTransHDMetadata.Meta();
			}
		}
		
		
		
		override protected esNursingAssessmentTransHDQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentTransHDQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NursingAssessmentTransHDQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentTransHDQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NursingAssessmentTransHDQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NursingAssessmentTransHDQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NursingAssessmentTransHDQuery : esNursingAssessmentTransHDQuery
	{
		public NursingAssessmentTransHDQuery()
		{

		}		
		
		public NursingAssessmentTransHDQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NursingAssessmentTransHDQuery";
        }
		
			
	}


	[Serializable]
	public partial class NursingAssessmentTransHDMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingAssessmentTransHDMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.AssessmentDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.CreateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.CreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingAssessmentTransHDMetadata.ColumnNames.QuestionFormReference, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentTransHDMetadata.PropertyNames.QuestionFormReference;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NursingAssessmentTransHDMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string AssessmentDateTime = "AssessmentDateTime";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string QuestionFormReference = "QuestionFormReference";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string TransactionNo = "TransactionNo";
			 public const string AssessmentDateTime = "AssessmentDateTime";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string QuestionFormReference = "QuestionFormReference";
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
			lock (typeof(NursingAssessmentTransHDMetadata))
			{
				if(NursingAssessmentTransHDMetadata.mapDelegates == null)
				{
					NursingAssessmentTransHDMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingAssessmentTransHDMetadata.meta == null)
				{
					NursingAssessmentTransHDMetadata.meta = new NursingAssessmentTransHDMetadata();
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
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssessmentDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("QuestionFormReference", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "NursingAssessmentTransHD";
				meta.Destination = "NursingAssessmentTransHD";
				
				meta.spInsert = "proc_NursingAssessmentTransHDInsert";				
				meta.spUpdate = "proc_NursingAssessmentTransHDUpdate";		
				meta.spDelete = "proc_NursingAssessmentTransHDDelete";
				meta.spLoadAll = "proc_NursingAssessmentTransHDLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingAssessmentTransHDLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingAssessmentTransHDMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
