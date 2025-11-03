<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemProductMedicalLabelDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemProductMedicalLabelDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemProductMedicLabel" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="ItemProductMedicLabel" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemProductMedicLabel"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblLabelID" runat="server" Text="Label"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboLabelID" Width="300px" AutoPostBack="True"
                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                OnItemDataBound="cboLabelID_ItemDataBound" OnItemsRequested="cboLabelID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "LabelName") %>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvLabelID" runat="server" ErrorMessage="Label required."
                ControlToValidate="cboLabelID" SetFocusOnError="True" ValidationGroup="ItemProductMedicLabel"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemProductMedicLabel"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemProductMedicLabel" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
