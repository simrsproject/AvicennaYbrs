<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CasemixExceptionItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.CasemixExceptionItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table>
    <tr>
        <td class="label">Item Name
        </td>
        <td class="entry">
            <telerik:RadTextBox runat="server" ID="txtItemID" Width="100px" AutoPostBack="true"
                OnTextChanged="txtItemID_TextChanged" />
            <asp:Label runat="server" ID="lblItemName" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvBridgingType" runat="server" ErrorMessage="Item Name required."
                ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label"></td>
        <td class="entry">
            <asp:CheckBox ID="chkIsUsingGlobalSetting" Text="Using Global Setting" runat="server" />
        </td>
        <td width="20px"></td>
        <td />
    </tr>
    <tr>
        <td class="label"></td>
        <td colspan="3">
            <table style="width: 100%">
                <tr>
                    <td class="label" style="text-align: center" colspan="2">Global
                    </td>
                    <td style="width: 2px"></td>
                    <td class="label" style="text-align: center" colspan="2">Inpatient
                    </td>
                    <td style="width: 2px"></td>
                    <td class="label" style="text-align: center" colspan="2">Outpatient
                    </td>
                    <td style="width: 2px"></td>
                    <td class="label" style="text-align: center" colspan="2">Emergency
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidate" Text="Need Casemix Validate" />
                    </td>
                    <td style="width: 2px"></td>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidateIpr" Text="Need Casemix Validate" />
                    </td>
                    <td style="width: 2px"></td>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidateOpr" Text="Need Casemix Validate" />
                    </td>
                    <td style="width: 2px"></td>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidateEmr" Text="Need Casemix Validate" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px"></td>
        <td />
    </tr>
    <tr>
        <td class="label">Quantity
        </td>
        <td colspan="3">
            <table style="width: 100%">
                <tr>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtQty" Width="100px" />
                        
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Qty - Global required."
                            ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtQtyIpr" Width="100px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvQtyIpr" runat="server" ErrorMessage="Qty - Inpatient required."
                            ControlToValidate="txtQtyIpr" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtQtyOpr" Width="100px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvQtyOpr" runat="server" ErrorMessage="Qty - Outpatient required."
                            ControlToValidate="txtQtyOpr" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtQtyEmr" Width="100px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvQtyEmr" runat="server" ErrorMessage="Qty - Emergency required."
                            ControlToValidate="txtQtyEmr" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td colspan="3">
            <asp:Label runat="server" ID="lblCaption" Text="* Need Casemix Validate : checked if validation is required without considering the quantity limit" ForeColor="Blue"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td colspan="3">
            <asp:Label runat="server" ID="lblCaption2" Text="** Quantity : fill it with 0 (zero) if there is no quantity limit" ForeColor="Blue"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
