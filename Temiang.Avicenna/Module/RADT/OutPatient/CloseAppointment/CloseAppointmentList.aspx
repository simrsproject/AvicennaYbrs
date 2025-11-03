<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="CloseAppointmentList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.CloseAppointmentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "closed":
                        if (confirm('Are you sure to close selected appointment?'))
                            __doPostBack("<%= grdAppointmentList.UniqueID %>", 'closed');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdAppointmentList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointmentList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointmentList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterMedicalNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointmentList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Closing Process" Value="closed" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
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
                </table>
            </td>
            <td width="50%">
                <table width="100%">
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
                    <tr style="height: 24px; display: none">
                        <td style="text-align: right">
                            <asp:Button ID="btnCloseProcess" runat="server" Text="Close Process" Width="120px"
                                OnClick="btnCloseProcess_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdAppointmentList" runat="server" OnNeedDataSource="grdAppointmentList_NeedDataSource"
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
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="35px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Visible="true"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
                    HeaderStyle-Width="170px" SortExpression="VisitTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="VisitDuration" HeaderText="Duration"
                    UniqueName="VisitDuration" SortExpression="VisitDuration" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Status" UniqueName="ItemName"
                    HeaderStyle-Width="70px" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
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
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
