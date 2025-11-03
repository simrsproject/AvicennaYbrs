/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/11/2021 6:51:34 PM
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
	abstract public class esRasproCollection : esEntityCollectionWAuditLog
	{
		public esRasproCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RasproCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRasproQuery query)
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
			this.InitQuery(query as esRasproQuery);
		}
		#endregion
			
		virtual public Raspro DetachEntity(Raspro entity)
		{
			return base.DetachEntity(entity) as Raspro;
		}
		
		virtual public Raspro AttachEntity(Raspro entity)
		{
			return base.AttachEntity(entity) as Raspro;
		}
		
		virtual public void Combine(RasproCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Raspro this[int index]
		{
			get
			{
				return base[index] as Raspro;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Raspro);
		}
	}

	[Serializable]
	abstract public class esRaspro : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRasproQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRaspro()
		{
		}
	
		public esRaspro(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String rasproLineID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rasproLineID);
			else
				return LoadByPrimaryKeyStoredProcedure(rasproLineID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String rasproLineID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rasproLineID);
			else
				return LoadByPrimaryKeyStoredProcedure(rasproLineID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String rasproLineID)
		{
			esRasproQuery query = this.GetDynamicQuery();
			query.Where(query.RasproLineID == rasproLineID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String rasproLineID)
		{
			esParameters parms = new esParameters();
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
						case "RasproLineID": this.str.RasproLineID = (string)value; break;
						case "SRRaspro": this.str.SRRaspro = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "Spesification": this.str.Spesification = (string)value; break;
						case "YesAction": this.str.YesAction = (string)value; break;
						case "IsYesContinue": this.str.IsYesContinue = (string)value; break;
						case "YesActionDescription": this.str.YesActionDescription = (string)value; break;
						case "NoAction": this.str.NoAction = (string)value; break;
						case "IsNoContinue": this.str.IsNoContinue = (string)value; break;
						case "NoActionDescription": this.str.NoActionDescription = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
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
						case "IsYesContinue":
						
							if (value == null || value is System.Boolean)
								this.IsYesContinue = (System.Boolean?)value;
							break;
						case "IsNoContinue":
						
							if (value == null || value is System.Boolean)
								this.IsNoContinue = (System.Boolean?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to Raspro.RasproLineID
		/// </summary>
		virtual public System.String RasproLineID
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.RasproLineID);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.RasproLineID, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.SRRaspro
		/// </summary>
		virtual public System.String SRRaspro
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.SRRaspro);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.SRRaspro, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(RasproMetadata.ColumnNames.SeqNo);
			}
			
			set
			{
				base.SetSystemInt32(RasproMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.Spesification
		/// </summary>
		virtual public System.String Spesification
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.Spesification);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.Spesification, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.YesAction
		/// </summary>
		virtual public System.String YesAction
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.YesAction);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.YesAction, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.IsYesContinue
		/// </summary>
		virtual public System.Boolean? IsYesContinue
		{
			get
			{
				return base.GetSystemBoolean(RasproMetadata.ColumnNames.IsYesContinue);
			}
			
			set
			{
				base.SetSystemBoolean(RasproMetadata.ColumnNames.IsYesContinue, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.YesActionDescription
		/// </summary>
		virtual public System.String YesActionDescription
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.YesActionDescription);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.YesActionDescription, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.NoAction
		/// </summary>
		virtual public System.String NoAction
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.NoAction);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.NoAction, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.IsNoContinue
		/// </summary>
		virtual public System.Boolean? IsNoContinue
		{
			get
			{
				return base.GetSystemBoolean(RasproMetadata.ColumnNames.IsNoContinue);
			}
			
			set
			{
				base.SetSystemBoolean(RasproMetadata.ColumnNames.IsNoContinue, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.NoActionDescription
		/// </summary>
		virtual public System.String NoActionDescription
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.NoActionDescription);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.NoActionDescription, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RasproMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RasproMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RasproMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RasproMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Raspro.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RasproMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RasproMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRaspro entity)
			{
				this.entity = entity;
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
			public System.String SRRaspro
			{
				get
				{
					System.String data = entity.SRRaspro;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRaspro = null;
					else entity.SRRaspro = Convert.ToString(value);
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
			public System.String Spesification
			{
				get
				{
					System.String data = entity.Spesification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Spesification = null;
					else entity.Spesification = Convert.ToString(value);
				}
			}
			public System.String YesAction
			{
				get
				{
					System.String data = entity.YesAction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YesAction = null;
					else entity.YesAction = Convert.ToString(value);
				}
			}
			public System.String IsYesContinue
			{
				get
				{
					System.Boolean? data = entity.IsYesContinue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsYesContinue = null;
					else entity.IsYesContinue = Convert.ToBoolean(value);
				}
			}
			public System.String YesActionDescription
			{
				get
				{
					System.String data = entity.YesActionDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YesActionDescription = null;
					else entity.YesActionDescription = Convert.ToString(value);
				}
			}
			public System.String NoAction
			{
				get
				{
					System.String data = entity.NoAction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoAction = null;
					else entity.NoAction = Convert.ToString(value);
				}
			}
			public System.String IsNoContinue
			{
				get
				{
					System.Boolean? data = entity.IsNoContinue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNoContinue = null;
					else entity.IsNoContinue = Convert.ToBoolean(value);
				}
			}
			public System.String NoActionDescription
			{
				get
				{
					System.String data = entity.NoActionDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoActionDescription = null;
					else entity.NoActionDescription = Convert.ToString(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			private esRaspro entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRasproQuery query)
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
				throw new Exception("esRaspro can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Raspro : esRaspro
	{	
	}

	[Serializable]
	abstract public class esRasproQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RasproMetadata.Meta();
			}
		}	
			
		public esQueryItem RasproLineID
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.RasproLineID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRaspro
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.SRRaspro, esSystemType.String);
			}
		} 
			
		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Spesification
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.Spesification, esSystemType.String);
			}
		} 
			
		public esQueryItem YesAction
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.YesAction, esSystemType.String);
			}
		} 
			
		public esQueryItem IsYesContinue
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.IsYesContinue, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem YesActionDescription
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.YesActionDescription, esSystemType.String);
			}
		} 
			
		public esQueryItem NoAction
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.NoAction, esSystemType.String);
			}
		} 
			
		public esQueryItem IsNoContinue
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.IsNoContinue, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem NoActionDescription
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.NoActionDescription, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RasproMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RasproCollection")]
	public partial class RasproCollection : esRasproCollection, IEnumerable< Raspro>
	{
		public RasproCollection()
		{

		}	
		
		public static implicit operator List< Raspro>(RasproCollection coll)
		{
			List< Raspro> list = new List< Raspro>();
			
			foreach (Raspro emp in coll)
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
				return  RasproMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RasproQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Raspro(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Raspro();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RasproQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RasproQuery();
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
		public bool Load(RasproQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Raspro AddNew()
		{
			Raspro entity = base.AddNewEntity() as Raspro;
			
			return entity;		
		}
		public Raspro FindByPrimaryKey(String rasproLineID)
		{
			return base.FindByPrimaryKey(rasproLineID) as Raspro;
		}

		#region IEnumerable< Raspro> Members

		IEnumerator< Raspro> IEnumerable< Raspro>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Raspro;
			}
		}

		#endregion
		
		private RasproQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Raspro' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Raspro ({RasproLineID})")]
	[Serializable]
	public partial class Raspro : esRaspro
	{
		public Raspro()
		{
		}	
	
		public Raspro(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RasproMetadata.Meta();
			}
		}	
	
		override protected esRasproQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RasproQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RasproQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RasproQuery();
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
		public bool Load(RasproQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RasproQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RasproQuery : esRasproQuery
	{
		public RasproQuery()
		{

		}		
		
		public RasproQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RasproQuery";
        }
	}

	[Serializable]
	public partial class RasproMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RasproMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.RasproLineID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.RasproLineID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.SRRaspro, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.SRRaspro;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.SeqNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RasproMetadata.PropertyNames.SeqNo;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.Spesification, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.Spesification;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.YesAction, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.YesAction;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.IsYesContinue, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RasproMetadata.PropertyNames.IsYesContinue;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.YesActionDescription, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.YesActionDescription;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.NoAction, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.NoAction;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.IsNoContinue, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RasproMetadata.PropertyNames.IsNoContinue;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.NoActionDescription, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.NoActionDescription;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.CreateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RasproMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.CreateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RasproMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RasproMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RasproMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RasproMetadata Meta()
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
			public const string RasproLineID = "RasproLineID";
			public const string SRRaspro = "SRRaspro";
			public const string SeqNo = "SeqNo";
			public const string Spesification = "Spesification";
			public const string YesAction = "YesAction";
			public const string IsYesContinue = "IsYesContinue";
			public const string YesActionDescription = "YesActionDescription";
			public const string NoAction = "NoAction";
			public const string IsNoContinue = "IsNoContinue";
			public const string NoActionDescription = "NoActionDescription";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RasproLineID = "RasproLineID";
			public const string SRRaspro = "SRRaspro";
			public const string SeqNo = "SeqNo";
			public const string Spesification = "Spesification";
			public const string YesAction = "YesAction";
			public const string IsYesContinue = "IsYesContinue";
			public const string YesActionDescription = "YesActionDescription";
			public const string NoAction = "NoAction";
			public const string IsNoContinue = "IsNoContinue";
			public const string NoActionDescription = "NoActionDescription";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
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
			lock (typeof(RasproMetadata))
			{
				if(RasproMetadata.mapDelegates == null)
				{
					RasproMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RasproMetadata.meta == null)
				{
					RasproMetadata.meta = new RasproMetadata();
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
				
				meta.AddTypeMap("RasproLineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRaspro", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Spesification", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("YesAction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsYesContinue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("YesActionDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoAction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNoContinue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NoActionDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "Raspro";
				meta.Destination = "Raspro";
				meta.spInsert = "proc_RasproInsert";				
				meta.spUpdate = "proc_RasproUpdate";		
				meta.spDelete = "proc_RasproDelete";
				meta.spLoadAll = "proc_RasproLoadAll";
				meta.spLoadByPrimaryKey = "proc_RasproLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RasproMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
