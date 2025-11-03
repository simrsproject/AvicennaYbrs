/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/28/2021 4:04:09 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esPatientReceivableMonthlySummaryCollection : esEntityCollectionWAuditLog
	{
		public esPatientReceivableMonthlySummaryCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientReceivableMonthlySummaryCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientReceivableMonthlySummaryQuery query)
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
			this.InitQuery(query as esPatientReceivableMonthlySummaryQuery);
		}
		#endregion
			
		virtual public PatientReceivableMonthlySummary DetachEntity(PatientReceivableMonthlySummary entity)
		{
			return base.DetachEntity(entity) as PatientReceivableMonthlySummary;
		}
		
		virtual public PatientReceivableMonthlySummary AttachEntity(PatientReceivableMonthlySummary entity)
		{
			return base.AttachEntity(entity) as PatientReceivableMonthlySummary;
		}
		
		virtual public void Combine(PatientReceivableMonthlySummaryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientReceivableMonthlySummary this[int index]
		{
			get
			{
				return base[index] as PatientReceivableMonthlySummary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientReceivableMonthlySummary);
		}
	}

	[Serializable]
	abstract public class esPatientReceivableMonthlySummary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientReceivableMonthlySummaryQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientReceivableMonthlySummary()
		{
		}
	
		public esPatientReceivableMonthlySummary(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 iD)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
		}
	
		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 iD)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 iD)
		{
			esPatientReceivableMonthlySummaryQuery query = this.GetDynamicQuery();
			query.Where(query.ID==iD);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 iD)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",iD);
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
						case "ID": this.str.ID = (string)value; break;
						case "Period": this.str.Period = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "DownPayment": this.str.DownPayment = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "JournalId": this.str.JournalId = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "SRGuarantorType": this.str.SRGuarantorType = (string)value; break;
						case "IsDischarged": this.str.IsDischarged = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ID":
						
							if (value == null || value is System.Int32)
								this.ID = (System.Int32?)value;
							break;
						case "Period":
						
							if (value == null || value is System.DateTime)
								this.Period = (System.DateTime?)value;
							break;
						case "DownPayment":
						
							if (value == null || value is System.Decimal)
								this.DownPayment = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
							break;
						case "IsDischarged":
						
							if (value == null || value is System.Boolean)
								this.IsDischarged = (System.Boolean?)value;
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
		/// Maps to PatientReceivableMonthlySummary.ID
		/// </summary>
		virtual public System.Int32? ID
		{
			get
			{
				return base.GetSystemInt32(PatientReceivableMonthlySummaryMetadata.ColumnNames.ID);
			}
			
			set
			{
				base.SetSystemInt32(PatientReceivableMonthlySummaryMetadata.ColumnNames.ID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.Period
		/// </summary>
		virtual public System.DateTime? Period
		{
			get
			{
				return base.GetSystemDateTime(PatientReceivableMonthlySummaryMetadata.ColumnNames.Period);
			}
			
			set
			{
				base.SetSystemDateTime(PatientReceivableMonthlySummaryMetadata.ColumnNames.Period, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.DownPayment
		/// </summary>
		virtual public System.Decimal? DownPayment
		{
			get
			{
				return base.GetSystemDecimal(PatientReceivableMonthlySummaryMetadata.ColumnNames.DownPayment);
			}
			
			set
			{
				base.SetSystemDecimal(PatientReceivableMonthlySummaryMetadata.ColumnNames.DownPayment, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(PatientReceivableMonthlySummaryMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(PatientReceivableMonthlySummaryMetadata.ColumnNames.JournalId, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.SRGuarantorType
		/// </summary>
		virtual public System.String SRGuarantorType
		{
			get
			{
				return base.GetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.SRGuarantorType);
			}
			
			set
			{
				base.SetSystemString(PatientReceivableMonthlySummaryMetadata.ColumnNames.SRGuarantorType, value);
			}
		}
		/// <summary>
		/// Maps to PatientReceivableMonthlySummary.IsDischarged
		/// </summary>
		virtual public System.Boolean? IsDischarged
		{
			get
			{
				return base.GetSystemBoolean(PatientReceivableMonthlySummaryMetadata.ColumnNames.IsDischarged);
			}
			
			set
			{
				base.SetSystemBoolean(PatientReceivableMonthlySummaryMetadata.ColumnNames.IsDischarged, value);
			}
		}
		
		#endregion	

		#region String Properties
		
		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
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
			public esStrings(esPatientReceivableMonthlySummary entity)
			{
				this.entity = entity;
			}
			public System.String ID
			{
				get
				{
					System.Int32? data = entity.ID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ID = null;
					else entity.ID = Convert.ToInt32(value);
				}
			}
			public System.String Period
			{
				get
				{
					System.DateTime? data = entity.Period;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Period = null;
					else entity.Period = Convert.ToDateTime(value);
				}
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String DownPayment
			{
				get
				{
					System.Decimal? data = entity.DownPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DownPayment = null;
					else entity.DownPayment = Convert.ToDecimal(value);
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
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
				}
			}
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
			public System.String SRGuarantorType
			{
				get
				{
					System.String data = entity.SRGuarantorType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGuarantorType = null;
					else entity.SRGuarantorType = Convert.ToString(value);
				}
			}
			public System.String IsDischarged
			{
				get
				{
					System.Boolean? data = entity.IsDischarged;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDischarged = null;
					else entity.IsDischarged = Convert.ToBoolean(value);
				}
			}
			private esPatientReceivableMonthlySummary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientReceivableMonthlySummaryQuery query)
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
				throw new Exception("esPatientReceivableMonthlySummary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientReceivableMonthlySummary : esPatientReceivableMonthlySummary
	{	
	}

	[Serializable]
	abstract public class esPatientReceivableMonthlySummaryQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientReceivableMonthlySummaryMetadata.Meta();
			}
		}	
			
		public esQueryItem ID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.ID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Period
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.Period, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem DownPayment
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.DownPayment, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRGuarantorType
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.SRGuarantorType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDischarged
		{
			get
			{
				return new esQueryItem(this, PatientReceivableMonthlySummaryMetadata.ColumnNames.IsDischarged, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientReceivableMonthlySummaryCollection")]
	public partial class PatientReceivableMonthlySummaryCollection : esPatientReceivableMonthlySummaryCollection, IEnumerable< PatientReceivableMonthlySummary>
	{
		public PatientReceivableMonthlySummaryCollection()
		{

		}	
		
		public static implicit operator List< PatientReceivableMonthlySummary>(PatientReceivableMonthlySummaryCollection coll)
		{
			List< PatientReceivableMonthlySummary> list = new List< PatientReceivableMonthlySummary>();
			
			foreach (PatientReceivableMonthlySummary emp in coll)
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
				return  PatientReceivableMonthlySummaryMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientReceivableMonthlySummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientReceivableMonthlySummary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientReceivableMonthlySummary();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientReceivableMonthlySummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientReceivableMonthlySummaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(PatientReceivableMonthlySummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientReceivableMonthlySummary AddNew()
		{
			PatientReceivableMonthlySummary entity = base.AddNewEntity() as PatientReceivableMonthlySummary;
			
			return entity;		
		}
		public PatientReceivableMonthlySummary FindByPrimaryKey(Int32 iD)
		{
			return base.FindByPrimaryKey(iD) as PatientReceivableMonthlySummary;
		}

		#region IEnumerable< PatientReceivableMonthlySummary> Members

		IEnumerator< PatientReceivableMonthlySummary> IEnumerable< PatientReceivableMonthlySummary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientReceivableMonthlySummary;
			}
		}

		#endregion
		
		private PatientReceivableMonthlySummaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientReceivableMonthlySummary' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientReceivableMonthlySummary ({ID})")]
	[Serializable]
	public partial class PatientReceivableMonthlySummary : esPatientReceivableMonthlySummary
	{
		public PatientReceivableMonthlySummary()
		{
		}	
	
		public PatientReceivableMonthlySummary(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientReceivableMonthlySummaryMetadata.Meta();
			}
		}	
	
		override protected esPatientReceivableMonthlySummaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientReceivableMonthlySummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientReceivableMonthlySummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientReceivableMonthlySummaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(PatientReceivableMonthlySummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientReceivableMonthlySummaryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientReceivableMonthlySummaryQuery : esPatientReceivableMonthlySummaryQuery
	{
		public PatientReceivableMonthlySummaryQuery()
		{

		}		
		
		public PatientReceivableMonthlySummaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientReceivableMonthlySummaryQuery";
        }
	}

	[Serializable]
	public partial class PatientReceivableMonthlySummaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientReceivableMonthlySummaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.ID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.ID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.Period, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.Period;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.DownPayment, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.DownPayment;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.JournalId, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.GuarantorID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.SRGuarantorType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.SRGuarantorType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientReceivableMonthlySummaryMetadata.ColumnNames.IsDischarged, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientReceivableMonthlySummaryMetadata.PropertyNames.IsDischarged;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientReceivableMonthlySummaryMetadata Meta()
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
			public const string ID = "ID";
			public const string Period = "Period";
			public const string RegistrationNo = "RegistrationNo";
			public const string DownPayment = "DownPayment";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string JournalId = "JournalId";
			public const string GuarantorID = "GuarantorID";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string IsDischarged = "IsDischarged";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ID = "ID";
			public const string Period = "Period";
			public const string RegistrationNo = "RegistrationNo";
			public const string DownPayment = "DownPayment";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string JournalId = "JournalId";
			public const string GuarantorID = "GuarantorID";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string IsDischarged = "IsDischarged";
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
			lock (typeof(PatientReceivableMonthlySummaryMetadata))
			{
				if(PatientReceivableMonthlySummaryMetadata.mapDelegates == null)
				{
					PatientReceivableMonthlySummaryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientReceivableMonthlySummaryMetadata.meta == null)
				{
					PatientReceivableMonthlySummaryMetadata.meta = new PatientReceivableMonthlySummaryMetadata();
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
				
				meta.AddTypeMap("ID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Period", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DownPayment", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGuarantorType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDischarged", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "PatientReceivableMonthlySummary";
				meta.Destination = "PatientReceivableMonthlySummary";
				meta.spInsert = "proc_PatientReceivableMonthlySummaryInsert";				
				meta.spUpdate = "proc_PatientReceivableMonthlySummaryUpdate";		
				meta.spDelete = "proc_PatientReceivableMonthlySummaryDelete";
				meta.spLoadAll = "proc_PatientReceivableMonthlySummaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientReceivableMonthlySummaryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientReceivableMonthlySummaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
