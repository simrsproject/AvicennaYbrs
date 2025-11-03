<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master"
    AutoEventWireup="true" CodeBehind="PrescriptionHighAlertCheck.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.PrescriptionHighAlertCheck" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdfReturnValue" />
    <fieldset>
        <legend>High Alert</legend>
        <telerik:RadGrid ID="grdPrescriptionHighAlert" Width="100%" runat="server" RenderMode="Lightweight"
            AutoGenerateColumns="False" EnableViewState="true" AllowMultiRowSelection="True"
            OnItemDataBound="grdPrescriptionHighAlert_ItemDataBound" OnNeedDataSource="grdPrescriptionHighAlert_NeedDataSource">
            <MasterTableView DataKeyNames="ItemID" Width="100%">
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsSelected" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText=""
                        HeaderStyle-Width="30px" Display="False" />
                    <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText=""
                        HeaderStyle-Width="200px" />
                    <telerik:GridBoundColumn DataField="UserName" UniqueName="UserName"
                        HeaderText="By" />
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                <Resizing AllowColumnResize="False" />
                <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
            </ClientSettings>
        </telerik:RadGrid>
    </fieldset>
</asp:Content>
