/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/3/2023 9:53:16 PM
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
	abstract public class esPathologyAnatomyPapSmearCollection : esEntityCollectionWAuditLog
	{
		public esPathologyAnatomyPapSmearCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PathologyAnatomyPapSmearCollection";
		}

		#region Query Logic
		protected void InitQuery(esPathologyAnatomyPapSmearQuery query)
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
			this.InitQuery(query as esPathologyAnatomyPapSmearQuery);
		}
		#endregion

		virtual public PathologyAnatomyPapSmear DetachEntity(PathologyAnatomyPapSmear entity)
		{
			return base.DetachEntity(entity) as PathologyAnatomyPapSmear;
		}

		virtual public PathologyAnatomyPapSmear AttachEntity(PathologyAnatomyPapSmear entity)
		{
			return base.AttachEntity(entity) as PathologyAnatomyPapSmear;
		}

		virtual public void Combine(PathologyAnatomyPapSmearCollection collection)
		{
			base.Combine(collection);
		}

		new public PathologyAnatomyPapSmear this[int index]
		{
			get
			{
				return base[index] as PathologyAnatomyPapSmear;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PathologyAnatomyPapSmear);
		}
	}

	[Serializable]
	abstract public class esPathologyAnatomyPapSmear : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPathologyAnatomyPapSmearQuery GetDynamicQuery()
		{
			return null;
		}

		public esPathologyAnatomyPapSmear()
		{
		}

		public esPathologyAnatomyPapSmear(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String resultNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(resultNo);
			else
				return LoadByPrimaryKeyStoredProcedure(resultNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String resultNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(resultNo);
			else
				return LoadByPrimaryKeyStoredProcedure(resultNo);
		}

		private bool LoadByPrimaryKeyDynamic(String resultNo)
		{
			esPathologyAnatomyPapSmearQuery query = this.GetDynamicQuery();
			query.Where(query.ResultNo == resultNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String resultNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ResultNo", resultNo);
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
						case "ResultNo": this.str.ResultNo = (string)value; break;
						case "MaturationIndex": this.str.MaturationIndex = (string)value; break;
						case "IsCytoplasmVacuolization": this.str.IsCytoplasmVacuolization = (string)value; break;
						case "IsCytoplasmic": this.str.IsCytoplasmic = (string)value; break;
						case "IsMultinucleation": this.str.IsMultinucleation = (string)value; break;
						case "IsKoilocyte": this.str.IsKoilocyte = (string)value; break;
						case "IsClueCell": this.str.IsClueCell = (string)value; break;
						case "IsMoldedCore": this.str.IsMoldedCore = (string)value; break;
						case "IsCoreGroundGlass": this.str.IsCoreGroundGlass = (string)value; break;
						case "IsAscUs": this.str.IsAscUs = (string)value; break;
						case "IsAscH": this.str.IsAscH = (string)value; break;
						case "IsLight": this.str.IsLight = (string)value; break;
						case "IsCurrently": this.str.IsCurrently = (string)value; break;
						case "IsHeavy": this.str.IsHeavy = (string)value; break;
						case "IsMalignantCells": this.str.IsMalignantCells = (string)value; break;
						case "IsTrichomonasvaginalis": this.str.IsTrichomonasvaginalis = (string)value; break;
						case "IsCandidaspp": this.str.IsCandidaspp = (string)value; break;
						case "IsActinomycesspp": this.str.IsActinomycesspp = (string)value; break;
						case "EndocervicalCells": this.str.EndocervicalCells = (string)value; break;
						case "IsEndometrialCells": this.str.IsEndometrialCells = (string)value; break;
						case "IsSquamousMetaplasia": this.str.IsSquamousMetaplasia = (string)value; break;
						case "IsAtypicalCells": this.str.IsAtypicalCells = (string)value; break;
						case "IsAdenocarcinomaInSitu": this.str.IsAdenocarcinomaInSitu = (string)value; break;
						case "IsAdenocarcinoma": this.str.IsAdenocarcinoma = (string)value; break;
						case "IsNeutrophils": this.str.IsNeutrophils = (string)value; break;
						case "IsLymphocytes": this.str.IsLymphocytes = (string)value; break;
						case "IsHistiocytes": this.str.IsHistiocytes = (string)value; break;
						case "IsOtherInflammatory": this.str.IsOtherInflammatory = (string)value; break;
						case "IsErythrocytes": this.str.IsErythrocytes = (string)value; break;
						case "IsSpermatozoa": this.str.IsSpermatozoa = (string)value; break;
						case "IsOtherFindings": this.str.IsOtherFindings = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "QtyAndCondition": this.str.QtyAndCondition = (string)value; break;
						case "PreparationType": this.str.PreparationType = (string)value; break;
						case "ClinicDescription": this.str.ClinicDescription = (string)value; break;
						case "PreparationEducation": this.str.PreparationEducation = (string)value; break;
						case "Diagnosis": this.str.Diagnosis = (string)value; break;
						case "Conclusion": this.str.Conclusion = (string)value; break;
						case "BethesdaSistem": this.str.BethesdaSistem = (string)value; break;
						case "Suggestion2": this.str.Suggestion2 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsCytoplasmVacuolization":

							if (value == null || value is System.Boolean)
								this.IsCytoplasmVacuolization = (System.Boolean?)value;
							break;
						case "IsCytoplasmic":

							if (value == null || value is System.Boolean)
								this.IsCytoplasmic = (System.Boolean?)value;
							break;
						case "IsMultinucleation":

							if (value == null || value is System.Boolean)
								this.IsMultinucleation = (System.Boolean?)value;
							break;
						case "IsKoilocyte":

							if (value == null || value is System.Boolean)
								this.IsKoilocyte = (System.Boolean?)value;
							break;
						case "IsClueCell":

							if (value == null || value is System.Boolean)
								this.IsClueCell = (System.Boolean?)value;
							break;
						case "IsMoldedCore":

							if (value == null || value is System.Boolean)
								this.IsMoldedCore = (System.Boolean?)value;
							break;
						case "IsCoreGroundGlass":

							if (value == null || value is System.Boolean)
								this.IsCoreGroundGlass = (System.Boolean?)value;
							break;
						case "IsAscUs":

							if (value == null || value is System.Boolean)
								this.IsAscUs = (System.Boolean?)value;
							break;
						case "IsAscH":

							if (value == null || value is System.Boolean)
								this.IsAscH = (System.Boolean?)value;
							break;
						case "IsLight":

							if (value == null || value is System.Boolean)
								this.IsLight = (System.Boolean?)value;
							break;
						case "IsCurrently":

							if (value == null || value is System.Boolean)
								this.IsCurrently = (System.Boolean?)value;
							break;
						case "IsHeavy":

							if (value == null || value is System.Boolean)
								this.IsHeavy = (System.Boolean?)value;
							break;
						case "IsMalignantCells":

							if (value == null || value is System.Boolean)
								this.IsMalignantCells = (System.Boolean?)value;
							break;
						case "IsTrichomonasvaginalis":

							if (value == null || value is System.Boolean)
								this.IsTrichomonasvaginalis = (System.Boolean?)value;
							break;
						case "IsCandidaspp":

							if (value == null || value is System.Boolean)
								this.IsCandidaspp = (System.Boolean?)value;
							break;
						case "IsActinomycesspp":

							if (value == null || value is System.Boolean)
								this.IsActinomycesspp = (System.Boolean?)value;
							break;
						case "IsEndometrialCells":

							if (value == null || value is System.Boolean)
								this.IsEndometrialCells = (System.Boolean?)value;
							break;
						case "IsSquamousMetaplasia":

							if (value == null || value is System.Boolean)
								this.IsSquamousMetaplasia = (System.Boolean?)value;
							break;
						case "IsAtypicalCells":

							if (value == null || value is System.Boolean)
								this.IsAtypicalCells = (System.Boolean?)value;
							break;
						case "IsAdenocarcinomaInSitu":

							if (value == null || value is System.Boolean)
								this.IsAdenocarcinomaInSitu = (System.Boolean?)value;
							break;
						case "IsAdenocarcinoma":

							if (value == null || value is System.Boolean)
								this.IsAdenocarcinoma = (System.Boolean?)value;
							break;
						case "IsNeutrophils":

							if (value == null || value is System.Boolean)
								this.IsNeutrophils = (System.Boolean?)value;
							break;
						case "IsLymphocytes":

							if (value == null || value is System.Boolean)
								this.IsLymphocytes = (System.Boolean?)value;
							break;
						case "IsHistiocytes":

							if (value == null || value is System.Boolean)
								this.IsHistiocytes = (System.Boolean?)value;
							break;
						case "IsOtherInflammatory":

							if (value == null || value is System.Boolean)
								this.IsOtherInflammatory = (System.Boolean?)value;
							break;
						case "IsErythrocytes":

							if (value == null || value is System.Boolean)
								this.IsErythrocytes = (System.Boolean?)value;
							break;
						case "IsSpermatozoa":

							if (value == null || value is System.Boolean)
								this.IsSpermatozoa = (System.Boolean?)value;
							break;
						case "IsOtherFindings":

							if (value == null || value is System.Boolean)
								this.IsOtherFindings = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to PathologyAnatomyPapSmear.ResultNo
		/// </summary>
		virtual public System.String ResultNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.ResultNo);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.ResultNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.MaturationIndex
		/// </summary>
		virtual public System.String MaturationIndex
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.MaturationIndex);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.MaturationIndex, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsCytoplasmVacuolization
		/// </summary>
		virtual public System.Boolean? IsCytoplasmVacuolization
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmVacuolization);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmVacuolization, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsCytoplasmic
		/// </summary>
		virtual public System.Boolean? IsCytoplasmic
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmic);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmic, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsMultinucleation
		/// </summary>
		virtual public System.Boolean? IsMultinucleation
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMultinucleation);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMultinucleation, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsKoilocyte
		/// </summary>
		virtual public System.Boolean? IsKoilocyte
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsKoilocyte);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsKoilocyte, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsClueCell
		/// </summary>
		virtual public System.Boolean? IsClueCell
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsClueCell);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsClueCell, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsMoldedCore
		/// </summary>
		virtual public System.Boolean? IsMoldedCore
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMoldedCore);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMoldedCore, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsCoreGroundGlass
		/// </summary>
		virtual public System.Boolean? IsCoreGroundGlass
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCoreGroundGlass);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCoreGroundGlass, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsAscUs
		/// </summary>
		virtual public System.Boolean? IsAscUs
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscUs);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscUs, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsAscH
		/// </summary>
		virtual public System.Boolean? IsAscH
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscH);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscH, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsLight
		/// </summary>
		virtual public System.Boolean? IsLight
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsLight);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsLight, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsCurrently
		/// </summary>
		virtual public System.Boolean? IsCurrently
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCurrently);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCurrently, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsHeavy
		/// </summary>
		virtual public System.Boolean? IsHeavy
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsHeavy);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsHeavy, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsMalignantCells
		/// </summary>
		virtual public System.Boolean? IsMalignantCells
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMalignantCells);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMalignantCells, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsTrichomonasvaginalis
		/// </summary>
		virtual public System.Boolean? IsTrichomonasvaginalis
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsTrichomonasvaginalis);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsTrichomonasvaginalis, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsCandidaspp
		/// </summary>
		virtual public System.Boolean? IsCandidaspp
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCandidaspp);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCandidaspp, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsActinomycesspp
		/// </summary>
		virtual public System.Boolean? IsActinomycesspp
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsActinomycesspp);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsActinomycesspp, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.EndocervicalCells
		/// </summary>
		virtual public System.String EndocervicalCells
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.EndocervicalCells);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.EndocervicalCells, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsEndometrialCells
		/// </summary>
		virtual public System.Boolean? IsEndometrialCells
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsEndometrialCells);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsEndometrialCells, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsSquamousMetaplasia
		/// </summary>
		virtual public System.Boolean? IsSquamousMetaplasia
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsSquamousMetaplasia);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsSquamousMetaplasia, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsAtypicalCells
		/// </summary>
		virtual public System.Boolean? IsAtypicalCells
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAtypicalCells);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAtypicalCells, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsAdenocarcinomaInSitu
		/// </summary>
		virtual public System.Boolean? IsAdenocarcinomaInSitu
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinomaInSitu);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinomaInSitu, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsAdenocarcinoma
		/// </summary>
		virtual public System.Boolean? IsAdenocarcinoma
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinoma);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinoma, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsNeutrophils
		/// </summary>
		virtual public System.Boolean? IsNeutrophils
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsNeutrophils);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsNeutrophils, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsLymphocytes
		/// </summary>
		virtual public System.Boolean? IsLymphocytes
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsLymphocytes);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsLymphocytes, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsHistiocytes
		/// </summary>
		virtual public System.Boolean? IsHistiocytes
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsHistiocytes);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsHistiocytes, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsOtherInflammatory
		/// </summary>
		virtual public System.Boolean? IsOtherInflammatory
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherInflammatory);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherInflammatory, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsErythrocytes
		/// </summary>
		virtual public System.Boolean? IsErythrocytes
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsErythrocytes);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsErythrocytes, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsSpermatozoa
		/// </summary>
		virtual public System.Boolean? IsSpermatozoa
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsSpermatozoa);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsSpermatozoa, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.IsOtherFindings
		/// </summary>
		virtual public System.Boolean? IsOtherFindings
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherFindings);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherFindings, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.QtyAndCondition
		/// </summary>
		virtual public System.String QtyAndCondition
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.QtyAndCondition);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.QtyAndCondition, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.PreparationType
		/// </summary>
		virtual public System.String PreparationType
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationType);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationType, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.ClinicDescription
		/// </summary>
		virtual public System.String ClinicDescription
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.ClinicDescription);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.ClinicDescription, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.PreparationEducation
		/// </summary>
		virtual public System.String PreparationEducation
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationEducation);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationEducation, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.Diagnosis
		/// </summary>
		virtual public System.String Diagnosis
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.Diagnosis);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.Diagnosis, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.Conclusion
		/// </summary>
		virtual public System.String Conclusion
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.Conclusion);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.Conclusion, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.BethesdaSistem
		/// </summary>
		virtual public System.String BethesdaSistem
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.BethesdaSistem);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.BethesdaSistem, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomyPapSmear.Suggestion2
		/// </summary>
		virtual public System.String Suggestion2
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.Suggestion2);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyPapSmearMetadata.ColumnNames.Suggestion2, value);
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
			public esStrings(esPathologyAnatomyPapSmear entity)
			{
				this.entity = entity;
			}
			public System.String ResultNo
			{
				get
				{
					System.String data = entity.ResultNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultNo = null;
					else entity.ResultNo = Convert.ToString(value);
				}
			}
			public System.String MaturationIndex
			{
				get
				{
					System.String data = entity.MaturationIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaturationIndex = null;
					else entity.MaturationIndex = Convert.ToString(value);
				}
			}
			public System.String IsCytoplasmVacuolization
			{
				get
				{
					System.Boolean? data = entity.IsCytoplasmVacuolization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCytoplasmVacuolization = null;
					else entity.IsCytoplasmVacuolization = Convert.ToBoolean(value);
				}
			}
			public System.String IsCytoplasmic
			{
				get
				{
					System.Boolean? data = entity.IsCytoplasmic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCytoplasmic = null;
					else entity.IsCytoplasmic = Convert.ToBoolean(value);
				}
			}
			public System.String IsMultinucleation
			{
				get
				{
					System.Boolean? data = entity.IsMultinucleation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMultinucleation = null;
					else entity.IsMultinucleation = Convert.ToBoolean(value);
				}
			}
			public System.String IsKoilocyte
			{
				get
				{
					System.Boolean? data = entity.IsKoilocyte;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKoilocyte = null;
					else entity.IsKoilocyte = Convert.ToBoolean(value);
				}
			}
			public System.String IsClueCell
			{
				get
				{
					System.Boolean? data = entity.IsClueCell;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClueCell = null;
					else entity.IsClueCell = Convert.ToBoolean(value);
				}
			}
			public System.String IsMoldedCore
			{
				get
				{
					System.Boolean? data = entity.IsMoldedCore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMoldedCore = null;
					else entity.IsMoldedCore = Convert.ToBoolean(value);
				}
			}
			public System.String IsCoreGroundGlass
			{
				get
				{
					System.Boolean? data = entity.IsCoreGroundGlass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCoreGroundGlass = null;
					else entity.IsCoreGroundGlass = Convert.ToBoolean(value);
				}
			}
			public System.String IsAscUs
			{
				get
				{
					System.Boolean? data = entity.IsAscUs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAscUs = null;
					else entity.IsAscUs = Convert.ToBoolean(value);
				}
			}
			public System.String IsAscH
			{
				get
				{
					System.Boolean? data = entity.IsAscH;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAscH = null;
					else entity.IsAscH = Convert.ToBoolean(value);
				}
			}
			public System.String IsLight
			{
				get
				{
					System.Boolean? data = entity.IsLight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLight = null;
					else entity.IsLight = Convert.ToBoolean(value);
				}
			}
			public System.String IsCurrently
			{
				get
				{
					System.Boolean? data = entity.IsCurrently;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCurrently = null;
					else entity.IsCurrently = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeavy
			{
				get
				{
					System.Boolean? data = entity.IsHeavy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeavy = null;
					else entity.IsHeavy = Convert.ToBoolean(value);
				}
			}
			public System.String IsMalignantCells
			{
				get
				{
					System.Boolean? data = entity.IsMalignantCells;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMalignantCells = null;
					else entity.IsMalignantCells = Convert.ToBoolean(value);
				}
			}
			public System.String IsTrichomonasvaginalis
			{
				get
				{
					System.Boolean? data = entity.IsTrichomonasvaginalis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTrichomonasvaginalis = null;
					else entity.IsTrichomonasvaginalis = Convert.ToBoolean(value);
				}
			}
			public System.String IsCandidaspp
			{
				get
				{
					System.Boolean? data = entity.IsCandidaspp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCandidaspp = null;
					else entity.IsCandidaspp = Convert.ToBoolean(value);
				}
			}
			public System.String IsActinomycesspp
			{
				get
				{
					System.Boolean? data = entity.IsActinomycesspp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActinomycesspp = null;
					else entity.IsActinomycesspp = Convert.ToBoolean(value);
				}
			}
			public System.String EndocervicalCells
			{
				get
				{
					System.String data = entity.EndocervicalCells;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndocervicalCells = null;
					else entity.EndocervicalCells = Convert.ToString(value);
				}
			}
			public System.String IsEndometrialCells
			{
				get
				{
					System.Boolean? data = entity.IsEndometrialCells;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEndometrialCells = null;
					else entity.IsEndometrialCells = Convert.ToBoolean(value);
				}
			}
			public System.String IsSquamousMetaplasia
			{
				get
				{
					System.Boolean? data = entity.IsSquamousMetaplasia;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSquamousMetaplasia = null;
					else entity.IsSquamousMetaplasia = Convert.ToBoolean(value);
				}
			}
			public System.String IsAtypicalCells
			{
				get
				{
					System.Boolean? data = entity.IsAtypicalCells;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAtypicalCells = null;
					else entity.IsAtypicalCells = Convert.ToBoolean(value);
				}
			}
			public System.String IsAdenocarcinomaInSitu
			{
				get
				{
					System.Boolean? data = entity.IsAdenocarcinomaInSitu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdenocarcinomaInSitu = null;
					else entity.IsAdenocarcinomaInSitu = Convert.ToBoolean(value);
				}
			}
			public System.String IsAdenocarcinoma
			{
				get
				{
					System.Boolean? data = entity.IsAdenocarcinoma;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdenocarcinoma = null;
					else entity.IsAdenocarcinoma = Convert.ToBoolean(value);
				}
			}
			public System.String IsNeutrophils
			{
				get
				{
					System.Boolean? data = entity.IsNeutrophils;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeutrophils = null;
					else entity.IsNeutrophils = Convert.ToBoolean(value);
				}
			}
			public System.String IsLymphocytes
			{
				get
				{
					System.Boolean? data = entity.IsLymphocytes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLymphocytes = null;
					else entity.IsLymphocytes = Convert.ToBoolean(value);
				}
			}
			public System.String IsHistiocytes
			{
				get
				{
					System.Boolean? data = entity.IsHistiocytes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHistiocytes = null;
					else entity.IsHistiocytes = Convert.ToBoolean(value);
				}
			}
			public System.String IsOtherInflammatory
			{
				get
				{
					System.Boolean? data = entity.IsOtherInflammatory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOtherInflammatory = null;
					else entity.IsOtherInflammatory = Convert.ToBoolean(value);
				}
			}
			public System.String IsErythrocytes
			{
				get
				{
					System.Boolean? data = entity.IsErythrocytes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsErythrocytes = null;
					else entity.IsErythrocytes = Convert.ToBoolean(value);
				}
			}
			public System.String IsSpermatozoa
			{
				get
				{
					System.Boolean? data = entity.IsSpermatozoa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSpermatozoa = null;
					else entity.IsSpermatozoa = Convert.ToBoolean(value);
				}
			}
			public System.String IsOtherFindings
			{
				get
				{
					System.Boolean? data = entity.IsOtherFindings;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOtherFindings = null;
					else entity.IsOtherFindings = Convert.ToBoolean(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			public System.String QtyAndCondition
			{
				get
				{
					System.String data = entity.QtyAndCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyAndCondition = null;
					else entity.QtyAndCondition = Convert.ToString(value);
				}
			}
			public System.String PreparationType
			{
				get
				{
					System.String data = entity.PreparationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreparationType = null;
					else entity.PreparationType = Convert.ToString(value);
				}
			}
			public System.String ClinicDescription
			{
				get
				{
					System.String data = entity.ClinicDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicDescription = null;
					else entity.ClinicDescription = Convert.ToString(value);
				}
			}
			public System.String PreparationEducation
			{
				get
				{
					System.String data = entity.PreparationEducation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreparationEducation = null;
					else entity.PreparationEducation = Convert.ToString(value);
				}
			}
			public System.String Diagnosis
			{
				get
				{
					System.String data = entity.Diagnosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Diagnosis = null;
					else entity.Diagnosis = Convert.ToString(value);
				}
			}
			public System.String Conclusion
			{
				get
				{
					System.String data = entity.Conclusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Conclusion = null;
					else entity.Conclusion = Convert.ToString(value);
				}
			}
			public System.String BethesdaSistem
			{
				get
				{
					System.String data = entity.BethesdaSistem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BethesdaSistem = null;
					else entity.BethesdaSistem = Convert.ToString(value);
				}
			}
			public System.String Suggestion2
			{
				get
				{
					System.String data = entity.Suggestion2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Suggestion2 = null;
					else entity.Suggestion2 = Convert.ToString(value);
				}
			}
			private esPathologyAnatomyPapSmear entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPathologyAnatomyPapSmearQuery query)
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
				throw new Exception("esPathologyAnatomyPapSmear can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PathologyAnatomyPapSmear : esPathologyAnatomyPapSmear
	{
	}

	[Serializable]
	abstract public class esPathologyAnatomyPapSmearQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PathologyAnatomyPapSmearMetadata.Meta();
			}
		}

		public esQueryItem ResultNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.ResultNo, esSystemType.String);
			}
		}

		public esQueryItem MaturationIndex
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.MaturationIndex, esSystemType.String);
			}
		}

		public esQueryItem IsCytoplasmVacuolization
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmVacuolization, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCytoplasmic
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmic, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMultinucleation
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsMultinucleation, esSystemType.Boolean);
			}
		}

		public esQueryItem IsKoilocyte
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsKoilocyte, esSystemType.Boolean);
			}
		}

		public esQueryItem IsClueCell
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsClueCell, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMoldedCore
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsMoldedCore, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCoreGroundGlass
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsCoreGroundGlass, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAscUs
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscUs, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAscH
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscH, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLight
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsLight, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCurrently
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsCurrently, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeavy
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsHeavy, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMalignantCells
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsMalignantCells, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTrichomonasvaginalis
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsTrichomonasvaginalis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCandidaspp
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsCandidaspp, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActinomycesspp
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsActinomycesspp, esSystemType.Boolean);
			}
		}

		public esQueryItem EndocervicalCells
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.EndocervicalCells, esSystemType.String);
			}
		}

		public esQueryItem IsEndometrialCells
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsEndometrialCells, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSquamousMetaplasia
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsSquamousMetaplasia, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAtypicalCells
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsAtypicalCells, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAdenocarcinomaInSitu
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinomaInSitu, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAdenocarcinoma
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinoma, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNeutrophils
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsNeutrophils, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLymphocytes
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsLymphocytes, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHistiocytes
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsHistiocytes, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOtherInflammatory
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherInflammatory, esSystemType.Boolean);
			}
		}

		public esQueryItem IsErythrocytes
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsErythrocytes, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSpermatozoa
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsSpermatozoa, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOtherFindings
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherFindings, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem QtyAndCondition
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.QtyAndCondition, esSystemType.String);
			}
		}

		public esQueryItem PreparationType
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationType, esSystemType.String);
			}
		}

		public esQueryItem ClinicDescription
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.ClinicDescription, esSystemType.String);
			}
		}

		public esQueryItem PreparationEducation
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationEducation, esSystemType.String);
			}
		}

		public esQueryItem Diagnosis
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.Diagnosis, esSystemType.String);
			}
		}

		public esQueryItem Conclusion
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.Conclusion, esSystemType.String);
			}
		}

		public esQueryItem BethesdaSistem
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.BethesdaSistem, esSystemType.String);
			}
		}

		public esQueryItem Suggestion2
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyPapSmearMetadata.ColumnNames.Suggestion2, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PathologyAnatomyPapSmearCollection")]
	public partial class PathologyAnatomyPapSmearCollection : esPathologyAnatomyPapSmearCollection, IEnumerable<PathologyAnatomyPapSmear>
	{
		public PathologyAnatomyPapSmearCollection()
		{

		}

		public static implicit operator List<PathologyAnatomyPapSmear>(PathologyAnatomyPapSmearCollection coll)
		{
			List<PathologyAnatomyPapSmear> list = new List<PathologyAnatomyPapSmear>();

			foreach (PathologyAnatomyPapSmear emp in coll)
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
				return PathologyAnatomyPapSmearMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PathologyAnatomyPapSmearQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PathologyAnatomyPapSmear(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PathologyAnatomyPapSmear();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PathologyAnatomyPapSmearQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PathologyAnatomyPapSmearQuery();
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
		public bool Load(PathologyAnatomyPapSmearQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PathologyAnatomyPapSmear AddNew()
		{
			PathologyAnatomyPapSmear entity = base.AddNewEntity() as PathologyAnatomyPapSmear;

			return entity;
		}
		public PathologyAnatomyPapSmear FindByPrimaryKey(String resultNo)
		{
			return base.FindByPrimaryKey(resultNo) as PathologyAnatomyPapSmear;
		}

		#region IEnumerable< PathologyAnatomyPapSmear> Members

		IEnumerator<PathologyAnatomyPapSmear> IEnumerable<PathologyAnatomyPapSmear>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PathologyAnatomyPapSmear;
			}
		}

		#endregion

		private PathologyAnatomyPapSmearQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PathologyAnatomyPapSmear' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PathologyAnatomyPapSmear ({ResultNo})")]
	[Serializable]
	public partial class PathologyAnatomyPapSmear : esPathologyAnatomyPapSmear
	{
		public PathologyAnatomyPapSmear()
		{
		}

		public PathologyAnatomyPapSmear(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PathologyAnatomyPapSmearMetadata.Meta();
			}
		}

		override protected esPathologyAnatomyPapSmearQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PathologyAnatomyPapSmearQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PathologyAnatomyPapSmearQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PathologyAnatomyPapSmearQuery();
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
		public bool Load(PathologyAnatomyPapSmearQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PathologyAnatomyPapSmearQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PathologyAnatomyPapSmearQuery : esPathologyAnatomyPapSmearQuery
	{
		public PathologyAnatomyPapSmearQuery()
		{

		}

		public PathologyAnatomyPapSmearQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PathologyAnatomyPapSmearQuery";
		}
	}

	[Serializable]
	public partial class PathologyAnatomyPapSmearMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PathologyAnatomyPapSmearMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.ResultNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.ResultNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.MaturationIndex, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.MaturationIndex;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmVacuolization, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsCytoplasmVacuolization;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCytoplasmic, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsCytoplasmic;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMultinucleation, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsMultinucleation;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsKoilocyte, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsKoilocyte;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsClueCell, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsClueCell;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMoldedCore, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsMoldedCore;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCoreGroundGlass, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsCoreGroundGlass;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscUs, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsAscUs;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAscH, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsAscH;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsLight, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsLight;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCurrently, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsCurrently;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsHeavy, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsHeavy;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsMalignantCells, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsMalignantCells;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsTrichomonasvaginalis, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsTrichomonasvaginalis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsCandidaspp, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsCandidaspp;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsActinomycesspp, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsActinomycesspp;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.EndocervicalCells, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.EndocervicalCells;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsEndometrialCells, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsEndometrialCells;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsSquamousMetaplasia, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsSquamousMetaplasia;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAtypicalCells, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsAtypicalCells;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinomaInSitu, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsAdenocarcinomaInSitu;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsAdenocarcinoma, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsAdenocarcinoma;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsNeutrophils, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsNeutrophils;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsLymphocytes, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsLymphocytes;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsHistiocytes, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsHistiocytes;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherInflammatory, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsOtherInflammatory;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsErythrocytes, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsErythrocytes;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsSpermatozoa, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsSpermatozoa;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.IsOtherFindings, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.IsOtherFindings;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.CreatedByUserID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateDateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.LastUpdateByUserID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.QtyAndCondition, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.QtyAndCondition;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationType, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.PreparationType;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.ClinicDescription, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.ClinicDescription;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.PreparationEducation, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.PreparationEducation;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.Diagnosis, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.Diagnosis;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.Conclusion, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.Conclusion;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.BethesdaSistem, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.BethesdaSistem;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyPapSmearMetadata.ColumnNames.Suggestion2, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyPapSmearMetadata.PropertyNames.Suggestion2;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PathologyAnatomyPapSmearMetadata Meta()
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
			public const string ResultNo = "ResultNo";
			public const string MaturationIndex = "MaturationIndex";
			public const string IsCytoplasmVacuolization = "IsCytoplasmVacuolization";
			public const string IsCytoplasmic = "IsCytoplasmic";
			public const string IsMultinucleation = "IsMultinucleation";
			public const string IsKoilocyte = "IsKoilocyte";
			public const string IsClueCell = "IsClueCell";
			public const string IsMoldedCore = "IsMoldedCore";
			public const string IsCoreGroundGlass = "IsCoreGroundGlass";
			public const string IsAscUs = "IsAscUs";
			public const string IsAscH = "IsAscH";
			public const string IsLight = "IsLight";
			public const string IsCurrently = "IsCurrently";
			public const string IsHeavy = "IsHeavy";
			public const string IsMalignantCells = "IsMalignantCells";
			public const string IsTrichomonasvaginalis = "IsTrichomonasvaginalis";
			public const string IsCandidaspp = "IsCandidaspp";
			public const string IsActinomycesspp = "IsActinomycesspp";
			public const string EndocervicalCells = "EndocervicalCells";
			public const string IsEndometrialCells = "IsEndometrialCells";
			public const string IsSquamousMetaplasia = "IsSquamousMetaplasia";
			public const string IsAtypicalCells = "IsAtypicalCells";
			public const string IsAdenocarcinomaInSitu = "IsAdenocarcinomaInSitu";
			public const string IsAdenocarcinoma = "IsAdenocarcinoma";
			public const string IsNeutrophils = "IsNeutrophils";
			public const string IsLymphocytes = "IsLymphocytes";
			public const string IsHistiocytes = "IsHistiocytes";
			public const string IsOtherInflammatory = "IsOtherInflammatory";
			public const string IsErythrocytes = "IsErythrocytes";
			public const string IsSpermatozoa = "IsSpermatozoa";
			public const string IsOtherFindings = "IsOtherFindings";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string QtyAndCondition = "QtyAndCondition";
			public const string PreparationType = "PreparationType";
			public const string ClinicDescription = "ClinicDescription";
			public const string PreparationEducation = "PreparationEducation";
			public const string Diagnosis = "Diagnosis";
			public const string Conclusion = "Conclusion";
			public const string BethesdaSistem = "BethesdaSistem";
			public const string Suggestion2 = "Suggestion2";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ResultNo = "ResultNo";
			public const string MaturationIndex = "MaturationIndex";
			public const string IsCytoplasmVacuolization = "IsCytoplasmVacuolization";
			public const string IsCytoplasmic = "IsCytoplasmic";
			public const string IsMultinucleation = "IsMultinucleation";
			public const string IsKoilocyte = "IsKoilocyte";
			public const string IsClueCell = "IsClueCell";
			public const string IsMoldedCore = "IsMoldedCore";
			public const string IsCoreGroundGlass = "IsCoreGroundGlass";
			public const string IsAscUs = "IsAscUs";
			public const string IsAscH = "IsAscH";
			public const string IsLight = "IsLight";
			public const string IsCurrently = "IsCurrently";
			public const string IsHeavy = "IsHeavy";
			public const string IsMalignantCells = "IsMalignantCells";
			public const string IsTrichomonasvaginalis = "IsTrichomonasvaginalis";
			public const string IsCandidaspp = "IsCandidaspp";
			public const string IsActinomycesspp = "IsActinomycesspp";
			public const string EndocervicalCells = "EndocervicalCells";
			public const string IsEndometrialCells = "IsEndometrialCells";
			public const string IsSquamousMetaplasia = "IsSquamousMetaplasia";
			public const string IsAtypicalCells = "IsAtypicalCells";
			public const string IsAdenocarcinomaInSitu = "IsAdenocarcinomaInSitu";
			public const string IsAdenocarcinoma = "IsAdenocarcinoma";
			public const string IsNeutrophils = "IsNeutrophils";
			public const string IsLymphocytes = "IsLymphocytes";
			public const string IsHistiocytes = "IsHistiocytes";
			public const string IsOtherInflammatory = "IsOtherInflammatory";
			public const string IsErythrocytes = "IsErythrocytes";
			public const string IsSpermatozoa = "IsSpermatozoa";
			public const string IsOtherFindings = "IsOtherFindings";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string QtyAndCondition = "QtyAndCondition";
			public const string PreparationType = "PreparationType";
			public const string ClinicDescription = "ClinicDescription";
			public const string PreparationEducation = "PreparationEducation";
			public const string Diagnosis = "Diagnosis";
			public const string Conclusion = "Conclusion";
			public const string BethesdaSistem = "BethesdaSistem";
			public const string Suggestion2 = "Suggestion2";
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
			lock (typeof(PathologyAnatomyPapSmearMetadata))
			{
				if (PathologyAnatomyPapSmearMetadata.mapDelegates == null)
				{
					PathologyAnatomyPapSmearMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PathologyAnatomyPapSmearMetadata.meta == null)
				{
					PathologyAnatomyPapSmearMetadata.meta = new PathologyAnatomyPapSmearMetadata();
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

				meta.AddTypeMap("ResultNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaturationIndex", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCytoplasmVacuolization", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCytoplasmic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMultinucleation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsKoilocyte", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClueCell", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMoldedCore", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCoreGroundGlass", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAscUs", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAscH", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLight", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCurrently", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeavy", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMalignantCells", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTrichomonasvaginalis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCandidaspp", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActinomycesspp", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("EndocervicalCells", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEndometrialCells", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSquamousMetaplasia", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAtypicalCells", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAdenocarcinomaInSitu", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAdenocarcinoma", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNeutrophils", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLymphocytes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHistiocytes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOtherInflammatory", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsErythrocytes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSpermatozoa", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOtherFindings", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyAndCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreparationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreparationEducation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Diagnosis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Conclusion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BethesdaSistem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Suggestion2", new esTypeMap("varchar", "System.String"));


				meta.Source = "PathologyAnatomyPapSmear";
				meta.Destination = "PathologyAnatomyPapSmear";
				meta.spInsert = "proc_PathologyAnatomyPapSmearInsert";
				meta.spUpdate = "proc_PathologyAnatomyPapSmearUpdate";
				meta.spDelete = "proc_PathologyAnatomyPapSmearDelete";
				meta.spLoadAll = "proc_PathologyAnatomyPapSmearLoadAll";
				meta.spLoadByPrimaryKey = "proc_PathologyAnatomyPapSmearLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PathologyAnatomyPapSmearMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
