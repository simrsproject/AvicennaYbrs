/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/12/2014 1:21:38 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.SYSMEX
{

	[Serializable]
	abstract public class esLisOrderCollection : esEntityCollectionWAuditLog
	{
		public esLisOrderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LisOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esLisOrderQuery query)
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
			this.InitQuery(query as esLisOrderQuery);
		}
		#endregion

		virtual public LisOrder DetachEntity(LisOrder entity)
		{
			return base.DetachEntity(entity) as LisOrder;
		}

		virtual public LisOrder AttachEntity(LisOrder entity)
		{
			return base.AttachEntity(entity) as LisOrder;
		}

		virtual public void Combine(LisOrderCollection collection)
		{
			base.Combine(collection);
		}

		new public LisOrder this[int index]
		{
			get
			{
				return base[index] as LisOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LisOrder);
		}
	}



	[Serializable]
	abstract public class esLisOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLisOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esLisOrder()
		{

		}

		public esLisOrder(DataRow row)
			: base(row)
		{

		}

		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 id)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
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
			esLisOrderQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("ID", id);
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
						case "Id": this.str.Id = (string)value; break;
						case "MessageDt": this.str.MessageDt = (string)value; break;
						case "OrderControl": this.str.OrderControl = (string)value; break;
						case "Pid": this.str.Pid = (string)value; break;
						case "Pname": this.str.Pname = (string)value; break;
						case "Address1": this.str.Address1 = (string)value; break;
						case "Address2": this.str.Address2 = (string)value; break;
						case "Address3": this.str.Address3 = (string)value; break;
						case "Address4": this.str.Address4 = (string)value; break;
						case "Ptype": this.str.Ptype = (string)value; break;
						case "BirthDt": this.str.BirthDt = (string)value; break;
						case "Sex": this.str.Sex = (string)value; break;
						case "Ono": this.str.Ono = (string)value; break;
						case "RequestDt": this.str.RequestDt = (string)value; break;
						case "Source": this.str.Source = (string)value; break;
						case "Clinician": this.str.Clinician = (string)value; break;
						case "RoomNo": this.str.RoomNo = (string)value; break;
						case "Priority": this.str.Priority = (string)value; break;
						case "Cmt": this.str.Cmt = (string)value; break;
						case "Visitno": this.str.Visitno = (string)value; break;
						case "OrderTestid": this.str.OrderTestid = (string)value; break;
						case "IsConfirm": this.str.IsConfirm = (string)value; break;
						case "HealthcareCode": this.str.HealthcareCode = (string)value; break;
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
						case "IsConfirm":

							if (value == null || value is System.Boolean)
								this.IsConfirm = (System.Boolean?)value;
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
		/// Maps to LIS_ORDER.ID
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(LisOrderMetadata.ColumnNames.Id);
			}

			set
			{
				base.SetSystemInt32(LisOrderMetadata.ColumnNames.Id, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.MESSAGE_DT
		/// </summary>
		virtual public System.String MessageDt
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.MessageDt);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.MessageDt, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ORDER_CONTROL
		/// </summary>
		virtual public System.String OrderControl
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.OrderControl);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.OrderControl, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.PID
		/// </summary>
		virtual public System.String Pid
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Pid);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Pid, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.PNAME
		/// </summary>
		virtual public System.String Pname
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Pname);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Pname, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ADDRESS1
		/// </summary>
		virtual public System.String Address1
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Address1);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Address1, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ADDRESS2
		/// </summary>
		virtual public System.String Address2
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Address2);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Address2, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ADDRESS3
		/// </summary>
		virtual public System.String Address3
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Address3);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Address3, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ADDRESS4
		/// </summary>
		virtual public System.String Address4
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Address4);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Address4, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.PTYPE
		/// </summary>
		virtual public System.String Ptype
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Ptype);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Ptype, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.BIRTH_DT
		/// </summary>
		virtual public System.String BirthDt
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.BirthDt);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.BirthDt, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.SEX
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Sex);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Sex, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ONO
		/// </summary>
		virtual public System.String Ono
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Ono);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Ono, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.REQUEST_DT
		/// </summary>
		virtual public System.String RequestDt
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.RequestDt);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.RequestDt, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.SOURCE
		/// </summary>
		virtual public System.String Source
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Source);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Source, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.CLINICIAN
		/// </summary>
		virtual public System.String Clinician
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Clinician);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Clinician, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ROOM_NO
		/// </summary>
		virtual public System.String RoomNo
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.RoomNo);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.RoomNo, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.PRIORITY
		/// </summary>
		virtual public System.String Priority
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Priority);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Priority, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.CMT
		/// </summary>
		virtual public System.String Cmt
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Cmt);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Cmt, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.VISITNO
		/// </summary>
		virtual public System.String Visitno
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.Visitno);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.Visitno, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.ORDER_TESTID
		/// </summary>
		virtual public System.String OrderTestid
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.OrderTestid);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.OrderTestid, value);
			}
		}

		/// <summary>
		/// Maps to LIS_ORDER.IS_CONFIRM
		/// </summary>
		virtual public System.Boolean? IsConfirm
		{
			get
			{
				return base.GetSystemBoolean(LisOrderMetadata.ColumnNames.IsConfirm);
			}

			set
			{
				base.SetSystemBoolean(LisOrderMetadata.ColumnNames.IsConfirm, value);
			}
		}
		/// <summary>
		/// Maps to LIS_ORDER.HEALTHCARE_CODE
		/// </summary>
		virtual public System.String HealthcareCode
		{
			get
			{
				return base.GetSystemString(LisOrderMetadata.ColumnNames.HealthcareCode);
			}

			set
			{
				base.SetSystemString(LisOrderMetadata.ColumnNames.HealthcareCode, value);
			}
		}

		#endregion

		#region String Properties


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
			public esStrings(esLisOrder entity)
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

			public System.String MessageDt
			{
				get
				{
					System.String data = entity.MessageDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MessageDt = null;
					else entity.MessageDt = Convert.ToString(value);
				}
			}

			public System.String OrderControl
			{
				get
				{
					System.String data = entity.OrderControl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderControl = null;
					else entity.OrderControl = Convert.ToString(value);
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

			public System.String Pname
			{
				get
				{
					System.String data = entity.Pname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pname = null;
					else entity.Pname = Convert.ToString(value);
				}
			}

			public System.String Address1
			{
				get
				{
					System.String data = entity.Address1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address1 = null;
					else entity.Address1 = Convert.ToString(value);
				}
			}

			public System.String Address2
			{
				get
				{
					System.String data = entity.Address2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address2 = null;
					else entity.Address2 = Convert.ToString(value);
				}
			}

			public System.String Address3
			{
				get
				{
					System.String data = entity.Address3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address3 = null;
					else entity.Address3 = Convert.ToString(value);
				}
			}

			public System.String Address4
			{
				get
				{
					System.String data = entity.Address4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address4 = null;
					else entity.Address4 = Convert.ToString(value);
				}
			}

			public System.String Ptype
			{
				get
				{
					System.String data = entity.Ptype;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ptype = null;
					else entity.Ptype = Convert.ToString(value);
				}
			}

			public System.String BirthDt
			{
				get
				{
					System.String data = entity.BirthDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthDt = null;
					else entity.BirthDt = Convert.ToString(value);
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

			public System.String Ono
			{
				get
				{
					System.String data = entity.Ono;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ono = null;
					else entity.Ono = Convert.ToString(value);
				}
			}

			public System.String RequestDt
			{
				get
				{
					System.String data = entity.RequestDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDt = null;
					else entity.RequestDt = Convert.ToString(value);
				}
			}

			public System.String Source
			{
				get
				{
					System.String data = entity.Source;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Source = null;
					else entity.Source = Convert.ToString(value);
				}
			}

			public System.String Clinician
			{
				get
				{
					System.String data = entity.Clinician;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Clinician = null;
					else entity.Clinician = Convert.ToString(value);
				}
			}

			public System.String RoomNo
			{
				get
				{
					System.String data = entity.RoomNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomNo = null;
					else entity.RoomNo = Convert.ToString(value);
				}
			}

			public System.String Priority
			{
				get
				{
					System.String data = entity.Priority;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Priority = null;
					else entity.Priority = Convert.ToString(value);
				}
			}

			public System.String Cmt
			{
				get
				{
					System.String data = entity.Cmt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cmt = null;
					else entity.Cmt = Convert.ToString(value);
				}
			}

			public System.String Visitno
			{
				get
				{
					System.String data = entity.Visitno;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Visitno = null;
					else entity.Visitno = Convert.ToString(value);
				}
			}

			public System.String OrderTestid
			{
				get
				{
					System.String data = entity.OrderTestid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderTestid = null;
					else entity.OrderTestid = Convert.ToString(value);
				}
			}

			public System.String IsConfirm
			{
				get
				{
					System.Boolean? data = entity.IsConfirm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConfirm = null;
					else entity.IsConfirm = Convert.ToBoolean(value);
				}
			}

			public System.String HealthcareCode
			{
				get
				{
					System.String data = entity.HealthcareCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HealthcareCode = null;
					else entity.HealthcareCode = Convert.ToString(value);
				}
			}


			private esLisOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLisOrderQuery query)
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
				throw new Exception("esLisOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esLisOrderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LisOrderMetadata.Meta();
			}
		}


		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		}

		public esQueryItem MessageDt
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.MessageDt, esSystemType.String);
			}
		}

		public esQueryItem OrderControl
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.OrderControl, esSystemType.String);
			}
		}

		public esQueryItem Pid
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Pid, esSystemType.String);
			}
		}

		public esQueryItem Pname
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Pname, esSystemType.String);
			}
		}

		public esQueryItem Address1
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Address1, esSystemType.String);
			}
		}

		public esQueryItem Address2
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Address2, esSystemType.String);
			}
		}

		public esQueryItem Address3
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Address3, esSystemType.String);
			}
		}

		public esQueryItem Address4
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Address4, esSystemType.String);
			}
		}

		public esQueryItem Ptype
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Ptype, esSystemType.String);
			}
		}

		public esQueryItem BirthDt
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.BirthDt, esSystemType.String);
			}
		}

		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Sex, esSystemType.String);
			}
		}

		public esQueryItem Ono
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Ono, esSystemType.String);
			}
		}

		public esQueryItem RequestDt
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.RequestDt, esSystemType.String);
			}
		}

		public esQueryItem Source
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Source, esSystemType.String);
			}
		}

		public esQueryItem Clinician
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Clinician, esSystemType.String);
			}
		}

		public esQueryItem RoomNo
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.RoomNo, esSystemType.String);
			}
		}

		public esQueryItem Priority
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Priority, esSystemType.String);
			}
		}

		public esQueryItem Cmt
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Cmt, esSystemType.String);
			}
		}

		public esQueryItem Visitno
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.Visitno, esSystemType.String);
			}
		}

		public esQueryItem OrderTestid
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.OrderTestid, esSystemType.String);
			}
		}

		public esQueryItem IsConfirm
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.IsConfirm, esSystemType.Boolean);
			}
		}

		public esQueryItem HealthcareCode
		{
			get
			{
				return new esQueryItem(this, LisOrderMetadata.ColumnNames.HealthcareCode, esSystemType.String);
			}
		}

	}



	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LisOrderCollection")]
	public partial class LisOrderCollection : esLisOrderCollection, IEnumerable<LisOrder>
	{
		public LisOrderCollection()
		{

		}

		public static implicit operator List<LisOrder>(LisOrderCollection coll)
		{
			List<LisOrder> list = new List<LisOrder>();

			foreach (LisOrder emp in coll)
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
				return LisOrderMetadata.Meta();
			}
		}



		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LisOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LisOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LisOrder();
		}


		#endregion


		[BrowsableAttribute(false)]
		public LisOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LisOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LisOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		public LisOrder AddNew()
		{
			LisOrder entity = base.AddNewEntity() as LisOrder;

			return entity;
		}

		public LisOrder FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as LisOrder;
		}


		#region IEnumerable<LisOrder> Members

		IEnumerator<LisOrder> IEnumerable<LisOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LisOrder;
			}
		}

		#endregion

		private LisOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LIS_ORDER' table
	/// </summary>

	[System.Diagnostics.DebuggerDisplay("LisOrder ({Id})")]
	[Serializable]
	public partial class LisOrder : esLisOrder
	{
		public LisOrder()
		{

		}

		public LisOrder(DataRow row)
			: base(row)
		{

		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LisOrderMetadata.Meta();
			}
		}



		override protected esLisOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LisOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion




		[BrowsableAttribute(false)]
		public LisOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LisOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}


		public bool Load(LisOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LisOrderQuery query;
	}



	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]

	public partial class LisOrderQuery : esLisOrderQuery
	{
		public LisOrderQuery()
		{

		}

		public LisOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LisOrderQuery";
		}


	}


	[Serializable]
	public partial class LisOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LisOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LisOrderMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.MessageDt, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.MessageDt;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.OrderControl, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.OrderControl;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Pid, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Pid;
			c.CharacterMaxLength = 16;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Pname, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Pname;
			c.CharacterMaxLength = 152;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Address1, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Address1;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Address2, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Address2;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Address3, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Address3;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Address4, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Address4;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Ptype, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Ptype;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.BirthDt, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.BirthDt;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Sex, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Ono, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Ono;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.RequestDt, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.RequestDt;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Source, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Source;
			c.CharacterMaxLength = 57;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Clinician, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Clinician;
			c.CharacterMaxLength = 266;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.RoomNo, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.RoomNo;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Priority, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Priority;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Cmt, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Cmt;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.Visitno, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.Visitno;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.OrderTestid, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.OrderTestid;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.IsConfirm, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LisOrderMetadata.PropertyNames.IsConfirm;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LisOrderMetadata.ColumnNames.HealthcareCode, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = LisOrderMetadata.PropertyNames.HealthcareCode;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion

		static public LisOrderMetadata Meta()
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
			public const string Id = "ID";
			public const string MessageDt = "MESSAGE_DT";
			public const string OrderControl = "ORDER_CONTROL";
			public const string Pid = "PID";
			public const string Pname = "PNAME";
			public const string Address1 = "ADDRESS1";
			public const string Address2 = "ADDRESS2";
			public const string Address3 = "ADDRESS3";
			public const string Address4 = "ADDRESS4";
			public const string Ptype = "PTYPE";
			public const string BirthDt = "BIRTH_DT";
			public const string Sex = "SEX";
			public const string Ono = "ONO";
			public const string RequestDt = "REQUEST_DT";
			public const string Source = "SOURCE";
			public const string Clinician = "CLINICIAN";
			public const string RoomNo = "ROOM_NO";
			public const string Priority = "PRIORITY";
			public const string Cmt = "CMT";
			public const string Visitno = "VISITNO";
			public const string OrderTestid = "ORDER_TESTID";
			public const string IsConfirm = "IS_CONFIRM";
			public const string HealthcareCode = "HEALTHCARE_CODE";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string Id = "Id";
			public const string MessageDt = "MessageDt";
			public const string OrderControl = "OrderControl";
			public const string Pid = "Pid";
			public const string Pname = "Pname";
			public const string Address1 = "Address1";
			public const string Address2 = "Address2";
			public const string Address3 = "Address3";
			public const string Address4 = "Address4";
			public const string Ptype = "Ptype";
			public const string BirthDt = "BirthDt";
			public const string Sex = "Sex";
			public const string Ono = "Ono";
			public const string RequestDt = "RequestDt";
			public const string Source = "Source";
			public const string Clinician = "Clinician";
			public const string RoomNo = "RoomNo";
			public const string Priority = "Priority";
			public const string Cmt = "Cmt";
			public const string Visitno = "Visitno";
			public const string OrderTestid = "OrderTestid";
			public const string IsConfirm = "IsConfirm";
			public const string HealthcareCode = "HealthcareCode";
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
			lock (typeof(LisOrderMetadata))
			{
				if (LisOrderMetadata.mapDelegates == null)
				{
					LisOrderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LisOrderMetadata.meta == null)
				{
					LisOrderMetadata.meta = new LisOrderMetadata();
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


				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MessageDt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderControl", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pname", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ptype", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BirthDt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ono", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestDt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Source", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Clinician", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Priority", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Cmt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Visitno", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderTestid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConfirm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("HealthcareCode", new esTypeMap("varchar", "System.String"));



				meta.Source = "LIS_ORDER";
				meta.Destination = "LIS_ORDER";

				meta.spInsert = "proc_LIS_ORDERInsert";
				meta.spUpdate = "proc_LIS_ORDERUpdate";
				meta.spDelete = "proc_LIS_ORDERDelete";
				meta.spLoadAll = "proc_LIS_ORDERLoadAll";
				meta.spLoadByPrimaryKey = "proc_LIS_ORDERLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LisOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
