/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/06/2020 14:59:06
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
	abstract public class esVwHasilLabMobileSysmexCollection : esEntityCollection
	{
		public esVwHasilLabMobileSysmexCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "VwHasilLabMobileSysmexCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVwHasilLabMobileSysmexQuery query)
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
			this.InitQuery(query as esVwHasilLabMobileSysmexQuery);
		}
		#endregion
			
		virtual public VwHasilLabMobileSysmex DetachEntity(VwHasilLabMobileSysmex entity)
		{
			return base.DetachEntity(entity) as VwHasilLabMobileSysmex;
		}
		
		virtual public VwHasilLabMobileSysmex AttachEntity(VwHasilLabMobileSysmex entity)
		{
			return base.AttachEntity(entity) as VwHasilLabMobileSysmex;
		}
		
		virtual public void Combine(VwHasilLabMobileSysmexCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwHasilLabMobileSysmex this[int index]
		{
			get
			{
				return base[index] as VwHasilLabMobileSysmex;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwHasilLabMobileSysmex);
		}
	}

	[Serializable]
	abstract public class esVwHasilLabMobileSysmex : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwHasilLabMobileSysmexQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVwHasilLabMobileSysmex()
		{
		}
	
		public esVwHasilLabMobileSysmex(DataRow row)
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "MedicalNo": this.str.MedicalNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "RegistrationDate": this.str.RegistrationDate = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "TestGroup": this.str.TestGroup = (string)value; break;
						case "TestCode": this.str.TestCode = (string)value; break;
						case "TestName": this.str.TestName = (string)value; break;
						case "Result": this.str.Result = (string)value; break;
						case "Flag": this.str.Flag = (string)value; break;
						case "NormalResult": this.str.NormalResult = (string)value; break;
						case "TestComment": this.str.TestComment = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RegistrationDate":
						
							if (value == null || value is System.DateTime)
								this.RegistrationDate = (System.DateTime?)value;
							break;
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
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
		/// Maps to VwHasilLabMobileSysmex.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.MedicalNo
		/// </summary>
		virtual public System.String MedicalNo
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.MedicalNo);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.MedicalNo, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.RegistrationDate
		/// </summary>
		virtual public System.DateTime? RegistrationDate
		{
			get
			{
				return base.GetSystemDateTime(VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationDate, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.TestGroup
		/// </summary>
		virtual public System.String TestGroup
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestGroup);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestGroup, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.TestCode
		/// </summary>
		virtual public System.String TestCode
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestCode);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestCode, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.TestName
		/// </summary>
		virtual public System.String TestName
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestName);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestName, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.Result);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.Result, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.Flag
		/// </summary>
		virtual public System.String Flag
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.Flag);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.Flag, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.NormalResult
		/// </summary>
		virtual public System.String NormalResult
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.NormalResult);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.NormalResult, value);
			}
		}
		/// <summary>
		/// Maps to VwHasilLabMobileSysmex.TestComment
		/// </summary>
		virtual public System.String TestComment
		{
			get
			{
				return base.GetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestComment);
			}
			
			set
			{
				base.SetSystemString(VwHasilLabMobileSysmexMetadata.ColumnNames.TestComment, value);
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
			public esStrings(esVwHasilLabMobileSysmex entity)
			{
				this.entity = entity;
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
			public System.String MedicalNo
			{
				get
				{
					System.String data = entity.MedicalNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalNo = null;
					else entity.MedicalNo = Convert.ToString(value);
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
			public System.String RegistrationDate
			{
				get
				{
					System.DateTime? data = entity.RegistrationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationDate = null;
					else entity.RegistrationDate = Convert.ToDateTime(value);
				}
			}
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String TestGroup
			{
				get
				{
					System.String data = entity.TestGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestGroup = null;
					else entity.TestGroup = Convert.ToString(value);
				}
			}
			public System.String TestCode
			{
				get
				{
					System.String data = entity.TestCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestCode = null;
					else entity.TestCode = Convert.ToString(value);
				}
			}
			public System.String TestName
			{
				get
				{
					System.String data = entity.TestName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestName = null;
					else entity.TestName = Convert.ToString(value);
				}
			}
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
			public System.String Flag
			{
				get
				{
					System.String data = entity.Flag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Flag = null;
					else entity.Flag = Convert.ToString(value);
				}
			}
			public System.String NormalResult
			{
				get
				{
					System.String data = entity.NormalResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalResult = null;
					else entity.NormalResult = Convert.ToString(value);
				}
			}
			public System.String TestComment
			{
				get
				{
					System.String data = entity.TestComment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestComment = null;
					else entity.TestComment = Convert.ToString(value);
				}
			}
			private esVwHasilLabMobileSysmex entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwHasilLabMobileSysmexQuery query)
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
				throw new Exception("esVwHasilLabMobileSysmex can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VwHasilLabMobileSysmex : esVwHasilLabMobileSysmex
	{	
	}

	[Serializable]
	abstract public class esVwHasilLabMobileSysmexQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return VwHasilLabMobileSysmexMetadata.Meta();
			}
		}	
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MedicalNo
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.MedicalNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationDate
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem TestGroup
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.TestGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem TestCode
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.TestCode, esSystemType.String);
			}
		} 
			
		public esQueryItem TestName
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.TestName, esSystemType.String);
			}
		} 
			
		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.Result, esSystemType.String);
			}
		} 
			
		public esQueryItem Flag
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.Flag, esSystemType.String);
			}
		} 
			
		public esQueryItem NormalResult
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.NormalResult, esSystemType.String);
			}
		} 
			
		public esQueryItem TestComment
		{
			get
			{
				return new esQueryItem(this, VwHasilLabMobileSysmexMetadata.ColumnNames.TestComment, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwHasilLabMobileSysmexCollection")]
	public partial class VwHasilLabMobileSysmexCollection : esVwHasilLabMobileSysmexCollection, IEnumerable< VwHasilLabMobileSysmex>
	{
		public VwHasilLabMobileSysmexCollection()
		{

		}	
		
		public static implicit operator List< VwHasilLabMobileSysmex>(VwHasilLabMobileSysmexCollection coll)
		{
			List< VwHasilLabMobileSysmex> list = new List< VwHasilLabMobileSysmex>();
			
			foreach (VwHasilLabMobileSysmex emp in coll)
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
				return  VwHasilLabMobileSysmexMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwHasilLabMobileSysmexQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwHasilLabMobileSysmex(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwHasilLabMobileSysmex();
		}
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public VwHasilLabMobileSysmexQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwHasilLabMobileSysmexQuery();
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
		public bool Load(VwHasilLabMobileSysmexQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VwHasilLabMobileSysmex AddNew()
		{
			VwHasilLabMobileSysmex entity = base.AddNewEntity() as VwHasilLabMobileSysmex;
			
			return entity;		
		}

		#region IEnumerable< VwHasilLabMobileSysmex> Members

		IEnumerator< VwHasilLabMobileSysmex> IEnumerable< VwHasilLabMobileSysmex>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwHasilLabMobileSysmex;
			}
		}

		#endregion
		
		private VwHasilLabMobileSysmexQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VwHasilLabMobileSysmex' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VwHasilLabMobileSysmex ()")]
	[Serializable]
	public partial class VwHasilLabMobileSysmex : esVwHasilLabMobileSysmex
	{
		public VwHasilLabMobileSysmex()
		{
		}	
	
		public VwHasilLabMobileSysmex(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwHasilLabMobileSysmexMetadata.Meta();
			}
		}	
	
		override protected esVwHasilLabMobileSysmexQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwHasilLabMobileSysmexQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public VwHasilLabMobileSysmexQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwHasilLabMobileSysmexQuery();
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
		public bool Load(VwHasilLabMobileSysmexQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private VwHasilLabMobileSysmexQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VwHasilLabMobileSysmexQuery : esVwHasilLabMobileSysmexQuery
	{
		public VwHasilLabMobileSysmexQuery()
		{

		}		
		
		public VwHasilLabMobileSysmexQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "VwHasilLabMobileSysmexQuery";
        }
	}

	[Serializable]
	public partial class VwHasilLabMobileSysmexMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwHasilLabMobileSysmexMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.SequenceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 1;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.MedicalNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.MedicalNo;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.RegistrationDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.RegistrationDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.TransactionDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.TransactionDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.TestGroup, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.TestGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.TestCode, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.TestCode;
			c.CharacterMaxLength = 6;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.TestName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.TestName;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.Result, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 4016;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.Flag, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.Flag;
			c.CharacterMaxLength = 6;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.NormalResult, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.NormalResult;
			c.CharacterMaxLength = 46;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwHasilLabMobileSysmexMetadata.ColumnNames.TestComment, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilLabMobileSysmexMetadata.PropertyNames.TestComment;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public VwHasilLabMobileSysmexMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string MedicalNo = "MedicalNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationDate = "RegistrationDate";
			public const string TransactionDate = "TransactionDate";
			public const string TestGroup = "TestGroup";
			public const string TestCode = "TestCode";
			public const string TestName = "TestName";
			public const string Result = "Result";
			public const string Flag = "Flag";
			public const string NormalResult = "NormalResult";
			public const string TestComment = "TestComment";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SequenceNo = "SequenceNo";
			public const string MedicalNo = "MedicalNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationDate = "RegistrationDate";
			public const string TransactionDate = "TransactionDate";
			public const string TestGroup = "TestGroup";
			public const string TestCode = "TestCode";
			public const string TestName = "TestName";
			public const string Result = "Result";
			public const string Flag = "Flag";
			public const string NormalResult = "NormalResult";
			public const string TestComment = "TestComment";
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
			lock (typeof(VwHasilLabMobileSysmexMetadata))
			{
				if(VwHasilLabMobileSysmexMetadata.mapDelegates == null)
				{
					VwHasilLabMobileSysmexMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwHasilLabMobileSysmexMetadata.meta == null)
				{
					VwHasilLabMobileSysmexMetadata.meta = new VwHasilLabMobileSysmexMetadata();
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
				
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MedicalNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TestGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Flag", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NormalResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestComment", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "vw_HasilLabMobileSysmex";
				meta.Destination = "vw_HasilLabMobileSysmex";
				meta.spInsert = "proc_vw_HasilLabMobileSysmexInsert";				
				meta.spUpdate = "proc_vw_HasilLabMobileSysmexUpdate";		
				meta.spDelete = "proc_vw_HasilLabMobileSysmexDelete";
				meta.spLoadAll = "proc_vw_HasilLabMobileSysmexLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_HasilLabMobileSysmexLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwHasilLabMobileSysmexMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
