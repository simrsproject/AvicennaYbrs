/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/28/2021 10:41:00 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.ROCHE
{

	[Serializable]
	abstract public class esPocAdtCollection : esEntityCollectionWAuditLog
	{
		public esPocAdtCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PocAdtCollection";
		}

		#region Query Logic
		protected void InitQuery(esPocAdtQuery query)
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
			this.InitQuery(query as esPocAdtQuery);
		}
		#endregion
		
		virtual public PocAdt DetachEntity(PocAdt entity)
		{
			return base.DetachEntity(entity) as PocAdt;
		}
		
		virtual public PocAdt AttachEntity(PocAdt entity)
		{
			return base.AttachEntity(entity) as PocAdt;
		}
		
		virtual public void Combine(PocAdtCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PocAdt this[int index]
		{
			get
			{
				return base[index] as PocAdt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PocAdt);
		}
	}



	[Serializable]
	abstract public class esPocAdt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPocAdtQuery GetDynamicQuery()
		{
			return null;
		}

		public esPocAdt()
		{

		}

		public esPocAdt(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 id)
		{
			esPocAdtQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",id);
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
						case "Id": this.str.Id = (string)value; break;							
						case "Action": this.str.Action = (string)value; break;							
						case "PatientName": this.str.PatientName = (string)value; break;							
						case "Dob": this.str.Dob = (string)value; break;							
						case "Sex": this.str.Sex = (string)value; break;							
						case "VisitNum": this.str.VisitNum = (string)value; break;							
						case "PatientClass": this.str.PatientClass = (string)value; break;							
						case "LocationCode": this.str.LocationCode = (string)value; break;							
						case "LocationName": this.str.LocationName = (string)value; break;							
						case "DoctorCode": this.str.DoctorCode = (string)value; break;							
						case "DoctorName": this.str.DoctorName = (string)value; break;							
						case "AdmitDt": this.str.AdmitDt = (string)value; break;							
						case "Pid": this.str.Pid = (string)value; break;							
						case "RoomCode": this.str.RoomCode = (string)value; break;							
						case "RoomName": this.str.RoomName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
							break;
						
						case "Dob":
						
							if (value == null || value is System.DateTime)
								this.Dob = (System.DateTime?)value;
							break;
						
						case "AdmitDt":
						
							if (value == null || value is System.DateTime)
								this.AdmitDt = (System.DateTime?)value;
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
		/// Maps to POC_ADT.ID
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(PocAdtMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(PocAdtMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.ACTION
		/// </summary>
		virtual public System.String Action
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.Action);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.Action, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.PATIENT_NAME
		/// </summary>
		virtual public System.String PatientName
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.PatientName);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.PatientName, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.DOB
		/// </summary>
		virtual public System.DateTime? Dob
		{
			get
			{
				return base.GetSystemDateTime(PocAdtMetadata.ColumnNames.Dob);
			}
			
			set
			{
				base.SetSystemDateTime(PocAdtMetadata.ColumnNames.Dob, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.SEX
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.Sex);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.Sex, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.VISIT_NUM
		/// </summary>
		virtual public System.String VisitNum
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.VisitNum);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.VisitNum, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.PATIENT_CLASS
		/// </summary>
		virtual public System.String PatientClass
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.PatientClass);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.PatientClass, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.LOCATION_CODE
		/// </summary>
		virtual public System.String LocationCode
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.LocationCode);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.LocationCode, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.LOCATION_NAME
		/// </summary>
		virtual public System.String LocationName
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.LocationName);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.LocationName, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.DOCTOR_CODE
		/// </summary>
		virtual public System.String DoctorCode
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.DoctorCode);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.DoctorCode, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.DOCTOR_NAME
		/// </summary>
		virtual public System.String DoctorName
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.DoctorName);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.DoctorName, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.ADMIT_DT
		/// </summary>
		virtual public System.DateTime? AdmitDt
		{
			get
			{
				return base.GetSystemDateTime(PocAdtMetadata.ColumnNames.AdmitDt);
			}
			
			set
			{
				base.SetSystemDateTime(PocAdtMetadata.ColumnNames.AdmitDt, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.PID
		/// </summary>
		virtual public System.String Pid
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.Pid);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.Pid, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.ROOM_CODE
		/// </summary>
		virtual public System.String RoomCode
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.RoomCode);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.RoomCode, value);
			}
		}
		
		/// <summary>
		/// Maps to POC_ADT.ROOM_NAME
		/// </summary>
		virtual public System.String RoomName
		{
			get
			{
				return base.GetSystemString(PocAdtMetadata.ColumnNames.RoomName);
			}
			
			set
			{
				base.SetSystemString(PocAdtMetadata.ColumnNames.RoomName, value);
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
			public esStrings(esPocAdt entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
				}
			}
				
			public System.String Action
			{
				get
				{
					System.String data = entity.Action;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action = null;
					else entity.Action = Convert.ToString(value);
				}
			}
				
			public System.String PatientName
			{
				get
				{
					System.String data = entity.PatientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientName = null;
					else entity.PatientName = Convert.ToString(value);
				}
			}
				
			public System.String Dob
			{
				get
				{
					System.DateTime? data = entity.Dob;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Dob = null;
					else entity.Dob = Convert.ToDateTime(value);
				}
			}
				
			public System.String Sex
			{
				get
				{
					System.String data = entity.Sex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sex = null;
					else entity.Sex = Convert.ToString(value);
				}
			}
				
			public System.String VisitNum
			{
				get
				{
					System.String data = entity.VisitNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitNum = null;
					else entity.VisitNum = Convert.ToString(value);
				}
			}
				
			public System.String PatientClass
			{
				get
				{
					System.String data = entity.PatientClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientClass = null;
					else entity.PatientClass = Convert.ToString(value);
				}
			}
				
			public System.String LocationCode
			{
				get
				{
					System.String data = entity.LocationCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationCode = null;
					else entity.LocationCode = Convert.ToString(value);
				}
			}
				
			public System.String LocationName
			{
				get
				{
					System.String data = entity.LocationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationName = null;
					else entity.LocationName = Convert.ToString(value);
				}
			}
				
			public System.String DoctorCode
			{
				get
				{
					System.String data = entity.DoctorCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DoctorCode = null;
					else entity.DoctorCode = Convert.ToString(value);
				}
			}
				
			public System.String DoctorName
			{
				get
				{
					System.String data = entity.DoctorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DoctorName = null;
					else entity.DoctorName = Convert.ToString(value);
				}
			}
				
			public System.String AdmitDt
			{
				get
				{
					System.DateTime? data = entity.AdmitDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdmitDt = null;
					else entity.AdmitDt = Convert.ToDateTime(value);
				}
			}
				
			public System.String Pid
			{
				get
				{
					System.String data = entity.Pid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pid = null;
					else entity.Pid = Convert.ToString(value);
				}
			}
				
			public System.String RoomCode
			{
				get
				{
					System.String data = entity.RoomCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomCode = null;
					else entity.RoomCode = Convert.ToString(value);
				}
			}
				
			public System.String RoomName
			{
				get
				{
					System.String data = entity.RoomName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomName = null;
					else entity.RoomName = Convert.ToString(value);
				}
			}
			

			private esPocAdt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPocAdtQuery query)
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
				throw new Exception("esPocAdt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esPocAdtQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PocAdtMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Action
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.Action, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientName
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.PatientName, esSystemType.String);
			}
		} 
		
		public esQueryItem Dob
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.Dob, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.Sex, esSystemType.String);
			}
		} 
		
		public esQueryItem VisitNum
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.VisitNum, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientClass
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.PatientClass, esSystemType.String);
			}
		} 
		
		public esQueryItem LocationCode
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.LocationCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LocationName
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.LocationName, esSystemType.String);
			}
		} 
		
		public esQueryItem DoctorCode
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.DoctorCode, esSystemType.String);
			}
		} 
		
		public esQueryItem DoctorName
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.DoctorName, esSystemType.String);
			}
		} 
		
		public esQueryItem AdmitDt
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.AdmitDt, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Pid
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.Pid, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomCode
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.RoomCode, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomName
		{
			get
			{
				return new esQueryItem(this, PocAdtMetadata.ColumnNames.RoomName, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PocAdtCollection")]
	public partial class PocAdtCollection : esPocAdtCollection, IEnumerable<PocAdt>
	{
		public PocAdtCollection()
		{

		}
		
		public static implicit operator List<PocAdt>(PocAdtCollection coll)
		{
			List<PocAdt> list = new List<PocAdt>();
			
			foreach (PocAdt emp in coll)
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
				return  PocAdtMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PocAdtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PocAdt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PocAdt();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PocAdtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PocAdtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PocAdtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PocAdt AddNew()
		{
			PocAdt entity = base.AddNewEntity() as PocAdt;
			
			return entity;
		}

		public PocAdt FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as PocAdt;
		}


		#region IEnumerable<PocAdt> Members

		IEnumerator<PocAdt> IEnumerable<PocAdt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PocAdt;
			}
		}

		#endregion
		
		private PocAdtQuery query;
	}


	/// <summary>
	/// Encapsulates the 'POC_ADT' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PocAdt ({Id})")]
	[Serializable]
	public partial class PocAdt : esPocAdt
	{
		public PocAdt()
		{

		}
	
		public PocAdt(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PocAdtMetadata.Meta();
			}
		}
		
		
		
		override protected esPocAdtQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PocAdtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PocAdtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PocAdtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PocAdtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PocAdtQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PocAdtQuery : esPocAdtQuery
	{
		public PocAdtQuery()
		{

		}		
		
		public PocAdtQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PocAdtQuery";
        }
		
			
	}


	[Serializable]
	public partial class PocAdtMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PocAdtMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PocAdtMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.Action, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.Action;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.PatientName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.PatientName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.Dob, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PocAdtMetadata.PropertyNames.Dob;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.Sex, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.VisitNum, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.VisitNum;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.PatientClass, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.PatientClass;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.LocationCode, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.LocationCode;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.LocationName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.LocationName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.DoctorCode, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.DoctorCode;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.DoctorName, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.DoctorName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.AdmitDt, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PocAdtMetadata.PropertyNames.AdmitDt;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.Pid, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.Pid;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.RoomCode, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.RoomCode;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PocAdtMetadata.ColumnNames.RoomName, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PocAdtMetadata.PropertyNames.RoomName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PocAdtMetadata Meta()
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
			 public const string Id = "ID";
			 public const string Action = "ACTION";
			 public const string PatientName = "PATIENT_NAME";
			 public const string Dob = "DOB";
			 public const string Sex = "SEX";
			 public const string VisitNum = "VISIT_NUM";
			 public const string PatientClass = "PATIENT_CLASS";
			 public const string LocationCode = "LOCATION_CODE";
			 public const string LocationName = "LOCATION_NAME";
			 public const string DoctorCode = "DOCTOR_CODE";
			 public const string DoctorName = "DOCTOR_NAME";
			 public const string AdmitDt = "ADMIT_DT";
			 public const string Pid = "PID";
			 public const string RoomCode = "ROOM_CODE";
			 public const string RoomName = "ROOM_NAME";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Action = "Action";
			 public const string PatientName = "PatientName";
			 public const string Dob = "Dob";
			 public const string Sex = "Sex";
			 public const string VisitNum = "VisitNum";
			 public const string PatientClass = "PatientClass";
			 public const string LocationCode = "LocationCode";
			 public const string LocationName = "LocationName";
			 public const string DoctorCode = "DoctorCode";
			 public const string DoctorName = "DoctorName";
			 public const string AdmitDt = "AdmitDt";
			 public const string Pid = "Pid";
			 public const string RoomCode = "RoomCode";
			 public const string RoomName = "RoomName";
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
			lock (typeof(PocAdtMetadata))
			{
				if(PocAdtMetadata.mapDelegates == null)
				{
					PocAdtMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PocAdtMetadata.meta == null)
				{
					PocAdtMetadata.meta = new PocAdtMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Action", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Dob", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Sex", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VisitNum", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientClass", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DoctorCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DoctorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdmitDt", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Pid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomName", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "POC_ADT";
				meta.Destination = "POC_ADT";
				
				meta.spInsert = "proc_POC_ADTInsert";				
				meta.spUpdate = "proc_POC_ADTUpdate";		
				meta.spDelete = "proc_POC_ADTDelete";
				meta.spLoadAll = "proc_POC_ADTLoadAll";
				meta.spLoadByPrimaryKey = "proc_POC_ADTLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PocAdtMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
