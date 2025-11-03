<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemExpiryDateDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.ItemExpiryDateDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:radajaxmanagerproxy id="RadAjaxManagerProxy1" runat="server">
    <ajaxsettings>
        <telerik:ajaxsetting ajaxcontrolid="txtReferenceNo">
            <updatedcontrols>
                <telerik:ajaxupdatedcontrol controlid="txtBatchNumber" />
                <telerik:ajaxupdatedcontrol controlid="txtExpiredDate" />
            </updatedcontrols>
        </telerik:ajaxsetting>
        <telerik:ajaxsetting ajaxcontrolid="txtBatchNumber">
            <updatedcontrols>
                <telerik:ajaxupdatedcontrol controlid="txtExpiredDate" />
            </updatedcontrols>
        </telerik:ajaxsetting>
        <telerik:ajaxsetting ajaxcontrolid="cboSRItemUnit">
            <updatedcontrols>
                <telerik:ajaxupdatedcontrol controlid="txtConversionFactor" />
                <telerik:ajaxupdatedcontrol controlid="txtQuantity" />
            </updatedcontrols>
        </telerik:ajaxsetting>
    </ajaxsettings>
</telerik:radajaxmanagerproxy>
<telerik:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function openWinItemEdPorList() {
            var oWnd = $find("<%= winPOR.ClientID %>");
            var oit = $find("<%= txtItemID.ClientID %>");
            oWnd.setUrl('ItemExpiryDatePorList.aspx?itemId=' + oit.get_value());
            oWnd.show();
        }
        function onClientClose(oWnd, args) {
            var txtRefNo = $find("<%= txtReferenceNo.ClientID %>");

            if (oWnd.argument)
                txtRefNo.set_value(oWnd.argument.tno);
        }
    </script>
</telerik:radcodeblock>
<telerik:radwindow id="winPOR" animation="None" width="900px" height="500px" runat="server"
    showcontentduringload="false" behavior="Close" visiblestatusbar="false" modal="true"
    onclientclose="onClientClose" />
<asp:ValidationSummary ID="vsumItemTransactionItemEd" runat="server" ValidationGroup="ItemTransactionItemEd" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItemEd"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <asp:Panel ID="pnlReferenceNo" runat="server">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text=">> Select Batch Number" ForeColor="Red" Font-Italic="true"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtItemID" runat="server" width="100px" readonly="true" visible="true" forecolor="Gray" />
                            <telerik:radtextbox id="txtReferenceNo" runat="server" width="200px" maxlength="30"
                                showbutton="true" autopostback="true" ontextchanged="txtReferenceNo_TextChanged"
                                clientevents-onbuttonclick="openWinItemEdPorList" readonly="true" forecolor="Gray" />
                        </td>
                        <td width="20px">
                        </td>
                        <td></td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBatchNumber" runat="server" Text="Batch Number"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:radtextbox id="txtBatchNumber" runat="server" width="300px" maxlength="50" autopostback="true" ontextchanged="txtBatchNumber_TextChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBatchNo" runat="server" ErrorMessage="Batch Number required."
                            ControlToValidate="txtBatchNumber" SetFocusOnError="True" ValidationGroup="ItemTransactionItemEd"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExpiredDate" runat="server" Text="Expired Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker id="txtExpiredDate" runat="server" width="100px" MinDate="01/01/1900" MaxDate="06/06/2079" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvExpiredDate" runat="server" ErrorMessage="Expired Date required."
                            ControlToValidate="txtExpiredDate" SetFocusOnError="True" ValidationGroup="ItemTransactionItemEd"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:radnumerictextbox id="txtQuantity" runat="server" width="100px" maxlength="10"
                                        maxvalue="99999999" minvalue="0" numberformat-decimaldigits="2" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>
                                    <telerik:radcombobox id="cboSRItemUnit" runat="server" width="100px" autopostback="True"
                                        onselectedindexchanged="cboSRItemUnit_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConversionFactor" runat="server" Text="Conversion Factor" />
                    </td>
                    <td class="entry">
                        <telerik:radnumerictextbox id="txtConversionFactor" runat="server" width="100px"
                            minvalue="1" readonly="True" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConversionFactor" runat="server" ErrorMessage="Conversion Factor required."
                            ControlToValidate="txtConversionFactor" SetFocusOnError="True" ValidationGroup="ItemTransactionItemEd"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItemEd"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemTransactionItemEd" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%" valign="top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
