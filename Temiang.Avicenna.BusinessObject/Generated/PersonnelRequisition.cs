/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:22 PM
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
	abstract public class esPersonnelRequisitionCollection : esEntityCollectionWAuditLog
	{
		public esPersonnelRequisitionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PersonnelRequisitionCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonnelRequisitionQuery query)
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
			this.InitQuery(query as esPersonnelRequisitionQuery);
		}
		#endregion
		
		virtual public PersonnelRequisition DetachEntity(PersonnelRequisition entity)
		{
			return base.DetachEntity(entity) as PersonnelRequisition;
		}
		
		virtual public PersonnelRequisition AttachEntity(PersonnelRequisition entity)
		{
			return base.AttachEntity(entity) as PersonnelRequisition;
		}
		
		virtual public void Combine(PersonnelRequisitionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PersonnelRequisition this[int index]
		{
			get
			{
				return base[index] as PersonnelRequisition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonnelRequisition);
		}
	}



	[Serializable]
	abstract public class esPersonnelRequisition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonnelRequisitionQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonnelRequisition()
		{

		}

		public esPersonnelRequisition(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 personnelRequisitionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personnelRequisitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(personnelRequisitionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 personnelRequisitionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personnelRequisitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(personnelRequisitionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 personnelRequisitionID)
		{
			esPersonnelRequisitionQuery query = this.GetDynamicQuery();
			query.Where(query.PersonnelRequisitionID == personnelRequisitionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 personnelRequisitionID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonnelRequisitionID",personnelRequisitionID);
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
						case "PersonnelRequisitionID": this.str.PersonnelRequisitionID = (string)value; break;							
						case "RequestedByPersonID": this.str.RequestedByPersonID = (string)value; break;							
						case "NumberOfRequiredEmployee": this.str.NumberOfRequiredEmployee = (string)value; break;							
						case "SRRequestStatus": this.str.SRRequestStatus = (string)value; break;							
						case "RecruitmentPlanID": this.str.RecruitmentPlanID = (string)value; break;							
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;							
						case "SRPreferredSource": this.str.SRPreferredSource = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "Reason": this.str.Reason = (string)value; break;							
						case "MiscellaneousSpec": this.str.MiscellaneousSpec = (string)value; break;							
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;							
						case "ProbationMonth": this.str.ProbationMonth = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PersonnelRequisitionID":
						
							if (value == null || value is System.Int32)
								this.PersonnelRequisitionID = (System.Int32?)value;
							break;
						
						case "RequestedByPersonID":
						
							if (value == null || value is System.Int32)
								this.RequestedByPersonID = (System.Int32?)value;
							break;
						
						case "NumberOfRequiredEmployee":
						
							if (value == null || value is System.Int32)
								this.NumberOfRequiredEmployee = (System.Int32?)value;
							break;
						
						case "RecruitmentPlanID":
						
							if (value == null || value is System.Int32)
								this.RecruitmentPlanID = (System.Int32?)value;
							break;
						
						case "OrganizationUnitID":
						
							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						
						case "ProbationMonth":
						
							if (value == null || value is System.Int32)
								this.ProbationMonth = (System.Int32?)value;
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
		/// Maps to PersonnelRequisition.PersonnelRequisitionID
		/// </summary>
		virtual public System.Int32? PersonnelRequisitionID
		{
			get
			{
				return base.GetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID);
			}
			
			set
			{
				base.SetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.RequestedByPersonID
		/// </summary>
		virtual public System.Int32? RequestedByPersonID
		{
			get
			{
				return base.GetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.RequestedByPersonID);
			}
			
			set
			{
				base.SetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.RequestedByPersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.NumberOfRequiredEmployee
		/// </summary>
		virtual public System.Int32? NumberOfRequiredEmployee
		{
			get
			{
				return base.GetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.NumberOfRequiredEmployee);
			}
			
			set
			{
				base.SetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.NumberOfRequiredEmployee, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.SRRequestStatus
		/// </summary>
		virtual public System.String SRRequestStatus
		{
			get
			{
				return base.GetSystemString(PersonnelRequisitionMetadata.ColumnNames.SRRequestStatus);
			}
			
			set
			{
				base.SetSystemString(PersonnelRequisitionMetadata.ColumnNames.SRRequestStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.RecruitmentPlanID
		/// </summary>
		virtual public System.Int32? RecruitmentPlanID
		{
			get
			{
				return base.GetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.RecruitmentPlanID);
			}
			
			set
			{
				base.SetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.RecruitmentPlanID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.OrganizationUnitID);
			}
			
			set
			{
				base.SetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.SRPreferredSource
		/// </summary>
		virtual public System.String SRPreferredSource
		{
			get
			{
				return base.GetSystemString(PersonnelRequisitionMetadata.ColumnNames.SRPreferredSource);
			}
			
			set
			{
				base.SetSystemString(PersonnelRequisitionMetadata.ColumnNames.SRPreferredSource, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(PersonnelRequisitionMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(PersonnelRequisitionMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(PersonnelRequisitionMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(PersonnelRequisitionMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(PersonnelRequisitionMetadata.ColumnNames.Reason);
			}
			
			set
			{
				base.SetSystemString(PersonnelRequisitionMetadata.ColumnNames.Reason, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.MiscellaneousSpec
		/// </summary>
		virtual public System.String MiscellaneousSpec
		{
			get
			{
				return base.GetSystemString(PersonnelRequisitionMetadata.ColumnNames.MiscellaneousSpec);
			}
			
			set
			{
				base.SetSystemString(PersonnelRequisitionMetadata.ColumnNames.MiscellaneousSpec, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(PersonnelRequisitionMetadata.ColumnNames.SREmploymentType);
			}
			
			set
			{
				base.SetSystemString(PersonnelRequisitionMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.ProbationMonth
		/// </summary>
		virtual public System.Int32? ProbationMonth
		{
			get
			{
				return base.GetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.ProbationMonth);
			}
			
			set
			{
				base.SetSystemInt32(PersonnelRequisitionMetadata.ColumnNames.ProbationMonth, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonnelRequisition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonnelRequisition entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PersonnelRequisitionID
			{
				get
				{
					System.Int32? data = entity.PersonnelRequisitionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonnelRequisitionID = null;
					else entity.PersonnelRequisitionID = Convert.ToInt32(value);
				}
			}
				
			public System.String RequestedByPersonID
			{
				get
				{
					System.Int32? data = entity.RequestedByPersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestedByPersonID = null;
					else entity.RequestedByPersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String NumberOfRequiredEmployee
			{
				get
				{
					System.Int32? data = entity.NumberOfRequiredEmployee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfRequiredEmployee = null;
					else entity.NumberOfRequiredEmployee = Convert.ToInt32(value);
				}
			}
				
			public System.String SRRequestStatus
			{
				get
				{
					System.String data = entity.SRRequestStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRequestStatus = null;
					else entity.SRRequestStatus = Convert.ToString(value);
				}
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
				
			public System.String SRPreferredSource
			{
				get
				{
					System.String data = entity.SRPreferredSource;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPreferredSource = null;
					else entity.SRPreferredSource = Convert.ToString(value);
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
				
			public System.String Reason
			{
				get
				{
					System.String data = entity.Reason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reason = null;
					else entity.Reason = Convert.ToString(value);
				}
			}
				
			public System.String MiscellaneousSpec
			{
				get
				{
					System.String data = entity.MiscellaneousSpec;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MiscellaneousSpec = null;
					else entity.MiscellaneousSpec = Convert.ToString(value);
				}
			}
				
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
				}
			}
				
			public System.String ProbationMonth
			{
				get
				{
					System.Int32? data = entity.ProbationMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProbationMonth = null;
					else entity.ProbationMonth = Convert.ToInt32(value);
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
			

			private esPersonnelRequisition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonnelRequisitionQuery query)
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
				throw new Exception("esPersonnelRequisition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PersonnelRequisition : esPersonnelRequisition
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
	abstract public class esPersonnelRequisitionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PersonnelRequisitionMetadata.Meta();
			}
		}	
		

		public esQueryItem PersonnelRequisitionID
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RequestedByPersonID
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.RequestedByPersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NumberOfRequiredEmployee
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.NumberOfRequiredEmployee, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRequestStatus
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.SRRequestStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem RecruitmentPlanID
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.RecruitmentPlanID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPreferredSource
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.SRPreferredSource, esSystemType.String);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.Reason, esSystemType.String);
			}
		} 
		
		public esQueryItem MiscellaneousSpec
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.MiscellaneousSpec, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		} 
		
		public esQueryItem ProbationMonth
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.ProbationMonth, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonnelRequisitionCollection")]
	public partial class PersonnelRequisitionCollection : esPersonnelRequisitionCollection, IEnumerable<PersonnelRequisition>
	{
		public PersonnelRequisitionCollection()
		{

		}
		
		public static implicit operator List<PersonnelRequisition>(PersonnelRequisitionCollection coll)
		{
			List<PersonnelRequisition> list = new List<PersonnelRequisition>();
			
			foreach (PersonnelRequisition emp in coll)
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
				return  PersonnelRequisitionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonnelRequisitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonnelRequisition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonnelRequisition();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PersonnelRequisitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonnelRequisitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PersonnelRequisitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PersonnelRequisition AddNew()
		{
			PersonnelRequisition entity = base.AddNewEntity() as PersonnelRequisition;
			
			return entity;
		}

		public PersonnelRequisition FindByPrimaryKey(System.Int32 personnelRequisitionID)
		{
			return base.FindByPrimaryKey(personnelRequisitionID) as PersonnelRequisition;
		}


		#region IEnumerable<PersonnelRequisition> Members

		IEnumerator<PersonnelRequisition> IEnumerable<PersonnelRequisition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PersonnelRequisition;
			}
		}

		#endregion
		
		private PersonnelRequisitionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonnelRequisition' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PersonnelRequisition ({PersonnelRequisitionID})")]
	[Serializable]
	public partial class PersonnelRequisition : esPersonnelRequisition
	{
		public PersonnelRequisition()
		{

		}
	
		public PersonnelRequisition(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonnelRequisitionMetadata.Meta();
			}
		}
		
		
		
		override protected esPersonnelRequisitionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonnelRequisitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PersonnelRequisitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonnelRequisitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PersonnelRequisitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PersonnelRequisitionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PersonnelRequisitionQuery : esPersonnelRequisitionQuery
	{
		public PersonnelRequisitionQuery()
		{

		}		
		
		public PersonnelRequisitionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PersonnelRequisitionQuery";
        }
		
			
	}


	[Serializable]
	public partial class PersonnelRequisitionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonnelRequisitionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.PersonnelRequisitionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.RequestedByPersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.RequestedByPersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.NumberOfRequiredEmployee, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.NumberOfRequiredEmployee;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.SRRequestStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.SRRequestStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.RecruitmentPlanID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.RecruitmentPlanID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.OrganizationUnitID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.SRPreferredSource, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.SRPreferredSource;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.ValidFrom, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.ValidTo, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.ValidTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.Reason, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.MiscellaneousSpec, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.MiscellaneousSpec;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.SREmploymentType, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.ProbationMonth, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.ProbationMonth;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonnelRequisitionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PersonnelRequisitionMetadata Meta()
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
			 public const string PersonnelRequisitionID = "PersonnelRequisitionID";
			 public const string RequestedByPersonID = "RequestedByPersonID";
			 public const string NumberOfRequiredEmployee = "NumberOfRequiredEmployee";
			 public const string SRRequestStatus = "SRRequestStatus";
			 public const string RecruitmentPlanID = "RecruitmentPlanID";
			 public const string OrganizationUnitID = "OrganizationUnitID";
			 public const string SRPreferredSource = "SRPreferredSource";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string Reason = "Reason";
			 public const string MiscellaneousSpec = "MiscellaneousSpec";
			 public const string SREmploymentType = "SREmploymentType";
			 public const string ProbationMonth = "ProbationMonth";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PersonnelRequisitionID = "PersonnelRequisitionID";
			 public const string RequestedByPersonID = "RequestedByPersonID";
			 public const string NumberOfRequiredEmployee = "NumberOfRequiredEmployee";
			 public const string SRRequestStatus = "SRRequestStatus";
			 public const string RecruitmentPlanID = "RecruitmentPlanID";
			 public const string OrganizationUnitID = "OrganizationUnitID";
			 public const string SRPreferredSource = "SRPreferredSource";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string Reason = "Reason";
			 public const string MiscellaneousSpec = "MiscellaneousSpec";
			 public const string SREmploymentType = "SREmploymentType";
			 public const string ProbationMonth = "ProbationMonth";
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
			lock (typeof(PersonnelRequisitionMetadata))
			{
				if(PersonnelRequisitionMetadata.mapDelegates == null)
				{
					PersonnelRequisitionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PersonnelRequisitionMetadata.meta == null)
				{
					PersonnelRequisitionMetadata.meta = new PersonnelRequisitionMetadata();
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
				

				meta.AddTypeMap("PersonnelRequisitionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RequestedByPersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NumberOfRequiredEmployee", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRequestStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecruitmentPlanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPreferredSource", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MiscellaneousSpec", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProbationMonth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PersonnelRequisition";
				meta.Destination = "PersonnelRequisition";
				
				meta.spInsert = "proc_PersonnelRequisitionInsert";				
				meta.spUpdate = "proc_PersonnelRequisitionUpdate";		
				meta.spDelete = "proc_PersonnelRequisitionDelete";
				meta.spLoadAll = "proc_PersonnelRequisitionLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonnelRequisitionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonnelRequisitionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
