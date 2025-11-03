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
	abstract public class esEpisodeDiagnoseInaGroupperCollection : esEntityCollectionWAuditLog
	{
		public esEpisodeDiagnoseInaGroupperCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EpisodeDiagnoseInaGroupperCollection";
		}

		#region Query Logic
		protected void InitQuery(esEpisodeDiagnoseInaGroupperQuery query)
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
			this.InitQuery(query as esEpisodeDiagnoseInaGroupperQuery);
		}
		#endregion
		
		virtual public EpisodeDiagnoseInaGroupper DetachEntity(EpisodeDiagnoseInaGroupper entity)
		{
			return base.DetachEntity(entity) as EpisodeDiagnoseInaGroupper;
		}
		
		virtual public EpisodeDiagnoseInaGroupper AttachEntity(EpisodeDiagnoseInaGroupper entity)
		{
			return base.AttachEntity(entity) as EpisodeDiagnoseInaGroupper;
		}
		
		virtual public void Combine(EpisodeDiagnoseInaGroupperCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EpisodeDiagnoseInaGroupper this[int index]
		{
			get
			{
				return base[index] as EpisodeDiagnoseInaGroupper;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EpisodeDiagnoseInaGroupper);
		}
	}



	[Serializable]
	abstract public class esEpisodeDiagnoseInaGroupper : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEpisodeDiagnoseInaGroupperQuery GetDynamicQuery()
		{
			return null;
		}

		public esEpisodeDiagnoseInaGroupper()
		{

		}

		public esEpisodeDiagnoseInaGroupper(DataRow row)
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
			esEpisodeDiagnoseInaGroupperQuery query = this.GetDynamicQuery();
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
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;							
						case "SRDiagnoseType": this.str.SRDiagnoseType = (string)value; break;							
						case "DiagnosisText": this.str.DiagnosisText = (string)value; break;							
						case "MorphologyID": this.str.MorphologyID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "IsAcuteDisease": this.str.IsAcuteDisease = (string)value; break;							
						case "IsChronicDisease": this.str.IsChronicDisease = (string)value; break;							
						case "IsOldCase": this.str.IsOldCase = (string)value; break;							
						case "IsConfirmed": this.str.IsConfirmed = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "ExternalCauseID": this.str.ExternalCauseID = (string)value; break;							
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;							
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "DiagnoseSynonym": this.str.DiagnoseSynonym = (string)value; break;
                    }
				}
				else
				{
					switch (name)
					{	
						case "IsAcuteDisease":
						
							if (value == null || value is System.Boolean)
								this.IsAcuteDisease = (System.Boolean?)value;
							break;
						
						case "IsChronicDisease":
						
							if (value == null || value is System.Boolean)
								this.IsChronicDisease = (System.Boolean?)value;
							break;
						
						case "IsOldCase":
						
							if (value == null || value is System.Boolean)
								this.IsOldCase = (System.Boolean?)value;
							break;
						
						case "IsConfirmed":
						
							if (value == null || value is System.Boolean)
								this.IsConfirmed = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to EpisodeDiagnoseInaGroupper.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.SRDiagnoseType
		/// </summary>
		virtual public System.String SRDiagnoseType
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SRDiagnoseType);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SRDiagnoseType, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.DiagnosisText
		/// </summary>
		virtual public System.String DiagnosisText
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnosisText);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnosisText, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.MorphologyID
		/// </summary>
		virtual public System.String MorphologyID
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.MorphologyID);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.MorphologyID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.IsAcuteDisease
		/// </summary>
		virtual public System.Boolean? IsAcuteDisease
		{
			get
			{
				return base.GetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsAcuteDisease);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsAcuteDisease, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.IsChronicDisease
		/// </summary>
		virtual public System.Boolean? IsChronicDisease
		{
			get
			{
				return base.GetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.IsOldCase
		/// </summary>
		virtual public System.Boolean? IsOldCase
		{
			get
			{
				return base.GetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsOldCase);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsOldCase, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.IsConfirmed
		/// </summary>
		virtual public System.Boolean? IsConfirmed
		{
			get
			{
				return base.GetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsConfirmed);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsConfirmed, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.ExternalCauseID
		/// </summary>
		virtual public System.String ExternalCauseID
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ExternalCauseID);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ExternalCauseID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EpisodeDiagnoseInaGroupper.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateDateTime, value);
			}
		}

        /// <summary>
        /// Maps to EpisodeDiagnoseInaGroupper.DiagnoseSynonym
        /// </summary>
        virtual public System.String DiagnoseSynonym
        {
            get
            {
                return base.GetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseSynonym);
            }

            set
            {
                base.SetSystemString(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseSynonym, value);
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
			public esStrings(esEpisodeDiagnoseInaGroupper entity)
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
				
			public System.String DiagnoseID
			{
				get
				{
					System.String data = entity.DiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseID = null;
					else entity.DiagnoseID = Convert.ToString(value);
				}
			}
				
			public System.String SRDiagnoseType
			{
				get
				{
					System.String data = entity.SRDiagnoseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDiagnoseType = null;
					else entity.SRDiagnoseType = Convert.ToString(value);
				}
			}
				
			public System.String DiagnosisText
			{
				get
				{
					System.String data = entity.DiagnosisText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnosisText = null;
					else entity.DiagnosisText = Convert.ToString(value);
				}
			}
				
			public System.String MorphologyID
			{
				get
				{
					System.String data = entity.MorphologyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MorphologyID = null;
					else entity.MorphologyID = Convert.ToString(value);
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
				
			public System.String IsAcuteDisease
			{
				get
				{
					System.Boolean? data = entity.IsAcuteDisease;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAcuteDisease = null;
					else entity.IsAcuteDisease = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsChronicDisease
			{
				get
				{
					System.Boolean? data = entity.IsChronicDisease;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsChronicDisease = null;
					else entity.IsChronicDisease = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsOldCase
			{
				get
				{
					System.Boolean? data = entity.IsOldCase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOldCase = null;
					else entity.IsOldCase = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsConfirmed
			{
				get
				{
					System.Boolean? data = entity.IsConfirmed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConfirmed = null;
					else entity.IsConfirmed = Convert.ToBoolean(value);
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
				
			public System.String ExternalCauseID
			{
				get
				{
					System.String data = entity.ExternalCauseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExternalCauseID = null;
					else entity.ExternalCauseID = Convert.ToString(value);
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

            public System.String DiagnoseSynonym
            {
                get
                {
                    System.String data = entity.DiagnoseSynonym;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseSynonym = null;
                    else entity.DiagnoseSynonym = Convert.ToString(value);
                }
            }

            private esEpisodeDiagnoseInaGroupper entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEpisodeDiagnoseInaGroupperQuery query)
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
				throw new Exception("esEpisodeDiagnoseInaGroupper can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esEpisodeDiagnoseInaGroupperQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EpisodeDiagnoseInaGroupperMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRDiagnoseType
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SRDiagnoseType, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnosisText
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnosisText, esSystemType.String);
			}
		} 
		
		public esQueryItem MorphologyID
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.MorphologyID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsAcuteDisease
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsAcuteDisease, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsChronicDisease
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsOldCase
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsOldCase, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsConfirmed
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsConfirmed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ExternalCauseID
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ExternalCauseID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
        }

        public esQueryItem DiagnoseSynonym
        {
            get
            {
                return new esQueryItem(this, EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseSynonym, esSystemType.String);
            }
        }
    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EpisodeDiagnoseInaGroupperCollection")]
	public partial class EpisodeDiagnoseInaGroupperCollection : esEpisodeDiagnoseInaGroupperCollection, IEnumerable<EpisodeDiagnoseInaGroupper>
	{
		public EpisodeDiagnoseInaGroupperCollection()
		{

		}
		
		public static implicit operator List<EpisodeDiagnoseInaGroupper>(EpisodeDiagnoseInaGroupperCollection coll)
		{
			List<EpisodeDiagnoseInaGroupper> list = new List<EpisodeDiagnoseInaGroupper>();
			
			foreach (EpisodeDiagnoseInaGroupper emp in coll)
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
				return  EpisodeDiagnoseInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EpisodeDiagnoseInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EpisodeDiagnoseInaGroupper(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EpisodeDiagnoseInaGroupper();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EpisodeDiagnoseInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EpisodeDiagnoseInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EpisodeDiagnoseInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EpisodeDiagnoseInaGroupper AddNew()
		{
			EpisodeDiagnoseInaGroupper entity = base.AddNewEntity() as EpisodeDiagnoseInaGroupper;
			
			return entity;
		}

		public EpisodeDiagnoseInaGroupper FindByPrimaryKey(System.String registrationNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo) as EpisodeDiagnoseInaGroupper;
		}


		#region IEnumerable<EpisodeDiagnoseInaGroupper> Members

		IEnumerator<EpisodeDiagnoseInaGroupper> IEnumerable<EpisodeDiagnoseInaGroupper>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EpisodeDiagnoseInaGroupper;
			}
		}

		#endregion
		
		private EpisodeDiagnoseInaGroupperQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EpisodeDiagnoseInaGroupper' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EpisodeDiagnoseInaGroupper ({RegistrationNo},{SequenceNo})")]
	[Serializable]
	public partial class EpisodeDiagnoseInaGroupper : esEpisodeDiagnoseInaGroupper
	{
		public EpisodeDiagnoseInaGroupper()
		{

		}
	
		public EpisodeDiagnoseInaGroupper(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EpisodeDiagnoseInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esEpisodeDiagnoseInaGroupperQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EpisodeDiagnoseInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EpisodeDiagnoseInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EpisodeDiagnoseInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EpisodeDiagnoseInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EpisodeDiagnoseInaGroupperQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EpisodeDiagnoseInaGroupperQuery : esEpisodeDiagnoseInaGroupperQuery
	{
		public EpisodeDiagnoseInaGroupperQuery()
		{

		}		
		
		public EpisodeDiagnoseInaGroupperQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EpisodeDiagnoseInaGroupperQuery";
        }
		
			
	}


	[Serializable]
	public partial class EpisodeDiagnoseInaGroupperMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EpisodeDiagnoseInaGroupperMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.DiagnoseID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.SRDiagnoseType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.SRDiagnoseType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnosisText, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.DiagnosisText;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.MorphologyID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.MorphologyID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsAcuteDisease, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.IsAcuteDisease;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.IsChronicDisease;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsOldCase, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.IsOldCase;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsConfirmed, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.IsConfirmed;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.IsVoid, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.Notes, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.ExternalCauseID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.ExternalCauseID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.CreateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(EpisodeDiagnoseInaGroupperMetadata.ColumnNames.DiagnoseSynonym, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeDiagnoseInaGroupperMetadata.PropertyNames.DiagnoseSynonym;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            _columns.Add(c);
        }
		#endregion	
	
		static public EpisodeDiagnoseInaGroupperMetadata Meta()
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
			 public const string DiagnoseID = "DiagnoseID";
			 public const string SRDiagnoseType = "SRDiagnoseType";
			 public const string DiagnosisText = "DiagnosisText";
			 public const string MorphologyID = "MorphologyID";
			 public const string ParamedicID = "ParamedicID";
			 public const string IsAcuteDisease = "IsAcuteDisease";
			 public const string IsChronicDisease = "IsChronicDisease";
			 public const string IsOldCase = "IsOldCase";
			 public const string IsConfirmed = "IsConfirmed";
			 public const string IsVoid = "IsVoid";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ExternalCauseID = "ExternalCauseID";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string DiagnoseSynonym = "DiagnoseSynonym";
        }
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string DiagnoseID = "DiagnoseID";
			 public const string SRDiagnoseType = "SRDiagnoseType";
			 public const string DiagnosisText = "DiagnosisText";
			 public const string MorphologyID = "MorphologyID";
			 public const string ParamedicID = "ParamedicID";
			 public const string IsAcuteDisease = "IsAcuteDisease";
			 public const string IsChronicDisease = "IsChronicDisease";
			 public const string IsOldCase = "IsOldCase";
			 public const string IsConfirmed = "IsConfirmed";
			 public const string IsVoid = "IsVoid";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ExternalCauseID = "ExternalCauseID";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string CreateDateTime = "CreateDateTime";
             public const string DiagnoseSynonym = "DiagnoseSynonym";
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
			lock (typeof(EpisodeDiagnoseInaGroupperMetadata))
			{
				if(EpisodeDiagnoseInaGroupperMetadata.mapDelegates == null)
				{
					EpisodeDiagnoseInaGroupperMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EpisodeDiagnoseInaGroupperMetadata.meta == null)
				{
					EpisodeDiagnoseInaGroupperMetadata.meta = new EpisodeDiagnoseInaGroupperMetadata();
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
				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDiagnoseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnosisText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MorphologyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAcuteDisease", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsChronicDisease", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOldCase", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsConfirmed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExternalCauseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DiagnoseSynonym", new esTypeMap("varchar", "System.String"));



                meta.Source = "EpisodeDiagnoseInaGroupper";
				meta.Destination = "EpisodeDiagnoseInaGroupper";
				
				meta.spInsert = "proc_EpisodeDiagnoseInaGroupperInsert";				
				meta.spUpdate = "proc_EpisodeDiagnoseInaGroupperUpdate";		
				meta.spDelete = "proc_EpisodeDiagnoseInaGroupperDelete";
				meta.spLoadAll = "proc_EpisodeDiagnoseInaGroupperLoadAll";
				meta.spLoadByPrimaryKey = "proc_EpisodeDiagnoseInaGroupperLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EpisodeDiagnoseInaGroupperMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
