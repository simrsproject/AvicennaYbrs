<%@ Page Title="Transaction Detail Setting" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="HealthScreeningTariffComponent.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.HealthScreeningTariffComponent" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdTariff" runat="server" OnItemCreated="grdTariff_ItemCreated"
        AutoGenerateColumns="False" GridLines="None" Height="300px" OnNeedDataSource="grdTariff_NeedDataSource">
        <MasterTableView DataKeyNames="TariffComponentID" CellSpacing="-1">
            <Columns>
                <telerik:GridBoundColumn DataField="TariffComponentID" UniqueName="TariffComponentID"
                    SortExpression="TariffComponentID" Visible="False" />
                <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component Name"
                    UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Physician" UniqueName="DiscountText" HeaderStyle-HorizontalAlign="left">
                    <HeaderStyle Width="310px" />
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ParamedicID" UniqueName="ParamedicID" Visible="False" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
