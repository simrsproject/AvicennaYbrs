<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorServiceUnitPlafondDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorServiceUnitPlafondDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumGuarantorServiceUnitPlafond" runat="server" ValidationGroup="GuarantorServiceUnitPlafond" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorServiceUnitPlafond"
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
                        <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                            OnItemsRequested="cboServiceUnitID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                            ControlToValidate="cboServiceUnitID" SetFocusOnError="True" ValidationGroup="GuarantorServiceUnitPlafond"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPlafondAmount" runat="server" Text="Plafond Amount"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPlafondAmount" runat="server" Width="100px" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvPlafondAmount" runat="server" ErrorMessage="Plafond Amount required."
                            ControlToValidate="txtPlafondAmount" SetFocusOnError="True" ValidationGroup="GuarantorServiceUnitPlafond"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorServiceUnitPlafond"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="GuarantorServiceUnitPlafond" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
