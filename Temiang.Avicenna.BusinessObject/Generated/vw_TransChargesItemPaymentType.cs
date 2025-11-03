/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/22/2021 6:25:03 PM
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
	abstract public class esvwTransChargesItemPaymentTypeCollection : esEntityCollection
	{
		public esvwTransChargesItemPaymentTypeCollection()
		{

		}
		
		protected override string GetCollectionName()
		{
			return "vwTransChargesItemPaymentTypeCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esvwTransChargesItemPaymentTypeQuery query)
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
			this.InitQuery(query as esvwTransChargesItemPaymentTypeQuery);
		}
		#endregion
			
		virtual public vwTransChargesItemPaymentType DetachEntity(vwTransChargesItemPaymentType entity)
		{
			return base.DetachEntity(entity) as vwTransChargesItemPaymentType;
		}
		
		virtual public vwTransChargesItemPaymentType AttachEntity(vwTransChargesItemPaymentType entity)
		{
			return base.AttachEntity(entity) as vwTransChargesItemPaymentType;
		}
		
		virtual public void Combine(vwTransChargesItemPaymentTypeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public vwTransChargesItemPaymentType this[int index]
		{
			get
			{
				return base[index] as vwTransChargesItemPaymentType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(vwTransChargesItemPaymentType);
		}
	}

	[Serializable]
	abstract public class esvwTransChargesItemPaymentType : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esvwTransChargesItemPaymentTypeQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esvwTransChargesItemPaymentType()
		{
		}
	
		public esvwTransChargesItemPaymentType(DataRow row)
			: base(row)
		{
		}
		
		protected override string GetConnectionName()
		{
			return "Vangkeh";
		}
		
		#region LoadByPrimaryKey

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
						case "PaymentMethodName": this.str.PaymentMethodName = (string)value; break;
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
		/// Maps to vwTransChargesItemPaymentType.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(vwTransChargesItemPaymentTypeMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(vwTransChargesItemPaymentTypeMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to vwTransChargesItemPaymentType.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(vwTransChargesItemPaymentTypeMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(vwTransChargesItemPaymentTypeMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to vwTransChargesItemPaymentType.PaymentMethodName
		/// </summary>
		virtual public System.String PaymentMethodName
		{
			get
			{
				return base.GetSystemString(vwTransChargesItemPaymentTypeMetadata.ColumnNames.PaymentMethodName);
			}
			
			set
			{
				base.SetSystemString(vwTransChargesItemPaymentTypeMetadata.ColumnNames.PaymentMethodName, value);
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
			public esStrings(esvwTransChargesItemPaymentType entity)
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
			public System.String PaymentMethodName
			{
				get
				{
					System.String data = entity.PaymentMethodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentMethodName = null;
					else entity.PaymentMethodName = Convert.ToString(value);
				}
			}
			private esvwTransChargesItemPaymentType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esvwTransChargesItemPaymentTypeQuery query)
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
				throw new Exception("esvwTransChargesItemPaymentType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class vwTransChargesItemPaymentType : esvwTransChargesItemPaymentType
	{	
	}

	[Serializable]
	abstract public class esvwTransChargesItemPaymentTypeQuery : esDynamicQuery
	{
		protected override string GetConnectionName()
		{
			return "Vangkeh";
		}
		
		override protected IMetadata Meta
		{
			get
			{
				return vwTransChargesItemPaymentTypeMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, vwTransChargesItemPaymentTypeMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, vwTransChargesItemPaymentTypeMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentMethodName
		{
			get
			{
				return new esQueryItem(this, vwTransChargesItemPaymentTypeMetadata.ColumnNames.PaymentMethodName, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("vwTransChargesItemPaymentTypeCollection")]
	public partial class vwTransChargesItemPaymentTypeCollection : esvwTransChargesItemPaymentTypeCollection, IEnumerable< vwTransChargesItemPaymentType>
	{
		public vwTransChargesItemPaymentTypeCollection()
		{

		}	
		
		public static implicit operator List< vwTransChargesItemPaymentType>(vwTransChargesItemPaymentTypeCollection coll)
		{
			List< vwTransChargesItemPaymentType> list = new List< vwTransChargesItemPaymentType>();
			
			foreach (vwTransChargesItemPaymentType emp in coll)
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
				return  vwTransChargesItemPaymentTypeMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vwTransChargesItemPaymentTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new vwTransChargesItemPaymentType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new vwTransChargesItemPaymentType();
		}
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public vwTransChargesItemPaymentTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vwTransChargesItemPaymentTypeQuery();
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
		public bool Load(vwTransChargesItemPaymentTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public vwTransChargesItemPaymentType AddNew()
		{
			vwTransChargesItemPaymentType entity = base.AddNewEntity() as vwTransChargesItemPaymentType;
			
			return entity;		
		}

		#region IEnumerable< vwTransChargesItemPaymentType> Members

		IEnumerator< vwTransChargesItemPaymentType> IEnumerable< vwTransChargesItemPaymentType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as vwTransChargesItemPaymentType;
			}
		}

		#endregion
		
		private vwTransChargesItemPaymentTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vwTransChargesItemPaymentType' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("vwTransChargesItemPaymentType ()")]
	[Serializable]
	public partial class vwTransChargesItemPaymentType : esvwTransChargesItemPaymentType
	{
		public vwTransChargesItemPaymentType()
		{
		}	
	
		public vwTransChargesItemPaymentType(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vwTransChargesItemPaymentTypeMetadata.Meta();
			}
		}	
	
		override protected esvwTransChargesItemPaymentTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vwTransChargesItemPaymentTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public vwTransChargesItemPaymentTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vwTransChargesItemPaymentTypeQuery();
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
		public bool Load(vwTransChargesItemPaymentTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private vwTransChargesItemPaymentTypeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class vwTransChargesItemPaymentTypeQuery : esvwTransChargesItemPaymentTypeQuery
	{
		public vwTransChargesItemPaymentTypeQuery()
		{

		}		
		
		public vwTransChargesItemPaymentTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "vwTransChargesItemPaymentTypeQuery";
        }
	}

	[Serializable]
	public partial class vwTransChargesItemPaymentTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vwTransChargesItemPaymentTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(vwTransChargesItemPaymentTypeMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransChargesItemPaymentTypeMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransChargesItemPaymentTypeMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransChargesItemPaymentTypeMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 12;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwTransChargesItemPaymentTypeMetadata.ColumnNames.PaymentMethodName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = vwTransChargesItemPaymentTypeMetadata.PropertyNames.PaymentMethodName;
			c.CharacterMaxLength = -1;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public vwTransChargesItemPaymentTypeMetadata Meta()
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
			public const string PaymentMethodName = "PaymentMethodName";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string PaymentMethodName = "PaymentMethodName";
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
			lock (typeof(vwTransChargesItemPaymentTypeMetadata))
			{
				if(vwTransChargesItemPaymentTypeMetadata.mapDelegates == null)
				{
					vwTransChargesItemPaymentTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vwTransChargesItemPaymentTypeMetadata.meta == null)
				{
					vwTransChargesItemPaymentTypeMetadata.meta = new vwTransChargesItemPaymentTypeMetadata();
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
				meta.AddTypeMap("PaymentMethodName", new esTypeMap("nvarchar", "System.String"));
		

				meta.Source = "vw_TransChargesItemPaymentType";
				meta.Destination = "vw_TransChargesItemPaymentType";
				meta.spInsert = "proc_vw_TransChargesItemPaymentTypeInsert";				
				meta.spUpdate = "proc_vw_TransChargesItemPaymentTypeUpdate";		
				meta.spDelete = "proc_vw_TransChargesItemPaymentTypeDelete";
				meta.spLoadAll = "proc_vw_TransChargesItemPaymentTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_TransChargesItemPaymentTypeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vwTransChargesItemPaymentTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
