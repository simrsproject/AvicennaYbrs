<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="AuditLogList.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.AuditLogList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTableName" runat="server" Text="Table Name" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTableName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="30px">
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAuditActionType" runat="server" Text="Audit Action Type" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboAuditActionType" runat="server" Width="300px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Create" Value="C" />
                                    <telerik:RadComboBoxItem runat="server" Text="Edit" Value="U" Selected="true" />
                                    <telerik:RadComboBoxItem runat="server" Text="Delete" Value="D" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="30px">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrimaryKeyData" runat="server" Text="Primary Key Data" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPrimaryKeyData" runat="server" Width="300px" MaxLength="1000" />
                        </td>
                        <td width="30px">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblActionByUserID" runat="server" Text="Action By User ID" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtActionByUserID" runat="server" Width="300px" MaxLength="40" />
                        </td>
                        <td width="30px">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Start Date" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtLogDateStart" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtLogTimeStart" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="30px">
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="End Date" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtLogDateEnd" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtLogTimeEnd" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="30px">
                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="false" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="AuditLogID,LogDateTime">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="AuditLogID" HeaderText="No"
                    UniqueName="AuditLogID" SortExpression="AuditLogID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TableName" HeaderText="Table Name"
                    UniqueName="TableName" SortExpression="TableName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="AuditActionType" HeaderText="Type"
                    UniqueName="AuditActionType" SortExpression="AuditActionType" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="PrimaryKeyData" HeaderText="Primary Key Data"
                    UniqueName="PrimaryKeyData" SortExpression="PrimaryKeyData" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ActionByUserID" HeaderText="User ID"
                    UniqueName="ActionByUserID" SortExpression="ActionByUserID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="160px" DataField="LogDateTime" HeaderText="Log Date Time"
                    UniqueName="LogDateTime" SortExpression="LogDateTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Caption=".: DETAIL CHANGE VALUE :." Name="grdAuditLogData"
                    AutoGenerateColumns="False">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ColumnName" HeaderText="Column Name"
                            UniqueName="ColumnName" SortExpression="ColumnName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="OldValue" HeaderText="OldValue"
                            UniqueName="OldValue" SortExpression="OldValue" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="NewValue" HeaderText="NewValue" UniqueName="NewValue"
                            SortExpression="NewValue" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </telerik:GridTableView>
                <telerik:GridTableView Caption=".: AUDIT LOG IN SAME SECOND :." Name="grdDetailInSameTime"
                    DataKeyNames="AuditLogID" AutoGenerateColumns="False">
                    <Columns>
                        <telerik:GridNumericColumn HeaderStyle-Width="30px" DataField="AuditLogID" HeaderText="No"
                            UniqueName="AuditLogID" SortExpression="AuditLogID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TableName" HeaderText="Table Name"
                            UniqueName="TableName" SortExpression="TableName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="AuditActionType" HeaderText="Type"
                            UniqueName="AuditActionType" SortExpression="AuditActionType" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="PrimaryKeyData" HeaderText="Primary Key Data"
                            UniqueName="PrimaryKeyData" SortExpression="PrimaryKeyData" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ActionByUserID" HeaderText="User ID"
                            UniqueName="ActionByUserID" SortExpression="ActionByUserID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="160px" DataField="LogDateTime" HeaderText="Log Date Time"
                            UniqueName="LogDateTime" SortExpression="LogDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Caption="Details Field Value" Name="grdAuditLogData2" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ColumnName" HeaderText="Column Name"
                                    UniqueName="ColumnName" SortExpression="ColumnName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="OldValue" HeaderText="OldValue"
                                    UniqueName="OldValue" SortExpression="OldValue" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="NewValue" HeaderText="NewValue" UniqueName="NewValue"
                                    SortExpression="NewValue" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
