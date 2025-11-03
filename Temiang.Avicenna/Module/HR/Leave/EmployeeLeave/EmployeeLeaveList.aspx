<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmployeeLeaveList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.EmployeeLeaveList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinImport(programID) {
                var oWnd = $find("<%= winImport.ClientID %>");
                oWnd.setUrl("Import.aspx?id=" + programID);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="350px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" ID="winImport">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="1" cellspacing="1" runat="server" id="tblExportParameter">
        <tr>
            <td>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblExportParameter" runat="server" Text="EXPORT PARAMETER" Font-Bold="true"></asp:Label>
                    </legend>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeLeaveType" runat="server" Text="Employee Leave Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSREmployeeLeaveType" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" />
                                        </td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmploymentType" runat="server" Text="Employment Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSREmploymentType" runat="server" Width="300px" />
                                        </td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOrganizationUnitID" runat="server" Text="Department"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboOrganizationUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound"
                                                OnItemsRequested="cboOrganizationUnitID_ItemsRequested" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 20 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Section / Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                                OnItemsRequested="cboServiceUnitID_ItemsRequested">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 20 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="EmployeeLeaveID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeLeaveID"
                    HeaderText="ID" UniqueName="EmployeeLeaveID" SortExpression="EmployeeLeaveID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmployeeLeaveTypeName" HeaderText="Employee Leave Type"
                    UniqueName="EmployeeLeaveTypeName" SortExpression="EmployeeLeaveTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                    UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                    UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="LeaveEntitlementsQty"
                    HeaderText="Leave Entitlements (Days)" UniqueName="LeaveEntitlementsQty" SortExpression="LeaveEntitlementsQty"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Balance" HeaderText="Balance"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
