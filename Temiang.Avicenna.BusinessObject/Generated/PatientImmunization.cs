/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/18/19 5:14:30 PM
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
	abstract public class esPatientImmunizationCollection : esEntityCollectionWAuditLog
	{
		public esPatientImmunizationCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientImmunizationCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientImmunizationQuery query)
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
			this.InitQuery(query as esPatientImmunizationQuery);
		}
		#endregion
			
		virtual public PatientImmunization DetachEntity(PatientImmunization entity)
		{
			return base.DetachEntity(entity) as PatientImmunization;
		}
		
		virtual public PatientImmunization AttachEntity(PatientImmunization entity)
		{
			return base.AttachEntity(entity) as PatientImmunization;
		}
		
		virtual public void Combine(PatientImmunizationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientImmunization this[int index]
		{
			get
			{
				return base[index] as PatientImmunization;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientImmunization);
		}
	}

	[Serializable]
	abstract public class esPatientImmunization : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientImmunizationQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientImmunization()
		{
		}
	
		public esPatientImmunization(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String patientID, String immunizationID, Int32 immunizationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, immunizationID, immunizationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, immunizationID, immunizationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, String immunizationID, Int32 immunizationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, immunizationID, immunizationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, immunizationID, immunizationNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String patientID, String immunizationID, Int32 immunizationNo)
		{
			esPatientImmunizationQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID, query.ImmunizationID == immunizationID, query.ImmunizationNo == immunizationNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String patientID, String immunizationID, Int32 immunizationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);
			parms.Add("ImmunizationID",immunizationID);
			parms.Add("ImmunizationNo",immunizationNo);
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
						case "PatientID": this.str.PatientID = (string)value; break;
						case "ImmunizationID": this.str.ImmunizationID = (string)value; break;
						case "ImmunizationNo": this.str.ImmunizationNo = (string)value; break;
						case "ImmunizationDate": this.str.ImmunizationDate = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ReferenceItemID": this.str.ReferenceItemID = (string)value; break;
						case "VaccineID": this.str.VaccineID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsDateInMonthYear": this.str.IsDateInMonthYear = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ImmunizationNo":
						
							if (value == null || value is System.Int32)
								this.ImmunizationNo = (System.Int32?)value;
							break;
						case "ImmunizationDate":
						
							if (value == null || value is System.DateTime)
								this.ImmunizationDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsDateInMonthYear":
						
							if (value == null || value is System.Boolean)
								this.IsDateInMonthYear = (System.Boolean?)value;
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
		/// Maps to PatientImmunization.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientImmunizationMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(PatientImmunizationMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.ImmunizationID
		/// </summary>
		virtual public System.String ImmunizationID
		{
			get
			{
				return base.GetSystemString(PatientImmunizationMetadata.ColumnNames.ImmunizationID);
			}
			
			set
			{
				base.SetSystemString(PatientImmunizationMetadata.ColumnNames.ImmunizationID, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.ImmunizationNo
		/// </summary>
		virtual public System.Int32? ImmunizationNo
		{
			get
			{
				return base.GetSystemInt32(PatientImmunizationMetadata.ColumnNames.ImmunizationNo);
			}
			
			set
			{
				base.SetSystemInt32(PatientImmunizationMetadata.ColumnNames.ImmunizationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.ImmunizationDate
		/// </summary>
		virtual public System.DateTime? ImmunizationDate
		{
			get
			{
				return base.GetSystemDateTime(PatientImmunizationMetadata.ColumnNames.ImmunizationDate);
			}
			
			set
			{
				base.SetSystemDateTime(PatientImmunizationMetadata.ColumnNames.ImmunizationDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(PatientImmunizationMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(PatientImmunizationMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.ReferenceItemID
		/// </summary>
		virtual public System.String ReferenceItemID
		{
			get
			{
				return base.GetSystemString(PatientImmunizationMetadata.ColumnNames.ReferenceItemID);
			}
			
			set
			{
				base.SetSystemString(PatientImmunizationMetadata.ColumnNames.ReferenceItemID, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.VaccineID
		/// </summary>
		virtual public System.String VaccineID
		{
			get
			{
				return base.GetSystemString(PatientImmunizationMetadata.ColumnNames.VaccineID);
			}
			
			set
			{
				base.SetSystemString(PatientImmunizationMetadata.ColumnNames.VaccineID, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientImmunizationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientImmunizationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientImmunizationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientImmunizationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientImmunization.IsDateInMonthYear
		/// </summary>
		virtual public System.Boolean? IsDateInMonthYear
		{
			get
			{
				return base.GetSystemBoolean(PatientImmunizationMetadata.ColumnNames.IsDateInMonthYear);
			}
			
			set
			{
				base.SetSystemBoolean(PatientImmunizationMetadata.ColumnNames.IsDateInMonthYear, value);
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
			public esStrings(esPatientImmunization entity)
			{
				this.entity = entity;
			}
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
			public System.String ImmunizationID
			{
				get
				{
					System.String data = entity.ImmunizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImmunizationID = null;
					else entity.ImmunizationID = Convert.ToString(value);
				}
			}
			public System.String ImmunizationNo
			{
				get
				{
					System.Int32? data = entity.ImmunizationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImmunizationNo = null;
					else entity.ImmunizationNo = Convert.ToInt32(value);
				}
			}
			public System.String ImmunizationDate
			{
				get
				{
					System.DateTime? data = entity.ImmunizationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImmunizationDate = null;
					else entity.ImmunizationDate = Convert.ToDateTime(value);
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
			public System.String ReferenceItemID
			{
				get
				{
					System.String data = entity.ReferenceItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceItemID = null;
					else entity.ReferenceItemID = Convert.ToString(value);
				}
			}
			public System.String VaccineID
			{
				get
				{
					System.String data = entity.VaccineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VaccineID = null;
					else entity.VaccineID = Convert.ToString(value);
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
			public System.String IsDateInMonthYear
			{
				get
				{
					System.Boolean? data = entity.IsDateInMonthYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDateInMonthYear = null;
					else entity.IsDateInMonthYear = Convert.ToBoolean(value);
				}
			}
			private esPatientImmunization entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientImmunizationQuery query)
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
				throw new Exception("esPatientImmunization can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientImmunization : esPatientImmunization
	{	
	}

	[Serializable]
	abstract public class esPatientImmunizationQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientImmunizationMetadata.Meta();
			}
		}	
			
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
			
		public esQueryItem ImmunizationID
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.ImmunizationID, esSystemType.String);
			}
		} 
			
		public esQueryItem ImmunizationNo
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.ImmunizationNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ImmunizationDate
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.ImmunizationDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceItemID
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.ReferenceItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem VaccineID
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.VaccineID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDateInMonthYear
		{
			get
			{
				return new esQueryItem(this, PatientImmunizationMetadata.ColumnNames.IsDateInMonthYear, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientImmunizationCollection")]
	public partial class PatientImmunizationCollection : esPatientImmunizationCollection, IEnumerable< PatientImmunization>
	{
		public PatientImmunizationCollection()
		{

		}	
		
		public static implicit operator List< PatientImmunization>(PatientImmunizationCollection coll)
		{
			List< PatientImmunization> list = new List< PatientImmunization>();
			
			foreach (PatientImmunization emp in coll)
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
				return  PatientImmunizationMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientImmunizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientImmunization(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientImmunization();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientImmunizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientImmunizationQuery();
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
		public bool Load(PatientImmunizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientImmunization AddNew()
		{
			PatientImmunization entity = base.AddNewEntity() as PatientImmunization;
			
			return entity;		
		}
		public PatientImmunization FindByPrimaryKey(String patientID, String immunizationID, Int32 immunizationNo)
		{
			return base.FindByPrimaryKey(patientID, immunizationID, immunizationNo) as PatientImmunization;
		}

		#region IEnumerable< PatientImmunization> Members

		IEnumerator< PatientImmunization> IEnumerable< PatientImmunization>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientImmunization;
			}
		}

		#endregion
		
		private PatientImmunizationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientImmunization' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientImmunization ({PatientID, ImmunizationID, ImmunizationNo})")]
	[Serializable]
	public partial class PatientImmunization : esPatientImmunization
	{
		public PatientImmunization()
		{
		}	
	
		public PatientImmunization(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientImmunizationMetadata.Meta();
			}
		}	
	
		override protected esPatientImmunizationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientImmunizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientImmunizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientImmunizationQuery();
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
		public bool Load(PatientImmunizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientImmunizationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientImmunizationQuery : esPatientImmunizationQuery
	{
		public PatientImmunizationQuery()
		{

		}		
		
		public PatientImmunizationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientImmunizationQuery";
        }
	}

	[Serializable]
	public partial class PatientImmunizationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientImmunizationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.ImmunizationID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.ImmunizationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.ImmunizationNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.ImmunizationNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.ImmunizationDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.ImmunizationDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.ReferenceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.ReferenceItemID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.ReferenceItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.VaccineID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.VaccineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientImmunizationMetadata.ColumnNames.IsDateInMonthYear, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientImmunizationMetadata.PropertyNames.IsDateInMonthYear;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientImmunizationMetadata Meta()
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
			public const string PatientID = "PatientID";
			public const string ImmunizationID = "ImmunizationID";
			public const string ImmunizationNo = "ImmunizationNo";
			public const string ImmunizationDate = "ImmunizationDate";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceItemID = "ReferenceItemID";
			public const string VaccineID = "VaccineID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsDateInMonthYear = "IsDateInMonthYear";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PatientID = "PatientID";
			public const string ImmunizationID = "ImmunizationID";
			public const string ImmunizationNo = "ImmunizationNo";
			public const string ImmunizationDate = "ImmunizationDate";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceItemID = "ReferenceItemID";
			public const string VaccineID = "VaccineID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsDateInMonthYear = "IsDateInMonthYear";
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
			lock (typeof(PatientImmunizationMetadata))
			{
				if(PatientImmunizationMetadata.mapDelegates == null)
				{
					PatientImmunizationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientImmunizationMetadata.meta == null)
				{
					PatientImmunizationMetadata.meta = new PatientImmunizationMetadata();
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
				
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImmunizationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImmunizationNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ImmunizationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VaccineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDateInMonthYear", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "PatientImmunization";
				meta.Destination = "PatientImmunization";
				meta.spInsert = "proc_PatientImmunizationInsert";				
				meta.spUpdate = "proc_PatientImmunizationUpdate";		
				meta.spDelete = "proc_PatientImmunizationDelete";
				meta.spLoadAll = "proc_PatientImmunizationLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientImmunizationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientImmunizationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
