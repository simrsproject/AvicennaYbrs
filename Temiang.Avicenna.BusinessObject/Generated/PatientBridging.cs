/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/30/2023 8:40:14 AM
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
	abstract public class esPatientBridgingCollection : esEntityCollectionWAuditLog
	{
		public esPatientBridgingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientBridgingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientBridgingQuery query)
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
			this.InitQuery(query as esPatientBridgingQuery);
		}
		#endregion
			
		virtual public PatientBridging DetachEntity(PatientBridging entity)
		{
			return base.DetachEntity(entity) as PatientBridging;
		}
		
		virtual public PatientBridging AttachEntity(PatientBridging entity)
		{
			return base.AttachEntity(entity) as PatientBridging;
		}
		
		virtual public void Combine(PatientBridgingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientBridging this[int index]
		{
			get
			{
				return base[index] as PatientBridging;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientBridging);
		}
	}

	[Serializable]
	abstract public class esPatientBridging : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientBridgingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientBridging()
		{
		}
	
		public esPatientBridging(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String patientID, String sRBridgingType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, sRBridgingType);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, sRBridgingType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, String sRBridgingType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, sRBridgingType);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, sRBridgingType);
		}
	
		private bool LoadByPrimaryKeyDynamic(String patientID, String sRBridgingType)
		{
			esPatientBridgingQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID, query.SRBridgingType == sRBridgingType);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String patientID, String sRBridgingType)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);
			parms.Add("SRBridgingType",sRBridgingType);
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
						case "PatientID": this.str.PatientID = (string)value; break;
						case "SRBridgingType": this.str.SRBridgingType = (string)value; break;
						case "BridgingID": this.str.BridgingID = (string)value; break;
						case "BridgingName": this.str.BridgingName = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to PatientBridging.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientBridgingMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(PatientBridgingMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientBridging.SRBridgingType
		/// </summary>
		virtual public System.String SRBridgingType
		{
			get
			{
				return base.GetSystemString(PatientBridgingMetadata.ColumnNames.SRBridgingType);
			}
			
			set
			{
				base.SetSystemString(PatientBridgingMetadata.ColumnNames.SRBridgingType, value);
			}
		}
		/// <summary>
		/// Maps to PatientBridging.BridgingID
		/// </summary>
		virtual public System.String BridgingID
		{
			get
			{
				return base.GetSystemString(PatientBridgingMetadata.ColumnNames.BridgingID);
			}
			
			set
			{
				base.SetSystemString(PatientBridgingMetadata.ColumnNames.BridgingID, value);
			}
		}
		/// <summary>
		/// Maps to PatientBridging.BridgingName
		/// </summary>
		virtual public System.String BridgingName
		{
			get
			{
				return base.GetSystemString(PatientBridgingMetadata.ColumnNames.BridgingName);
			}
			
			set
			{
				base.SetSystemString(PatientBridgingMetadata.ColumnNames.BridgingName, value);
			}
		}
		/// <summary>
		/// Maps to PatientBridging.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(PatientBridgingMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(PatientBridgingMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to PatientBridging.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientBridgingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientBridgingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientBridging.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientBridgingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientBridgingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPatientBridging entity)
			{
				this.entity = entity;
			}
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
			public System.String SRBridgingType
			{
				get
				{
					System.String data = entity.SRBridgingType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBridgingType = null;
					else entity.SRBridgingType = Convert.ToString(value);
				}
			}
			public System.String BridgingID
			{
				get
				{
					System.String data = entity.BridgingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingID = null;
					else entity.BridgingID = Convert.ToString(value);
				}
			}
			public System.String BridgingName
			{
				get
				{
					System.String data = entity.BridgingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingName = null;
					else entity.BridgingName = Convert.ToString(value);
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
			private esPatientBridging entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientBridgingQuery query)
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
				throw new Exception("esPatientBridging can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientBridging : esPatientBridging
	{	
	}

	[Serializable]
	abstract public class esPatientBridgingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientBridgingMetadata.Meta();
			}
		}	
			
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientBridgingMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBridgingType
		{
			get
			{
				return new esQueryItem(this, PatientBridgingMetadata.ColumnNames.SRBridgingType, esSystemType.String);
			}
		} 
			
		public esQueryItem BridgingID
		{
			get
			{
				return new esQueryItem(this, PatientBridgingMetadata.ColumnNames.BridgingID, esSystemType.String);
			}
		} 
			
		public esQueryItem BridgingName
		{
			get
			{
				return new esQueryItem(this, PatientBridgingMetadata.ColumnNames.BridgingName, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, PatientBridgingMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientBridgingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientBridgingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientBridgingCollection")]
	public partial class PatientBridgingCollection : esPatientBridgingCollection, IEnumerable< PatientBridging>
	{
		public PatientBridgingCollection()
		{

		}	
		
		public static implicit operator List< PatientBridging>(PatientBridgingCollection coll)
		{
			List< PatientBridging> list = new List< PatientBridging>();
			
			foreach (PatientBridging emp in coll)
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
				return  PatientBridgingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientBridging(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientBridging();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientBridgingQuery();
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
		public bool Load(PatientBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientBridging AddNew()
		{
			PatientBridging entity = base.AddNewEntity() as PatientBridging;
			
			return entity;		
		}
		public PatientBridging FindByPrimaryKey(String patientID, String sRBridgingType)
		{
			return base.FindByPrimaryKey(patientID, sRBridgingType) as PatientBridging;
		}

		#region IEnumerable< PatientBridging> Members

		IEnumerator< PatientBridging> IEnumerable< PatientBridging>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientBridging;
			}
		}

		#endregion
		
		private PatientBridgingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientBridging' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientBridging ({PatientID, SRBridgingType})")]
	[Serializable]
	public partial class PatientBridging : esPatientBridging
	{
		public PatientBridging()
		{
		}	
	
		public PatientBridging(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientBridgingMetadata.Meta();
			}
		}	
	
		override protected esPatientBridgingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientBridgingQuery();
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
		public bool Load(PatientBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientBridgingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientBridgingQuery : esPatientBridgingQuery
	{
		public PatientBridgingQuery()
		{

		}		
		
		public PatientBridgingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientBridgingQuery";
        }
	}

	[Serializable]
	public partial class PatientBridgingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientBridgingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientBridgingMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBridgingMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBridgingMetadata.ColumnNames.SRBridgingType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBridgingMetadata.PropertyNames.SRBridgingType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBridgingMetadata.ColumnNames.BridgingID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBridgingMetadata.PropertyNames.BridgingID;
			c.CharacterMaxLength = 36;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBridgingMetadata.ColumnNames.BridgingName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBridgingMetadata.PropertyNames.BridgingName;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBridgingMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientBridgingMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBridgingMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientBridgingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBridgingMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBridgingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientBridgingMetadata Meta()
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
			public const string PatientID = "PatientID";
			public const string SRBridgingType = "SRBridgingType";
			public const string BridgingID = "BridgingID";
			public const string BridgingName = "BridgingName";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PatientID = "PatientID";
			public const string SRBridgingType = "SRBridgingType";
			public const string BridgingID = "BridgingID";
			public const string BridgingName = "BridgingName";
			public const string IsActive = "IsActive";
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
			lock (typeof(PatientBridgingMetadata))
			{
				if(PatientBridgingMetadata.mapDelegates == null)
				{
					PatientBridgingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientBridgingMetadata.meta == null)
				{
					PatientBridgingMetadata.meta = new PatientBridgingMetadata();
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
				
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBridgingType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PatientBridging";
				meta.Destination = "PatientBridging";
				meta.spInsert = "proc_PatientBridgingInsert";				
				meta.spUpdate = "proc_PatientBridgingUpdate";		
				meta.spDelete = "proc_PatientBridgingDelete";
				meta.spLoadAll = "proc_PatientBridgingLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientBridgingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientBridgingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
