/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/22/2023 2:44:33 PM
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
	abstract public class esGuarantorRecipeMarginValueCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorRecipeMarginValueCollection()
		{

		}
		
		protected override string GetConnectionName()
		{
			return "sa";
		}
		
		protected override string GetCollectionName()
		{
			return "GuarantorRecipeMarginValueCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esGuarantorRecipeMarginValueQuery query)
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
			this.InitQuery(query as esGuarantorRecipeMarginValueQuery);
		}
		#endregion
			
		virtual public GuarantorRecipeMarginValue DetachEntity(GuarantorRecipeMarginValue entity)
		{
			return base.DetachEntity(entity) as GuarantorRecipeMarginValue;
		}
		
		virtual public GuarantorRecipeMarginValue AttachEntity(GuarantorRecipeMarginValue entity)
		{
			return base.AttachEntity(entity) as GuarantorRecipeMarginValue;
		}
		
		virtual public void Combine(GuarantorRecipeMarginValueCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorRecipeMarginValue this[int index]
		{
			get
			{
				return base[index] as GuarantorRecipeMarginValue;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorRecipeMarginValue);
		}
	}

	[Serializable]
	abstract public class esGuarantorRecipeMarginValue : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorRecipeMarginValueQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esGuarantorRecipeMarginValue()
		{
		}
	
		public esGuarantorRecipeMarginValue(DataRow row)
			: base(row)
		{
		}
		
		protected override string GetConnectionName()
		{
			return "sa";
		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String guarantorID, Int32 counterID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, counterID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String guarantorID, Int32 counterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, counterID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String guarantorID, Int32 counterID)
		{
			esGuarantorRecipeMarginValueQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.CounterID == counterID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String guarantorID, Int32 counterID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);
			parms.Add("CounterID",counterID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "CounterID": this.str.CounterID = (string)value; break;
						case "StartingValue": this.str.StartingValue = (string)value; break;
						case "EndingValue": this.str.EndingValue = (string)value; break;
						case "RecipeAmount": this.str.RecipeAmount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CounterID":
						
							if (value == null || value is System.Int32)
								this.CounterID = (System.Int32?)value;
							break;
						case "StartingValue":
						
							if (value == null || value is System.Decimal)
								this.StartingValue = (System.Decimal?)value;
							break;
						case "EndingValue":
						
							if (value == null || value is System.Decimal)
								this.EndingValue = (System.Decimal?)value;
							break;
						case "RecipeAmount":
						
							if (value == null || value is System.Decimal)
								this.RecipeAmount = (System.Decimal?)value;
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
		/// Maps to GuarantorRecipeMarginValue.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorRecipeMarginValueMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorRecipeMarginValueMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorRecipeMarginValue.CounterID
		/// </summary>
		virtual public System.Int32? CounterID
		{
			get
			{
				return base.GetSystemInt32(GuarantorRecipeMarginValueMetadata.ColumnNames.CounterID);
			}
			
			set
			{
				base.SetSystemInt32(GuarantorRecipeMarginValueMetadata.ColumnNames.CounterID, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorRecipeMarginValue.StartingValue
		/// </summary>
		virtual public System.Decimal? StartingValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorRecipeMarginValueMetadata.ColumnNames.StartingValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorRecipeMarginValueMetadata.ColumnNames.StartingValue, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorRecipeMarginValue.EndingValue
		/// </summary>
		virtual public System.Decimal? EndingValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorRecipeMarginValueMetadata.ColumnNames.EndingValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorRecipeMarginValueMetadata.ColumnNames.EndingValue, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorRecipeMarginValue.RecipeAmount
		/// </summary>
		virtual public System.Decimal? RecipeAmount
		{
			get
			{
				return base.GetSystemDecimal(GuarantorRecipeMarginValueMetadata.ColumnNames.RecipeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorRecipeMarginValueMetadata.ColumnNames.RecipeAmount, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorRecipeMarginValue.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorRecipeMarginValue.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorRecipeMarginValue entity)
			{
				this.entity = entity;
			}
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
			public System.String CounterID
			{
				get
				{
					System.Int32? data = entity.CounterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounterID = null;
					else entity.CounterID = Convert.ToInt32(value);
				}
			}
			public System.String StartingValue
			{
				get
				{
					System.Decimal? data = entity.StartingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingValue = null;
					else entity.StartingValue = Convert.ToDecimal(value);
				}
			}
			public System.String EndingValue
			{
				get
				{
					System.Decimal? data = entity.EndingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndingValue = null;
					else entity.EndingValue = Convert.ToDecimal(value);
				}
			}
			public System.String RecipeAmount
			{
				get
				{
					System.Decimal? data = entity.RecipeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecipeAmount = null;
					else entity.RecipeAmount = Convert.ToDecimal(value);
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
			private esGuarantorRecipeMarginValue entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorRecipeMarginValueQuery query)
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
				throw new Exception("esGuarantorRecipeMarginValue can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class GuarantorRecipeMarginValue : esGuarantorRecipeMarginValue
	{	
	}

	[Serializable]
	abstract public class esGuarantorRecipeMarginValueQuery : esDynamicQuery
	{
		protected override string GetConnectionName()
		{
			return "sa";
		}
		
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorRecipeMarginValueMetadata.Meta();
			}
		}	
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorRecipeMarginValueMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem CounterID
		{
			get
			{
				return new esQueryItem(this, GuarantorRecipeMarginValueMetadata.ColumnNames.CounterID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem StartingValue
		{
			get
			{
				return new esQueryItem(this, GuarantorRecipeMarginValueMetadata.ColumnNames.StartingValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem EndingValue
		{
			get
			{
				return new esQueryItem(this, GuarantorRecipeMarginValueMetadata.ColumnNames.EndingValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem RecipeAmount
		{
			get
			{
				return new esQueryItem(this, GuarantorRecipeMarginValueMetadata.ColumnNames.RecipeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorRecipeMarginValueCollection")]
	public partial class GuarantorRecipeMarginValueCollection : esGuarantorRecipeMarginValueCollection, IEnumerable< GuarantorRecipeMarginValue>
	{
		public GuarantorRecipeMarginValueCollection()
		{

		}	
		
		public static implicit operator List< GuarantorRecipeMarginValue>(GuarantorRecipeMarginValueCollection coll)
		{
			List< GuarantorRecipeMarginValue> list = new List< GuarantorRecipeMarginValue>();
			
			foreach (GuarantorRecipeMarginValue emp in coll)
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
				return  GuarantorRecipeMarginValueMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorRecipeMarginValueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorRecipeMarginValue(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorRecipeMarginValue();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public GuarantorRecipeMarginValueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorRecipeMarginValueQuery();
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
		public bool Load(GuarantorRecipeMarginValueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public GuarantorRecipeMarginValue AddNew()
		{
			GuarantorRecipeMarginValue entity = base.AddNewEntity() as GuarantorRecipeMarginValue;
			
			return entity;		
		}
		public GuarantorRecipeMarginValue FindByPrimaryKey(String guarantorID, Int32 counterID)
		{
			return base.FindByPrimaryKey(guarantorID, counterID) as GuarantorRecipeMarginValue;
		}

		#region IEnumerable< GuarantorRecipeMarginValue> Members

		IEnumerator< GuarantorRecipeMarginValue> IEnumerable< GuarantorRecipeMarginValue>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorRecipeMarginValue;
			}
		}

		#endregion
		
		private GuarantorRecipeMarginValueQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorRecipeMarginValue' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("GuarantorRecipeMarginValue ({GuarantorID, CounterID})")]
	[Serializable]
	public partial class GuarantorRecipeMarginValue : esGuarantorRecipeMarginValue
	{
		public GuarantorRecipeMarginValue()
		{
		}	
	
		public GuarantorRecipeMarginValue(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorRecipeMarginValueMetadata.Meta();
			}
		}	
	
		override protected esGuarantorRecipeMarginValueQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorRecipeMarginValueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public GuarantorRecipeMarginValueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorRecipeMarginValueQuery();
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
		public bool Load(GuarantorRecipeMarginValueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private GuarantorRecipeMarginValueQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class GuarantorRecipeMarginValueQuery : esGuarantorRecipeMarginValueQuery
	{
		public GuarantorRecipeMarginValueQuery()
		{

		}		
		
		public GuarantorRecipeMarginValueQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "GuarantorRecipeMarginValueQuery";
        }
	}

	[Serializable]
	public partial class GuarantorRecipeMarginValueMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorRecipeMarginValueMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(GuarantorRecipeMarginValueMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorRecipeMarginValueMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(GuarantorRecipeMarginValueMetadata.ColumnNames.CounterID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorRecipeMarginValueMetadata.PropertyNames.CounterID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(GuarantorRecipeMarginValueMetadata.ColumnNames.StartingValue, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorRecipeMarginValueMetadata.PropertyNames.StartingValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(GuarantorRecipeMarginValueMetadata.ColumnNames.EndingValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorRecipeMarginValueMetadata.PropertyNames.EndingValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(GuarantorRecipeMarginValueMetadata.ColumnNames.RecipeAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorRecipeMarginValueMetadata.PropertyNames.RecipeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorRecipeMarginValueMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(GuarantorRecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorRecipeMarginValueMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public GuarantorRecipeMarginValueMetadata Meta()
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
			public const string GuarantorID = "GuarantorID";
			public const string CounterID = "CounterID";
			public const string StartingValue = "StartingValue";
			public const string EndingValue = "EndingValue";
			public const string RecipeAmount = "RecipeAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string GuarantorID = "GuarantorID";
			public const string CounterID = "CounterID";
			public const string StartingValue = "StartingValue";
			public const string EndingValue = "EndingValue";
			public const string RecipeAmount = "RecipeAmount";
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
			lock (typeof(GuarantorRecipeMarginValueMetadata))
			{
				if(GuarantorRecipeMarginValueMetadata.mapDelegates == null)
				{
					GuarantorRecipeMarginValueMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorRecipeMarginValueMetadata.meta == null)
				{
					GuarantorRecipeMarginValueMetadata.meta = new GuarantorRecipeMarginValueMetadata();
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
				
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StartingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EndingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RecipeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "GuarantorRecipeMarginValue";
				meta.Destination = "GuarantorRecipeMarginValue";
				meta.spInsert = "proc_GuarantorRecipeMarginValueInsert";				
				meta.spUpdate = "proc_GuarantorRecipeMarginValueUpdate";		
				meta.spDelete = "proc_GuarantorRecipeMarginValueDelete";
				meta.spLoadAll = "proc_GuarantorRecipeMarginValueLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorRecipeMarginValueLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorRecipeMarginValueMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
