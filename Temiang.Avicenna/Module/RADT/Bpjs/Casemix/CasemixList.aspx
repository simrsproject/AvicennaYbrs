<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="CasemixList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.CasemixList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoAddUrl(regno, appst, BpjsSepNo) {
                if (!BpjsSepNo || BpjsSepNo == "") {
                    alert("Error: Data can't be open because BPJS SEP Number is Empty.");
                    return;
                }
                var url = 'CasemixDetail.aspx?md=new&regno=' + regno + '&appst=' + appst;
                window.location.href = url;
            }
            function onClickDocument(regNo) {
                window.location.href = "casemixDetail.aspx?regNo=" + regNo + "&tab=pgDocument";
            }
            function openMDSPrint(regNo) {
                radopen("CasemixPrint.aspx?regno=" + regNo + "&rt=", "winMDSPrint");
            }

            function onClientCloseMDSPrint(oWnd, args) {
                if (oWnd.argument && oWnd.argument.print != null) {
                    var opn;
                    opn = radopen('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>', "winPrintPreview");
                    opn.maximize();
                }
                oWnd = null;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDischarge">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterNoSep">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Modal="true" VisibleStatusbar="false"
        DestroyOnClose="false" Behavior="Close, Move" ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>            
            <telerik:RadWindow ID="winMDSPrint" Width="400px" Height="300px" runat="server" Title="Select report then click Ok button"
                OnClientClose="onClientCloseMDSPrint">
            </telerik:RadWindow>            
            <telerik:RadWindow ID="winPrintPreview" Behavior="Maximize,Move,Close" Width="1000px"
                Height="630px" runat="server" Title="Preview">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="60000" Enabled="True" />
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding" PageViewID="pgOutstanding" Selected="true" />
            <telerik:RadTab runat="server" Text="History" PageViewID="pgHistory" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr id="Tr1" runat="server">
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" Text="Transaction Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="110px"></telerik:RadDatePicker>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterOutstanding_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                AllowSorting="true" OnItemDataBound="grdList_ItemDataBound">
                <MasterTableView Name="master" DataKeyNames="RegistrationNo, ServiceUnitID" ClientDataKeyNames="RegistrationNo, ServiceUnitID"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Physician "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}', '1', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "BpjsSepNo"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate">
                            <HeaderStyle HorizontalAlign="Center" Width="75px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="145px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="200px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="55px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Group" HeaderText="Service Unit" UniqueName="Group"
                            SortExpression="Group">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Room" UniqueName="RoomName" SortExpression="RoomName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="110px" HeaderText="Bed No">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="LoS" HeaderText="LoS" UniqueName="LoS"
                            SortExpression="LoS" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "DiagnoseName")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Pathway Name" UniqueName="PathwayName"
                            SortExpression="PathwayName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PathwayID")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "PathwayName")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                        <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID"
                            Visible="False" />
                        <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />
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
        <telerik:RadPageView ID="pgHistory" runat="server">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr id="pnlFilterDate" runat="server">
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>&nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtToDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblDischargeDate" runat="server" Text="Discharge Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDischareFromDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>&nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDischareToDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchDischarge" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboRegistrationType" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
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
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                            <FooterTemplate>
                                                Note : Show max 30 result
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">No. SEP
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtNoSep" runat="server" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnFilterNoSep" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td />
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
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList2" runat="server" OnNeedDataSource="grdList2_NeedDataSource" 
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                AllowSorting="true" OnItemDataBound="grdList_ItemDataBound" OnPageIndexChanged="grdList2_PageIndexChanged">
                <MasterTableView Name="master" DataKeyNames="RegistrationNo, ServiceUnitID" ClientDataKeyNames="RegistrationNo, ServiceUnitID"
                    GroupLoadMode="Client">
                    <%--<GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Physician "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>--%>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}', '', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "BpjsSepNo"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate">
                            <HeaderStyle HorizontalAlign="Center" Width="75px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="145px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BpjsSepNo" HeaderText="No. SEP" UniqueName="BpjsSepNo"
                            SortExpression="BpjsSepNo">
                            <HeaderStyle HorizontalAlign="Center" Width="145px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="200px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="55px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Group" HeaderText="Service Unit" UniqueName="Group"
                            SortExpression="Group">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Room" UniqueName="RoomName" SortExpression="RoomName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Left" UniqueName="BedNo"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="110px" HeaderText="Bed No">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="LoS" HeaderText="LoS" UniqueName="LoS"
                            SortExpression="LoS" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "DiagnoseName")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                        
                        <telerik:GridTemplateColumn HeaderText="Pathway Name" UniqueName="PathwayName"
                            SortExpression="PathwayName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PathwayID")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "PathwayName")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                           
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center" UniqueName="document"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="">
                            <ItemTemplate>
                                <%#(string.Format("<a href=\"#\" onclick=\"onClickDocument('{0}'); return false;\">{1}</a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                        "<img src=\"../../../../Images/Toolbar/folder.png\" border=\"0\" alt=\"Document\" title=\"Document\" />"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center" UniqueName="print"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <%#(string.Format("<a href=\"#\" onclick=\"openMDSPrint('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsConsul" HeaderText="Consul"
                            UniqueName="IsConsul" SortExpression="IsConsul" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                        <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID"
                            Visible="False" />
                        <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />        
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
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
    </telerik:RadMultiPage>
</asp:Content>
