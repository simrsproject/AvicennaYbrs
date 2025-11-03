<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalRangeUserDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.ApprovalRangeUserDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumApprovalRangeUser" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="ApprovalRangeUser" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ApprovalRangeUser"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItem" runat="server" Text="User Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboUserID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="true" MarkFirstMatch="true" OnItemDataBound="cboUserID_ItemDataBound"
                            OnItemsRequested="cboUserID_ItemsRequested" >
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "UserName") %>
                                &nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "UserID")%>) </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ErrorMessage="Item required."
                            ValidationGroup="ApprovalRangeUser" ControlToValidate="cboUserID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblApprovalLevel" runat="server" Text="Approval Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtApprovalLevel" runat="server" Width="100px" MaxValue="10" MinValue="0" NumberFormat-DecimalDigits="0"/>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ApprovalRangeUser"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ApprovalRangeUser" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
