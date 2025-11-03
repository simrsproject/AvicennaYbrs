<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Title="Service Unit Item Class" CodeBehind="ServiceUnitItemServiceClass.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitItemServiceClass" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdServiceUnitItemService">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdServiceUnitItemService" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblItemName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdServiceUnitItemService" runat="server" OnNeedDataSource="grdServiceUnitItemService_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdServiceUnitItemService_UpdateCommand">
                    <HeaderContextMenu>                      
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, ItemID, ClassID, TariffComponentID"
                        PageSize="15">
                         <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ClassName" HeaderText="Class">
                                </telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ClassID" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn HeaderText="Class" UniqueName="TemplateItemName">
                                <ItemTemplate>
                                    <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassID") %>' />
                                    &nbsp;-&nbsp;
                                    <asp:Label ID="lblClassName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Tariff Component" UniqueName="TemplateItemName">
                                <ItemTemplate>
                                    <asp:Label ID="lblTariffComponentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TariffComponentID") %>' />
                                    &nbsp;-&nbsp;
                                    <asp:Label ID="lblTariffComponentName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TariffComponentName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Revenue" UniqueName="TemplateItemName" HeaderStyle-Width="300px">
                                <ItemTemplate>
                                    COA :
                                    <asp:Label ID="lblCOARevenueName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COARevenueName") %>' />
                                    <br />
                                    Subledger :
                                    <asp:Label ID="lblSubledgerRevenueName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubledgerRevenueName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Discount" UniqueName="TemplateItemName" HeaderStyle-Width="300px">
                                <ItemTemplate>
                                    COA :
                                    <asp:Label ID="lblCOADiscountName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COADiscountName") %>' />
                                    <br />
                                    Subledger :
                                    <asp:Label ID="lblSubledgerDiscountName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubledgerDiscountName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Cost" UniqueName="TemplateItemName" HeaderStyle-Width="300px"
                                Visible="false">
                                <ItemTemplate>
                                    COA :
                                    <asp:Label ID="lblCOACostName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COACostName") %>' />
                                    <br />
                                    Subledger :
                                    <asp:Label ID="lblSubledgerCostName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubledgerCostName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ServiceUnitItemServiceClassDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ServiceUnitItemServiceClassDetailEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                        
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
