<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="AppointmentStatusList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.AppointmentStatusList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowConfirmed(apptNo) {
                __doPostBack("<%= grdOutstandingList.UniqueID %>", 'confirmed|' + apptNo);
            }
            function rowNoResponse(apptNo) {
                var msg = prompt('Notes');
                if (msg != null) {
                    var param = 'noresponse|' + apptNo + '|' + msg;
                    __doPostBack("<%= grdOutstandingList.UniqueID %>", param);
                }
                <%--__doPostBack("<%= grdOutstandingList.UniqueID %>", 'noresponse|' + apptNo);--%>
            }
            function rowCanceled(apptNo) {
                var msg = prompt('Reason');
                if (msg != null) {
                    var param = 'canceled|' + apptNo + '|' + msg;
                    __doPostBack("<%= grdOutstandingList.UniqueID %>", param);
                }
                <%--__doPostBack("<%= grdOutstandingList.UniqueID %>", 'canceled|' + apptNo);--%>
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdOutstandingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchNotes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterMedicalNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAppointmentType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFollowUpList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%" runat="server" id="pnlFilterDate">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAppointmentDate" runat="server" Text="Appointment Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20">
                            <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" MaxLength="20" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnFilterMedicalNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" MaxLength="50" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:ImageButton ID="btnSearchServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:ImageButton ID="btnSearchParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAppointmentType" runat="server" Text="Appointment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRAppoinmentType" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSRAppoinmentType_ItemDataBound"
                                OnItemsRequested="cboSRAppoinmentType_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:ImageButton ID="btnSearchAppointmentType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" MaxLength="50" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:ImageButton ID="btnSearchNotes" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding List" PageViewID="pgOs" Selected="True" />
            <telerik:RadTab runat="server" Text="Follow Up List" PageViewID="pgFu" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOs" runat="server" Selected="true">
            <telerik:RadGrid ID="grdOutstandingList" runat="server" OnNeedDataSource="grdOutstandingList_NeedDataSource"
                AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowMultiRowSelection="true">
                <MasterTableView DataKeyNames="AppointmentNo">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="GROUPGRID" HeaderText="Physician "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="GROUPGRID" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="AppointmentQue" HeaderText="Que No"
                            UniqueName="AppointmentQue" SortExpression="AppointmentQue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="AppointmentNo" HeaderText="Appointment No"
                            UniqueName="AppointmentNo" SortExpression="AppointmentNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="AppointmentDate"
                            HeaderText="Date" UniqueName="AppointmentDate" SortExpression="AppointmentDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="50px" DataField="AppointmentTime"
                            HeaderText="Time" UniqueName="AppointmentDate" SortExpression="AppointmentTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="VisitTypeName" HeaderText="Visit Type" UniqueName="VisitTypeName"
                            HeaderStyle-Width="100px" SortExpression="VisitTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="VisitDuration" HeaderText="Duration"
                            UniqueName="VisitDuration" SortExpression="VisitDuration" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridBoundColumn DataField="AppointmentStatus" HeaderText="Status" UniqueName="AppointmentStatus"
                            HeaderStyle-Width="90px" SortExpression="AppointmentStatus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="AppointmentType" HeaderText="Appt. Type" UniqueName="AppointmentType"
                            HeaderStyle-Width="100px" SortExpression="AppointmentType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes" HeaderStyle-Width="150px"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            HeaderStyle-Width="100px" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                            HeaderStyle-Width="100px" SortExpression="PhoneNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile No" UniqueName="MobilePhoneNo"
                            HeaderStyle-Width="100px" SortExpression="MobilePhoneNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="LastCreateByUserID" HeaderText="Created by"
                            UniqueName="LastCreateByUserID" SortExpression="LastCreateByUserID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"rowConfirmed('{0}'); return false;\"><img src=\"../../../../Images/ok16.png\" border=\"0\" title=\"Confirmed\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "AppointmentNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"rowNoResponse('{0}'); return false;\"><img src=\"../../../../Images/questionmark16.png\" border=\"0\" title=\"No Response\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "AppointmentNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"rowCanceled('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/cancel16.png\" border=\"0\" title=\"Cancel\" /></a>",
                                DataBinder.Eval(Container.DataItem, "AppointmentNo"))%>
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
        <telerik:RadPageView ID="pgFu" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%">
                        <table width="100%" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRAppointmentStatus" Width="300px" AllowCustomText="true"
                                        Filter="Contains" AutoPostBack="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdFollowUpList" runat="server" OnNeedDataSource="grdFollowUpList_NeedDataSource"
                AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowMultiRowSelection="true">
                <MasterTableView DataKeyNames="AppointmentNo">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="GROUPGRID" HeaderText="Physician "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="GROUPGRID" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="AppointmentQue" HeaderText="Que No"
                            UniqueName="AppointmentQue" SortExpression="AppointmentQue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="AppointmentNo" HeaderText="Appointment No"
                            UniqueName="AppointmentNo" SortExpression="AppointmentNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="AppointmentDate"
                            HeaderText="Date" UniqueName="AppointmentDate" SortExpression="AppointmentDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="40px" DataField="AppointmentTime"
                            HeaderText="Time" UniqueName="AppointmentDate" SortExpression="AppointmentTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="VisitTypeName" HeaderText="Visit Type" UniqueName="VisitTypeName"
                            HeaderStyle-Width="100px" SortExpression="VisitTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="VisitDuration" HeaderText="Duration"
                            UniqueName="VisitDuration" SortExpression="VisitDuration" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridBoundColumn DataField="AppointmentStatus" HeaderText="Status" UniqueName="AppointmentStatus"
                            HeaderStyle-Width="90px" SortExpression="AppointmentStatus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="AppointmentType" HeaderText="Appt. Type" UniqueName="AppointmentType"
                            HeaderStyle-Width="100px" SortExpression="AppointmentType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            HeaderStyle-Width="100px" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                            HeaderStyle-Width="100px" SortExpression="PhoneNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile No" UniqueName="MobilePhoneNo"
                            HeaderStyle-Width="100px" SortExpression="MobilePhoneNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="OfficerPicName" HeaderText="Follow Up By" UniqueName="OfficerPicName"
                            SortExpression="OfficerPicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="130px" DataField="FollowUpDateTime"
                            HeaderText="Follow Up Date" UniqueName="FollowUpDateTime" SortExpression="FollowUpDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
