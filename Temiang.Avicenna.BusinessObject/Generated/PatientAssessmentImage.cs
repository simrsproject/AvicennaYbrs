/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/18/2021 9:20:37 PM
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
	abstract public class esPatientAssessmentImageCollection : esEntityCollectionWAuditLog
	{
		public esPatientAssessmentImageCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientAssessmentImageCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientAssessmentImageQuery query)
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
			this.InitQuery(query as esPatientAssessmentImageQuery);
		}
		#endregion
			
		virtual public PatientAssessmentImage DetachEntity(PatientAssessmentImage entity)
		{
			return base.DetachEntity(entity) as PatientAssessmentImage;
		}
		
		virtual public PatientAssessmentImage AttachEntity(PatientAssessmentImage entity)
		{
			return base.AttachEntity(entity) as PatientAssessmentImage;
		}
		
		virtual public void Combine(PatientAssessmentImageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientAssessmentImage this[int index]
		{
			get
			{
				return base[index] as PatientAssessmentImage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientAssessmentImage);
		}
	}

	[Serializable]
	abstract public class esPatientAssessmentImage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientAssessmentImageQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientAssessmentImage()
		{
		}
	
		public esPatientAssessmentImage(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationInfoMedicID, Int32 imageNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, imageNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, imageNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationInfoMedicID, Int32 imageNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, imageNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, imageNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationInfoMedicID, Int32 imageNo)
		{
			esPatientAssessmentImageQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationInfoMedicID == registrationInfoMedicID, query.ImageNo == imageNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationInfoMedicID, Int32 imageNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationInfoMedicID",registrationInfoMedicID);
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
						case "RegistrationInfoMedicID": this.str.RegistrationInfoMedicID = (string)value; break;
						case "ImageNo": this.str.ImageNo = (string)value; break;
						case "DocumentName": this.str.DocumentName = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
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
		/// Maps to PatientAssessmentImage.RegistrationInfoMedicID
		/// </summary>
		virtual public System.String RegistrationInfoMedicID
		{
			get
			{
				return base.GetSystemString(PatientAssessmentImageMetadata.ColumnNames.RegistrationInfoMedicID);
			}
			
			set
			{
				base.SetSystemString(PatientAssessmentImageMetadata.ColumnNames.RegistrationInfoMedicID, value);
			}
		}
		/// <summary>
		/// Maps to PatientAssessmentImage.ImageNo
		/// </summary>
		virtual public System.Int32? ImageNo
		{
			get
			{
				return base.GetSystemInt32(PatientAssessmentImageMetadata.ColumnNames.ImageNo);
			}
			
			set
			{
				base.SetSystemInt32(PatientAssessmentImageMetadata.ColumnNames.ImageNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientAssessmentImage.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(PatientAssessmentImageMetadata.ColumnNames.DocumentName);
			}
			
			set
			{
				base.SetSystemString(PatientAssessmentImageMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to PatientAssessmentImage.DocumentImage
		/// </summary>
		virtual public System.Byte[] DocumentImage
		{
			get
			{
				return base.GetSystemByteArray(PatientAssessmentImageMetadata.ColumnNames.DocumentImage);
			}
			
			set
			{
				base.SetSystemByteArray(PatientAssessmentImageMetadata.ColumnNames.DocumentImage, value);
			}
		}
		/// <summary>
		/// Maps to PatientAssessmentImage.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientAssessmentImageMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientAssessmentImageMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientAssessmentImage.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientAssessmentImageMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientAssessmentImageMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esPatientAssessmentImage entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationInfoMedicID
			{
				get
				{
					System.String data = entity.RegistrationInfoMedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationInfoMedicID = null;
					else entity.RegistrationInfoMedicID = Convert.ToString(value);
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
			private esPatientAssessmentImage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientAssessmentImageQuery query)
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
				throw new Exception("esPatientAssessmentImage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientAssessmentImage : esPatientAssessmentImage
	{	
	}

	[Serializable]
	abstract public class esPatientAssessmentImageQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientAssessmentImageMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationInfoMedicID
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentImageMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ImageNo
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentImageMetadata.ColumnNames.ImageNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentImageMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		} 
			
		public esQueryItem DocumentImage
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentImageMetadata.ColumnNames.DocumentImage, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentImageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientAssessmentImageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientAssessmentImageCollection")]
	public partial class PatientAssessmentImageCollection : esPatientAssessmentImageCollection, IEnumerable< PatientAssessmentImage>
	{
		public PatientAssessmentImageCollection()
		{

		}	
		
		public static implicit operator List< PatientAssessmentImage>(PatientAssessmentImageCollection coll)
		{
			List< PatientAssessmentImage> list = new List< PatientAssessmentImage>();
			
			foreach (PatientAssessmentImage emp in coll)
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
				return  PatientAssessmentImageMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientAssessmentImageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientAssessmentImage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientAssessmentImage();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientAssessmentImageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientAssessmentImageQuery();
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
		public bool Load(PatientAssessmentImageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientAssessmentImage AddNew()
		{
			PatientAssessmentImage entity = base.AddNewEntity() as PatientAssessmentImage;
			
			return entity;		
		}
		public PatientAssessmentImage FindByPrimaryKey(String registrationInfoMedicID, Int32 imageNo)
		{
			return base.FindByPrimaryKey(registrationInfoMedicID, imageNo) as PatientAssessmentImage;
		}

		#region IEnumerable< PatientAssessmentImage> Members

		IEnumerator< PatientAssessmentImage> IEnumerable< PatientAssessmentImage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientAssessmentImage;
			}
		}

		#endregion
		
		private PatientAssessmentImageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientAssessmentImage' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientAssessmentImage ({RegistrationInfoMedicID, ImageNo})")]
	[Serializable]
	public partial class PatientAssessmentImage : esPatientAssessmentImage
	{
		public PatientAssessmentImage()
		{
		}	
	
		public PatientAssessmentImage(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientAssessmentImageMetadata.Meta();
			}
		}	
	
		override protected esPatientAssessmentImageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientAssessmentImageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientAssessmentImageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientAssessmentImageQuery();
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
		public bool Load(PatientAssessmentImageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientAssessmentImageQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientAssessmentImageQuery : esPatientAssessmentImageQuery
	{
		public PatientAssessmentImageQuery()
		{

		}		
		
		public PatientAssessmentImageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientAssessmentImageQuery";
        }
	}

	[Serializable]
	public partial class PatientAssessmentImageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientAssessmentImageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientAssessmentImageMetadata.ColumnNames.RegistrationInfoMedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientAssessmentImageMetadata.PropertyNames.RegistrationInfoMedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientAssessmentImageMetadata.ColumnNames.ImageNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientAssessmentImageMetadata.PropertyNames.ImageNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientAssessmentImageMetadata.ColumnNames.DocumentName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientAssessmentImageMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientAssessmentImageMetadata.ColumnNames.DocumentImage, 3, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PatientAssessmentImageMetadata.PropertyNames.DocumentImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientAssessmentImageMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientAssessmentImageMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientAssessmentImageMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientAssessmentImageMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientAssessmentImageMetadata Meta()
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
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string ImageNo = "ImageNo";
			public const string DocumentName = "DocumentName";
			public const string DocumentImage = "DocumentImage";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string ImageNo = "ImageNo";
			public const string DocumentName = "DocumentName";
			public const string DocumentImage = "DocumentImage";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(PatientAssessmentImageMetadata))
			{
				if(PatientAssessmentImageMetadata.mapDelegates == null)
				{
					PatientAssessmentImageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientAssessmentImageMetadata.meta == null)
				{
					PatientAssessmentImageMetadata.meta = new PatientAssessmentImageMetadata();
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
				
				meta.AddTypeMap("RegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImageNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DocumentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "PatientAssessmentImage";
				meta.Destination = "PatientAssessmentImage";
				meta.spInsert = "proc_PatientAssessmentImageInsert";				
				meta.spUpdate = "proc_PatientAssessmentImageUpdate";		
				meta.spDelete = "proc_PatientAssessmentImageDelete";
				meta.spLoadAll = "proc_PatientAssessmentImageLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientAssessmentImageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientAssessmentImageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
