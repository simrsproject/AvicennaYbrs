<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemProductMedicalZatActiveDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemProductMedicalZatActiveDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemProductMedicZatActive" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="ItemProductMedicZatActive" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemProductMedicZatActive"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblZatActiveID" runat="server" Text="Zat Active"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboZatActiveID" Width="300px" AutoPostBack="True"
                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                OnItemDataBound="cboZatActiveID_ItemDataBound" OnItemsRequested="cboZatActiveID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ZatActiveName") %>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvZatActiveID" runat="server" ErrorMessage="ZatActive required."
                ControlToValidate="cboZatActiveID" SetFocusOnError="True" ValidationGroup="ItemProductMedicZatActive"
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
            <asp:CheckBox ID="chkIsPrinted" runat="server" Text="Printed in PO Precursor" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemProductMedicZatActive"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemProductMedicZatActive" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
