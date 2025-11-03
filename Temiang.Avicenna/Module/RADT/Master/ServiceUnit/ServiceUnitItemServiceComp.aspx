<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitItemServiceComp.aspx.cs" Title="Service Unit Item Component"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitItemServiceComp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdServiceUnitItemServiceComp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdServiceUnitItemServiceComp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
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
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
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
                <telerik:RadGrid ID="grdServiceUnitItemServiceComp" runat="server" OnNeedDataSource="grdServiceUnitItemServiceComp_NeedDataSource"
                    OnInsertCommand="grdServiceUnitItemServiceComp_InsertCommand" OnUpdateCommand="grdServiceUnitItemServiceComp_UpdateCommand"
                    AutoGenerateColumns="False" GridLines="None">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ServiceUnitID, ItemID, TariffComponentID, SRRegistrationType, SRGuarantorIncomeGroup">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn HeaderText="Tariff Component" UniqueName="TemplateItemName">
                                <ItemTemplate>
                                    <asp:Label ID="lblTariffComponentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TariffComponentID") %>' />
                                    &nbsp;-&nbsp;
                                    <asp:Label ID="lblTariffComponentName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TariffComponentName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn  HeaderStyle-Width="150px" DataField="RegistrationType" HeaderText="Registration Type"
                                UniqueName="RegistrationType" SortExpression="RegistrationType" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn HeaderText="Revenue" UniqueName="TemplateItemName" HeaderStyle-Width="400px">
                                <ItemTemplate>
                                    COA :
                                    <asp:Label ID="lblCOARevenueName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COARevenueName") %>' />
                                    <br />
                                    Subledger :
                                    <asp:Label ID="lblSubledgerRevenueName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubledgerRevenueName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Discount" UniqueName="TemplateItemName" HeaderStyle-Width="400px">
                                <ItemTemplate>
                                    COA :
                                    <asp:Label ID="lblCOADiscountName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COADiscountName") %>' />
                                    <br />
                                    Subledger :
                                    <asp:Label ID="lblSubledgerDiscountName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubledgerDiscountName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Paramedic Fee Cost" UniqueName="TemplateItemName"
                                HeaderStyle-Width="400px" Visible="true">
                                <ItemTemplate>
                                    COA :
                                    <asp:Label ID="lblCOACostName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COACostName") %>' />
                                    <br />
                                    Subledger :
                                    <asp:Label ID="lblSubledgerCostName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubledgerCostName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="GuarantorIncomeGroupName" HeaderText="Account Group"
                                UniqueName="GuarantorIncomeGroupName" SortExpression="GuarantorIncomeGroupName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <EditFormSettings UserControlName="ServiceUnitItemServiceCompDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ServiceUnitItemServiceCompDetailEditCommand">
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
