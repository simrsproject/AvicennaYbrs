/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/2/2019 12:32:28 PM
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
	abstract public class esPatientHealthRecordDeletedCollection : esEntityCollectionWAuditLog
	{
		public esPatientHealthRecordDeletedCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientHealthRecordDeletedCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientHealthRecordDeletedQuery query)
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
			this.InitQuery(query as esPatientHealthRecordDeletedQuery);
		}
		#endregion
			
		virtual public PatientHealthRecordDeleted DetachEntity(PatientHealthRecordDeleted entity)
		{
			return base.DetachEntity(entity) as PatientHealthRecordDeleted;
		}
		
		virtual public PatientHealthRecordDeleted AttachEntity(PatientHealthRecordDeleted entity)
		{
			return base.AttachEntity(entity) as PatientHealthRecordDeleted;
		}
		
		virtual public void Combine(PatientHealthRecordDeletedCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientHealthRecordDeleted this[int index]
		{
			get
			{
				return base[index] as PatientHealthRecordDeleted;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientHealthRecordDeleted);
		}
	}

	[Serializable]
	abstract public class esPatientHealthRecordDeleted : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientHealthRecordDeletedQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientHealthRecordDeleted()
		{
		}
	
		public esPatientHealthRecordDeleted(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String registrationNo, String questionFormID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, registrationNo, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, registrationNo, questionFormID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String registrationNo, String questionFormID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, registrationNo, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, registrationNo, questionFormID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String registrationNo, String questionFormID)
		{
			esPatientHealthRecordDeletedQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.RegistrationNo==registrationNo, query.QuestionFormID==questionFormID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String registrationNo, String questionFormID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("QuestionFormID",questionFormID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "RecordDate": this.str.RecordDate = (string)value; break;
						case "RecordTime": this.str.RecordTime = (string)value; break;
						case "EmployeeID": this.str.EmployeeID = (string)value; break;
						case "IsComplete": this.str.IsComplete = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ExaminerID": this.str.ExaminerID = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RecordDate":
						
							if (value == null || value is System.DateTime)
								this.RecordDate = (System.DateTime?)value;
							break;
						case "IsComplete":
						
							if (value == null || value is System.Boolean)
								this.IsComplete = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to PatientHealthRecordDeleted.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.RecordDate
		/// </summary>
		virtual public System.DateTime? RecordDate
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordDeletedMetadata.ColumnNames.RecordDate);
			}
			
			set
			{
				base.SetSystemDateTime(PatientHealthRecordDeletedMetadata.ColumnNames.RecordDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.RecordTime
		/// </summary>
		virtual public System.String RecordTime
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.RecordTime);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.RecordTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.EmployeeID
		/// </summary>
		virtual public System.String EmployeeID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.EmployeeID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.EmployeeID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.IsComplete
		/// </summary>
		virtual public System.Boolean? IsComplete
		{
			get
			{
				return base.GetSystemBoolean(PatientHealthRecordDeletedMetadata.ColumnNames.IsComplete);
			}
			
			set
			{
				base.SetSystemBoolean(PatientHealthRecordDeletedMetadata.ColumnNames.IsComplete, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.ExaminerID
		/// </summary>
		virtual public System.String ExaminerID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.ExaminerID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.ExaminerID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordDeletedMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientHealthRecordDeletedMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecordDeleted.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(PatientHealthRecordDeletedMetadata.ColumnNames.ReferenceNo, value);
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
			public esStrings(esPatientHealthRecordDeleted entity)
			{
				this.entity = entity;
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
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
				}
			}
			public System.String RecordDate
			{
				get
				{
					System.DateTime? data = entity.RecordDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecordDate = null;
					else entity.RecordDate = Convert.ToDateTime(value);
				}
			}
			public System.String RecordTime
			{
				get
				{
					System.String data = entity.RecordTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecordTime = null;
					else entity.RecordTime = Convert.ToString(value);
				}
			}
			public System.String EmployeeID
			{
				get
				{
					System.String data = entity.EmployeeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeID = null;
					else entity.EmployeeID = Convert.ToString(value);
				}
			}
			public System.String IsComplete
			{
				get
				{
					System.Boolean? data = entity.IsComplete;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComplete = null;
					else entity.IsComplete = Convert.ToBoolean(value);
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
			public System.String ExaminerID
			{
				get
				{
					System.String data = entity.ExaminerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExaminerID = null;
					else entity.ExaminerID = Convert.ToString(value);
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
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			private esPatientHealthRecordDeleted entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientHealthRecordDeletedQuery query)
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
				throw new Exception("esPatientHealthRecordDeleted can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientHealthRecordDeleted : esPatientHealthRecordDeleted
	{	
	}

	[Serializable]
	abstract public class esPatientHealthRecordDeletedQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientHealthRecordDeletedMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
			
		public esQueryItem RecordDate
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.RecordDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RecordTime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.RecordTime, esSystemType.String);
			}
		} 
			
		public esQueryItem EmployeeID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.EmployeeID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsComplete
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.IsComplete, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ExaminerID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.ExaminerID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordDeletedMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientHealthRecordDeletedCollection")]
	public partial class PatientHealthRecordDeletedCollection : esPatientHealthRecordDeletedCollection, IEnumerable< PatientHealthRecordDeleted>
	{
		public PatientHealthRecordDeletedCollection()
		{

		}	
		
		public static implicit operator List< PatientHealthRecordDeleted>(PatientHealthRecordDeletedCollection coll)
		{
			List< PatientHealthRecordDeleted> list = new List< PatientHealthRecordDeleted>();
			
			foreach (PatientHealthRecordDeleted emp in coll)
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
				return  PatientHealthRecordDeletedMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientHealthRecordDeletedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientHealthRecordDeleted(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientHealthRecordDeleted();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientHealthRecordDeletedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientHealthRecordDeletedQuery();
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
		public bool Load(PatientHealthRecordDeletedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientHealthRecordDeleted AddNew()
		{
			PatientHealthRecordDeleted entity = base.AddNewEntity() as PatientHealthRecordDeleted;
			
			return entity;		
		}
		public PatientHealthRecordDeleted FindByPrimaryKey(String transactionNo, String registrationNo, String questionFormID)
		{
			return base.FindByPrimaryKey(transactionNo, registrationNo, questionFormID) as PatientHealthRecordDeleted;
		}

		#region IEnumerable< PatientHealthRecordDeleted> Members

		IEnumerator< PatientHealthRecordDeleted> IEnumerable< PatientHealthRecordDeleted>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientHealthRecordDeleted;
			}
		}

		#endregion
		
		private PatientHealthRecordDeletedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientHealthRecordDeleted' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientHealthRecordDeleted ({TransactionNo, RegistrationNo, QuestionFormID})")]
	[Serializable]
	public partial class PatientHealthRecordDeleted : esPatientHealthRecordDeleted
	{
		public PatientHealthRecordDeleted()
		{
		}	
	
		public PatientHealthRecordDeleted(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientHealthRecordDeletedMetadata.Meta();
			}
		}	
	
		override protected esPatientHealthRecordDeletedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientHealthRecordDeletedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientHealthRecordDeletedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientHealthRecordDeletedQuery();
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
		public bool Load(PatientHealthRecordDeletedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientHealthRecordDeletedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientHealthRecordDeletedQuery : esPatientHealthRecordDeletedQuery
	{
		public PatientHealthRecordDeletedQuery()
		{

		}		
		
		public PatientHealthRecordDeletedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientHealthRecordDeletedQuery";
        }
	}

	[Serializable]
	public partial class PatientHealthRecordDeletedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientHealthRecordDeletedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.QuestionFormID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.RecordDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.RecordDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.RecordTime, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.RecordTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.EmployeeID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.EmployeeID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.IsComplete, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.IsComplete;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.ExaminerID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.ExaminerID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.CreateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.CreateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.ServiceUnitID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientHealthRecordDeletedMetadata.ColumnNames.ReferenceNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordDeletedMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientHealthRecordDeletedMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string RecordDate = "RecordDate";
			public const string RecordTime = "RecordTime";
			public const string EmployeeID = "EmployeeID";
			public const string IsComplete = "IsComplete";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ExaminerID = "ExaminerID";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ReferenceNo = "ReferenceNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string RecordDate = "RecordDate";
			public const string RecordTime = "RecordTime";
			public const string EmployeeID = "EmployeeID";
			public const string IsComplete = "IsComplete";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ExaminerID = "ExaminerID";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ReferenceNo = "ReferenceNo";
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
			lock (typeof(PatientHealthRecordDeletedMetadata))
			{
				if(PatientHealthRecordDeletedMetadata.mapDelegates == null)
				{
					PatientHealthRecordDeletedMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientHealthRecordDeletedMetadata.meta == null)
				{
					PatientHealthRecordDeletedMetadata.meta = new PatientHealthRecordDeletedMetadata();
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
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecordDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RecordTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EmployeeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsComplete", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExaminerID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PatientHealthRecordDeleted";
				meta.Destination = "PatientHealthRecordDeleted";
				meta.spInsert = "proc_PatientHealthRecordDeletedInsert";				
				meta.spUpdate = "proc_PatientHealthRecordDeletedUpdate";		
				meta.spDelete = "proc_PatientHealthRecordDeletedDelete";
				meta.spLoadAll = "proc_PatientHealthRecordDeletedLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientHealthRecordDeletedLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientHealthRecordDeletedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
