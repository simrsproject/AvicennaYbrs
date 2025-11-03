<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustomNoMenu.Master" 
    AutoEventWireup="true" CodeBehind="PrescriptionQueue.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionQueue" %>

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
    <table>
        <tr>
            <td valign="top">
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    OnItemDataBound="grdList_ItemDataBound"
                    AllowPaging="false" AutoGenerateColumns="false">
                    <MasterTableView DataKeyNames="PrescriptionNo" Font-Size="18pt"
                    ItemStyle-Height="30" AlternatingItemStyle-Height="30" 
                    ItemStyle-Font-Names="Rockwell,Cambria,Georgia" AlternatingItemStyle-Font-Names="Rockwell,Cambria,Georgia"
                    HeaderStyle-Height="40" HeaderStyle-Font-Bold="true" >
                        <Columns>
                            <telerik:GridBoundColumn DataField="PrescriptionNo" HeaderText="Prescription No"
                                UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Nama"
                                UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status"
                                UniqueName="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn DataField="Duration" HeaderText="Waktu" HeaderStyle-Width="90"
                                UniqueName="Duration" SortExpression="Duration" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="true" />
                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" HeaderStyle-Width="140"
                                UniqueName="StatusName" SortExpression="StatusName" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="Flag" HeaderText="Compound<br>(Racikan)" HeaderStyle-Width="80" HeaderStyle-Font-Size="10"
                                UniqueName="Flag" SortExpression="Flag" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="False">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td valign="top">
                <telerik:RadGrid ID="grdListComplete" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    OnItemDataBound="grdList_ItemDataBound"
                    AllowPaging="false" AutoGenerateColumns="false">
                    <MasterTableView DataKeyNames="PrescriptionNo" Font-Size="18pt"
                    ItemStyle-Height="30" AlternatingItemStyle-Height="30"
                    ItemStyle-Font-Names="Rockwell,Cambria,Georgia" AlternatingItemStyle-Font-Names="Rockwell,Cambria,Georgia"
                    HeaderStyle-Height="40" HeaderStyle-Font-Bold="true" >
                        <Columns>
                            <telerik:GridBoundColumn DataField="PrescriptionNo" HeaderText="Prescription No"
                                UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Nama"
                                UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status"
                                UniqueName="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn DataField="Duration" HeaderText="Waktu" HeaderStyle-Width="90"
                                UniqueName="Duration" SortExpression="Duration" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="true" />
                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" HeaderStyle-Width="140"
                                UniqueName="StatusName" SortExpression="StatusName" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="Flag" HeaderText="Compound<br>(Racikan)" HeaderStyle-Width="80" HeaderStyle-Font-Size="10"
                                UniqueName="Flag" SortExpression="Flag" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="False">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
