/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esAdvertisedPersonnelRequisitionCollection : esEntityCollectionWAuditLog
	{
		public esAdvertisedPersonnelRequisitionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AdvertisedPersonnelRequisitionCollection";
		}

		#region Query Logic
		protected void InitQuery(esAdvertisedPersonnelRequisitionQuery query)
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
			this.InitQuery(query as esAdvertisedPersonnelRequisitionQuery);
		}
		#endregion
		
		virtual public AdvertisedPersonnelRequisition DetachEntity(AdvertisedPersonnelRequisition entity)
		{
			return base.DetachEntity(entity) as AdvertisedPersonnelRequisition;
		}
		
		virtual public AdvertisedPersonnelRequisition AttachEntity(AdvertisedPersonnelRequisition entity)
		{
			return base.AttachEntity(entity) as AdvertisedPersonnelRequisition;
		}
		
		virtual public void Combine(AdvertisedPersonnelRequisitionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AdvertisedPersonnelRequisition this[int index]
		{
			get
			{
				return base[index] as AdvertisedPersonnelRequisition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AdvertisedPersonnelRequisition);
		}
	}



	[Serializable]
	abstract public class esAdvertisedPersonnelRequisition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAdvertisedPersonnelRequisitionQuery GetDynamicQuery()
		{
			return null;
		}

		public esAdvertisedPersonnelRequisition()
		{

		}

		public esAdvertisedPersonnelRequisition(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 advertisedPersonnelRequisitionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(advertisedPersonnelRequisitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(advertisedPersonnelRequisitionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 advertisedPersonnelRequisitionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(advertisedPersonnelRequisitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(advertisedPersonnelRequisitionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 advertisedPersonnelRequisitionID)
		{
			esAdvertisedPersonnelRequisitionQuery query = this.GetDynamicQuery();
			query.Where(query.AdvertisedPersonnelRequisitionID == advertisedPersonnelRequisitionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 advertisedPersonnelRequisitionID)
		{
			esParameters parms = new esParameters();
			parms.Add("AdvertisedPersonnelRequisitionID",advertisedPersonnelRequisitionID);
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
						case "AdvertisedPersonnelRequisitionID": this.str.AdvertisedPersonnelRequisitionID = (string)value; break;							
						case "JobOpportunityID": this.str.JobOpportunityID = (string)value; break;							
						case "PersonnelRequisitionID": this.str.PersonnelRequisitionID = (string)value; break;							
						case "EstimatedSalary": this.str.EstimatedSalary = (string)value; break;							
						case "MinimumEstimatedSalary": this.str.MinimumEstimatedSalary = (string)value; break;							
						case "MaximumEstimatedSalary": this.str.MaximumEstimatedSalary = (string)value; break;							
						case "JobDescription": this.str.JobDescription = (string)value; break;							
						case "JobSpecification": this.str.JobSpecification = (string)value; break;							
						case "ContactPerson": this.str.ContactPerson = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AdvertisedPersonnelRequisitionID":
						
							if (value == null || value is System.Int32)
								this.AdvertisedPersonnelRequisitionID = (System.Int32?)value;
							break;
						
						case "JobOpportunityID":
						
							if (value == null || value is System.Int32)
								this.JobOpportunityID = (System.Int32?)value;
							break;
						
						case "PersonnelRequisitionID":
						
							if (value == null || value is System.Int32)
								this.PersonnelRequisitionID = (System.Int32?)value;
							break;
						
						case "EstimatedSalary":
						
							if (value == null || value is System.Decimal)
								this.EstimatedSalary = (System.Decimal?)value;
							break;
						
						case "MinimumEstimatedSalary":
						
							if (value == null || value is System.Decimal)
								this.MinimumEstimatedSalary = (System.Decimal?)value;
							break;
						
						case "MaximumEstimatedSalary":
						
							if (value == null || value is System.Decimal)
								this.MaximumEstimatedSalary = (System.Decimal?)value;
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
		/// Maps to AdvertisedPersonnelRequisition.AdvertisedPersonnelRequisitionID
		/// </summary>
		virtual public System.Int32? AdvertisedPersonnelRequisitionID
		{
			get
			{
				return base.GetSystemInt32(AdvertisedPersonnelRequisitionMetadata.ColumnNames.AdvertisedPersonnelRequisitionID);
			}
			
			set
			{
				base.SetSystemInt32(AdvertisedPersonnelRequisitionMetadata.ColumnNames.AdvertisedPersonnelRequisitionID, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.JobOpportunityID
		/// </summary>
		virtual public System.Int32? JobOpportunityID
		{
			get
			{
				return base.GetSystemInt32(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobOpportunityID);
			}
			
			set
			{
				base.SetSystemInt32(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobOpportunityID, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.PersonnelRequisitionID
		/// </summary>
		virtual public System.Int32? PersonnelRequisitionID
		{
			get
			{
				return base.GetSystemInt32(AdvertisedPersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID);
			}
			
			set
			{
				base.SetSystemInt32(AdvertisedPersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.EstimatedSalary
		/// </summary>
		virtual public System.Decimal? EstimatedSalary
		{
			get
			{
				return base.GetSystemDecimal(AdvertisedPersonnelRequisitionMetadata.ColumnNames.EstimatedSalary);
			}
			
			set
			{
				base.SetSystemDecimal(AdvertisedPersonnelRequisitionMetadata.ColumnNames.EstimatedSalary, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.MinimumEstimatedSalary
		/// </summary>
		virtual public System.Decimal? MinimumEstimatedSalary
		{
			get
			{
				return base.GetSystemDecimal(AdvertisedPersonnelRequisitionMetadata.ColumnNames.MinimumEstimatedSalary);
			}
			
			set
			{
				base.SetSystemDecimal(AdvertisedPersonnelRequisitionMetadata.ColumnNames.MinimumEstimatedSalary, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.MaximumEstimatedSalary
		/// </summary>
		virtual public System.Decimal? MaximumEstimatedSalary
		{
			get
			{
				return base.GetSystemDecimal(AdvertisedPersonnelRequisitionMetadata.ColumnNames.MaximumEstimatedSalary);
			}
			
			set
			{
				base.SetSystemDecimal(AdvertisedPersonnelRequisitionMetadata.ColumnNames.MaximumEstimatedSalary, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.JobDescription
		/// </summary>
		virtual public System.String JobDescription
		{
			get
			{
				return base.GetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobDescription);
			}
			
			set
			{
				base.SetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobDescription, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.JobSpecification
		/// </summary>
		virtual public System.String JobSpecification
		{
			get
			{
				return base.GetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobSpecification);
			}
			
			set
			{
				base.SetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobSpecification, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.ContactPerson
		/// </summary>
		virtual public System.String ContactPerson
		{
			get
			{
				return base.GetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.ContactPerson);
			}
			
			set
			{
				base.SetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.ContactPerson, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AdvertisedPersonnelRequisition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAdvertisedPersonnelRequisition entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AdvertisedPersonnelRequisitionID
			{
				get
				{
					System.Int32? data = entity.AdvertisedPersonnelRequisitionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdvertisedPersonnelRequisitionID = null;
					else entity.AdvertisedPersonnelRequisitionID = Convert.ToInt32(value);
				}
			}
				
			public System.String JobOpportunityID
			{
				get
				{
					System.Int32? data = entity.JobOpportunityID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobOpportunityID = null;
					else entity.JobOpportunityID = Convert.ToInt32(value);
				}
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
				
			public System.String EstimatedSalary
			{
				get
				{
					System.Decimal? data = entity.EstimatedSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EstimatedSalary = null;
					else entity.EstimatedSalary = Convert.ToDecimal(value);
				}
			}
				
			public System.String MinimumEstimatedSalary
			{
				get
				{
					System.Decimal? data = entity.MinimumEstimatedSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinimumEstimatedSalary = null;
					else entity.MinimumEstimatedSalary = Convert.ToDecimal(value);
				}
			}
				
			public System.String MaximumEstimatedSalary
			{
				get
				{
					System.Decimal? data = entity.MaximumEstimatedSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaximumEstimatedSalary = null;
					else entity.MaximumEstimatedSalary = Convert.ToDecimal(value);
				}
			}
				
			public System.String JobDescription
			{
				get
				{
					System.String data = entity.JobDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobDescription = null;
					else entity.JobDescription = Convert.ToString(value);
				}
			}
				
			public System.String JobSpecification
			{
				get
				{
					System.String data = entity.JobSpecification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobSpecification = null;
					else entity.JobSpecification = Convert.ToString(value);
				}
			}
				
			public System.String ContactPerson
			{
				get
				{
					System.String data = entity.ContactPerson;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactPerson = null;
					else entity.ContactPerson = Convert.ToString(value);
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
			

			private esAdvertisedPersonnelRequisition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAdvertisedPersonnelRequisitionQuery query)
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
				throw new Exception("esAdvertisedPersonnelRequisition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AdvertisedPersonnelRequisition : esAdvertisedPersonnelRequisition
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
	abstract public class esAdvertisedPersonnelRequisitionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AdvertisedPersonnelRequisitionMetadata.Meta();
			}
		}	
		

		public esQueryItem AdvertisedPersonnelRequisitionID
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.AdvertisedPersonnelRequisitionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JobOpportunityID
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobOpportunityID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonnelRequisitionID
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EstimatedSalary
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.EstimatedSalary, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MinimumEstimatedSalary
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.MinimumEstimatedSalary, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MaximumEstimatedSalary
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.MaximumEstimatedSalary, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem JobDescription
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobDescription, esSystemType.String);
			}
		} 
		
		public esQueryItem JobSpecification
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobSpecification, esSystemType.String);
			}
		} 
		
		public esQueryItem ContactPerson
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.ContactPerson, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AdvertisedPersonnelRequisitionCollection")]
	public partial class AdvertisedPersonnelRequisitionCollection : esAdvertisedPersonnelRequisitionCollection, IEnumerable<AdvertisedPersonnelRequisition>
	{
		public AdvertisedPersonnelRequisitionCollection()
		{

		}
		
		public static implicit operator List<AdvertisedPersonnelRequisition>(AdvertisedPersonnelRequisitionCollection coll)
		{
			List<AdvertisedPersonnelRequisition> list = new List<AdvertisedPersonnelRequisition>();
			
			foreach (AdvertisedPersonnelRequisition emp in coll)
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
				return  AdvertisedPersonnelRequisitionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AdvertisedPersonnelRequisitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AdvertisedPersonnelRequisition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AdvertisedPersonnelRequisition();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AdvertisedPersonnelRequisitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AdvertisedPersonnelRequisitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AdvertisedPersonnelRequisitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AdvertisedPersonnelRequisition AddNew()
		{
			AdvertisedPersonnelRequisition entity = base.AddNewEntity() as AdvertisedPersonnelRequisition;
			
			return entity;
		}

		public AdvertisedPersonnelRequisition FindByPrimaryKey(System.Int32 advertisedPersonnelRequisitionID)
		{
			return base.FindByPrimaryKey(advertisedPersonnelRequisitionID) as AdvertisedPersonnelRequisition;
		}


		#region IEnumerable<AdvertisedPersonnelRequisition> Members

		IEnumerator<AdvertisedPersonnelRequisition> IEnumerable<AdvertisedPersonnelRequisition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AdvertisedPersonnelRequisition;
			}
		}

		#endregion
		
		private AdvertisedPersonnelRequisitionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AdvertisedPersonnelRequisition' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AdvertisedPersonnelRequisition ({AdvertisedPersonnelRequisitionID})")]
	[Serializable]
	public partial class AdvertisedPersonnelRequisition : esAdvertisedPersonnelRequisition
	{
		public AdvertisedPersonnelRequisition()
		{

		}
	
		public AdvertisedPersonnelRequisition(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AdvertisedPersonnelRequisitionMetadata.Meta();
			}
		}
		
		
		
		override protected esAdvertisedPersonnelRequisitionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AdvertisedPersonnelRequisitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AdvertisedPersonnelRequisitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AdvertisedPersonnelRequisitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AdvertisedPersonnelRequisitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AdvertisedPersonnelRequisitionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AdvertisedPersonnelRequisitionQuery : esAdvertisedPersonnelRequisitionQuery
	{
		public AdvertisedPersonnelRequisitionQuery()
		{

		}		
		
		public AdvertisedPersonnelRequisitionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AdvertisedPersonnelRequisitionQuery";
        }
		
			
	}


	[Serializable]
	public partial class AdvertisedPersonnelRequisitionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AdvertisedPersonnelRequisitionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.AdvertisedPersonnelRequisitionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.AdvertisedPersonnelRequisitionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobOpportunityID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.JobOpportunityID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.PersonnelRequisitionID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.PersonnelRequisitionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.EstimatedSalary, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.EstimatedSalary;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.MinimumEstimatedSalary, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.MinimumEstimatedSalary;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.MaximumEstimatedSalary, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.MaximumEstimatedSalary;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobDescription, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.JobDescription;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.JobSpecification, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.JobSpecification;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.ContactPerson, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.ContactPerson;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(AdvertisedPersonnelRequisitionMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AdvertisedPersonnelRequisitionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AdvertisedPersonnelRequisitionMetadata Meta()
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
			 public const string AdvertisedPersonnelRequisitionID = "AdvertisedPersonnelRequisitionID";
			 public const string JobOpportunityID = "JobOpportunityID";
			 public const string PersonnelRequisitionID = "PersonnelRequisitionID";
			 public const string EstimatedSalary = "EstimatedSalary";
			 public const string MinimumEstimatedSalary = "MinimumEstimatedSalary";
			 public const string MaximumEstimatedSalary = "MaximumEstimatedSalary";
			 public const string JobDescription = "JobDescription";
			 public const string JobSpecification = "JobSpecification";
			 public const string ContactPerson = "ContactPerson";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AdvertisedPersonnelRequisitionID = "AdvertisedPersonnelRequisitionID";
			 public const string JobOpportunityID = "JobOpportunityID";
			 public const string PersonnelRequisitionID = "PersonnelRequisitionID";
			 public const string EstimatedSalary = "EstimatedSalary";
			 public const string MinimumEstimatedSalary = "MinimumEstimatedSalary";
			 public const string MaximumEstimatedSalary = "MaximumEstimatedSalary";
			 public const string JobDescription = "JobDescription";
			 public const string JobSpecification = "JobSpecification";
			 public const string ContactPerson = "ContactPerson";
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
			lock (typeof(AdvertisedPersonnelRequisitionMetadata))
			{
				if(AdvertisedPersonnelRequisitionMetadata.mapDelegates == null)
				{
					AdvertisedPersonnelRequisitionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AdvertisedPersonnelRequisitionMetadata.meta == null)
				{
					AdvertisedPersonnelRequisitionMetadata.meta = new AdvertisedPersonnelRequisitionMetadata();
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
				

				meta.AddTypeMap("AdvertisedPersonnelRequisitionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JobOpportunityID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonnelRequisitionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EstimatedSalary", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("MinimumEstimatedSalary", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("MaximumEstimatedSalary", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("JobDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JobSpecification", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContactPerson", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AdvertisedPersonnelRequisition";
				meta.Destination = "AdvertisedPersonnelRequisition";
				
				meta.spInsert = "proc_AdvertisedPersonnelRequisitionInsert";				
				meta.spUpdate = "proc_AdvertisedPersonnelRequisitionUpdate";		
				meta.spDelete = "proc_AdvertisedPersonnelRequisitionDelete";
				meta.spLoadAll = "proc_AdvertisedPersonnelRequisitionLoadAll";
				meta.spLoadByPrimaryKey = "proc_AdvertisedPersonnelRequisitionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AdvertisedPersonnelRequisitionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
