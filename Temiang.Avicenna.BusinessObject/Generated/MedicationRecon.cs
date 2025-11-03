/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/13/2023 7:06:04 AM
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
	abstract public class esMedicationReconCollection : esEntityCollectionWAuditLog
	{
		public esMedicationReconCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicationReconCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicationReconQuery query)
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
			this.InitQuery(query as esMedicationReconQuery);
		}
		#endregion

		virtual public MedicationRecon DetachEntity(MedicationRecon entity)
		{
			return base.DetachEntity(entity) as MedicationRecon;
		}

		virtual public MedicationRecon AttachEntity(MedicationRecon entity)
		{
			return base.AttachEntity(entity) as MedicationRecon;
		}

		virtual public void Combine(MedicationReconCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicationRecon this[int index]
		{
			get
			{
				return base[index] as MedicationRecon;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicationRecon);
		}
	}

	[Serializable]
	abstract public class esMedicationRecon : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicationReconQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicationRecon()
		{
		}

		public esMedicationRecon(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 reconSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, reconSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, reconSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 reconSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, reconSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, reconSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 reconSeqNo)
		{
			esMedicationReconQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.ReconSeqNo == reconSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 reconSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("ReconSeqNo", reconSeqNo);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ReconSeqNo": this.str.ReconSeqNo = (string)value; break;
						case "ReconType": this.str.ReconType = (string)value; break;
						case "BodyWeight": this.str.BodyWeight = (string)value; break;
						case "TransferNo": this.str.TransferNo = (string)value; break;
						case "IsFinish": this.str.IsFinish = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReconSeqNo":

							if (value == null || value is System.Int32)
								this.ReconSeqNo = (System.Int32?)value;
							break;
						case "BodyWeight":

							if (value == null || value is System.Decimal)
								this.BodyWeight = (System.Decimal?)value;
							break;
						case "IsFinish":

							if (value == null || value is System.Boolean)
								this.IsFinish = (System.Boolean?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SignImg":

							if (value == null || value is System.Byte[])
								this.SignImg = (System.Byte[])value;
							break;
						case "ParamedicSignImg":

							if (value == null || value is System.Byte[])
								this.ParamedicSignImg = (System.Byte[])value;
							break;
						case "PatientSignImg":

							if (value == null || value is System.Byte[])
								this.PatientSignImg = (System.Byte[])value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to MedicationRecon.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicationReconMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MedicationReconMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.ReconSeqNo
		/// </summary>
		virtual public System.Int32? ReconSeqNo
		{
			get
			{
				return base.GetSystemInt32(MedicationReconMetadata.ColumnNames.ReconSeqNo);
			}

			set
			{
				base.SetSystemInt32(MedicationReconMetadata.ColumnNames.ReconSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.ReconType
		/// </summary>
		virtual public System.String ReconType
		{
			get
			{
				return base.GetSystemString(MedicationReconMetadata.ColumnNames.ReconType);
			}

			set
			{
				base.SetSystemString(MedicationReconMetadata.ColumnNames.ReconType, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.BodyWeight
		/// </summary>
		virtual public System.Decimal? BodyWeight
		{
			get
			{
				return base.GetSystemDecimal(MedicationReconMetadata.ColumnNames.BodyWeight);
			}

			set
			{
				base.SetSystemDecimal(MedicationReconMetadata.ColumnNames.BodyWeight, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.TransferNo
		/// </summary>
		virtual public System.String TransferNo
		{
			get
			{
				return base.GetSystemString(MedicationReconMetadata.ColumnNames.TransferNo);
			}

			set
			{
				base.SetSystemString(MedicationReconMetadata.ColumnNames.TransferNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.IsFinish
		/// </summary>
		virtual public System.Boolean? IsFinish
		{
			get
			{
				return base.GetSystemBoolean(MedicationReconMetadata.ColumnNames.IsFinish);
			}

			set
			{
				base.SetSystemBoolean(MedicationReconMetadata.ColumnNames.IsFinish, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReconMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReconMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReconMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(MedicationReconMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReconMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReconMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReconMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicationReconMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.SignImg
		/// </summary>
		virtual public System.Byte[] SignImg
		{
			get
			{
				return base.GetSystemByteArray(MedicationReconMetadata.ColumnNames.SignImg);
			}

			set
			{
				base.SetSystemByteArray(MedicationReconMetadata.ColumnNames.SignImg, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.ParamedicSignImg
		/// </summary>
		virtual public System.Byte[] ParamedicSignImg
		{
			get
			{
				return base.GetSystemByteArray(MedicationReconMetadata.ColumnNames.ParamedicSignImg);
			}

			set
			{
				base.SetSystemByteArray(MedicationReconMetadata.ColumnNames.ParamedicSignImg, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.PatientSignImg
		/// </summary>
		virtual public System.Byte[] PatientSignImg
		{
			get
			{
				return base.GetSystemByteArray(MedicationReconMetadata.ColumnNames.PatientSignImg);
			}

			set
			{
				base.SetSystemByteArray(MedicationReconMetadata.ColumnNames.PatientSignImg, value);
			}
		}
		/// <summary>
		/// Maps to MedicationRecon.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(MedicationReconMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(MedicationReconMetadata.ColumnNames.ParamedicID, value);
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
		[BrowsableAttribute(false)]
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
			public esStrings(esMedicationRecon entity)
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
			public System.String ReconSeqNo
			{
				get
				{
					System.Int32? data = entity.ReconSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReconSeqNo = null;
					else entity.ReconSeqNo = Convert.ToInt32(value);
				}
			}
			public System.String ReconType
			{
				get
				{
					System.String data = entity.ReconType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReconType = null;
					else entity.ReconType = Convert.ToString(value);
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
			public System.String TransferNo
			{
				get
				{
					System.String data = entity.TransferNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransferNo = null;
					else entity.TransferNo = Convert.ToString(value);
				}
			}
			public System.String IsFinish
			{
				get
				{
					System.Boolean? data = entity.IsFinish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFinish = null;
					else entity.IsFinish = Convert.ToBoolean(value);
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
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			private esMedicationRecon entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicationReconQuery query)
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
				throw new Exception("esMedicationRecon can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicationRecon : esMedicationRecon
	{
	}

	[Serializable]
	abstract public class esMedicationReconQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicationReconMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ReconSeqNo
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.ReconSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem ReconType
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.ReconType, esSystemType.String);
			}
		}

		public esQueryItem BodyWeight
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.BodyWeight, esSystemType.Decimal);
			}
		}

		public esQueryItem TransferNo
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.TransferNo, esSystemType.String);
			}
		}

		public esQueryItem IsFinish
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.IsFinish, esSystemType.Boolean);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SignImg
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.SignImg, esSystemType.ByteArray);
			}
		}

		public esQueryItem ParamedicSignImg
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.ParamedicSignImg, esSystemType.ByteArray);
			}
		}

		public esQueryItem PatientSignImg
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.PatientSignImg, esSystemType.ByteArray);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, MedicationReconMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicationReconCollection")]
	public partial class MedicationReconCollection : esMedicationReconCollection, IEnumerable<MedicationRecon>
	{
		public MedicationReconCollection()
		{

		}

		public static implicit operator List<MedicationRecon>(MedicationReconCollection coll)
		{
			List<MedicationRecon> list = new List<MedicationRecon>();

			foreach (MedicationRecon emp in coll)
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
				return MedicationReconMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReconQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicationRecon(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicationRecon();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicationReconQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReconQuery();
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
		public bool Load(MedicationReconQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicationRecon AddNew()
		{
			MedicationRecon entity = base.AddNewEntity() as MedicationRecon;

			return entity;
		}
		public MedicationRecon FindByPrimaryKey(String registrationNo, Int32 reconSeqNo)
		{
			return base.FindByPrimaryKey(registrationNo, reconSeqNo) as MedicationRecon;
		}

		#region IEnumerable< MedicationRecon> Members

		IEnumerator<MedicationRecon> IEnumerable<MedicationRecon>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicationRecon;
			}
		}

		#endregion

		private MedicationReconQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicationRecon' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicationRecon ({RegistrationNo, ReconSeqNo})")]
	[Serializable]
	public partial class MedicationRecon : esMedicationRecon
	{
		public MedicationRecon()
		{
		}

		public MedicationRecon(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReconMetadata.Meta();
			}
		}

		override protected esMedicationReconQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReconQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicationReconQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReconQuery();
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
		public bool Load(MedicationReconQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicationReconQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicationReconQuery : esMedicationReconQuery
	{
		public MedicationReconQuery()
		{

		}

		public MedicationReconQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicationReconQuery";
		}
	}

	[Serializable]
	public partial class MedicationReconMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicationReconMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.ReconSeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicationReconMetadata.PropertyNames.ReconSeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.ReconType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconMetadata.PropertyNames.ReconType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.BodyWeight, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicationReconMetadata.PropertyNames.BodyWeight;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.TransferNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconMetadata.PropertyNames.TransferNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.IsFinish, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReconMetadata.PropertyNames.IsFinish;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReconMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.CreateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReconMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.SignImg, 10, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = MedicationReconMetadata.PropertyNames.SignImg;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.ParamedicSignImg, 11, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = MedicationReconMetadata.PropertyNames.ParamedicSignImg;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.PatientSignImg, 12, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = MedicationReconMetadata.PropertyNames.PatientSignImg;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconMetadata.ColumnNames.ParamedicID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicationReconMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string ReconSeqNo = "ReconSeqNo";
			public const string ReconType = "ReconType";
			public const string BodyWeight = "BodyWeight";
			public const string TransferNo = "TransferNo";
			public const string IsFinish = "IsFinish";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SignImg = "SignImg";
			public const string ParamedicSignImg = "ParamedicSignImg";
			public const string PatientSignImg = "PatientSignImg";
			public const string ParamedicID = "ParamedicID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string ReconSeqNo = "ReconSeqNo";
			public const string ReconType = "ReconType";
			public const string BodyWeight = "BodyWeight";
			public const string TransferNo = "TransferNo";
			public const string IsFinish = "IsFinish";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SignImg = "SignImg";
			public const string ParamedicSignImg = "ParamedicSignImg";
			public const string PatientSignImg = "PatientSignImg";
			public const string ParamedicID = "ParamedicID";
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
			lock (typeof(MedicationReconMetadata))
			{
				if (MedicationReconMetadata.mapDelegates == null)
				{
					MedicationReconMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicationReconMetadata.meta == null)
				{
					MedicationReconMetadata.meta = new MedicationReconMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReconSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ReconType", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("BodyWeight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TransferNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFinish", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignImg", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("ParamedicSignImg", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("PatientSignImg", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicationRecon";
				meta.Destination = "MedicationRecon";
				meta.spInsert = "proc_MedicationReconInsert";
				meta.spUpdate = "proc_MedicationReconUpdate";
				meta.spDelete = "proc_MedicationReconDelete";
				meta.spLoadAll = "proc_MedicationReconLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicationReconLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicationReconMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
