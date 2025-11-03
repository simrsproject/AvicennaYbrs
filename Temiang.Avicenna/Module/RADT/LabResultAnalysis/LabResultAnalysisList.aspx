<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="LabResultAnalysisList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.LabResultAnalysisList" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function examOrderResult(patientID, regNo, serviceUnitID, trno) {

                if (serviceUnitID == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitLaboratoryID%>') {
                    var url =
                        '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderLabResult.aspx?patid=' + patientID + '&regno=' + regNo + '&trno=' +
                            trno;
                    openWindow(url, 1200, 800);
                }
            }

        </script>

    </telerik:RadCodeBlock>


    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="1200px" Height="800px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadWindowManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterExecutionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxPanel ID="radAjaxPanel" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Job Order Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtOrderDate1" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtOrderDate2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionNo" runat="server" Text="Job Order No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterTransactionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Execution Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtExecutionDate" runat="server" Width="100px" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterExecutionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="20000" Enabled="False" />
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnItemCommand="grdList_ItemCommand" OnItemCreated="grdList_ItemCreated"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="15">

        <MasterTableView DataKeyNames="RegistrationNo, TransactionNo" ClientDataKeyNames="RegistrationNo, TransactionNo"
            GroupLoadMode="Server" HierarchyLoadMode="ServerOnDemand">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="Group" HeaderText="Job Order Date " FormatString="{0:dd-MMM-yyyy}"></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="Group" SortOrder="Descending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>

            <NestedViewTemplate>
                <asp:Panel runat="server" ID="InnerContainer" CssClass="viewWrap" Visible="false">
                    <div style="padding-left: 35px;">
                        <%#LabResult(DataBinder.Eval(Container.DataItem, "TransactionNo").ToString())%>
                    </div>
                </asp:Panel>
            </NestedViewTemplate>

            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="35px">
                    <ItemTemplate>
                        <%# string.Format(
                                "<a href=\"#\" onclick=\"javascript:examOrderResult('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../Images/Toolbar/views16.png\"  alt=\"View\" /></a>",
                                DataBinder.Eval(Container.DataItem,"PatientID"), DataBinder.Eval(Container.DataItem,"RegistrationNo"), DataBinder.Eval(Container.DataItem,"ServiceUnitID"), DataBinder.Eval(Container.DataItem,"TransactionNo")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="135px" HeaderText="Transaction No" UniqueName="TransactionNo" SortExpression="TransactionNo"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>&nbsp;
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCitoAvailable")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Cito\" title=\"Cito\" />" : string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="TransactionTime" HeaderText="Time"
                    UniqueName="TransactionTime" SortExpression="TransactionTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExecutionDate" HeaderText="Execution Date"
                    UniqueName="ExecutionDate" SortExpression="ExecutionDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                    UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClusterName" HeaderText="Service Unit" UniqueName="ClusterName"
                    SortExpression="ClusterName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BedID" HeaderText="Bed No"
                    UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
