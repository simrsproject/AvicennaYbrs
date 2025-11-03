/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/04/19 9:06:49 PM
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
	abstract public class esServiceUnitBookingBodyImageCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitBookingBodyImageCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceUnitBookingBodyImageCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceUnitBookingBodyImageQuery query)
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
			this.InitQuery(query as esServiceUnitBookingBodyImageQuery);
		}
		#endregion
			
		virtual public ServiceUnitBookingBodyImage DetachEntity(ServiceUnitBookingBodyImage entity)
		{
			return base.DetachEntity(entity) as ServiceUnitBookingBodyImage;
		}
		
		virtual public ServiceUnitBookingBodyImage AttachEntity(ServiceUnitBookingBodyImage entity)
		{
			return base.AttachEntity(entity) as ServiceUnitBookingBodyImage;
		}
		
		virtual public void Combine(ServiceUnitBookingBodyImageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitBookingBodyImage this[int index]
		{
			get
			{
				return base[index] as ServiceUnitBookingBodyImage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitBookingBodyImage);
		}
	}

	[Serializable]
	abstract public class esServiceUnitBookingBodyImage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitBookingBodyImageQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceUnitBookingBodyImage()
		{
		}
	
		public esServiceUnitBookingBodyImage(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String bookingNo, String opNotesSeqNo, String bodyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bookingNo, opNotesSeqNo, bodyID);
			else
				return LoadByPrimaryKeyStoredProcedure(bookingNo, opNotesSeqNo, bodyID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bookingNo, String opNotesSeqNo, String bodyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bookingNo, opNotesSeqNo, bodyID);
			else
				return LoadByPrimaryKeyStoredProcedure(bookingNo, opNotesSeqNo, bodyID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String bookingNo, String opNotesSeqNo, String bodyID)
		{
			esServiceUnitBookingBodyImageQuery query = this.GetDynamicQuery();
			query.Where(query.BookingNo == bookingNo, query.OpNotesSeqNo == opNotesSeqNo, query.BodyID == bodyID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String bookingNo, String opNotesSeqNo, String bodyID)
		{
			esParameters parms = new esParameters();
			parms.Add("BookingNo",bookingNo);
			parms.Add("OpNotesSeqNo",opNotesSeqNo);
			parms.Add("BodyID",bodyID);
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
						case "BookingNo": this.str.BookingNo = (string)value; break;
						case "OpNotesSeqNo": this.str.OpNotesSeqNo = (string)value; break;
						case "BodyID": this.str.BodyID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BodyImage":
						
							if (value == null || value is System.Byte[])
								this.BodyImage = (System.Byte[])value;
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
		/// Maps to ServiceUnitBookingBodyImage.BookingNo
		/// </summary>
		virtual public System.String BookingNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.BookingNo);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.BookingNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingBodyImage.OpNotesSeqNo
		/// </summary>
		virtual public System.String OpNotesSeqNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.OpNotesSeqNo);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.OpNotesSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingBodyImage.BodyID
		/// </summary>
		virtual public System.String BodyID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingBodyImage.BodyImage
		/// </summary>
		virtual public System.Byte[] BodyImage
		{
			get
			{
				return base.GetSystemByteArray(ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyImage);
			}
			
			set
			{
				base.SetSystemByteArray(ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyImage, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingBodyImage.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingBodyImage.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingBodyImage.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingBodyImage.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esServiceUnitBookingBodyImage entity)
			{
				this.entity = entity;
			}
			public System.String BookingNo
			{
				get
				{
					System.String data = entity.BookingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingNo = null;
					else entity.BookingNo = Convert.ToString(value);
				}
			}
			public System.String OpNotesSeqNo
			{
				get
				{
					System.String data = entity.OpNotesSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpNotesSeqNo = null;
					else entity.OpNotesSeqNo = Convert.ToString(value);
				}
			}
			public System.String BodyID
			{
				get
				{
					System.String data = entity.BodyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyID = null;
					else entity.BodyID = Convert.ToString(value);
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
			private esServiceUnitBookingBodyImage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitBookingBodyImageQuery query)
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
				throw new Exception("esServiceUnitBookingBodyImage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnitBookingBodyImage : esServiceUnitBookingBodyImage
	{	
	}

	[Serializable]
	abstract public class esServiceUnitBookingBodyImageQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitBookingBodyImageMetadata.Meta();
			}
		}	
			
		public esQueryItem BookingNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.BookingNo, esSystemType.String);
			}
		} 
			
		public esQueryItem OpNotesSeqNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.OpNotesSeqNo, esSystemType.String);
			}
		} 
			
		public esQueryItem BodyID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyID, esSystemType.String);
			}
		} 
			
		public esQueryItem BodyImage
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyImage, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitBookingBodyImageCollection")]
	public partial class ServiceUnitBookingBodyImageCollection : esServiceUnitBookingBodyImageCollection, IEnumerable< ServiceUnitBookingBodyImage>
	{
		public ServiceUnitBookingBodyImageCollection()
		{

		}	
		
		public static implicit operator List< ServiceUnitBookingBodyImage>(ServiceUnitBookingBodyImageCollection coll)
		{
			List< ServiceUnitBookingBodyImage> list = new List< ServiceUnitBookingBodyImage>();
			
			foreach (ServiceUnitBookingBodyImage emp in coll)
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
				return  ServiceUnitBookingBodyImageMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitBookingBodyImageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitBookingBodyImage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitBookingBodyImage();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceUnitBookingBodyImageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitBookingBodyImageQuery();
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
		public bool Load(ServiceUnitBookingBodyImageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnitBookingBodyImage AddNew()
		{
			ServiceUnitBookingBodyImage entity = base.AddNewEntity() as ServiceUnitBookingBodyImage;
			
			return entity;		
		}
		public ServiceUnitBookingBodyImage FindByPrimaryKey(String bookingNo, String opNotesSeqNo, String bodyID)
		{
			return base.FindByPrimaryKey(bookingNo, opNotesSeqNo, bodyID) as ServiceUnitBookingBodyImage;
		}

		#region IEnumerable< ServiceUnitBookingBodyImage> Members

		IEnumerator< ServiceUnitBookingBodyImage> IEnumerable< ServiceUnitBookingBodyImage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitBookingBodyImage;
			}
		}

		#endregion
		
		private ServiceUnitBookingBodyImageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitBookingBodyImage' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnitBookingBodyImage ({BookingNo, OpNotesSeqNo, BodyID})")]
	[Serializable]
	public partial class ServiceUnitBookingBodyImage : esServiceUnitBookingBodyImage
	{
		public ServiceUnitBookingBodyImage()
		{
		}	
	
		public ServiceUnitBookingBodyImage(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitBookingBodyImageMetadata.Meta();
			}
		}	
	
		override protected esServiceUnitBookingBodyImageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitBookingBodyImageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceUnitBookingBodyImageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitBookingBodyImageQuery();
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
		public bool Load(ServiceUnitBookingBodyImageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceUnitBookingBodyImageQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitBookingBodyImageQuery : esServiceUnitBookingBodyImageQuery
	{
		public ServiceUnitBookingBodyImageQuery()
		{

		}		
		
		public ServiceUnitBookingBodyImageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceUnitBookingBodyImageQuery";
        }
	}

	[Serializable]
	public partial class ServiceUnitBookingBodyImageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitBookingBodyImageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.BookingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.BookingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.OpNotesSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.OpNotesSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.BodyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.BodyImage, 3, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.BodyImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitBookingBodyImageMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingBodyImageMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceUnitBookingBodyImageMetadata Meta()
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
			public const string BookingNo = "BookingNo";
			public const string OpNotesSeqNo = "OpNotesSeqNo";
			public const string BodyID = "BodyID";
			public const string BodyImage = "BodyImage";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string BookingNo = "BookingNo";
			public const string OpNotesSeqNo = "OpNotesSeqNo";
			public const string BodyID = "BodyID";
			public const string BodyImage = "BodyImage";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(ServiceUnitBookingBodyImageMetadata))
			{
				if(ServiceUnitBookingBodyImageMetadata.mapDelegates == null)
				{
					ServiceUnitBookingBodyImageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitBookingBodyImageMetadata.meta == null)
				{
					ServiceUnitBookingBodyImageMetadata.meta = new ServiceUnitBookingBodyImageMetadata();
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
				
				meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OpNotesSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ServiceUnitBookingBodyImage";
				meta.Destination = "ServiceUnitBookingBodyImage";
				meta.spInsert = "proc_ServiceUnitBookingBodyImageInsert";				
				meta.spUpdate = "proc_ServiceUnitBookingBodyImageUpdate";		
				meta.spDelete = "proc_ServiceUnitBookingBodyImageDelete";
				meta.spLoadAll = "proc_ServiceUnitBookingBodyImageLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitBookingBodyImageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitBookingBodyImageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
