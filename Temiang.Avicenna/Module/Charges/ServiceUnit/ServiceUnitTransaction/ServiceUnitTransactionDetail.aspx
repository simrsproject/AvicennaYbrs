<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitTransactionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitTransactionDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script src="<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/JavaScript/DateFormat.js"></script>
        <script type="text/javascript" language="javascript">
            var _wcWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth) %>;
            var _wcHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight) %>;

            function onWinWebCamClose(sender, eventArgs) {
                // Get retval
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("imgFromWebCam");
                    img.setAttribute('src', arg);
                    var hdnImgData = document.getElementById("hdnImgFromWebCam");
                    hdnImgData.value = arg;
                }
            }

            function openWinWebCam() {
                var oWnd = $find("<%= winWebCam.ClientID %>");
                oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PatientDocument/WebCamJCrop.aspx");
                oWnd.setSize(_wcWidth + 40, _wcHeight + 80);
                oWnd.center();
                oWnd.show();
            }
        </script>
        <script type="text/javascript" language="javascript">
            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.rebind != null)
                    __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebind");
            }

            function openWinPickList() {
                var tr = '<%= Request.QueryString["type"] %>';
                if (tr == 'jo' || tr == 'ds') {
                    var cboJO = $find("<%= cboToServiceUnitID.ClientID %>");
                    if (cboJO != null) {
                        if (cboJO.get_visible()) {
                            if (cboJO.get_value() == '') {
                                alert('Service Unit Order is required.');
                                return;
                            }
                        }
                    }
                }
                else {
                    var cboLoc = $find("<%= cboLocationID.ClientID %>");
                    if (cboLoc != null) {
                        if (cboLoc.get_visible()) {
                            if (cboLoc.get_value() == '') {
                                alert('Location is required.');
                                return;
                            }
                        }
                    }
                }

                var trans = $find("<%= txtTransactionNo.ClientID %>");

                var reg = $find("<%= txtRegistrationNo.ClientID %>");

                var unit = $find("<%= cboResponUnit.ClientID %>");
                if (unit == null)
                    unit = $find("<%= cboToServiceUnitID.ClientID %>");
                if (unit == null)
                    unit = $find("<%= cboFromServiceUnitID.ClientID %>");
                var loc = $find("<%= cboLocationID.ClientID %>");
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;

                if (unit.get_value() != '') {
                    var oWnd = $find("<%= winCharges.ClientID %>");

                    if (tr == 'jo' || tr == 'ds') {
                        var FUnit = $find("<%= cboFromServiceUnitID.ClientID %>").get_value();
                        var TUnit = $find("<%= cboToServiceUnitID.ClientID %>").get_value();
                        //alert(FUnit);alert(TUnit);

                        oWnd.setUrl('ItemPickerList.aspx?transno=' + trans.get_value() + '&unit=' + unit.get_value() + '&reg=' + reg.get_value() +
                            '&type=' + '<%= Request.QueryString["type"] %>' + '&FUnit=' + FUnit + '&TUnit=' + TUnit + '&emr=' + '<%= Request.QueryString["emr"] %>' + '&pageId=' + pageId + '&casemix=' + '<%= Request.QueryString["casemix"] %>');
                        oWnd.set_title('Item List');
                    }
                    else if (loc.get_value() != '') {
                        oWnd.setUrl('ItemPickerListItemProduct.aspx?transno=' + trans.get_value() + '&loc=' + loc.get_value() + '&reg=' + reg.get_value() + '&type=' + '<%= Request.QueryString["type"] %>' + '&emr=' + '<%= Request.QueryString["emr"] %>' + '&pageId=' + pageId);
                        oWnd.set_title('Item Product List');
                    }

                    oWnd.show();
                    //oWnd.maximize();
                    oWnd.add_pageLoad(onClientPageLoad);
                }
            }

            function openWinComp(transNo, seqNo, itemID) {
                var oWnd = $find("<%= winCharges.ClientID %>");
                var reg = $find("<%= txtRegistrationNo.ClientID %>");
                var trans = $find("<%= txtTransactionNo.ClientID %>");
                var unitF = $find("<%= cboFromServiceUnitID.ClientID %>");

                var unitT = $find("<%= cboResponUnit.ClientID %>");
                if (unitT == null)
                    unitT = $find("<%= cboToServiceUnitID.ClientID %>");

                var date = $find("<%= txtTransactionDate.ClientID %>");
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;

                if (unitT != null)
                    oWnd.setUrl('TariffComponentPackage.aspx?transno=' + trans.get_value() + '&reg=' + reg.get_value() + '&date=' + date.get_selectedDate().format("isoDate") + '&from=' + unitF.get_value() + '&to=' + unitT.get_value() + '&trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&type=' + '<%= Request.QueryString["type"] %>' + '&pageId=' + pageId);
                else
                    oWnd.setUrl('TariffComponentPackage.aspx?transno=' + trans.get_value() + '&reg=' + reg.get_value() + '&date=' + date.get_selectedDate().format("isoDate") + '&from=' + unitF.get_value() + '&to=&trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&type=' + '<%= Request.QueryString["type"] %>' + '&pageId=' + pageId);

                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinCons(transNo, seqNo, itemID) {
                var oWnd = $find("<%= winCharges.ClientID %>");

                var unit = $find("<%= cboResponUnit.ClientID %>");
                if (unit == null)
                    unit = $find("<%= cboToServiceUnitID.ClientID %>");
                if (unit == null)
                    unit = $find("<%= cboFromServiceUnitID.ClientID %>");

                var reg = $find("<%= txtRegistrationNo.ClientID %>");
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;

                oWnd.setUrl('ItemConsumptionPackage.aspx?trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + "&unit=" + unit.get_value() + '&reg=' + reg.get_value() + '&pageId=' + pageId);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinExtra(transNo, seqNo, itemID) {
                var oWnd = $find("<%= winCharges.ClientID %>");
                var grr = $find("<%= cboGuarantorID.ClientID %>");
                var reg = $find("<%= txtRegistrationNo.ClientID %>");
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;

                oWnd.setUrl('ItemExtraPackage.aspx?trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&grr=' + grr.get_value() + '&reg=' + reg.get_value() + '&pageId=' + pageId);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openWinQuestionFormCheckList() {
                var oWnd = $find("<%= winDocsOption.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo2.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/Registration/RegistrationDocumentCheckList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_title('Document Checklist');
                oWnd.show();
            }

            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinO2Calculation() {
                var tr = '<%= Request.QueryString["type"] %>';
                if (tr != 'tr') {
                    alert('O2 calculation for service unit transaction only.');
                    return;
                }

                var oWnd = $find("<%= winO2Calc.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("O2CalculationDialog.aspx")%>');
                oWnd.show();
            }

            function winO2Calc_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    var parentID = "<%= grdTransChargesItem.ClientID %>";
                    var txt = $find(parentID + "_ctl00_ctl02_ctl02_EditFormControl_txtChargeQuantity");
                    txt.set_value(arg.TotalUsage);
                }
            }

            function openWinFiO2Calculation() {
                var tr = '<%= Request.QueryString["type"] %>';
                if (tr != 'tr') {
                    alert('FiO2 calculation for service unit transaction only.');
                    return;
                }

                var oWnd = $find("<%= winO2Calc.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("FiO2CalculationDialog.aspx")%>');
                oWnd.show();
            }

            function txtBarcodeEntryKeyPress(sender, eventArgs) {
                var code = eventArgs.get_keyCode();
                if (code == 13) {
                    eventArgs.set_cancel(true); // Supaya tidak membuka edit grid
                    __doPostBack(sender._clientID.replace(/_/g, "$"), "addwithbarcode|" + sender.get_value());
                }
            }

            function openSaveAsNewTemplate(transNo) {
                var tr = '<%= Request.QueryString["type"] %>';
                if (tr == 'jo' || tr == 'ds') {
                    var cboJO = $find("<%= cboToServiceUnitID.ClientID %>");
                    if (cboJO != null) {
                        if (cboJO.get_visible()) {
                            if (cboJO.get_value() == '') {
                                alert('Service Unit Order is required.');
                                return;
                            }
                        }
                    }
                }

                var toUnit = $find("<%= cboToServiceUnitID.ClientID %>").get_value();
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;
                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl("Template/TemplateNew.aspx?transNo=" + transNo + "&ccm=rebind&pageId=" + pageId + "&toUnit=" + toUnit);
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

            function openWinPickListTemplate() {
                var tr = '<%= Request.QueryString["type"] %>';
                if (tr == 'jo' || tr == 'ds') {
                    var cboJO = $find("<%= cboToServiceUnitID.ClientID %>");
                    if (cboJO != null) {
                        if (cboJO.get_visible()) {
                            if (cboJO.get_value() == '') {
                                alert('Service Unit Order is required.');
                                return;
                            }
                        }
                    }
                }

                var trans = $find("<%= txtTransactionNo.ClientID %>");
                var reg = $find("<%= txtRegistrationNo.ClientID %>");
                var unit = $find("<%= cboToServiceUnitID.ClientID %>");
                var loc = $find("<%= cboLocationID.ClientID %>");
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;

                if (unit.get_value() != '') {
                    var oWnd = $find("<%= winCharges.ClientID %>");

                    if (tr == 'jo' || tr == 'ds') {
                        var FUnit = $find("<%= cboFromServiceUnitID.ClientID %>").get_value();
                        var TUnit = $find("<%= cboToServiceUnitID.ClientID %>").get_value();
                        //alert(FUnit);alert(TUnit);

                        oWnd.setUrl('ItemPickerListTemplate.aspx?transno=' + trans.get_value() + '&unit=' + unit.get_value() + '&reg=' + reg.get_value() +
                            '&type=' + '<%= Request.QueryString["type"] %>' + '&FUnit=' + FUnit + '&TUnit=' + TUnit + '&emr=' + '<%= Request.QueryString["emr"] %>' + '&pageId=' + pageId);
                        oWnd.set_title('Item Template List');
                    }

                    oWnd.show();
                    //oWnd.maximize();
                    oWnd.add_pageLoad(onClientPageLoad);
                }
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winO2Calc_ClientClose"
        ID="winO2Calc">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="500px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winCharges">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
        ID="winDocsOption">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winWebCam" Width="680px" Height="620px" runat="server" Modal="true"
        ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
        OnClientClose="onWinWebCamClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnPageId" />
    <asp:HiddenField runat="server" ID="hdnIsMandatoryBookingNo" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPackageReferenceNo" runat="server" Text="PackageReferenceNo"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPackageReferenceNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtTransactionTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblExecutionDateTime" runat="server" Text="Execution Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtExecutionDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtExecutionTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvExecutionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtExecutionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="pnlResponUnit" runat="server">
                        <td class="label">
                            <asp:Label ID="lblSRShift" runat="server" Text="Respon Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboResponUnit" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboResponUnit_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRShift" runat="server" ErrorMessage="Respon Unit required."
                                ValidationGroup="entry" ControlToValidate="cboResponUnit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" OnItemDataBound="cboFromServiceUnitID_ItemDataBound">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="pnlJobOrder" runat="server">
                        <td class="label">
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="Service Unit Order"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboToServiceUnitID_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="Service Unit Order required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboLocationID" runat="server" Width="300px" AllowCustomText="true"
                                AutoPostBack="true" OnSelectedIndexChanged="cboLocationID_SelectedIndexChanged"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvLocationID" runat="server" ErrorMessage="Location required."
                                ValidationGroup="entry" ControlToValidate="cboLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="TrDiagNo" runat="server">
                        <td class="label">
                            <asp:Label ID="lblDiagnosticNo" runat="server" Text="Radiology No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDiagnosticNo" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr id="pnlJobOrder2" runat="server">
                        <td class="label">
                            <asp:Label ID="lblTypeResult" runat="server" Text="Type Result"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTypeResult" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboTypeResult_ItemDataBound"
                                OnItemsRequested="cboTypeResult_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="TrBloodSampleTakenBy" runat="server">
                        <td class="label">
                            <asp:Label ID="lblBloodSampleTakenBy" runat="server" Text="Specimen Taken By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRBloodSampleTakenBy" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSRBloodSampleTakenBy_ItemDataBound"
                                OnItemsRequested="cboSRBloodSampleTakenBy_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ErrorMessage="Notes required."
                                ValidationGroup="entry" ControlToValidate="txtNotes" SetFocusOnError="True" Width="100%"
                                Enabled="false">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="pnlSurgeryPackage" runat="server">
                        <td class="label">
                            <asp:Label ID="lblSurgeryPackageID" runat="server" Text="Surgery Package"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSurgeryPackageID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSurgeryPackageID_ItemDataBound"
                                OnItemsRequested="cboSurgeryPackageID_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr id="pnlServiceUnitBookingNo" runat="server" visible="False">
                        <td class="label">
                            <asp:Label ID="lblServiceUnitBookingNo" runat="server" Text="To Booking No"></asp:Label>
                            <asp:Label ID="lblServiceUnitBookingNoMandatory" runat="server" Text="To Booking No *" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitBookingNo" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitBookingNo" runat="server" ErrorMessage="To Booking No required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitBookingNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="pnlKiaCaseType" runat="server" visible="False">
                        <td class="label">
                            <asp:Label ID="lblKiaCaseType" runat="server" Text="KIA Case Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRKiaCaseType" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="False" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSRKiaCaseType_ItemDataBound"
                                OnItemsRequested="cboSRKiaCaseType_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRKiaCaseType" runat="server" ErrorMessage="KIA Case Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRKiaCaseType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="pnlObstetricType" runat="server" visible="False">
                        <td class="label">
                            <asp:Label ID="lblObstetricTyoe" runat="server" Text="Obstetric Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRObstetricType" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="False" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSRObstetricType_ItemDataBound"
                                OnItemsRequested="cboSRObstetricType_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRObstetricType" runat="server" ErrorMessage="Obstetric Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRObstetricType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trPhysicianSenders" visible="False">
                        <td class="label">
                            <asp:Label ID="lblPhysicianSenders" runat="server" Text="Physician Senders"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPhysicianSender" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="False" EnableLoadOnDemand="true" OnItemsRequested="cboPhysicianSender_ItemsRequested"
                                OnItemDataBound="cboPhysicianSender_ItemDataBound">
                            </telerik:RadComboBox>
                            <telerik:RadTextBox ID="txtPhysicianSenders" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPhysicianSenders" runat="server" ErrorMessage="Physician Senders required."
                                ValidationGroup="entry" ControlToValidate="cboPhysicianSender" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvTxtPhysicianSenders" runat="server" ErrorMessage="Physician Senders required."
                                ValidationGroup="entry" ControlToValidate="txtPhysicianSenders" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trClinicalDiagnosis">
                        <td class="label">
                            <asp:Label ID="lblClinicalDiagnosis" runat="server" Text="Clinical Diagnosis"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClinicalDiagnosis" runat="server" Width="300px" TextMode="MultiLine" Height="40px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr runat="server" id="trPASUS" visible="False">
                        <td colspan="4" class="label">
                            <asp:Label ID="lblPASUS" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="Red" 
                                style="text-decoration:blink; width: 135px; height: 19px; font-weight:500;" 
                                Text="" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" runat="server" id="tblTemporaryBill">
                    <tr>
                        <td>
                            <fieldset>
                            <legend>
                                <asp:Label ID="lblTemporaryBill" runat="server" Text="ESTIMATED TOTAL TEMPORARY BILL" Font-Bold="true"></asp:Label></legend>
                                <table width="100%" border="0" >
                                    <tr>
                                        <td class="label" style="background-color:lightseagreen">
                                            <asp:Label ID="lblTemporaryBillPlafond" runat="server" Text="Plafond" ForeColor="WhiteSmoke" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtTemporaryBillPlafond" runat="server" Width="120px" ReadOnly="true" Font-Bold="true"/>
                                        </td>
                                        <td width="20" />
                                        <td />
                                    </tr>
                                    <tr>
                                    <td class="label" style="background-color:lightseagreen">
                                        <asp:Label ID="lblTemporaryBillTotal" runat="server" Text="Total Temporary Bill" ForeColor="WhiteSmoke" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtTemporaryBillTotal" runat="server" Width="120px" ReadOnly="true" Font-Bold="true" />
                                    </td>
                                    <td width="20" />
                                    <td />
                                </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDateTime" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td class="label" runat="server" id="tdLengthOfStay">
                                        <asp:Label ID="lblLengthOfStay" runat="server" Text="Length of Stay"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td class="entry2Column">
                                        <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="41px" ReadOnly="True">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                        &nbsp;<asp:Label ID="lblLengthOfStayDays" runat="server" Text="Day(s)"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="194px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td style="width: 6px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                                Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <a href="javascript:void(0);" onclick="javascript:openWinQuestionFormCheckList();"
                                            class="noti2_Container">&nbsp;
                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo2" AssociatedControlID="txtRegistrationNo"
                                                Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRoomName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <telerik:RadNumericTextBox ID="txtTariffDiscForRoomIn" runat="server" Width="100px"
                                ReadOnly="true" Visible="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Charge Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClassName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboBedID" runat="server" Width="300px" AutoPostBack="True"
                                            OnSelectedIndexChanged="cboBedID_SelectedIndexChanged" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkIsRoomIn" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 10 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                            Enabled="False">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td><a href="javascript:void(0);" onclick="javascript:openWinGuarantorInfo();"
                                        class="noti_Container">
                                        <asp:Label CssClass="noti_bubble" runat="server" ID="lblGuarantorInfo" AssociatedControlID="cboGuarantorID"
                                            Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trBpjsSepNo">
                        <td class="label">
                            <asp:Label ID="lblBpjsSepNo" runat="server" Text="BPJS SEP No"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadTextBox ID="txtBpjsSepNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr height="24px" id="pnlJobOrder3" runat="server">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsProceed" runat="server" Text="Proceed" Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlProdia" Visible="False">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRProdiaContractID" runat="server" Text="Prodia Contract ID"></asp:Label>
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboSRProdiaContractID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" Enabled="False">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvSRProdiaContractID" runat="server" ErrorMessage="Prodia Contract ID required."
                                    ValidationGroup="entry" ControlToValidate="cboSRProdiaContractID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLinkLis" Visible="False">
                        <tr>
                            <td class="label">Clinical Pathologist
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboPhysicianIDPathology" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Laboratory Analyst
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboAnalystID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                    </asp:Panel>
                </table>
            </td>
            <td style="vertical-align: top">
                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                    <legend>Photo</legend>
                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
        ShowFooter="true" AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransChargesItem_UpdateCommand"
        OnDeleteCommand="grdTransChargesItem_DeleteCommand" OnInsertCommand="grdTransChargesItem_InsertCommand"
        OnItemCreated="grdTransChargesItem_ItemCreated" OnItemCommand="grdTransChargesItem_ItemCommand" AllowMultiRowEdit="false" OnDataBound="grdTransChargesItem_DataBound">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
            FilterExpression="ParentNo = ''">
            <CommandItemTemplate>
                <table width="100%">
                    <tr>
                        <td align="left"">
                            &nbsp;&nbsp;&nbsp;Barcode Scan&nbsp;&nbsp;
                            <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px">
                                <ClientEvents OnKeyPress="txtBarcodeEntryKeyPress"></ClientEvents>
                            </telerik:RadTextBox>
                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbO2Calculation" runat="server" ToolTip="O2 Calculation" Visible='<%# grdTransChargesItem.MasterTableView.IsItemInserted %>'
                                OnClientClick="javascript:openWinO2Calculation();return false;">
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/O2.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbfiO2Calculation" runat="server" ToolTip="FiO2 Calculation" Visible='<%# grdTransChargesItem.MasterTableView.IsItemInserted %>'
                                OnClientClick="javascript:openWinFiO2Calculation();return false;">
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/FiO2.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdTransChargesItem.MasterTableView.IsItemInserted %>'>
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdTransChargesItem.MasterTableView.IsItemInserted %>'
                                OnClientClick="javascript:openWinPickList();return false;">
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                                &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Item picker"></asp:Label>
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lbPickFromTemplate" runat="server" Visible="false"
                                OnClientClick="javascript:openWinPickListTemplate();return false;">
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                                &nbsp;<asp:Label runat="server" ID="lblPickFromTemplate" Text="Pick from template"></asp:Label>
                            </asp:LinkButton>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lbSaveAsNewTemplate" runat="server" Visible="false"
                                OnClientClick="javascript:openSaveAsNewTemplate();return false;">
                                <img style="border: 0px; vertical-align: middle;" alt="Copy" src="../../../../Images/Toolbar/copy16.png" />
                                &nbsp;<asp:Label runat="server" ID="lblSaveAsNewTemplate" Text="Save As New Template"></asp:Label>
                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="extra" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsPackage").Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(true)? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinExtra('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/insert16.png\" border=\"0\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="comp" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsPackage").Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(true)? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinComp('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/dokter.png\" border=\"0\" title=\"Package Details\"/></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="cons" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsItemTypeService").Equals(true) && DataBinder.Eval(Container.DataItem, "IsApprove").Equals(false) && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(false) ? string.Format("<a href=\"#\" onclick=\"openWinCons('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/consumption.png\" border=\"0\" title=\"Item Consumption\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")) : string.Empty
                                )%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ParentNo" UniqueName="ParentNo" SortExpression="ParentNo"
                    Visible="false" />
                <telerik:GridTemplateColumn UniqueName="ItemName" HeaderText="Item Name" Groupable="false">
                    <ItemTemplate>
                        <i>Item Group :
                            <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%></i>
                        <br />
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "ItemConditionRuleName")%>
                        <br />
                        <span style="color: red"><%# DataBinder.Eval(Container.DataItem, "PrevOrder")%></span>&nbsp;
                        <span style="color: orange"><%# DataBinder.Eval(Container.DataItem, "Notes")%></span>&nbsp;
                        <i><span style="color: blue"><%# DataBinder.Eval(Container.DataItem, "CombinedNotes")%></span></i>&nbsp
                        <i><span style="color: green"><%# DataBinder.Eval(Container.DataItem, "SpecimenTypeName")%></span></i>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ParamedicCollectionName" HeaderText="Physician"
                    UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                    UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CitoAmount" HeaderText="Cito"
                    UniqueName="CitoAmount" SortExpression="CitoAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="ChargeQuantity,Price,DiscountAmount,CitoAmount"
                    SortExpression="Total" Expression="(({0} * {1}) - {2}) + {3}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Locked"
                    UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Updater" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ItemTransactionDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemTransactionDetailEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
