/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:10 PM
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
	abstract public class esApplicantWorkExperienceCollection : esEntityCollectionWAuditLog
	{
		public esApplicantWorkExperienceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantWorkExperienceCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantWorkExperienceQuery query)
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
			this.InitQuery(query as esApplicantWorkExperienceQuery);
		}
		#endregion
		
		virtual public ApplicantWorkExperience DetachEntity(ApplicantWorkExperience entity)
		{
			return base.DetachEntity(entity) as ApplicantWorkExperience;
		}
		
		virtual public ApplicantWorkExperience AttachEntity(ApplicantWorkExperience entity)
		{
			return base.AttachEntity(entity) as ApplicantWorkExperience;
		}
		
		virtual public void Combine(ApplicantWorkExperienceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantWorkExperience this[int index]
		{
			get
			{
				return base[index] as ApplicantWorkExperience;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantWorkExperience);
		}
	}



	[Serializable]
	abstract public class esApplicantWorkExperience : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantWorkExperienceQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantWorkExperience()
		{

		}

		public esApplicantWorkExperience(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantWorkExperienceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantWorkExperienceID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantWorkExperienceID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantWorkExperienceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantWorkExperienceID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantWorkExperienceID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantWorkExperienceID)
		{
			esApplicantWorkExperienceQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantWorkExperienceID == applicantWorkExperienceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantWorkExperienceID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantWorkExperienceID",applicantWorkExperienceID);
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
						case "ApplicantWorkExperienceID": this.str.ApplicantWorkExperienceID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "SRLineBisnis": this.str.SRLineBisnis = (string)value; break;							
						case "StartDate": this.str.StartDate = (string)value; break;							
						case "EndDate": this.str.EndDate = (string)value; break;							
						case "Company": this.str.Company = (string)value; break;							
						case "Division": this.str.Division = (string)value; break;							
						case "Location": this.str.Location = (string)value; break;							
						case "JobDesc": this.str.JobDesc = (string)value; break;							
						case "SupervisorName": this.str.SupervisorName = (string)value; break;							
						case "LastSalary": this.str.LastSalary = (string)value; break;							
						case "ReasonOfLeaving": this.str.ReasonOfLeaving = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantWorkExperienceID":
						
							if (value == null || value is System.Int32)
								this.ApplicantWorkExperienceID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
							break;
						
						case "StartDate":
						
							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						
						case "EndDate":
						
							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						
						case "LastSalary":
						
							if (value == null || value is System.Decimal)
								this.LastSalary = (System.Decimal?)value;
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
		/// Maps to ApplicantWorkExperience.ApplicantWorkExperienceID
		/// </summary>
		virtual public System.Int32? ApplicantWorkExperienceID
		{
			get
			{
				return base.GetSystemInt32(ApplicantWorkExperienceMetadata.ColumnNames.ApplicantWorkExperienceID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantWorkExperienceMetadata.ColumnNames.ApplicantWorkExperienceID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantWorkExperienceMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantWorkExperienceMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.SRLineBisnis
		/// </summary>
		virtual public System.String SRLineBisnis
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.SRLineBisnis);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.SRLineBisnis, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(ApplicantWorkExperienceMetadata.ColumnNames.StartDate);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantWorkExperienceMetadata.ColumnNames.StartDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(ApplicantWorkExperienceMetadata.ColumnNames.EndDate);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantWorkExperienceMetadata.ColumnNames.EndDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.Company
		/// </summary>
		virtual public System.String Company
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.Company);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.Company, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.Division
		/// </summary>
		virtual public System.String Division
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.Division);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.Division, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.Location
		/// </summary>
		virtual public System.String Location
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.Location);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.Location, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.JobDesc
		/// </summary>
		virtual public System.String JobDesc
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.JobDesc);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.JobDesc, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.SupervisorName
		/// </summary>
		virtual public System.String SupervisorName
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.SupervisorName);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.SupervisorName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.LastSalary
		/// </summary>
		virtual public System.Decimal? LastSalary
		{
			get
			{
				return base.GetSystemDecimal(ApplicantWorkExperienceMetadata.ColumnNames.LastSalary);
			}
			
			set
			{
				base.SetSystemDecimal(ApplicantWorkExperienceMetadata.ColumnNames.LastSalary, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.ReasonOfLeaving
		/// </summary>
		virtual public System.String ReasonOfLeaving
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.ReasonOfLeaving);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.ReasonOfLeaving, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantWorkExperience.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantWorkExperience entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantWorkExperienceID
			{
				get
				{
					System.Int32? data = entity.ApplicantWorkExperienceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantWorkExperienceID = null;
					else entity.ApplicantWorkExperienceID = Convert.ToInt32(value);
				}
			}
				
			public System.String ApplicantID
			{
				get
				{
					System.Int32? data = entity.ApplicantID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantID = null;
					else entity.ApplicantID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRLineBisnis
			{
				get
				{
					System.String data = entity.SRLineBisnis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLineBisnis = null;
					else entity.SRLineBisnis = Convert.ToString(value);
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
				
			public System.String Company
			{
				get
				{
					System.String data = entity.Company;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Company = null;
					else entity.Company = Convert.ToString(value);
				}
			}
				
			public System.String Division
			{
				get
				{
					System.String data = entity.Division;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Division = null;
					else entity.Division = Convert.ToString(value);
				}
			}
				
			public System.String Location
			{
				get
				{
					System.String data = entity.Location;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Location = null;
					else entity.Location = Convert.ToString(value);
				}
			}
				
			public System.String JobDesc
			{
				get
				{
					System.String data = entity.JobDesc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobDesc = null;
					else entity.JobDesc = Convert.ToString(value);
				}
			}
				
			public System.String SupervisorName
			{
				get
				{
					System.String data = entity.SupervisorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorName = null;
					else entity.SupervisorName = Convert.ToString(value);
				}
			}
				
			public System.String LastSalary
			{
				get
				{
					System.Decimal? data = entity.LastSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastSalary = null;
					else entity.LastSalary = Convert.ToDecimal(value);
				}
			}
				
			public System.String ReasonOfLeaving
			{
				get
				{
					System.String data = entity.ReasonOfLeaving;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonOfLeaving = null;
					else entity.ReasonOfLeaving = Convert.ToString(value);
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
			

			private esApplicantWorkExperience entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantWorkExperienceQuery query)
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
				throw new Exception("esApplicantWorkExperience can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantWorkExperience : esApplicantWorkExperience
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
	abstract public class esApplicantWorkExperienceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantWorkExperienceMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantWorkExperienceID
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.ApplicantWorkExperienceID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRLineBisnis
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.SRLineBisnis, esSystemType.String);
			}
		} 
		
		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Company
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.Company, esSystemType.String);
			}
		} 
		
		public esQueryItem Division
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.Division, esSystemType.String);
			}
		} 
		
		public esQueryItem Location
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.Location, esSystemType.String);
			}
		} 
		
		public esQueryItem JobDesc
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.JobDesc, esSystemType.String);
			}
		} 
		
		public esQueryItem SupervisorName
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.SupervisorName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastSalary
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.LastSalary, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ReasonOfLeaving
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.ReasonOfLeaving, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantWorkExperienceCollection")]
	public partial class ApplicantWorkExperienceCollection : esApplicantWorkExperienceCollection, IEnumerable<ApplicantWorkExperience>
	{
		public ApplicantWorkExperienceCollection()
		{

		}
		
		public static implicit operator List<ApplicantWorkExperience>(ApplicantWorkExperienceCollection coll)
		{
			List<ApplicantWorkExperience> list = new List<ApplicantWorkExperience>();
			
			foreach (ApplicantWorkExperience emp in coll)
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
				return  ApplicantWorkExperienceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantWorkExperience(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantWorkExperience();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantWorkExperienceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantWorkExperience AddNew()
		{
			ApplicantWorkExperience entity = base.AddNewEntity() as ApplicantWorkExperience;
			
			return entity;
		}

		public ApplicantWorkExperience FindByPrimaryKey(System.Int32 applicantWorkExperienceID)
		{
			return base.FindByPrimaryKey(applicantWorkExperienceID) as ApplicantWorkExperience;
		}


		#region IEnumerable<ApplicantWorkExperience> Members

		IEnumerator<ApplicantWorkExperience> IEnumerable<ApplicantWorkExperience>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantWorkExperience;
			}
		}

		#endregion
		
		private ApplicantWorkExperienceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantWorkExperience' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantWorkExperience ({ApplicantWorkExperienceID})")]
	[Serializable]
	public partial class ApplicantWorkExperience : esApplicantWorkExperience
	{
		public ApplicantWorkExperience()
		{

		}
	
		public ApplicantWorkExperience(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantWorkExperienceMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantWorkExperienceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantWorkExperienceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantWorkExperienceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantWorkExperienceQuery : esApplicantWorkExperienceQuery
	{
		public ApplicantWorkExperienceQuery()
		{

		}		
		
		public ApplicantWorkExperienceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantWorkExperienceQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantWorkExperienceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantWorkExperienceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.ApplicantWorkExperienceID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.ApplicantWorkExperienceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.SRLineBisnis, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.SRLineBisnis;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.StartDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.EndDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.Company, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.Company;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.Division, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.Division;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.Location, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.Location;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.JobDesc, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.JobDesc;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.SupervisorName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.SupervisorName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.LastSalary, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.LastSalary;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.ReasonOfLeaving, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.ReasonOfLeaving;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantWorkExperienceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantWorkExperienceMetadata Meta()
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
			 public const string ApplicantWorkExperienceID = "ApplicantWorkExperienceID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRLineBisnis = "SRLineBisnis";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string Company = "Company";
			 public const string Division = "Division";
			 public const string Location = "Location";
			 public const string JobDesc = "JobDesc";
			 public const string SupervisorName = "SupervisorName";
			 public const string LastSalary = "LastSalary";
			 public const string ReasonOfLeaving = "ReasonOfLeaving";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantWorkExperienceID = "ApplicantWorkExperienceID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRLineBisnis = "SRLineBisnis";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string Company = "Company";
			 public const string Division = "Division";
			 public const string Location = "Location";
			 public const string JobDesc = "JobDesc";
			 public const string SupervisorName = "SupervisorName";
			 public const string LastSalary = "LastSalary";
			 public const string ReasonOfLeaving = "ReasonOfLeaving";
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
			lock (typeof(ApplicantWorkExperienceMetadata))
			{
				if(ApplicantWorkExperienceMetadata.mapDelegates == null)
				{
					ApplicantWorkExperienceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantWorkExperienceMetadata.meta == null)
				{
					ApplicantWorkExperienceMetadata.meta = new ApplicantWorkExperienceMetadata();
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
				

				meta.AddTypeMap("ApplicantWorkExperienceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRLineBisnis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Company", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Division", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Location", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JobDesc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupervisorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastSalary", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ReasonOfLeaving", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantWorkExperience";
				meta.Destination = "ApplicantWorkExperience";
				
				meta.spInsert = "proc_ApplicantWorkExperienceInsert";				
				meta.spUpdate = "proc_ApplicantWorkExperienceUpdate";		
				meta.spDelete = "proc_ApplicantWorkExperienceDelete";
				meta.spLoadAll = "proc_ApplicantWorkExperienceLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantWorkExperienceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantWorkExperienceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
