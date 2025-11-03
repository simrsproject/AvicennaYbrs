<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemConditionRuleItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ItemConditionRuleItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemConditionRuleItem" runat="server" ValidationGroup="ItemConditionRuleItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemConditionRuleItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="30%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EmptyMessage="Select a Item"
                            EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                            OnClientItemsRequesting="cboItemID_ClientItemsRequesting" OnClientFocus="showDropDown">
                            <WebServiceSettings Method="ItemTransactionEntrySelection" Path="~/WebService/ComboBoxDataService.asmx" />
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ItemConditionRuleItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="entry" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemConditionRuleItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemConditionRuleItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
