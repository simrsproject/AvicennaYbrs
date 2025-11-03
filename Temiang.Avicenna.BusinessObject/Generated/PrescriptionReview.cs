/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/18/2020 6:52:44 AM
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
	abstract public class esPrescriptionReviewCollection : esEntityCollectionWAuditLog
	{
		public esPrescriptionReviewCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PrescriptionReviewCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPrescriptionReviewQuery query)
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
			this.InitQuery(query as esPrescriptionReviewQuery);
		}
		#endregion
			
		virtual public PrescriptionReview DetachEntity(PrescriptionReview entity)
		{
			return base.DetachEntity(entity) as PrescriptionReview;
		}
		
		virtual public PrescriptionReview AttachEntity(PrescriptionReview entity)
		{
			return base.AttachEntity(entity) as PrescriptionReview;
		}
		
		virtual public void Combine(PrescriptionReviewCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PrescriptionReview this[int index]
		{
			get
			{
				return base[index] as PrescriptionReview;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PrescriptionReview);
		}
	}

	[Serializable]
	abstract public class esPrescriptionReview : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPrescriptionReviewQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPrescriptionReview()
		{
		}
	
		public esPrescriptionReview(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String prescriptionNo, String sRPrescReview)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescReview);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescReview);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo, String sRPrescReview)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescReview);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescReview);
		}
	
		private bool LoadByPrimaryKeyDynamic(String prescriptionNo, String sRPrescReview)
		{
			esPrescriptionReviewQuery query = this.GetDynamicQuery();
			query.Where(query.PrescriptionNo == prescriptionNo, query.SRPrescReview == sRPrescReview);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo, String sRPrescReview)
		{
			esParameters parms = new esParameters();
			parms.Add("PrescriptionNo",prescriptionNo);
			parms.Add("SRPrescReview",sRPrescReview);
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
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
						case "SRPrescReview": this.str.SRPrescReview = (string)value; break;
						case "IsRight": this.str.IsRight = (string)value; break;
						case "Information": this.str.Information = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsRight":
						
							if (value == null || value is System.Boolean)
								this.IsRight = (System.Boolean?)value;
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
		/// Maps to PrescriptionReview.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(PrescriptionReviewMetadata.ColumnNames.PrescriptionNo);
			}
			
			set
			{
				base.SetSystemString(PrescriptionReviewMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		/// <summary>
		/// Maps to PrescriptionReview.SRPrescReview
		/// </summary>
		virtual public System.String SRPrescReview
		{
			get
			{
				return base.GetSystemString(PrescriptionReviewMetadata.ColumnNames.SRPrescReview);
			}
			
			set
			{
				base.SetSystemString(PrescriptionReviewMetadata.ColumnNames.SRPrescReview, value);
			}
		}
		/// <summary>
		/// Maps to PrescriptionReview.IsRight
		/// </summary>
		virtual public System.Boolean? IsRight
		{
			get
			{
				return base.GetSystemBoolean(PrescriptionReviewMetadata.ColumnNames.IsRight);
			}
			
			set
			{
				base.SetSystemBoolean(PrescriptionReviewMetadata.ColumnNames.IsRight, value);
			}
		}
		/// <summary>
		/// Maps to PrescriptionReview.Information
		/// </summary>
		virtual public System.String Information
		{
			get
			{
				return base.GetSystemString(PrescriptionReviewMetadata.ColumnNames.Information);
			}
			
			set
			{
				base.SetSystemString(PrescriptionReviewMetadata.ColumnNames.Information, value);
			}
		}
		/// <summary>
		/// Maps to PrescriptionReview.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PrescriptionReview.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPrescriptionReview entity)
			{
				this.entity = entity;
			}
			public System.String PrescriptionNo
			{
				get
				{
					System.String data = entity.PrescriptionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionNo = null;
					else entity.PrescriptionNo = Convert.ToString(value);
				}
			}
			public System.String SRPrescReview
			{
				get
				{
					System.String data = entity.SRPrescReview;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPrescReview = null;
					else entity.SRPrescReview = Convert.ToString(value);
				}
			}
			public System.String IsRight
			{
				get
				{
					System.Boolean? data = entity.IsRight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRight = null;
					else entity.IsRight = Convert.ToBoolean(value);
				}
			}
			public System.String Information
			{
				get
				{
					System.String data = entity.Information;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Information = null;
					else entity.Information = Convert.ToString(value);
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
			private esPrescriptionReview entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPrescriptionReviewQuery query)
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
				throw new Exception("esPrescriptionReview can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PrescriptionReview : esPrescriptionReview
	{	
	}

	[Serializable]
	abstract public class esPrescriptionReviewQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PrescriptionReviewMetadata.Meta();
			}
		}	
			
		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, PrescriptionReviewMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPrescReview
		{
			get
			{
				return new esQueryItem(this, PrescriptionReviewMetadata.ColumnNames.SRPrescReview, esSystemType.String);
			}
		} 
			
		public esQueryItem IsRight
		{
			get
			{
				return new esQueryItem(this, PrescriptionReviewMetadata.ColumnNames.IsRight, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Information
		{
			get
			{
				return new esQueryItem(this, PrescriptionReviewMetadata.ColumnNames.Information, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PrescriptionReviewCollection")]
	public partial class PrescriptionReviewCollection : esPrescriptionReviewCollection, IEnumerable< PrescriptionReview>
	{
		public PrescriptionReviewCollection()
		{

		}	
		
		public static implicit operator List< PrescriptionReview>(PrescriptionReviewCollection coll)
		{
			List< PrescriptionReview> list = new List< PrescriptionReview>();
			
			foreach (PrescriptionReview emp in coll)
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
				return  PrescriptionReviewMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrescriptionReviewQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PrescriptionReview(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PrescriptionReview();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PrescriptionReviewQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrescriptionReviewQuery();
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
		public bool Load(PrescriptionReviewQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PrescriptionReview AddNew()
		{
			PrescriptionReview entity = base.AddNewEntity() as PrescriptionReview;
			
			return entity;		
		}
		public PrescriptionReview FindByPrimaryKey(String prescriptionNo, String sRPrescReview)
		{
			return base.FindByPrimaryKey(prescriptionNo, sRPrescReview) as PrescriptionReview;
		}

		#region IEnumerable< PrescriptionReview> Members

		IEnumerator< PrescriptionReview> IEnumerable< PrescriptionReview>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PrescriptionReview;
			}
		}

		#endregion
		
		private PrescriptionReviewQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PrescriptionReview' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PrescriptionReview ({PrescriptionNo, SRPrescReview})")]
	[Serializable]
	public partial class PrescriptionReview : esPrescriptionReview
	{
		public PrescriptionReview()
		{
		}	
	
		public PrescriptionReview(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PrescriptionReviewMetadata.Meta();
			}
		}	
	
		override protected esPrescriptionReviewQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrescriptionReviewQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PrescriptionReviewQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrescriptionReviewQuery();
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
		public bool Load(PrescriptionReviewQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PrescriptionReviewQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PrescriptionReviewQuery : esPrescriptionReviewQuery
	{
		public PrescriptionReviewQuery()
		{

		}		
		
		public PrescriptionReviewQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PrescriptionReviewQuery";
        }
	}

	[Serializable]
	public partial class PrescriptionReviewMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PrescriptionReviewMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PrescriptionReviewMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PrescriptionReviewMetadata.PropertyNames.PrescriptionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PrescriptionReviewMetadata.ColumnNames.SRPrescReview, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PrescriptionReviewMetadata.PropertyNames.SRPrescReview;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PrescriptionReviewMetadata.ColumnNames.IsRight, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PrescriptionReviewMetadata.PropertyNames.IsRight;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PrescriptionReviewMetadata.ColumnNames.Information, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PrescriptionReviewMetadata.PropertyNames.Information;
			c.CharacterMaxLength = 800;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PrescriptionReviewMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PrescriptionReviewMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PrescriptionReviewMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PrescriptionReviewMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PrescriptionReviewMetadata Meta()
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
			public const string PrescriptionNo = "PrescriptionNo";
			public const string SRPrescReview = "SRPrescReview";
			public const string IsRight = "IsRight";
			public const string Information = "Information";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PrescriptionNo = "PrescriptionNo";
			public const string SRPrescReview = "SRPrescReview";
			public const string IsRight = "IsRight";
			public const string Information = "Information";
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
			lock (typeof(PrescriptionReviewMetadata))
			{
				if(PrescriptionReviewMetadata.mapDelegates == null)
				{
					PrescriptionReviewMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PrescriptionReviewMetadata.meta == null)
				{
					PrescriptionReviewMetadata.meta = new PrescriptionReviewMetadata();
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
				
				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPrescReview", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRight", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Information", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PrescriptionReview";
				meta.Destination = "PrescriptionReview";
				meta.spInsert = "proc_PrescriptionReviewInsert";				
				meta.spUpdate = "proc_PrescriptionReviewUpdate";		
				meta.spDelete = "proc_PrescriptionReviewDelete";
				meta.spLoadAll = "proc_PrescriptionReviewLoadAll";
				meta.spLoadByPrimaryKey = "proc_PrescriptionReviewLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PrescriptionReviewMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
