/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/10/2022 7:31:51 PM
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
	abstract public class esEmployeeRL4Collection : esEntityCollectionWAuditLog
	{
		public esEmployeeRL4Collection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeRL4Collection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeRL4Query query)
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
			this.InitQuery(query as esEmployeeRL4Query);
		}
		#endregion

		virtual public EmployeeRL4 DetachEntity(EmployeeRL4 entity)
		{
			return base.DetachEntity(entity) as EmployeeRL4;
		}

		virtual public EmployeeRL4 AttachEntity(EmployeeRL4 entity)
		{
			return base.AttachEntity(entity) as EmployeeRL4;
		}

		virtual public void Combine(EmployeeRL4Collection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeRL4 this[int index]
		{
			get
			{
				return base[index] as EmployeeRL4;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeRL4);
		}
	}

	[Serializable]
	abstract public class esEmployeeRL4 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeRL4Query GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeRL4()
		{
		}

		public esEmployeeRL4(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeRL4ID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeRL4ID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeRL4ID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeRL4ID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeRL4ID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeRL4ID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeRL4ID)
		{
			esEmployeeRL4Query query = this.GetDynamicQuery();
			query.Where(query.EmployeeRL4ID == employeeRL4ID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeRL4ID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeRL4ID", employeeRL4ID);
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
						case "EmployeeRL4ID": this.str.EmployeeRL4ID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "CompanyEducationProfileID": this.str.CompanyEducationProfileID = (string)value; break;
						case "CompanyFieldOfWorkProfileID": this.str.CompanyFieldOfWorkProfileID = (string)value; break;
						case "SRRL4Status": this.str.SRRL4Status = (string)value; break;
						case "SRRL4Type": this.str.SRRL4Type = (string)value; break;
						case "SRMedisType": this.str.SRMedisType = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "RL4EducationID": this.str.RL4EducationID = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRRL4ProfessionType": this.str.SRRL4ProfessionType = (string)value; break;
						case "SRRL4EducationLevel": this.str.SRRL4EducationLevel = (string)value; break;
						case "SRRL4EducationMajor": this.str.SRRL4EducationMajor = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeRL4ID":

							if (value == null || value is System.Int32)
								this.EmployeeRL4ID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "CompanyEducationProfileID":

							if (value == null || value is System.Int32)
								this.CompanyEducationProfileID = (System.Int32?)value;
							break;
						case "CompanyFieldOfWorkProfileID":

							if (value == null || value is System.Int32)
								this.CompanyFieldOfWorkProfileID = (System.Int32?)value;
							break;
						case "RL4EducationID":

							if (value == null || value is System.Int32)
								this.RL4EducationID = (System.Int32?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to EmployeeRL4.EmployeeRL4ID
		/// </summary>
		virtual public System.Int32? EmployeeRL4ID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRL4Metadata.ColumnNames.EmployeeRL4ID);
			}

			set
			{
				base.SetSystemInt32(EmployeeRL4Metadata.ColumnNames.EmployeeRL4ID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRL4Metadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeRL4Metadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.CompanyEducationProfileID
		/// </summary>
		virtual public System.Int32? CompanyEducationProfileID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRL4Metadata.ColumnNames.CompanyEducationProfileID);
			}

			set
			{
				base.SetSystemInt32(EmployeeRL4Metadata.ColumnNames.CompanyEducationProfileID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.CompanyFieldOfWorkProfileID
		/// </summary>
		virtual public System.Int32? CompanyFieldOfWorkProfileID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRL4Metadata.ColumnNames.CompanyFieldOfWorkProfileID);
			}

			set
			{
				base.SetSystemInt32(EmployeeRL4Metadata.ColumnNames.CompanyFieldOfWorkProfileID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.SRRL4Status
		/// </summary>
		virtual public System.String SRRL4Status
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4Status);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4Status, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.SRRL4Type
		/// </summary>
		virtual public System.String SRRL4Type
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4Type);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4Type, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.SRMedisType
		/// </summary>
		virtual public System.String SRMedisType
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.SRMedisType);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.SRMedisType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.RL4EducationID
		/// </summary>
		virtual public System.Int32? RL4EducationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRL4Metadata.ColumnNames.RL4EducationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeRL4Metadata.ColumnNames.RL4EducationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EmployeeRL4Metadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(EmployeeRL4Metadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRL4Metadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeRL4Metadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRL4Metadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeeRL4Metadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRL4Metadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeRL4Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.SRRL4ProfessionType
		/// </summary>
		virtual public System.String SRRL4ProfessionType
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4ProfessionType);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4ProfessionType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.SRRL4EducationLevel
		/// </summary>
		virtual public System.String SRRL4EducationLevel
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4EducationLevel);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4EducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRL4.SRRL4EducationMajor
		/// </summary>
		virtual public System.String SRRL4EducationMajor
		{
			get
			{
				return base.GetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4EducationMajor);
			}

			set
			{
				base.SetSystemString(EmployeeRL4Metadata.ColumnNames.SRRL4EducationMajor, value);
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
			public esStrings(esEmployeeRL4 entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeRL4ID
			{
				get
				{
					System.Int32? data = entity.EmployeeRL4ID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeRL4ID = null;
					else entity.EmployeeRL4ID = Convert.ToInt32(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String CompanyEducationProfileID
			{
				get
				{
					System.Int32? data = entity.CompanyEducationProfileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyEducationProfileID = null;
					else entity.CompanyEducationProfileID = Convert.ToInt32(value);
				}
			}
			public System.String CompanyFieldOfWorkProfileID
			{
				get
				{
					System.Int32? data = entity.CompanyFieldOfWorkProfileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyFieldOfWorkProfileID = null;
					else entity.CompanyFieldOfWorkProfileID = Convert.ToInt32(value);
				}
			}
			public System.String SRRL4Status
			{
				get
				{
					System.String data = entity.SRRL4Status;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRL4Status = null;
					else entity.SRRL4Status = Convert.ToString(value);
				}
			}
			public System.String SRRL4Type
			{
				get
				{
					System.String data = entity.SRRL4Type;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRL4Type = null;
					else entity.SRRL4Type = Convert.ToString(value);
				}
			}
			public System.String SRMedisType
			{
				get
				{
					System.String data = entity.SRMedisType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedisType = null;
					else entity.SRMedisType = Convert.ToString(value);
				}
			}
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
				}
			}
			public System.String RL4EducationID
			{
				get
				{
					System.Int32? data = entity.RL4EducationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RL4EducationID = null;
					else entity.RL4EducationID = Convert.ToInt32(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
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
			public System.String SRRL4ProfessionType
			{
				get
				{
					System.String data = entity.SRRL4ProfessionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRL4ProfessionType = null;
					else entity.SRRL4ProfessionType = Convert.ToString(value);
				}
			}
			public System.String SRRL4EducationLevel
			{
				get
				{
					System.String data = entity.SRRL4EducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRL4EducationLevel = null;
					else entity.SRRL4EducationLevel = Convert.ToString(value);
				}
			}
			public System.String SRRL4EducationMajor
			{
				get
				{
					System.String data = entity.SRRL4EducationMajor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRL4EducationMajor = null;
					else entity.SRRL4EducationMajor = Convert.ToString(value);
				}
			}
			private esEmployeeRL4 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeRL4Query query)
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
				throw new Exception("esEmployeeRL4 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeRL4 : esEmployeeRL4
	{
	}

	[Serializable]
	abstract public class esEmployeeRL4Query : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeRL4Metadata.Meta();
			}
		}

		public esQueryItem EmployeeRL4ID
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.EmployeeRL4ID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem CompanyEducationProfileID
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.CompanyEducationProfileID, esSystemType.Int32);
			}
		}

		public esQueryItem CompanyFieldOfWorkProfileID
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.CompanyFieldOfWorkProfileID, esSystemType.Int32);
			}
		}

		public esQueryItem SRRL4Status
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.SRRL4Status, esSystemType.String);
			}
		}

		public esQueryItem SRRL4Type
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.SRRL4Type, esSystemType.String);
			}
		}

		public esQueryItem SRMedisType
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.SRMedisType, esSystemType.String);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem RL4EducationID
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.RL4EducationID, esSystemType.Int32);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRRL4ProfessionType
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.SRRL4ProfessionType, esSystemType.String);
			}
		}

		public esQueryItem SRRL4EducationLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.SRRL4EducationLevel, esSystemType.String);
			}
		}

		public esQueryItem SRRL4EducationMajor
		{
			get
			{
				return new esQueryItem(this, EmployeeRL4Metadata.ColumnNames.SRRL4EducationMajor, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeRL4Collection")]
	public partial class EmployeeRL4Collection : esEmployeeRL4Collection, IEnumerable<EmployeeRL4>
	{
		public EmployeeRL4Collection()
		{

		}

		public static implicit operator List<EmployeeRL4>(EmployeeRL4Collection coll)
		{
			List<EmployeeRL4> list = new List<EmployeeRL4>();

			foreach (EmployeeRL4 emp in coll)
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
				return EmployeeRL4Metadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeRL4Query();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeRL4(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeRL4();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeRL4Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeRL4Query();
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
		public bool Load(EmployeeRL4Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeRL4 AddNew()
		{
			EmployeeRL4 entity = base.AddNewEntity() as EmployeeRL4;

			return entity;
		}
		public EmployeeRL4 FindByPrimaryKey(Int32 employeeRL4ID)
		{
			return base.FindByPrimaryKey(employeeRL4ID) as EmployeeRL4;
		}

		#region IEnumerable< EmployeeRL4> Members

		IEnumerator<EmployeeRL4> IEnumerable<EmployeeRL4>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeRL4;
			}
		}

		#endregion

		private EmployeeRL4Query query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeRL4' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeRL4 ({EmployeeRL4ID})")]
	[Serializable]
	public partial class EmployeeRL4 : esEmployeeRL4
	{
		public EmployeeRL4()
		{
		}

		public EmployeeRL4(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeRL4Metadata.Meta();
			}
		}

		override protected esEmployeeRL4Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeRL4Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeRL4Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeRL4Query();
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
		public bool Load(EmployeeRL4Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeRL4Query query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeRL4Query : esEmployeeRL4Query
	{
		public EmployeeRL4Query()
		{

		}

		public EmployeeRL4Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeRL4Query";
		}
	}

	[Serializable]
	public partial class EmployeeRL4Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeRL4Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.EmployeeRL4ID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.EmployeeRL4ID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.CompanyEducationProfileID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.CompanyEducationProfileID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.CompanyFieldOfWorkProfileID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.CompanyFieldOfWorkProfileID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.SRRL4Status, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.SRRL4Status;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.SRRL4Type, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.SRRL4Type;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.SRMedisType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.SRMedisType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.SREducationLevel, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.RL4EducationID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.RL4EducationID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.IsActive, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.ValidFrom, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.ValidTo, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.SRRL4ProfessionType, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.SRRL4ProfessionType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.SRRL4EducationLevel, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.SRRL4EducationLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeRL4Metadata.ColumnNames.SRRL4EducationMajor, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRL4Metadata.PropertyNames.SRRL4EducationMajor;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeRL4Metadata Meta()
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
			public const string EmployeeRL4ID = "EmployeeRL4ID";
			public const string PersonID = "PersonID";
			public const string CompanyEducationProfileID = "CompanyEducationProfileID";
			public const string CompanyFieldOfWorkProfileID = "CompanyFieldOfWorkProfileID";
			public const string SRRL4Status = "SRRL4Status";
			public const string SRRL4Type = "SRRL4Type";
			public const string SRMedisType = "SRMedisType";
			public const string SREducationLevel = "SREducationLevel";
			public const string RL4EducationID = "RL4EducationID";
			public const string IsActive = "IsActive";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRRL4ProfessionType = "SRRL4ProfessionType";
			public const string SRRL4EducationLevel = "SRRL4EducationLevel";
			public const string SRRL4EducationMajor = "SRRL4EducationMajor";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeRL4ID = "EmployeeRL4ID";
			public const string PersonID = "PersonID";
			public const string CompanyEducationProfileID = "CompanyEducationProfileID";
			public const string CompanyFieldOfWorkProfileID = "CompanyFieldOfWorkProfileID";
			public const string SRRL4Status = "SRRL4Status";
			public const string SRRL4Type = "SRRL4Type";
			public const string SRMedisType = "SRMedisType";
			public const string SREducationLevel = "SREducationLevel";
			public const string RL4EducationID = "RL4EducationID";
			public const string IsActive = "IsActive";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRRL4ProfessionType = "SRRL4ProfessionType";
			public const string SRRL4EducationLevel = "SRRL4EducationLevel";
			public const string SRRL4EducationMajor = "SRRL4EducationMajor";
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
			lock (typeof(EmployeeRL4Metadata))
			{
				if (EmployeeRL4Metadata.mapDelegates == null)
				{
					EmployeeRL4Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeRL4Metadata.meta == null)
				{
					EmployeeRL4Metadata.meta = new EmployeeRL4Metadata();
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

				meta.AddTypeMap("EmployeeRL4ID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CompanyEducationProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CompanyFieldOfWorkProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRL4Status", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRL4Type", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedisType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RL4EducationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRL4ProfessionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRL4EducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRL4EducationMajor", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeRL4";
				meta.Destination = "EmployeeRL4";
				meta.spInsert = "proc_EmployeeRL4Insert";
				meta.spUpdate = "proc_EmployeeRL4Update";
				meta.spDelete = "proc_EmployeeRL4Delete";
				meta.spLoadAll = "proc_EmployeeRL4LoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeRL4LoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeRL4Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
