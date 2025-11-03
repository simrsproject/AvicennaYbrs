<%@ Page Title="Update Physician" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" 
AutoEventWireup="true" CodeBehind="ParamedicFeeVerificationByDischargeDateDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.V2.ParamedicFeeVerificationByDischargeDateDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="300px" EnableLoadOnDemand="True"
                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboPhysicianID_ItemDataBound"
                    OnItemsRequested="cboPhysicianID_ItemsRequested">
                    <FooterTemplate>
                        Note : Show max 30 result
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician required."
                    ValidationGroup="entry" ControlToValidate="cboPhysicianID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>