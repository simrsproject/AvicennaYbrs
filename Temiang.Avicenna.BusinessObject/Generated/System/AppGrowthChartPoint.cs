/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/30/2021 9:11:29 AM
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
	abstract public class esAppGrowthChartPointCollection : esEntityCollectionWAuditLog
	{
		public esAppGrowthChartPointCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppGrowthChartPointCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppGrowthChartPointQuery query)
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
			this.InitQuery(query as esAppGrowthChartPointQuery);
		}
		#endregion
			
		virtual public AppGrowthChartPoint DetachEntity(AppGrowthChartPoint entity)
		{
			return base.DetachEntity(entity) as AppGrowthChartPoint;
		}
		
		virtual public AppGrowthChartPoint AttachEntity(AppGrowthChartPoint entity)
		{
			return base.AttachEntity(entity) as AppGrowthChartPoint;
		}
		
		virtual public void Combine(AppGrowthChartPointCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppGrowthChartPoint this[int index]
		{
			get
			{
				return base[index] as AppGrowthChartPoint;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppGrowthChartPoint);
		}
	}

	[Serializable]
	abstract public class esAppGrowthChartPoint : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppGrowthChartPointQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppGrowthChartPoint()
		{
		}
	
		public esAppGrowthChartPoint(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String chartGroup, String chartType, String gender, String ageGroup, String seriesName, Decimal xValue)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(chartGroup, chartType, gender, ageGroup, seriesName, xValue);
			else
				return LoadByPrimaryKeyStoredProcedure(chartGroup, chartType, gender, ageGroup, seriesName, xValue);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String chartGroup, String chartType, String gender, String ageGroup, String seriesName, Decimal xValue)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(chartGroup, chartType, gender, ageGroup, seriesName, xValue);
			else
				return LoadByPrimaryKeyStoredProcedure(chartGroup, chartType, gender, ageGroup, seriesName, xValue);
		}
	
		private bool LoadByPrimaryKeyDynamic(String chartGroup, String chartType, String gender, String ageGroup, String seriesName, Decimal xValue)
		{
			esAppGrowthChartPointQuery query = this.GetDynamicQuery();
			query.Where(query.ChartGroup == chartGroup, query.ChartType == chartType, query.Gender == gender, query.AgeGroup == ageGroup, query.SeriesName == seriesName, query.XValue == xValue);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String chartGroup, String chartType, String gender, String ageGroup, String seriesName, Decimal xValue)
		{
			esParameters parms = new esParameters();
			parms.Add("ChartGroup",chartGroup);
			parms.Add("ChartType",chartType);
			parms.Add("Gender",gender);
			parms.Add("AgeGroup",ageGroup);
			parms.Add("SeriesName",seriesName);
			parms.Add("XValue",xValue);
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
						case "XValue": this.str.XValue = (string)value; break;
						case "YValue": this.str.YValue = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "XValue":
						
							if (value == null || value is System.Decimal)
								this.XValue = (System.Decimal?)value;
							break;
						case "YValue":
						
							if (value == null || value is System.Double)
								this.YValue = (System.Double?)value;
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
		/// Maps to AppGrowthChartPoint.ChartGroup
		/// </summary>
		virtual public System.String ChartGroup
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointMetadata.ColumnNames.ChartGroup);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointMetadata.ColumnNames.ChartGroup, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPoint.ChartType
		/// </summary>
		virtual public System.String ChartType
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointMetadata.ColumnNames.ChartType);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointMetadata.ColumnNames.ChartType, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPoint.Gender
		/// </summary>
		virtual public System.String Gender
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointMetadata.ColumnNames.Gender);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointMetadata.ColumnNames.Gender, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPoint.AgeGroup
		/// </summary>
		virtual public System.String AgeGroup
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointMetadata.ColumnNames.AgeGroup);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointMetadata.ColumnNames.AgeGroup, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPoint.SeriesName
		/// </summary>
		virtual public System.String SeriesName
		{
			get
			{
				return base.GetSystemString(AppGrowthChartPointMetadata.ColumnNames.SeriesName);
			}
			
			set
			{
				base.SetSystemString(AppGrowthChartPointMetadata.ColumnNames.SeriesName, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPoint.XValue
		/// </summary>
		virtual public System.Decimal? XValue
		{
			get
			{
				return base.GetSystemDecimal(AppGrowthChartPointMetadata.ColumnNames.XValue);
			}
			
			set
			{
				base.SetSystemDecimal(AppGrowthChartPointMetadata.ColumnNames.XValue, value);
			}
		}
		/// <summary>
		/// Maps to AppGrowthChartPoint.YValue
		/// </summary>
		virtual public System.Double? YValue
		{
			get
			{
				return base.GetSystemDouble(AppGrowthChartPointMetadata.ColumnNames.YValue);
			}
			
			set
			{
				base.SetSystemDouble(AppGrowthChartPointMetadata.ColumnNames.YValue, value);
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
			public esStrings(esAppGrowthChartPoint entity)
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
			public System.String XValue
			{
				get
				{
					System.Decimal? data = entity.XValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.XValue = null;
					else entity.XValue = Convert.ToDecimal(value);
				}
			}
			public System.String YValue
			{
				get
				{
					System.Double? data = entity.YValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YValue = null;
					else entity.YValue = Convert.ToDouble(value);
				}
			}
			private esAppGrowthChartPoint entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppGrowthChartPointQuery query)
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
				throw new Exception("esAppGrowthChartPoint can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppGrowthChartPoint : esAppGrowthChartPoint
	{	
	}

	[Serializable]
	abstract public class esAppGrowthChartPointQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppGrowthChartPointMetadata.Meta();
			}
		}	
			
		public esQueryItem ChartGroup
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointMetadata.ColumnNames.ChartGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartType
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointMetadata.ColumnNames.ChartType, esSystemType.String);
			}
		} 
			
		public esQueryItem Gender
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointMetadata.ColumnNames.Gender, esSystemType.String);
			}
		} 
			
		public esQueryItem AgeGroup
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointMetadata.ColumnNames.AgeGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem SeriesName
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointMetadata.ColumnNames.SeriesName, esSystemType.String);
			}
		} 
			
		public esQueryItem XValue
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointMetadata.ColumnNames.XValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem YValue
		{
			get
			{
				return new esQueryItem(this, AppGrowthChartPointMetadata.ColumnNames.YValue, esSystemType.Double);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppGrowthChartPointCollection")]
	public partial class AppGrowthChartPointCollection : esAppGrowthChartPointCollection, IEnumerable< AppGrowthChartPoint>
	{
		public AppGrowthChartPointCollection()
		{

		}	
		
		public static implicit operator List< AppGrowthChartPoint>(AppGrowthChartPointCollection coll)
		{
			List< AppGrowthChartPoint> list = new List< AppGrowthChartPoint>();
			
			foreach (AppGrowthChartPoint emp in coll)
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
				return  AppGrowthChartPointMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppGrowthChartPointQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppGrowthChartPoint(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppGrowthChartPoint();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppGrowthChartPointQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppGrowthChartPointQuery();
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
		public bool Load(AppGrowthChartPointQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppGrowthChartPoint AddNew()
		{
			AppGrowthChartPoint entity = base.AddNewEntity() as AppGrowthChartPoint;
			
			return entity;		
		}
		public AppGrowthChartPoint FindByPrimaryKey(String chartGroup, String chartType, String gender, String ageGroup, String seriesName, Decimal xValue)
		{
			return base.FindByPrimaryKey(chartGroup, chartType, gender, ageGroup, seriesName, xValue) as AppGrowthChartPoint;
		}

		#region IEnumerable< AppGrowthChartPoint> Members

		IEnumerator< AppGrowthChartPoint> IEnumerable< AppGrowthChartPoint>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppGrowthChartPoint;
			}
		}

		#endregion
		
		private AppGrowthChartPointQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppGrowthChartPoint' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppGrowthChartPoint ({ChartGroup, ChartType, Gender, AgeGroup, SeriesName, XValue})")]
	[Serializable]
	public partial class AppGrowthChartPoint : esAppGrowthChartPoint
	{
		public AppGrowthChartPoint()
		{
		}	
	
		public AppGrowthChartPoint(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppGrowthChartPointMetadata.Meta();
			}
		}	
	
		override protected esAppGrowthChartPointQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppGrowthChartPointQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppGrowthChartPointQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppGrowthChartPointQuery();
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
		public bool Load(AppGrowthChartPointQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppGrowthChartPointQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppGrowthChartPointQuery : esAppGrowthChartPointQuery
	{
		public AppGrowthChartPointQuery()
		{

		}		
		
		public AppGrowthChartPointQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppGrowthChartPointQuery";
        }
	}

	[Serializable]
	public partial class AppGrowthChartPointMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppGrowthChartPointMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppGrowthChartPointMetadata.ColumnNames.ChartGroup, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointMetadata.PropertyNames.ChartGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 1;
			c.HasDefault = true;
			c.Default = @"('L')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointMetadata.ColumnNames.ChartType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointMetadata.PropertyNames.ChartType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointMetadata.ColumnNames.Gender, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointMetadata.PropertyNames.Gender;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointMetadata.ColumnNames.AgeGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointMetadata.PropertyNames.AgeGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointMetadata.ColumnNames.SeriesName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppGrowthChartPointMetadata.PropertyNames.SeriesName;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointMetadata.ColumnNames.XValue, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppGrowthChartPointMetadata.PropertyNames.XValue;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(AppGrowthChartPointMetadata.ColumnNames.YValue, 6, typeof(System.Double), esSystemType.Double);
			c.PropertyName = AppGrowthChartPointMetadata.PropertyNames.YValue;
			c.NumericPrecision = 53;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppGrowthChartPointMetadata Meta()
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
			public const string XValue = "XValue";
			public const string YValue = "YValue";
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
			public const string XValue = "XValue";
			public const string YValue = "YValue";
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
			lock (typeof(AppGrowthChartPointMetadata))
			{
				if(AppGrowthChartPointMetadata.mapDelegates == null)
				{
					AppGrowthChartPointMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppGrowthChartPointMetadata.meta == null)
				{
					AppGrowthChartPointMetadata.meta = new AppGrowthChartPointMetadata();
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
				meta.AddTypeMap("XValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("YValue", new esTypeMap("float", "System.Double"));
		

				meta.Source = "AppGrowthChartPoint";
				meta.Destination = "AppGrowthChartPoint";
				meta.spInsert = "proc_AppGrowthChartPointInsert";				
				meta.spUpdate = "proc_AppGrowthChartPointUpdate";		
				meta.spDelete = "proc_AppGrowthChartPointDelete";
				meta.spLoadAll = "proc_AppGrowthChartPointLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppGrowthChartPointLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppGrowthChartPointMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
