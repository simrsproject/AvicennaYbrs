<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustomNoMenu.Master" 
    AutoEventWireup="true" CodeBehind="BedInformationList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.BedInformationList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta http-equiv="Refresh" content="15" />
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <style type="text/css">
            body
                {
                    color: #000000;
                    margin: 0px;
                    font-family: Tahoma, Arial;
                    font-size: 11px;
                    background-color: #000000;
                }
        </style>
    </telerik:RadScriptBlock>
    <br /><br /><br /><br />
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnItemDataBound="grdList_ItemDataBound"
        AllowPaging="false" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="ServiceUnitID,ClassID,Gen" 
        ItemStyle-Height="35" AlternatingItemStyle-Height="35" Font-Size="20pt"
        ItemStyle-Font-Names="Rockwell,Cambria,Georgia" AlternatingItemStyle-Font-Names="Rockwell,Cambria,Georgia"
        HeaderStyle-Height="40" HeaderStyle-Font-Bold="true" >
            <Columns>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Ruangan"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClassName" HeaderText="Kelas"
                    UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Gen" HeaderText="Gen"
                    UniqueName="Gen" SortExpression="Gen" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="Gender" HeaderText="Gender"
                    UniqueName="Gender" SortExpression="Gender" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="Jumlah" HeaderText="Kapasitas" HeaderStyle-Width="140"
                    UniqueName="Jumlah" SortExpression="Jumlah" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Occupied" HeaderText="Terisi" HeaderStyle-Width="140"
                    UniqueName="Occupied" SortExpression="Occupied" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Ready" HeaderText="Tersedia" HeaderStyle-Width="140"
                    UniqueName="Ready" SortExpression="Ready" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
