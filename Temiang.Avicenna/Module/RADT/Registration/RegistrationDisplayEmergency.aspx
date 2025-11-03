<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustomNoMenu.Master" 
    AutoEventWireup="true" CodeBehind="RegistrationDisplayEmergency.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.RegistrationDisplayEmergency" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta http-equiv="Refresh" content="20" />
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
        <asp:Panel ID="pnlSU" runat="server">
        <table>
            <tr>
                <td>
                    <telerik:RadComboBox ID="cboSU" runat="server"></telerik:RadComboBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button runat="server" ID="btnSU" Text="GO!" OnClick="btnSU_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnItemDataBound="grdList_ItemDataBound"
        AllowPaging="false" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="MedicalNo" 
        ItemStyle-Height="35" AlternatingItemStyle-Height="35" Font-Size="20pt"
        ItemStyle-Font-Names="Rockwell,Cambria,Georgia" AlternatingItemStyle-Font-Names="Rockwell,Cambria,Georgia"
        HeaderStyle-Height="40" HeaderStyle-Font-Bold="true" >
            <Columns>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Kelas"
                    UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RegDate" HeaderText="RegistrationDate"
                    UniqueName="RegDate" SortExpression="RegDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Jam" HeaderText="Stay"
                    UniqueName="Jam" SortExpression="Jamr" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />                
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
