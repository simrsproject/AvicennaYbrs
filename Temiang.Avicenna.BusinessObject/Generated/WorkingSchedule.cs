/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/26/2021 12:11:43 PM
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
	abstract public class esWorkingScheduleCollection : esEntityCollectionWAuditLog
	{
		public esWorkingScheduleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WorkingScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingScheduleQuery query)
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
			this.InitQuery(query as esWorkingScheduleQuery);
		}
		#endregion
		
		virtual public WorkingSchedule DetachEntity(WorkingSchedule entity)
		{
			return base.DetachEntity(entity) as WorkingSchedule;
		}
		
		virtual public WorkingSchedule AttachEntity(WorkingSchedule entity)
		{
			return base.AttachEntity(entity) as WorkingSchedule;
		}
		
		virtual public void Combine(WorkingScheduleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WorkingSchedule this[int index]
		{
			get
			{
				return base[index] as WorkingSchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingSchedule);
		}
	}



	[Serializable]
	abstract public class esWorkingSchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingSchedule()
		{

		}

		public esWorkingSchedule(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 workingScheduleID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingScheduleID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingScheduleID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 workingScheduleID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingScheduleID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingScheduleID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 workingScheduleID)
		{
			esWorkingScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.WorkingScheduleID == workingScheduleID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 workingScheduleID)
		{
			esParameters parms = new esParameters();
			parms.Add("WorkingScheduleID",workingScheduleID);
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
						case "WorkingScheduleID": this.str.WorkingScheduleID = (string)value; break;							
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;							
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "WorkingScheduleID":
						
							if (value == null || value is System.Int32)
								this.WorkingScheduleID = (System.Int32?)value;
							break;
						
						case "PayrollPeriodID":
						
							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						
						case "OrganizationUnitID":
						
							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to WorkingSchedule.WorkingScheduleID
		/// </summary>
		virtual public System.Int32? WorkingScheduleID
		{
			get
			{
				return base.GetSystemInt32(WorkingScheduleMetadata.ColumnNames.WorkingScheduleID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingScheduleMetadata.ColumnNames.WorkingScheduleID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchedule.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(WorkingScheduleMetadata.ColumnNames.PayrollPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingScheduleMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchedule.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(WorkingScheduleMetadata.ColumnNames.OrganizationUnitID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingScheduleMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchedule.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(WorkingScheduleMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(WorkingScheduleMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchedule.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(WorkingScheduleMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(WorkingScheduleMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingSchedule.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(WorkingScheduleMetadata.ColumnNames.LastUpdateUserID);
			}
			
			set
			{
				base.SetSystemString(WorkingScheduleMetadata.ColumnNames.LastUpdateUserID, value);
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
			public esStrings(esWorkingSchedule entity)
			{
				this.entity = entity;
			}
			
	
			public System.String WorkingScheduleID
			{
				get
				{
					System.Int32? data = entity.WorkingScheduleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingScheduleID = null;
					else entity.WorkingScheduleID = Convert.ToInt32(value);
				}
			}
				
			public System.String PayrollPeriodID
			{
				get
				{
					System.Int32? data = entity.PayrollPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
					else entity.PayrollPeriodID = Convert.ToInt32(value);
				}
			}
				
			public System.String OrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.OrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
					else entity.OrganizationUnitID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
				
			public System.String LastUpdateUserID
			{
				get
				{
					System.String data = entity.LastUpdateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateUserID = null;
					else entity.LastUpdateUserID = Convert.ToString(value);
				}
			}
			

			private esWorkingSchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingScheduleQuery query)
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
				throw new Exception("esWorkingSchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class WorkingSchedule : esWorkingSchedule
	{

				
		#region WorkingSchduleInterventionCollectionByWorkingScheduleID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK__WorkingSc__Worki__4C98A3C6
		/// </summary>

		[XmlIgnore]
		public WorkingSchduleInterventionCollection WorkingSchduleInterventionCollectionByWorkingScheduleID
		{
			get
			{
				if(this._WorkingSchduleInterventionCollectionByWorkingScheduleID == null)
				{
					this._WorkingSchduleInterventionCollectionByWorkingScheduleID = new WorkingSchduleInterventionCollection();
					this._WorkingSchduleInterventionCollectionByWorkingScheduleID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("WorkingSchduleInterventionCollectionByWorkingScheduleID", this._WorkingSchduleInterventionCollectionByWorkingScheduleID);
				
					if(this.WorkingScheduleID != null)
					{
						this._WorkingSchduleInterventionCollectionByWorkingScheduleID.Query.Where(this._WorkingSchduleInterventionCollectionByWorkingScheduleID.Query.WorkingScheduleID == this.WorkingScheduleID);
						this._WorkingSchduleInterventionCollectionByWorkingScheduleID.Query.Load();

						// Auto-hookup Foreign Keys
						this._WorkingSchduleInterventionCollectionByWorkingScheduleID.fks.Add(WorkingSchduleInterventionMetadata.ColumnNames.WorkingScheduleID, this.WorkingScheduleID);
					}
				}

				return this._WorkingSchduleInterventionCollectionByWorkingScheduleID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._WorkingSchduleInterventionCollectionByWorkingScheduleID != null) 
				{ 
					this.RemovePostSave("WorkingSchduleInterventionCollectionByWorkingScheduleID"); 
					this._WorkingSchduleInterventionCollectionByWorkingScheduleID = null;
					
				} 
			} 			
		}

		private WorkingSchduleInterventionCollection _WorkingSchduleInterventionCollectionByWorkingScheduleID;
		#endregion

				
		#region WorkingScheduleDetailCollectionByWorkingScheduleID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK__WorkingSc__Worki__4AB05B54
		/// </summary>

		[XmlIgnore]
		public WorkingScheduleDetailCollection WorkingScheduleDetailCollectionByWorkingScheduleID
		{
			get
			{
				if(this._WorkingScheduleDetailCollectionByWorkingScheduleID == null)
				{
					this._WorkingScheduleDetailCollectionByWorkingScheduleID = new WorkingScheduleDetailCollection();
					this._WorkingScheduleDetailCollectionByWorkingScheduleID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("WorkingScheduleDetailCollectionByWorkingScheduleID", this._WorkingScheduleDetailCollectionByWorkingScheduleID);
				
					if(this.WorkingScheduleID != null)
					{
						this._WorkingScheduleDetailCollectionByWorkingScheduleID.Query.Where(this._WorkingScheduleDetailCollectionByWorkingScheduleID.Query.WorkingScheduleID == this.WorkingScheduleID);
						this._WorkingScheduleDetailCollectionByWorkingScheduleID.Query.Load();

						// Auto-hookup Foreign Keys
						this._WorkingScheduleDetailCollectionByWorkingScheduleID.fks.Add(WorkingScheduleDetailMetadata.ColumnNames.WorkingScheduleID, this.WorkingScheduleID);
					}
				}

				return this._WorkingScheduleDetailCollectionByWorkingScheduleID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._WorkingScheduleDetailCollectionByWorkingScheduleID != null) 
				{ 
					this.RemovePostSave("WorkingScheduleDetailCollectionByWorkingScheduleID"); 
					this._WorkingScheduleDetailCollectionByWorkingScheduleID = null;
					
				} 
			} 			
		}

		private WorkingScheduleDetailCollection _WorkingScheduleDetailCollectionByWorkingScheduleID;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "WorkingSchduleInterventionCollectionByWorkingScheduleID", typeof(WorkingSchduleInterventionCollection), new WorkingSchduleIntervention()));
			props.Add(new esPropertyDescriptor(this, "WorkingScheduleDetailCollectionByWorkingScheduleID", typeof(WorkingScheduleDetailCollection), new WorkingScheduleDetail()));
		
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
			if(this._WorkingSchduleInterventionCollectionByWorkingScheduleID != null)
			{
				foreach(WorkingSchduleIntervention obj in this._WorkingSchduleInterventionCollectionByWorkingScheduleID)
				{
					if(obj.es.IsAdded)
					{
						obj.WorkingScheduleID = this.WorkingScheduleID;
					}
				}
			}
			if(this._WorkingScheduleDetailCollectionByWorkingScheduleID != null)
			{
				foreach(WorkingScheduleDetail obj in this._WorkingScheduleDetailCollectionByWorkingScheduleID)
				{
					if(obj.es.IsAdded)
					{
						obj.WorkingScheduleID = this.WorkingScheduleID;
					}
				}
			}
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
	abstract public class esWorkingScheduleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WorkingScheduleMetadata.Meta();
			}
		}	
		

		public esQueryItem WorkingScheduleID
		{
			get
			{
				return new esQueryItem(this, WorkingScheduleMetadata.ColumnNames.WorkingScheduleID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, WorkingScheduleMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, WorkingScheduleMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, WorkingScheduleMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, WorkingScheduleMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, WorkingScheduleMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingScheduleCollection")]
	public partial class WorkingScheduleCollection : esWorkingScheduleCollection, IEnumerable<WorkingSchedule>
	{
		public WorkingScheduleCollection()
		{

		}
		
		public static implicit operator List<WorkingSchedule>(WorkingScheduleCollection coll)
		{
			List<WorkingSchedule> list = new List<WorkingSchedule>();
			
			foreach (WorkingSchedule emp in coll)
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
				return  WorkingScheduleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingSchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingSchedule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WorkingScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingScheduleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WorkingScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WorkingSchedule AddNew()
		{
			WorkingSchedule entity = base.AddNewEntity() as WorkingSchedule;
			
			return entity;
		}

		public WorkingSchedule FindByPrimaryKey(System.Int32 workingScheduleID)
		{
			return base.FindByPrimaryKey(workingScheduleID) as WorkingSchedule;
		}


		#region IEnumerable<WorkingSchedule> Members

		IEnumerator<WorkingSchedule> IEnumerable<WorkingSchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WorkingSchedule;
			}
		}

		#endregion
		
		private WorkingScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingSchedule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WorkingSchedule ({WorkingScheduleID})")]
	[Serializable]
	public partial class WorkingSchedule : esWorkingSchedule
	{
		public WorkingSchedule()
		{

		}
	
		public WorkingSchedule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingScheduleMetadata.Meta();
			}
		}
		
		
		
		override protected esWorkingScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WorkingScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingScheduleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WorkingScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WorkingScheduleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WorkingScheduleQuery : esWorkingScheduleQuery
	{
		public WorkingScheduleQuery()
		{

		}		
		
		public WorkingScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WorkingScheduleQuery";
        }
		
			
	}


	[Serializable]
	public partial class WorkingScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingScheduleMetadata.ColumnNames.WorkingScheduleID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingScheduleMetadata.PropertyNames.WorkingScheduleID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingScheduleMetadata.ColumnNames.PayrollPeriodID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingScheduleMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingScheduleMetadata.ColumnNames.OrganizationUnitID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingScheduleMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingScheduleMetadata.ColumnNames.IsApproved, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingScheduleMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingScheduleMetadata.ColumnNames.IsVoid, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingScheduleMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingScheduleMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingScheduleMetadata.ColumnNames.LastUpdateUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingScheduleMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WorkingScheduleMetadata Meta()
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
			 public const string WorkingScheduleID = "WorkingScheduleID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string OrganizationUnitID = "OrganizationUnitID";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WorkingScheduleID = "WorkingScheduleID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string OrganizationUnitID = "OrganizationUnitID";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
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
			lock (typeof(WorkingScheduleMetadata))
			{
				if(WorkingScheduleMetadata.mapDelegates == null)
				{
					WorkingScheduleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WorkingScheduleMetadata.meta == null)
				{
					WorkingScheduleMetadata.meta = new WorkingScheduleMetadata();
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
				

				meta.AddTypeMap("WorkingScheduleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				meta.Source = "WorkingSchedule";
				meta.Destination = "WorkingSchedule";
				
				meta.spInsert = "proc_WorkingScheduleInsert";				
				meta.spUpdate = "proc_WorkingScheduleUpdate";		
				meta.spDelete = "proc_WorkingScheduleDelete";
				meta.spLoadAll = "proc_WorkingScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingScheduleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
