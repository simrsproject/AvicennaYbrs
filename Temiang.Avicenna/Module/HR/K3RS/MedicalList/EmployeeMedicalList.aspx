<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="EmployeeMedicalList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.EmployeeMedicalList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeNumber">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDiagnose">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterVisitReason">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">Registration Date
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRegistrationDateFrom" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRegistrationDateTo" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left" width="20px">
                                <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Toolbar/print16.png"
                                    OnClick="btnPrint_Click" ToolTip="Print Employee List" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Service Unit
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left" width="20px">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="btnPrint2" runat="server" ImageUrl="~/Images/Toolbar/print16.png"
                                    OnClick="btnPrint2_Click" ToolTip="Print Top 10 Diagnose" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Employee No / Medical No
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtEmployeeNumber" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td style="text-align: left" width="20px">
                                <asp:ImageButton ID="btnFilterEmployeeNumber" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td align="center"></td>
                        </tr>
                        <tr>
                            <td class="label">Employee Name
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtPatientName" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td style="text-align: left" width="20px">
                                <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td align="center"></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">Physician
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;" width="20px">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Diagnose
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboDiagnoseID" Width="300px" AllowCustomText="true"
                                    EnableLoadOnDemand="true" OnItemsRequested="cboDiagnoseID_ItemsRequested" OnItemDataBound="cboDiagnoseID_ItemDataBound">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;" width="20px">
                                <asp:ImageButton ID="btnFilterDiagnose" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Visit Reason
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRVisitReason" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;" width="20px">
                                <asp:ImageButton ID="btnFilterVisitReason" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Employee List" PageViewID="pgvAddress" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Top 10 Diagnose" PageViewID="pgvContact">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvAddress" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind">
                <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Registration To Unit " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="EmployeeNumber" HeaderText="Employee No" UniqueName="EmployeeNumber"
                            SortExpression="EmployeeNumber">
                            <HeaderStyle HorizontalAlign="Center" Width="95px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="95px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AgeInYear" HeaderText="Age" UniqueName="AgeInYear"
                            SortExpression="AgeInYear">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="OrganizationUnitName" HeaderText="Organization Unit"
                            UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="SubOrganizationUnit" HeaderText="Organization Unit"
                            UniqueName="SubOrganizationUnit" SortExpression="SubOrganizationUnit">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="135px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date"
                            UniqueName="RegistrationDate" SortExpression="RegistrationDate">
                            <HeaderStyle HorizontalAlign="Center" Width="85px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="VisitReasonName" HeaderText="Visit Reason" UniqueName="VisitReasonName"
                            SortExpression="VisitReasonName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="RegistrationNo, ItemName" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridHyperLinkColumn DataTextField="RegistrationNo" DataNavigateUrlFields="RegistrationNo"
                                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" Visible="false" />
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ChargeQuantity" HeaderText="Quantity"
                                    UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvContact" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRRegistrationType" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter2_Click" ToolTip="Search" />
                                </td>
                            </tr>

                        </table>
                    </td>
                    <td width="50%" valign="top"></td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList2" runat="server" OnNeedDataSource="grdList2_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                AllowSorting="true">
                <MasterTableView DataKeyNames="DiagnoseName" ClientDataKeyNames="DiagnoseName" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Count" HeaderText="Count" UniqueName="Count"
                            SortExpression="Count">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                    OpenInNewWindow="true" />
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
