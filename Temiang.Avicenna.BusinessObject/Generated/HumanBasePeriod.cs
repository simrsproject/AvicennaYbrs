/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:16 PM
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
	abstract public class esHumanBasePeriodCollection : esEntityCollectionWAuditLog
	{
		public esHumanBasePeriodCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HumanBasePeriodCollection";
		}

		#region Query Logic
		protected void InitQuery(esHumanBasePeriodQuery query)
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
			this.InitQuery(query as esHumanBasePeriodQuery);
		}
		#endregion
		
		virtual public HumanBasePeriod DetachEntity(HumanBasePeriod entity)
		{
			return base.DetachEntity(entity) as HumanBasePeriod;
		}
		
		virtual public HumanBasePeriod AttachEntity(HumanBasePeriod entity)
		{
			return base.AttachEntity(entity) as HumanBasePeriod;
		}
		
		virtual public void Combine(HumanBasePeriodCollection collection)
		{
			base.Combine(collection);
		}
		
		new public HumanBasePeriod this[int index]
		{
			get
			{
				return base[index] as HumanBasePeriod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HumanBasePeriod);
		}
	}



	[Serializable]
	abstract public class esHumanBasePeriod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHumanBasePeriodQuery GetDynamicQuery()
		{
			return null;
		}

		public esHumanBasePeriod()
		{

		}

		public esHumanBasePeriod(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 humanBasePeriodID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(humanBasePeriodID);
			else
				return LoadByPrimaryKeyStoredProcedure(humanBasePeriodID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 humanBasePeriodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(humanBasePeriodID);
			else
				return LoadByPrimaryKeyStoredProcedure(humanBasePeriodID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 humanBasePeriodID)
		{
			esHumanBasePeriodQuery query = this.GetDynamicQuery();
			query.Where(query.HumanBasePeriodID == humanBasePeriodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 humanBasePeriodID)
		{
			esParameters parms = new esParameters();
			parms.Add("HumanBasePeriodID",humanBasePeriodID);
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
						case "HumanBasePeriodID": this.str.HumanBasePeriodID = (string)value; break;							
						case "YearPeriod": this.str.YearPeriod = (string)value; break;							
						case "PeriodeName": this.str.PeriodeName = (string)value; break;							
						case "StartDate": this.str.StartDate = (string)value; break;							
						case "EndDate": this.str.EndDate = (string)value; break;							
						case "IsRecruitment": this.str.IsRecruitment = (string)value; break;							
						case "IsTraining": this.str.IsTraining = (string)value; break;							
						case "IsCareer": this.str.IsCareer = (string)value; break;							
						case "IsAppraisal": this.str.IsAppraisal = (string)value; break;							
						case "IsMedical": this.str.IsMedical = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "HumanBasePeriodID":
						
							if (value == null || value is System.Int32)
								this.HumanBasePeriodID = (System.Int32?)value;
							break;
						
						case "StartDate":
						
							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						
						case "EndDate":
						
							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						
						case "IsRecruitment":
						
							if (value == null || value is System.Boolean)
								this.IsRecruitment = (System.Boolean?)value;
							break;
						
						case "IsTraining":
						
							if (value == null || value is System.Boolean)
								this.IsTraining = (System.Boolean?)value;
							break;
						
						case "IsCareer":
						
							if (value == null || value is System.Boolean)
								this.IsCareer = (System.Boolean?)value;
							break;
						
						case "IsAppraisal":
						
							if (value == null || value is System.Boolean)
								this.IsAppraisal = (System.Boolean?)value;
							break;
						
						case "IsMedical":
						
							if (value == null || value is System.Boolean)
								this.IsMedical = (System.Boolean?)value;
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
		/// Maps to HumanBasePeriod.HumanBasePeriodID
		/// </summary>
		virtual public System.Int32? HumanBasePeriodID
		{
			get
			{
				return base.GetSystemInt32(HumanBasePeriodMetadata.ColumnNames.HumanBasePeriodID);
			}
			
			set
			{
				base.SetSystemInt32(HumanBasePeriodMetadata.ColumnNames.HumanBasePeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.YearPeriod
		/// </summary>
		virtual public System.String YearPeriod
		{
			get
			{
				return base.GetSystemString(HumanBasePeriodMetadata.ColumnNames.YearPeriod);
			}
			
			set
			{
				base.SetSystemString(HumanBasePeriodMetadata.ColumnNames.YearPeriod, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.PeriodeName
		/// </summary>
		virtual public System.String PeriodeName
		{
			get
			{
				return base.GetSystemString(HumanBasePeriodMetadata.ColumnNames.PeriodeName);
			}
			
			set
			{
				base.SetSystemString(HumanBasePeriodMetadata.ColumnNames.PeriodeName, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(HumanBasePeriodMetadata.ColumnNames.StartDate);
			}
			
			set
			{
				base.SetSystemDateTime(HumanBasePeriodMetadata.ColumnNames.StartDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(HumanBasePeriodMetadata.ColumnNames.EndDate);
			}
			
			set
			{
				base.SetSystemDateTime(HumanBasePeriodMetadata.ColumnNames.EndDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.IsRecruitment
		/// </summary>
		virtual public System.Boolean? IsRecruitment
		{
			get
			{
				return base.GetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsRecruitment);
			}
			
			set
			{
				base.SetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsRecruitment, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.IsTraining
		/// </summary>
		virtual public System.Boolean? IsTraining
		{
			get
			{
				return base.GetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsTraining);
			}
			
			set
			{
				base.SetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsTraining, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.IsCareer
		/// </summary>
		virtual public System.Boolean? IsCareer
		{
			get
			{
				return base.GetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsCareer);
			}
			
			set
			{
				base.SetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsCareer, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.IsAppraisal
		/// </summary>
		virtual public System.Boolean? IsAppraisal
		{
			get
			{
				return base.GetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsAppraisal);
			}
			
			set
			{
				base.SetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsAppraisal, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.IsMedical
		/// </summary>
		virtual public System.Boolean? IsMedical
		{
			get
			{
				return base.GetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsMedical);
			}
			
			set
			{
				base.SetSystemBoolean(HumanBasePeriodMetadata.ColumnNames.IsMedical, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HumanBasePeriodMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(HumanBasePeriodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to HumanBasePeriod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HumanBasePeriodMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(HumanBasePeriodMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esHumanBasePeriod entity)
			{
				this.entity = entity;
			}
			
	
			public System.String HumanBasePeriodID
			{
				get
				{
					System.Int32? data = entity.HumanBasePeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HumanBasePeriodID = null;
					else entity.HumanBasePeriodID = Convert.ToInt32(value);
				}
			}
				
			public System.String YearPeriod
			{
				get
				{
					System.String data = entity.YearPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearPeriod = null;
					else entity.YearPeriod = Convert.ToString(value);
				}
			}
				
			public System.String PeriodeName
			{
				get
				{
					System.String data = entity.PeriodeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodeName = null;
					else entity.PeriodeName = Convert.ToString(value);
				}
			}
				
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String IsRecruitment
			{
				get
				{
					System.Boolean? data = entity.IsRecruitment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRecruitment = null;
					else entity.IsRecruitment = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsTraining
			{
				get
				{
					System.Boolean? data = entity.IsTraining;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTraining = null;
					else entity.IsTraining = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsCareer
			{
				get
				{
					System.Boolean? data = entity.IsCareer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCareer = null;
					else entity.IsCareer = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsAppraisal
			{
				get
				{
					System.Boolean? data = entity.IsAppraisal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAppraisal = null;
					else entity.IsAppraisal = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsMedical
			{
				get
				{
					System.Boolean? data = entity.IsMedical;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMedical = null;
					else entity.IsMedical = Convert.ToBoolean(value);
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
			

			private esHumanBasePeriod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHumanBasePeriodQuery query)
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
				throw new Exception("esHumanBasePeriod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class HumanBasePeriod : esHumanBasePeriod
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
	abstract public class esHumanBasePeriodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HumanBasePeriodMetadata.Meta();
			}
		}	
		

		public esQueryItem HumanBasePeriodID
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.HumanBasePeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem YearPeriod
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.YearPeriod, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodeName
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.PeriodeName, esSystemType.String);
			}
		} 
		
		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsRecruitment
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.IsRecruitment, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsTraining
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.IsTraining, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCareer
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.IsCareer, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAppraisal
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.IsAppraisal, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsMedical
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.IsMedical, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HumanBasePeriodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HumanBasePeriodCollection")]
	public partial class HumanBasePeriodCollection : esHumanBasePeriodCollection, IEnumerable<HumanBasePeriod>
	{
		public HumanBasePeriodCollection()
		{

		}
		
		public static implicit operator List<HumanBasePeriod>(HumanBasePeriodCollection coll)
		{
			List<HumanBasePeriod> list = new List<HumanBasePeriod>();
			
			foreach (HumanBasePeriod emp in coll)
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
				return  HumanBasePeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HumanBasePeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HumanBasePeriod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HumanBasePeriod();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HumanBasePeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HumanBasePeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HumanBasePeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public HumanBasePeriod AddNew()
		{
			HumanBasePeriod entity = base.AddNewEntity() as HumanBasePeriod;
			
			return entity;
		}

		public HumanBasePeriod FindByPrimaryKey(System.Int32 humanBasePeriodID)
		{
			return base.FindByPrimaryKey(humanBasePeriodID) as HumanBasePeriod;
		}


		#region IEnumerable<HumanBasePeriod> Members

		IEnumerator<HumanBasePeriod> IEnumerable<HumanBasePeriod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as HumanBasePeriod;
			}
		}

		#endregion
		
		private HumanBasePeriodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HumanBasePeriod' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("HumanBasePeriod ({HumanBasePeriodID})")]
	[Serializable]
	public partial class HumanBasePeriod : esHumanBasePeriod
	{
		public HumanBasePeriod()
		{

		}
	
		public HumanBasePeriod(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HumanBasePeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esHumanBasePeriodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HumanBasePeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HumanBasePeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HumanBasePeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HumanBasePeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HumanBasePeriodQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HumanBasePeriodQuery : esHumanBasePeriodQuery
	{
		public HumanBasePeriodQuery()
		{

		}		
		
		public HumanBasePeriodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HumanBasePeriodQuery";
        }
		
			
	}


	[Serializable]
	public partial class HumanBasePeriodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HumanBasePeriodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.HumanBasePeriodID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.HumanBasePeriodID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.YearPeriod, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.YearPeriod;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.PeriodeName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.PeriodeName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.StartDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.EndDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.IsRecruitment, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.IsRecruitment;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.IsTraining, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.IsTraining;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.IsCareer, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.IsCareer;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.IsAppraisal, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.IsAppraisal;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.IsMedical, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.IsMedical;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(HumanBasePeriodMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = HumanBasePeriodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HumanBasePeriodMetadata Meta()
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
			 public const string HumanBasePeriodID = "HumanBasePeriodID";
			 public const string YearPeriod = "YearPeriod";
			 public const string PeriodeName = "PeriodeName";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string IsRecruitment = "IsRecruitment";
			 public const string IsTraining = "IsTraining";
			 public const string IsCareer = "IsCareer";
			 public const string IsAppraisal = "IsAppraisal";
			 public const string IsMedical = "IsMedical";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string HumanBasePeriodID = "HumanBasePeriodID";
			 public const string YearPeriod = "YearPeriod";
			 public const string PeriodeName = "PeriodeName";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string IsRecruitment = "IsRecruitment";
			 public const string IsTraining = "IsTraining";
			 public const string IsCareer = "IsCareer";
			 public const string IsAppraisal = "IsAppraisal";
			 public const string IsMedical = "IsMedical";
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
			lock (typeof(HumanBasePeriodMetadata))
			{
				if(HumanBasePeriodMetadata.mapDelegates == null)
				{
					HumanBasePeriodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HumanBasePeriodMetadata.meta == null)
				{
					HumanBasePeriodMetadata.meta = new HumanBasePeriodMetadata();
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
				

				meta.AddTypeMap("HumanBasePeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YearPeriod", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PeriodeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsRecruitment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTraining", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCareer", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAppraisal", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMedical", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "HumanBasePeriod";
				meta.Destination = "HumanBasePeriod";
				
				meta.spInsert = "proc_HumanBasePeriodInsert";				
				meta.spUpdate = "proc_HumanBasePeriodUpdate";		
				meta.spDelete = "proc_HumanBasePeriodDelete";
				meta.spLoadAll = "proc_HumanBasePeriodLoadAll";
				meta.spLoadByPrimaryKey = "proc_HumanBasePeriodLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HumanBasePeriodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
