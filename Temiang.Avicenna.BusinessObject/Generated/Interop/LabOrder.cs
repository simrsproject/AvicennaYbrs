/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 1/19/2011 11:11:29 AM
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
	abstract public class esLabOrderCollection : esEntityCollectionWAuditLog
	{
		public esLabOrderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LabOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esLabOrderQuery query)
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
			this.InitQuery(query as esLabOrderQuery);
		}
		#endregion
		
		virtual public LabOrder DetachEntity(LabOrder entity)
		{
			return base.DetachEntity(entity) as LabOrder;
		}
		
		virtual public LabOrder AttachEntity(LabOrder entity)
		{
			return base.AttachEntity(entity) as LabOrder;
		}
		
		virtual public void Combine(LabOrderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LabOrder this[int index]
		{
			get
			{
				return base[index] as LabOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LabOrder);
		}
	}



	[Serializable]
	abstract public class esLabOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLabOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esLabOrder()
		{

		}

		public esLabOrder(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String receiptno)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receiptno);
			else
				return LoadByPrimaryKeyStoredProcedure(receiptno);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String receiptno)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receiptno);
			else
				return LoadByPrimaryKeyStoredProcedure(receiptno);
		}

		private bool LoadByPrimaryKeyDynamic(System.String receiptno)
		{
			esLabOrderQuery query = this.GetDynamicQuery();
			query.Where(query.Receiptno == receiptno);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String receiptno)
		{
			esParameters parms = new esParameters();
			parms.Add("RECEIPTNO",receiptno);
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
						case "Msh": this.str.Msh = (string)value; break;							
						case "Msfl": this.str.Msfl = (string)value; break;							
						case "Refno": this.str.Refno = (string)value; break;							
						case "Receiptno": this.str.Receiptno = (string)value; break;							
						case "Refdt": this.str.Refdt = (string)value; break;							
						case "Pid": this.str.Pid = (string)value; break;							
						case "Ptype": this.str.Ptype = (string)value; break;							
						case "Sourcecd": this.str.Sourcecd = (string)value; break;							
						case "Priority": this.str.Priority = (string)value; break;							
						case "Pricegrp": this.str.Pricegrp = (string)value; break;							
						case "Totamt": this.str.Totamt = (string)value; break;							
						case "Payamt": this.str.Payamt = (string)value; break;							
						case "Paystatus": this.str.Paystatus = (string)value; break;							
						case "Roomno": this.str.Roomno = (string)value; break;							
						case "Crtdt": this.str.Crtdt = (string)value; break;							
						case "Cusrid": this.str.Cusrid = (string)value; break;							
						case "Lupddt": this.str.Lupddt = (string)value; break;							
						case "Lusrid": this.str.Lusrid = (string)value; break;							
						case "Delflag": this.str.Delflag = (string)value; break;							
						case "Clinfo": this.str.Clinfo = (string)value; break;							
						case "Pname": this.str.Pname = (string)value; break;							
						case "Sex": this.str.Sex = (string)value; break;							
						case "Agey": this.str.Agey = (string)value; break;							
						case "Agem": this.str.Agem = (string)value; break;							
						case "Aged": this.str.Aged = (string)value; break;							
						case "Cliniccd": this.str.Cliniccd = (string)value; break;							
						case "Reftime": this.str.Reftime = (string)value; break;							
						case "Dob": this.str.Dob = (string)value; break;							
						case "Addr1": this.str.Addr1 = (string)value; break;							
						case "Addr2": this.str.Addr2 = (string)value; break;							
						case "Addr3": this.str.Addr3 = (string)value; break;							
						case "Addr4": this.str.Addr4 = (string)value; break;							
						case "Discpct": this.str.Discpct = (string)value; break;							
						case "Discamt": this.str.Discamt = (string)value; break;							
						case "Labno": this.str.Labno = (string)value; break;							
						case "Jsarana": this.str.Jsarana = (string)value; break;							
						case "Jmedis": this.str.Jmedis = (string)value; break;							
						case "Tgg": this.str.Tgg = (string)value; break;							
						case "Klb": this.str.Klb = (string)value; break;							
						case "Klbdesc": this.str.Klbdesc = (string)value; break;							
						case "Sjp": this.str.Sjp = (string)value; break;							
						case "Locacd": this.str.Locacd = (string)value; break;							
						case "Shfcd": this.str.Shfcd = (string)value; break;							
						case "Othtgg": this.str.Othtgg = (string)value; break;							
						case "Jsar": this.str.Jsar = (string)value; break;							
						case "Jmed": this.str.Jmed = (string)value; break;							
						case "Jsaroth": this.str.Jsaroth = (string)value; break;							
						case "Jmedoth": this.str.Jmedoth = (string)value; break;							
						case "Sourcenm": this.str.Sourcenm = (string)value; break;							
						case "Clinicnm": this.str.Clinicnm = (string)value; break;							
						case "Pricenm": this.str.Pricenm = (string)value; break;							
						case "Ctgg": this.str.Ctgg = (string)value; break;							
						case "Defprice": this.str.Defprice = (string)value; break;							
						case "Jsar2a": this.str.Jsar2a = (string)value; break;							
						case "Jmed2a": this.str.Jmed2a = (string)value; break;							
						case "Loket": this.str.Loket = (string)value; break;							
						case "SpDesc": this.str.SpDesc = (string)value; break;							
						case "SpCd": this.str.SpCd = (string)value; break;							
						case "ProgVer": this.str.ProgVer = (string)value; break;							
						case "Kencana": this.str.Kencana = (string)value; break;							
						case "Patklin": this.str.Patklin = (string)value; break;							
						case "Clscd": this.str.Clscd = (string)value; break;							
						case "Clsnm": this.str.Clsnm = (string)value; break;							
						case "Testcd": this.str.Testcd = (string)value; break;							
						case "Testcdext": this.str.Testcdext = (string)value; break;							
						case "Jsaranad": this.str.Jsaranad = (string)value; break;							
						case "Jmedisd": this.str.Jmedisd = (string)value; break;							
						case "Jsard": this.str.Jsard = (string)value; break;							
						case "Jmedd": this.str.Jmedd = (string)value; break;							
						case "Jsarothd": this.str.Jsarothd = (string)value; break;							
						case "Jmedothd": this.str.Jmedothd = (string)value; break;							
						case "Kencanad": this.str.Kencanad = (string)value; break;							
						case "Patklind": this.str.Patklind = (string)value; break;							
						case "Isconfirm": this.str.Isconfirm = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Totamt":
						
							if (value == null || value is System.Decimal)
								this.Totamt = (System.Decimal?)value;
							break;
						
						case "Payamt":
						
							if (value == null || value is System.Decimal)
								this.Payamt = (System.Decimal?)value;
							break;
						
						case "Discpct":
						
							if (value == null || value is System.Decimal)
								this.Discpct = (System.Decimal?)value;
							break;
						
						case "Discamt":
						
							if (value == null || value is System.Decimal)
								this.Discamt = (System.Decimal?)value;
							break;
						
						case "Jsarana":
						
							if (value == null || value is System.Decimal)
								this.Jsarana = (System.Decimal?)value;
							break;
						
						case "Jmedis":
						
							if (value == null || value is System.Decimal)
								this.Jmedis = (System.Decimal?)value;
							break;
						
						case "Tgg":
						
							if (value == null || value is System.Decimal)
								this.Tgg = (System.Decimal?)value;
							break;
						
						case "Klb":
						
							if (value == null || value is System.Decimal)
								this.Klb = (System.Decimal?)value;
							break;
						
						case "Othtgg":
						
							if (value == null || value is System.Decimal)
								this.Othtgg = (System.Decimal?)value;
							break;
						
						case "Jsar":
						
							if (value == null || value is System.Decimal)
								this.Jsar = (System.Decimal?)value;
							break;
						
						case "Jmed":
						
							if (value == null || value is System.Decimal)
								this.Jmed = (System.Decimal?)value;
							break;
						
						case "Jsaroth":
						
							if (value == null || value is System.Decimal)
								this.Jsaroth = (System.Decimal?)value;
							break;
						
						case "Jmedoth":
						
							if (value == null || value is System.Decimal)
								this.Jmedoth = (System.Decimal?)value;
							break;
						
						case "Jsar2a":
						
							if (value == null || value is System.Decimal)
								this.Jsar2a = (System.Decimal?)value;
							break;
						
						case "Jmed2a":
						
							if (value == null || value is System.Decimal)
								this.Jmed2a = (System.Decimal?)value;
							break;
						
						case "Kencana":
						
							if (value == null || value is System.Decimal)
								this.Kencana = (System.Decimal?)value;
							break;
						
						case "Patklin":
						
							if (value == null || value is System.Decimal)
								this.Patklin = (System.Decimal?)value;
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
		/// Maps to LabOrder.MSH
		/// </summary>
		virtual public System.String Msh
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Msh);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Msh, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.MSFL
		/// </summary>
		virtual public System.String Msfl
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Msfl);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Msfl, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.REFNO
		/// </summary>
		virtual public System.String Refno
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Refno);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Refno, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.RECEIPTNO
		/// </summary>
		virtual public System.String Receiptno
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Receiptno);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Receiptno, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.REFDT
		/// </summary>
		virtual public System.String Refdt
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Refdt);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Refdt, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PID
		/// </summary>
		virtual public System.String Pid
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Pid);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Pid, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PTYPE
		/// </summary>
		virtual public System.String Ptype
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Ptype);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Ptype, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.SOURCECD
		/// </summary>
		virtual public System.String Sourcecd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Sourcecd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Sourcecd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PRIORITY
		/// </summary>
		virtual public System.String Priority
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Priority);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Priority, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PRICEGRP
		/// </summary>
		virtual public System.String Pricegrp
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Pricegrp);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Pricegrp, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.TOTAMT
		/// </summary>
		virtual public System.Decimal? Totamt
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Totamt);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Totamt, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PAYAMT
		/// </summary>
		virtual public System.Decimal? Payamt
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Payamt);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Payamt, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PAYSTATUS
		/// </summary>
		virtual public System.String Paystatus
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Paystatus);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Paystatus, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.ROOMNO
		/// </summary>
		virtual public System.String Roomno
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Roomno);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Roomno, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CRTDT
		/// </summary>
		virtual public System.String Crtdt
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Crtdt);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Crtdt, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CUSRID
		/// </summary>
		virtual public System.String Cusrid
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Cusrid);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Cusrid, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.LUPDDT
		/// </summary>
		virtual public System.String Lupddt
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Lupddt);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Lupddt, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.LUSRID
		/// </summary>
		virtual public System.String Lusrid
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Lusrid);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Lusrid, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.DELFLAG
		/// </summary>
		virtual public System.String Delflag
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Delflag);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Delflag, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CLINFO
		/// </summary>
		virtual public System.String Clinfo
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Clinfo);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Clinfo, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PNAME
		/// </summary>
		virtual public System.String Pname
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Pname);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Pname, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.SEX
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Sex);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Sex, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.AGEY
		/// </summary>
		virtual public System.String Agey
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Agey);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Agey, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.AGEM
		/// </summary>
		virtual public System.String Agem
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Agem);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Agem, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.AGED
		/// </summary>
		virtual public System.String Aged
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Aged);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Aged, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CLINICCD
		/// </summary>
		virtual public System.String Cliniccd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Cliniccd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Cliniccd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.REFTIME
		/// </summary>
		virtual public System.String Reftime
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Reftime);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Reftime, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.DOB
		/// </summary>
		virtual public System.String Dob
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Dob);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Dob, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.ADDR1
		/// </summary>
		virtual public System.String Addr1
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Addr1);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Addr1, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.ADDR2
		/// </summary>
		virtual public System.String Addr2
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Addr2);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Addr2, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.ADDR3
		/// </summary>
		virtual public System.String Addr3
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Addr3);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Addr3, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.ADDR4
		/// </summary>
		virtual public System.String Addr4
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Addr4);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Addr4, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.DISCPCT
		/// </summary>
		virtual public System.Decimal? Discpct
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Discpct);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Discpct, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.DISCAMT
		/// </summary>
		virtual public System.Decimal? Discamt
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Discamt);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Discamt, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.LABNO
		/// </summary>
		virtual public System.String Labno
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Labno);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Labno, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JSARANA
		/// </summary>
		virtual public System.Decimal? Jsarana
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jsarana);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jsarana, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JMEDIS
		/// </summary>
		virtual public System.Decimal? Jmedis
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jmedis);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jmedis, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.TGG
		/// </summary>
		virtual public System.Decimal? Tgg
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Tgg);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Tgg, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.KLB
		/// </summary>
		virtual public System.Decimal? Klb
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Klb);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Klb, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.KLBDESC
		/// </summary>
		virtual public System.String Klbdesc
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Klbdesc);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Klbdesc, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.SJP
		/// </summary>
		virtual public System.String Sjp
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Sjp);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Sjp, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.LOCACD
		/// </summary>
		virtual public System.String Locacd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Locacd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Locacd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.SHFCD
		/// </summary>
		virtual public System.String Shfcd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Shfcd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Shfcd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.OTHTGG
		/// </summary>
		virtual public System.Decimal? Othtgg
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Othtgg);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Othtgg, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JSAR
		/// </summary>
		virtual public System.Decimal? Jsar
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jsar);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jsar, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JMED
		/// </summary>
		virtual public System.Decimal? Jmed
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jmed);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jmed, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JSAROTH
		/// </summary>
		virtual public System.Decimal? Jsaroth
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jsaroth);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jsaroth, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JMEDOTH
		/// </summary>
		virtual public System.Decimal? Jmedoth
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jmedoth);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jmedoth, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.SOURCENM
		/// </summary>
		virtual public System.String Sourcenm
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Sourcenm);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Sourcenm, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CLINICNM
		/// </summary>
		virtual public System.String Clinicnm
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Clinicnm);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Clinicnm, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PRICENM
		/// </summary>
		virtual public System.String Pricenm
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Pricenm);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Pricenm, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CTGG
		/// </summary>
		virtual public System.String Ctgg
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Ctgg);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Ctgg, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.DEFPRICE
		/// </summary>
		virtual public System.String Defprice
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Defprice);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Defprice, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JSAR2A
		/// </summary>
		virtual public System.Decimal? Jsar2a
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jsar2a);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jsar2a, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JMED2A
		/// </summary>
		virtual public System.Decimal? Jmed2a
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Jmed2a);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Jmed2a, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.LOKET
		/// </summary>
		virtual public System.String Loket
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Loket);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Loket, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.SP_DESC
		/// </summary>
		virtual public System.String SpDesc
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.SpDesc);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.SpDesc, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.SP_CD
		/// </summary>
		virtual public System.String SpCd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.SpCd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.SpCd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PROG_VER
		/// </summary>
		virtual public System.String ProgVer
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.ProgVer);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.ProgVer, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.KENCANA
		/// </summary>
		virtual public System.Decimal? Kencana
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Kencana);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Kencana, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PATKLIN
		/// </summary>
		virtual public System.Decimal? Patklin
		{
			get
			{
				return base.GetSystemDecimal(LabOrderMetadata.ColumnNames.Patklin);
			}
			
			set
			{
				base.SetSystemDecimal(LabOrderMetadata.ColumnNames.Patklin, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CLSCD
		/// </summary>
		virtual public System.String Clscd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Clscd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Clscd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.CLSNM
		/// </summary>
		virtual public System.String Clsnm
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Clsnm);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Clsnm, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.TESTCD
		/// </summary>
		virtual public System.String Testcd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Testcd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Testcd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.TESTCDEXT
		/// </summary>
		virtual public System.String Testcdext
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Testcdext);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Testcdext, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JSARANAD
		/// </summary>
		virtual public System.String Jsaranad
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Jsaranad);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Jsaranad, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JMEDISD
		/// </summary>
		virtual public System.String Jmedisd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Jmedisd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Jmedisd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JSARD
		/// </summary>
		virtual public System.String Jsard
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Jsard);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Jsard, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JMEDD
		/// </summary>
		virtual public System.String Jmedd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Jmedd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Jmedd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JSAROTHD
		/// </summary>
		virtual public System.String Jsarothd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Jsarothd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Jsarothd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.JMEDOTHD
		/// </summary>
		virtual public System.String Jmedothd
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Jmedothd);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Jmedothd, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.KENCANAD
		/// </summary>
		virtual public System.String Kencanad
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Kencanad);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Kencanad, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.PATKLIND
		/// </summary>
		virtual public System.String Patklind
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Patklind);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Patklind, value);
			}
		}
		
		/// <summary>
		/// Maps to LabOrder.ISCONFIRM
		/// </summary>
		virtual public System.String Isconfirm
		{
			get
			{
				return base.GetSystemString(LabOrderMetadata.ColumnNames.Isconfirm);
			}
			
			set
			{
				base.SetSystemString(LabOrderMetadata.ColumnNames.Isconfirm, value);
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
			public esStrings(esLabOrder entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Msh
			{
				get
				{
					System.String data = entity.Msh;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Msh = null;
					else entity.Msh = Convert.ToString(value);
				}
			}
				
			public System.String Msfl
			{
				get
				{
					System.String data = entity.Msfl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Msfl = null;
					else entity.Msfl = Convert.ToString(value);
				}
			}
				
			public System.String Refno
			{
				get
				{
					System.String data = entity.Refno;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Refno = null;
					else entity.Refno = Convert.ToString(value);
				}
			}
				
			public System.String Receiptno
			{
				get
				{
					System.String data = entity.Receiptno;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Receiptno = null;
					else entity.Receiptno = Convert.ToString(value);
				}
			}
				
			public System.String Refdt
			{
				get
				{
					System.String data = entity.Refdt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Refdt = null;
					else entity.Refdt = Convert.ToString(value);
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
				
			public System.String Sourcecd
			{
				get
				{
					System.String data = entity.Sourcecd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sourcecd = null;
					else entity.Sourcecd = Convert.ToString(value);
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
				
			public System.String Pricegrp
			{
				get
				{
					System.String data = entity.Pricegrp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pricegrp = null;
					else entity.Pricegrp = Convert.ToString(value);
				}
			}
				
			public System.String Totamt
			{
				get
				{
					System.Decimal? data = entity.Totamt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Totamt = null;
					else entity.Totamt = Convert.ToDecimal(value);
				}
			}
				
			public System.String Payamt
			{
				get
				{
					System.Decimal? data = entity.Payamt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Payamt = null;
					else entity.Payamt = Convert.ToDecimal(value);
				}
			}
				
			public System.String Paystatus
			{
				get
				{
					System.String data = entity.Paystatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Paystatus = null;
					else entity.Paystatus = Convert.ToString(value);
				}
			}
				
			public System.String Roomno
			{
				get
				{
					System.String data = entity.Roomno;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Roomno = null;
					else entity.Roomno = Convert.ToString(value);
				}
			}
				
			public System.String Crtdt
			{
				get
				{
					System.String data = entity.Crtdt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Crtdt = null;
					else entity.Crtdt = Convert.ToString(value);
				}
			}
				
			public System.String Cusrid
			{
				get
				{
					System.String data = entity.Cusrid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cusrid = null;
					else entity.Cusrid = Convert.ToString(value);
				}
			}
				
			public System.String Lupddt
			{
				get
				{
					System.String data = entity.Lupddt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Lupddt = null;
					else entity.Lupddt = Convert.ToString(value);
				}
			}
				
			public System.String Lusrid
			{
				get
				{
					System.String data = entity.Lusrid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Lusrid = null;
					else entity.Lusrid = Convert.ToString(value);
				}
			}
				
			public System.String Delflag
			{
				get
				{
					System.String data = entity.Delflag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Delflag = null;
					else entity.Delflag = Convert.ToString(value);
				}
			}
				
			public System.String Clinfo
			{
				get
				{
					System.String data = entity.Clinfo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Clinfo = null;
					else entity.Clinfo = Convert.ToString(value);
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
				
			public System.String Agey
			{
				get
				{
					System.String data = entity.Agey;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Agey = null;
					else entity.Agey = Convert.ToString(value);
				}
			}
				
			public System.String Agem
			{
				get
				{
					System.String data = entity.Agem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Agem = null;
					else entity.Agem = Convert.ToString(value);
				}
			}
				
			public System.String Aged
			{
				get
				{
					System.String data = entity.Aged;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Aged = null;
					else entity.Aged = Convert.ToString(value);
				}
			}
				
			public System.String Cliniccd
			{
				get
				{
					System.String data = entity.Cliniccd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cliniccd = null;
					else entity.Cliniccd = Convert.ToString(value);
				}
			}
				
			public System.String Reftime
			{
				get
				{
					System.String data = entity.Reftime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reftime = null;
					else entity.Reftime = Convert.ToString(value);
				}
			}
				
			public System.String Dob
			{
				get
				{
					System.String data = entity.Dob;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Dob = null;
					else entity.Dob = Convert.ToString(value);
				}
			}
				
			public System.String Addr1
			{
				get
				{
					System.String data = entity.Addr1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Addr1 = null;
					else entity.Addr1 = Convert.ToString(value);
				}
			}
				
			public System.String Addr2
			{
				get
				{
					System.String data = entity.Addr2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Addr2 = null;
					else entity.Addr2 = Convert.ToString(value);
				}
			}
				
			public System.String Addr3
			{
				get
				{
					System.String data = entity.Addr3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Addr3 = null;
					else entity.Addr3 = Convert.ToString(value);
				}
			}
				
			public System.String Addr4
			{
				get
				{
					System.String data = entity.Addr4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Addr4 = null;
					else entity.Addr4 = Convert.ToString(value);
				}
			}
				
			public System.String Discpct
			{
				get
				{
					System.Decimal? data = entity.Discpct;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discpct = null;
					else entity.Discpct = Convert.ToDecimal(value);
				}
			}
				
			public System.String Discamt
			{
				get
				{
					System.Decimal? data = entity.Discamt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discamt = null;
					else entity.Discamt = Convert.ToDecimal(value);
				}
			}
				
			public System.String Labno
			{
				get
				{
					System.String data = entity.Labno;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Labno = null;
					else entity.Labno = Convert.ToString(value);
				}
			}
				
			public System.String Jsarana
			{
				get
				{
					System.Decimal? data = entity.Jsarana;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jsarana = null;
					else entity.Jsarana = Convert.ToDecimal(value);
				}
			}
				
			public System.String Jmedis
			{
				get
				{
					System.Decimal? data = entity.Jmedis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jmedis = null;
					else entity.Jmedis = Convert.ToDecimal(value);
				}
			}
				
			public System.String Tgg
			{
				get
				{
					System.Decimal? data = entity.Tgg;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tgg = null;
					else entity.Tgg = Convert.ToDecimal(value);
				}
			}
				
			public System.String Klb
			{
				get
				{
					System.Decimal? data = entity.Klb;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Klb = null;
					else entity.Klb = Convert.ToDecimal(value);
				}
			}
				
			public System.String Klbdesc
			{
				get
				{
					System.String data = entity.Klbdesc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Klbdesc = null;
					else entity.Klbdesc = Convert.ToString(value);
				}
			}
				
			public System.String Sjp
			{
				get
				{
					System.String data = entity.Sjp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sjp = null;
					else entity.Sjp = Convert.ToString(value);
				}
			}
				
			public System.String Locacd
			{
				get
				{
					System.String data = entity.Locacd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Locacd = null;
					else entity.Locacd = Convert.ToString(value);
				}
			}
				
			public System.String Shfcd
			{
				get
				{
					System.String data = entity.Shfcd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Shfcd = null;
					else entity.Shfcd = Convert.ToString(value);
				}
			}
				
			public System.String Othtgg
			{
				get
				{
					System.Decimal? data = entity.Othtgg;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Othtgg = null;
					else entity.Othtgg = Convert.ToDecimal(value);
				}
			}
				
			public System.String Jsar
			{
				get
				{
					System.Decimal? data = entity.Jsar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jsar = null;
					else entity.Jsar = Convert.ToDecimal(value);
				}
			}
				
			public System.String Jmed
			{
				get
				{
					System.Decimal? data = entity.Jmed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jmed = null;
					else entity.Jmed = Convert.ToDecimal(value);
				}
			}
				
			public System.String Jsaroth
			{
				get
				{
					System.Decimal? data = entity.Jsaroth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jsaroth = null;
					else entity.Jsaroth = Convert.ToDecimal(value);
				}
			}
				
			public System.String Jmedoth
			{
				get
				{
					System.Decimal? data = entity.Jmedoth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jmedoth = null;
					else entity.Jmedoth = Convert.ToDecimal(value);
				}
			}
				
			public System.String Sourcenm
			{
				get
				{
					System.String data = entity.Sourcenm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sourcenm = null;
					else entity.Sourcenm = Convert.ToString(value);
				}
			}
				
			public System.String Clinicnm
			{
				get
				{
					System.String data = entity.Clinicnm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Clinicnm = null;
					else entity.Clinicnm = Convert.ToString(value);
				}
			}
				
			public System.String Pricenm
			{
				get
				{
					System.String data = entity.Pricenm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pricenm = null;
					else entity.Pricenm = Convert.ToString(value);
				}
			}
				
			public System.String Ctgg
			{
				get
				{
					System.String data = entity.Ctgg;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ctgg = null;
					else entity.Ctgg = Convert.ToString(value);
				}
			}
				
			public System.String Defprice
			{
				get
				{
					System.String data = entity.Defprice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Defprice = null;
					else entity.Defprice = Convert.ToString(value);
				}
			}
				
			public System.String Jsar2a
			{
				get
				{
					System.Decimal? data = entity.Jsar2a;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jsar2a = null;
					else entity.Jsar2a = Convert.ToDecimal(value);
				}
			}
				
			public System.String Jmed2a
			{
				get
				{
					System.Decimal? data = entity.Jmed2a;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jmed2a = null;
					else entity.Jmed2a = Convert.ToDecimal(value);
				}
			}
				
			public System.String Loket
			{
				get
				{
					System.String data = entity.Loket;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Loket = null;
					else entity.Loket = Convert.ToString(value);
				}
			}
				
			public System.String SpDesc
			{
				get
				{
					System.String data = entity.SpDesc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpDesc = null;
					else entity.SpDesc = Convert.ToString(value);
				}
			}
				
			public System.String SpCd
			{
				get
				{
					System.String data = entity.SpCd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpCd = null;
					else entity.SpCd = Convert.ToString(value);
				}
			}
				
			public System.String ProgVer
			{
				get
				{
					System.String data = entity.ProgVer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgVer = null;
					else entity.ProgVer = Convert.ToString(value);
				}
			}
				
			public System.String Kencana
			{
				get
				{
					System.Decimal? data = entity.Kencana;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kencana = null;
					else entity.Kencana = Convert.ToDecimal(value);
				}
			}
				
			public System.String Patklin
			{
				get
				{
					System.Decimal? data = entity.Patklin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Patklin = null;
					else entity.Patklin = Convert.ToDecimal(value);
				}
			}
				
			public System.String Clscd
			{
				get
				{
					System.String data = entity.Clscd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Clscd = null;
					else entity.Clscd = Convert.ToString(value);
				}
			}
				
			public System.String Clsnm
			{
				get
				{
					System.String data = entity.Clsnm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Clsnm = null;
					else entity.Clsnm = Convert.ToString(value);
				}
			}
				
			public System.String Testcd
			{
				get
				{
					System.String data = entity.Testcd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Testcd = null;
					else entity.Testcd = Convert.ToString(value);
				}
			}
				
			public System.String Testcdext
			{
				get
				{
					System.String data = entity.Testcdext;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Testcdext = null;
					else entity.Testcdext = Convert.ToString(value);
				}
			}
				
			public System.String Jsaranad
			{
				get
				{
					System.String data = entity.Jsaranad;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jsaranad = null;
					else entity.Jsaranad = Convert.ToString(value);
				}
			}
				
			public System.String Jmedisd
			{
				get
				{
					System.String data = entity.Jmedisd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jmedisd = null;
					else entity.Jmedisd = Convert.ToString(value);
				}
			}
				
			public System.String Jsard
			{
				get
				{
					System.String data = entity.Jsard;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jsard = null;
					else entity.Jsard = Convert.ToString(value);
				}
			}
				
			public System.String Jmedd
			{
				get
				{
					System.String data = entity.Jmedd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jmedd = null;
					else entity.Jmedd = Convert.ToString(value);
				}
			}
				
			public System.String Jsarothd
			{
				get
				{
					System.String data = entity.Jsarothd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jsarothd = null;
					else entity.Jsarothd = Convert.ToString(value);
				}
			}
				
			public System.String Jmedothd
			{
				get
				{
					System.String data = entity.Jmedothd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jmedothd = null;
					else entity.Jmedothd = Convert.ToString(value);
				}
			}
				
			public System.String Kencanad
			{
				get
				{
					System.String data = entity.Kencanad;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kencanad = null;
					else entity.Kencanad = Convert.ToString(value);
				}
			}
				
			public System.String Patklind
			{
				get
				{
					System.String data = entity.Patklind;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Patklind = null;
					else entity.Patklind = Convert.ToString(value);
				}
			}
				
			public System.String Isconfirm
			{
				get
				{
					System.String data = entity.Isconfirm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Isconfirm = null;
					else entity.Isconfirm = Convert.ToString(value);
				}
			}
			

			private esLabOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLabOrderQuery query)
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
				throw new Exception("esLabOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esLabOrderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LabOrderMetadata.Meta();
			}
		}	
		

		public esQueryItem Msh
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Msh, esSystemType.String);
			}
		} 
		
		public esQueryItem Msfl
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Msfl, esSystemType.String);
			}
		} 
		
		public esQueryItem Refno
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Refno, esSystemType.String);
			}
		} 
		
		public esQueryItem Receiptno
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Receiptno, esSystemType.String);
			}
		} 
		
		public esQueryItem Refdt
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Refdt, esSystemType.String);
			}
		} 
		
		public esQueryItem Pid
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Pid, esSystemType.String);
			}
		} 
		
		public esQueryItem Ptype
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Ptype, esSystemType.String);
			}
		} 
		
		public esQueryItem Sourcecd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Sourcecd, esSystemType.String);
			}
		} 
		
		public esQueryItem Priority
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Priority, esSystemType.String);
			}
		} 
		
		public esQueryItem Pricegrp
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Pricegrp, esSystemType.String);
			}
		} 
		
		public esQueryItem Totamt
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Totamt, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Payamt
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Payamt, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Paystatus
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Paystatus, esSystemType.String);
			}
		} 
		
		public esQueryItem Roomno
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Roomno, esSystemType.String);
			}
		} 
		
		public esQueryItem Crtdt
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Crtdt, esSystemType.String);
			}
		} 
		
		public esQueryItem Cusrid
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Cusrid, esSystemType.String);
			}
		} 
		
		public esQueryItem Lupddt
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Lupddt, esSystemType.String);
			}
		} 
		
		public esQueryItem Lusrid
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Lusrid, esSystemType.String);
			}
		} 
		
		public esQueryItem Delflag
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Delflag, esSystemType.String);
			}
		} 
		
		public esQueryItem Clinfo
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Clinfo, esSystemType.String);
			}
		} 
		
		public esQueryItem Pname
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Pname, esSystemType.String);
			}
		} 
		
		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Sex, esSystemType.String);
			}
		} 
		
		public esQueryItem Agey
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Agey, esSystemType.String);
			}
		} 
		
		public esQueryItem Agem
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Agem, esSystemType.String);
			}
		} 
		
		public esQueryItem Aged
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Aged, esSystemType.String);
			}
		} 
		
		public esQueryItem Cliniccd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Cliniccd, esSystemType.String);
			}
		} 
		
		public esQueryItem Reftime
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Reftime, esSystemType.String);
			}
		} 
		
		public esQueryItem Dob
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Dob, esSystemType.String);
			}
		} 
		
		public esQueryItem Addr1
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Addr1, esSystemType.String);
			}
		} 
		
		public esQueryItem Addr2
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Addr2, esSystemType.String);
			}
		} 
		
		public esQueryItem Addr3
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Addr3, esSystemType.String);
			}
		} 
		
		public esQueryItem Addr4
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Addr4, esSystemType.String);
			}
		} 
		
		public esQueryItem Discpct
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Discpct, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discamt
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Discamt, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Labno
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Labno, esSystemType.String);
			}
		} 
		
		public esQueryItem Jsarana
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jsarana, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Jmedis
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jmedis, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Tgg
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Tgg, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Klb
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Klb, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Klbdesc
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Klbdesc, esSystemType.String);
			}
		} 
		
		public esQueryItem Sjp
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Sjp, esSystemType.String);
			}
		} 
		
		public esQueryItem Locacd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Locacd, esSystemType.String);
			}
		} 
		
		public esQueryItem Shfcd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Shfcd, esSystemType.String);
			}
		} 
		
		public esQueryItem Othtgg
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Othtgg, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Jsar
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jsar, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Jmed
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jmed, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Jsaroth
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jsaroth, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Jmedoth
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jmedoth, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Sourcenm
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Sourcenm, esSystemType.String);
			}
		} 
		
		public esQueryItem Clinicnm
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Clinicnm, esSystemType.String);
			}
		} 
		
		public esQueryItem Pricenm
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Pricenm, esSystemType.String);
			}
		} 
		
		public esQueryItem Ctgg
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Ctgg, esSystemType.String);
			}
		} 
		
		public esQueryItem Defprice
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Defprice, esSystemType.String);
			}
		} 
		
		public esQueryItem Jsar2a
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jsar2a, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Jmed2a
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jmed2a, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Loket
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Loket, esSystemType.String);
			}
		} 
		
		public esQueryItem SpDesc
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.SpDesc, esSystemType.String);
			}
		} 
		
		public esQueryItem SpCd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.SpCd, esSystemType.String);
			}
		} 
		
		public esQueryItem ProgVer
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.ProgVer, esSystemType.String);
			}
		} 
		
		public esQueryItem Kencana
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Kencana, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Patklin
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Patklin, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Clscd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Clscd, esSystemType.String);
			}
		} 
		
		public esQueryItem Clsnm
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Clsnm, esSystemType.String);
			}
		} 
		
		public esQueryItem Testcd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Testcd, esSystemType.String);
			}
		} 
		
		public esQueryItem Testcdext
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Testcdext, esSystemType.String);
			}
		} 
		
		public esQueryItem Jsaranad
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jsaranad, esSystemType.String);
			}
		} 
		
		public esQueryItem Jmedisd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jmedisd, esSystemType.String);
			}
		} 
		
		public esQueryItem Jsard
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jsard, esSystemType.String);
			}
		} 
		
		public esQueryItem Jmedd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jmedd, esSystemType.String);
			}
		} 
		
		public esQueryItem Jsarothd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jsarothd, esSystemType.String);
			}
		} 
		
		public esQueryItem Jmedothd
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Jmedothd, esSystemType.String);
			}
		} 
		
		public esQueryItem Kencanad
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Kencanad, esSystemType.String);
			}
		} 
		
		public esQueryItem Patklind
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Patklind, esSystemType.String);
			}
		} 
		
		public esQueryItem Isconfirm
		{
			get
			{
				return new esQueryItem(this, LabOrderMetadata.ColumnNames.Isconfirm, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LabOrderCollection")]
	public partial class LabOrderCollection : esLabOrderCollection, IEnumerable<LabOrder>
	{
		public LabOrderCollection()
		{

		}
		
		public static implicit operator List<LabOrder>(LabOrderCollection coll)
		{
			List<LabOrder> list = new List<LabOrder>();
			
			foreach (LabOrder emp in coll)
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
				return  LabOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LabOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LabOrder();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LabOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LabOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LabOrder AddNew()
		{
			LabOrder entity = base.AddNewEntity() as LabOrder;
			
			return entity;
		}

		public LabOrder FindByPrimaryKey(System.String receiptno)
		{
			return base.FindByPrimaryKey(receiptno) as LabOrder;
		}


		#region IEnumerable<LabOrder> Members

		IEnumerator<LabOrder> IEnumerable<LabOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LabOrder;
			}
		}

		#endregion
		
		private LabOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LabOrder' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LabOrder ({Receiptno})")]
	[Serializable]
	public partial class LabOrder : esLabOrder
	{
		public LabOrder()
		{

		}
	
		public LabOrder(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LabOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esLabOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LabOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LabOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LabOrderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LabOrderQuery : esLabOrderQuery
	{
		public LabOrderQuery()
		{

		}		
		
		public LabOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LabOrderQuery";
        }
		
			
	}


	[Serializable]
	public partial class LabOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LabOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Msh, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Msh;
			c.CharacterMaxLength = 2;
			c.HasDefault = true;
			c.Default = @"('[]')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Msfl, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Msfl;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Refno, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Refno;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Receiptno, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Receiptno;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Refdt, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Refdt;
			c.CharacterMaxLength = 8;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Pid, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Pid;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Ptype, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Ptype;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Sourcecd, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Sourcecd;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Priority, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Priority;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Pricegrp, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Pricegrp;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Totamt, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Totamt;
			c.NumericPrecision = 9;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Payamt, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Payamt;
			c.NumericPrecision = 9;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Paystatus, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Paystatus;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Roomno, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Roomno;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Crtdt, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Crtdt;
			c.CharacterMaxLength = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Cusrid, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Cusrid;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Lupddt, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Lupddt;
			c.CharacterMaxLength = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Lusrid, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Lusrid;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Delflag, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Delflag;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Clinfo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Clinfo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Pname, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Pname;
			c.CharacterMaxLength = 150;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Sex, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Agey, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Agey;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Agem, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Agem;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Aged, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Aged;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Cliniccd, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Cliniccd;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Reftime, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Reftime;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Dob, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Dob;
			c.CharacterMaxLength = 8;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Addr1, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Addr1;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Addr2, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Addr2;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Addr3, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Addr3;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Addr4, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Addr4;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Discpct, 32, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Discpct;
			c.NumericPrecision = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Discamt, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Discamt;
			c.NumericPrecision = 9;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Labno, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Labno;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jsarana, 35, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jsarana;
			c.NumericPrecision = 9;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jmedis, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jmedis;
			c.NumericPrecision = 9;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Tgg, 37, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Tgg;
			c.NumericPrecision = 9;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Klb, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Klb;
			c.NumericPrecision = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Klbdesc, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Klbdesc;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Sjp, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Sjp;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Locacd, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Locacd;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Shfcd, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Shfcd;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Othtgg, 43, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Othtgg;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jsar, 44, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jsar;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jmed, 45, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jmed;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jsaroth, 46, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jsaroth;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jmedoth, 47, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jmedoth;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Sourcenm, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Sourcenm;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Clinicnm, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Clinicnm;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Pricenm, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Pricenm;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Ctgg, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Ctgg;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Defprice, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Defprice;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jsar2a, 53, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jsar2a;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jmed2a, 54, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jmed2a;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Loket, 55, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Loket;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.SpDesc, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.SpDesc;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.SpCd, 57, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.SpCd;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.ProgVer, 58, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.ProgVer;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Kencana, 59, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Kencana;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Patklin, 60, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LabOrderMetadata.PropertyNames.Patklin;
			c.NumericPrecision = 9;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Clscd, 61, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Clscd;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Clsnm, 62, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Clsnm;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Testcd, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Testcd;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Testcdext, 64, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Testcdext;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jsaranad, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jsaranad;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jmedisd, 66, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jmedisd;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jsard, 67, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jsard;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jmedd, 68, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jmedd;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jsarothd, 69, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jsarothd;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Jmedothd, 70, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Jmedothd;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Kencanad, 71, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Kencanad;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Patklind, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Patklind;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabOrderMetadata.ColumnNames.Isconfirm, 73, typeof(System.String), esSystemType.String);
			c.PropertyName = LabOrderMetadata.PropertyNames.Isconfirm;
			c.CharacterMaxLength = 1;
			c.HasDefault = true;
			c.Default = @"('0')";
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LabOrderMetadata Meta()
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
			 public const string Msh = "MSH";
			 public const string Msfl = "MSFL";
			 public const string Refno = "REFNO";
			 public const string Receiptno = "RECEIPTNO";
			 public const string Refdt = "REFDT";
			 public const string Pid = "PID";
			 public const string Ptype = "PTYPE";
			 public const string Sourcecd = "SOURCECD";
			 public const string Priority = "PRIORITY";
			 public const string Pricegrp = "PRICEGRP";
			 public const string Totamt = "TOTAMT";
			 public const string Payamt = "PAYAMT";
			 public const string Paystatus = "PAYSTATUS";
			 public const string Roomno = "ROOMNO";
			 public const string Crtdt = "CRTDT";
			 public const string Cusrid = "CUSRID";
			 public const string Lupddt = "LUPDDT";
			 public const string Lusrid = "LUSRID";
			 public const string Delflag = "DELFLAG";
			 public const string Clinfo = "CLINFO";
			 public const string Pname = "PNAME";
			 public const string Sex = "SEX";
			 public const string Agey = "AGEY";
			 public const string Agem = "AGEM";
			 public const string Aged = "AGED";
			 public const string Cliniccd = "CLINICCD";
			 public const string Reftime = "REFTIME";
			 public const string Dob = "DOB";
			 public const string Addr1 = "ADDR1";
			 public const string Addr2 = "ADDR2";
			 public const string Addr3 = "ADDR3";
			 public const string Addr4 = "ADDR4";
			 public const string Discpct = "DISCPCT";
			 public const string Discamt = "DISCAMT";
			 public const string Labno = "LABNO";
			 public const string Jsarana = "JSARANA";
			 public const string Jmedis = "JMEDIS";
			 public const string Tgg = "TGG";
			 public const string Klb = "KLB";
			 public const string Klbdesc = "KLBDESC";
			 public const string Sjp = "SJP";
			 public const string Locacd = "LOCACD";
			 public const string Shfcd = "SHFCD";
			 public const string Othtgg = "OTHTGG";
			 public const string Jsar = "JSAR";
			 public const string Jmed = "JMED";
			 public const string Jsaroth = "JSAROTH";
			 public const string Jmedoth = "JMEDOTH";
			 public const string Sourcenm = "SOURCENM";
			 public const string Clinicnm = "CLINICNM";
			 public const string Pricenm = "PRICENM";
			 public const string Ctgg = "CTGG";
			 public const string Defprice = "DEFPRICE";
			 public const string Jsar2a = "JSAR2A";
			 public const string Jmed2a = "JMED2A";
			 public const string Loket = "LOKET";
			 public const string SpDesc = "SP_DESC";
			 public const string SpCd = "SP_CD";
			 public const string ProgVer = "PROG_VER";
			 public const string Kencana = "KENCANA";
			 public const string Patklin = "PATKLIN";
			 public const string Clscd = "CLSCD";
			 public const string Clsnm = "CLSNM";
			 public const string Testcd = "TESTCD";
			 public const string Testcdext = "TESTCDEXT";
			 public const string Jsaranad = "JSARANAD";
			 public const string Jmedisd = "JMEDISD";
			 public const string Jsard = "JSARD";
			 public const string Jmedd = "JMEDD";
			 public const string Jsarothd = "JSAROTHD";
			 public const string Jmedothd = "JMEDOTHD";
			 public const string Kencanad = "KENCANAD";
			 public const string Patklind = "PATKLIND";
			 public const string Isconfirm = "ISCONFIRM";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Msh = "Msh";
			 public const string Msfl = "Msfl";
			 public const string Refno = "Refno";
			 public const string Receiptno = "Receiptno";
			 public const string Refdt = "Refdt";
			 public const string Pid = "Pid";
			 public const string Ptype = "Ptype";
			 public const string Sourcecd = "Sourcecd";
			 public const string Priority = "Priority";
			 public const string Pricegrp = "Pricegrp";
			 public const string Totamt = "Totamt";
			 public const string Payamt = "Payamt";
			 public const string Paystatus = "Paystatus";
			 public const string Roomno = "Roomno";
			 public const string Crtdt = "Crtdt";
			 public const string Cusrid = "Cusrid";
			 public const string Lupddt = "Lupddt";
			 public const string Lusrid = "Lusrid";
			 public const string Delflag = "Delflag";
			 public const string Clinfo = "Clinfo";
			 public const string Pname = "Pname";
			 public const string Sex = "Sex";
			 public const string Agey = "Agey";
			 public const string Agem = "Agem";
			 public const string Aged = "Aged";
			 public const string Cliniccd = "Cliniccd";
			 public const string Reftime = "Reftime";
			 public const string Dob = "Dob";
			 public const string Addr1 = "Addr1";
			 public const string Addr2 = "Addr2";
			 public const string Addr3 = "Addr3";
			 public const string Addr4 = "Addr4";
			 public const string Discpct = "Discpct";
			 public const string Discamt = "Discamt";
			 public const string Labno = "Labno";
			 public const string Jsarana = "Jsarana";
			 public const string Jmedis = "Jmedis";
			 public const string Tgg = "Tgg";
			 public const string Klb = "Klb";
			 public const string Klbdesc = "Klbdesc";
			 public const string Sjp = "Sjp";
			 public const string Locacd = "Locacd";
			 public const string Shfcd = "Shfcd";
			 public const string Othtgg = "Othtgg";
			 public const string Jsar = "Jsar";
			 public const string Jmed = "Jmed";
			 public const string Jsaroth = "Jsaroth";
			 public const string Jmedoth = "Jmedoth";
			 public const string Sourcenm = "Sourcenm";
			 public const string Clinicnm = "Clinicnm";
			 public const string Pricenm = "Pricenm";
			 public const string Ctgg = "Ctgg";
			 public const string Defprice = "Defprice";
			 public const string Jsar2a = "Jsar2a";
			 public const string Jmed2a = "Jmed2a";
			 public const string Loket = "Loket";
			 public const string SpDesc = "SpDesc";
			 public const string SpCd = "SpCd";
			 public const string ProgVer = "ProgVer";
			 public const string Kencana = "Kencana";
			 public const string Patklin = "Patklin";
			 public const string Clscd = "Clscd";
			 public const string Clsnm = "Clsnm";
			 public const string Testcd = "Testcd";
			 public const string Testcdext = "Testcdext";
			 public const string Jsaranad = "Jsaranad";
			 public const string Jmedisd = "Jmedisd";
			 public const string Jsard = "Jsard";
			 public const string Jmedd = "Jmedd";
			 public const string Jsarothd = "Jsarothd";
			 public const string Jmedothd = "Jmedothd";
			 public const string Kencanad = "Kencanad";
			 public const string Patklind = "Patklind";
			 public const string Isconfirm = "Isconfirm";
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
			lock (typeof(LabOrderMetadata))
			{
				if(LabOrderMetadata.mapDelegates == null)
				{
					LabOrderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LabOrderMetadata.meta == null)
				{
					LabOrderMetadata.meta = new LabOrderMetadata();
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
				

				meta.AddTypeMap("Msh", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Msfl", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Refno", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Receiptno", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Refdt", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Pid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ptype", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sourcecd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Priority", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Pricegrp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Totamt", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Payamt", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Paystatus", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Roomno", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Crtdt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Cusrid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Lupddt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Lusrid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Delflag", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Clinfo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pname", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Agey", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Agem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Aged", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Cliniccd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Reftime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Dob", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Addr1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Addr2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Addr3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Addr4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Discpct", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discamt", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Labno", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jsarana", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Jmedis", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Tgg", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Klb", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Klbdesc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sjp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Locacd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Shfcd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Othtgg", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Jsar", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Jmed", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Jsaroth", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Jmedoth", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Sourcenm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Clinicnm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pricenm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ctgg", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Defprice", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jsar2a", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Jmed2a", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Loket", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpDesc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpCd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProgVer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kencana", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Patklin", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Clscd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Clsnm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Testcd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Testcdext", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jsaranad", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jmedisd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jsard", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jmedd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jsarothd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jmedothd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kencanad", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Patklind", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Isconfirm", new esTypeMap("char", "System.String"));			
				
				
				
				meta.Source = "LabOrder";
				meta.Destination = "LabOrder";
				
				meta.spInsert = "proc_LabOrderInsert";				
				meta.spUpdate = "proc_LabOrderUpdate";		
				meta.spDelete = "proc_LabOrderDelete";
				meta.spLoadAll = "proc_LabOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_LabOrderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LabOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
