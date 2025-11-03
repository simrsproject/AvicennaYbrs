/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/30/2021 9:11:52 AM
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
	abstract public class esAppGrowthChartPointSetCollection : esEntityCollectionWAuditLog
	{
		public esAppGrowthChartPointSetCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppGrowthChartPointSetCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppGrowthChartPointSetQuery query)
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
			this.InitQuery(query as esAppGrowthChartPointSetQuery);
		}
		#endregion
			
		virtual public AppGrowthChartPointSet DetachEntity(AppGrowthChartPointSet entity)
		{
			return base.DetachEntity(entity) as AppGrowthChartPointSet;
		}
		
		virtual public AppGrowthChartPointSet AttachEntity(AppGrowthChartPointSet entity)
		{
			return base.AttachEntity(entity) as AppGrowthChartPointSet;
		}
		
		virtual public void Combine(AppGrowthChartPointSetCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppGrowthChartPointSet this[int index]
		{
			get
			{
				return base[index] as AppGrowthChartPointSet;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppGrowthChartPointSet);
		}
	}

	[Serializable]
	abstract public class esAppGrowthChartPointSet : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppGrowthChartPointSetQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppGrowthChartPointSet()
		{
		}
	
		public esAppGrowthChartPointSet(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String chartGroup, String chartType, String gender, String ageGroup, String seriesName)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(chartGroup, chartType, gender, ageGroup, seriesName);
			else
				return LoadByPrimaryKeyStoredProcedure(chartGroup, chartType, gender, ageGroup, seriesName);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String chartGroup, String chartType, String gender, String ageGroup, String seriesName)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(chartGroup, chartType, gender, ageGroup, seriesName);
			else
				return LoadByPrimaryKeyStoredProcedure(chartGroup, chartType, gender, ageGroup, seriesName);
		}
	
		private bool LoadByPrimaryKeyDynamic(String chartGroup, String chartType, String gender, String ageGroup, String seriesName)
		{
			esAppGrowthChartPointSetQuery query = this.GetDynamicQuery();
			query.Where(query.ChartGroup == chartGroup, query.ChartType == chartType, query.Gender == gender, query.AgeGroup == ageGroup, query.SeriesName == seriesName);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String chartGroup, String chartType, String gender, String ageGroup, String seriesName)
		{
			esParameters parms = new esParameters();
			parms.Add("ChartGroup",chartGroup);
			parms.Add("ChartType",chartType);
			parms.Add("Gender",gender);
			parms.Add("AgeGroup",ageGroup);
			parms.Add("SeriesName",seriesName);
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
						case "ChartGroup": this.str.ChartGroup = (string)value; break;
						case "ChartType": this.str.ChartType = (string)value; break;
						case "Gender": this.str.Gender = (string)value; break;
						case "AgeGroup": this.str.AgeGroup = (string)value; break;
						case "SeriesName": this.str.SeriesName = (string)value; break;
						case "SeriesWidth": this.str.SeriesWidth = (string)value; break;
						case "IsVisible": this.str.IsVisible = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SeriesWidth":
						
							if (value == null || value is System.Int32)
								this.SeriesWidth = (System.Int32?)value;
							break;
						case "IsVisible":
						
							if (value == null || value is System.Boolean)
								this.IsVisible = (System.Boolean?)value;
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
		/// Maps to AppGrowthChartPointSet.ChartGroup
		/// </summary>
		virtual public System.String ChartGroup
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.ChartGroup);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.ChartGroup, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPointSet.ChartType
		/// </summary>
		virtual public System.String ChartType
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.ChartType);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.ChartType, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPointSet.Gender
		/// </summary>
		virtual public System.String Gender
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.Gender);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.Gender, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPointSet.AgeGroup
		/// </summary>
		virtual public System.String AgeGroup
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.AgeGroup);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.AgeGroup, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPointSet.SeriesName
		/// </summary>
		virtual public System.String SeriesName
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.SeriesName);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointSetMetadata.ColumnNames.SeriesName, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPointSet.SeriesWidth
		/// </summary>
		virtual public System.Int32? SeriesWidth
		{
			get
			{
				return base.GetSystemInt32(AppGrowthChartPointSetMetadata.ColumnNames.SeriesWidth);
			}
			
			set
			{
				base.SetSystemInt32(AppGrowthChartPointSetMetadata.ColumnNames.SeriesWidth, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPointSet.IsVisible
		/// </summary>
		virtual public System.Boolean? IsVisible
		{
			get
			{
				return base.GetSystemBoolean(AppGrowthChartPointSetMetadata.ColumnNames.IsVisible);
			}
			
			set
			{
				base.SetSystemBoolean(AppGrowthChartPointSetMetadata.ColumnNames.IsVisible, value);
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
			public esStrings(esAppGrowthChartPointSet entity)
			{
				this.entity = entity;
			}
			public System.String ChartGroup
			{
				get
				{
					System.String data = entity.ChartGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartGroup = null;
					else entity.ChartGroup = Convert.ToString(value);
				}
			}
			public System.String ChartType
			{
				get
				{
					System.String data = entity.ChartType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartType = null;
					else entity.ChartType = Convert.ToString(value);
				}
			}
			public System.String Gender
			{
				get
				{
					System.String data = entity.Gender;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Gender = null;
					else entity.Gender = Convert.ToString(value);
				}
			}
			public System.String AgeGroup
			{
				get
				{
					System.String data = entity.AgeGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeGroup = null;
					else entity.AgeGroup = Convert.ToString(value);
				}
			}
			public System.String SeriesName
			{
				get
				{
					System.String data = entity.SeriesName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeriesName = null;
					else entity.SeriesName = Convert.ToString(value);
				}
			}
			public System.String SeriesWidth
			{
				get
				{
					System.Int32? data = entity.SeriesWidth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeriesWidth = null;
					else entity.SeriesWidth = Convert.ToInt32(value);
				}
			}
			public System.String IsVisible
			{
				get
				{
					System.Boolean? data = entity.IsVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisible = null;
					else entity.IsVisible = Convert.ToBoolean(value);
				}
			}
			private esAppGrowthChartPointSet entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppGrowthChartPointSetQuery query)
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
				throw new Exception("esAppGrowthChartPointSet can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppGrowthChartPointSet : esAppGrowthChartPointSet
	{	
	}

	[Serializable]
	abstract public class esAppGrowthChartPointSetQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppGrowthChartPointSetMetadata.Meta();
			}
		}	
			
		public esQueryItem ChartGroup
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointSetMetadata.ColumnNames.ChartGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartType
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointSetMetadata.ColumnNames.ChartType, esSystemType.String);
			}
		} 
			
		public esQueryItem Gender
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointSetMetadata.ColumnNames.Gender, esSystemType.String);
			}
		} 
			
		public esQueryItem AgeGroup
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointSetMetadata.ColumnNames.AgeGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem SeriesName
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointSetMetadata.ColumnNames.SeriesName, esSystemType.String);
			}
		} 
			
		public esQueryItem SeriesWidth
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointSetMetadata.ColumnNames.SeriesWidth, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsVisible
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointSetMetadata.ColumnNames.IsVisible, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppGrowthChartPointSetCollection")]
	public partial class AppGrowthChartPointSetCollection : esAppGrowthChartPointSetCollection, IEnumerable< AppGrowthChartPointSet>
	{
		public AppGrowthChartPointSetCollection()
		{

		}	
		
		public static implicit operator List< AppGrowthChartPointSet>(AppGrowthChartPointSetCollection coll)
		{
			List< AppGrowthChartPointSet> list = new List< AppGrowthChartPointSet>();
			
			foreach (AppGrowthChartPointSet emp in coll)
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
				return  AppGrowthChartPointSetMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppGrowthChartPointSetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppGrowthChartPointSet(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppGrowthChartPointSet();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppGrowthChartPointSetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppGrowthChartPointSetQuery();
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
		public bool Load(AppGrowthChartPointSetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppGrowthChartPointSet AddNew()
		{
			AppGrowthChartPointSet entity = base.AddNewEntity() as AppGrowthChartPointSet;
			
			return entity;		
		}
		public AppGrowthChartPointSet FindByPrimaryKey(String chartGroup, String chartType, String gender, String ageGroup, String seriesName)
		{
			return base.FindByPrimaryKey(chartGroup, chartType, gender, ageGroup, seriesName) as AppGrowthChartPointSet;
		}

		#region IEnumerable< AppGrowthChartPointSet> Members

		IEnumerator< AppGrowthChartPointSet> IEnumerable< AppGrowthChartPointSet>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppGrowthChartPointSet;
			}
		}

		#endregion
		
		private AppGrowthChartPointSetQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppGrowthChartPointSet' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppGrowthChartPointSet ({ChartGroup, ChartType, Gender, AgeGroup, SeriesName})")]
	[Serializable]
	public partial class AppGrowthChartPointSet : esAppGrowthChartPointSet
	{
		public AppGrowthChartPointSet()
		{
		}	
	
		public AppGrowthChartPointSet(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppGrowthChartPointSetMetadata.Meta();
			}
		}	
	
		override protected esAppGrowthChartPointSetQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppGrowthChartPointSetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppGrowthChartPointSetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppGrowthChartPointSetQuery();
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
		public bool Load(AppGrowthChartPointSetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppGrowthChartPointSetQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppGrowthChartPointSetQuery : esAppGrowthChartPointSetQuery
	{
		public AppGrowthChartPointSetQuery()
		{

		}		
		
		public AppGrowthChartPointSetQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppGrowthChartPointSetQuery";
        }
	}

	[Serializable]
	public partial class AppGrowthChartPointSetMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppGrowthChartPointSetMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppGrowthChartPointSetMetadata.ColumnNames.ChartGroup, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointSetMetadata.PropertyNames.ChartGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 1;
			c.HasDefault = true;
			c.Default = @"('L')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointSetMetadata.ColumnNames.ChartType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointSetMetadata.PropertyNames.ChartType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointSetMetadata.ColumnNames.Gender, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointSetMetadata.PropertyNames.Gender;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointSetMetadata.ColumnNames.AgeGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointSetMetadata.PropertyNames.AgeGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointSetMetadata.ColumnNames.SeriesName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointSetMetadata.PropertyNames.SeriesName;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointSetMetadata.ColumnNames.SeriesWidth, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppGrowthChartPointSetMetadata.PropertyNames.SeriesWidth;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointSetMetadata.ColumnNames.IsVisible, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppGrowthChartPointSetMetadata.PropertyNames.IsVisible;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppGrowthChartPointSetMetadata Meta()
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
			public const string ChartGroup = "ChartGroup";
			public const string ChartType = "ChartType";
			public const string Gender = "Gender";
			public const string AgeGroup = "AgeGroup";
			public const string SeriesName = "SeriesName";
			public const string SeriesWidth = "SeriesWidth";
			public const string IsVisible = "IsVisible";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ChartGroup = "ChartGroup";
			public const string ChartType = "ChartType";
			public const string Gender = "Gender";
			public const string AgeGroup = "AgeGroup";
			public const string SeriesName = "SeriesName";
			public const string SeriesWidth = "SeriesWidth";
			public const string IsVisible = "IsVisible";
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
			lock (typeof(AppGrowthChartPointSetMetadata))
			{
				if(AppGrowthChartPointSetMetadata.mapDelegates == null)
				{
					AppGrowthChartPointSetMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppGrowthChartPointSetMetadata.meta == null)
				{
					AppGrowthChartPointSetMetadata.meta = new AppGrowthChartPointSetMetadata();
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
				
				meta.AddTypeMap("ChartGroup", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ChartType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Gender", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("AgeGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeriesName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeriesWidth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsVisible", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "AppGrowthChartPointSet";
				meta.Destination = "AppGrowthChartPointSet";
				meta.spInsert = "proc_AppGrowthChartPointSetInsert";				
				meta.spUpdate = "proc_AppGrowthChartPointSetUpdate";		
				meta.spDelete = "proc_AppGrowthChartPointSetDelete";
				meta.spLoadAll = "proc_AppGrowthChartPointSetLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppGrowthChartPointSetLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppGrowthChartPointSetMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
