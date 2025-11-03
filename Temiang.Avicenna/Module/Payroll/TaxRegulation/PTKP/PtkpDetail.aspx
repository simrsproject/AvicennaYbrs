<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PtkpDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.TaxRegulation.PtkpDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblPtkpID" runat="server" Text="Ptkp ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtPtkpID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPtkpID" runat="server" ErrorMessage="Ptkp ID required."
                    ValidationGroup="entry" ControlToValidate="txtPtkpID" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td>
                            &nbsp;to&nbsp;&nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                    ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                    ValidationGroup="entry" ControlToValidate="txtValidTo" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRTaxStatus" runat="server" Text="Tax Status"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRTaxStatus" runat="server" Width="304px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRTaxStatus" runat="server" ErrorMessage="Tax Status required."
                    ValidationGroup="entry" ControlToValidate="cboSRTaxStatus" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Amount required."
                    ValidationGroup="entry" ControlToValidate="txtAmount" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
