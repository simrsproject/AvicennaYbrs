/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/20/2023 8:32:38 PM
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
	abstract public class esRegistrationDischargeDetailCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationDischargeDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationDischargeDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationDischargeDetailQuery query)
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
			this.InitQuery(query as esRegistrationDischargeDetailQuery);
		}
		#endregion
			
		virtual public RegistrationDischargeDetail DetachEntity(RegistrationDischargeDetail entity)
		{
			return base.DetachEntity(entity) as RegistrationDischargeDetail;
		}
		
		virtual public RegistrationDischargeDetail AttachEntity(RegistrationDischargeDetail entity)
		{
			return base.AttachEntity(entity) as RegistrationDischargeDetail;
		}
		
		virtual public void Combine(RegistrationDischargeDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationDischargeDetail this[int index]
		{
			get
			{
				return base[index] as RegistrationDischargeDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationDischargeDetail);
		}
	}

	[Serializable]
	abstract public class esRegistrationDischargeDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationDischargeDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationDischargeDetail()
		{
		}
	
		public esRegistrationDischargeDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo)
		{
			esRegistrationDischargeDetailQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ParamedicName": this.str.ParamedicName = (string)value; break;
						case "SRUnitIntended": this.str.SRUnitIntended = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "OtherExamination": this.str.OtherExamination = (string)value; break;
						case "SignID": this.str.SignID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SignID":
						
							if (value == null || value is System.Int64)
								this.SignID = (System.Int64?)value;
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
		/// Maps to RegistrationDischargeDetail.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDischargeDetail.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDischargeDetail.ParamedicName
		/// </summary>
		virtual public System.String ParamedicName
		{
			get
			{
				return base.GetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.ParamedicName);
			}
			
			set
			{
				base.SetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.ParamedicName, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDischargeDetail.SRUnitIntended
		/// </summary>
		virtual public System.String SRUnitIntended
		{
			get
			{
				return base.GetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.SRUnitIntended);
			}
			
			set
			{
				base.SetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.SRUnitIntended, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDischargeDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDischargeDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDischargeDetail.OtherExamination
		/// </summary>
		virtual public System.String OtherExamination
		{
			get
			{
				return base.GetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.OtherExamination);
			}
			
			set
			{
				base.SetSystemString(RegistrationDischargeDetailMetadata.ColumnNames.OtherExamination, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDischargeDetail.SignID
		/// </summary>
		virtual public System.Int64? SignID
		{
			get
			{
				return base.GetSystemInt64(RegistrationDischargeDetailMetadata.ColumnNames.SignID);
			}
			
			set
			{
				base.SetSystemInt64(RegistrationDischargeDetailMetadata.ColumnNames.SignID, value);
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
			public esStrings(esRegistrationDischargeDetail entity)
			{
				this.entity = entity;
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
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String ParamedicName
			{
				get
				{
					System.String data = entity.ParamedicName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicName = null;
					else entity.ParamedicName = Convert.ToString(value);
				}
			}
			public System.String SRUnitIntended
			{
				get
				{
					System.String data = entity.SRUnitIntended;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRUnitIntended = null;
					else entity.SRUnitIntended = Convert.ToString(value);
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
			public System.String OtherExamination
			{
				get
				{
					System.String data = entity.OtherExamination;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherExamination = null;
					else entity.OtherExamination = Convert.ToString(value);
				}
			}
			public System.String SignID
			{
				get
				{
					System.Int64? data = entity.SignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignID = null;
					else entity.SignID = Convert.ToInt64(value);
				}
			}
			private esRegistrationDischargeDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationDischargeDetailQuery query)
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
				throw new Exception("esRegistrationDischargeDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationDischargeDetail : esRegistrationDischargeDetail
	{	
	}

	[Serializable]
	abstract public class esRegistrationDischargeDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDischargeDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicName
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.ParamedicName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRUnitIntended
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.SRUnitIntended, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem OtherExamination
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.OtherExamination, esSystemType.String);
			}
		} 
			
		public esQueryItem SignID
		{
			get
			{
				return new esQueryItem(this, RegistrationDischargeDetailMetadata.ColumnNames.SignID, esSystemType.Int64);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationDischargeDetailCollection")]
	public partial class RegistrationDischargeDetailCollection : esRegistrationDischargeDetailCollection, IEnumerable< RegistrationDischargeDetail>
	{
		public RegistrationDischargeDetailCollection()
		{

		}	
		
		public static implicit operator List< RegistrationDischargeDetail>(RegistrationDischargeDetailCollection coll)
		{
			List< RegistrationDischargeDetail> list = new List< RegistrationDischargeDetail>();
			
			foreach (RegistrationDischargeDetail emp in coll)
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
				return  RegistrationDischargeDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDischargeDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationDischargeDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationDischargeDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationDischargeDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDischargeDetailQuery();
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
		public bool Load(RegistrationDischargeDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationDischargeDetail AddNew()
		{
			RegistrationDischargeDetail entity = base.AddNewEntity() as RegistrationDischargeDetail;
			
			return entity;		
		}
		public RegistrationDischargeDetail FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as RegistrationDischargeDetail;
		}

		#region IEnumerable< RegistrationDischargeDetail> Members

		IEnumerator< RegistrationDischargeDetail> IEnumerable< RegistrationDischargeDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationDischargeDetail;
			}
		}

		#endregion
		
		private RegistrationDischargeDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationDischargeDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationDischargeDetail ({RegistrationNo})")]
	[Serializable]
	public partial class RegistrationDischargeDetail : esRegistrationDischargeDetail
	{
		public RegistrationDischargeDetail()
		{
		}	
	
		public RegistrationDischargeDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDischargeDetailMetadata.Meta();
			}
		}	
	
		override protected esRegistrationDischargeDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDischargeDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationDischargeDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDischargeDetailQuery();
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
		public bool Load(RegistrationDischargeDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationDischargeDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationDischargeDetailQuery : esRegistrationDischargeDetailQuery
	{
		public RegistrationDischargeDetailQuery()
		{

		}		
		
		public RegistrationDischargeDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationDischargeDetailQuery";
        }
	}

	[Serializable]
	public partial class RegistrationDischargeDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationDischargeDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.ParamedicName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.ParamedicName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.SRUnitIntended, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.SRUnitIntended;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.OtherExamination, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.OtherExamination;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationDischargeDetailMetadata.ColumnNames.SignID, 7, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = RegistrationDischargeDetailMetadata.PropertyNames.SignID;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RegistrationDischargeDetailMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string ParamedicName = "ParamedicName";
			public const string SRUnitIntended = "SRUnitIntended";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OtherExamination = "OtherExamination";
			public const string SignID = "SignID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string ParamedicName = "ParamedicName";
			public const string SRUnitIntended = "SRUnitIntended";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OtherExamination = "OtherExamination";
			public const string SignID = "SignID";
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
			lock (typeof(RegistrationDischargeDetailMetadata))
			{
				if(RegistrationDischargeDetailMetadata.mapDelegates == null)
				{
					RegistrationDischargeDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationDischargeDetailMetadata.meta == null)
				{
					RegistrationDischargeDetailMetadata.meta = new RegistrationDischargeDetailMetadata();
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
				
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRUnitIntended", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherExamination", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignID", new esTypeMap("bigint", "System.Int64"));
		

				meta.Source = "RegistrationDischargeDetail";
				meta.Destination = "RegistrationDischargeDetail";
				meta.spInsert = "proc_RegistrationDischargeDetailInsert";				
				meta.spUpdate = "proc_RegistrationDischargeDetailUpdate";		
				meta.spDelete = "proc_RegistrationDischargeDetailDelete";
				meta.spLoadAll = "proc_RegistrationDischargeDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationDischargeDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationDischargeDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
