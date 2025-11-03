<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="EmployeeMedicalHistoryList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.EmployeeMedicalHistoryList" %>

<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../JavaScript/jQuery.js"></script>

    <telerik:RadCodeBlock runat="server">

        <script type="text/javascript">

            function gotoViewUrl(personId) {
                location.replace('EmployeeMedicalHistory.aspx?id=' + personId);
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeNumber">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align: top; width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee Number"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterEmployeeNumber" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterEmployeeName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top; width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSREmployeeStatus" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterEmployeeStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSREmployeeType" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterEmployeeType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false"
        OnNeedDataSource="grdList_NeedDataSource" AllowPaging="True" PageSize="15">
        <MasterTableView DataKeyNames="PersonID" GroupLoadMode="client">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="view">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View Medical History\" /></a>",
                            DataBinder.Eval(Container.DataItem, "PersonID")) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonID" HeaderText="Person ID"
                    UniqueName="PersonID" SortExpression="PersonID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn DataField="SupervisorName" HeaderText="Supervisor Name"
                    UniqueName="SupervisorName" SortExpression="SupervisorName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmployeeStatusName" HeaderText="Status"
                    UniqueName="EmployeeStatusName" SortExpression="EmployeeStatusName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RemunerationTypeName" HeaderText="Remuneration Type"
                    UniqueName="SRRemunerationType" SortExpression="SRRemunerationType"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="AbsenceCardNo" HeaderText="Absence Card No"
                    UniqueName="AbsenceCardNo" SortExpression="AbsenceCardNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="JoinDate" HeaderText="Join Date"
                    UniqueName="JoinDate" SortExpression="JoinDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="OrganizationUnitName" HeaderText="Organization Unit"
                    UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Section / Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="PositionName" HeaderText="Position" UniqueName="PositionName"
                    SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PositionGradeName" HeaderText="Employee Grade"
                    HeaderStyle-Width="120px" UniqueName="PositionGradeName" SortExpression="PositionGradeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
