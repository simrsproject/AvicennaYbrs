/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/21/2023 10:42:32 PM
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
	abstract public class esQuestionGroupCollection : esEntityCollectionWAuditLog
	{
		public esQuestionGroupCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "QuestionGroupCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esQuestionGroupQuery query)
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
			this.InitQuery(query as esQuestionGroupQuery);
		}
		#endregion
			
		virtual public QuestionGroup DetachEntity(QuestionGroup entity)
		{
			return base.DetachEntity(entity) as QuestionGroup;
		}
		
		virtual public QuestionGroup AttachEntity(QuestionGroup entity)
		{
			return base.AttachEntity(entity) as QuestionGroup;
		}
		
		virtual public void Combine(QuestionGroupCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QuestionGroup this[int index]
		{
			get
			{
				return base[index] as QuestionGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QuestionGroup);
		}
	}

	[Serializable]
	abstract public class esQuestionGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQuestionGroupQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esQuestionGroup()
		{
		}
	
		public esQuestionGroup(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String questionGroupID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionGroupID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String questionGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionGroupID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String questionGroupID)
		{
			esQuestionGroupQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionGroupID == questionGroupID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String questionGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionGroupID",questionGroupID);
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
						case "QuestionGroupID": this.str.QuestionGroupID = (string)value; break;
						case "QuestionGroupName": this.str.QuestionGroupName = (string)value; break;
						case "QuestionGroupNameEN": this.str.QuestionGroupNameEN = (string)value; break;
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SoapType": this.str.SoapType = (string)value; break;
						case "LabelWidth": this.str.LabelWidth = (string)value; break;
						case "Width": this.str.Width = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderNo":
						
							if (value == null || value is System.Int32)
								this.OrderNo = (System.Int32?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LabelWidth":
						
							if (value == null || value is System.Int32)
								this.LabelWidth = (System.Int32?)value;
							break;
						case "Width":
						
							if (value == null || value is System.Int32)
								this.Width = (System.Int32?)value;
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
		/// Maps to QuestionGroup.QuestionGroupID
		/// </summary>
		virtual public System.String QuestionGroupID
		{
			get
			{
				return base.GetSystemString(QuestionGroupMetadata.ColumnNames.QuestionGroupID);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupMetadata.ColumnNames.QuestionGroupID, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.QuestionGroupName
		/// </summary>
		virtual public System.String QuestionGroupName
		{
			get
			{
				return base.GetSystemString(QuestionGroupMetadata.ColumnNames.QuestionGroupName);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupMetadata.ColumnNames.QuestionGroupName, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.QuestionGroupNameEN
		/// </summary>
		virtual public System.String QuestionGroupNameEN
		{
			get
			{
				return base.GetSystemString(QuestionGroupMetadata.ColumnNames.QuestionGroupNameEN);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupMetadata.ColumnNames.QuestionGroupNameEN, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.OrderNo
		/// </summary>
		virtual public System.Int32? OrderNo
		{
			get
			{
				return base.GetSystemInt32(QuestionGroupMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemInt32(QuestionGroupMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(QuestionGroupMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionGroupMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QuestionGroupMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QuestionGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QuestionGroupMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.SoapType
		/// </summary>
		virtual public System.String SoapType
		{
			get
			{
				return base.GetSystemString(QuestionGroupMetadata.ColumnNames.SoapType);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupMetadata.ColumnNames.SoapType, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.LabelWidth
		/// </summary>
		virtual public System.Int32? LabelWidth
		{
			get
			{
				return base.GetSystemInt32(QuestionGroupMetadata.ColumnNames.LabelWidth);
			}
			
			set
			{
				base.SetSystemInt32(QuestionGroupMetadata.ColumnNames.LabelWidth, value);
			}
		}
		/// <summary>
		/// Maps to QuestionGroup.Width
		/// </summary>
		virtual public System.Int32? Width
		{
			get
			{
				return base.GetSystemInt32(QuestionGroupMetadata.ColumnNames.Width);
			}
			
			set
			{
				base.SetSystemInt32(QuestionGroupMetadata.ColumnNames.Width, value);
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
			public esStrings(esQuestionGroup entity)
			{
				this.entity = entity;
			}
			public System.String QuestionGroupID
			{
				get
				{
					System.String data = entity.QuestionGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionGroupID = null;
					else entity.QuestionGroupID = Convert.ToString(value);
				}
			}
			public System.String QuestionGroupName
			{
				get
				{
					System.String data = entity.QuestionGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionGroupName = null;
					else entity.QuestionGroupName = Convert.ToString(value);
				}
			}
			public System.String QuestionGroupNameEN
			{
				get
				{
					System.String data = entity.QuestionGroupNameEN;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionGroupNameEN = null;
					else entity.QuestionGroupNameEN = Convert.ToString(value);
				}
			}
			public System.String OrderNo
			{
				get
				{
					System.Int32? data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToInt32(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			public System.String SoapType
			{
				get
				{
					System.String data = entity.SoapType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SoapType = null;
					else entity.SoapType = Convert.ToString(value);
				}
			}
			public System.String LabelWidth
			{
				get
				{
					System.Int32? data = entity.LabelWidth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LabelWidth = null;
					else entity.LabelWidth = Convert.ToInt32(value);
				}
			}
			public System.String Width
			{
				get
				{
					System.Int32? data = entity.Width;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Width = null;
					else entity.Width = Convert.ToInt32(value);
				}
			}
			private esQuestionGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQuestionGroupQuery query)
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
				throw new Exception("esQuestionGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class QuestionGroup : esQuestionGroup
	{	
	}

	[Serializable]
	abstract public class esQuestionGroupQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return QuestionGroupMetadata.Meta();
			}
		}	
			
		public esQueryItem QuestionGroupID
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionGroupName
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.QuestionGroupName, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionGroupNameEN
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.QuestionGroupNameEN, esSystemType.String);
			}
		} 
			
		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.OrderNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem SoapType
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.SoapType, esSystemType.String);
			}
		} 
			
		public esQueryItem LabelWidth
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.LabelWidth, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Width
		{
			get
			{
				return new esQueryItem(this, QuestionGroupMetadata.ColumnNames.Width, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QuestionGroupCollection")]
	public partial class QuestionGroupCollection : esQuestionGroupCollection, IEnumerable< QuestionGroup>
	{
		public QuestionGroupCollection()
		{

		}	
		
		public static implicit operator List< QuestionGroup>(QuestionGroupCollection coll)
		{
			List< QuestionGroup> list = new List< QuestionGroup>();
			
			foreach (QuestionGroup emp in coll)
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
				return  QuestionGroupMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QuestionGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QuestionGroup();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public QuestionGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionGroupQuery();
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
		public bool Load(QuestionGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public QuestionGroup AddNew()
		{
			QuestionGroup entity = base.AddNewEntity() as QuestionGroup;
			
			return entity;		
		}
		public QuestionGroup FindByPrimaryKey(String questionGroupID)
		{
			return base.FindByPrimaryKey(questionGroupID) as QuestionGroup;
		}

		#region IEnumerable< QuestionGroup> Members

		IEnumerator< QuestionGroup> IEnumerable< QuestionGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QuestionGroup;
			}
		}

		#endregion
		
		private QuestionGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QuestionGroup' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("QuestionGroup ({QuestionGroupID})")]
	[Serializable]
	public partial class QuestionGroup : esQuestionGroup
	{
		public QuestionGroup()
		{
		}	
	
		public QuestionGroup(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QuestionGroupMetadata.Meta();
			}
		}	
	
		override protected esQuestionGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public QuestionGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionGroupQuery();
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
		public bool Load(QuestionGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private QuestionGroupQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class QuestionGroupQuery : esQuestionGroupQuery
	{
		public QuestionGroupQuery()
		{

		}		
		
		public QuestionGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "QuestionGroupQuery";
        }
	}

	[Serializable]
	public partial class QuestionGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QuestionGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.QuestionGroupID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.QuestionGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.QuestionGroupName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.QuestionGroupName;
			c.CharacterMaxLength = 400;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.QuestionGroupNameEN, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.QuestionGroupNameEN;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.OrderNo, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.OrderNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.SoapType, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.SoapType;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.LabelWidth, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.LabelWidth;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionGroupMetadata.ColumnNames.Width, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionGroupMetadata.PropertyNames.Width;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public QuestionGroupMetadata Meta()
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
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionGroupName = "QuestionGroupName";
			public const string QuestionGroupNameEN = "QuestionGroupNameEN";
			public const string OrderNo = "OrderNo";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SoapType = "SoapType";
			public const string LabelWidth = "LabelWidth";
			public const string Width = "Width";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionGroupName = "QuestionGroupName";
			public const string QuestionGroupNameEN = "QuestionGroupNameEN";
			public const string OrderNo = "OrderNo";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SoapType = "SoapType";
			public const string LabelWidth = "LabelWidth";
			public const string Width = "Width";
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
			lock (typeof(QuestionGroupMetadata))
			{
				if(QuestionGroupMetadata.mapDelegates == null)
				{
					QuestionGroupMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QuestionGroupMetadata.meta == null)
				{
					QuestionGroupMetadata.meta = new QuestionGroupMetadata();
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
				
				meta.AddTypeMap("QuestionGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionGroupNameEN", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SoapType", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LabelWidth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Width", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "QuestionGroup";
				meta.Destination = "QuestionGroup";
				meta.spInsert = "proc_QuestionGroupInsert";				
				meta.spUpdate = "proc_QuestionGroupUpdate";		
				meta.spDelete = "proc_QuestionGroupDelete";
				meta.spLoadAll = "proc_QuestionGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_QuestionGroupLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QuestionGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
