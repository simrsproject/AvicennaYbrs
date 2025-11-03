using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class TableRespondDialog : BasePageDialog
    {
        private string RegistrationNo {
            get {
                return Request.QueryString["regno"];
            }
        }
        private string[] TemplateIDs
        {
            get
            {
                var tid = Request.QueryString["tid"];
                var tids = tid.Split(',');
                return tids;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.EpisodeAndHistory;

            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Empty;
        }

        public string GenerateTable() {

            System.Text.StringBuilder stab = new System.Text.StringBuilder();

            foreach (var TemplateID in TemplateIDs)
            {
                var dtb = (new NursingDiagnosaTransDTCollection())
                    .PhrGetDataByIdTemplateImplementation(RegistrationNo, TemplateID);

                var dDatetimes = dtb.AsEnumerable().Select(x => x.Field<DateTime>("RecordDate")).Distinct();
                var dDates = dDatetimes.Select(x => x.Date).Distinct();
                var rowQ = dtb.AsEnumerable().Select(x =>
                new
                {
                    idx = x.Field<int>("RowIndex"),
                    qid = x.Field<string>("QuestionID"),
                    qtx = x.Field<string>("QuestionText"),
                }).Distinct();

                stab.Append("<table class=\"pretty-table\">");

                // header tgl
                stab.Append("HeaderTgl");
                DateTime cDate = new DateTime(1999, 1, 1);
                var cSpan = 0;
                var sHTgl = string.Empty;

                // header jam
                stab.Append("<tr>");
                stab.Append("<th scope=\"col\">"); stab.Append("</td>");
                int i = 0;
                foreach (var dDatetime in dDatetimes)
                {
                    //==========
                    if (cDate.Date != dDatetime.Date)
                    {
                        if (cDate != (new DateTime(1999, 1, 1)))
                        {
                            sHTgl = sHTgl + "<th scope=\"col\" colspan='" + cSpan * 2 + "'>" + cDate.ToString("MM/dd/yyyy") + "</th>";
                        }
                        cSpan = 1;
                        cDate = dDatetime;
                    }
                    else
                    {
                        cSpan += 1;
                    }
                    i += 1;

                    if (dDatetimes.Count() == i)
                    {
                        sHTgl = sHTgl + "<th scope=\"col\" colspan='" + cSpan * 2 + "'>" + cDate.ToString("MM/dd/yyyy") + "</th>";
                    }
                    //============

                    stab.Append("<th scope=\"col\" colspan='2'>");
                    stab.Append(dDatetime.ToString("HH:mm"));
                    stab.Append("</th>");
                }
                stab.Append("</tr>");

                stab.Replace("HeaderTgl", "<th scope=\"col\"></th>" + sHTgl);

                i = 0;
                foreach (var rq in rowQ)
                {
                    stab.Append(string.Format("<tr {0}>", (i%2 == 1) ? "class=\"alt\"":""));
                    stab.Append("<th scope=\"row\" align=\"left\">");
                    stab.Append(rq.qtx);
                    stab.Append("</th>");
                    foreach (var dDatetime in dDatetimes)
                    {

                        var xRow = dtb.AsEnumerable().Where(x => x.Field<string>("QuestionID") == rq.qid &&
                        x.Field<DateTime>("RecordDate") == dDatetime).FirstOrDefault();

                        switch (xRow == null ? "" : xRow.Field<string>("SRAnswerType"))
                        {
                            case "NUM":
                                {
                                    stab.Append("<td align=\"right\">");
                                    stab.Append((xRow.Field<decimal>("QuestionAnswerNum")).ToString("N" + xRow.Field<int?>("AnswerDecimalDigit") ?? 0.ToString()));
                                    stab.Append("</td>");
                                    stab.Append("<td>");
                                    stab.Append(xRow.Field<string>("QuestionAnswerSuffix"));
                                    stab.Append("</td>");

                                    break;
                                }
                            default:
                                {
                                    stab.Append("<td colspan='2'>");
                                    stab.Append(xRow == null ? "" : xRow.Field<string>("QuestionAnswerText"));
                                    stab.Append("</td>");
                                    break;
                                }
                        }

                    }
                    stab.Append("</tr>");
                }
                stab.Append("</table>");
                stab.Append("<BR /><BR />");
            }

            return stab.ToString();
        }
    }
}
