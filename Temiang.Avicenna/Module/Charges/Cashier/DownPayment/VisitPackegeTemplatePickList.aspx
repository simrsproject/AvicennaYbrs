<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="VisitPackegeTemplatePickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.VisitPackegeTemplatePickList" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnQuery">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table>
        <tr>
            <td class="label">
                <asp:Label ID="lblVisitPackageID" runat="server" Text="Visit Package"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboVisitPackageID" runat="server" Width="300px" 
                    MarkFirstMatch="true" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                    OnItemDataBound="cboVisitPackageID_ItemDataBound" OnItemsRequested="cboVisitPackageID_ItemsRequested">
                    <FooterTemplate>
                        Note : Show max 20 result
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:Button runat="server" ID="btnQuery" Text="Query" OnClick="btnQuery_Click" />
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False"
        GridLines="None" OnNeedDataSource="grdDetail_NeedDataSource" AllowPaging="True">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" PageSize="50">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px"
                    AllowFiltering="False">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# GetInt(DataBinder.Eval(Container.DataItem,"IsSelect"))==1%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="70px" DbValue='<%#DataBinder.Eval(Container.DataItem,"Qty")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
