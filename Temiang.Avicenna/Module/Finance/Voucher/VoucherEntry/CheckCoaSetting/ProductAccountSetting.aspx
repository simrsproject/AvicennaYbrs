<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ProductAccountSetting.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting.ProductAccountSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                Product Account
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboProductAccount" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboProductAccount_ItemDataBound"
                    OnItemsRequested="cboProductAccount_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ProductAccountID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ProductAccountName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20" />
            <td width="20px" />
        </tr>
    </table>
</asp:Content>
