<%@ Page Title="Batch Number & Expired Date List" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PrescriptionSalesEtiquetteDetailEdList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionSalesEtiquetteDetailEdList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="20"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="ListKey" GroupLoadMode="client">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="BatchNumber" HeaderText="Batch Number" UniqueName="BatchNumber"
                    SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="ExpiredDate"
                    HeaderText="Expired Date" UniqueName="ExpiredDate" SortExpression="ExpiredDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QuantityInBaseUnit" HeaderText="Purc. Quantity"
                    UniqueName="QuantityInBaseUnit" SortExpression="QuantityInBaseUnit" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>