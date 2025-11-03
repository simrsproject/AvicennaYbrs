/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/8/2022 2:33:14 PM
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
	abstract public class esEpisodeProcedureInaGroupperCollection : esEntityCollectionWAuditLog
	{
		public esEpisodeProcedureInaGroupperCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EpisodeProcedureInaGroupperCollection";
		}

		#region Query Logic
		protected void InitQuery(esEpisodeProcedureInaGroupperQuery query)
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
			this.InitQuery(query as esEpisodeProcedureInaGroupperQuery);
		}
		#endregion
		
		virtual public EpisodeProcedureInaGroupper DetachEntity(EpisodeProcedureInaGroupper entity)
		{
			return base.DetachEntity(entity) as EpisodeProcedureInaGroupper;
		}
		
		virtual public EpisodeProcedureInaGroupper AttachEntity(EpisodeProcedureInaGroupper entity)
		{
			return base.AttachEntity(entity) as EpisodeProcedureInaGroupper;
		}
		
		virtual public void Combine(EpisodeProcedureInaGroupperCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EpisodeProcedureInaGroupper this[int index]
		{
			get
			{
				return base[index] as EpisodeProcedureInaGroupper;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EpisodeProcedureInaGroupper);
		}
	}



	[Serializable]
	abstract public class esEpisodeProcedureInaGroupper : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEpisodeProcedureInaGroupperQuery GetDynamicQuery()
		{
			return null;
		}

		public esEpisodeProcedureInaGroupper()
		{

		}

		public esEpisodeProcedureInaGroupper(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String sequenceNo)
		{
			esEpisodeProcedureInaGroupperQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "OperatingNotes": this.str.OperatingNotes = (string)value; break;
                        case "ProcedureSynonym": this.str.ProcedureSynonym = (string)value; break;
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
		/// Maps to EpisodeProcedureInaGroupper.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ProcedureDate
		/// </summary>
		virtual public System.DateTime? ProcedureDate
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate);
			}
			
			set
			{
				base.SetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ProcedureTime
		/// </summary>
		virtual public System.String ProcedureTime
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ProcedureDate2
		/// </summary>
		virtual public System.DateTime? ProcedureDate2
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate2);
			}
			
			set
			{
				base.SetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate2, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ProcedureTime2
		/// </summary>
		virtual public System.String ProcedureTime2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime2);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime2, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ParamedicID2
		/// </summary>
		virtual public System.String ParamedicID2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ProcedureID
		/// </summary>
		virtual public System.String ProcedureID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureID);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.SRProcedureCategory
		/// </summary>
		virtual public System.String SRProcedureCategory
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.SRProcedureCategory);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.SRProcedureCategory, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.SRAnestesi
		/// </summary>
		virtual public System.String SRAnestesi
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.SRAnestesi);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.SRAnestesi, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.RoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.IsCito
		/// </summary>
		virtual public System.Boolean? IsCito
		{
			get
			{
				return base.GetSystemBoolean(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsCito);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsCito, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.AssistantID1
		/// </summary>
		virtual public System.String AssistantID1
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID1);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID1, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.AssistantID2
		/// </summary>
		virtual public System.String AssistantID2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID2);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID2, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.BookingNo
		/// </summary>
		virtual public System.String BookingNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.BookingNo);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.BookingNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ParamedicID2a
		/// </summary>
		virtual public System.String ParamedicID2a
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2a);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2a, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ParamedicID3a
		/// </summary>
		virtual public System.String ParamedicID3a
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID3a);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID3a, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ParamedicID4a
		/// </summary>
		virtual public System.String ParamedicID4a
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID4a);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID4a, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ParamedicIDAnestesi
		/// </summary>
		virtual public System.String ParamedicIDAnestesi
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicIDAnestesi);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicIDAnestesi, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.AssistantIDAnestesi
		/// </summary>
		virtual public System.String AssistantIDAnestesi
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantIDAnestesi);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantIDAnestesi, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.InstrumentatorID1
		/// </summary>
		virtual public System.String InstrumentatorID1
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID1);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID1, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.InstrumentatorID2
		/// </summary>
		virtual public System.String InstrumentatorID2
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID2);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID2, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.IsFromOperatingRoom
		/// </summary>
		virtual public System.Boolean? IsFromOperatingRoom
		{
			get
			{
				return base.GetSystemBoolean(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsFromOperatingRoom);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsFromOperatingRoom, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.AnestesyNotes
		/// </summary>
		virtual public System.String AnestesyNotes
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AnestesyNotes);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.AnestesyNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.ProcedureName
		/// </summary>
		virtual public System.String ProcedureName
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureName);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureName, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.OpNotesSeqNo
		/// </summary>
		virtual public System.String OpNotesSeqNo
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.OpNotesSeqNo);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.OpNotesSeqNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeProcedureInaGroupper.OperatingNotes
		/// </summary>
		virtual public System.String OperatingNotes
		{
			get
			{
				return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.OperatingNotes);
			}
			
			set
			{
				base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.OperatingNotes, value);
			}
		}

        /// <summary>
        /// Maps to EpisodeProcedureInaGroupper.ProcedureSynonym
        /// </summary>
        virtual public System.String ProcedureSynonym
        {
            get
            {
                return base.GetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureSynonym);
            }

            set
            {
                base.SetSystemString(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureSynonym, value);
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
			public esStrings(esEpisodeProcedureInaGroupper entity)
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

            private esEpisodeProcedureInaGroupper entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEpisodeProcedureInaGroupperQuery query)
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
				throw new Exception("esEpisodeProcedureInaGroupper can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esEpisodeProcedureInaGroupperQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EpisodeProcedureInaGroupperMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ProcedureDate
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ProcedureTime
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime, esSystemType.String);
			}
		} 
		
		public esQueryItem ProcedureDate2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate2, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ProcedureTime2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime2, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2, esSystemType.String);
			}
		} 
		
		public esQueryItem ProcedureID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRProcedureCategory
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.SRProcedureCategory, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAnestesi
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.SRAnestesi, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsCito
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.IsCito, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem AssistantID1
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID1, esSystemType.String);
			}
		} 
		
		public esQueryItem AssistantID2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID2, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem BookingNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.BookingNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID2a
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2a, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID3a
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID3a, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID4a
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID4a, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicIDAnestesi
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicIDAnestesi, esSystemType.String);
			}
		} 
		
		public esQueryItem AssistantIDAnestesi
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantIDAnestesi, esSystemType.String);
			}
		} 
		
		public esQueryItem InstrumentatorID1
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID1, esSystemType.String);
			}
		} 
		
		public esQueryItem InstrumentatorID2
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID2, esSystemType.String);
			}
		} 
		
		public esQueryItem IsFromOperatingRoom
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.IsFromOperatingRoom, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AnestesyNotes
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.AnestesyNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem ProcedureName
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureName, esSystemType.String);
			}
		} 
		
		public esQueryItem OpNotesSeqNo
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.OpNotesSeqNo, esSystemType.String);
			}
		} 
		
		public esQueryItem OperatingNotes
		{
			get
			{
				return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.OperatingNotes, esSystemType.String);
			}
        }

        public esQueryItem ProcedureSynonym
        {
            get
            {
                return new esQueryItem(this, EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureSynonym, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EpisodeProcedureInaGroupperCollection")]
	public partial class EpisodeProcedureInaGroupperCollection : esEpisodeProcedureInaGroupperCollection, IEnumerable<EpisodeProcedureInaGroupper>
	{
		public EpisodeProcedureInaGroupperCollection()
		{

		}
		
		public static implicit operator List<EpisodeProcedureInaGroupper>(EpisodeProcedureInaGroupperCollection coll)
		{
			List<EpisodeProcedureInaGroupper> list = new List<EpisodeProcedureInaGroupper>();
			
			foreach (EpisodeProcedureInaGroupper emp in coll)
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
				return  EpisodeProcedureInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EpisodeProcedureInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EpisodeProcedureInaGroupper(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EpisodeProcedureInaGroupper();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EpisodeProcedureInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EpisodeProcedureInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EpisodeProcedureInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EpisodeProcedureInaGroupper AddNew()
		{
			EpisodeProcedureInaGroupper entity = base.AddNewEntity() as EpisodeProcedureInaGroupper;
			
			return entity;
		}

		public EpisodeProcedureInaGroupper FindByPrimaryKey(System.String registrationNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo) as EpisodeProcedureInaGroupper;
		}


		#region IEnumerable<EpisodeProcedureInaGroupper> Members

		IEnumerator<EpisodeProcedureInaGroupper> IEnumerable<EpisodeProcedureInaGroupper>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EpisodeProcedureInaGroupper;
			}
		}

		#endregion
		
		private EpisodeProcedureInaGroupperQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EpisodeProcedureInaGroupper' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EpisodeProcedureInaGroupper ({RegistrationNo},{SequenceNo})")]
	[Serializable]
	public partial class EpisodeProcedureInaGroupper : esEpisodeProcedureInaGroupper
	{
		public EpisodeProcedureInaGroupper()
		{

		}
	
		public EpisodeProcedureInaGroupper(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EpisodeProcedureInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esEpisodeProcedureInaGroupperQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EpisodeProcedureInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EpisodeProcedureInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EpisodeProcedureInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EpisodeProcedureInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EpisodeProcedureInaGroupperQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EpisodeProcedureInaGroupperQuery : esEpisodeProcedureInaGroupperQuery
	{
		public EpisodeProcedureInaGroupperQuery()
		{

		}		
		
		public EpisodeProcedureInaGroupperQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EpisodeProcedureInaGroupperQuery";
        }
		
			
	}


	[Serializable]
	public partial class EpisodeProcedureInaGroupperMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EpisodeProcedureInaGroupperMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ProcedureDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ProcedureTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureDate2, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ProcedureDate2;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureTime2, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ProcedureTime2;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ParamedicID2;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ProcedureID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.SRProcedureCategory, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.SRProcedureCategory;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.SRAnestesi, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.SRAnestesi;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.RoomID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsCito, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.IsCito;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID1, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.AssistantID1;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantID2, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.AssistantID2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.Notes, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.BookingNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.BookingNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID2a, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ParamedicID2a;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID3a, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ParamedicID3a;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicID4a, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ParamedicID4a;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ParamedicIDAnestesi, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ParamedicIDAnestesi;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.AssistantIDAnestesi, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.AssistantIDAnestesi;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID1, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.InstrumentatorID1;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.InstrumentatorID2, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.InstrumentatorID2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.IsFromOperatingRoom, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.IsFromOperatingRoom;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.CreateDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.AnestesyNotes, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.AnestesyNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureName, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ProcedureName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.OpNotesSeqNo, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.OpNotesSeqNo;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.OperatingNotes, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.OperatingNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(EpisodeProcedureInaGroupperMetadata.ColumnNames.ProcedureSynonym, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeProcedureInaGroupperMetadata.PropertyNames.ProcedureSynonym;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            _columns.Add(c);
        }
		#endregion	
	
		static public EpisodeProcedureInaGroupperMetadata Meta()
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
			 public const string OperatingNotes = "OperatingNotes";
             public const string ProcedureSynonym = "ProcedureSynonym";
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
			 public const string OperatingNotes = "OperatingNotes";
             public const string ProcedureSynonym = "ProcedureSynonym";
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
			lock (typeof(EpisodeProcedureInaGroupperMetadata))
			{
				if(EpisodeProcedureInaGroupperMetadata.mapDelegates == null)
				{
					EpisodeProcedureInaGroupperMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EpisodeProcedureInaGroupperMetadata.meta == null)
				{
					EpisodeProcedureInaGroupperMetadata.meta = new EpisodeProcedureInaGroupperMetadata();
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
				meta.AddTypeMap("OperatingNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureSynonym", new esTypeMap("varchar", "System.String"));



                meta.Source = "EpisodeProcedureInaGroupper";
				meta.Destination = "EpisodeProcedureInaGroupper";
				
				meta.spInsert = "proc_EpisodeProcedureInaGroupperInsert";				
				meta.spUpdate = "proc_EpisodeProcedureInaGroupperUpdate";		
				meta.spDelete = "proc_EpisodeProcedureInaGroupperDelete";
				meta.spLoadAll = "proc_EpisodeProcedureInaGroupperLoadAll";
				meta.spLoadByPrimaryKey = "proc_EpisodeProcedureInaGroupperLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EpisodeProcedureInaGroupperMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
