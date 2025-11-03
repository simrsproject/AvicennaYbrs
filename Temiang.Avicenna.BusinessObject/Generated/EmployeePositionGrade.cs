/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/19/2022 6:43:47 PM
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
	abstract public class esEmployeePositionGradeCollection : esEntityCollectionWAuditLog
	{
		public esEmployeePositionGradeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeePositionGradeCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeePositionGradeQuery query)
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
			this.InitQuery(query as esEmployeePositionGradeQuery);
		}
		#endregion

		virtual public EmployeePositionGrade DetachEntity(EmployeePositionGrade entity)
		{
			return base.DetachEntity(entity) as EmployeePositionGrade;
		}

		virtual public EmployeePositionGrade AttachEntity(EmployeePositionGrade entity)
		{
			return base.AttachEntity(entity) as EmployeePositionGrade;
		}

		virtual public void Combine(EmployeePositionGradeCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeePositionGrade this[int index]
		{
			get
			{
				return base[index] as EmployeePositionGrade;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeePositionGrade);
		}
	}

	[Serializable]
	abstract public class esEmployeePositionGrade : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeePositionGradeQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeePositionGrade()
		{
		}

		public esEmployeePositionGrade(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 employeePositionGradeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeePositionGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeePositionGradeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 employeePositionGradeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeePositionGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeePositionGradeID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 employeePositionGradeID)
		{
			esEmployeePositionGradeQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeePositionGradeID == employeePositionGradeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 employeePositionGradeID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeePositionGradeID", employeePositionGradeID);
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
						case "EmployeePositionGradeID": this.str.EmployeePositionGradeID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "GradeYear": this.str.GradeYear = (string)value; break;
						case "SRDecreeType": this.str.SRDecreeType = (string)value; break;
						case "DecreeNo": this.str.DecreeNo = (string)value; break;
						case "PositionName": this.str.PositionName = (string)value; break;
						case "NextProposalDate": this.str.NextProposalDate = (string)value; break;
						case "NextPositionGradeID": this.str.NextPositionGradeID = (string)value; break;
						case "NextGradeYear": this.str.NextGradeYear = (string)value; break;
						case "SRDecreeTypeNext": this.str.SRDecreeTypeNext = (string)value; break;
						case "NextPositionName": this.str.NextPositionName = (string)value; break;
						case "SRDp3": this.str.SRDp3 = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SalaryScaleID": this.str.SalaryScaleID = (string)value; break;
						case "NextSalaryScaleID": this.str.NextSalaryScaleID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeePositionGradeID":

							if (value == null || value is System.Int64)
								this.EmployeePositionGradeID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "PositionGradeID":

							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						case "GradeYear":

							if (value == null || value is System.Int32)
								this.GradeYear = (System.Int32?)value;
							break;
						case "NextProposalDate":

							if (value == null || value is System.DateTime)
								this.NextProposalDate = (System.DateTime?)value;
							break;
						case "NextPositionGradeID":

							if (value == null || value is System.Int32)
								this.NextPositionGradeID = (System.Int32?)value;
							break;
						case "NextGradeYear":

							if (value == null || value is System.Int32)
								this.NextGradeYear = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SalaryScaleID":

							if (value == null || value is System.Int32)
								this.SalaryScaleID = (System.Int32?)value;
							break;
						case "NextSalaryScaleID":

							if (value == null || value is System.Int32)
								this.NextSalaryScaleID = (System.Int32?)value;
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
		/// Maps to EmployeePositionGrade.EmployeePositionGradeID
		/// </summary>
		virtual public System.Int64? EmployeePositionGradeID
		{
			get
			{
				return base.GetSystemInt64(EmployeePositionGradeMetadata.ColumnNames.EmployeePositionGradeID);
			}

			set
			{
				base.SetSystemInt64(EmployeePositionGradeMetadata.ColumnNames.EmployeePositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeePositionGradeMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeePositionGradeMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.GradeYear
		/// </summary>
		virtual public System.Int32? GradeYear
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.GradeYear);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.GradeYear, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.SRDecreeType
		/// </summary>
		virtual public System.String SRDecreeType
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.SRDecreeType);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.SRDecreeType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.DecreeNo
		/// </summary>
		virtual public System.String DecreeNo
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.DecreeNo);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.DecreeNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.PositionName
		/// </summary>
		virtual public System.String PositionName
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.PositionName);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.PositionName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.NextProposalDate
		/// </summary>
		virtual public System.DateTime? NextProposalDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeePositionGradeMetadata.ColumnNames.NextProposalDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeePositionGradeMetadata.ColumnNames.NextProposalDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.NextPositionGradeID
		/// </summary>
		virtual public System.Int32? NextPositionGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.NextPositionGradeID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.NextPositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.NextGradeYear
		/// </summary>
		virtual public System.Int32? NextGradeYear
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.NextGradeYear);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.NextGradeYear, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.SRDecreeTypeNext
		/// </summary>
		virtual public System.String SRDecreeTypeNext
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.SRDecreeTypeNext);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.SRDecreeTypeNext, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.NextPositionName
		/// </summary>
		virtual public System.String NextPositionName
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.NextPositionName);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.NextPositionName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.SRDp3
		/// </summary>
		virtual public System.String SRDp3
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.SRDp3);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.SRDp3, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePositionGradeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePositionGradeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePositionGradeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePositionGradeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.SalaryScaleID
		/// </summary>
		virtual public System.Int32? SalaryScaleID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.SalaryScaleID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.SalaryScaleID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePositionGrade.NextSalaryScaleID
		/// </summary>
		virtual public System.Int32? NextSalaryScaleID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.NextSalaryScaleID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionGradeMetadata.ColumnNames.NextSalaryScaleID, value);
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
			public esStrings(esEmployeePositionGrade entity)
			{
				this.entity = entity;
			}
			public System.String EmployeePositionGradeID
			{
				get
				{
					System.Int64? data = entity.EmployeePositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeePositionGradeID = null;
					else entity.EmployeePositionGradeID = Convert.ToInt64(value);
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
			public System.String PositionGradeID
			{
				get
				{
					System.Int32? data = entity.PositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeID = null;
					else entity.PositionGradeID = Convert.ToInt32(value);
				}
			}
			public System.String GradeYear
			{
				get
				{
					System.Int32? data = entity.GradeYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GradeYear = null;
					else entity.GradeYear = Convert.ToInt32(value);
				}
			}
			public System.String SRDecreeType
			{
				get
				{
					System.String data = entity.SRDecreeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDecreeType = null;
					else entity.SRDecreeType = Convert.ToString(value);
				}
			}
			public System.String DecreeNo
			{
				get
				{
					System.String data = entity.DecreeNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecreeNo = null;
					else entity.DecreeNo = Convert.ToString(value);
				}
			}
			public System.String PositionName
			{
				get
				{
					System.String data = entity.PositionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionName = null;
					else entity.PositionName = Convert.ToString(value);
				}
			}
			public System.String NextProposalDate
			{
				get
				{
					System.DateTime? data = entity.NextProposalDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NextProposalDate = null;
					else entity.NextProposalDate = Convert.ToDateTime(value);
				}
			}
			public System.String NextPositionGradeID
			{
				get
				{
					System.Int32? data = entity.NextPositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NextPositionGradeID = null;
					else entity.NextPositionGradeID = Convert.ToInt32(value);
				}
			}
			public System.String NextGradeYear
			{
				get
				{
					System.Int32? data = entity.NextGradeYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NextGradeYear = null;
					else entity.NextGradeYear = Convert.ToInt32(value);
				}
			}
			public System.String SRDecreeTypeNext
			{
				get
				{
					System.String data = entity.SRDecreeTypeNext;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDecreeTypeNext = null;
					else entity.SRDecreeTypeNext = Convert.ToString(value);
				}
			}
			public System.String NextPositionName
			{
				get
				{
					System.String data = entity.NextPositionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NextPositionName = null;
					else entity.NextPositionName = Convert.ToString(value);
				}
			}
			public System.String SRDp3
			{
				get
				{
					System.String data = entity.SRDp3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDp3 = null;
					else entity.SRDp3 = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			public System.String SalaryScaleID
			{
				get
				{
					System.Int32? data = entity.SalaryScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryScaleID = null;
					else entity.SalaryScaleID = Convert.ToInt32(value);
				}
			}
			public System.String NextSalaryScaleID
			{
				get
				{
					System.Int32? data = entity.NextSalaryScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NextSalaryScaleID = null;
					else entity.NextSalaryScaleID = Convert.ToInt32(value);
				}
			}
			private esEmployeePositionGrade entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeePositionGradeQuery query)
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
				throw new Exception("esEmployeePositionGrade can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeePositionGrade : esEmployeePositionGrade
	{
	}

	[Serializable]
	abstract public class esEmployeePositionGradeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeePositionGradeMetadata.Meta();
			}
		}

		public esQueryItem EmployeePositionGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.EmployeePositionGradeID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem GradeYear
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.GradeYear, esSystemType.Int32);
			}
		}

		public esQueryItem SRDecreeType
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.SRDecreeType, esSystemType.String);
			}
		}

		public esQueryItem DecreeNo
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.DecreeNo, esSystemType.String);
			}
		}

		public esQueryItem PositionName
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.PositionName, esSystemType.String);
			}
		}

		public esQueryItem NextProposalDate
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.NextProposalDate, esSystemType.DateTime);
			}
		}

		public esQueryItem NextPositionGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.NextPositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem NextGradeYear
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.NextGradeYear, esSystemType.Int32);
			}
		}

		public esQueryItem SRDecreeTypeNext
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.SRDecreeTypeNext, esSystemType.String);
			}
		}

		public esQueryItem NextPositionName
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.NextPositionName, esSystemType.String);
			}
		}

		public esQueryItem SRDp3
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.SRDp3, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SalaryScaleID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.SalaryScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem NextSalaryScaleID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionGradeMetadata.ColumnNames.NextSalaryScaleID, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeePositionGradeCollection")]
	public partial class EmployeePositionGradeCollection : esEmployeePositionGradeCollection, IEnumerable<EmployeePositionGrade>
	{
		public EmployeePositionGradeCollection()
		{

		}

		public static implicit operator List<EmployeePositionGrade>(EmployeePositionGradeCollection coll)
		{
			List<EmployeePositionGrade> list = new List<EmployeePositionGrade>();

			foreach (EmployeePositionGrade emp in coll)
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
				return EmployeePositionGradeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePositionGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeePositionGrade(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeePositionGrade();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeePositionGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePositionGradeQuery();
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
		public bool Load(EmployeePositionGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeePositionGrade AddNew()
		{
			EmployeePositionGrade entity = base.AddNewEntity() as EmployeePositionGrade;

			return entity;
		}
		public EmployeePositionGrade FindByPrimaryKey(Int64 employeePositionGradeID)
		{
			return base.FindByPrimaryKey(employeePositionGradeID) as EmployeePositionGrade;
		}

		#region IEnumerable< EmployeePositionGrade> Members

		IEnumerator<EmployeePositionGrade> IEnumerable<EmployeePositionGrade>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeePositionGrade;
			}
		}

		#endregion

		private EmployeePositionGradeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeePositionGrade' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeePositionGrade ({EmployeePositionGradeID})")]
	[Serializable]
	public partial class EmployeePositionGrade : esEmployeePositionGrade
	{
		public EmployeePositionGrade()
		{
		}

		public EmployeePositionGrade(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeePositionGradeMetadata.Meta();
			}
		}

		override protected esEmployeePositionGradeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePositionGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeePositionGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePositionGradeQuery();
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
		public bool Load(EmployeePositionGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeePositionGradeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeePositionGradeQuery : esEmployeePositionGradeQuery
	{
		public EmployeePositionGradeQuery()
		{

		}

		public EmployeePositionGradeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeePositionGradeQuery";
		}
	}

	[Serializable]
	public partial class EmployeePositionGradeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeePositionGradeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.EmployeePositionGradeID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.EmployeePositionGradeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.SREducationLevel, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.PositionGradeID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.GradeYear, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.GradeYear;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.SRDecreeType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.SRDecreeType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.DecreeNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.DecreeNo;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.PositionName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.PositionName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.NextProposalDate, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.NextProposalDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.NextPositionGradeID, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.NextPositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.NextGradeYear, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.NextGradeYear;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.SRDecreeTypeNext, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.SRDecreeTypeNext;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.NextPositionName, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.NextPositionName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.SRDp3, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.SRDp3;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.Notes, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.SalaryScaleID, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.SalaryScaleID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionGradeMetadata.ColumnNames.NextSalaryScaleID, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionGradeMetadata.PropertyNames.NextSalaryScaleID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeePositionGradeMetadata Meta()
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
			public const string EmployeePositionGradeID = "EmployeePositionGradeID";
			public const string PersonID = "PersonID";
			public const string SREducationLevel = "SREducationLevel";
			public const string ValidFrom = "ValidFrom";
			public const string PositionGradeID = "PositionGradeID";
			public const string GradeYear = "GradeYear";
			public const string SRDecreeType = "SRDecreeType";
			public const string DecreeNo = "DecreeNo";
			public const string PositionName = "PositionName";
			public const string NextProposalDate = "NextProposalDate";
			public const string NextPositionGradeID = "NextPositionGradeID";
			public const string NextGradeYear = "NextGradeYear";
			public const string SRDecreeTypeNext = "SRDecreeTypeNext";
			public const string NextPositionName = "NextPositionName";
			public const string SRDp3 = "SRDp3";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SalaryScaleID = "SalaryScaleID";
			public const string NextSalaryScaleID = "NextSalaryScaleID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeePositionGradeID = "EmployeePositionGradeID";
			public const string PersonID = "PersonID";
			public const string SREducationLevel = "SREducationLevel";
			public const string ValidFrom = "ValidFrom";
			public const string PositionGradeID = "PositionGradeID";
			public const string GradeYear = "GradeYear";
			public const string SRDecreeType = "SRDecreeType";
			public const string DecreeNo = "DecreeNo";
			public const string PositionName = "PositionName";
			public const string NextProposalDate = "NextProposalDate";
			public const string NextPositionGradeID = "NextPositionGradeID";
			public const string NextGradeYear = "NextGradeYear";
			public const string SRDecreeTypeNext = "SRDecreeTypeNext";
			public const string NextPositionName = "NextPositionName";
			public const string SRDp3 = "SRDp3";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SalaryScaleID = "SalaryScaleID";
			public const string NextSalaryScaleID = "NextSalaryScaleID";
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
			lock (typeof(EmployeePositionGradeMetadata))
			{
				if (EmployeePositionGradeMetadata.mapDelegates == null)
				{
					EmployeePositionGradeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeePositionGradeMetadata.meta == null)
				{
					EmployeePositionGradeMetadata.meta = new EmployeePositionGradeMetadata();
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

				meta.AddTypeMap("EmployeePositionGradeID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GradeYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRDecreeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DecreeNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NextProposalDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("NextPositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NextGradeYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRDecreeTypeNext", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NextPositionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDp3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalaryScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NextSalaryScaleID", new esTypeMap("int", "System.Int32"));


				meta.Source = "EmployeePositionGrade";
				meta.Destination = "EmployeePositionGrade";
				meta.spInsert = "proc_EmployeePositionGradeInsert";
				meta.spUpdate = "proc_EmployeePositionGradeUpdate";
				meta.spDelete = "proc_EmployeePositionGradeDelete";
				meta.spLoadAll = "proc_EmployeePositionGradeLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeePositionGradeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeePositionGradeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
