/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/24/2022 1:32:44 PM
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
	abstract public class esCoorporateGradeCollection : esEntityCollectionWAuditLog
	{
		public esCoorporateGradeCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "CoorporateGradeCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esCoorporateGradeQuery query)
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
			this.InitQuery(query as esCoorporateGradeQuery);
		}
		#endregion
			
		virtual public CoorporateGrade DetachEntity(CoorporateGrade entity)
		{
			return base.DetachEntity(entity) as CoorporateGrade;
		}
		
		virtual public CoorporateGrade AttachEntity(CoorporateGrade entity)
		{
			return base.AttachEntity(entity) as CoorporateGrade;
		}
		
		virtual public void Combine(CoorporateGradeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CoorporateGrade this[int index]
		{
			get
			{
				return base[index] as CoorporateGrade;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CoorporateGrade);
		}
	}

	[Serializable]
	abstract public class esCoorporateGrade : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCoorporateGradeQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esCoorporateGrade()
		{
		}
	
		public esCoorporateGrade(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 coorporateGradeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(coorporateGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(coorporateGradeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 coorporateGradeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(coorporateGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(coorporateGradeID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 coorporateGradeID)
		{
			esCoorporateGradeQuery query = this.GetDynamicQuery();
			query.Where(query.CoorporateGradeID==coorporateGradeID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 coorporateGradeID)
		{
			esParameters parms = new esParameters();
			parms.Add("CoorporateGradeID",coorporateGradeID);
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
						case "CoorporateGradeID": this.str.CoorporateGradeID = (string)value; break;
						case "CoorporateGradeLevel": this.str.CoorporateGradeLevel = (string)value; break;
						case "CoorporateGradeMin": this.str.CoorporateGradeMin = (string)value; break;
						case "CoorporateGradeMax": this.str.CoorporateGradeMax = (string)value; break;
						case "CoorporateGradeInterval": this.str.CoorporateGradeInterval = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "CoorporateGradeCoefficient": this.str.CoorporateGradeCoefficient = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CoorporateGradeID":
						
							if (value == null || value is System.Int32)
								this.CoorporateGradeID = (System.Int32?)value;
							break;
						case "CoorporateGradeLevel":
						
							if (value == null || value is System.Int16)
								this.CoorporateGradeLevel = (System.Int16?)value;
							break;
						case "CoorporateGradeMin":
						
							if (value == null || value is System.Decimal)
								this.CoorporateGradeMin = (System.Decimal?)value;
							break;
						case "CoorporateGradeMax":
						
							if (value == null || value is System.Decimal)
								this.CoorporateGradeMax = (System.Decimal?)value;
							break;
						case "CoorporateGradeInterval":
						
							if (value == null || value is System.Decimal)
								this.CoorporateGradeInterval = (System.Decimal?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "CoorporateGradeCoefficient":
						
							if (value == null || value is System.Decimal)
								this.CoorporateGradeCoefficient = (System.Decimal?)value;
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
		/// Maps to CoorporateGrade.CoorporateGradeID
		/// </summary>
		virtual public System.Int32? CoorporateGradeID
		{
			get
			{
				return base.GetSystemInt32(CoorporateGradeMetadata.ColumnNames.CoorporateGradeID);
			}
			
			set
			{
				base.SetSystemInt32(CoorporateGradeMetadata.ColumnNames.CoorporateGradeID, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.CoorporateGradeLevel
		/// </summary>
		virtual public System.Int16? CoorporateGradeLevel
		{
			get
			{
				return base.GetSystemInt16(CoorporateGradeMetadata.ColumnNames.CoorporateGradeLevel);
			}
			
			set
			{
				base.SetSystemInt16(CoorporateGradeMetadata.ColumnNames.CoorporateGradeLevel, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.CoorporateGradeMin
		/// </summary>
		virtual public System.Decimal? CoorporateGradeMin
		{
			get
			{
				return base.GetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeMin);
			}
			
			set
			{
				base.SetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeMin, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.CoorporateGradeMax
		/// </summary>
		virtual public System.Decimal? CoorporateGradeMax
		{
			get
			{
				return base.GetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeMax);
			}
			
			set
			{
				base.SetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeMax, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.CoorporateGradeInterval
		/// </summary>
		virtual public System.Decimal? CoorporateGradeInterval
		{
			get
			{
				return base.GetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeInterval);
			}
			
			set
			{
				base.SetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeInterval, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CoorporateGradeMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CoorporateGradeMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(CoorporateGradeMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(CoorporateGradeMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CoorporateGradeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CoorporateGradeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CoorporateGradeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CoorporateGradeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CoorporateGrade.CoorporateGradeCoefficient
		/// </summary>
		virtual public System.Decimal? CoorporateGradeCoefficient
		{
			get
			{
				return base.GetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeCoefficient);
			}
			
			set
			{
				base.SetSystemDecimal(CoorporateGradeMetadata.ColumnNames.CoorporateGradeCoefficient, value);
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
			public esStrings(esCoorporateGrade entity)
			{
				this.entity = entity;
			}
			public System.String CoorporateGradeID
			{
				get
				{
					System.Int32? data = entity.CoorporateGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeID = null;
					else entity.CoorporateGradeID = Convert.ToInt32(value);
				}
			}
			public System.String CoorporateGradeLevel
			{
				get
				{
					System.Int16? data = entity.CoorporateGradeLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeLevel = null;
					else entity.CoorporateGradeLevel = Convert.ToInt16(value);
				}
			}
			public System.String CoorporateGradeMin
			{
				get
				{
					System.Decimal? data = entity.CoorporateGradeMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeMin = null;
					else entity.CoorporateGradeMin = Convert.ToDecimal(value);
				}
			}
			public System.String CoorporateGradeMax
			{
				get
				{
					System.Decimal? data = entity.CoorporateGradeMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeMax = null;
					else entity.CoorporateGradeMax = Convert.ToDecimal(value);
				}
			}
			public System.String CoorporateGradeInterval
			{
				get
				{
					System.Decimal? data = entity.CoorporateGradeInterval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeInterval = null;
					else entity.CoorporateGradeInterval = Convert.ToDecimal(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			public System.String CoorporateGradeCoefficient
			{
				get
				{
					System.Decimal? data = entity.CoorporateGradeCoefficient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeCoefficient = null;
					else entity.CoorporateGradeCoefficient = Convert.ToDecimal(value);
				}
			}
			private esCoorporateGrade entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCoorporateGradeQuery query)
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
				throw new Exception("esCoorporateGrade can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CoorporateGrade : esCoorporateGrade
	{	
	}

	[Serializable]
	abstract public class esCoorporateGradeQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return CoorporateGradeMetadata.Meta();
			}
		}	
			
		public esQueryItem CoorporateGradeID
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CoorporateGradeID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CoorporateGradeLevel
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CoorporateGradeLevel, esSystemType.Int16);
			}
		} 
			
		public esQueryItem CoorporateGradeMin
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CoorporateGradeMin, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CoorporateGradeMax
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CoorporateGradeMax, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CoorporateGradeInterval
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CoorporateGradeInterval, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CoorporateGradeCoefficient
		{
			get
			{
				return new esQueryItem(this, CoorporateGradeMetadata.ColumnNames.CoorporateGradeCoefficient, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CoorporateGradeCollection")]
	public partial class CoorporateGradeCollection : esCoorporateGradeCollection, IEnumerable< CoorporateGrade>
	{
		public CoorporateGradeCollection()
		{

		}	
		
		public static implicit operator List< CoorporateGrade>(CoorporateGradeCollection coll)
		{
			List< CoorporateGrade> list = new List< CoorporateGrade>();
			
			foreach (CoorporateGrade emp in coll)
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
				return  CoorporateGradeMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CoorporateGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CoorporateGrade(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CoorporateGrade();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public CoorporateGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CoorporateGradeQuery();
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
		public bool Load(CoorporateGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CoorporateGrade AddNew()
		{
			CoorporateGrade entity = base.AddNewEntity() as CoorporateGrade;
			
			return entity;		
		}
		public CoorporateGrade FindByPrimaryKey(Int32 coorporateGradeID)
		{
			return base.FindByPrimaryKey(coorporateGradeID) as CoorporateGrade;
		}

		#region IEnumerable< CoorporateGrade> Members

		IEnumerator< CoorporateGrade> IEnumerable< CoorporateGrade>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CoorporateGrade;
			}
		}

		#endregion
		
		private CoorporateGradeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CoorporateGrade' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CoorporateGrade ({CoorporateGradeID})")]
	[Serializable]
	public partial class CoorporateGrade : esCoorporateGrade
	{
		public CoorporateGrade()
		{
		}	
	
		public CoorporateGrade(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CoorporateGradeMetadata.Meta();
			}
		}	
	
		override protected esCoorporateGradeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CoorporateGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public CoorporateGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CoorporateGradeQuery();
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
		public bool Load(CoorporateGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private CoorporateGradeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CoorporateGradeQuery : esCoorporateGradeQuery
	{
		public CoorporateGradeQuery()
		{

		}		
		
		public CoorporateGradeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "CoorporateGradeQuery";
        }
	}

	[Serializable]
	public partial class CoorporateGradeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CoorporateGradeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CoorporateGradeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CoorporateGradeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CoorporateGradeLevel, 1, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CoorporateGradeLevel;
			c.NumericPrecision = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CoorporateGradeMin, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CoorporateGradeMin;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CoorporateGradeMax, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CoorporateGradeMax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CoorporateGradeInterval, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CoorporateGradeInterval;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CoorporateGradeMetadata.ColumnNames.CoorporateGradeCoefficient, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CoorporateGradeMetadata.PropertyNames.CoorporateGradeCoefficient;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public CoorporateGradeMetadata Meta()
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
			public const string CoorporateGradeID = "CoorporateGradeID";
			public const string CoorporateGradeLevel = "CoorporateGradeLevel";
			public const string CoorporateGradeMin = "CoorporateGradeMin";
			public const string CoorporateGradeMax = "CoorporateGradeMax";
			public const string CoorporateGradeInterval = "CoorporateGradeInterval";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CoorporateGradeCoefficient = "CoorporateGradeCoefficient";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string CoorporateGradeID = "CoorporateGradeID";
			public const string CoorporateGradeLevel = "CoorporateGradeLevel";
			public const string CoorporateGradeMin = "CoorporateGradeMin";
			public const string CoorporateGradeMax = "CoorporateGradeMax";
			public const string CoorporateGradeInterval = "CoorporateGradeInterval";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CoorporateGradeCoefficient = "CoorporateGradeCoefficient";
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
			lock (typeof(CoorporateGradeMetadata))
			{
				if(CoorporateGradeMetadata.mapDelegates == null)
				{
					CoorporateGradeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CoorporateGradeMetadata.meta == null)
				{
					CoorporateGradeMetadata.meta = new CoorporateGradeMetadata();
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
				
				meta.AddTypeMap("CoorporateGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CoorporateGradeLevel", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("CoorporateGradeMin", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CoorporateGradeMax", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CoorporateGradeInterval", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoorporateGradeCoefficient", new esTypeMap("decimal", "System.Decimal"));
		

				meta.Source = "CoorporateGrade";
				meta.Destination = "CoorporateGrade";
				meta.spInsert = "proc_CoorporateGradeInsert";				
				meta.spUpdate = "proc_CoorporateGradeUpdate";		
				meta.spDelete = "proc_CoorporateGradeDelete";
				meta.spLoadAll = "proc_CoorporateGradeLoadAll";
				meta.spLoadByPrimaryKey = "proc_CoorporateGradeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CoorporateGradeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
