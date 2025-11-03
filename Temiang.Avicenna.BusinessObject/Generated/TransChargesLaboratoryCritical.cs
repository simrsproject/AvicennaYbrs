/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/16/2022 4:39:58 PM
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
	abstract public class esTransChargesLaboratoryCriticalCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesLaboratoryCriticalCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "TransChargesLaboratoryCriticalCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esTransChargesLaboratoryCriticalQuery query)
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
			this.InitQuery(query as esTransChargesLaboratoryCriticalQuery);
		}
		#endregion
			
		virtual public TransChargesLaboratoryCritical DetachEntity(TransChargesLaboratoryCritical entity)
		{
			return base.DetachEntity(entity) as TransChargesLaboratoryCritical;
		}
		
		virtual public TransChargesLaboratoryCritical AttachEntity(TransChargesLaboratoryCritical entity)
		{
			return base.AttachEntity(entity) as TransChargesLaboratoryCritical;
		}
		
		virtual public void Combine(TransChargesLaboratoryCriticalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransChargesLaboratoryCritical this[int index]
		{
			get
			{
				return base[index] as TransChargesLaboratoryCritical;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesLaboratoryCritical);
		}
	}

	[Serializable]
	abstract public class esTransChargesLaboratoryCritical : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesLaboratoryCriticalQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esTransChargesLaboratoryCritical()
		{
		}
	
		public esTransChargesLaboratoryCritical(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String lisTestID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, lisTestID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, lisTestID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String lisTestID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, lisTestID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, lisTestID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String lisTestID)
		{
			esTransChargesLaboratoryCriticalQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.LisTestID==lisTestID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String lisTestID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("LisTestID",lisTestID);
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
						case "LisTestID": this.str.LisTestID = (string)value; break;
						case "ReadByPhysicianID": this.str.ReadByPhysicianID = (string)value; break;
						case "ReadByPhysicianDateTime": this.str.ReadByPhysicianDateTime = (string)value; break;
						case "ReportedByNurseID": this.str.ReportedByNurseID = (string)value; break;
						case "ReportedByNurseDateTime": this.str.ReportedByNurseDateTime = (string)value; break;
						case "CompletelyReportedByUserID": this.str.CompletelyReportedByUserID = (string)value; break;
						case "CompletelyReportedDateTime": this.str.CompletelyReportedDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ReadByPhysicianDateTime":
						
							if (value == null || value is System.DateTime)
								this.ReadByPhysicianDateTime = (System.DateTime?)value;
							break;
						case "ReportedByNurseDateTime":
						
							if (value == null || value is System.DateTime)
								this.ReportedByNurseDateTime = (System.DateTime?)value;
							break;
						case "CompletelyReportedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CompletelyReportedDateTime = (System.DateTime?)value;
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
		/// Maps to TransChargesLaboratoryCritical.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesLaboratoryCritical.LisTestID
		/// </summary>
		virtual public System.String LisTestID
		{
			get
			{
				return base.GetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.LisTestID);
			}
			
			set
			{
				base.SetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.LisTestID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesLaboratoryCritical.ReadByPhysicianID
		/// </summary>
		virtual public System.String ReadByPhysicianID
		{
			get
			{
				return base.GetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianID);
			}
			
			set
			{
				base.SetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesLaboratoryCritical.ReadByPhysicianDateTime
		/// </summary>
		virtual public System.DateTime? ReadByPhysicianDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesLaboratoryCritical.ReportedByNurseID
		/// </summary>
		virtual public System.String ReportedByNurseID
		{
			get
			{
				return base.GetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseID);
			}
			
			set
			{
				base.SetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesLaboratoryCritical.ReportedByNurseDateTime
		/// </summary>
		virtual public System.DateTime? ReportedByNurseDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesLaboratoryCritical.CompletelyReportedByUserID
		/// </summary>
		virtual public System.String CompletelyReportedByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedByUserID);
			}
			
			set
			{
				base.SetSystemString(TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesLaboratoryCritical.CompletelyReportedDateTime
		/// </summary>
		virtual public System.DateTime? CompletelyReportedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedDateTime, value);
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
			public esStrings(esTransChargesLaboratoryCritical entity)
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
			public System.String LisTestID
			{
				get
				{
					System.String data = entity.LisTestID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LisTestID = null;
					else entity.LisTestID = Convert.ToString(value);
				}
			}
			public System.String ReadByPhysicianID
			{
				get
				{
					System.String data = entity.ReadByPhysicianID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReadByPhysicianID = null;
					else entity.ReadByPhysicianID = Convert.ToString(value);
				}
			}
			public System.String ReadByPhysicianDateTime
			{
				get
				{
					System.DateTime? data = entity.ReadByPhysicianDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReadByPhysicianDateTime = null;
					else entity.ReadByPhysicianDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReportedByNurseID
			{
				get
				{
					System.String data = entity.ReportedByNurseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportedByNurseID = null;
					else entity.ReportedByNurseID = Convert.ToString(value);
				}
			}
			public System.String ReportedByNurseDateTime
			{
				get
				{
					System.DateTime? data = entity.ReportedByNurseDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportedByNurseDateTime = null;
					else entity.ReportedByNurseDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CompletelyReportedByUserID
			{
				get
				{
					System.String data = entity.CompletelyReportedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompletelyReportedByUserID = null;
					else entity.CompletelyReportedByUserID = Convert.ToString(value);
				}
			}
			public System.String CompletelyReportedDateTime
			{
				get
				{
					System.DateTime? data = entity.CompletelyReportedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompletelyReportedDateTime = null;
					else entity.CompletelyReportedDateTime = Convert.ToDateTime(value);
				}
			}
			private esTransChargesLaboratoryCritical entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesLaboratoryCriticalQuery query)
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
				throw new Exception("esTransChargesLaboratoryCritical can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransChargesLaboratoryCritical : esTransChargesLaboratoryCritical
	{	
	}

	[Serializable]
	abstract public class esTransChargesLaboratoryCriticalQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesLaboratoryCriticalMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem LisTestID
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.LisTestID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReadByPhysicianID
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReadByPhysicianDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ReportedByNurseID
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReportedByNurseDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CompletelyReportedByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CompletelyReportedDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesLaboratoryCriticalCollection")]
	public partial class TransChargesLaboratoryCriticalCollection : esTransChargesLaboratoryCriticalCollection, IEnumerable< TransChargesLaboratoryCritical>
	{
		public TransChargesLaboratoryCriticalCollection()
		{

		}	
		
		public static implicit operator List< TransChargesLaboratoryCritical>(TransChargesLaboratoryCriticalCollection coll)
		{
			List< TransChargesLaboratoryCritical> list = new List< TransChargesLaboratoryCritical>();
			
			foreach (TransChargesLaboratoryCritical emp in coll)
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
				return  TransChargesLaboratoryCriticalMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesLaboratoryCriticalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesLaboratoryCritical(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesLaboratoryCritical();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public TransChargesLaboratoryCriticalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesLaboratoryCriticalQuery();
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
		public bool Load(TransChargesLaboratoryCriticalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransChargesLaboratoryCritical AddNew()
		{
			TransChargesLaboratoryCritical entity = base.AddNewEntity() as TransChargesLaboratoryCritical;
			
			return entity;		
		}
		public TransChargesLaboratoryCritical FindByPrimaryKey(String transactionNo, String lisTestID)
		{
			return base.FindByPrimaryKey(transactionNo, lisTestID) as TransChargesLaboratoryCritical;
		}

		#region IEnumerable< TransChargesLaboratoryCritical> Members

		IEnumerator< TransChargesLaboratoryCritical> IEnumerable< TransChargesLaboratoryCritical>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesLaboratoryCritical;
			}
		}

		#endregion
		
		private TransChargesLaboratoryCriticalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesLaboratoryCritical' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransChargesLaboratoryCritical ({TransactionNo, LisTestID})")]
	[Serializable]
	public partial class TransChargesLaboratoryCritical : esTransChargesLaboratoryCritical
	{
		public TransChargesLaboratoryCritical()
		{
		}	
	
		public TransChargesLaboratoryCritical(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesLaboratoryCriticalMetadata.Meta();
			}
		}	
	
		override protected esTransChargesLaboratoryCriticalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesLaboratoryCriticalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public TransChargesLaboratoryCriticalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesLaboratoryCriticalQuery();
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
		public bool Load(TransChargesLaboratoryCriticalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private TransChargesLaboratoryCriticalQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransChargesLaboratoryCriticalQuery : esTransChargesLaboratoryCriticalQuery
	{
		public TransChargesLaboratoryCriticalQuery()
		{

		}		
		
		public TransChargesLaboratoryCriticalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "TransChargesLaboratoryCriticalQuery";
        }
	}

	[Serializable]
	public partial class TransChargesLaboratoryCriticalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesLaboratoryCriticalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.LisTestID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.LisTestID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.ReadByPhysicianID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReadByPhysicianDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.ReadByPhysicianDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.ReportedByNurseID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.ReportedByNurseDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.ReportedByNurseDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.CompletelyReportedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TransChargesLaboratoryCriticalMetadata.ColumnNames.CompletelyReportedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesLaboratoryCriticalMetadata.PropertyNames.CompletelyReportedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public TransChargesLaboratoryCriticalMetadata Meta()
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
			public const string LisTestID = "LisTestID";
			public const string ReadByPhysicianID = "ReadByPhysicianID";
			public const string ReadByPhysicianDateTime = "ReadByPhysicianDateTime";
			public const string ReportedByNurseID = "ReportedByNurseID";
			public const string ReportedByNurseDateTime = "ReportedByNurseDateTime";
			public const string CompletelyReportedByUserID = "CompletelyReportedByUserID";
			public const string CompletelyReportedDateTime = "CompletelyReportedDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string LisTestID = "LisTestID";
			public const string ReadByPhysicianID = "ReadByPhysicianID";
			public const string ReadByPhysicianDateTime = "ReadByPhysicianDateTime";
			public const string ReportedByNurseID = "ReportedByNurseID";
			public const string ReportedByNurseDateTime = "ReportedByNurseDateTime";
			public const string CompletelyReportedByUserID = "CompletelyReportedByUserID";
			public const string CompletelyReportedDateTime = "CompletelyReportedDateTime";
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
			lock (typeof(TransChargesLaboratoryCriticalMetadata))
			{
				if(TransChargesLaboratoryCriticalMetadata.mapDelegates == null)
				{
					TransChargesLaboratoryCriticalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransChargesLaboratoryCriticalMetadata.meta == null)
				{
					TransChargesLaboratoryCriticalMetadata.meta = new TransChargesLaboratoryCriticalMetadata();
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
				meta.AddTypeMap("LisTestID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReadByPhysicianID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReadByPhysicianDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReportedByNurseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReportedByNurseDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CompletelyReportedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CompletelyReportedDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "TransChargesLaboratoryCritical";
				meta.Destination = "TransChargesLaboratoryCritical";
				meta.spInsert = "proc_TransChargesLaboratoryCriticalInsert";				
				meta.spUpdate = "proc_TransChargesLaboratoryCriticalUpdate";		
				meta.spDelete = "proc_TransChargesLaboratoryCriticalDelete";
				meta.spLoadAll = "proc_TransChargesLaboratoryCriticalLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesLaboratoryCriticalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesLaboratoryCriticalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
