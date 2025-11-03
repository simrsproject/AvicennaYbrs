<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="PatientIncidentInvestigationList.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentInvestigationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoEditUrl(ino, uid) {
                var url = 'PatientIncidentInvestigationDetail.aspx?md=edit&id=' + ino + '&uid=' + uid;
                window.location.href = url;
            }

            function gotoViewUrl(ino, uid) {
                var url = 'PatientIncidentInvestigationDetail.aspx?md=view&id=' + ino + '&uid=' + uid;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterIncidentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterIncidentNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInvestigationServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReportedBy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInvestigationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOutstanding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdInvestigation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvestigation" />
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
                            <td class="label">
                                <asp:Label ID="lblIncidentDate" runat="server" Text="Incident Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtIncidentFromDate" runat="server" Width="100px" />
                                        </td>
                                        <td></td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtIncidentToDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterIncidentDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblIncidentNo" runat="server" Text="Incident No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtIncidentNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterIncidentNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Investigation Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboInvestigationServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterInvestigationServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Reported By Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReportedBy" runat="server" Text="Reported By"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtReportedBy" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterReportedBy" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Oustanding List" PageViewID="pgOutstanding"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Investigation List" PageViewID="pgInvestigation">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <telerik:RadGrid ID="grdOutstanding" runat="server" OnNeedDataSource="grdOutstanding_NeedDataSource"
                AllowSorting="true" ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="PatientIncidentNo, ServiceUnitID" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserEditAble.Equals(false) ? string.Empty :
                                        string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                                                                                                    DataBinder.Eval(Container.DataItem, "PatientIncidentNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                                                                                                DataBinder.Eval(Container.DataItem, "PatientIncidentNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="PatientIncidentNo" HeaderText="Incident No" UniqueName="PatientIncidentNo"
                            SortExpression="PatientIncidentNo">
                            <HeaderStyle HorizontalAlign="Center" Width="135px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IncidentDateTime" HeaderText="Incident Date"
                            UniqueName="IncidentDateTime" SortExpression="IncidentDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ReportingDateTime" HeaderText="Reporting Date"
                            UniqueName="ReportingDateTime" SortExpression="ReportingDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RelatedUnitName" HeaderText="Investigation Unit" UniqueName="RelatedUnitName"
                            SortExpression="RelatedUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="135px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="95px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Reported By Unit" UniqueName="ReportedBy">
                            <ItemTemplate>
                                <asp:Label ID="lblServiceUnitName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName") %>' /><br />
                                By :
                                <asp:Label ID="lblReportedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReportedBy") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IncidentGroupName" HeaderText="Incident Group"
                            UniqueName="IncidentGroupName" SortExpression="IncidentGroupName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RiskGradingName" HeaderText="Risk Grading"
                            UniqueName="RiskGradingName" SortExpression="RiskGradingName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="RiskGradingColor" HeaderStyle-Width="50px" HeaderText="Risk Grading"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRiskGradingColor" runat="server" Width="30px" BackColor='<%# GetColorOfGradingColor(DataBinder.Eval(Container.DataItem,"RiskGradingColor")) %>'></asp:TextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgInvestigation" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblInvestigationDate" runat="server" Text="Investigation Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtInvestigationDate" runat="server" Width="100px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterInvestigationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdInvestigation" runat="server" OnNeedDataSource="grdInvestigation_NeedDataSource"
                OnDetailTableDataBind="grdInvestigation_DetailTableDataBind" AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="PatientIncidentNo, ServiceUnitID" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                                                                                                                                            DataBinder.Eval(Container.DataItem, "PatientIncidentNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="PatientIncidentNo" HeaderText="Incident No" UniqueName="PatientIncidentNo"
                            SortExpression="PatientIncidentNo">
                            <HeaderStyle HorizontalAlign="Center" Width="135px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IncidentDateTime" HeaderText="Incident Date"
                            UniqueName="IncidentDateTime" SortExpression="IncidentDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ReportingDateTime" HeaderText="Reporting Date"
                            UniqueName="ReportingDateTime" SortExpression="ReportingDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RelatedUnitName" HeaderText="Investigation Unit" UniqueName="RelatedUnitName"
                            SortExpression="RelatedUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="135px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="95px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Reported By Unit" UniqueName="ReportedBy">
                            <ItemTemplate>
                                <asp:Label ID="lblServiceUnitName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName") %>' /><br />
                                By :
                                <asp:Label ID="lblReportedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReportedBy") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IncidentGroupName" HeaderText="Incident Group"
                            UniqueName="IncidentGroupName" SortExpression="IncidentGroupName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RiskGradingName" HeaderText="Risk Grading"
                            UniqueName="RiskGradingName" SortExpression="RiskGradingName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="RiskGradingColor" HeaderStyle-Width="50px" HeaderText="Risk Grading"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRiskGradingColor" runat="server" Width="30px" BackColor='<%# GetColorOfGradingColor(DataBinder.Eval(Container.DataItem,"RiskGradingColor")) %>'></asp:TextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsInvestigationApproved"
                            HeaderText="Approved" UniqueName="IsInvestigationApproved" SortExpression="IsInvestigationApproved"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="Detail" DataKeyNames="SeqNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SeqNo" HeaderText="SeqNo"
                                    UniqueName="SeqNo" SortExpression="SeqNo" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Recomendation" HeaderText="Recomendation" UniqueName="Recomendation"
                                    SortExpression="Recomendation">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RecomendationDateTime"
                                    HeaderText="Recomendation Date" UniqueName="RecomendationDateTime" SortExpression="RecomendationDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PersonInCharge" HeaderText="Person In Charge"
                                    UniqueName="PersonInCharge" SortExpression="PersonInCharge">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Implementation" HeaderText="Implementation" UniqueName="Implementation"
                                    SortExpression="Implementation">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ImplementationDateTime"
                                    HeaderText="Implementation Date" UniqueName="ImplementationDateTime" SortExpression="ImplementationDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
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
    </telerik:RadMultiPage>
</asp:Content>
