/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/9/2022 11:01:09 AM
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
	abstract public class esQualityIndicatorSurveyDetailCollection : esEntityCollectionWAuditLog
	{
		public esQualityIndicatorSurveyDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "QualityIndicatorSurveyDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esQualityIndicatorSurveyDetailQuery query)
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
			this.InitQuery(query as esQualityIndicatorSurveyDetailQuery);
		}
		#endregion
			
		virtual public QualityIndicatorSurveyDetail DetachEntity(QualityIndicatorSurveyDetail entity)
		{
			return base.DetachEntity(entity) as QualityIndicatorSurveyDetail;
		}
		
		virtual public QualityIndicatorSurveyDetail AttachEntity(QualityIndicatorSurveyDetail entity)
		{
			return base.AttachEntity(entity) as QualityIndicatorSurveyDetail;
		}
		
		virtual public void Combine(QualityIndicatorSurveyDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QualityIndicatorSurveyDetail this[int index]
		{
			get
			{
				return base[index] as QualityIndicatorSurveyDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QualityIndicatorSurveyDetail);
		}
	}

	[Serializable]
	abstract public class esQualityIndicatorSurveyDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQualityIndicatorSurveyDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esQualityIndicatorSurveyDetail()
		{
		}
	
		public esQualityIndicatorSurveyDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 detailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailID);
			else
				return LoadByPrimaryKeyStoredProcedure(detailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 detailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailID);
			else
				return LoadByPrimaryKeyStoredProcedure(detailID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 detailID)
		{
			esQualityIndicatorSurveyDetailQuery query = this.GetDynamicQuery();
			query.Where(query.DetailID==detailID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 detailID)
		{
			esParameters parms = new esParameters();
			parms.Add("DetailID",detailID);
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
						case "DetailID": this.str.DetailID = (string)value; break;
						case "SurveyID": this.str.SurveyID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Numerator": this.str.Numerator = (string)value; break;
						case "InputQueryNumer": this.str.InputQueryNumer = (string)value; break;
						case "Denumerator": this.str.Denumerator = (string)value; break;
						case "InputQueryDenum": this.str.InputQueryDenum = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DetailID":
						
							if (value == null || value is System.Int32)
								this.DetailID = (System.Int32?)value;
							break;
						case "SurveyID":
						
							if (value == null || value is System.Int32)
								this.SurveyID = (System.Int32?)value;
							break;
						case "Numerator":
						
							if (value == null || value is System.Int32)
								this.Numerator = (System.Int32?)value;
							break;
						case "Denumerator":
						
							if (value == null || value is System.Int32)
								this.Denumerator = (System.Int32?)value;
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
		/// Maps to QualityIndicatorSurveyDetail.DetailID
		/// </summary>
		virtual public System.Int32? DetailID
		{
			get
			{
				return base.GetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.DetailID);
			}
			
			set
			{
				base.SetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.DetailID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.SurveyID
		/// </summary>
		virtual public System.Int32? SurveyID
		{
			get
			{
				return base.GetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.SurveyID);
			}
			
			set
			{
				base.SetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.SurveyID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.Numerator
		/// </summary>
		virtual public System.Int32? Numerator
		{
			get
			{
				return base.GetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.Numerator);
			}
			
			set
			{
				base.SetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.Numerator, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.InputQueryNumer
		/// </summary>
		virtual public System.String InputQueryNumer
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryNumer);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryNumer, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.Denumerator
		/// </summary>
		virtual public System.Int32? Denumerator
		{
			get
			{
				return base.GetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.Denumerator);
			}
			
			set
			{
				base.SetSystemInt32(QualityIndicatorSurveyDetailMetadata.ColumnNames.Denumerator, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.InputQueryDenum
		/// </summary>
		virtual public System.String InputQueryDenum
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryDenum);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryDenum, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QualityIndicatorSurveyDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esQualityIndicatorSurveyDetail entity)
			{
				this.entity = entity;
			}
			public System.String DetailID
			{
				get
				{
					System.Int32? data = entity.DetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailID = null;
					else entity.DetailID = Convert.ToInt32(value);
				}
			}
			public System.String SurveyID
			{
				get
				{
					System.Int32? data = entity.SurveyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SurveyID = null;
					else entity.SurveyID = Convert.ToInt32(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String Numerator
			{
				get
				{
					System.Int32? data = entity.Numerator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Numerator = null;
					else entity.Numerator = Convert.ToInt32(value);
				}
			}
			public System.String InputQueryNumer
			{
				get
				{
					System.String data = entity.InputQueryNumer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InputQueryNumer = null;
					else entity.InputQueryNumer = Convert.ToString(value);
				}
			}
			public System.String Denumerator
			{
				get
				{
					System.Int32? data = entity.Denumerator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Denumerator = null;
					else entity.Denumerator = Convert.ToInt32(value);
				}
			}
			public System.String InputQueryDenum
			{
				get
				{
					System.String data = entity.InputQueryDenum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InputQueryDenum = null;
					else entity.InputQueryDenum = Convert.ToString(value);
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
			private esQualityIndicatorSurveyDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQualityIndicatorSurveyDetailQuery query)
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
				throw new Exception("esQualityIndicatorSurveyDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class QualityIndicatorSurveyDetail : esQualityIndicatorSurveyDetail
	{	
	}

	[Serializable]
	abstract public class esQualityIndicatorSurveyDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return QualityIndicatorSurveyDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem DetailID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.DetailID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SurveyID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.SurveyID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem Numerator
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.Numerator, esSystemType.Int32);
			}
		} 
			
		public esQueryItem InputQueryNumer
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryNumer, esSystemType.String);
			}
		} 
			
		public esQueryItem Denumerator
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.Denumerator, esSystemType.Int32);
			}
		} 
			
		public esQueryItem InputQueryDenum
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryDenum, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QualityIndicatorSurveyDetailCollection")]
	public partial class QualityIndicatorSurveyDetailCollection : esQualityIndicatorSurveyDetailCollection, IEnumerable< QualityIndicatorSurveyDetail>
	{
		public QualityIndicatorSurveyDetailCollection()
		{

		}	
		
		public static implicit operator List< QualityIndicatorSurveyDetail>(QualityIndicatorSurveyDetailCollection coll)
		{
			List< QualityIndicatorSurveyDetail> list = new List< QualityIndicatorSurveyDetail>();
			
			foreach (QualityIndicatorSurveyDetail emp in coll)
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
				return  QualityIndicatorSurveyDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QualityIndicatorSurveyDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QualityIndicatorSurveyDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QualityIndicatorSurveyDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public QualityIndicatorSurveyDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QualityIndicatorSurveyDetailQuery();
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
		public bool Load(QualityIndicatorSurveyDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public QualityIndicatorSurveyDetail AddNew()
		{
			QualityIndicatorSurveyDetail entity = base.AddNewEntity() as QualityIndicatorSurveyDetail;
			
			return entity;		
		}
		public QualityIndicatorSurveyDetail FindByPrimaryKey(Int32 detailID)
		{
			return base.FindByPrimaryKey(detailID) as QualityIndicatorSurveyDetail;
		}

		#region IEnumerable< QualityIndicatorSurveyDetail> Members

		IEnumerator< QualityIndicatorSurveyDetail> IEnumerable< QualityIndicatorSurveyDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QualityIndicatorSurveyDetail;
			}
		}

		#endregion
		
		private QualityIndicatorSurveyDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QualityIndicatorSurveyDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("QualityIndicatorSurveyDetail ({DetailID})")]
	[Serializable]
	public partial class QualityIndicatorSurveyDetail : esQualityIndicatorSurveyDetail
	{
		public QualityIndicatorSurveyDetail()
		{
		}	
	
		public QualityIndicatorSurveyDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QualityIndicatorSurveyDetailMetadata.Meta();
			}
		}	
	
		override protected esQualityIndicatorSurveyDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QualityIndicatorSurveyDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public QualityIndicatorSurveyDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QualityIndicatorSurveyDetailQuery();
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
		public bool Load(QualityIndicatorSurveyDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private QualityIndicatorSurveyDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class QualityIndicatorSurveyDetailQuery : esQualityIndicatorSurveyDetailQuery
	{
		public QualityIndicatorSurveyDetailQuery()
		{

		}		
		
		public QualityIndicatorSurveyDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "QualityIndicatorSurveyDetailQuery";
        }
	}

	[Serializable]
	public partial class QualityIndicatorSurveyDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QualityIndicatorSurveyDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.DetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.DetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.SurveyID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.SurveyID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.Numerator, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.Numerator;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryNumer, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.InputQueryNumer;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.Denumerator, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.Denumerator;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.InputQueryDenum, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.InputQueryDenum;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.CreateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QualityIndicatorSurveyDetailMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QualityIndicatorSurveyDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public QualityIndicatorSurveyDetailMetadata Meta()
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
			public const string DetailID = "DetailID";
			public const string SurveyID = "SurveyID";
			public const string ItemID = "ItemID";
			public const string Numerator = "Numerator";
			public const string InputQueryNumer = "InputQueryNumer";
			public const string Denumerator = "Denumerator";
			public const string InputQueryDenum = "InputQueryDenum";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string DetailID = "DetailID";
			public const string SurveyID = "SurveyID";
			public const string ItemID = "ItemID";
			public const string Numerator = "Numerator";
			public const string InputQueryNumer = "InputQueryNumer";
			public const string Denumerator = "Denumerator";
			public const string InputQueryDenum = "InputQueryDenum";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
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
			lock (typeof(QualityIndicatorSurveyDetailMetadata))
			{
				if(QualityIndicatorSurveyDetailMetadata.mapDelegates == null)
				{
					QualityIndicatorSurveyDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QualityIndicatorSurveyDetailMetadata.meta == null)
				{
					QualityIndicatorSurveyDetailMetadata.meta = new QualityIndicatorSurveyDetailMetadata();
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
				
				meta.AddTypeMap("DetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SurveyID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Numerator", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InputQueryNumer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Denumerator", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InputQueryDenum", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "QualityIndicatorSurveyDetail";
				meta.Destination = "QualityIndicatorSurveyDetail";
				meta.spInsert = "proc_QualityIndicatorSurveyDetailInsert";				
				meta.spUpdate = "proc_QualityIndicatorSurveyDetailUpdate";		
				meta.spDelete = "proc_QualityIndicatorSurveyDetailDelete";
				meta.spLoadAll = "proc_QualityIndicatorSurveyDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_QualityIndicatorSurveyDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QualityIndicatorSurveyDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
