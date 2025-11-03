/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/11/2023 1:53:00 PM
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
	abstract public class esPatientFieldCollection : esEntityCollectionWAuditLog
	{
		public esPatientFieldCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientFieldCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientFieldQuery query)
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
			this.InitQuery(query as esPatientFieldQuery);
		}
		#endregion
			
		virtual public PatientField DetachEntity(PatientField entity)
		{
			return base.DetachEntity(entity) as PatientField;
		}
		
		virtual public PatientField AttachEntity(PatientField entity)
		{
			return base.AttachEntity(entity) as PatientField;
		}
		
		virtual public void Combine(PatientFieldCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientField this[int index]
		{
			get
			{
				return base[index] as PatientField;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientField);
		}
	}

	[Serializable]
	abstract public class esPatientField : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientFieldQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientField()
		{
		}
	
		public esPatientField(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String patientID, Int32 fieldID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, fieldID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, fieldID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, Int32 fieldID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, fieldID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, fieldID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String patientID, Int32 fieldID)
		{
			esPatientFieldQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID, query.FieldID == fieldID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String patientID, Int32 fieldID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);
			parms.Add("FieldID",fieldID);
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
						case "FieldID": this.str.FieldID = (string)value; break;
						case "DataDateTime": this.str.DataDateTime = (string)value; break;
						case "ValueInString": this.str.ValueInString = (string)value; break;
						case "ValueInNumeric": this.str.ValueInNumeric = (string)value; break;
						case "ValueInDatetime": this.str.ValueInDatetime = (string)value; break;
						case "ValueInBool": this.str.ValueInBool = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "FieldID":
						
							if (value == null || value is System.Int32)
								this.FieldID = (System.Int32?)value;
							break;
						case "DataDateTime":
						
							if (value == null || value is System.DateTime)
								this.DataDateTime = (System.DateTime?)value;
							break;
						case "ValueInNumeric":
						
							if (value == null || value is System.Decimal)
								this.ValueInNumeric = (System.Decimal?)value;
							break;
						case "ValueInDatetime":
						
							if (value == null || value is System.DateTime)
								this.ValueInDatetime = (System.DateTime?)value;
							break;
						case "ValueInBool":
						
							if (value == null || value is System.Boolean)
								this.ValueInBool = (System.Boolean?)value;
							break;
						case "ValueInImage":
						
							if (value == null || value is System.Byte[])
								this.ValueInImage = (System.Byte[])value;
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
		/// Maps to PatientField.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientFieldMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(PatientFieldMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.FieldID
		/// </summary>
		virtual public System.Int32? FieldID
		{
			get
			{
				return base.GetSystemInt32(PatientFieldMetadata.ColumnNames.FieldID);
			}
			
			set
			{
				base.SetSystemInt32(PatientFieldMetadata.ColumnNames.FieldID, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.DataDateTime
		/// </summary>
		virtual public System.DateTime? DataDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientFieldMetadata.ColumnNames.DataDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientFieldMetadata.ColumnNames.DataDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.ValueInString
		/// </summary>
		virtual public System.String ValueInString
		{
			get
			{
				return base.GetSystemString(PatientFieldMetadata.ColumnNames.ValueInString);
			}
			
			set
			{
				base.SetSystemString(PatientFieldMetadata.ColumnNames.ValueInString, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.ValueInNumeric
		/// </summary>
		virtual public System.Decimal? ValueInNumeric
		{
			get
			{
				return base.GetSystemDecimal(PatientFieldMetadata.ColumnNames.ValueInNumeric);
			}
			
			set
			{
				base.SetSystemDecimal(PatientFieldMetadata.ColumnNames.ValueInNumeric, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.ValueInDatetime
		/// </summary>
		virtual public System.DateTime? ValueInDatetime
		{
			get
			{
				return base.GetSystemDateTime(PatientFieldMetadata.ColumnNames.ValueInDatetime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientFieldMetadata.ColumnNames.ValueInDatetime, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.ValueInBool
		/// </summary>
		virtual public System.Boolean? ValueInBool
		{
			get
			{
				return base.GetSystemBoolean(PatientFieldMetadata.ColumnNames.ValueInBool);
			}
			
			set
			{
				base.SetSystemBoolean(PatientFieldMetadata.ColumnNames.ValueInBool, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.ValueInImage
		/// </summary>
		virtual public System.Byte[] ValueInImage
		{
			get
			{
				return base.GetSystemByteArray(PatientFieldMetadata.ColumnNames.ValueInImage);
			}
			
			set
			{
				base.SetSystemByteArray(PatientFieldMetadata.ColumnNames.ValueInImage, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientFieldMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientFieldMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientField.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientFieldMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientFieldMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esPatientField entity)
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
			public System.String FieldID
			{
				get
				{
					System.Int32? data = entity.FieldID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FieldID = null;
					else entity.FieldID = Convert.ToInt32(value);
				}
			}
			public System.String DataDateTime
			{
				get
				{
					System.DateTime? data = entity.DataDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DataDateTime = null;
					else entity.DataDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValueInString
			{
				get
				{
					System.String data = entity.ValueInString;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueInString = null;
					else entity.ValueInString = Convert.ToString(value);
				}
			}
			public System.String ValueInNumeric
			{
				get
				{
					System.Decimal? data = entity.ValueInNumeric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueInNumeric = null;
					else entity.ValueInNumeric = Convert.ToDecimal(value);
				}
			}
			public System.String ValueInDatetime
			{
				get
				{
					System.DateTime? data = entity.ValueInDatetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueInDatetime = null;
					else entity.ValueInDatetime = Convert.ToDateTime(value);
				}
			}
			public System.String ValueInBool
			{
				get
				{
					System.Boolean? data = entity.ValueInBool;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueInBool = null;
					else entity.ValueInBool = Convert.ToBoolean(value);
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
			private esPatientField entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientFieldQuery query)
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
				throw new Exception("esPatientField can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientField : esPatientField
	{	
	}

	[Serializable]
	abstract public class esPatientFieldQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientFieldMetadata.Meta();
			}
		}	
			
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
			
		public esQueryItem FieldID
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.FieldID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DataDateTime
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.DataDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ValueInString
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.ValueInString, esSystemType.String);
			}
		} 
			
		public esQueryItem ValueInNumeric
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.ValueInNumeric, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ValueInDatetime
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.ValueInDatetime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ValueInBool
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.ValueInBool, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ValueInImage
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.ValueInImage, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientFieldMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientFieldCollection")]
	public partial class PatientFieldCollection : esPatientFieldCollection, IEnumerable< PatientField>
	{
		public PatientFieldCollection()
		{

		}	
		
		public static implicit operator List< PatientField>(PatientFieldCollection coll)
		{
			List< PatientField> list = new List< PatientField>();
			
			foreach (PatientField emp in coll)
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
				return  PatientFieldMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFieldQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientField(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientField();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFieldQuery();
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
		public bool Load(PatientFieldQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientField AddNew()
		{
			PatientField entity = base.AddNewEntity() as PatientField;
			
			return entity;		
		}
		public PatientField FindByPrimaryKey(String patientID, Int32 fieldID)
		{
			return base.FindByPrimaryKey(patientID, fieldID) as PatientField;
		}

		#region IEnumerable< PatientField> Members

		IEnumerator< PatientField> IEnumerable< PatientField>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientField;
			}
		}

		#endregion
		
		private PatientFieldQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientField' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientField ({PatientID, FieldID})")]
	[Serializable]
	public partial class PatientField : esPatientField
	{
		public PatientField()
		{
		}	
	
		public PatientField(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientFieldMetadata.Meta();
			}
		}	
	
		override protected esPatientFieldQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFieldQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFieldQuery();
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
		public bool Load(PatientFieldQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientFieldQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientFieldQuery : esPatientFieldQuery
	{
		public PatientFieldQuery()
		{

		}		
		
		public PatientFieldQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientFieldQuery";
        }
	}

	[Serializable]
	public partial class PatientFieldMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientFieldMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFieldMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.FieldID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFieldMetadata.PropertyNames.FieldID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.DataDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFieldMetadata.PropertyNames.DataDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.ValueInString, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFieldMetadata.PropertyNames.ValueInString;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.ValueInNumeric, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFieldMetadata.PropertyNames.ValueInNumeric;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.ValueInDatetime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFieldMetadata.PropertyNames.ValueInDatetime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.ValueInBool, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientFieldMetadata.PropertyNames.ValueInBool;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.ValueInImage, 7, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PatientFieldMetadata.PropertyNames.ValueInImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFieldMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFieldMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFieldMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientFieldMetadata Meta()
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
			public const string FieldID = "FieldID";
			public const string DataDateTime = "DataDateTime";
			public const string ValueInString = "ValueInString";
			public const string ValueInNumeric = "ValueInNumeric";
			public const string ValueInDatetime = "ValueInDatetime";
			public const string ValueInBool = "ValueInBool";
			public const string ValueInImage = "ValueInImage";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PatientID = "PatientID";
			public const string FieldID = "FieldID";
			public const string DataDateTime = "DataDateTime";
			public const string ValueInString = "ValueInString";
			public const string ValueInNumeric = "ValueInNumeric";
			public const string ValueInDatetime = "ValueInDatetime";
			public const string ValueInBool = "ValueInBool";
			public const string ValueInImage = "ValueInImage";
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
			lock (typeof(PatientFieldMetadata))
			{
				if(PatientFieldMetadata.mapDelegates == null)
				{
					PatientFieldMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientFieldMetadata.meta == null)
				{
					PatientFieldMetadata.meta = new PatientFieldMetadata();
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
				meta.AddTypeMap("FieldID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DataDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValueInString", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValueInNumeric", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValueInDatetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValueInBool", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValueInImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "PatientField";
				meta.Destination = "PatientField";
				meta.spInsert = "proc_PatientFieldInsert";				
				meta.spUpdate = "proc_PatientFieldUpdate";		
				meta.spDelete = "proc_PatientFieldDelete";
				meta.spLoadAll = "proc_PatientFieldLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientFieldLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientFieldMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
