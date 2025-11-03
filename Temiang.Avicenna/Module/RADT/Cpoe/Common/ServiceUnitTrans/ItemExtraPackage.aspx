<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ItemExtraPackage.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ItemExtraPackage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItemPackage">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemPackage" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdItemPackage" runat="server" OnNeedDataSource="grdItemPackage_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None">
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="DetailItemID" ClientDataKeyNames="DetailItemID">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server"  ></asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsSelected") %>'></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Item ID"
                                UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="refToItem_ItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                                SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
