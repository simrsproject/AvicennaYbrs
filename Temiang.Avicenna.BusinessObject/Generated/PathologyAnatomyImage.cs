/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 02/21/20 11:26:05 AM
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
	abstract public class esPathologyAnatomyImageCollection : esEntityCollectionWAuditLog
	{
		public esPathologyAnatomyImageCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PathologyAnatomyImageCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPathologyAnatomyImageQuery query)
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
			this.InitQuery(query as esPathologyAnatomyImageQuery);
		}
		#endregion
			
		virtual public PathologyAnatomyImage DetachEntity(PathologyAnatomyImage entity)
		{
			return base.DetachEntity(entity) as PathologyAnatomyImage;
		}
		
		virtual public PathologyAnatomyImage AttachEntity(PathologyAnatomyImage entity)
		{
			return base.AttachEntity(entity) as PathologyAnatomyImage;
		}
		
		virtual public void Combine(PathologyAnatomyImageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PathologyAnatomyImage this[int index]
		{
			get
			{
				return base[index] as PathologyAnatomyImage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PathologyAnatomyImage);
		}
	}

	[Serializable]
	abstract public class esPathologyAnatomyImage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPathologyAnatomyImageQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPathologyAnatomyImage()
		{
		}
	
		public esPathologyAnatomyImage(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String resultNo, Int32 imageNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(resultNo, imageNo);
			else
				return LoadByPrimaryKeyStoredProcedure(resultNo, imageNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String resultNo, Int32 imageNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(resultNo, imageNo);
			else
				return LoadByPrimaryKeyStoredProcedure(resultNo, imageNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String resultNo, Int32 imageNo)
		{
			esPathologyAnatomyImageQuery query = this.GetDynamicQuery();
			query.Where(query.ResultNo == resultNo, query.ImageNo == imageNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String resultNo, Int32 imageNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ResultNo",resultNo);
			parms.Add("ImageNo",imageNo);
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
						case "ResultNo": this.str.ResultNo = (string)value; break;
						case "ImageNo": this.str.ImageNo = (string)value; break;
						case "DocumentName": this.str.DocumentName = (string)value; break;
						case "DocumentNotes": this.str.DocumentNotes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ImageNo":
						
							if (value == null || value is System.Int32)
								this.ImageNo = (System.Int32?)value;
							break;
						case "DocumentImage":
						
							if (value == null || value is System.Byte[])
								this.DocumentImage = (System.Byte[])value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to PathologyAnatomyImage.ResultNo
		/// </summary>
		virtual public System.String ResultNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyImageMetadata.ColumnNames.ResultNo);
			}
			
			set
			{
				base.SetSystemString(PathologyAnatomyImageMetadata.ColumnNames.ResultNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.ImageNo
		/// </summary>
		virtual public System.Int32? ImageNo
		{
			get
			{
				return base.GetSystemInt32(PathologyAnatomyImageMetadata.ColumnNames.ImageNo);
			}
			
			set
			{
				base.SetSystemInt32(PathologyAnatomyImageMetadata.ColumnNames.ImageNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyImageMetadata.ColumnNames.DocumentName);
			}
			
			set
			{
				base.SetSystemString(PathologyAnatomyImageMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.DocumentImage
		/// </summary>
		virtual public System.Byte[] DocumentImage
		{
			get
			{
				return base.GetSystemByteArray(PathologyAnatomyImageMetadata.ColumnNames.DocumentImage);
			}
			
			set
			{
				base.SetSystemByteArray(PathologyAnatomyImageMetadata.ColumnNames.DocumentImage, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.DocumentNotes
		/// </summary>
		virtual public System.String DocumentNotes
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyImageMetadata.ColumnNames.DocumentNotes);
			}
			
			set
			{
				base.SetSystemString(PathologyAnatomyImageMetadata.ColumnNames.DocumentNotes, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyImageMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PathologyAnatomyImageMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyImageMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PathologyAnatomyImageMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyImageMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PathologyAnatomyImageMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyImage.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyImageMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(PathologyAnatomyImageMetadata.ColumnNames.CreatedByUserID, value);
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
			public esStrings(esPathologyAnatomyImage entity)
			{
				this.entity = entity;
			}
			public System.String ResultNo
			{
				get
				{
					System.String data = entity.ResultNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultNo = null;
					else entity.ResultNo = Convert.ToString(value);
				}
			}
			public System.String ImageNo
			{
				get
				{
					System.Int32? data = entity.ImageNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImageNo = null;
					else entity.ImageNo = Convert.ToInt32(value);
				}
			}
			public System.String DocumentName
			{
				get
				{
					System.String data = entity.DocumentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentName = null;
					else entity.DocumentName = Convert.ToString(value);
				}
			}
			public System.String DocumentNotes
			{
				get
				{
					System.String data = entity.DocumentNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentNotes = null;
					else entity.DocumentNotes = Convert.ToString(value);
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
			private esPathologyAnatomyImage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPathologyAnatomyImageQuery query)
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
				throw new Exception("esPathologyAnatomyImage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PathologyAnatomyImage : esPathologyAnatomyImage
	{	
	}

	[Serializable]
	abstract public class esPathologyAnatomyImageQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PathologyAnatomyImageMetadata.Meta();
			}
		}	
			
		public esQueryItem ResultNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.ResultNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ImageNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.ImageNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		} 
			
		public esQueryItem DocumentImage
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.DocumentImage, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem DocumentNotes
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.DocumentNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyImageMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PathologyAnatomyImageCollection")]
	public partial class PathologyAnatomyImageCollection : esPathologyAnatomyImageCollection, IEnumerable< PathologyAnatomyImage>
	{
		public PathologyAnatomyImageCollection()
		{

		}	
		
		public static implicit operator List< PathologyAnatomyImage>(PathologyAnatomyImageCollection coll)
		{
			List< PathologyAnatomyImage> list = new List< PathologyAnatomyImage>();
			
			foreach (PathologyAnatomyImage emp in coll)
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
				return  PathologyAnatomyImageMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PathologyAnatomyImageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PathologyAnatomyImage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PathologyAnatomyImage();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PathologyAnatomyImageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PathologyAnatomyImageQuery();
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
		public bool Load(PathologyAnatomyImageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PathologyAnatomyImage AddNew()
		{
			PathologyAnatomyImage entity = base.AddNewEntity() as PathologyAnatomyImage;
			
			return entity;		
		}
		public PathologyAnatomyImage FindByPrimaryKey(String resultNo, Int32 imageNo)
		{
			return base.FindByPrimaryKey(resultNo, imageNo) as PathologyAnatomyImage;
		}

		#region IEnumerable< PathologyAnatomyImage> Members

		IEnumerator< PathologyAnatomyImage> IEnumerable< PathologyAnatomyImage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PathologyAnatomyImage;
			}
		}

		#endregion
		
		private PathologyAnatomyImageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PathologyAnatomyImage' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PathologyAnatomyImage ({ResultNo, ImageNo})")]
	[Serializable]
	public partial class PathologyAnatomyImage : esPathologyAnatomyImage
	{
		public PathologyAnatomyImage()
		{
		}	
	
		public PathologyAnatomyImage(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PathologyAnatomyImageMetadata.Meta();
			}
		}	
	
		override protected esPathologyAnatomyImageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PathologyAnatomyImageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PathologyAnatomyImageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PathologyAnatomyImageQuery();
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
		public bool Load(PathologyAnatomyImageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PathologyAnatomyImageQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PathologyAnatomyImageQuery : esPathologyAnatomyImageQuery
	{
		public PathologyAnatomyImageQuery()
		{

		}		
		
		public PathologyAnatomyImageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PathologyAnatomyImageQuery";
        }
	}

	[Serializable]
	public partial class PathologyAnatomyImageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PathologyAnatomyImageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.ResultNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.ResultNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.ImageNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.ImageNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.DocumentName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.DocumentImage, 3, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.DocumentImage;
			c.NumericPrecision = 0;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.DocumentNotes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.DocumentNotes;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.CreatedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PathologyAnatomyImageMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyImageMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PathologyAnatomyImageMetadata Meta()
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
			public const string ResultNo = "ResultNo";
			public const string ImageNo = "ImageNo";
			public const string DocumentName = "DocumentName";
			public const string DocumentImage = "DocumentImage";
			public const string DocumentNotes = "DocumentNotes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ResultNo = "ResultNo";
			public const string ImageNo = "ImageNo";
			public const string DocumentName = "DocumentName";
			public const string DocumentImage = "DocumentImage";
			public const string DocumentNotes = "DocumentNotes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(PathologyAnatomyImageMetadata))
			{
				if(PathologyAnatomyImageMetadata.mapDelegates == null)
				{
					PathologyAnatomyImageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PathologyAnatomyImageMetadata.meta == null)
				{
					PathologyAnatomyImageMetadata.meta = new PathologyAnatomyImageMetadata();
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
				
				meta.AddTypeMap("ResultNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImageNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DocumentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("DocumentNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PathologyAnatomyImage";
				meta.Destination = "PathologyAnatomyImage";
				meta.spInsert = "proc_PathologyAnatomyImageInsert";				
				meta.spUpdate = "proc_PathologyAnatomyImageUpdate";		
				meta.spDelete = "proc_PathologyAnatomyImageDelete";
				meta.spLoadAll = "proc_PathologyAnatomyImageLoadAll";
				meta.spLoadByPrimaryKey = "proc_PathologyAnatomyImageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PathologyAnatomyImageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
