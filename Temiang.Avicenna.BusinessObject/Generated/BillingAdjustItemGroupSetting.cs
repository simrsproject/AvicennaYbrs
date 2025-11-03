/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/17/2017 12:50:11 PM
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
	abstract public class esBillingAdjustItemGroupSettingCollection : esEntityCollectionWAuditLog
	{
		public esBillingAdjustItemGroupSettingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BillingAdjustItemGroupSettingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBillingAdjustItemGroupSettingQuery query)
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
			this.InitQuery(query as esBillingAdjustItemGroupSettingQuery);
		}
		#endregion
			
		virtual public BillingAdjustItemGroupSetting DetachEntity(BillingAdjustItemGroupSetting entity)
		{
			return base.DetachEntity(entity) as BillingAdjustItemGroupSetting;
		}
		
		virtual public BillingAdjustItemGroupSetting AttachEntity(BillingAdjustItemGroupSetting entity)
		{
			return base.AttachEntity(entity) as BillingAdjustItemGroupSetting;
		}
		
		virtual public void Combine(BillingAdjustItemGroupSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BillingAdjustItemGroupSetting this[int index]
		{
			get
			{
				return base[index] as BillingAdjustItemGroupSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BillingAdjustItemGroupSetting);
		}
	}

	[Serializable]
	abstract public class esBillingAdjustItemGroupSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBillingAdjustItemGroupSettingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBillingAdjustItemGroupSetting()
		{
		}
	
		public esBillingAdjustItemGroupSetting(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String itemGroupID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemGroupID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemGroupID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String itemGroupID)
		{
			esBillingAdjustItemGroupSettingQuery query = this.GetDynamicQuery();
			query.Where(query.ItemGroupID==itemGroupID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String itemGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemGroupID",itemGroupID);
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
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "DiscValue": this.str.DiscValue = (string)value; break;
						case "DiscSelection": this.str.DiscSelection = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DiscValue":
						
							if (value == null || value is System.Decimal)
								this.DiscValue = (System.Decimal?)value;
							break;
						case "DiscSelection":
						
							if (value == null || value is System.Int32)
								this.DiscSelection = (System.Int32?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to BillingAdjustItemGroupSetting.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemGroupSettingMetadata.ColumnNames.ItemGroupID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemGroupSettingMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemGroupSetting.DiscValue
		/// </summary>
		virtual public System.Decimal? DiscValue
		{
			get
			{
				return base.GetSystemDecimal(BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscValue);
			}
			
			set
			{
				base.SetSystemDecimal(BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscValue, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemGroupSetting.DiscSelection
		/// </summary>
		virtual public System.Int32? DiscSelection
		{
			get
			{
				return base.GetSystemInt32(BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscSelection);
			}
			
			set
			{
				base.SetSystemInt32(BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscSelection, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemGroupSetting.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemGroupSetting.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemGroupSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BillingAdjustItemGroupSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esBillingAdjustItemGroupSetting entity)
			{
				this.entity = entity;
			}
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String DiscValue
			{
				get
				{
					System.Decimal? data = entity.DiscValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscValue = null;
					else entity.DiscValue = Convert.ToDecimal(value);
				}
			}
			public System.String DiscSelection
			{
				get
				{
					System.Int32? data = entity.DiscSelection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscSelection = null;
					else entity.DiscSelection = Convert.ToInt32(value);
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
			private esBillingAdjustItemGroupSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBillingAdjustItemGroupSettingQuery query)
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
				throw new Exception("esBillingAdjustItemGroupSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BillingAdjustItemGroupSetting : esBillingAdjustItemGroupSetting
	{	
	}

	[Serializable]
	abstract public class esBillingAdjustItemGroupSettingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BillingAdjustItemGroupSettingMetadata.Meta();
			}
		}	
			
		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemGroupSettingMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem DiscValue
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DiscSelection
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscSelection, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BillingAdjustItemGroupSettingCollection")]
	public partial class BillingAdjustItemGroupSettingCollection : esBillingAdjustItemGroupSettingCollection, IEnumerable< BillingAdjustItemGroupSetting>
	{
		public BillingAdjustItemGroupSettingCollection()
		{

		}	
		
		public static implicit operator List< BillingAdjustItemGroupSetting>(BillingAdjustItemGroupSettingCollection coll)
		{
			List< BillingAdjustItemGroupSetting> list = new List< BillingAdjustItemGroupSetting>();
			
			foreach (BillingAdjustItemGroupSetting emp in coll)
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
				return  BillingAdjustItemGroupSettingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BillingAdjustItemGroupSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BillingAdjustItemGroupSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BillingAdjustItemGroupSetting();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BillingAdjustItemGroupSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BillingAdjustItemGroupSettingQuery();
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
		public bool Load(BillingAdjustItemGroupSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BillingAdjustItemGroupSetting AddNew()
		{
			BillingAdjustItemGroupSetting entity = base.AddNewEntity() as BillingAdjustItemGroupSetting;
			
			return entity;		
		}
		public BillingAdjustItemGroupSetting FindByPrimaryKey(String itemGroupID)
		{
			return base.FindByPrimaryKey(itemGroupID) as BillingAdjustItemGroupSetting;
		}

		#region IEnumerable< BillingAdjustItemGroupSetting> Members

		IEnumerator< BillingAdjustItemGroupSetting> IEnumerable< BillingAdjustItemGroupSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BillingAdjustItemGroupSetting;
			}
		}

		#endregion
		
		private BillingAdjustItemGroupSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BillingAdjustItemGroupSetting' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BillingAdjustItemGroupSetting ({ItemGroupID})")]
	[Serializable]
	public partial class BillingAdjustItemGroupSetting : esBillingAdjustItemGroupSetting
	{
		public BillingAdjustItemGroupSetting()
		{
		}	
	
		public BillingAdjustItemGroupSetting(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BillingAdjustItemGroupSettingMetadata.Meta();
			}
		}	
	
		override protected esBillingAdjustItemGroupSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BillingAdjustItemGroupSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BillingAdjustItemGroupSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BillingAdjustItemGroupSettingQuery();
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
		public bool Load(BillingAdjustItemGroupSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BillingAdjustItemGroupSettingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BillingAdjustItemGroupSettingQuery : esBillingAdjustItemGroupSettingQuery
	{
		public BillingAdjustItemGroupSettingQuery()
		{

		}		
		
		public BillingAdjustItemGroupSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BillingAdjustItemGroupSettingQuery";
        }
	}

	[Serializable]
	public partial class BillingAdjustItemGroupSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BillingAdjustItemGroupSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BillingAdjustItemGroupSettingMetadata.ColumnNames.ItemGroupID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemGroupSettingMetadata.PropertyNames.ItemGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscValue, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BillingAdjustItemGroupSettingMetadata.PropertyNames.DiscValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemGroupSettingMetadata.ColumnNames.DiscSelection, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BillingAdjustItemGroupSettingMetadata.PropertyNames.DiscSelection;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemGroupSettingMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemGroupSettingMetadata.ColumnNames.CreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BillingAdjustItemGroupSettingMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BillingAdjustItemGroupSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BillingAdjustItemGroupSettingMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BillingAdjustItemGroupSettingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BillingAdjustItemGroupSettingMetadata Meta()
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
			public const string ItemGroupID = "ItemGroupID";
			public const string DiscValue = "DiscValue";
			public const string DiscSelection = "DiscSelection";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ItemGroupID = "ItemGroupID";
			public const string DiscValue = "DiscValue";
			public const string DiscSelection = "DiscSelection";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
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
			lock (typeof(BillingAdjustItemGroupSettingMetadata))
			{
				if(BillingAdjustItemGroupSettingMetadata.mapDelegates == null)
				{
					BillingAdjustItemGroupSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BillingAdjustItemGroupSettingMetadata.meta == null)
				{
					BillingAdjustItemGroupSettingMetadata.meta = new BillingAdjustItemGroupSettingMetadata();
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
				
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiscValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DiscSelection", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "BillingAdjustItemGroupSetting";
				meta.Destination = "BillingAdjustItemGroupSetting";
				meta.spInsert = "proc_BillingAdjustItemGroupSettingInsert";				
				meta.spUpdate = "proc_BillingAdjustItemGroupSettingUpdate";		
				meta.spDelete = "proc_BillingAdjustItemGroupSettingDelete";
				meta.spLoadAll = "proc_BillingAdjustItemGroupSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_BillingAdjustItemGroupSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BillingAdjustItemGroupSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
