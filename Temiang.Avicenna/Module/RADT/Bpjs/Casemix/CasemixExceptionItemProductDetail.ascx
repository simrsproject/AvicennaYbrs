<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CasemixExceptionItemProductDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.CasemixExceptionItemProductDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCasemixCoveredDetailProduct" runat="server" ValidationGroup="CasemixCoveredDetailProduct" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CasemixCoveredDetailProduct"
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
                ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="CasemixCoveredDetailProduct"
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
                    <td class="label" style="text-align: center">Global
                    </td>
                    <td style="width: 2px"></td>
                    <td class="label" style="text-align: center">Inpatient
                    </td>
                    <td style="width: 2px"></td>
                    <td class="label" style="text-align: center">Outpatient
                    </td>
                    <td style="width: 2px"></td>
                    <td class="label" style="text-align: center">Emergency
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidate" Text="Need Casemix Validate" />
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidateIpr" Text="Need Casemix Validate" />
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidateOpr" Text="Need Casemix Validate" />
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsNeedCasemixValidateEmr" Text="Need Casemix Validate" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsAllowedToOrder" Text="Allowed To Order" />
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsAllowedToOrderIpr" Text="Allowed To Order" />
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsAllowedToOrderOpr" Text="Allowed To Order" />
                    </td>
                    <td style="width: 2px"></td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsAllowedToOrderEmr" Text="Allowed To Order" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td colspan="3">
            <asp:Label runat="server" ID="lblCaption" Text="* Need Casemix Validate : checked if validation is required" ForeColor="Blue"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td colspan="3">
            <asp:Label runat="server" ID="lblCaption2" Text="** Allowed To Order : allowed to be ordered on prescription order (EMR)" ForeColor="Blue"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CasemixCoveredDetailProduct"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="CasemixCoveredDetailProduct" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
