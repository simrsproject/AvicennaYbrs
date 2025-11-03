/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/9/2020 12:29:27 PM
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
	abstract public class esRegistrationCounselingCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationCounselingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationCounselingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationCounselingQuery query)
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
			this.InitQuery(query as esRegistrationCounselingQuery);
		}
		#endregion
			
		virtual public RegistrationCounseling DetachEntity(RegistrationCounseling entity)
		{
			return base.DetachEntity(entity) as RegistrationCounseling;
		}
		
		virtual public RegistrationCounseling AttachEntity(RegistrationCounseling entity)
		{
			return base.AttachEntity(entity) as RegistrationCounseling;
		}
		
		virtual public void Combine(RegistrationCounselingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationCounseling this[int index]
		{
			get
			{
				return base[index] as RegistrationCounseling;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationCounseling);
		}
	}

	[Serializable]
	abstract public class esRegistrationCounseling : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationCounselingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationCounseling()
		{
		}
	
		public esRegistrationCounseling(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 counselingNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, counselingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, counselingNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 counselingNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, counselingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, counselingNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 counselingNo)
		{
			esRegistrationCounselingQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.CounselingNo == counselingNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 counselingNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("CounselingNo",counselingNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "CounselingNo": this.str.CounselingNo = (string)value; break;
						case "CounselingDateTime": this.str.CounselingDateTime = (string)value; break;
						case "CounselingNotes": this.str.CounselingNotes = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CounselingNo":
						
							if (value == null || value is System.Int32)
								this.CounselingNo = (System.Int32?)value;
							break;
						case "CounselingDateTime":
						
							if (value == null || value is System.DateTime)
								this.CounselingDateTime = (System.DateTime?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationCounseling.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.CounselingNo
		/// </summary>
		virtual public System.Int32? CounselingNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationCounselingMetadata.ColumnNames.CounselingNo);
			}
			
			set
			{
				base.SetSystemInt32(RegistrationCounselingMetadata.ColumnNames.CounselingNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.CounselingDateTime
		/// </summary>
		virtual public System.DateTime? CounselingDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationCounselingMetadata.ColumnNames.CounselingDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationCounselingMetadata.ColumnNames.CounselingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.CounselingNotes
		/// </summary>
		virtual public System.String CounselingNotes
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingMetadata.ColumnNames.CounselingNotes);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingMetadata.ColumnNames.CounselingNotes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(RegistrationCounselingMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationCounselingMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationCounselingMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationCounselingMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationCounselingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationCounselingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCounseling.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(RegistrationCounselingMetadata.ColumnNames.BedID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCounselingMetadata.ColumnNames.BedID, value);
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
			public esStrings(esRegistrationCounseling entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String CounselingNo
			{
				get
				{
					System.Int32? data = entity.CounselingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounselingNo = null;
					else entity.CounselingNo = Convert.ToInt32(value);
				}
			}
			public System.String CounselingDateTime
			{
				get
				{
					System.DateTime? data = entity.CounselingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounselingDateTime = null;
					else entity.CounselingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CounselingNotes
			{
				get
				{
					System.String data = entity.CounselingNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounselingNotes = null;
					else entity.CounselingNotes = Convert.ToString(value);
				}
			}
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
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
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
				}
			}
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			private esRegistrationCounseling entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationCounselingQuery query)
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
				throw new Exception("esRegistrationCounseling can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationCounseling : esRegistrationCounseling
	{	
	}

	[Serializable]
	abstract public class esRegistrationCounselingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCounselingMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem CounselingNo
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.CounselingNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CounselingDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.CounselingDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CounselingNotes
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.CounselingNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
			
		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, RegistrationCounselingMetadata.ColumnNames.BedID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationCounselingCollection")]
	public partial class RegistrationCounselingCollection : esRegistrationCounselingCollection, IEnumerable< RegistrationCounseling>
	{
		public RegistrationCounselingCollection()
		{

		}	
		
		public static implicit operator List< RegistrationCounseling>(RegistrationCounselingCollection coll)
		{
			List< RegistrationCounseling> list = new List< RegistrationCounseling>();
			
			foreach (RegistrationCounseling emp in coll)
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
				return  RegistrationCounselingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCounselingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationCounseling(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationCounseling();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationCounselingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCounselingQuery();
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
		public bool Load(RegistrationCounselingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationCounseling AddNew()
		{
			RegistrationCounseling entity = base.AddNewEntity() as RegistrationCounseling;
			
			return entity;		
		}
		public RegistrationCounseling FindByPrimaryKey(String registrationNo, Int32 counselingNo)
		{
			return base.FindByPrimaryKey(registrationNo, counselingNo) as RegistrationCounseling;
		}

		#region IEnumerable< RegistrationCounseling> Members

		IEnumerator< RegistrationCounseling> IEnumerable< RegistrationCounseling>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationCounseling;
			}
		}

		#endregion
		
		private RegistrationCounselingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationCounseling' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationCounseling ({RegistrationNo, CounselingNo})")]
	[Serializable]
	public partial class RegistrationCounseling : esRegistrationCounseling
	{
		public RegistrationCounseling()
		{
		}	
	
		public RegistrationCounseling(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCounselingMetadata.Meta();
			}
		}	
	
		override protected esRegistrationCounselingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCounselingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationCounselingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCounselingQuery();
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
		public bool Load(RegistrationCounselingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationCounselingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationCounselingQuery : esRegistrationCounselingQuery
	{
		public RegistrationCounselingQuery()
		{

		}		
		
		public RegistrationCounselingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationCounselingQuery";
        }
	}

	[Serializable]
	public partial class RegistrationCounselingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationCounselingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.CounselingNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.CounselingNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.CounselingDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.CounselingDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.CounselingNotes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.CounselingNotes;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.IsDeleted, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.CreatedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.ServiceUnitID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.RoomID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationCounselingMetadata.ColumnNames.BedID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCounselingMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RegistrationCounselingMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string CounselingNo = "CounselingNo";
			public const string CounselingDateTime = "CounselingDateTime";
			public const string CounselingNotes = "CounselingNotes";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string CounselingNo = "CounselingNo";
			public const string CounselingDateTime = "CounselingDateTime";
			public const string CounselingNotes = "CounselingNotes";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
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
			lock (typeof(RegistrationCounselingMetadata))
			{
				if(RegistrationCounselingMetadata.mapDelegates == null)
				{
					RegistrationCounselingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationCounselingMetadata.meta == null)
				{
					RegistrationCounselingMetadata.meta = new RegistrationCounselingMetadata();
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
				
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CounselingNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CounselingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CounselingNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "RegistrationCounseling";
				meta.Destination = "RegistrationCounseling";
				meta.spInsert = "proc_RegistrationCounselingInsert";				
				meta.spUpdate = "proc_RegistrationCounselingUpdate";		
				meta.spDelete = "proc_RegistrationCounselingDelete";
				meta.spLoadAll = "proc_RegistrationCounselingLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationCounselingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationCounselingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
