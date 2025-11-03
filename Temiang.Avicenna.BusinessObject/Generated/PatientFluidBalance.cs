/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/04/19 1:22:58 PM
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
	abstract public class esPatientFluidBalanceCollection : esEntityCollectionWAuditLog
	{
		public esPatientFluidBalanceCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientFluidBalanceCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientFluidBalanceQuery query)
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
			this.InitQuery(query as esPatientFluidBalanceQuery);
		}
		#endregion
			
		virtual public PatientFluidBalance DetachEntity(PatientFluidBalance entity)
		{
			return base.DetachEntity(entity) as PatientFluidBalance;
		}
		
		virtual public PatientFluidBalance AttachEntity(PatientFluidBalance entity)
		{
			return base.AttachEntity(entity) as PatientFluidBalance;
		}
		
		virtual public void Combine(PatientFluidBalanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientFluidBalance this[int index]
		{
			get
			{
				return base[index] as PatientFluidBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientFluidBalance);
		}
	}

	[Serializable]
	abstract public class esPatientFluidBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientFluidBalanceQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientFluidBalance()
		{
		}
	
		public esPatientFluidBalance(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 sequenceNo)
		{
			esPatientFluidBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("SequenceNo",sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "InOutDate": this.str.InOutDate = (string)value; break;
						case "SchemaInfus": this.str.SchemaInfus = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastTemp": this.str.LastTemp = (string)value; break;
						case "IwlForHour": this.str.IwlForHour = (string)value; break;
						case "BodyWeight": this.str.BodyWeight = (string)value; break;
						case "NormalTemp": this.str.NormalTemp = (string)value; break;
                        case "IwlConstant": this.str.IwlConstant = (string)value; break;
                    }
				}
				else
				{
					switch (name)
					{	
						case "SequenceNo":
						
							if (value == null || value is System.Int32)
								this.SequenceNo = (System.Int32?)value;
							break;
						case "InOutDate":
						
							if (value == null || value is System.DateTime)
								this.InOutDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LastTemp":
						
							if (value == null || value is System.Decimal)
								this.LastTemp = (System.Decimal?)value;
							break;
						case "IwlForHour":
						
							if (value == null || value is System.Int32)
								this.IwlForHour = (System.Int32?)value;
							break;
						case "BodyWeight":
						
							if (value == null || value is System.Decimal)
								this.BodyWeight = (System.Decimal?)value;
							break;
						case "NormalTemp":
						
							if (value == null || value is System.Decimal)
								this.NormalTemp = (System.Decimal?)value;
							break;
                        case "IwlConstant":

                            if (value == null || value is System.Int32)
                                this.IwlConstant = (System.Int32?)value;
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
		/// Maps to PatientFluidBalance.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientFluidBalanceMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(PatientFluidBalanceMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(PatientFluidBalanceMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.InOutDate
		/// </summary>
		virtual public System.DateTime? InOutDate
		{
			get
			{
				return base.GetSystemDateTime(PatientFluidBalanceMetadata.ColumnNames.InOutDate);
			}
			
			set
			{
				base.SetSystemDateTime(PatientFluidBalanceMetadata.ColumnNames.InOutDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.SchemaInfus
		/// </summary>
		virtual public System.String SchemaInfus
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceMetadata.ColumnNames.SchemaInfus);
			}
			
			set
			{
				base.SetSystemString(PatientFluidBalanceMetadata.ColumnNames.SchemaInfus, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientFluidBalanceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientFluidBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientFluidBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.LastTemp
		/// </summary>
		virtual public System.Decimal? LastTemp
		{
			get
			{
				return base.GetSystemDecimal(PatientFluidBalanceMetadata.ColumnNames.LastTemp);
			}
			
			set
			{
				base.SetSystemDecimal(PatientFluidBalanceMetadata.ColumnNames.LastTemp, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.IwlForHour
		/// </summary>
		virtual public System.Int32? IwlForHour
		{
			get
			{
				return base.GetSystemInt32(PatientFluidBalanceMetadata.ColumnNames.IwlForHour);
			}
			
			set
			{
				base.SetSystemInt32(PatientFluidBalanceMetadata.ColumnNames.IwlForHour, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.BodyWeight
		/// </summary>
		virtual public System.Decimal? BodyWeight
		{
			get
			{
				return base.GetSystemDecimal(PatientFluidBalanceMetadata.ColumnNames.BodyWeight);
			}
			
			set
			{
				base.SetSystemDecimal(PatientFluidBalanceMetadata.ColumnNames.BodyWeight, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalance.NormalTemp
		/// </summary>
		virtual public System.Decimal? NormalTemp
		{
			get
			{
				return base.GetSystemDecimal(PatientFluidBalanceMetadata.ColumnNames.NormalTemp);
			}
			
			set
			{
				base.SetSystemDecimal(PatientFluidBalanceMetadata.ColumnNames.NormalTemp, value);
			}
		}
        /// <summary>
        /// Maps to PatientFluidBalance.IwlConstant
        /// </summary>
        virtual public System.Int32? IwlConstant
        {
            get
            {
                return base.GetSystemInt32(PatientFluidBalanceMetadata.ColumnNames.IwlConstant);
            }

            set
            {
                base.SetSystemInt32(PatientFluidBalanceMetadata.ColumnNames.IwlConstant, value);
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
			public esStrings(esPatientFluidBalance entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.Int32? data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToInt32(value);
				}
			}
			public System.String InOutDate
			{
				get
				{
					System.DateTime? data = entity.InOutDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InOutDate = null;
					else entity.InOutDate = Convert.ToDateTime(value);
				}
			}
			public System.String SchemaInfus
			{
				get
				{
					System.String data = entity.SchemaInfus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchemaInfus = null;
					else entity.SchemaInfus = Convert.ToString(value);
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
			public System.String LastTemp
			{
				get
				{
					System.Decimal? data = entity.LastTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastTemp = null;
					else entity.LastTemp = Convert.ToDecimal(value);
				}
			}
			public System.String IwlForHour
			{
				get
				{
					System.Int32? data = entity.IwlForHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IwlForHour = null;
					else entity.IwlForHour = Convert.ToInt32(value);
				}
			}
			public System.String BodyWeight
			{
				get
				{
					System.Decimal? data = entity.BodyWeight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyWeight = null;
					else entity.BodyWeight = Convert.ToDecimal(value);
				}
			}
			public System.String NormalTemp
			{
				get
				{
					System.Decimal? data = entity.NormalTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalTemp = null;
					else entity.NormalTemp = Convert.ToDecimal(value);
				}
			}
            public System.String IwlConstant
            {
                get
                {
                    System.Int32? data = entity.IwlConstant;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IwlConstant = null;
                    else entity.IwlConstant = Convert.ToInt32(value);
                }
            }
            private esPatientFluidBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientFluidBalanceQuery query)
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
				throw new Exception("esPatientFluidBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientFluidBalance : esPatientFluidBalance
	{	
	}

	[Serializable]
	abstract public class esPatientFluidBalanceQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientFluidBalanceMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem InOutDate
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.InOutDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SchemaInfus
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.SchemaInfus, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastTemp
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.LastTemp, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IwlForHour
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.IwlForHour, esSystemType.Int32);
			}
		} 
			
		public esQueryItem BodyWeight
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.BodyWeight, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem NormalTemp
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.NormalTemp, esSystemType.Decimal);
			}
		}

        public esQueryItem IwlConstant
        {
            get
            {
                return new esQueryItem(this, PatientFluidBalanceMetadata.ColumnNames.IwlConstant, esSystemType.Int32);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientFluidBalanceCollection")]
	public partial class PatientFluidBalanceCollection : esPatientFluidBalanceCollection, IEnumerable< PatientFluidBalance>
	{
		public PatientFluidBalanceCollection()
		{

		}	
		
		public static implicit operator List< PatientFluidBalance>(PatientFluidBalanceCollection coll)
		{
			List< PatientFluidBalance> list = new List< PatientFluidBalance>();
			
			foreach (PatientFluidBalance emp in coll)
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
				return  PatientFluidBalanceMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFluidBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientFluidBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientFluidBalance();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientFluidBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFluidBalanceQuery();
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
		public bool Load(PatientFluidBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientFluidBalance AddNew()
		{
			PatientFluidBalance entity = base.AddNewEntity() as PatientFluidBalance;
			
			return entity;		
		}
		public PatientFluidBalance FindByPrimaryKey(String registrationNo, Int32 sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo) as PatientFluidBalance;
		}

		#region IEnumerable< PatientFluidBalance> Members

		IEnumerator< PatientFluidBalance> IEnumerable< PatientFluidBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientFluidBalance;
			}
		}

		#endregion
		
		private PatientFluidBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientFluidBalance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientFluidBalance ({RegistrationNo, SequenceNo})")]
	[Serializable]
	public partial class PatientFluidBalance : esPatientFluidBalance
	{
		public PatientFluidBalance()
		{
		}	
	
		public PatientFluidBalance(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientFluidBalanceMetadata.Meta();
			}
		}	
	
		override protected esPatientFluidBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFluidBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientFluidBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFluidBalanceQuery();
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
		public bool Load(PatientFluidBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientFluidBalanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientFluidBalanceQuery : esPatientFluidBalanceQuery
	{
		public PatientFluidBalanceQuery()
		{

		}		
		
		public PatientFluidBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientFluidBalanceQuery";
        }
	}

	[Serializable]
	public partial class PatientFluidBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientFluidBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.InOutDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.InOutDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.SchemaInfus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.SchemaInfus;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.LastTemp, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.LastTemp;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.IwlForHour, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.IwlForHour;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.BodyWeight, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.BodyWeight;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.NormalTemp, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.NormalTemp;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(PatientFluidBalanceMetadata.ColumnNames.IwlConstant, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientFluidBalanceMetadata.PropertyNames.IwlConstant;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
		#endregion
	
		static public PatientFluidBalanceMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string InOutDate = "InOutDate";
			public const string SchemaInfus = "SchemaInfus";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastTemp = "LastTemp";
			public const string IwlForHour = "IwlForHour";
			public const string BodyWeight = "BodyWeight";
			public const string NormalTemp = "NormalTemp";
            public const string IwlConstant = "IwlConstant";
        }
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string SequenceNo = "SequenceNo";
			public const string InOutDate = "InOutDate";
			public const string SchemaInfus = "SchemaInfus";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastTemp = "LastTemp";
			public const string IwlForHour = "IwlForHour";
			public const string BodyWeight = "BodyWeight";
			public const string NormalTemp = "NormalTemp";
            public const string IwlConstant = "IwlConstant";
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
			lock (typeof(PatientFluidBalanceMetadata))
			{
				if(PatientFluidBalanceMetadata.mapDelegates == null)
				{
					PatientFluidBalanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientFluidBalanceMetadata.meta == null)
				{
					PatientFluidBalanceMetadata.meta = new PatientFluidBalanceMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InOutDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("SchemaInfus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastTemp", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IwlForHour", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BodyWeight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NormalTemp", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IwlConstant", new esTypeMap("int", "System.Int32"));


                meta.Source = "PatientFluidBalance";
				meta.Destination = "PatientFluidBalance";
				meta.spInsert = "proc_PatientFluidBalanceInsert";				
				meta.spUpdate = "proc_PatientFluidBalanceUpdate";		
				meta.spDelete = "proc_PatientFluidBalanceDelete";
				meta.spLoadAll = "proc_PatientFluidBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientFluidBalanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientFluidBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
