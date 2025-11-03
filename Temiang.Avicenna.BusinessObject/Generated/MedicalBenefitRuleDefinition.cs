/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/10/2011 8:31:55 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esMedicalBenefitRuleDefinitionCollection : esEntityCollectionWAuditLog
	{
		public esMedicalBenefitRuleDefinitionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MedicalBenefitRuleDefinitionCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalBenefitRuleDefinitionQuery query)
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
			this.InitQuery(query as esMedicalBenefitRuleDefinitionQuery);
		}
		#endregion
		
		virtual public MedicalBenefitRuleDefinition DetachEntity(MedicalBenefitRuleDefinition entity)
		{
			return base.DetachEntity(entity) as MedicalBenefitRuleDefinition;
		}
		
		virtual public MedicalBenefitRuleDefinition AttachEntity(MedicalBenefitRuleDefinition entity)
		{
			return base.AttachEntity(entity) as MedicalBenefitRuleDefinition;
		}
		
		virtual public void Combine(MedicalBenefitRuleDefinitionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalBenefitRuleDefinition this[int index]
		{
			get
			{
				return base[index] as MedicalBenefitRuleDefinition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalBenefitRuleDefinition);
		}
	}



	[Serializable]
	abstract public class esMedicalBenefitRuleDefinition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalBenefitRuleDefinitionQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalBenefitRuleDefinition()
		{

		}

		public esMedicalBenefitRuleDefinition(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 medicalBenefitRuleDefinitionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitRuleDefinitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitRuleDefinitionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 medicalBenefitRuleDefinitionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitRuleDefinitionID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitRuleDefinitionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 medicalBenefitRuleDefinitionID)
		{
			esMedicalBenefitRuleDefinitionQuery query = this.GetDynamicQuery();
			query.Where(query.MedicalBenefitRuleDefinitionID == medicalBenefitRuleDefinitionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 medicalBenefitRuleDefinitionID)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicalBenefitRuleDefinitionID",medicalBenefitRuleDefinitionID);
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
						case "MedicalBenefitRuleDefinitionID": this.str.MedicalBenefitRuleDefinitionID = (string)value; break;							
						case "MedicalBenefitInfoID": this.str.MedicalBenefitInfoID = (string)value; break;							
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;							
						case "SREmployeeStatus": this.str.SREmployeeStatus = (string)value; break;							
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;							
						case "EmployeeGradeID": this.str.EmployeeGradeID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "AgeFrom": this.str.AgeFrom = (string)value; break;							
						case "AgeTo": this.str.AgeTo = (string)value; break;							
						case "ServiceYearFrom": this.str.ServiceYearFrom = (string)value; break;							
						case "ServiceYearTo": this.str.ServiceYearTo = (string)value; break;							
						case "IsUnlimit": this.str.IsUnlimit = (string)value; break;							
						case "BasicSalaryFactor": this.str.BasicSalaryFactor = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MedicalBenefitRuleDefinitionID":
						
							if (value == null || value is System.Int64)
								this.MedicalBenefitRuleDefinitionID = (System.Int64?)value;
							break;
						
						case "MedicalBenefitInfoID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitInfoID = (System.Int32?)value;
							break;
						
						case "PositionGradeID":
						
							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						
						case "EmployeeGradeID":
						
							if (value == null || value is System.Int32)
								this.EmployeeGradeID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "AgeFrom":
						
							if (value == null || value is System.Int32)
								this.AgeFrom = (System.Int32?)value;
							break;
						
						case "AgeTo":
						
							if (value == null || value is System.Int32)
								this.AgeTo = (System.Int32?)value;
							break;
						
						case "ServiceYearFrom":
						
							if (value == null || value is System.Int32)
								this.ServiceYearFrom = (System.Int32?)value;
							break;
						
						case "ServiceYearTo":
						
							if (value == null || value is System.Int32)
								this.ServiceYearTo = (System.Int32?)value;
							break;
						
						case "IsUnlimit":
						
							if (value == null || value is System.Boolean)
								this.IsUnlimit = (System.Boolean?)value;
							break;
						
						case "BasicSalaryFactor":
						
							if (value == null || value is System.Int32)
								this.BasicSalaryFactor = (System.Int32?)value;
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
		/// Maps to MedicalBenefitRuleDefinition.MedicalBenefitRuleDefinitionID
		/// </summary>
		virtual public System.Int64? MedicalBenefitRuleDefinitionID
		{
			get
			{
				return base.GetSystemInt64(MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitRuleDefinitionID);
			}
			
			set
			{
				base.SetSystemInt64(MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitRuleDefinitionID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.MedicalBenefitInfoID
		/// </summary>
		virtual public System.Int32? MedicalBenefitInfoID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitInfoID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmploymentType);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmployeeStatus);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.PositionGradeID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.EmployeeGradeID
		/// </summary>
		virtual public System.Int32? EmployeeGradeID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.EmployeeGradeID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.EmployeeGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.AgeFrom
		/// </summary>
		virtual public System.Int32? AgeFrom
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeFrom);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.AgeTo
		/// </summary>
		virtual public System.Int32? AgeTo
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeTo);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeTo, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.ServiceYearFrom
		/// </summary>
		virtual public System.Int32? ServiceYearFrom
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearFrom);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.ServiceYearTo
		/// </summary>
		virtual public System.Int32? ServiceYearTo
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearTo);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearTo, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.IsUnlimit
		/// </summary>
		virtual public System.Boolean? IsUnlimit
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitRuleDefinitionMetadata.ColumnNames.IsUnlimit);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitRuleDefinitionMetadata.ColumnNames.IsUnlimit, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.BasicSalaryFactor
		/// </summary>
		virtual public System.Int32? BasicSalaryFactor
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.BasicSalaryFactor);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitRuleDefinitionMetadata.ColumnNames.BasicSalaryFactor, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitRuleDefinition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esMedicalBenefitRuleDefinition entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MedicalBenefitRuleDefinitionID
			{
				get
				{
					System.Int64? data = entity.MedicalBenefitRuleDefinitionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitRuleDefinitionID = null;
					else entity.MedicalBenefitRuleDefinitionID = Convert.ToInt64(value);
				}
			}
				
			public System.String MedicalBenefitInfoID
			{
				get
				{
					System.Int32? data = entity.MedicalBenefitInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitInfoID = null;
					else entity.MedicalBenefitInfoID = Convert.ToInt32(value);
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
				
			public System.String AgeFrom
			{
				get
				{
					System.Int32? data = entity.AgeFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeFrom = null;
					else entity.AgeFrom = Convert.ToInt32(value);
				}
			}
				
			public System.String AgeTo
			{
				get
				{
					System.Int32? data = entity.AgeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeTo = null;
					else entity.AgeTo = Convert.ToInt32(value);
				}
			}
				
			public System.String ServiceYearFrom
			{
				get
				{
					System.Int32? data = entity.ServiceYearFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYearFrom = null;
					else entity.ServiceYearFrom = Convert.ToInt32(value);
				}
			}
				
			public System.String ServiceYearTo
			{
				get
				{
					System.Int32? data = entity.ServiceYearTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYearTo = null;
					else entity.ServiceYearTo = Convert.ToInt32(value);
				}
			}
				
			public System.String IsUnlimit
			{
				get
				{
					System.Boolean? data = entity.IsUnlimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUnlimit = null;
					else entity.IsUnlimit = Convert.ToBoolean(value);
				}
			}
				
			public System.String BasicSalaryFactor
			{
				get
				{
					System.Int32? data = entity.BasicSalaryFactor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BasicSalaryFactor = null;
					else entity.BasicSalaryFactor = Convert.ToInt32(value);
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
			

			private esMedicalBenefitRuleDefinition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalBenefitRuleDefinitionQuery query)
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
				throw new Exception("esMedicalBenefitRuleDefinition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MedicalBenefitRuleDefinition : esMedicalBenefitRuleDefinition
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esMedicalBenefitRuleDefinitionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitRuleDefinitionMetadata.Meta();
			}
		}	
		

		public esQueryItem MedicalBenefitRuleDefinitionID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitRuleDefinitionID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem MedicalBenefitInfoID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitInfoID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EmployeeGradeID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.EmployeeGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AgeFrom
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeFrom, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AgeTo
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeTo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ServiceYearFrom
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearFrom, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ServiceYearTo
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearTo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsUnlimit
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.IsUnlimit, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem BasicSalaryFactor
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.BasicSalaryFactor, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalBenefitRuleDefinitionCollection")]
	public partial class MedicalBenefitRuleDefinitionCollection : esMedicalBenefitRuleDefinitionCollection, IEnumerable<MedicalBenefitRuleDefinition>
	{
		public MedicalBenefitRuleDefinitionCollection()
		{

		}
		
		public static implicit operator List<MedicalBenefitRuleDefinition>(MedicalBenefitRuleDefinitionCollection coll)
		{
			List<MedicalBenefitRuleDefinition> list = new List<MedicalBenefitRuleDefinition>();
			
			foreach (MedicalBenefitRuleDefinition emp in coll)
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
				return  MedicalBenefitRuleDefinitionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitRuleDefinitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalBenefitRuleDefinition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalBenefitRuleDefinition();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MedicalBenefitRuleDefinitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitRuleDefinitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MedicalBenefitRuleDefinitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MedicalBenefitRuleDefinition AddNew()
		{
			MedicalBenefitRuleDefinition entity = base.AddNewEntity() as MedicalBenefitRuleDefinition;
			
			return entity;
		}

		public MedicalBenefitRuleDefinition FindByPrimaryKey(System.Int64 medicalBenefitRuleDefinitionID)
		{
			return base.FindByPrimaryKey(medicalBenefitRuleDefinitionID) as MedicalBenefitRuleDefinition;
		}


		#region IEnumerable<MedicalBenefitRuleDefinition> Members

		IEnumerator<MedicalBenefitRuleDefinition> IEnumerable<MedicalBenefitRuleDefinition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalBenefitRuleDefinition;
			}
		}

		#endregion
		
		private MedicalBenefitRuleDefinitionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalBenefitRuleDefinition' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalBenefitRuleDefinition ({MedicalBenefitRuleDefinitionID})")]
	[Serializable]
	public partial class MedicalBenefitRuleDefinition : esMedicalBenefitRuleDefinition
	{
		public MedicalBenefitRuleDefinition()
		{

		}
	
		public MedicalBenefitRuleDefinition(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitRuleDefinitionMetadata.Meta();
			}
		}
		
		
		
		override protected esMedicalBenefitRuleDefinitionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitRuleDefinitionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MedicalBenefitRuleDefinitionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitRuleDefinitionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MedicalBenefitRuleDefinitionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MedicalBenefitRuleDefinitionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MedicalBenefitRuleDefinitionQuery : esMedicalBenefitRuleDefinitionQuery
	{
		public MedicalBenefitRuleDefinitionQuery()
		{

		}		
		
		public MedicalBenefitRuleDefinitionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MedicalBenefitRuleDefinitionQuery";
        }
		
			
	}


	[Serializable]
	public partial class MedicalBenefitRuleDefinitionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalBenefitRuleDefinitionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitRuleDefinitionID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.MedicalBenefitRuleDefinitionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.MedicalBenefitInfoID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.MedicalBenefitInfoID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmploymentType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.SREmployeeStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.PositionGradeID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.EmployeeGradeID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.EmployeeGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.PersonID, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeFrom, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.AgeFrom;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.AgeTo, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.AgeTo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearFrom, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.ServiceYearFrom;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.ServiceYearTo, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.ServiceYearTo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.IsUnlimit, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.IsUnlimit;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.BasicSalaryFactor, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.BasicSalaryFactor;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitRuleDefinitionMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitRuleDefinitionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MedicalBenefitRuleDefinitionMetadata Meta()
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
			 public const string MedicalBenefitRuleDefinitionID = "MedicalBenefitRuleDefinitionID";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string SREmploymentType = "SREmploymentType";
			 public const string SREmployeeStatus = "SREmployeeStatus";
			 public const string PositionGradeID = "PositionGradeID";
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string PersonID = "PersonID";
			 public const string AgeFrom = "AgeFrom";
			 public const string AgeTo = "AgeTo";
			 public const string ServiceYearFrom = "ServiceYearFrom";
			 public const string ServiceYearTo = "ServiceYearTo";
			 public const string IsUnlimit = "IsUnlimit";
			 public const string BasicSalaryFactor = "BasicSalaryFactor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MedicalBenefitRuleDefinitionID = "MedicalBenefitRuleDefinitionID";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string SREmploymentType = "SREmploymentType";
			 public const string SREmployeeStatus = "SREmployeeStatus";
			 public const string PositionGradeID = "PositionGradeID";
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string PersonID = "PersonID";
			 public const string AgeFrom = "AgeFrom";
			 public const string AgeTo = "AgeTo";
			 public const string ServiceYearFrom = "ServiceYearFrom";
			 public const string ServiceYearTo = "ServiceYearTo";
			 public const string IsUnlimit = "IsUnlimit";
			 public const string BasicSalaryFactor = "BasicSalaryFactor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(MedicalBenefitRuleDefinitionMetadata))
			{
				if(MedicalBenefitRuleDefinitionMetadata.mapDelegates == null)
				{
					MedicalBenefitRuleDefinitionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalBenefitRuleDefinitionMetadata.meta == null)
				{
					MedicalBenefitRuleDefinitionMetadata.meta = new MedicalBenefitRuleDefinitionMetadata();
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
				

				meta.AddTypeMap("MedicalBenefitRuleDefinitionID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("MedicalBenefitInfoID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AgeFrom", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AgeTo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceYearFrom", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceYearTo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsUnlimit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BasicSalaryFactor", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MedicalBenefitRuleDefinition";
				meta.Destination = "MedicalBenefitRuleDefinition";
				
				meta.spInsert = "proc_MedicalBenefitRuleDefinitionInsert";				
				meta.spUpdate = "proc_MedicalBenefitRuleDefinitionUpdate";		
				meta.spDelete = "proc_MedicalBenefitRuleDefinitionDelete";
				meta.spLoadAll = "proc_MedicalBenefitRuleDefinitionLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalBenefitRuleDefinitionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalBenefitRuleDefinitionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
