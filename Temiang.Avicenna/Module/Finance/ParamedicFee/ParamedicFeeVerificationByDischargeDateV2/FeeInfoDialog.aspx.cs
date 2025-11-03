using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using Telerik.Reporting;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class FeeInfoDialog : BasePageDialog
    {
        private string TransactionNo
        {
            get { return Request.QueryString["TransactionNo"]; }
        }
        private string SequenceNo
        {
            get { return Request.QueryString["SequenceNo"]; }
        }
        private string TariffComponentID
        {
            get { return Request.QueryString["TariffComponentID"]; }
        }
        private string ParamedicID
        {
            get { return Request.QueryString["ParamedicID"]; }
        }
        private bool IsPhysicianMember
        {
            get { 
                return System.Convert.ToBoolean(Request.QueryString["IsPhysicianMember"]); 
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool isOld = true;
                var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                if (fee.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID))
                {
                    if (!string.IsNullOrEmpty(fee.SRPhysicianFeeCategory)) {
                        var iV = System.Convert.ToInt32(fee.SRPhysicianFeeCategory);
                        if (iV >= 7) {
                            SetExecutedFormulaV7(GetExecutedFormulaV7());
                            isOld = false;
                        }
                    }
                }

                if(isOld) SetExecutedFormula(GetExecutedFormula());
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = ''";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
        #region ExecutedFormula Old
        private List<ExecutedFormula> GetExecutedFormula() {
           
            var ef = new ParamedicFeeExecutedFormula();
            if (ef.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID))
            {
                if (!string.IsNullOrEmpty(ef.ExecutedFormula))
                {
                    var EFs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExecutedFormula>>(ef.ExecutedFormula);
                    return EFs;
                }
            }
            else {
                var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                if (fee.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID))
                {
                    if (!string.IsNullOrEmpty(fee.ExecutedFormula))
                    {
                        var EFs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExecutedFormula>>(fee.ExecutedFormula);
                        return EFs;
                    }
                }
            }
            return new List<ExecutedFormula>();
        }

        private void SetExecutedFormula(List<ExecutedFormula> EFs) {
            foreach (var ef in EFs) {
                var table = new HtmlTable();

                var row = new HtmlTableRow();
                var cell = new HtmlTableCell();
                cell.InnerText = "Level"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = ef.Lvl.ToString(); cell.Attributes.Add("class", "");
                row.Cells.Add(cell);
                table.Rows.Add(row);

                var feeSetting = new ParamedicFeeByFee4ServiceSetting();
                feeSetting.LoadByPrimaryKey(ef.SId);

                if (!string.IsNullOrEmpty(feeSetting.Notes)) {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = "Formula Name"; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerText = feeSetting.Notes; cell.Attributes.Add("class", "");
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }

                if (!string.IsNullOrEmpty(feeSetting.Formula)) {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = "Formula"; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerText = feeSetting.Formula; cell.Attributes.Add("class", "");
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }

                if (!string.IsNullOrEmpty(ef.Fml)) {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = "Executed Formula"; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerText = ef.Fml; cell.Attributes.Add("class", "");
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }

                if (ef.Params.Count() > 0)
                {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = "Params"; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerText = string.Join(", ", ef.Params.Select(p => string.Format("{0}:{1}", p.Key, p.Value.ToString()))); cell.Attributes.Add("class", "");
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }

                var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                if (fee.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID)) {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = "Qty"; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerText = fee.Qty.ToString();
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }

                if (!string.IsNullOrEmpty(ef.ExecMsg)) {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = "Execute Message"; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerText = ef.ExecMsg;
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }
                if (!string.IsNullOrEmpty(fee.ExecutedMessage))
                {
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.InnerText = "Execute Message"; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerText = fee.ExecutedMessage;
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }

                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "Level Value"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = ef.Val.ToString("N2"); cell.Attributes.Add("class", "");
                row.Cells.Add(cell);
                table.Rows.Add(row);

                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "";
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = "";
                row.Cells.Add(cell);
                table.Rows.Add(row);

                content.Controls.Add(table);
            }
        }
        #endregion

        #region ExecutedFormula V7
        private List<ExecutedFormulaV7> GetExecutedFormulaV7()
        {

            var ef = new ParamedicFeeExecutedFormula();
            if (ef.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID))
            {
                if (!string.IsNullOrEmpty(ef.ExecutedFormula))
                {
                    var EFs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExecutedFormulaV7>>(ef.ExecutedFormula);
                    return EFs;
                }
            }
            return new List<ExecutedFormulaV7>();
        }

        private void SetExecutedFormulaV7(List<ExecutedFormulaV7> EFs)
        {
            ExecutedFormulaForFeeByTeam(EFs);

            var tableB = new HtmlTable();

            var rowB = new HtmlTableRow();
            var cellB = new HtmlTableCell();
            cellB.InnerText = "BRUTO"; cellB.Attributes.Add("class", "label");
            rowB.Cells.Add(cellB);
            cellB = new HtmlTableCell();
            cellB.InnerText = ""; cellB.Attributes.Add("class", "");
            rowB.Cells.Add(cellB);
            tableB.Rows.Add(rowB);

            content.Controls.Add(tableB);

            foreach (var ef in EFs.Where(ef => !ef.IsNetto))
            {
                AddTableExecutedFormula(ef);
            }

            var tableN = new HtmlTable();

            var rowN = new HtmlTableRow();
            var cellN = new HtmlTableCell();
            cellN.InnerText = "NETTO"; cellN.Attributes.Add("class", "label");
            rowN.Cells.Add(cellN);
            cellN = new HtmlTableCell();
            cellN.InnerText = ""; cellN.Attributes.Add("class", "");
            rowN.Cells.Add(cellN);
            tableN.Rows.Add(rowN);

            content.Controls.Add(tableN);

            foreach (var ef in EFs.Where(ef => ef.IsNetto))
            {
                AddTableExecutedFormula(ef);
            }

        }

        private void ExecutedFormulaForFeeByTeam(List<ExecutedFormulaV7> EFs) {
            if (IsPhysicianMember) {
                var feeMember = new ParamedicFeeTransChargesItemCompByTeam();
                if (feeMember.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID, ParamedicID)) {
                    var efb = new ExecutedFormulaV7();
                    efb.Lvl = 99;
                    efb.IsNetto = false;
                    efb.Fml = string.Format("@prevLevel * {0} / 100", (feeMember.CalculatedAmount ?? 0).ToString());
                    efb.Val = feeMember.FeeAmountBruto ?? 0;
                    efb.Dat = new Dictionary<string, string>();
                    EFs.Add(efb);

                    var efn = new ExecutedFormulaV7();
                    efn.Lvl = 99;
                    efn.IsNetto = true;
                    efn.Fml = string.Format("@prevLevel * {0} / 100", (feeMember.CalculatedAmount ?? 0).ToString());
                    efn.Val = feeMember.FeeAmount ?? 0;
                    efn.Dat = new Dictionary<string, string>();
                    EFs.Add(efn);
                }
            }
        }

        private void AddTableExecutedFormula(ExecutedFormulaV7 ef)
        {
            var table = new HtmlTable();

            var row = new HtmlTableRow();
            var cell = new HtmlTableCell();
            cell.InnerText = "Level"; cell.Attributes.Add("class", "label");
            row.Cells.Add(cell);
            cell = new HtmlTableCell();
            cell.InnerText = ef.Lvl.ToString(); cell.Attributes.Add("class", "");
            row.Cells.Add(cell);
            table.Rows.Add(row);

            var feeSetting = new ParamedicFeeByFee4ServiceSetting();
            feeSetting.LoadByPrimaryKey(ef.SId);

            if (!string.IsNullOrEmpty(feeSetting.Notes))
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "Formula Name"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = feeSetting.Notes; cell.Attributes.Add("class", "");
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }

            if (!string.IsNullOrEmpty(feeSetting.Formula))
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "Formula"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = feeSetting.Formula; cell.Attributes.Add("class", "");
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }

            if (!string.IsNullOrEmpty(ef.Fml))
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "Executed Formula"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = ef.Fml; cell.Attributes.Add("class", "");
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }

            if (ef.Params.Count() > 0 || ef.Dat.Count() > 0)
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "Params"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = string.Join(", ", 
                    ef.Params.Select(p => string.Format("{0}:{1}", p.Key, p.Value.ToString()))
                    .Union(ef.Dat.Select(p => string.Format("{0}:{1}", p.Key, p.Value.ToString())))
                    .ToArray()
                    );
                cell.Attributes.Add("class", "");
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }

            var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
            if (fee.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID))
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "Qty"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = fee.Qty.ToString();
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }

            if (!string.IsNullOrEmpty(ef.ExecMsg) || !string.IsNullOrEmpty(ef.CbgExcp.Message))
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                cell.InnerText = "Execute Message"; cell.Attributes.Add("class", "label");
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                cell.InnerText = ef.ExecMsg + (string.IsNullOrEmpty(ef.ExecMsg) ? "" : (string.IsNullOrEmpty(ef.CbgExcp.Message)? "" : ", ")) + ("Cbg Error: " + ef.CbgExcp.Message ?? "");
                cell.Style.Add("color", "red");
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }

            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            cell.InnerText = "Level Value"; cell.Attributes.Add("class", "label");
            row.Cells.Add(cell);
            cell = new HtmlTableCell();
            cell.InnerText = ef.Val.ToString("N2"); cell.Attributes.Add("class", "");
            row.Cells.Add(cell);
            table.Rows.Add(row);

            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            cell.InnerText = "";
            row.Cells.Add(cell);
            cell = new HtmlTableCell();
            cell.InnerText = "";
            row.Cells.Add(cell);
            table.Rows.Add(row);

            content.Controls.Add(table);
        }
        #endregion
    }
}
