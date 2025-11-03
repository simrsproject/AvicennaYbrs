/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/11/2020 5:36:51 PM
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
	abstract public class esRegistrationCounselingLineCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationCounselingLineCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationCounselingLineCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationCounselingLineQuery query)
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
			this.InitQuery(query as esRegistrationCounselingLineQuery);
		}
		#endregion
			
		virtual public RegistrationCounselingLine DetachEntity(RegistrationCounselingLine entity)
		{
			return base.DetachEntity(entity) as RegistrationCounselingLine;
		}
		
		virtual public RegistrationCounselingLine AttachEntity(RegistrationCounselingLine entity)
		{
			return base.AttachEntity(entity) as RegistrationCounselingLine;
		}
		
		virtual public void Combine(RegistrationCounselingLineCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationCounselingLine this[int index]
		{
			get
			{
				return base[index] as RegistrationCounselingLine;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationCounselingLine);
		}
	}

	[Serializable]
	abstract public class esRegistrationCounselingLine : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationCounselingLineQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationCounselingLine()
		{
		}
	
		public esRegistrationCounselingLine(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 counselingNo, String sRDrugCounseling)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, counselingNo, sRDrugCounseling);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, counselingNo, sRDrugCounseling);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 counselingNo, String sRDrugCounseling)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, counselingNo, sRDrugCounseling);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, counselingNo, sRDrugCounseling);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 counselingNo, String sRDrugCounseling)
		{
			esRegistrationCounselingLineQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.CounselingNo == counselingNo, query.SRDrugCounseling == sRDrugCounseling);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 counselingNo, String sRDrugCounseling)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("CounselingNo",counselingNo);
			parms.Add("SRDrugCounseling",sRDrugCounseling);
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
						case "CounselingNo": this.str.CounselingNo = (string)value; break;
						case "SRDrugCounseling": this.str.SRDrugCounseling = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CounselingNo":
						
							if (value == null || value is System.Int32)
								this.CounselingNo = (System.Int32?)value;
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
		/// Maps to RegistrationCounselingLine.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingLineMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingLineMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounselingLine.CounselingNo
		/// </summary>
		virtual public System.Int32? CounselingNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationCounselingLineMetadata.ColumnNames.CounselingNo);
			}
			
			set
			{
				base.SetSystemInt32(RegistrationCounselingLineMetadata.ColumnNames.CounselingNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounselingLine.SRDrugCounseling
		/// </summary>
		virtual public System.String SRDrugCounseling
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingLineMetadata.ColumnNames.SRDrugCounseling);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingLineMetadata.ColumnNames.SRDrugCounseling, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounselingLine.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingLineMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingLineMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounselingLine.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingLineMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingLineMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounselingLine.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationCounselingLineMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationCounselingLineMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRegistrationCounselingLine entity)
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
			public System.String CounselingNo
			{
				get
				{
					System.Int32? data = entity.CounselingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounselingNo = null;
					else entity.CounselingNo = Convert.ToInt32(value);
				}
			}
			public System.String SRDrugCounseling
			{
				get
				{
					System.String data = entity.SRDrugCounseling;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDrugCounseling = null;
					else entity.SRDrugCounseling = Convert.ToString(value);
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
			private esRegistrationCounselingLine entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationCounselingLineQuery query)
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
				throw new Exception("esRegistrationCounselingLine can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationCounselingLine : esRegistrationCounselingLine
	{	
	}

	[Serializable]
	abstract public class esRegistrationCounselingLineQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCounselingLineMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingLineMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem CounselingNo
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingLineMetadata.ColumnNames.CounselingNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRDrugCounseling
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingLineMetadata.ColumnNames.SRDrugCounseling, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingLineMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationCounselingLineCollection")]
	public partial class RegistrationCounselingLineCollection : esRegistrationCounselingLineCollection, IEnumerable< RegistrationCounselingLine>
	{
		public RegistrationCounselingLineCollection()
		{

		}	
		
		public static implicit operator List< RegistrationCounselingLine>(RegistrationCounselingLineCollection coll)
		{
			List< RegistrationCounselingLine> list = new List< RegistrationCounselingLine>();
			
			foreach (RegistrationCounselingLine emp in coll)
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
				return  RegistrationCounselingLineMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCounselingLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationCounselingLine(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationCounselingLine();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationCounselingLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCounselingLineQuery();
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
		public bool Load(RegistrationCounselingLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationCounselingLine AddNew()
		{
			RegistrationCounselingLine entity = base.AddNewEntity() as RegistrationCounselingLine;
			
			return entity;		
		}
		public RegistrationCounselingLine FindByPrimaryKey(String registrationNo, Int32 counselingNo, String sRDrugCounseling)
		{
			return base.FindByPrimaryKey(registrationNo, counselingNo, sRDrugCounseling) as RegistrationCounselingLine;
		}

		#region IEnumerable< RegistrationCounselingLine> Members

		IEnumerator< RegistrationCounselingLine> IEnumerable< RegistrationCounselingLine>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationCounselingLine;
			}
		}

		#endregion
		
		private RegistrationCounselingLineQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationCounselingLine' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationCounselingLine ({RegistrationNo, CounselingNo, SRDrugCounseling})")]
	[Serializable]
	public partial class RegistrationCounselingLine : esRegistrationCounselingLine
	{
		public RegistrationCounselingLine()
		{
		}	
	
		public RegistrationCounselingLine(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCounselingLineMetadata.Meta();
			}
		}	
	
		override protected esRegistrationCounselingLineQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCounselingLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationCounselingLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCounselingLineQuery();
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
		public bool Load(RegistrationCounselingLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationCounselingLineQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationCounselingLineQuery : esRegistrationCounselingLineQuery
	{
		public RegistrationCounselingLineQuery()
		{

		}		
		
		public RegistrationCounselingLineQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationCounselingLineQuery";
        }
	}

	[Serializable]
	public partial class RegistrationCounselingLineMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationCounselingLineMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationCounselingLineMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingLineMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingLineMetadata.ColumnNames.CounselingNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationCounselingLineMetadata.PropertyNames.CounselingNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingLineMetadata.ColumnNames.SRDrugCounseling, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingLineMetadata.PropertyNames.SRDrugCounseling;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingLineMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingLineMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingLineMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingLineMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingLineMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationCounselingLineMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RegistrationCounselingLineMetadata Meta()
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
			public const string CounselingNo = "CounselingNo";
			public const string SRDrugCounseling = "SRDrugCounseling";
			public const string Notes = "Notes";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string CounselingNo = "CounselingNo";
			public const string SRDrugCounseling = "SRDrugCounseling";
			public const string Notes = "Notes";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(RegistrationCounselingLineMetadata))
			{
				if(RegistrationCounselingLineMetadata.mapDelegates == null)
				{
					RegistrationCounselingLineMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationCounselingLineMetadata.meta == null)
				{
					RegistrationCounselingLineMetadata.meta = new RegistrationCounselingLineMetadata();
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
				meta.AddTypeMap("CounselingNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRDrugCounseling", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "RegistrationCounselingLine";
				meta.Destination = "RegistrationCounselingLine";
				meta.spInsert = "proc_RegistrationCounselingLineInsert";				
				meta.spUpdate = "proc_RegistrationCounselingLineUpdate";		
				meta.spDelete = "proc_RegistrationCounselingLineDelete";
				meta.spLoadAll = "proc_RegistrationCounselingLineLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationCounselingLineLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationCounselingLineMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
