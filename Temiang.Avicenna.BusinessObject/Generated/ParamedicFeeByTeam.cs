/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/1/2023 11:49:09 AM
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
	abstract public class esParamedicFeeByTeamCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeByTeamCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeByTeamCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeByTeamQuery query)
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
			this.InitQuery(query as esParamedicFeeByTeamQuery);
		}
		#endregion
			
		virtual public ParamedicFeeByTeam DetachEntity(ParamedicFeeByTeam entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeByTeam;
		}
		
		virtual public ParamedicFeeByTeam AttachEntity(ParamedicFeeByTeam entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeByTeam;
		}
		
		virtual public void Combine(ParamedicFeeByTeamCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeByTeam this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeByTeam;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeByTeam);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeByTeam : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeByTeamQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeByTeam()
		{
		}
	
		public esParamedicFeeByTeam(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paramedicID, String paramedicMemberID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, paramedicMemberID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, paramedicMemberID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paramedicID, String paramedicMemberID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, paramedicMemberID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, paramedicMemberID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String paramedicID, String paramedicMemberID)
		{
			esParamedicFeeByTeamQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID==paramedicID, query.ParamedicMemberID==paramedicMemberID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String paramedicID, String paramedicMemberID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID",paramedicID);
			parms.Add("ParamedicMemberID",paramedicMemberID);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ParamedicMemberID": this.str.ParamedicMemberID = (string)value; break;
						case "FeePercentage": this.str.FeePercentage = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "FeePercentage":
						
							if (value == null || value is System.Decimal)
								this.FeePercentage = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeByTeam.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByTeamMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByTeamMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByTeam.ParamedicMemberID
		/// </summary>
		virtual public System.String ParamedicMemberID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByTeamMetadata.ColumnNames.ParamedicMemberID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByTeamMetadata.ColumnNames.ParamedicMemberID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByTeam.FeePercentage
		/// </summary>
		virtual public System.Decimal? FeePercentage
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeByTeamMetadata.ColumnNames.FeePercentage);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeByTeamMetadata.ColumnNames.FeePercentage, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByTeam.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeByTeam.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeByTeam entity)
			{
				this.entity = entity;
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
			public System.String ParamedicMemberID
			{
				get
				{
					System.String data = entity.ParamedicMemberID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicMemberID = null;
					else entity.ParamedicMemberID = Convert.ToString(value);
				}
			}
			public System.String FeePercentage
			{
				get
				{
					System.Decimal? data = entity.FeePercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeePercentage = null;
					else entity.FeePercentage = Convert.ToDecimal(value);
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
			private esParamedicFeeByTeam entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeByTeamQuery query)
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
				throw new Exception("esParamedicFeeByTeam can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeByTeam : esParamedicFeeByTeam
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeByTeamQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByTeamMetadata.Meta();
			}
		}	
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByTeamMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicMemberID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByTeamMetadata.ColumnNames.ParamedicMemberID, esSystemType.String);
			}
		} 
			
		public esQueryItem FeePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByTeamMetadata.ColumnNames.FeePercentage, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeByTeamCollection")]
	public partial class ParamedicFeeByTeamCollection : esParamedicFeeByTeamCollection, IEnumerable< ParamedicFeeByTeam>
	{
		public ParamedicFeeByTeamCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeByTeam>(ParamedicFeeByTeamCollection coll)
		{
			List< ParamedicFeeByTeam> list = new List< ParamedicFeeByTeam>();
			
			foreach (ParamedicFeeByTeam emp in coll)
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
				return  ParamedicFeeByTeamMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeByTeam(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeByTeam();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeByTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByTeamQuery();
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
		public bool Load(ParamedicFeeByTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeByTeam AddNew()
		{
			ParamedicFeeByTeam entity = base.AddNewEntity() as ParamedicFeeByTeam;
			
			return entity;		
		}
		public ParamedicFeeByTeam FindByPrimaryKey(String paramedicID, String paramedicMemberID)
		{
			return base.FindByPrimaryKey(paramedicID, paramedicMemberID) as ParamedicFeeByTeam;
		}

		#region IEnumerable< ParamedicFeeByTeam> Members

		IEnumerator< ParamedicFeeByTeam> IEnumerable< ParamedicFeeByTeam>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeByTeam;
			}
		}

		#endregion
		
		private ParamedicFeeByTeamQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeByTeam' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeByTeam ({ParamedicID, ParamedicMemberID})")]
	[Serializable]
	public partial class ParamedicFeeByTeam : esParamedicFeeByTeam
	{
		public ParamedicFeeByTeam()
		{
		}	
	
		public ParamedicFeeByTeam(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByTeamMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeByTeamQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeByTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByTeamQuery();
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
		public bool Load(ParamedicFeeByTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeByTeamQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeByTeamQuery : esParamedicFeeByTeamQuery
	{
		public ParamedicFeeByTeamQuery()
		{

		}		
		
		public ParamedicFeeByTeamQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeByTeamQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeByTeamMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeByTeamMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeByTeamMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByTeamMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByTeamMetadata.ColumnNames.ParamedicMemberID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByTeamMetadata.PropertyNames.ParamedicMemberID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByTeamMetadata.ColumnNames.FeePercentage, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeByTeamMetadata.PropertyNames.FeePercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByTeamMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeByTeamMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByTeamMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeByTeamMetadata Meta()
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
			public const string ParamedicID = "ParamedicID";
			public const string ParamedicMemberID = "ParamedicMemberID";
			public const string FeePercentage = "FeePercentage";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ParamedicID = "ParamedicID";
			public const string ParamedicMemberID = "ParamedicMemberID";
			public const string FeePercentage = "FeePercentage";
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
			lock (typeof(ParamedicFeeByTeamMetadata))
			{
				if(ParamedicFeeByTeamMetadata.mapDelegates == null)
				{
					ParamedicFeeByTeamMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeByTeamMetadata.meta == null)
				{
					ParamedicFeeByTeamMetadata.meta = new ParamedicFeeByTeamMetadata();
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
				
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicMemberID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FeePercentage", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ParamedicFeeByTeam";
				meta.Destination = "ParamedicFeeByTeam";
				meta.spInsert = "proc_ParamedicFeeByTeamInsert";				
				meta.spUpdate = "proc_ParamedicFeeByTeamUpdate";		
				meta.spDelete = "proc_ParamedicFeeByTeamDelete";
				meta.spLoadAll = "proc_ParamedicFeeByTeamLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeByTeamLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeByTeamMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
