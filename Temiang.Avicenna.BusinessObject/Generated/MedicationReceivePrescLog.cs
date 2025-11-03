/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/6/2023 1:10:12 PM
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
	abstract public class esMedicationReceivePrescLogCollection : esEntityCollectionWAuditLog
	{
		public esMedicationReceivePrescLogCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "MedicationReceivePrescLogCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esMedicationReceivePrescLogQuery query)
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
			this.InitQuery(query as esMedicationReceivePrescLogQuery);
		}
		#endregion
			
		virtual public MedicationReceivePrescLog DetachEntity(MedicationReceivePrescLog entity)
		{
			return base.DetachEntity(entity) as MedicationReceivePrescLog;
		}
		
		virtual public MedicationReceivePrescLog AttachEntity(MedicationReceivePrescLog entity)
		{
			return base.AttachEntity(entity) as MedicationReceivePrescLog;
		}
		
		virtual public void Combine(MedicationReceivePrescLogCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicationReceivePrescLog this[int index]
		{
			get
			{
				return base[index] as MedicationReceivePrescLog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicationReceivePrescLog);
		}
	}

	[Serializable]
	abstract public class esMedicationReceivePrescLog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicationReceivePrescLogQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esMedicationReceivePrescLog()
		{
		}
	
		public esMedicationReceivePrescLog(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 medRecPrescLogID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medRecPrescLogID);
			else
				return LoadByPrimaryKeyStoredProcedure(medRecPrescLogID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medRecPrescLogID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medRecPrescLogID);
			else
				return LoadByPrimaryKeyStoredProcedure(medRecPrescLogID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 medRecPrescLogID)
		{
			esMedicationReceivePrescLogQuery query = this.GetDynamicQuery();
			query.Where(query.MedRecPrescLogID == medRecPrescLogID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 medRecPrescLogID)
		{
			esParameters parms = new esParameters();
			parms.Add("MedRecPrescLogID",medRecPrescLogID);
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
						case "MedRecPrescLogID": this.str.MedRecPrescLogID = (string)value; break;
						case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "IsPrescriptionVoid": this.str.IsPrescriptionVoid = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MedRecPrescLogID":
						
							if (value == null || value is System.Int64)
								this.MedRecPrescLogID = (System.Int64?)value;
							break;
						case "MedicationReceiveNo":
						
							if (value == null || value is System.Int64)
								this.MedicationReceiveNo = (System.Int64?)value;
							break;
						case "IsPrescriptionVoid":
						
							if (value == null || value is System.Boolean)
								this.IsPrescriptionVoid = (System.Boolean?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to MedicationReceivePrescLog.MedRecPrescLogID
		/// </summary>
		virtual public System.Int64? MedRecPrescLogID
		{
			get
			{
				return base.GetSystemInt64(MedicationReceivePrescLogMetadata.ColumnNames.MedRecPrescLogID);
			}
			
			set
			{
				base.SetSystemInt64(MedicationReceivePrescLogMetadata.ColumnNames.MedRecPrescLogID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceivePrescLog.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(MedicationReceivePrescLogMetadata.ColumnNames.MedicationReceiveNo);
			}
			
			set
			{
				base.SetSystemInt64(MedicationReceivePrescLogMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceivePrescLog.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(MedicationReceivePrescLogMetadata.ColumnNames.PrescriptionNo);
			}
			
			set
			{
				base.SetSystemString(MedicationReceivePrescLogMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceivePrescLog.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(MedicationReceivePrescLogMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(MedicationReceivePrescLogMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceivePrescLog.IsPrescriptionVoid
		/// </summary>
		virtual public System.Boolean? IsPrescriptionVoid
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceivePrescLogMetadata.ColumnNames.IsPrescriptionVoid);
			}
			
			set
			{
				base.SetSystemBoolean(MedicationReceivePrescLogMetadata.ColumnNames.IsPrescriptionVoid, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceivePrescLog.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceivePrescLogMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicationReceivePrescLogMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceivePrescLog.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceivePrescLogMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceivePrescLogMetadata.ColumnNames.CreatedByUserID, value);
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
			public esStrings(esMedicationReceivePrescLog entity)
			{
				this.entity = entity;
			}
			public System.String MedRecPrescLogID
			{
				get
				{
					System.Int64? data = entity.MedRecPrescLogID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedRecPrescLogID = null;
					else entity.MedRecPrescLogID = Convert.ToInt64(value);
				}
			}
			public System.String MedicationReceiveNo
			{
				get
				{
					System.Int64? data = entity.MedicationReceiveNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicationReceiveNo = null;
					else entity.MedicationReceiveNo = Convert.ToInt64(value);
				}
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
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String IsPrescriptionVoid
			{
				get
				{
					System.Boolean? data = entity.IsPrescriptionVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrescriptionVoid = null;
					else entity.IsPrescriptionVoid = Convert.ToBoolean(value);
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
			private esMedicationReceivePrescLog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicationReceivePrescLogQuery query)
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
				throw new Exception("esMedicationReceivePrescLog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicationReceivePrescLog : esMedicationReceivePrescLog
	{	
	}

	[Serializable]
	abstract public class esMedicationReceivePrescLogQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceivePrescLogMetadata.Meta();
			}
		}	
			
		public esQueryItem MedRecPrescLogID
		{
			get
			{
				return new esQueryItem(this, MedicationReceivePrescLogMetadata.ColumnNames.MedRecPrescLogID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, MedicationReceivePrescLogMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		} 
			
		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, MedicationReceivePrescLogMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, MedicationReceivePrescLogMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPrescriptionVoid
		{
			get
			{
				return new esQueryItem(this, MedicationReceivePrescLogMetadata.ColumnNames.IsPrescriptionVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceivePrescLogMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceivePrescLogMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicationReceivePrescLogCollection")]
	public partial class MedicationReceivePrescLogCollection : esMedicationReceivePrescLogCollection, IEnumerable< MedicationReceivePrescLog>
	{
		public MedicationReceivePrescLogCollection()
		{

		}	
		
		public static implicit operator List< MedicationReceivePrescLog>(MedicationReceivePrescLogCollection coll)
		{
			List< MedicationReceivePrescLog> list = new List< MedicationReceivePrescLog>();
			
			foreach (MedicationReceivePrescLog emp in coll)
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
				return  MedicationReceivePrescLogMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceivePrescLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicationReceivePrescLog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicationReceivePrescLog();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public MedicationReceivePrescLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceivePrescLogQuery();
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
		public bool Load(MedicationReceivePrescLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicationReceivePrescLog AddNew()
		{
			MedicationReceivePrescLog entity = base.AddNewEntity() as MedicationReceivePrescLog;
			
			return entity;		
		}
		public MedicationReceivePrescLog FindByPrimaryKey(Int64 medRecPrescLogID)
		{
			return base.FindByPrimaryKey(medRecPrescLogID) as MedicationReceivePrescLog;
		}

		#region IEnumerable< MedicationReceivePrescLog> Members

		IEnumerator< MedicationReceivePrescLog> IEnumerable< MedicationReceivePrescLog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicationReceivePrescLog;
			}
		}

		#endregion
		
		private MedicationReceivePrescLogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicationReceivePrescLog' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicationReceivePrescLog ({MedRecPrescLogID})")]
	[Serializable]
	public partial class MedicationReceivePrescLog : esMedicationReceivePrescLog
	{
		public MedicationReceivePrescLog()
		{
		}	
	
		public MedicationReceivePrescLog(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceivePrescLogMetadata.Meta();
			}
		}	
	
		override protected esMedicationReceivePrescLogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceivePrescLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public MedicationReceivePrescLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceivePrescLogQuery();
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
		public bool Load(MedicationReceivePrescLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private MedicationReceivePrescLogQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicationReceivePrescLogQuery : esMedicationReceivePrescLogQuery
	{
		public MedicationReceivePrescLogQuery()
		{

		}		
		
		public MedicationReceivePrescLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "MedicationReceivePrescLogQuery";
        }
	}

	[Serializable]
	public partial class MedicationReceivePrescLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicationReceivePrescLogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(MedicationReceivePrescLogMetadata.ColumnNames.MedRecPrescLogID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationReceivePrescLogMetadata.PropertyNames.MedRecPrescLogID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceivePrescLogMetadata.ColumnNames.MedicationReceiveNo, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationReceivePrescLogMetadata.PropertyNames.MedicationReceiveNo;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceivePrescLogMetadata.ColumnNames.PrescriptionNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceivePrescLogMetadata.PropertyNames.PrescriptionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceivePrescLogMetadata.ColumnNames.SequenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceivePrescLogMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceivePrescLogMetadata.ColumnNames.IsPrescriptionVoid, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceivePrescLogMetadata.PropertyNames.IsPrescriptionVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceivePrescLogMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceivePrescLogMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceivePrescLogMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceivePrescLogMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public MedicationReceivePrescLogMetadata Meta()
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
			public const string MedRecPrescLogID = "MedRecPrescLogID";
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string SequenceNo = "SequenceNo";
			public const string IsPrescriptionVoid = "IsPrescriptionVoid";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string MedRecPrescLogID = "MedRecPrescLogID";
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string SequenceNo = "SequenceNo";
			public const string IsPrescriptionVoid = "IsPrescriptionVoid";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(MedicationReceivePrescLogMetadata))
			{
				if(MedicationReceivePrescLogMetadata.mapDelegates == null)
				{
					MedicationReceivePrescLogMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicationReceivePrescLogMetadata.meta == null)
				{
					MedicationReceivePrescLogMetadata.meta = new MedicationReceivePrescLogMetadata();
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
				
				meta.AddTypeMap("MedRecPrescLogID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPrescriptionVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "MedicationReceivePrescLog";
				meta.Destination = "MedicationReceivePrescLog";
				meta.spInsert = "proc_MedicationReceivePrescLogInsert";				
				meta.spUpdate = "proc_MedicationReceivePrescLogUpdate";		
				meta.spDelete = "proc_MedicationReceivePrescLogDelete";
				meta.spLoadAll = "proc_MedicationReceivePrescLogLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicationReceivePrescLogLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicationReceivePrescLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
