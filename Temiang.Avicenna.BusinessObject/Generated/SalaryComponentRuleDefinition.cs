/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/26/2020 12:23:30 PM
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
	abstract public class esSalaryComponentRuleDefinitionCollection : esEntityCollectionWAuditLog
	{
		public esSalaryComponentRuleDefinitionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SalaryComponentRuleDefinitionCollection";
		}

		#region Query Logic
		protected void InitQuery(esSalaryComponentRuleDefinitionQuery query)
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
			this.InitQuery(query as esSalaryComponentRuleDefinitionQuery);
		}
		#endregion

		virtual public SalaryComponentRuleDefinition DetachEntity(SalaryComponentRuleDefinition entity)
		{
			return base.DetachEntity(entity) as SalaryComponentRuleDefinition;
		}

		virtual public SalaryComponentRuleDefinition AttachEntity(SalaryComponentRuleDefinition entity)
		{
			return base.AttachEntity(entity) as SalaryComponentRuleDefinition;
		}

		virtual public void Combine(SalaryComponentRuleDefinitionCollection collection)
		{
			base.Combine(collection);
		}

		new public SalaryComponentRuleDefinition this[int index]
		{
			get
			{
				return base[index] as SalaryComponentRuleDefinition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SalaryComponentRuleDefinition);
		}
	}

	[Serializable]
	abstract public class esSalaryComponentRuleDefinition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSalaryComponentRuleDefinitionQuery GetDynamicQuery()
		{
			return null;
		}

		public esSalaryComponentRuleDefinition()
		{
		}

		public esSalaryComponentRuleDefinition(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 salaryComponentRuleDefinitionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentRuleDefinitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentRuleDefinitionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 salaryComponentRuleDefinitionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentRuleDefinitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentRuleDefinitionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 salaryComponentRuleDefinitionID)
		{
			esSalaryComponentRuleDefinitionQuery query = this.GetDynamicQuery();
			query.Where(query.SalaryComponentRuleDefinitionID == salaryComponentRuleDefinitionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 salaryComponentRuleDefinitionID)
		{
			esParameters parms = new esParameters();
			parms.Add("SalaryComponentRuleDefinitionID", salaryComponentRuleDefinitionID);
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
						case "SalaryComponentRuleDefinitionID": this.str.SalaryComponentRuleDefinitionID = (string)value; break;
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
						case "SREmployeeStatus": this.str.SREmployeeStatus = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "SRReligion": this.str.SRReligion = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;
						case "ServiceYear": this.str.ServiceYear = (string)value; break;
						case "SalaryTableNumber": this.str.SalaryTableNumber = (string)value; break;
						case "EmployeeGradeID": this.str.EmployeeGradeID = (string)value; break;
						case "NoOfDependent": this.str.NoOfDependent = (string)value; break;
						case "AttedanceMatrixID": this.str.AttedanceMatrixID = (string)value; break;
						case "NominalAmount": this.str.NominalAmount = (string)value; break;
						case "PercentageAmount": this.str.PercentageAmount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "PercentageComponentID": this.str.PercentageComponentID = (string)value; break;
						case "SREducationLevelID": this.str.SREducationLevelID = (string)value; break;
						case "SREmployeeType": this.str.SREmployeeType = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SalaryComponentRuleDefinitionID":

							if (value == null || value is System.Int64)
								this.SalaryComponentRuleDefinitionID = (System.Int64?)value;
							break;
						case "SalaryComponentID":

							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						case "OrganizationUnitID":

							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PositionGradeID":

							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						case "SalaryTableNumber":

							if (value == null || value is System.Int32)
								this.SalaryTableNumber = (System.Int32?)value;
							break;
						case "EmployeeGradeID":

							if (value == null || value is System.Int32)
								this.EmployeeGradeID = (System.Int32?)value;
							break;
						case "NoOfDependent":

							if (value == null || value is System.Int32)
								this.NoOfDependent = (System.Int32?)value;
							break;
						case "AttedanceMatrixID":

							if (value == null || value is System.Int32)
								this.AttedanceMatrixID = (System.Int32?)value;
							break;
						case "NominalAmount":

							if (value == null || value is System.Decimal)
								this.NominalAmount = (System.Decimal?)value;
							break;
						case "PercentageAmount":

							if (value == null || value is System.Decimal)
								this.PercentageAmount = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "PercentageComponentID":

							if (value == null || value is System.Int32)
								this.PercentageComponentID = (System.Int32?)value;
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
		/// Maps to SalaryComponentRuleDefinition.SalaryComponentRuleDefinitionID
		/// </summary>
		virtual public System.Int64? SalaryComponentRuleDefinitionID
		{
			get
			{
				return base.GetSystemInt64(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentRuleDefinitionID);
			}

			set
			{
				base.SetSystemInt64(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentRuleDefinitionID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeStatus);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SRReligion
		/// </summary>
		virtual public System.String SRReligion
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SRReligion);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SRReligion, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmploymentType);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SRMaritalStatus);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.ServiceYear
		/// </summary>
		virtual public System.String ServiceYear
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceYear);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceYear, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SalaryTableNumber
		/// </summary>
		virtual public System.Int32? SalaryTableNumber
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryTableNumber);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryTableNumber, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.EmployeeGradeID
		/// </summary>
		virtual public System.Int32? EmployeeGradeID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.EmployeeGradeID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.EmployeeGradeID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.NoOfDependent
		/// </summary>
		virtual public System.Int32? NoOfDependent
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.NoOfDependent);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.NoOfDependent, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.AttedanceMatrixID
		/// </summary>
		virtual public System.Int32? AttedanceMatrixID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.AttedanceMatrixID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.AttedanceMatrixID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.NominalAmount
		/// </summary>
		virtual public System.Decimal? NominalAmount
		{
			get
			{
				return base.GetSystemDecimal(SalaryComponentRuleDefinitionMetadata.ColumnNames.NominalAmount);
			}

			set
			{
				base.SetSystemDecimal(SalaryComponentRuleDefinitionMetadata.ColumnNames.NominalAmount, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.PercentageAmount
		/// </summary>
		virtual public System.Decimal? PercentageAmount
		{
			get
			{
				return base.GetSystemDecimal(SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageAmount);
			}

			set
			{
				base.SetSystemDecimal(SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageAmount, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.PercentageComponentID
		/// </summary>
		virtual public System.Int32? PercentageComponentID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageComponentID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageComponentID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SREducationLevelID
		/// </summary>
		virtual public System.String SREducationLevelID
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREducationLevelID);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREducationLevelID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.SREmployeeType
		/// </summary>
		virtual public System.String SREmployeeType
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeType);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponentRuleDefinition.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceUnitID, value);
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
			public esStrings(esSalaryComponentRuleDefinition entity)
			{
				this.entity = entity;
			}
			public System.String SalaryComponentRuleDefinitionID
			{
				get
				{
					System.Int64? data = entity.SalaryComponentRuleDefinitionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentRuleDefinitionID = null;
					else entity.SalaryComponentRuleDefinitionID = Convert.ToInt64(value);
				}
			}
			public System.String SalaryComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentID = null;
					else entity.SalaryComponentID = Convert.ToInt32(value);
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
			public System.String OrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.OrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
					else entity.OrganizationUnitID = Convert.ToInt32(value);
				}
			}
			public System.String SREmployeeStatus
			{
				get
				{
					System.String data = entity.SREmployeeStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeStatus = null;
					else entity.SREmployeeStatus = Convert.ToString(value);
				}
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String SRReligion
			{
				get
				{
					System.String data = entity.SRReligion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReligion = null;
					else entity.SRReligion = Convert.ToString(value);
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
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
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
			public System.String SRMaritalStatus
			{
				get
				{
					System.String data = entity.SRMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
					else entity.SRMaritalStatus = Convert.ToString(value);
				}
			}
			public System.String ServiceYear
			{
				get
				{
					System.String data = entity.ServiceYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYear = null;
					else entity.ServiceYear = Convert.ToString(value);
				}
			}
			public System.String SalaryTableNumber
			{
				get
				{
					System.Int32? data = entity.SalaryTableNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryTableNumber = null;
					else entity.SalaryTableNumber = Convert.ToInt32(value);
				}
			}
			public System.String EmployeeGradeID
			{
				get
				{
					System.Int32? data = entity.EmployeeGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeGradeID = null;
					else entity.EmployeeGradeID = Convert.ToInt32(value);
				}
			}
			public System.String NoOfDependent
			{
				get
				{
					System.Int32? data = entity.NoOfDependent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoOfDependent = null;
					else entity.NoOfDependent = Convert.ToInt32(value);
				}
			}
			public System.String AttedanceMatrixID
			{
				get
				{
					System.Int32? data = entity.AttedanceMatrixID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AttedanceMatrixID = null;
					else entity.AttedanceMatrixID = Convert.ToInt32(value);
				}
			}
			public System.String NominalAmount
			{
				get
				{
					System.Decimal? data = entity.NominalAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NominalAmount = null;
					else entity.NominalAmount = Convert.ToDecimal(value);
				}
			}
			public System.String PercentageAmount
			{
				get
				{
					System.Decimal? data = entity.PercentageAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentageAmount = null;
					else entity.PercentageAmount = Convert.ToDecimal(value);
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
			public System.String PercentageComponentID
			{
				get
				{
					System.Int32? data = entity.PercentageComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentageComponentID = null;
					else entity.PercentageComponentID = Convert.ToInt32(value);
				}
			}
			public System.String SREducationLevelID
			{
				get
				{
					System.String data = entity.SREducationLevelID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevelID = null;
					else entity.SREducationLevelID = Convert.ToString(value);
				}
			}
			public System.String SREmployeeType
			{
				get
				{
					System.String data = entity.SREmployeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeType = null;
					else entity.SREmployeeType = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			private esSalaryComponentRuleDefinition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSalaryComponentRuleDefinitionQuery query)
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
				throw new Exception("esSalaryComponentRuleDefinition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SalaryComponentRuleDefinition : esSalaryComponentRuleDefinition
	{
	}

	[Serializable]
	abstract public class esSalaryComponentRuleDefinitionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentRuleDefinitionMetadata.Meta();
			}
		}

		public esQueryItem SalaryComponentRuleDefinitionID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentRuleDefinitionID, esSystemType.Int64);
			}
		}

		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem SRReligion
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SRReligion, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		}

		public esQueryItem ServiceYear
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceYear, esSystemType.String);
			}
		}

		public esQueryItem SalaryTableNumber
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryTableNumber, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeGradeID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.EmployeeGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem NoOfDependent
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.NoOfDependent, esSystemType.Int32);
			}
		}

		public esQueryItem AttedanceMatrixID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.AttedanceMatrixID, esSystemType.Int32);
			}
		}

		public esQueryItem NominalAmount
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.NominalAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem PercentageAmount
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem PercentageComponentID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageComponentID, esSystemType.Int32);
			}
		}

		public esQueryItem SREducationLevelID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREducationLevelID, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeType, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SalaryComponentRuleDefinitionCollection")]
	public partial class SalaryComponentRuleDefinitionCollection : esSalaryComponentRuleDefinitionCollection, IEnumerable<SalaryComponentRuleDefinition>
	{
		public SalaryComponentRuleDefinitionCollection()
		{

		}

		public static implicit operator List<SalaryComponentRuleDefinition>(SalaryComponentRuleDefinitionCollection coll)
		{
			List<SalaryComponentRuleDefinition> list = new List<SalaryComponentRuleDefinition>();

			foreach (SalaryComponentRuleDefinition emp in coll)
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
				return SalaryComponentRuleDefinitionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentRuleDefinitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SalaryComponentRuleDefinition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SalaryComponentRuleDefinition();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SalaryComponentRuleDefinitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentRuleDefinitionQuery();
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
		public bool Load(SalaryComponentRuleDefinitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SalaryComponentRuleDefinition AddNew()
		{
			SalaryComponentRuleDefinition entity = base.AddNewEntity() as SalaryComponentRuleDefinition;

			return entity;
		}
		public SalaryComponentRuleDefinition FindByPrimaryKey(Int64 salaryComponentRuleDefinitionID)
		{
			return base.FindByPrimaryKey(salaryComponentRuleDefinitionID) as SalaryComponentRuleDefinition;
		}

		#region IEnumerable< SalaryComponentRuleDefinition> Members

		IEnumerator<SalaryComponentRuleDefinition> IEnumerable<SalaryComponentRuleDefinition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SalaryComponentRuleDefinition;
			}
		}

		#endregion

		private SalaryComponentRuleDefinitionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SalaryComponentRuleDefinition' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SalaryComponentRuleDefinition ({SalaryComponentRuleDefinitionID})")]
	[Serializable]
	public partial class SalaryComponentRuleDefinition : esSalaryComponentRuleDefinition
	{
		public SalaryComponentRuleDefinition()
		{
		}

		public SalaryComponentRuleDefinition(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentRuleDefinitionMetadata.Meta();
			}
		}

		override protected esSalaryComponentRuleDefinitionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentRuleDefinitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SalaryComponentRuleDefinitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentRuleDefinitionQuery();
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
		public bool Load(SalaryComponentRuleDefinitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SalaryComponentRuleDefinitionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SalaryComponentRuleDefinitionQuery : esSalaryComponentRuleDefinitionQuery
	{
		public SalaryComponentRuleDefinitionQuery()
		{

		}

		public SalaryComponentRuleDefinitionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SalaryComponentRuleDefinitionQuery";
		}
	}

	[Serializable]
	public partial class SalaryComponentRuleDefinitionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SalaryComponentRuleDefinitionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentRuleDefinitionID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SalaryComponentRuleDefinitionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SalaryComponentID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidFrom, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidTo, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.ValidTo;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.OrganizationUnitID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeStatus, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionID, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SRReligion, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SRReligion;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.PersonID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmploymentType, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.PositionGradeID, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SRMaritalStatus, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceYear, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.ServiceYear;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryTableNumber, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SalaryTableNumber;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.EmployeeGradeID, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.EmployeeGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.NoOfDependent, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.NoOfDependent;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.AttedanceMatrixID, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.AttedanceMatrixID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.NominalAmount, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.NominalAmount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.PercentageAmount;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageComponentID, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.PercentageComponentID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREducationLevelID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SREducationLevelID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeType, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.SREmployeeType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceUnitID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRuleDefinitionMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SalaryComponentRuleDefinitionMetadata Meta()
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
			public const string SalaryComponentRuleDefinitionID = "SalaryComponentRuleDefinitionID";
			public const string SalaryComponentID = "SalaryComponentID";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string PositionID = "PositionID";
			public const string SRReligion = "SRReligion";
			public const string PersonID = "PersonID";
			public const string SREmploymentType = "SREmploymentType";
			public const string PositionGradeID = "PositionGradeID";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string ServiceYear = "ServiceYear";
			public const string SalaryTableNumber = "SalaryTableNumber";
			public const string EmployeeGradeID = "EmployeeGradeID";
			public const string NoOfDependent = "NoOfDependent";
			public const string AttedanceMatrixID = "AttedanceMatrixID";
			public const string NominalAmount = "NominalAmount";
			public const string PercentageAmount = "PercentageAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PercentageComponentID = "PercentageComponentID";
			public const string SREducationLevelID = "SREducationLevelID";
			public const string SREmployeeType = "SREmployeeType";
			public const string ServiceUnitID = "ServiceUnitID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SalaryComponentRuleDefinitionID = "SalaryComponentRuleDefinitionID";
			public const string SalaryComponentID = "SalaryComponentID";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string PositionID = "PositionID";
			public const string SRReligion = "SRReligion";
			public const string PersonID = "PersonID";
			public const string SREmploymentType = "SREmploymentType";
			public const string PositionGradeID = "PositionGradeID";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string ServiceYear = "ServiceYear";
			public const string SalaryTableNumber = "SalaryTableNumber";
			public const string EmployeeGradeID = "EmployeeGradeID";
			public const string NoOfDependent = "NoOfDependent";
			public const string AttedanceMatrixID = "AttedanceMatrixID";
			public const string NominalAmount = "NominalAmount";
			public const string PercentageAmount = "PercentageAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PercentageComponentID = "PercentageComponentID";
			public const string SREducationLevelID = "SREducationLevelID";
			public const string SREmployeeType = "SREmployeeType";
			public const string ServiceUnitID = "ServiceUnitID";
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
			lock (typeof(SalaryComponentRuleDefinitionMetadata))
			{
				if (SalaryComponentRuleDefinitionMetadata.mapDelegates == null)
				{
					SalaryComponentRuleDefinitionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SalaryComponentRuleDefinitionMetadata.meta == null)
				{
					SalaryComponentRuleDefinitionMetadata.meta = new SalaryComponentRuleDefinitionMetadata();
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

				meta.AddTypeMap("SalaryComponentRuleDefinitionID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalaryTableNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NoOfDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AttedanceMatrixID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NominalAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("PercentageAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PercentageComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevelID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SalaryComponentRuleDefinition";
				meta.Destination = "SalaryComponentRuleDefinition";
				meta.spInsert = "proc_SalaryComponentRuleDefinitionInsert";
				meta.spUpdate = "proc_SalaryComponentRuleDefinitionUpdate";
				meta.spDelete = "proc_SalaryComponentRuleDefinitionDelete";
				meta.spLoadAll = "proc_SalaryComponentRuleDefinitionLoadAll";
				meta.spLoadByPrimaryKey = "proc_SalaryComponentRuleDefinitionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SalaryComponentRuleDefinitionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
