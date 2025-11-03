<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorRecipeAmountDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorRecipeAmountDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumGuarantorRecipeAmount" runat="server" ValidationGroup="GuarantorRecipeAmount" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorRecipeAmount"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
               <tr>
                        <td class="label">
                            <asp:Label ID="lblStartingValue" runat="server" Text="Starting Value" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtStartingValue" runat="server" Width="100px" MaxValue="9999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStartingValue" runat="server" ErrorMessage="Starting Value required."
                                ValidationGroup="GuarantorRecipeAmount" ControlToValidate="txtStartingValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEndingValue" runat="server" Text="Ending Value" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtEndingValue" runat="server" Width="100px" MaxValue="9999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEndingValue" runat="server" ErrorMessage="Ending Value required."
                                ValidationGroup="GuarantorRecipeAmount" ControlToValidate="txtEndingValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRecipeAmount" runat="server" Text="Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRecipeAmount" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRecipeAmount" runat="server" ErrorMessage="Recipe Amount required."
                                ValidationGroup="GuarantorRecipeAmount" ControlToValidate="txtRecipeAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorRecipeAmount"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="GuarantorRecipeAmount" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%" runat="server">
                
            </table>
        </td>
    </tr>
</table>