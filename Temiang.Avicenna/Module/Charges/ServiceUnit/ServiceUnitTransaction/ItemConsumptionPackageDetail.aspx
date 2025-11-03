<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ItemConsumptionPackageDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ItemConsumptionPackageDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblItemID" runat="server" Text="Item" />
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="True"
                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboItemID_ItemDataBound"
                    OnItemsRequested="cboItemID_ItemsRequested">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                        </b>
                        <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Balance")) > 0 ? DataBinder.Eval(Container.DataItem, "Balance", "<br />Stock : {0:n2}") : string.Empty%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 15 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                    ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
    </table>
</asp:Content>
