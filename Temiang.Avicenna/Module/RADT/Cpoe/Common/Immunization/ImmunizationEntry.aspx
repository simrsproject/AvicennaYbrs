<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ImmunizationEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ImmunizationEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../../JavaScript/DateFormat.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.rebind != null)
                    __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebind");
            }


            function openWinComp(transNo, seqNo, itemID) {
                var oWnd = $find("<%= winCharges.ClientID %>");
                var reg = $find("<%= txtRegistrationNo.ClientID %>");
                var unitF = $find("<%= cboFromServiceUnitID.ClientID %>");

                var unitT = $find("<%= cboResponUnit.ClientID %>");
                if (unitT == null)
                    unitT = $find("<%= cboToServiceUnitID.ClientID %>");

                var date = $find("<%= txtTransactionDate.ClientID %>");

                if (unitT != null)
                    oWnd.setUrl('TariffComponentPackage.aspx?reg=' + reg.get_value() + '&date=' + date.get_selectedDate().format("isoDate") + '&from=' + unitF.get_value() + '&to=' + unitT.get_value() + '&trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&type=' + '<%= Request.QueryString["type"] %>');
                else
                    oWnd.setUrl('TariffComponentPackage.aspx?reg=' + reg.get_value() + '&date=' + date.get_selectedDate().format("isoDate") + '&from=' + unitF.get_value() + '&to=&trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&type=' + '<%= Request.QueryString["type"] %>');

                oWnd.show();
                oWnd.maximize();
            }

            function openWinCons(transNo, seqNo, itemID) {
                var oWnd = $find("<%= winCharges.ClientID %>");

                var unit = $find("<%= cboResponUnit.ClientID %>");
                if (unit == null)
                    unit = $find("<%= cboToServiceUnitID.ClientID %>");
                if (unit == null)
                    unit = $find("<%= cboFromServiceUnitID.ClientID %>");

                oWnd.setUrl('ItemConsumptionPackage.aspx?trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + "&unit=" + unit.get_value());
                oWnd.show();
                oWnd.maximize();
            }

            function openWinExtra(transNo, seqNo, itemID) {
                var oWnd = $find("<%= winCharges.ClientID %>");
                var grr = $find("<%= cboGuarantorID.ClientID %>");

                oWnd.setUrl('ItemExtraPackage.aspx?trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&grr=' + grr.get_value());
                oWnd.show();
                oWnd.maximize();
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
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
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winCharges">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
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
                        <td>
                        </td>
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
                                    <td>
                                        &nbsp;
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
                        <td>
                        </td>
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
                                    <td>
                                        &nbsp;
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
                        <td>
                        </td>
                    </tr>
                    <tr id="pnlResponUnit" runat="server">
                        <td class="label">
                            <asp:Label ID="lblSRShift" runat="server" Text="Respon Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboResponUnit" runat="server" Width="304px" AutoPostBack="true"
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="304px" OnItemDataBound="cboFromServiceUnitID_ItemDataBound">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="pnlJobOrder" runat="server">
                        <td class="label">
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="Service Unit Order"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="304px" AutoPostBack="True"
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
                        <td>
                        </td>
                    </tr>
                    <tr id="pnlJobOrder2" runat="server">
                        <td class="label">
                            <asp:Label ID="lblTypeResult" runat="server" Text="Type Result"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTypeResult" runat="server" Width="304px" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboTypeResult_ItemDataBound"
                                OnItemsRequested="cboTypeResult_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" Height="43px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBarcode" runat="server" Text="Barcode Scan Entry"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px" AutoPostBack="True"
                                OnTextChanged="txtBarcodeEntry_OnTextChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr id="pnlSurgeryPackage" runat="server">
                        <td class="label">
                            <asp:Label ID="lblSurgeryPackageID" runat="server" Text="Surgery Package"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSurgeryPackageID" runat="server" Width="304px" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSurgeryPackageID_ItemDataBound"
                                OnItemsRequested="cboSurgeryPackageID_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="pnlServiceUnitBookingNo" runat="server" visible="False">
                        <td class="label">
                            <asp:Label ID="lblServiceUnitBookingNo" runat="server" Text="To Booking No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitBookingNo" runat="server" Width="304px" HighlightTemplatedItems="True"
                                MarkFirstMatch="False" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboServiceUnitBookingNo_ItemDataBound"
                                OnItemsRequested="cboServiceUnitBookingNo_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitBookingNo" runat="server" ErrorMessage="To Booking No required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitBookingNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="pnlKiaCaseType" runat="server" visible="False">
                        <td class="label">
                            <asp:Label ID="lblKiaCaseType" runat="server" Text="KIA Case Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRKiaCaseType" runat="server" Width="304px" HighlightTemplatedItems="True"
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
                        <td>
                        </td>
                    </tr>
                    <tr id="pnlObstetricType" runat="server" visible="False">
                        <td class="label">
                            <asp:Label ID="lblObstetricTyoe" runat="server" Text="Obstetric Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRObstetricType" runat="server" Width="304px" HighlightTemplatedItems="True"
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
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trPhysicianSenders" visible="False">
                        <td class="label">
                            <asp:Label ID="lblPhysicianSenders" runat="server" Text="Physician Senders"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysicianSenders" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPhysicianSenders" runat="server" ErrorMessage="Physician Senders required."
                                ValidationGroup="entry" ControlToValidate="txtPhysicianSenders" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="197px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="275px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
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
                        <td width="20">
                        </td>
                        <td>
                        </td>
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
                                    <td>
                                        &nbsp;
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
                        <td>
                        </td>
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
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClassName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboBedID" runat="server" Width="304px" AutoPostBack="True"
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
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="304px" EnableLoadOnDemand="True"
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="304px" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                            Enabled="False">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp; <a href="javascript:void(0);" onclick="javascript:openWinGuarantorInfo();"
                                            class="noti_Container">
                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblGuarantorInfo" AssociatedControlID="cboGuarantorID"
                                                Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlLengthOfStay">
                        <td class="label">
                            <asp:Label ID="lblLengthOfStay" runat="server" Text="Length Of Stay"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="50px" ReadOnly="True">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Day(s)
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr height="24px" id="pnlJobOrder3" runat="server">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsProceed" runat="server" Text="Proceed" Enabled="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
        ShowFooter="true" AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransChargesItem_UpdateCommand"
        OnDeleteCommand="grdTransChargesItem_DeleteCommand" OnInsertCommand="grdTransChargesItem_InsertCommand"
        OnItemCreated="grdTransChargesItem_ItemCreated" OnItemCommand="grdTransChargesItem_ItemCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="TransactionNo, SequenceNo"
            FilterExpression="ParentNo = ''">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdTransChargesItem.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>
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
                                string.Format("<a href=\"#\" onclick=\"openWinComp('{0}','{1}','{2}'); return false;\"><img src=\"../../../../../Images/Toolbar/dokter.png\" border=\"0\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="cons" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsPackage").Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(true)? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinCons('{0}','{1}','{2}'); return false;\"><img src=\"../../../../../Images/Toolbar/consumption.png\" border=\"0\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ParentNo" UniqueName="ParentNo" SortExpression="ParentNo"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Physician"
                    UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                    UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="DiscountAmount" HeaderText="Disc"
                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="CitoAmount" HeaderText="Cito"
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
            <EditFormSettings UserControlName="ImmunizationDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ImmunizationEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
