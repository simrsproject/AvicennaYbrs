/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 04/27/20 1:42:26 PM
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
	abstract public class esRegistrationRasproLineCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationRasproLineCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationRasproLineCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationRasproLineQuery query)
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
			this.InitQuery(query as esRegistrationRasproLineQuery);
		}
		#endregion
			
		virtual public RegistrationRasproLine DetachEntity(RegistrationRasproLine entity)
		{
			return base.DetachEntity(entity) as RegistrationRasproLine;
		}
		
		virtual public RegistrationRasproLine AttachEntity(RegistrationRasproLine entity)
		{
			return base.AttachEntity(entity) as RegistrationRasproLine;
		}
		
		virtual public void Combine(RegistrationRasproLineCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationRasproLine this[int index]
		{
			get
			{
				return base[index] as RegistrationRasproLine;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationRasproLine);
		}
	}

	[Serializable]
	abstract public class esRegistrationRasproLine : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationRasproLineQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationRasproLine()
		{
		}
	
		public esRegistrationRasproLine(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo, rasproLineID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo, rasproLineID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 seqNo, String rasproLineID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo, rasproLineID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo, rasproLineID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			esRegistrationRasproLineQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo, query.RasproLineID == rasproLineID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("SeqNo",seqNo);
			parms.Add("RasproLineID",rasproLineID);
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
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "RasproLineID": this.str.RasproLineID = (string)value; break;
						case "Condition": this.str.Condition = (string)value; break;
						case "ActionNo": this.str.ActionNo = (string)value; break;
						case "AntibioticLevel": this.str.AntibioticLevel = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SeqNo":
						
							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
							break;
						case "ActionNo":
						
							if (value == null || value is System.Int32)
								this.ActionNo = (System.Int32?)value;
							break;
						case "AntibioticLevel":
						
							if (value == null || value is System.Int32)
								this.AntibioticLevel = (System.Int32?)value;
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
		/// Maps to RegistrationRasproLine.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationRasproLineMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationRasproLineMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproLine.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproLineMetadata.ColumnNames.SeqNo);
			}
			
			set
			{
				base.SetSystemInt32(RegistrationRasproLineMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproLine.RasproLineID
		/// </summary>
		virtual public System.String RasproLineID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproLineMetadata.ColumnNames.RasproLineID);
			}
			
			set
			{
				base.SetSystemString(RegistrationRasproLineMetadata.ColumnNames.RasproLineID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproLine.Condition
		/// </summary>
		virtual public System.String Condition
		{
			get
			{
				return base.GetSystemString(RegistrationRasproLineMetadata.ColumnNames.Condition);
			}
			
			set
			{
				base.SetSystemString(RegistrationRasproLineMetadata.ColumnNames.Condition, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproLine.ActionNo
		/// </summary>
		virtual public System.Int32? ActionNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproLineMetadata.ColumnNames.ActionNo);
			}
			
			set
			{
				base.SetSystemInt32(RegistrationRasproLineMetadata.ColumnNames.ActionNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproLine.AntibioticLevel
		/// </summary>
		virtual public System.Int32? AntibioticLevel
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproLineMetadata.ColumnNames.AntibioticLevel);
			}
			
			set
			{
				base.SetSystemInt32(RegistrationRasproLineMetadata.ColumnNames.AntibioticLevel, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproLine.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationRasproLineMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationRasproLineMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproLine.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproLineMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationRasproLineMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationRasproLine entity)
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
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
				}
			}
			public System.String RasproLineID
			{
				get
				{
					System.String data = entity.RasproLineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasproLineID = null;
					else entity.RasproLineID = Convert.ToString(value);
				}
			}
			public System.String Condition
			{
				get
				{
					System.String data = entity.Condition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Condition = null;
					else entity.Condition = Convert.ToString(value);
				}
			}
			public System.String ActionNo
			{
				get
				{
					System.Int32? data = entity.ActionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionNo = null;
					else entity.ActionNo = Convert.ToInt32(value);
				}
			}
			public System.String AntibioticLevel
			{
				get
				{
					System.Int32? data = entity.AntibioticLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AntibioticLevel = null;
					else entity.AntibioticLevel = Convert.ToInt32(value);
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
			private esRegistrationRasproLine entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationRasproLineQuery query)
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
				throw new Exception("esRegistrationRasproLine can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationRasproLine : esRegistrationRasproLine
	{	
	}

	[Serializable]
	abstract public class esRegistrationRasproLineQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationRasproLineMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RasproLineID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.RasproLineID, esSystemType.String);
			}
		} 
			
		public esQueryItem Condition
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.Condition, esSystemType.String);
			}
		} 
			
		public esQueryItem ActionNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.ActionNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AntibioticLevel
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.AntibioticLevel, esSystemType.Int32);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationRasproLineCollection")]
	public partial class RegistrationRasproLineCollection : esRegistrationRasproLineCollection, IEnumerable< RegistrationRasproLine>
	{
		public RegistrationRasproLineCollection()
		{

		}	
		
		public static implicit operator List< RegistrationRasproLine>(RegistrationRasproLineCollection coll)
		{
			List< RegistrationRasproLine> list = new List< RegistrationRasproLine>();
			
			foreach (RegistrationRasproLine emp in coll)
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
				return  RegistrationRasproLineMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationRasproLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationRasproLine(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationRasproLine();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationRasproLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationRasproLineQuery();
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
		public bool Load(RegistrationRasproLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationRasproLine AddNew()
		{
			RegistrationRasproLine entity = base.AddNewEntity() as RegistrationRasproLine;
			
			return entity;		
		}
		public RegistrationRasproLine FindByPrimaryKey(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			return base.FindByPrimaryKey(registrationNo, seqNo, rasproLineID) as RegistrationRasproLine;
		}

		#region IEnumerable< RegistrationRasproLine> Members

		IEnumerator< RegistrationRasproLine> IEnumerable< RegistrationRasproLine>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationRasproLine;
			}
		}

		#endregion
		
		private RegistrationRasproLineQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationRasproLine' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationRasproLine ({RegistrationNo, SeqNo, RasproLineID})")]
	[Serializable]
	public partial class RegistrationRasproLine : esRegistrationRasproLine
	{
		public RegistrationRasproLine()
		{
		}	
	
		public RegistrationRasproLine(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationRasproLineMetadata.Meta();
			}
		}	
	
		override protected esRegistrationRasproLineQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationRasproLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationRasproLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationRasproLineQuery();
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
		public bool Load(RegistrationRasproLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationRasproLineQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationRasproLineQuery : esRegistrationRasproLineQuery
	{
		public RegistrationRasproLineQuery()
		{

		}		
		
		public RegistrationRasproLineQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationRasproLineQuery";
        }
	}

	[Serializable]
	public partial class RegistrationRasproLineMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationRasproLineMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.RasproLineID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.RasproLineID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.Condition, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.Condition;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.ActionNo, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.ActionNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.AntibioticLevel, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.AntibioticLevel;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationRasproLineMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproLineMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RegistrationRasproLineMetadata Meta()
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
			public const string SeqNo = "SeqNo";
			public const string RasproLineID = "RasproLineID";
			public const string Condition = "Condition";
			public const string ActionNo = "ActionNo";
			public const string AntibioticLevel = "AntibioticLevel";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string RasproLineID = "RasproLineID";
			public const string Condition = "Condition";
			public const string ActionNo = "ActionNo";
			public const string AntibioticLevel = "AntibioticLevel";
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
			lock (typeof(RegistrationRasproLineMetadata))
			{
				if(RegistrationRasproLineMetadata.mapDelegates == null)
				{
					RegistrationRasproLineMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationRasproLineMetadata.meta == null)
				{
					RegistrationRasproLineMetadata.meta = new RegistrationRasproLineMetadata();
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
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RasproLineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Condition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AntibioticLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "RegistrationRasproLine";
				meta.Destination = "RegistrationRasproLine";
				meta.spInsert = "proc_RegistrationRasproLineInsert";				
				meta.spUpdate = "proc_RegistrationRasproLineUpdate";		
				meta.spDelete = "proc_RegistrationRasproLineDelete";
				meta.spLoadAll = "proc_RegistrationRasproLineLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationRasproLineLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationRasproLineMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
