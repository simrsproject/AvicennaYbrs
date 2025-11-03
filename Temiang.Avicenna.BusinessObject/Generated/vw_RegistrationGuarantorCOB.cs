/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/6/2017 2:55:03 PM
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
	abstract public class esvwRegistrationGuarantorCOBCollection : esEntityCollection
	{
		public esvwRegistrationGuarantorCOBCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "vwRegistrationGuarantorCOBCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esvwRegistrationGuarantorCOBQuery query)
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
			this.InitQuery(query as esvwRegistrationGuarantorCOBQuery);
		}
		#endregion
			
		virtual public vwRegistrationGuarantorCOB DetachEntity(vwRegistrationGuarantorCOB entity)
		{
			return base.DetachEntity(entity) as vwRegistrationGuarantorCOB;
		}
		
		virtual public vwRegistrationGuarantorCOB AttachEntity(vwRegistrationGuarantorCOB entity)
		{
			return base.AttachEntity(entity) as vwRegistrationGuarantorCOB;
		}
		
		virtual public void Combine(vwRegistrationGuarantorCOBCollection collection)
		{
			base.Combine(collection);
		}
		
		new public vwRegistrationGuarantorCOB this[int index]
		{
			get
			{
				return base[index] as vwRegistrationGuarantorCOB;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(vwRegistrationGuarantorCOB);
		}
	}

	[Serializable]
	abstract public class esvwRegistrationGuarantorCOB : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esvwRegistrationGuarantorCOBQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esvwRegistrationGuarantorCOB()
		{
		}
	
		public esvwRegistrationGuarantorCOB(DataRow row)
			: base(row)
		{
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "GuarantorName": this.str.GuarantorName = (string)value; break;
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
		/// Maps to vwRegistrationGuarantorCOB.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(vwRegistrationGuarantorCOBMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(vwRegistrationGuarantorCOBMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to vwRegistrationGuarantorCOB.GuarantorName
		/// </summary>
		virtual public System.String GuarantorName
		{
			get
			{
				return base.GetSystemString(vwRegistrationGuarantorCOBMetadata.ColumnNames.GuarantorName);
			}
			
			set
			{
				base.SetSystemString(vwRegistrationGuarantorCOBMetadata.ColumnNames.GuarantorName, value);
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
			public esStrings(esvwRegistrationGuarantorCOB entity)
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
			public System.String GuarantorName
			{
				get
				{
					System.String data = entity.GuarantorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorName = null;
					else entity.GuarantorName = Convert.ToString(value);
				}
			}
			private esvwRegistrationGuarantorCOB entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esvwRegistrationGuarantorCOBQuery query)
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
				throw new Exception("esvwRegistrationGuarantorCOB can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class vwRegistrationGuarantorCOB : esvwRegistrationGuarantorCOB
	{	
	}

	[Serializable]
	abstract public class esvwRegistrationGuarantorCOBQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return vwRegistrationGuarantorCOBMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, vwRegistrationGuarantorCOBMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem GuarantorName
		{
			get
			{
				return new esQueryItem(this, vwRegistrationGuarantorCOBMetadata.ColumnNames.GuarantorName, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("vwRegistrationGuarantorCOBCollection")]
	public partial class vwRegistrationGuarantorCOBCollection : esvwRegistrationGuarantorCOBCollection, IEnumerable< vwRegistrationGuarantorCOB>
	{
		public vwRegistrationGuarantorCOBCollection()
		{

		}	
		
		public static implicit operator List< vwRegistrationGuarantorCOB>(vwRegistrationGuarantorCOBCollection coll)
		{
			List< vwRegistrationGuarantorCOB> list = new List< vwRegistrationGuarantorCOB>();
			
			foreach (vwRegistrationGuarantorCOB emp in coll)
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
				return  vwRegistrationGuarantorCOBMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vwRegistrationGuarantorCOBQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new vwRegistrationGuarantorCOB(row);
		}

		override protected esEntity CreateEntity()
		{
			return new vwRegistrationGuarantorCOB();
		}
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public vwRegistrationGuarantorCOBQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vwRegistrationGuarantorCOBQuery();
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
		public bool Load(vwRegistrationGuarantorCOBQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public vwRegistrationGuarantorCOB AddNew()
		{
			vwRegistrationGuarantorCOB entity = base.AddNewEntity() as vwRegistrationGuarantorCOB;
			
			return entity;		
		}

		#region IEnumerable< vwRegistrationGuarantorCOB> Members

		IEnumerator< vwRegistrationGuarantorCOB> IEnumerable< vwRegistrationGuarantorCOB>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as vwRegistrationGuarantorCOB;
			}
		}

		#endregion
		
		private vwRegistrationGuarantorCOBQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vwRegistrationGuarantorCOB' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("vwRegistrationGuarantorCOB ()")]
	[Serializable]
	public partial class vwRegistrationGuarantorCOB : esvwRegistrationGuarantorCOB
	{
		public vwRegistrationGuarantorCOB()
		{
		}	
	
		public vwRegistrationGuarantorCOB(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vwRegistrationGuarantorCOBMetadata.Meta();
			}
		}	
	
		override protected esvwRegistrationGuarantorCOBQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vwRegistrationGuarantorCOBQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public vwRegistrationGuarantorCOBQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vwRegistrationGuarantorCOBQuery();
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
		public bool Load(vwRegistrationGuarantorCOBQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private vwRegistrationGuarantorCOBQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class vwRegistrationGuarantorCOBQuery : esvwRegistrationGuarantorCOBQuery
	{
		public vwRegistrationGuarantorCOBQuery()
		{

		}		
		
		public vwRegistrationGuarantorCOBQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "vwRegistrationGuarantorCOBQuery";
        }
	}

	[Serializable]
	public partial class vwRegistrationGuarantorCOBMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vwRegistrationGuarantorCOBMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(vwRegistrationGuarantorCOBMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = vwRegistrationGuarantorCOBMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(vwRegistrationGuarantorCOBMetadata.ColumnNames.GuarantorName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = vwRegistrationGuarantorCOBMetadata.PropertyNames.GuarantorName;
			c.CharacterMaxLength = -1;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public vwRegistrationGuarantorCOBMetadata Meta()
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
			public const string GuarantorName = "GuarantorName";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string GuarantorName = "GuarantorName";
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
			lock (typeof(vwRegistrationGuarantorCOBMetadata))
			{
				if(vwRegistrationGuarantorCOBMetadata.mapDelegates == null)
				{
					vwRegistrationGuarantorCOBMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vwRegistrationGuarantorCOBMetadata.meta == null)
				{
					vwRegistrationGuarantorCOBMetadata.meta = new vwRegistrationGuarantorCOBMetadata();
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
				meta.AddTypeMap("GuarantorName", new esTypeMap("nvarchar", "System.String"));
		

				meta.Source = "vw_RegistrationGuarantorCOB";
				meta.Destination = "vw_RegistrationGuarantorCOB";
				meta.spInsert = "proc_vw_RegistrationGuarantorCOBInsert";				
				meta.spUpdate = "proc_vw_RegistrationGuarantorCOBUpdate";		
				meta.spDelete = "proc_vw_RegistrationGuarantorCOBDelete";
				meta.spLoadAll = "proc_vw_RegistrationGuarantorCOBLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_RegistrationGuarantorCOBLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vwRegistrationGuarantorCOBMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
