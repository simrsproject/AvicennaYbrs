/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/26/2023 1:49:22 PM
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
	abstract public class esEpisodeProcedureCollection : esEntityCollectionWAuditLog
	{
		public esEpisodeProcedureCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EpisodeProcedureCollection";
		}

		#region Query Logic
		protected void InitQuery(esEpisodeProcedureQuery query)
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
			this.InitQuery(query as esEpisodeProcedureQuery);
		}
		#endregion

		virtual public EpisodeProcedure DetachEntity(EpisodeProcedure entity)
		{
			return base.DetachEntity(entity) as EpisodeProcedure;
		}

		virtual public EpisodeProcedure AttachEntity(EpisodeProcedure entity)
		{
			return base.AttachEntity(entity) as EpisodeProcedure;
		}

		virtual public void Combine(EpisodeProcedureCollection collection)
		{
			base.Combine(collection);
		}

		new public EpisodeProcedure this[int index]
		{
			get
			{
				return base[index] as EpisodeProcedure;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EpisodeProcedure);
		}
	}

	[Serializable]
	abstract public class esEpisodeProcedure : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEpisodeProcedureQuery GetDynamicQuery()
		{
			return null;
		}

		public esEpisodeProcedure()
		{
		}

		public esEpisodeProcedure(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, String sequenceNo)
		{
			esEpisodeProcedureQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("SequenceNo", sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ProcedureDate": this.str.ProcedureDate = (string)value; break;
						case "ProcedureTime": this.str.ProcedureTime = (string)value; break;
						case "ProcedureDate2": this.str.ProcedureDate2 = (string)value; break;
						case "ProcedureTime2": this.str.ProcedureTime2 = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ParamedicID2": this.str.ParamedicID2 = (string)value; break;
						case "ProcedureID": this.str.ProcedureID = (string)value; break;
						case "SRProcedureCategory": this.str.SRProcedureCategory = (string)value; break;
						case "SRAnestesi": this.str.SRAnestesi = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "IsCito": this.str.IsCito = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "AssistantID1": this.str.AssistantID1 = (string)value; break;
						case "AssistantID2": this.str.AssistantID2 = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "BookingNo": this.str.BookingNo = (string)value; break;
						case "ParamedicID2a": this.str.ParamedicID2a = (string)value; break;
						case "ParamedicID3a": this.str.ParamedicID3a = (string)value; break;
						case "ParamedicID4a": this.str.ParamedicID4a = (string)value; break;
						case "ParamedicIDAnestesi": this.str.ParamedicIDAnestesi = (string)value; break;
						case "AssistantIDAnestesi": this.str.AssistantIDAnestesi = (string)value; break;
						case "InstrumentatorID1": this.str.InstrumentatorID1 = (string)value; break;
						case "InstrumentatorID2": this.str.InstrumentatorID2 = (string)value; break;
						case "IsFromOperatingRoom": this.str.IsFromOperatingRoom = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "AnestesyNotes": this.str.AnestesyNotes = (string)value; break;
						case "ProcedureName": this.str.ProcedureName = (string)value; break;
						case "OpNotesSeqNo": this.str.OpNotesSeqNo = (string)value; break;
						case "IncisionDateTime": this.str.IncisionDateTime = (string)value; break;
						case "RegistrationInfoMedicID": this.str.RegistrationInfoMedicID = (string)value; break;
						case "OperatingNotes": this.str.OperatingNotes = (string)value; break;
						case "AssistantIDAnestesi2": this.str.AssistantIDAnestesi2 = (string)value; break;
                        case "ProcedureSynonym": this.str.ProcedureSynonym = (string)value; break;
                        case "QtyICD": this.str.QtyICD = (string)value; break;
                    }
				}
				else
				{
					switch (name)
					{
						case "ProcedureDate":

							if (value == null || value is System.DateTime)
								this.ProcedureDate = (System.DateTime?)value;
							break;
						case "ProcedureDate2":

							if (value == null || value is System.DateTime)
								this.ProcedureDate2 = (System.DateTime?)value;
							break;
						case "IsCito":

							if (value == null || value is System.Boolean)
								this.IsCito = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsFromOperatingRoom":

							if (value == null || value is System.Boolean)
								this.IsFromOperatingRoom = (System.Boolean?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "IncisionDateTime":

							if (value == null || value is System.DateTime)
								this.IncisionDateTime = (System.DateTime?)value;
							break;
                        case "QtyICD":

                            if (value == null || value is System.Int32)
                                this.QtyICD = (System.Int32?)value;
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
		/// Maps to EpisodeProcedure.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ProcedureDate
		/// </summary>
		virtual public System.DateTime? ProcedureDate
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.ProcedureDate);
			}

			set
			{
				base.SetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.ProcedureDate, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ProcedureTime
		/// </summary>
		virtual public System.String ProcedureTime
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureTime);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureTime, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ProcedureDate2
		/// </summary>
		virtual public System.DateTime? ProcedureDate2
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.ProcedureDate2);
			}

			set
			{
				base.SetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.ProcedureDate2, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ProcedureTime2
		/// </summary>
		virtual public System.String ProcedureTime2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureTime2);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureTime2, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ParamedicID2
		/// </summary>
		virtual public System.String ParamedicID2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID2);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID2, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ProcedureID
		/// </summary>
		virtual public System.String ProcedureID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureID);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureID, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.SRProcedureCategory
		/// </summary>
		virtual public System.String SRProcedureCategory
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.SRProcedureCategory);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.SRProcedureCategory, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.SRAnestesi
		/// </summary>
		virtual public System.String SRAnestesi
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.SRAnestesi);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.SRAnestesi, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.IsCito
		/// </summary>
		virtual public System.Boolean? IsCito
		{
			get
			{
				return base.GetSystemBoolean(EpisodeProcedureMetadata.ColumnNames.IsCito);
			}

			set
			{
				base.SetSystemBoolean(EpisodeProcedureMetadata.ColumnNames.IsCito, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EpisodeProcedureMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EpisodeProcedureMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.AssistantID1
		/// </summary>
		virtual public System.String AssistantID1
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantID1);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantID1, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.AssistantID2
		/// </summary>
		virtual public System.String AssistantID2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantID2);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantID2, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.BookingNo
		/// </summary>
		virtual public System.String BookingNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.BookingNo);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.BookingNo, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ParamedicID2a
		/// </summary>
		virtual public System.String ParamedicID2a
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID2a);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID2a, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ParamedicID3a
		/// </summary>
		virtual public System.String ParamedicID3a
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID3a);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID3a, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ParamedicID4a
		/// </summary>
		virtual public System.String ParamedicID4a
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID4a);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicID4a, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ParamedicIDAnestesi
		/// </summary>
		virtual public System.String ParamedicIDAnestesi
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicIDAnestesi);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ParamedicIDAnestesi, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.AssistantIDAnestesi
		/// </summary>
		virtual public System.String AssistantIDAnestesi
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.InstrumentatorID1
		/// </summary>
		virtual public System.String InstrumentatorID1
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.InstrumentatorID1);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.InstrumentatorID1, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.InstrumentatorID2
		/// </summary>
		virtual public System.String InstrumentatorID2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.InstrumentatorID2);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.InstrumentatorID2, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.IsFromOperatingRoom
		/// </summary>
		virtual public System.Boolean? IsFromOperatingRoom
		{
			get
			{
				return base.GetSystemBoolean(EpisodeProcedureMetadata.ColumnNames.IsFromOperatingRoom);
			}

			set
			{
				base.SetSystemBoolean(EpisodeProcedureMetadata.ColumnNames.IsFromOperatingRoom, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.AnestesyNotes
		/// </summary>
		virtual public System.String AnestesyNotes
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.AnestesyNotes);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.AnestesyNotes, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.ProcedureName
		/// </summary>
		virtual public System.String ProcedureName
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureName);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureName, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.OpNotesSeqNo
		/// </summary>
		virtual public System.String OpNotesSeqNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.OpNotesSeqNo);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.OpNotesSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.IncisionDateTime
		/// </summary>
		virtual public System.DateTime? IncisionDateTime
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.IncisionDateTime);
			}

			set
			{
				base.SetSystemDateTime(EpisodeProcedureMetadata.ColumnNames.IncisionDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.RegistrationInfoMedicID
		/// </summary>
		virtual public System.String RegistrationInfoMedicID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.RegistrationInfoMedicID);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.RegistrationInfoMedicID, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.OperatingNotes
		/// </summary>
		virtual public System.String OperatingNotes
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.OperatingNotes);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.OperatingNotes, value);
			}
		}
		/// <summary>
		/// Maps to EpisodeProcedure.AssistantIDAnestesi2
		/// </summary>
		virtual public System.String AssistantIDAnestesi2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi2);
			}

			set
			{
				base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi2, value);
			}
		}
        /// <summary>
        /// Maps to EpisodeProcedure.ProcedureSynonym
        /// </summary>
        virtual public System.String ProcedureSynonym
        {
            get
            {
                return base.GetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureSynonym);
            }

            set
            {
                base.SetSystemString(EpisodeProcedureMetadata.ColumnNames.ProcedureSynonym, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeProcedure.QtyICD
        /// </summary>
        virtual public System.Int32? QtyICD
        {
            get
            {
                return base.GetSystemInt32(EpisodeProcedureMetadata.ColumnNames.QtyICD);
            }

            set
            {
                base.SetSystemInt32(EpisodeProcedureMetadata.ColumnNames.QtyICD, value);
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
			public esStrings(esEpisodeProcedure entity)
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
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String ProcedureDate
			{
				get
				{
					System.DateTime? data = entity.ProcedureDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureDate = null;
					else entity.ProcedureDate = Convert.ToDateTime(value);
				}
			}
			public System.String ProcedureTime
			{
				get
				{
					System.String data = entity.ProcedureTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureTime = null;
					else entity.ProcedureTime = Convert.ToString(value);
				}
			}
			public System.String ProcedureDate2
			{
				get
				{
					System.DateTime? data = entity.ProcedureDate2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureDate2 = null;
					else entity.ProcedureDate2 = Convert.ToDateTime(value);
				}
			}
			public System.String ProcedureTime2
			{
				get
				{
					System.String data = entity.ProcedureTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureTime2 = null;
					else entity.ProcedureTime2 = Convert.ToString(value);
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
			public System.String ParamedicID2
			{
				get
				{
					System.String data = entity.ParamedicID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID2 = null;
					else entity.ParamedicID2 = Convert.ToString(value);
				}
			}
			public System.String ProcedureID
			{
				get
				{
					System.String data = entity.ProcedureID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureID = null;
					else entity.ProcedureID = Convert.ToString(value);
				}
			}
			public System.String SRProcedureCategory
			{
				get
				{
					System.String data = entity.SRProcedureCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedureCategory = null;
					else entity.SRProcedureCategory = Convert.ToString(value);
				}
			}
			public System.String SRAnestesi
			{
				get
				{
					System.String data = entity.SRAnestesi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAnestesi = null;
					else entity.SRAnestesi = Convert.ToString(value);
				}
			}
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
				}
			}
			public System.String IsCito
			{
				get
				{
					System.Boolean? data = entity.IsCito;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCito = null;
					else entity.IsCito = Convert.ToBoolean(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
			public System.String AssistantID1
			{
				get
				{
					System.String data = entity.AssistantID1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantID1 = null;
					else entity.AssistantID1 = Convert.ToString(value);
				}
			}
			public System.String AssistantID2
			{
				get
				{
					System.String data = entity.AssistantID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantID2 = null;
					else entity.AssistantID2 = Convert.ToString(value);
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
			public System.String BookingNo
			{
				get
				{
					System.String data = entity.BookingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingNo = null;
					else entity.BookingNo = Convert.ToString(value);
				}
			}
			public System.String ParamedicID2a
			{
				get
				{
					System.String data = entity.ParamedicID2a;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID2a = null;
					else entity.ParamedicID2a = Convert.ToString(value);
				}
			}
			public System.String ParamedicID3a
			{
				get
				{
					System.String data = entity.ParamedicID3a;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID3a = null;
					else entity.ParamedicID3a = Convert.ToString(value);
				}
			}
			public System.String ParamedicID4a
			{
				get
				{
					System.String data = entity.ParamedicID4a;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID4a = null;
					else entity.ParamedicID4a = Convert.ToString(value);
				}
			}
			public System.String ParamedicIDAnestesi
			{
				get
				{
					System.String data = entity.ParamedicIDAnestesi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicIDAnestesi = null;
					else entity.ParamedicIDAnestesi = Convert.ToString(value);
				}
			}
			public System.String AssistantIDAnestesi
			{
				get
				{
					System.String data = entity.AssistantIDAnestesi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDAnestesi = null;
					else entity.AssistantIDAnestesi = Convert.ToString(value);
				}
			}
			public System.String InstrumentatorID1
			{
				get
				{
					System.String data = entity.InstrumentatorID1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstrumentatorID1 = null;
					else entity.InstrumentatorID1 = Convert.ToString(value);
				}
			}
			public System.String InstrumentatorID2
			{
				get
				{
					System.String data = entity.InstrumentatorID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstrumentatorID2 = null;
					else entity.InstrumentatorID2 = Convert.ToString(value);
				}
			}
			public System.String IsFromOperatingRoom
			{
				get
				{
					System.Boolean? data = entity.IsFromOperatingRoom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromOperatingRoom = null;
					else entity.IsFromOperatingRoom = Convert.ToBoolean(value);
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
			public System.String AnestesyNotes
			{
				get
				{
					System.String data = entity.AnestesyNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnestesyNotes = null;
					else entity.AnestesyNotes = Convert.ToString(value);
				}
			}
			public System.String ProcedureName
			{
				get
				{
					System.String data = entity.ProcedureName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureName = null;
					else entity.ProcedureName = Convert.ToString(value);
				}
			}
			public System.String OpNotesSeqNo
			{
				get
				{
					System.String data = entity.OpNotesSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpNotesSeqNo = null;
					else entity.OpNotesSeqNo = Convert.ToString(value);
				}
			}
			public System.String IncisionDateTime
			{
				get
				{
					System.DateTime? data = entity.IncisionDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncisionDateTime = null;
					else entity.IncisionDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RegistrationInfoMedicID
			{
				get
				{
					System.String data = entity.RegistrationInfoMedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationInfoMedicID = null;
					else entity.RegistrationInfoMedicID = Convert.ToString(value);
				}
			}
			public System.String OperatingNotes
			{
				get
				{
					System.String data = entity.OperatingNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperatingNotes = null;
					else entity.OperatingNotes = Convert.ToString(value);
				}
			}
			public System.String AssistantIDAnestesi2
			{
				get
				{
					System.String data = entity.AssistantIDAnestesi2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDAnestesi2 = null;
					else entity.AssistantIDAnestesi2 = Convert.ToString(value);
				}
            }
            public System.String ProcedureSynonym
            {
                get
                {
                    System.String data = entity.ProcedureSynonym;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureSynonym = null;
                    else entity.ProcedureSynonym = Convert.ToString(value);
                }
            }
            public System.String QtyICD
            {
                get
                {
                    System.Int32? data = entity.QtyICD;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QtyICD = null;
                    else entity.QtyICD = Convert.ToInt32(value);
                }
            }
            private esEpisodeProcedure entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEpisodeProcedureQuery query)
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
				throw new Exception("esEpisodeProcedure can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EpisodeProcedure : esEpisodeProcedure
	{
	}

	[Serializable]
	abstract public class esEpisodeProcedureQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EpisodeProcedureMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ProcedureDate
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ProcedureDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ProcedureTime
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ProcedureTime, esSystemType.String);
			}
		}

		public esQueryItem ProcedureDate2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ProcedureDate2, esSystemType.DateTime);
			}
		}

		public esQueryItem ProcedureTime2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ProcedureTime2, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ParamedicID2, esSystemType.String);
			}
		}

		public esQueryItem ProcedureID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ProcedureID, esSystemType.String);
			}
		}

		public esQueryItem SRProcedureCategory
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.SRProcedureCategory, esSystemType.String);
			}
		}

		public esQueryItem SRAnestesi
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.SRAnestesi, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem IsCito
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.IsCito, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem AssistantID1
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.AssistantID1, esSystemType.String);
			}
		}

		public esQueryItem AssistantID2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.AssistantID2, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem BookingNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.BookingNo, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID2a
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ParamedicID2a, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID3a
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ParamedicID3a, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID4a
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ParamedicID4a, esSystemType.String);
			}
		}

		public esQueryItem ParamedicIDAnestesi
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ParamedicIDAnestesi, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDAnestesi
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi, esSystemType.String);
			}
		}

		public esQueryItem InstrumentatorID1
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.InstrumentatorID1, esSystemType.String);
			}
		}

		public esQueryItem InstrumentatorID2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.InstrumentatorID2, esSystemType.String);
			}
		}

		public esQueryItem IsFromOperatingRoom
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.IsFromOperatingRoom, esSystemType.Boolean);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem AnestesyNotes
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.AnestesyNotes, esSystemType.String);
			}
		}

		public esQueryItem ProcedureName
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ProcedureName, esSystemType.String);
			}
		}

		public esQueryItem OpNotesSeqNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.OpNotesSeqNo, esSystemType.String);
			}
		}

		public esQueryItem IncisionDateTime
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.IncisionDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationInfoMedicID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
			}
		}

		public esQueryItem OperatingNotes
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.OperatingNotes, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDAnestesi2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi2, esSystemType.String);
			}
		}

        public esQueryItem ProcedureSynonym
        {
            get
            {
                return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.ProcedureSynonym, esSystemType.String);
            }
        }

        public esQueryItem QtyICD
        {
            get
            {
                return new esQueryItem(this, EpisodeProcedureMetadata.ColumnNames.QtyICD, esSystemType.Int32);
            }
        }
    }

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EpisodeProcedureCollection")]
	public partial class EpisodeProcedureCollection : esEpisodeProcedureCollection, IEnumerable<EpisodeProcedure>
	{
		public EpisodeProcedureCollection()
		{

		}

		public static implicit operator List<EpisodeProcedure>(EpisodeProcedureCollection coll)
		{
			List<EpisodeProcedure> list = new List<EpisodeProcedure>();

			foreach (EpisodeProcedure emp in coll)
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
				return EpisodeProcedureMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EpisodeProcedureQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EpisodeProcedure(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EpisodeProcedure();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EpisodeProcedureQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EpisodeProcedureQuery();
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
		public bool Load(EpisodeProcedureQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EpisodeProcedure AddNew()
		{
			EpisodeProcedure entity = base.AddNewEntity() as EpisodeProcedure;

			return entity;
		}
		public EpisodeProcedure FindByPrimaryKey(String registrationNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo) as EpisodeProcedure;
		}

		#region IEnumerable< EpisodeProcedure> Members

		IEnumerator<EpisodeProcedure> IEnumerable<EpisodeProcedure>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EpisodeProcedure;
			}
		}

		#endregion

		private EpisodeProcedureQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EpisodeProcedure' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EpisodeProcedure ({RegistrationNo, SequenceNo})")]
	[Serializable]
	public partial class EpisodeProcedure : esEpisodeProcedure
	{
		public EpisodeProcedure()
		{
		}

		public EpisodeProcedure(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EpisodeProcedureMetadata.Meta();
			}
		}

		override protected esEpisodeProcedureQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EpisodeProcedureQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EpisodeProcedureQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EpisodeProcedureQuery();
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
		public bool Load(EpisodeProcedureQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EpisodeProcedureQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EpisodeProcedureQuery : esEpisodeProcedureQuery
	{
		public EpisodeProcedureQuery()
		{

		}

		public EpisodeProcedureQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EpisodeProcedureQuery";
		}
	}

	[Serializable]
	public partial class EpisodeProcedureMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EpisodeProcedureMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ProcedureDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ProcedureDate;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ProcedureTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ProcedureTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ProcedureDate2, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ProcedureDate2;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ProcedureTime2, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ProcedureTime2;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ParamedicID2, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ParamedicID2;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ProcedureID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ProcedureID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.SRProcedureCategory, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.SRProcedureCategory;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.SRAnestesi, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.SRAnestesi;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.RoomID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.IsCito, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.IsCito;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.AssistantID1, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.AssistantID1;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.AssistantID2, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.AssistantID2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.Notes, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.BookingNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.BookingNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ParamedicID2a, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ParamedicID2a;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ParamedicID3a, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ParamedicID3a;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ParamedicID4a, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ParamedicID4a;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ParamedicIDAnestesi, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ParamedicIDAnestesi;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.AssistantIDAnestesi;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.InstrumentatorID1, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.InstrumentatorID1;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.InstrumentatorID2, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.InstrumentatorID2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.IsFromOperatingRoom, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.IsFromOperatingRoom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.CreateByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.CreateDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.AnestesyNotes, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.AnestesyNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ProcedureName, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ProcedureName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.OpNotesSeqNo, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.OpNotesSeqNo;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.IncisionDateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.IncisionDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.RegistrationInfoMedicID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.RegistrationInfoMedicID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.OperatingNotes, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.OperatingNotes;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi2, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureMetadata.PropertyNames.AssistantIDAnestesi2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.ProcedureSynonym, 37, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeProcedureMetadata.PropertyNames.ProcedureSynonym;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeProcedureMetadata.ColumnNames.QtyICD, 38, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EpisodeProcedureMetadata.PropertyNames.QtyICD;
            c.NumericPrecision = 2;
            _columns.Add(c);
        }
		#endregion

		static public EpisodeProcedureMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string ProcedureDate = "ProcedureDate";
			public const string ProcedureTime = "ProcedureTime";
			public const string ProcedureDate2 = "ProcedureDate2";
			public const string ProcedureTime2 = "ProcedureTime2";
			public const string ParamedicID = "ParamedicID";
			public const string ParamedicID2 = "ParamedicID2";
			public const string ProcedureID = "ProcedureID";
			public const string SRProcedureCategory = "SRProcedureCategory";
			public const string SRAnestesi = "SRAnestesi";
			public const string RoomID = "RoomID";
			public const string IsCito = "IsCito";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AssistantID1 = "AssistantID1";
			public const string AssistantID2 = "AssistantID2";
			public const string Notes = "Notes";
			public const string BookingNo = "BookingNo";
			public const string ParamedicID2a = "ParamedicID2a";
			public const string ParamedicID3a = "ParamedicID3a";
			public const string ParamedicID4a = "ParamedicID4a";
			public const string ParamedicIDAnestesi = "ParamedicIDAnestesi";
			public const string AssistantIDAnestesi = "AssistantIDAnestesi";
			public const string InstrumentatorID1 = "InstrumentatorID1";
			public const string InstrumentatorID2 = "InstrumentatorID2";
			public const string IsFromOperatingRoom = "IsFromOperatingRoom";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string AnestesyNotes = "AnestesyNotes";
			public const string ProcedureName = "ProcedureName";
			public const string OpNotesSeqNo = "OpNotesSeqNo";
			public const string IncisionDateTime = "IncisionDateTime";
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string OperatingNotes = "OperatingNotes";
			public const string AssistantIDAnestesi2 = "AssistantIDAnestesi2";
            public const string ProcedureSynonym = "ProcedureSynonym";
            public const string QtyICD = "QtyICD";
        }
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string SequenceNo = "SequenceNo";
			public const string ProcedureDate = "ProcedureDate";
			public const string ProcedureTime = "ProcedureTime";
			public const string ProcedureDate2 = "ProcedureDate2";
			public const string ProcedureTime2 = "ProcedureTime2";
			public const string ParamedicID = "ParamedicID";
			public const string ParamedicID2 = "ParamedicID2";
			public const string ProcedureID = "ProcedureID";
			public const string SRProcedureCategory = "SRProcedureCategory";
			public const string SRAnestesi = "SRAnestesi";
			public const string RoomID = "RoomID";
			public const string IsCito = "IsCito";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AssistantID1 = "AssistantID1";
			public const string AssistantID2 = "AssistantID2";
			public const string Notes = "Notes";
			public const string BookingNo = "BookingNo";
			public const string ParamedicID2a = "ParamedicID2a";
			public const string ParamedicID3a = "ParamedicID3a";
			public const string ParamedicID4a = "ParamedicID4a";
			public const string ParamedicIDAnestesi = "ParamedicIDAnestesi";
			public const string AssistantIDAnestesi = "AssistantIDAnestesi";
			public const string InstrumentatorID1 = "InstrumentatorID1";
			public const string InstrumentatorID2 = "InstrumentatorID2";
			public const string IsFromOperatingRoom = "IsFromOperatingRoom";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string AnestesyNotes = "AnestesyNotes";
			public const string ProcedureName = "ProcedureName";
			public const string OpNotesSeqNo = "OpNotesSeqNo";
			public const string IncisionDateTime = "IncisionDateTime";
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string OperatingNotes = "OperatingNotes";
			public const string AssistantIDAnestesi2 = "AssistantIDAnestesi2";
            public const string ProcedureSynonym = "ProcedureSynonym";
            public const string QtyICD = "QtyICD";
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
			lock (typeof(EpisodeProcedureMetadata))
			{
				if (EpisodeProcedureMetadata.mapDelegates == null)
				{
					EpisodeProcedureMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EpisodeProcedureMetadata.meta == null)
				{
					EpisodeProcedureMetadata.meta = new EpisodeProcedureMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcedureDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ProcedureTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ProcedureDate2", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ProcedureTime2", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcedureCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAnestesi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCito", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantID1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID2a", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID3a", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID4a", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicIDAnestesi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDAnestesi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstrumentatorID1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstrumentatorID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFromOperatingRoom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AnestesyNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcedureName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OpNotesSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncisionDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OperatingNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDAnestesi2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureSynonym", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QtyICD", new esTypeMap("int", "System.Int32"));


                meta.Source = "EpisodeProcedure";
				meta.Destination = "EpisodeProcedure";
				meta.spInsert = "proc_EpisodeProcedureInsert";
				meta.spUpdate = "proc_EpisodeProcedureUpdate";
				meta.spDelete = "proc_EpisodeProcedureDelete";
				meta.spLoadAll = "proc_EpisodeProcedureLoadAll";
				meta.spLoadByPrimaryKey = "proc_EpisodeProcedureLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EpisodeProcedureMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
