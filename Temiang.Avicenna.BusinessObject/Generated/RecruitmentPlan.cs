/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/11/2016 4:02:09 PM
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
	abstract public class esRecruitmentPlanCollection : esEntityCollectionWAuditLog
	{
		public esRecruitmentPlanCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RecruitmentPlanCollection";
		}

		#region Query Logic
		protected void InitQuery(esRecruitmentPlanQuery query)
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
			this.InitQuery(query as esRecruitmentPlanQuery);
		}
		#endregion
		
		virtual public RecruitmentPlan DetachEntity(RecruitmentPlan entity)
		{
			return base.DetachEntity(entity) as RecruitmentPlan;
		}
		
		virtual public RecruitmentPlan AttachEntity(RecruitmentPlan entity)
		{
			return base.AttachEntity(entity) as RecruitmentPlan;
		}
		
		virtual public void Combine(RecruitmentPlanCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RecruitmentPlan this[int index]
		{
			get
			{
				return base[index] as RecruitmentPlan;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RecruitmentPlan);
		}
	}



	[Serializable]
	abstract public class esRecruitmentPlan : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRecruitmentPlanQuery GetDynamicQuery()
		{
			return null;
		}

		public esRecruitmentPlan()
		{

		}

		public esRecruitmentPlan(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 recruitmentPlanID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recruitmentPlanID);
			else
				return LoadByPrimaryKeyStoredProcedure(recruitmentPlanID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 recruitmentPlanID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recruitmentPlanID);
			else
				return LoadByPrimaryKeyStoredProcedure(recruitmentPlanID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 recruitmentPlanID)
		{
			esRecruitmentPlanQuery query = this.GetDynamicQuery();
			query.Where(query.RecruitmentPlanID == recruitmentPlanID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 recruitmentPlanID)
		{
			esParameters parms = new esParameters();
			parms.Add("RecruitmentPlanID",recruitmentPlanID);
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
						case "RecruitmentPlanID": this.str.RecruitmentPlanID = (string)value; break;							
						case "RecruitmentPlanName": this.str.RecruitmentPlanName = (string)value; break;
						case "DivisionID": this.str.DivisionID = (string)value; break;
						case "SubDivisionID": this.str.SubDivisionID = (string)value; break;
						case "SectionID": this.str.SectionID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "NumberOfRequestedEmployees": this.str.NumberOfRequestedEmployees = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RecruitmentPlanID":
						
							if (value == null || value is System.Int32)
								this.RecruitmentPlanID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						
						case "NumberOfRequestedEmployees":
						
							if (value == null || value is System.Int32)
								this.NumberOfRequestedEmployees = (System.Int32?)value;
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
		/// Maps to RecruitmentPlan.RecruitmentPlanID
		/// </summary>
		virtual public System.Int32? RecruitmentPlanID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanID);
			}
			
			set
			{
				base.SetSystemInt32(RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentPlan.RecruitmentPlanName
		/// </summary>
		virtual public System.String RecruitmentPlanName
		{
			get
			{
				return base.GetSystemString(RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanName);
			}
			
			set
			{
				base.SetSystemString(RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanName, value);
			}
		}

		/// <summary>
		/// Maps to RecruitmentPlan.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentPlanMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(RecruitmentPlanMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}

		/// <summary>
		/// Maps to RecruitmentPlan.DivisionID
		/// </summary>
		virtual public System.Int32? DivisionID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentPlanMetadata.ColumnNames.DivisionID);
			}

			set
			{
				base.SetSystemInt32(RecruitmentPlanMetadata.ColumnNames.DivisionID, value);
			}
		}

		/// <summary>
		/// Maps to RecruitmentPlan.SubDivisionID
		/// </summary>
		virtual public System.Int32? SubDivisionID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentPlanMetadata.ColumnNames.SubDivisionID);
			}

			set
			{
				base.SetSystemInt32(RecruitmentPlanMetadata.ColumnNames.SubDivisionID, value);
			}
		}

		/// <summary>
		/// Maps to RecruitmentPlan.SectionID
		/// </summary>
		virtual public System.Int32? SectionID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentPlanMetadata.ColumnNames.SectionID);
			}

			set
			{
				base.SetSystemInt32(RecruitmentPlanMetadata.ColumnNames.SectionID, value);
			}
		}

		/// <summary>
		/// Maps to RecruitmentPlan.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentPlanMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(RecruitmentPlanMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentPlan.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(RecruitmentPlanMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(RecruitmentPlanMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentPlan.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(RecruitmentPlanMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(RecruitmentPlanMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentPlan.NumberOfRequestedEmployees
		/// </summary>
		virtual public System.Int32? NumberOfRequestedEmployees
		{
			get
			{
				return base.GetSystemInt32(RecruitmentPlanMetadata.ColumnNames.NumberOfRequestedEmployees);
			}
			
			set
			{
				base.SetSystemInt32(RecruitmentPlanMetadata.ColumnNames.NumberOfRequestedEmployees, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentPlan.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RecruitmentPlanMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RecruitmentPlanMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentPlan.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RecruitmentPlanMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RecruitmentPlanMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentPlan.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(RecruitmentPlanMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(RecruitmentPlanMetadata.ColumnNames.Notes, value);
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
			public esStrings(esRecruitmentPlan entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RecruitmentPlanID
			{
				get
				{
					System.Int32? data = entity.RecruitmentPlanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecruitmentPlanID = null;
					else entity.RecruitmentPlanID = Convert.ToInt32(value);
				}
			}
				
			public System.String RecruitmentPlanName
			{
				get
				{
					System.String data = entity.RecruitmentPlanName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecruitmentPlanName = null;
					else entity.RecruitmentPlanName = Convert.ToString(value);
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

			public System.String DivisionID
			{
				get
				{
					System.Int32? data = entity.DivisionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DivisionID = null;
					else entity.DivisionID = Convert.ToInt32(value);
				}
			}

			public System.String SubDivisionID
			{
				get
				{
					System.Int32? data = entity.SubDivisionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubDivisionID = null;
					else entity.SubDivisionID = Convert.ToInt32(value);
				}
			}
			public System.String SectionID
			{
				get
				{
					System.Int32? data = entity.SectionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SectionID = null;
					else entity.SectionID = Convert.ToInt32(value);
				}
			}


			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
				}
			}
				
			public System.String NumberOfRequestedEmployees
			{
				get
				{
					System.Int32? data = entity.NumberOfRequestedEmployees;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfRequestedEmployees = null;
					else entity.NumberOfRequestedEmployees = Convert.ToInt32(value);
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
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			

			private esRecruitmentPlan entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRecruitmentPlanQuery query)
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
				throw new Exception("esRecruitmentPlan can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RecruitmentPlan : esRecruitmentPlan
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
	abstract public class esRecruitmentPlanQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RecruitmentPlanMetadata.Meta();
			}
		}	
		

		public esQueryItem RecruitmentPlanID
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RecruitmentPlanName
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanName, esSystemType.String);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem DivisionID
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.DivisionID, esSystemType.Int32);
			}
		}

		public esQueryItem SubDivisionID
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.SubDivisionID, esSystemType.Int32);
			}
		}

		public esQueryItem SectionID
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.SectionID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NumberOfRequestedEmployees
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.NumberOfRequestedEmployees, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, RecruitmentPlanMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RecruitmentPlanCollection")]
	public partial class RecruitmentPlanCollection : esRecruitmentPlanCollection, IEnumerable<RecruitmentPlan>
	{
		public RecruitmentPlanCollection()
		{

		}
		
		public static implicit operator List<RecruitmentPlan>(RecruitmentPlanCollection coll)
		{
			List<RecruitmentPlan> list = new List<RecruitmentPlan>();
			
			foreach (RecruitmentPlan emp in coll)
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
				return  RecruitmentPlanMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecruitmentPlanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RecruitmentPlan(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RecruitmentPlan();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RecruitmentPlanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecruitmentPlanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RecruitmentPlanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RecruitmentPlan AddNew()
		{
			RecruitmentPlan entity = base.AddNewEntity() as RecruitmentPlan;
			
			return entity;
		}

		public RecruitmentPlan FindByPrimaryKey(System.Int32 recruitmentPlanID)
		{
			return base.FindByPrimaryKey(recruitmentPlanID) as RecruitmentPlan;
		}


		#region IEnumerable<RecruitmentPlan> Members

		IEnumerator<RecruitmentPlan> IEnumerable<RecruitmentPlan>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RecruitmentPlan;
			}
		}

		#endregion
		
		private RecruitmentPlanQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RecruitmentPlan' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RecruitmentPlan ({RecruitmentPlanID})")]
	[Serializable]
	public partial class RecruitmentPlan : esRecruitmentPlan
	{
		public RecruitmentPlan()
		{

		}
	
		public RecruitmentPlan(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RecruitmentPlanMetadata.Meta();
			}
		}
		
		
		
		override protected esRecruitmentPlanQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecruitmentPlanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RecruitmentPlanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecruitmentPlanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RecruitmentPlanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RecruitmentPlanQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RecruitmentPlanQuery : esRecruitmentPlanQuery
	{
		public RecruitmentPlanQuery()
		{

		}		
		
		public RecruitmentPlanQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RecruitmentPlanQuery";
        }
		
			
	}


	[Serializable]
	public partial class RecruitmentPlanMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RecruitmentPlanMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.RecruitmentPlanID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.RecruitmentPlanName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.RecruitmentPlanName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.OrganizationUnitID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.DivisionID, 3, typeof(System.Int32), esSystemType.String);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.DivisionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.SubDivisionID, 4, typeof(System.Int32), esSystemType.String);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.SubDivisionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.SectionID, 5, typeof(System.Int32), esSystemType.String);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.SectionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.PositionID, 6, typeof(System.Int32), esSystemType.String);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.ValidFrom, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.ValidTo, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.ValidTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.NumberOfRequestedEmployees, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.NumberOfRequestedEmployees;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentPlanMetadata.ColumnNames.Notes, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RecruitmentPlanMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RecruitmentPlanMetadata Meta()
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
			 public const string RecruitmentPlanID = "RecruitmentPlanID";
			 public const string RecruitmentPlanName = "RecruitmentPlanName";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string DivisionID = "DivisionID";
			public const string SubDivisionID = "SubDivisionID";
			public const string SectionID = "SectionID";
			public const string PositionID = "PositionID";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string NumberOfRequestedEmployees = "NumberOfRequestedEmployees";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string Notes = "Notes";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RecruitmentPlanID = "RecruitmentPlanID";
			 public const string RecruitmentPlanName = "RecruitmentPlanName";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string DivisionID = "DivisionID";
			public const string SubDivisionID = "SubDivisionID";
			public const string SectionID = "SectionID";
			public const string PositionID = "PositionID";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string NumberOfRequestedEmployees = "NumberOfRequestedEmployees";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string Notes = "Notes";
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
			lock (typeof(RecruitmentPlanMetadata))
			{
				if(RecruitmentPlanMetadata.mapDelegates == null)
				{
					RecruitmentPlanMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RecruitmentPlanMetadata.meta == null)
				{
					RecruitmentPlanMetadata.meta = new RecruitmentPlanMetadata();
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
				

				meta.AddTypeMap("RecruitmentPlanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RecruitmentPlanName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DivisionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubDivisionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SectionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("NumberOfRequestedEmployees", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("text", "System.String"));			
				
				
				
				meta.Source = "RecruitmentPlan";
				meta.Destination = "RecruitmentPlan";
				
				meta.spInsert = "proc_RecruitmentPlanInsert";				
				meta.spUpdate = "proc_RecruitmentPlanUpdate";		
				meta.spDelete = "proc_RecruitmentPlanDelete";
				meta.spLoadAll = "proc_RecruitmentPlanLoadAll";
				meta.spLoadByPrimaryKey = "proc_RecruitmentPlanLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RecruitmentPlanMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
