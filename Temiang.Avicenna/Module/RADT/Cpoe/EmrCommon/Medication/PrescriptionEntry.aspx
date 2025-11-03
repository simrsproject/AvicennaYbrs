<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PrescriptionEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphList" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboLocationID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransPrescriptionItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" />
                    <telerik:AjaxUpdatedControl ControlID="tabPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="lblZatActiveInteraction" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrescriptionTemplate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionTemplate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <style>
            .AutoHeightGridClass .rgDataDiv {
                height: auto !important;
            }

            /* Big Checkbox */
            button.RadCheckBox {
                font-size: 20px;
            }

            /* makes the checkbox icon elastic in addition to the label text */
            .RadButton.RadCheckBox .rbIcon,
            .RadButton.RadCheckBox .rbIcon::before {
                font-size: inherit;
                width: 1em;
                height: 1em;
            }

            #druginteraction {
                border-collapse: collapse;
                width: 100%;
            }

                #druginteraction td, #druginteraction th {
                    border: 1px solid #ddd;
                    padding: 2px;
                }

                #druginteraction tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

                #druginteraction tr:hover {
                    background-color: #ddd;
                }

                #druginteraction th {
                    padding-top: 4px;
                    padding-bottom: 4px;
                    text-align: left;
                    background-color: grey;
                    color: white;
                }
        </style>
        <script type="text/javascript">
            var _stockStyleDisplay = '<%= AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionShowStock)?"block":"none" %>';
            function cboItemID_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();

                var cboServ = $find("<%=cboServiceUnitID.ClientID %>");
                context["ServiceUnitID"] = cboServ.get_selectedItem().get_value();

                var cboLoc = $find("<%=cboLocationID.ClientID %>");
                context["LocationID"] = cboLoc.get_selectedItem().get_value();

                context["IsOnlyInStock"] = <%= AppSession.Parameter.IsPrescriptionOnlyInStock.ToString().ToLower() %>;
                context["GuarantorID"] = "<%=GuarantorID%>";
                context["UserType"] = "<%=AppSession.UserLogin.SRUserType%>";

                context["IsRasproEnable"] = <%= (AppSession.Parameter.IsRasproEnable && RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient).ToString().ToLower() %>;

                context["AbLevel"] = document.getElementById("<%=hdnAbLevel.ClientID%>").value;
                context["AbRestrictionID"] = document.getElementById("<%=hdnAbRestrictionID.ClientID%>").value;

                if (document.getElementById("<%=hdnRasproIsNew.ClientID%>").value != "1")
                    context["RasproSeqNo"] = document.getElementById("<%=hdnRasproSeqNo4Filter.ClientID%>").value;
                else
                    context["RasproSeqNo"] = "0";

                context["RasproType"] = document.getElementById("<%=hdnRasproType.ClientID%>").value;
                context["RegistrationNo"] = "<%=RegistrationNo%>";
                context["IsFornas"] = <%=IsFornas.ToString().ToLower()%>;
                context["IsBalTot"] = true;
                context["IsUdd"] = false;
                context["IsFormPrescOrderForm"] = true;
                context["ArLine"] = "<%= AntibioticRestrictionForLine %>";

            }
            function cboMedicationConsume_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();
                context["RefID"] = "MedicationConsume";
            }
            function cboItemID_ClientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                __doPostBack(sender._uniqueId, "")
            }

            function cboConsumeUnit_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();
                context["ItemID"] = $telerik.findControl(sender.get_parent().get_element(), "cboItemID").get_value();
            }

            function showDropDown(sender, eventArgs) {
                sender.showDropDown();
                sender.requestItems("[showall]", false);
            }
            function showDropDownNoKeypress(sender, eventArgs) {
                sender.showDropDown();
            }
            function openVitalSignChart(vitalSignID) {
                var url = '../../Common/VitalSignChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&vid=' + vitalSignID;

                openWindow(url, "Vital Sign Chart");
            }

            function openWindow(url, title) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title(title);
                oWnd.show();
                oWnd.maximize();
                //oWnd.setSize(1040, 750);
                //oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }

            function showCopyPrescription(no, isFromTemplate) {
                var cboLoc = $find("<%=cboLocationID.ClientID %>");
                var locID = cboLoc.get_selectedItem().get_value();

                if (isFromTemplate === true)
                    openWindow("Template/PrescriptionCopyDialog.aspx?tno=" + no + "&locID=" + locID + "&regno=<%= RegistrationNo %>&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "Copy Prescription from Template");
                else {
                    openWindow("Template/PrescriptionCopyDialog.aspx?prescno=" + no + "&locID=" + locID + "&regno=<%= RegistrationNo %>&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "Copy Prescription from Transaction");
                }
            }
            function showCopyPrescription2(no, type) {
                var cboLoc = $find("<%=cboLocationID.ClientID %>");
                var locID = cboLoc.get_selectedItem().get_value();

                switch (type) {
                    case "TP":
                        openWindow("Template/PrescriptionCopyDialog.aspx?tno=" + no + "&locID=" + locID + "&regno=<%= RegistrationNo %>&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "Copy Prescription from Template");
                        break;
                    case "TR":
                        openWindow("Template/PrescriptionCopyDialog.aspx?prescno=" + no + "&locID=" + locID + "&regno=<%= RegistrationNo %>&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "Copy Prescription from Transaction");
                        break;
                    case "BO":
                        openWindow("Template/PrescriptionCopyDialog.aspx?locID=" + locID + "&regno=<%= RegistrationNo %>&fregno=<%= string.Empty %>&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "Medication Will Out Of Balance");
                        break;

                }
            }

            function newRasal() {
                showRaspro("RASAL", "0", "new");
            }
            function newRaslan() {
                showRaspro("RASLAN", "0", "new");
            }
            function newRaspraja() {
                showRaspro("RASPRAJA", "0", "new");
            }
            function newRaspatur() {
                showRaspro("RASPATUR", "0", "new");
            }
            function newProphylaxis() {
                showRaspro("PPLAXIS", "0", "new");
            }
            function showRaspro(rasprotype, seqno, mod) {
                switch (rasprotype) {
                    case "RASAL":
                    case "RASLAN":
                        openWinRaspro("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproForm.aspx?mod=" + mod +"&rsno=<%=hdnRasproSeqNo.Value%>&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=" + seqno + "&raspro=" + rasprotype +"&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "");
                        break;
                    case "RASPRAJA":
                        openWinRaspro("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasprajaEntry.aspx?mod=" + mod +"&rsno=<%=hdnRasproSeqNo.Value%>&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=" + seqno +"&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "");
                        break;
                    case "RASPATUR":
                        openWinRaspro("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RaspaturEntry.aspx?mod=" + mod + "&rsno=<%=hdnRasproSeqNo.Value%>&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=" + seqno + "&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "");
                        break;
                    case "PPLAXIS":
                        openWinRaspro("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/ProphylaxisEntry.aspx?mod=" + mod + "rsno=<%=hdnRasproSeqNo.Value%>&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=" + seqno + "&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "");
                        break;
                    default:
                        break;
                }
            }

            function openSaveAsNewTemplate(prescno) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("Template/PrescriptionTemplateNew.aspx?prescno=" + prescno + "&ccm=rebind&cet=<%=grdPrescriptionTemplate.ClientID %>");
                oWnd.set_title("Save As New Template");
                oWnd.show();
                oWnd.setSize(500, 150);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                return false;
            }

            function radWindow_ClientClose(oWnd, args) {
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {

                        if (arg.callbackMethod === 'rebind') {
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }
                        }
                    }
                }

            }

            function cboEmbalace_ClientSelectedIndexChanged(sender, args) {
                var val = args.get_item().get_value();
                var combo = $find("<%= grdTransPrescriptionItem.ClientID %>_ctl00_ctl02_ctl02_EditFormControl_cboConsumeUnit");
                var itm = combo.findItemByValue(val);
                itm.select();
            }

            function cboConsumeMethod_ClientSelectedIndexChanged(sender, args) {
                var val = args.get_item().get_text();
                if (val.toLowerCase().includes("diketahui")) {
                    var txtConsumeQty = $find("<%= grdTransPrescriptionItem.ClientID %>_ctl00_ctl02_ctl02_EditFormControl_txtConsumeQty");
                    txtConsumeQty.set_value(1);
                }
            }

            function ResetLineAmount(sender, args) {
                var edtForm = sender.get_parent().get_element();
                var itemID = $telerik.findControl(edtForm, "cboItemID").get_value();
                if (itemID === "") return;
                var compound = $telerik.findControl(edtForm, "chkIsCompound").get_checked();
                var parentNo = $telerik.findControl(edtForm, "cboParentNo").get_value();
                var formulaQty = $telerik.findControl(edtForm, "txtDosage").get_value();
                var formulaUnit = $telerik.findControl(edtForm, "cboDosageUnit").get_value();
                var itemQty = $telerik.findControl(edtForm, "txtQty").get_value();
                var itemUnit = $telerik.findControl(edtForm, "txtItemUnit").get_value();
                var embalaceID = $telerik.findControl(edtForm, "cboEmbalace").get_value();

                var obj = {};
                obj.registrationNo = "<%= RegistrationNo %>";
                obj.itemID = itemID;
                obj.compound = compound;
                obj.parentNo = parentNo;
                obj.formulaQty = formulaQty;
                obj.formulaUnit = formulaUnit;
                obj.itemQty = itemQty;
                obj.itemUnit = itemUnit;
                obj.embalaceID = embalaceID;

                $.ajax({
                    url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Medication/PrescriptionWebService.asmx/LineAmount',
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        var txtLineAmt = $telerik.findControl(edtForm, "txtLineAmount");
                        txtLineAmt.set_value(decodeURI(data.d));
                    },
                    error: function (x, y, z) {
                        //alert(x.responseText + "  " + x.status);
                    }
                });
            }

            function ApplyIsCompound(sender, args) {
                var tbl = document.getElementById('tblItemEntry');
                var isShow = args.get_checked();

                show_hide_column(tbl, 1, isShow);
            }

            function show_hide_column(tbl, colNo, isShow) {
                var rows = tbl.getElementsByTagName('tr');

                for (var row = 0; row < rows.length; row++) {
                    var cols = rows[row].children;
                    if (colNo >= 0 && colNo < cols.length) {
                        var cell = cols[colNo];
                        if (cell.tagName == 'TD') cell.style.display = isShow ? 'block' : 'none';
                    }
                }
            }
            function openMedicationHist() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%= hdnFromRegistrationNo.Value %>';
                openWindow(url, "Medication Consumption History");
            }

            function openWinRaspro(url, title) {
                var oWnd = $find("<%= winRaspro.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title(title);
                oWnd.setActive();
                oWnd.show();
                oWnd.maximize();

                //// Cek position
                //var pos = oWnd.getWindowBounds();
                //if (pos.y < 0)
                //    oWnd.moveTo(pos.x, 0);
            }

            function winRaspro_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    // Postback untuk merefresh Info Raspro 
                    var urlEdit = "<%= Helper.UrlRoot2() %>/Module/RADT/Cpoe/EmrCommon/Medication/PrescriptionEntry.aspx?mod=<%=Request.QueryString["mod"] %>&patid=<%=Request.QueryString["patid"] %>&regno=<%= Request.QueryString["regno"] %>&presno=<%= Request.QueryString["presno"] %>&parid=<%= Request.QueryString["parid"] %>";
                    //alert(urlEdit);
                    document.location.href = urlEdit;
                }
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindow ID="winDialog" Width="1000px" Height="600px" runat="server" Modal="True" ShowContentDuringLoad="False"
        Behaviors="Close,Move" VisibleStatusbar="False" OnClientClose="radWindow_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRaspro" runat="server" Modal="True" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Behaviors="None" OnClientClose="winRaspro_ClientClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnFromRegistrationNo" />
    <asp:HiddenField runat="server" ID="hdnRegistrationType" />
    <asp:HiddenField runat="server" ID="hdnRasproSeqNo" />
    <asp:HiddenField runat="server" ID="hdnRasproSeqNo4Filter" />
    <asp:HiddenField runat="server" ID="hdnRasproRefNo" />
    <asp:HiddenField runat="server" ID="hdnRasproType" />
    <asp:HiddenField runat="server" ID="hdnRasproIsFirstPrescriptionNo" />
    <asp:HiddenField runat="server" ID="hdnAbLevel" />
    <asp:HiddenField runat="server" ID="hdnAbRestrictionID" />
    <asp:HiddenField runat="server" ID="hdnRasproIsNew" />
    <asp:HiddenField runat="server" ID="hdnIsRasproEnableApplied" />

    <cc:CollapsePanel ID="cpnAllergies" runat="server" Width="100%" Title="Allergies">
        <asp:Literal runat="server" ID="litPatientAllergy"></asp:Literal>

        <div style="height: 10px;">
        </div>
    </cc:CollapsePanel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Width="100%" Title="Vital Sign">
        <telerik:RadGrid ID="grdVitalSign" runat="server" OnNeedDataSource="grdVitalSign_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None">
            <MasterTableView DataKeyNames="VitalSignID" ShowHeader="False">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Question">
                        <ItemTemplate>
                            <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                <%#DataBinder.Eval(Container.DataItem, "VitalSignName")%>
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Answer" HeaderStyle-Width="80px">
                        <ItemTemplate>
                            <span>
                                <%# string.Format("<div style='background-color: {0};width:100%;padding-left: 2px'>{1}</div>",DataBinder.Eval(Container.DataItem, "EwsLevelColor"),DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted"))%>
                            </span>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="72px"></telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="43px"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="">
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemTemplate>
                            <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                <img src='../../../../../Images/Toolbar/barchart.bmp' alt='chart' /></a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText=""></telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="false">
                <Selecting AllowRowSelect="false" />
                <Resizing AllowColumnResize="True" />
            </ClientSettings>
        </telerik:RadGrid>
        <div style="height: 10px">
        </div>
    </cc:CollapsePanel>
    <cc:CollapsePanel ID="clpAntibioticSuggest" runat="server" Width="100%" Title="Antibiotic Suggestion">
        <asp:Literal runat="server" ID="litRasproInfo"></asp:Literal>
        <asp:Literal runat="server" ID="litAntibioticSuggest"></asp:Literal>
        <telerik:RadGrid ID="grdLaboratoryCultureResult" runat="server" AutoGenerateColumns="False" GridLines="None">
            <MasterTableView DataKeyNames="OrderLabNo" GroupLoadMode="Client">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="TestGroup" HeaderText="Group" />
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="TestGroup" SortOrder="None" />
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <telerik:GridTemplateColumn DataField="LabOrderSummary" UniqueName="LabOrderSummary"
                        HeaderText="Exam Name" HeaderStyle-Width="250px">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn DataField="ResultDatetime" UniqueName="ResultDatetime" HeaderText="Result Date" HeaderStyle-Width="120px" />
                    <telerik:GridBoundColumn DataField="Flag" UniqueName="Flag" HeaderText="Flag" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Result")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                        HeaderStyle-Width="150px" />
                    <telerik:GridBoundColumn DataField="ResultComment" UniqueName="ResultComment" HeaderText="Result Comment" />
                    <telerik:GridBoundColumn DataField="LabOrderCode" UniqueName="LabOrderCode" HeaderText="Code" Display="False" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>

        <br />
        <div style="height: 10px">
        </div>
    </cc:CollapsePanel>
    <cc:CollapsePanel ID="cplTemplate" runat="server" Width="100%" Title="Prescription Template">
        <telerik:RadGrid ID="grdPrescriptionTemplate" runat="server" CssClass="AutoHeightGridClass" EnableViewState="False"
            OnNeedDataSource="grdPrescriptionTemplate_NeedDataSource" OnDeleteCommand="grdPrescriptionTemplate_DeleteCommand"
            AutoGenerateColumns="False" GridLines="None" Skin="">
            <MasterTableView DataKeyNames="TemplateNo" ShowHeader="False">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateNo" HeaderText="">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "PrescriptionTemplate")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                        ButtonType="ImageButton" ConfirmText="Delete this prescription template?">
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling AllowScroll="False" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>
        <div style="height: 10px">
        </div>
    </cc:CollapsePanel>

    <cc:CollapsePanel ID="cplLastPrescription" runat="server" Width="100%" Title="20 Last Prescription Transaction">
        <telerik:RadGrid ID="grdPrescriptionHist" runat="server" CssClass="AutoHeightGridClass" EnableViewState="False"
            OnNeedDataSource="grdPrescriptionHist_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None">
            <MasterTableView DataKeyNames="RegistrationNo" ShowHeader="False">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Prescription" HeaderText="Last Prescription (max 20)">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>Reg No</td>
                                    <td>:</td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%> [<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(AppConstant.DisplayFormat.Date)%>]</td>
                                </tr>
                                <tr>
                                    <td>Physician</td>
                                    <td>:</td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "ParamedicName")%></td>
                                </tr>
                                <tr>
                                    <td valign="top">Diagnosis</td>
                                    <td valign="top">:</td>
                                    <td valign="top"><%#DataBinder.Eval(Container.DataItem, "Diagnosis")%></td>
                                </tr>
                                <tr>
                                    <td valign="top">ICD-10</td>
                                    <td valign="top">:</td>
                                    <td valign="top"><%#DataBinder.Eval(Container.DataItem, "ICD10")%></td>
                                </tr>
                            </table>
                            <%#DataBinder.Eval(Container.DataItem, "Prescription")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling AllowScroll="False" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </cc:CollapsePanel>
</asp:Content>

<asp:Content runat="server" ID="entry" ContentPlaceHolderID="cphEntry">
    <table width="100%">
        <tr>
            <td style="width: 70%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td style="width: 100px;" rowspan="4">
                            <asp:Image runat="server" ID="imgPatientPhoto" Width="100px" Height="100px" /></td>
                        <td class="label" style="width: 90px;">No / Date
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="140px" ReadOnly="True" />&nbsp;/&nbsp;
                            <telerik:RadDateTimePicker ID="txtPrescriptionDate" runat="server" Width="120px" ReadOnly="True">
                                <DatePopupButton Visible="False"></DatePopupButton>
                                <TimePopupButton Visible="False"></TimePopupButton>
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 90px;">Type</td>
                        <td>
                            <telerik:RadComboBox ID="cboSRPrescriptionCategory" runat="server" Width="280px" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <%--<tr>
                        <td class="label" style="width: 90px;">Dispensary
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="280px" AutoPostBack="True" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Dispensary required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="label" style="width: 90px;">Dispensary
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="280px" AutoPostBack="True" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" />
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Dispensary required."
                                            ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <%--<td style="width: 5px"></td>--%>
                                    <td style="width: 40px"></td>
                                    <td>
                                        <b><i>Age</i></b>&nbsp;
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtAgeInYear" runat="server" Width="150px" Font-Size="14px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 90px;">Location
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboLocationID" runat="server" Width="280px" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsCitoPresc" runat="server" Text="Cito" Font-Italic="True" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsUnitDosePresc" runat="server" Text="Unit Dose" Font-Italic="True" />
                                    </td>
                                    <td style="width: 40px"></td>
                                    <td>
                                        <b><i>Number Of R/</i></b>
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtQtyR" runat="server" Width="50px" MinValue="0" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLocationID" runat="server" ErrorMessage="Location required."
                                ValidationGroup="entry" ControlToValidate="cboLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%" runat="server" id="tblTemporaryBill">
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="lblTemporaryBill" runat="server" Text="ESTIMATED TOTAL TEMPORARY BILL" Font-Bold="true"></asp:Label></legend>
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td class="label" style="background-color: lightseagreen;width:50%">
                                                        <asp:Label ID="lblTemporaryBillPlafond" runat="server" Text="Plafond" ForeColor="WhiteSmoke" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadNumericTextBox ID="txtTemporaryBillPlafond" runat="server" Width="120px" ReadOnly="true" Font-Bold="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" style="background-color: lightseagreen">
                                                        <asp:Label ID="lblTemporaryBillTotal" runat="server" Text="Total Temporary Bill" ForeColor="WhiteSmoke" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadNumericTextBox ID="txtTemporaryBillTotal" runat="server" Width="120px" ReadOnly="true" Font-Bold="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
    <table width="100%">
        <tr>
            <td class="label" style="width: 90px;">Notes
            </td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtNotesPresc" runat="server" Width="99.5%" TextMode="MultiLine" Resize="Vertical" MaxLength="1000" />

            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label runat="server" ID="lblZatActiveInteraction"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:Panel runat="server" ID="pnlRaspro">
        <fieldset>
            <legend>PPRA Form (Antibiotic Guidance and Form)</legend>
            <table>
                <tr>
                    <td>
                        <telerik:RadLinkButton runat="server" ID="lnkNewRasal" Text="RASAL" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Raspro Alur Antibiotik Awal" OnClientClicked="newRasal"></telerik:RadLinkButton>
                    </td>
                    <td>
                        <telerik:RadLinkButton runat="server" ID="lnkNewRaslan" Text="RASLAN" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Raspro Alur Antibiotik Lanjutan" OnClientClicked="newRaslan"></telerik:RadLinkButton>
                    </td>
                    <td>
                        <telerik:RadLinkButton runat="server" ID="lnkNewRaspraja" Text="RASPRAJA" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Raspro Antibiotik Berkepanjangan" OnClientClicked="newRaspraja"></telerik:RadLinkButton>
                    </td>
                    <td>
                        <telerik:RadLinkButton runat="server" ID="lnkNewRaspatur" Text="RASPATUR" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Antibiotik Sesuai Kultur" OnClientClicked="newRaspatur"></telerik:RadLinkButton>

                    </td>
                    <td>
                        <telerik:RadLinkButton runat="server" ID="lnkNewProphylaxis" Text="PROFILAKSIS" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Antibiotik Profilaksis" OnClientClicked="newProphylaxis"></telerik:RadLinkButton>

                    </td>
                    <td>
                        <telerik:RadLinkButton Visible="false" runat="server" ID="lnkPrintRaspro" Text="Print" Icon-Url="~/Images/Toolbar/print16.png" ToolTip="Print Raspro Form" Enabled="false"></telerik:RadLinkButton>

                    </td>
                    <td></td>
                </tr>
            </table>

        </fieldset>
    </asp:Panel>

    <telerik:RadGrid ID="grdTransPrescriptionItem" Width="98%" runat="server" OnNeedDataSource="grdTransPrescriptionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransPrescriptionItem_UpdateCommand"
        OnDeleteCommand="grdTransPrescriptionItem_DeleteCommand" OnInsertCommand="grdTransPrescriptionItem_InsertCommand" OnItemCommand="grdTransPrescriptionItem_ItemCommand"
        ShowHeader="false" AllowMultiRowEdit="false">
        <MasterTableView DataKeyNames="SequenceNo" CommandItemDisplay="Top">
            <CommandItemTemplate>
                <table width="100%">
                    <tr>
                        <td align="left">&nbsp;
                            <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdTransPrescriptionItem.MasterTableView.IsItemInserted %>'>
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                                &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                            </asp:LinkButton></td>
                        <td><a style="display: none" href='javascript:void(0);' onclick="javascript:showCopyPrescription2('','BO')" />
                            <img src='../../../../../Images/Toolbar/ordering16.png' alt='Copy' />&nbsp;Add from Medication will out of balance</a></td>

                        <td align="right"><%# string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) ? string.Empty:"<a href='javascript:void(0);' onclick=\"javascript:openSaveAsNewTemplate('',false)\"><img src=\"../../../../../Images/Toolbar/copy16.png\" alt='Copy' />&nbsp;Save As New Template</a>&nbsp;&nbsp;" %></td>
                    </tr>
                </table>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" Visible="true">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                    HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Parent No" UniqueName="ParentNo"
                    HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsRFlag" HeaderText="R/"
                    UniqueName="IsRFlag" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    Visible="False" />
                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                        <br />
                        <i>Intervention :</i>
                        <asp:Label ID="lblInterventionItemName" runat="server" Font-Italic="true" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemInterventionName")) %>' />
                        <br />
                        <i><span style="color: orangered"><%# DataBinder.Eval(Container.DataItem, "FornasRestrictionNotes")%></span></i>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateResultQty" HeaderStyle-Width="50px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblResultQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemQtyInString") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateSRItemUnit" HeaderStyle-Width="100px"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? 
                                string.Format("{0} @{1} {2}",DataBinder.Eval(Container.DataItem, "EmbalaceLabel"),DataBinder.Eval(Container.DataItem, "DosageQty"),DataBinder.Eval(Container.DataItem, "SRDosageUnit")) : DataBinder.Eval(Container.DataItem, "SRItemUnit") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="SRConsumeMethodName" HeaderStyle-Width="150px"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) && DataBinder.Eval(Container.DataItem, "ParentNo") != null && !string.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "ParentNo").ToString())
                        ? string.Empty : string.Format("{0} @{1} {2}", DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),  DataBinder.Eval(Container.DataItem, "ConsumeQty"),   DataBinder.Eval(Container.DataItem, "SRConsumeUnit")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="PrescriptionItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="TransPrescriptionItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
