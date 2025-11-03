/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/29/2022 12:37:09 PM
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
	abstract public class esParamedicFeeExecutedFormulaCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeExecutedFormulaCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeExecutedFormulaCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeExecutedFormulaQuery query)
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
			this.InitQuery(query as esParamedicFeeExecutedFormulaQuery);
		}
		#endregion
			
		virtual public ParamedicFeeExecutedFormula DetachEntity(ParamedicFeeExecutedFormula entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeExecutedFormula;
		}
		
		virtual public ParamedicFeeExecutedFormula AttachEntity(ParamedicFeeExecutedFormula entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeExecutedFormula;
		}
		
		virtual public void Combine(ParamedicFeeExecutedFormulaCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeExecutedFormula this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeExecutedFormula;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeExecutedFormula);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeExecutedFormula : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeExecutedFormulaQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeExecutedFormula()
		{
		}
	
		public esParamedicFeeExecutedFormula(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			esParamedicFeeExecutedFormulaQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.SequenceNo==sequenceNo, query.TariffComponentID==tariffComponentID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("SequenceNo",sequenceNo);
			parms.Add("TariffComponentID",tariffComponentID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ExecutedFormula": this.str.ExecutedFormula = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
					
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
		/// Maps to ParamedicFeeExecutedFormula.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeExecutedFormula.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeExecutedFormula.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeExecutedFormula.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeExecutedFormula.ExecutedFormula
		/// </summary>
		virtual public System.String ExecutedFormula
		{
			get
			{
				return base.GetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.ExecutedFormula);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeExecutedFormulaMetadata.ColumnNames.ExecutedFormula, value);
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
			public esStrings(esParamedicFeeExecutedFormula entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
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
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
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
			public System.String ExecutedFormula
			{
				get
				{
					System.String data = entity.ExecutedFormula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExecutedFormula = null;
					else entity.ExecutedFormula = Convert.ToString(value);
				}
			}
			private esParamedicFeeExecutedFormula entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeExecutedFormulaQuery query)
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
				throw new Exception("esParamedicFeeExecutedFormula can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeExecutedFormula : esParamedicFeeExecutedFormula
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeExecutedFormulaQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeExecutedFormulaMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeExecutedFormulaMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeExecutedFormulaMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeExecutedFormulaMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeExecutedFormulaMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ExecutedFormula
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeExecutedFormulaMetadata.ColumnNames.ExecutedFormula, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeExecutedFormulaCollection")]
	public partial class ParamedicFeeExecutedFormulaCollection : esParamedicFeeExecutedFormulaCollection, IEnumerable< ParamedicFeeExecutedFormula>
	{
		public ParamedicFeeExecutedFormulaCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeExecutedFormula>(ParamedicFeeExecutedFormulaCollection coll)
		{
			List< ParamedicFeeExecutedFormula> list = new List< ParamedicFeeExecutedFormula>();
			
			foreach (ParamedicFeeExecutedFormula emp in coll)
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
				return  ParamedicFeeExecutedFormulaMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeExecutedFormulaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeExecutedFormula(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeExecutedFormula();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeExecutedFormulaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeExecutedFormulaQuery();
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
		public bool Load(ParamedicFeeExecutedFormulaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeExecutedFormula AddNew()
		{
			ParamedicFeeExecutedFormula entity = base.AddNewEntity() as ParamedicFeeExecutedFormula;
			
			return entity;		
		}
		public ParamedicFeeExecutedFormula FindByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID) as ParamedicFeeExecutedFormula;
		}

		#region IEnumerable< ParamedicFeeExecutedFormula> Members

		IEnumerator< ParamedicFeeExecutedFormula> IEnumerable< ParamedicFeeExecutedFormula>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeExecutedFormula;
			}
		}

		#endregion
		
		private ParamedicFeeExecutedFormulaQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeExecutedFormula' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeExecutedFormula ({TransactionNo, SequenceNo, TariffComponentID})")]
	[Serializable]
	public partial class ParamedicFeeExecutedFormula : esParamedicFeeExecutedFormula
	{
		public ParamedicFeeExecutedFormula()
		{
		}	
	
		public ParamedicFeeExecutedFormula(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeExecutedFormulaMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeExecutedFormulaQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeExecutedFormulaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeExecutedFormulaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeExecutedFormulaQuery();
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
		public bool Load(ParamedicFeeExecutedFormulaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeExecutedFormulaQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeExecutedFormulaQuery : esParamedicFeeExecutedFormulaQuery
	{
		public ParamedicFeeExecutedFormulaQuery()
		{

		}		
		
		public ParamedicFeeExecutedFormulaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeExecutedFormulaQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeExecutedFormulaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeExecutedFormulaMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeExecutedFormulaMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeExecutedFormulaMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeExecutedFormulaMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeExecutedFormulaMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeExecutedFormulaMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeExecutedFormulaMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeExecutedFormulaMetadata.ColumnNames.RegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeExecutedFormulaMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeExecutedFormulaMetadata.ColumnNames.ExecutedFormula, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeExecutedFormulaMetadata.PropertyNames.ExecutedFormula;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeExecutedFormulaMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string RegistrationNo = "RegistrationNo";
			public const string ExecutedFormula = "ExecutedFormula";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string RegistrationNo = "RegistrationNo";
			public const string ExecutedFormula = "ExecutedFormula";
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
			lock (typeof(ParamedicFeeExecutedFormulaMetadata))
			{
				if(ParamedicFeeExecutedFormulaMetadata.mapDelegates == null)
				{
					ParamedicFeeExecutedFormulaMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeExecutedFormulaMetadata.meta == null)
				{
					ParamedicFeeExecutedFormulaMetadata.meta = new ParamedicFeeExecutedFormulaMetadata();
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
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExecutedFormula", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ParamedicFeeExecutedFormula";
				meta.Destination = "ParamedicFeeExecutedFormula";
				meta.spInsert = "proc_ParamedicFeeExecutedFormulaInsert";				
				meta.spUpdate = "proc_ParamedicFeeExecutedFormulaUpdate";		
				meta.spDelete = "proc_ParamedicFeeExecutedFormulaDelete";
				meta.spLoadAll = "proc_ParamedicFeeExecutedFormulaLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeExecutedFormulaLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeExecutedFormulaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
