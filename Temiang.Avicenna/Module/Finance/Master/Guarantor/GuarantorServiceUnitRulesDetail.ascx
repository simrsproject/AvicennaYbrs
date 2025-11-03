<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorServiceUnitRulesDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorServiceUnitRulesDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumGuarantorItemRule" runat="server" ValidationGroup="GuarantorItemRule" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorItemRule"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rblInclude">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblRuleType" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Height="190px" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                            OnItemsRequested="cboServiceUnitID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                            ControlToValidate="cboServiceUnitID" SetFocusOnError="True" ValidationGroup="GuarantorItemRule"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rblInclude" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Include</asp:ListItem>
                            <asp:ListItem>Exclude</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorItemRule"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="GuarantorItemRule" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
        </td>
    </tr>
</table>
