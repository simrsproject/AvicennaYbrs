/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/13/2020 1:27:06 PM
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
	abstract public class esRegistrationPtoCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationPtoCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationPtoCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationPtoQuery query)
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
			this.InitQuery(query as esRegistrationPtoQuery);
		}
		#endregion
			
		virtual public RegistrationPto DetachEntity(RegistrationPto entity)
		{
			return base.DetachEntity(entity) as RegistrationPto;
		}
		
		virtual public RegistrationPto AttachEntity(RegistrationPto entity)
		{
			return base.AttachEntity(entity) as RegistrationPto;
		}
		
		virtual public void Combine(RegistrationPtoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationPto this[int index]
		{
			get
			{
				return base[index] as RegistrationPto;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationPto);
		}
	}

	[Serializable]
	abstract public class esRegistrationPto : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationPtoQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationPto()
		{
		}
	
		public esRegistrationPto(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 ptoNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, ptoNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, ptoNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 ptoNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, ptoNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, ptoNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 ptoNo)
		{
			esRegistrationPtoQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.PtoNo == ptoNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 ptoNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("PtoNo",ptoNo);
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
						case "PtoNo": this.str.PtoNo = (string)value; break;
						case "PtoDateTime": this.str.PtoDateTime = (string)value; break;
						case "PtoS": this.str.PtoS = (string)value; break;
						case "PtoO": this.str.PtoO = (string)value; break;
						case "PtoA": this.str.PtoA = (string)value; break;
						case "PtoP": this.str.PtoP = (string)value; break;
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
						case "PtoNo":
						
							if (value == null || value is System.Int32)
								this.PtoNo = (System.Int32?)value;
							break;
						case "PtoDateTime":
						
							if (value == null || value is System.DateTime)
								this.PtoDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationPto.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.PtoNo
		/// </summary>
		virtual public System.Int32? PtoNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationPtoMetadata.ColumnNames.PtoNo);
			}
			
			set
			{
				base.SetSystemInt32(RegistrationPtoMetadata.ColumnNames.PtoNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.PtoDateTime
		/// </summary>
		virtual public System.DateTime? PtoDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationPtoMetadata.ColumnNames.PtoDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationPtoMetadata.ColumnNames.PtoDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.PtoS
		/// </summary>
		virtual public System.String PtoS
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.PtoS);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.PtoS, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.PtoO
		/// </summary>
		virtual public System.String PtoO
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.PtoO);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.PtoO, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.PtoA
		/// </summary>
		virtual public System.String PtoA
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.PtoA);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.PtoA, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.PtoP
		/// </summary>
		virtual public System.String PtoP
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.PtoP);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.PtoP, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(RegistrationPtoMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationPtoMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationPtoMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationPtoMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationPtoMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationPtoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPto.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(RegistrationPtoMetadata.ColumnNames.BedID);
			}
			
			set
			{
				base.SetSystemString(RegistrationPtoMetadata.ColumnNames.BedID, value);
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
			public esStrings(esRegistrationPto entity)
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
			public System.String PtoNo
			{
				get
				{
					System.Int32? data = entity.PtoNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PtoNo = null;
					else entity.PtoNo = Convert.ToInt32(value);
				}
			}
			public System.String PtoDateTime
			{
				get
				{
					System.DateTime? data = entity.PtoDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PtoDateTime = null;
					else entity.PtoDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String PtoS
			{
				get
				{
					System.String data = entity.PtoS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PtoS = null;
					else entity.PtoS = Convert.ToString(value);
				}
			}
			public System.String PtoO
			{
				get
				{
					System.String data = entity.PtoO;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PtoO = null;
					else entity.PtoO = Convert.ToString(value);
				}
			}
			public System.String PtoA
			{
				get
				{
					System.String data = entity.PtoA;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PtoA = null;
					else entity.PtoA = Convert.ToString(value);
				}
			}
			public System.String PtoP
			{
				get
				{
					System.String data = entity.PtoP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PtoP = null;
					else entity.PtoP = Convert.ToString(value);
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
			private esRegistrationPto entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationPtoQuery query)
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
				throw new Exception("esRegistrationPto can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationPto : esRegistrationPto
	{	
	}

	[Serializable]
	abstract public class esRegistrationPtoQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPtoMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PtoNo
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.PtoNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PtoDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.PtoDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PtoS
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.PtoS, esSystemType.String);
			}
		} 
			
		public esQueryItem PtoO
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.PtoO, esSystemType.String);
			}
		} 
			
		public esQueryItem PtoA
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.PtoA, esSystemType.String);
			}
		} 
			
		public esQueryItem PtoP
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.PtoP, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
			
		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, RegistrationPtoMetadata.ColumnNames.BedID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationPtoCollection")]
	public partial class RegistrationPtoCollection : esRegistrationPtoCollection, IEnumerable< RegistrationPto>
	{
		public RegistrationPtoCollection()
		{

		}	
		
		public static implicit operator List< RegistrationPto>(RegistrationPtoCollection coll)
		{
			List< RegistrationPto> list = new List< RegistrationPto>();
			
			foreach (RegistrationPto emp in coll)
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
				return  RegistrationPtoMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPtoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationPto(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationPto();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationPtoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPtoQuery();
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
		public bool Load(RegistrationPtoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationPto AddNew()
		{
			RegistrationPto entity = base.AddNewEntity() as RegistrationPto;
			
			return entity;		
		}
		public RegistrationPto FindByPrimaryKey(String registrationNo, Int32 ptoNo)
		{
			return base.FindByPrimaryKey(registrationNo, ptoNo) as RegistrationPto;
		}

		#region IEnumerable< RegistrationPto> Members

		IEnumerator< RegistrationPto> IEnumerable< RegistrationPto>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationPto;
			}
		}

		#endregion
		
		private RegistrationPtoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationPto' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationPto ({RegistrationNo, PtoNo})")]
	[Serializable]
	public partial class RegistrationPto : esRegistrationPto
	{
		public RegistrationPto()
		{
		}	
	
		public RegistrationPto(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPtoMetadata.Meta();
			}
		}	
	
		override protected esRegistrationPtoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPtoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationPtoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPtoQuery();
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
		public bool Load(RegistrationPtoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationPtoQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationPtoQuery : esRegistrationPtoQuery
	{
		public RegistrationPtoQuery()
		{

		}		
		
		public RegistrationPtoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationPtoQuery";
        }
	}

	[Serializable]
	public partial class RegistrationPtoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationPtoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.PtoNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.PtoNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.PtoDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.PtoDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.PtoS, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.PtoS;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.PtoO, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.PtoO;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.PtoA, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.PtoA;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.PtoP, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.PtoP;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.IsDeleted, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.ServiceUnitID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.RoomID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationPtoMetadata.ColumnNames.BedID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPtoMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RegistrationPtoMetadata Meta()
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
			public const string PtoNo = "PtoNo";
			public const string PtoDateTime = "PtoDateTime";
			public const string PtoS = "PtoS";
			public const string PtoO = "PtoO";
			public const string PtoA = "PtoA";
			public const string PtoP = "PtoP";
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
			public const string PtoNo = "PtoNo";
			public const string PtoDateTime = "PtoDateTime";
			public const string PtoS = "PtoS";
			public const string PtoO = "PtoO";
			public const string PtoA = "PtoA";
			public const string PtoP = "PtoP";
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
			lock (typeof(RegistrationPtoMetadata))
			{
				if(RegistrationPtoMetadata.mapDelegates == null)
				{
					RegistrationPtoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationPtoMetadata.meta == null)
				{
					RegistrationPtoMetadata.meta = new RegistrationPtoMetadata();
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
				meta.AddTypeMap("PtoNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PtoDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PtoS", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PtoO", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PtoA", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PtoP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "RegistrationPto";
				meta.Destination = "RegistrationPto";
				meta.spInsert = "proc_RegistrationPtoInsert";				
				meta.spUpdate = "proc_RegistrationPtoUpdate";		
				meta.spDelete = "proc_RegistrationPtoDelete";
				meta.spLoadAll = "proc_RegistrationPtoLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationPtoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationPtoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
