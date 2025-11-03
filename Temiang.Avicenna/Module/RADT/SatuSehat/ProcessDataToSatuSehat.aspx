<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ProcessDataToSatuSehat.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.ProcessDataToSatuSehat" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "process":
                        if (confirm('Are you sure to process the selected registration?'))
                            __doPostBack("<%= grdRegisteredList.UniqueID %>", 'process');
                        break;
                }
            }

            function openSatuSehatResultInfo(rtype, rid) {
                var oWnd = $find("<%= winInfo.ClientID %>");
                var url = 'SatuSehatResultInfo.aspx?rtype=' + rtype + '&rid=' + rid;
                // JANGAN PAKAI radopen,  urlnya harus lengkap dgn rootnya jika pakai radopen
                oWnd.setUrl(url);
                oWnd.setSize(1040, 600);
                oWnd.center();
                oWnd.show();
            }

            function CloseStatus(registrationNo) {
                if (confirm("Close this pending SatuSehat data?"))
                    __doPostBack("<%= grdRegisteredList.UniqueID %>", "closestatus|" + registrationNo);
            }

            function showDropDown(sender, eventArgs) {
                sender.showDropDown();
                sender.requestItems("[showall]", false);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterMedicalNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegisteredList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winInfo" Width="1000px" Height="900px" runat="server" Modal="true" VisibleStatusbar="false"
        DestroyOnClose="false" Behavior="Close, Move" ReloadOnShow="True" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process Selected Reg." Value="process" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <cc:CollapsePanel runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblDate" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" DateInput-DateFormat="dd/MM/yyyy">
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
                                <asp:Label runat="server" ID="Label1" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
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
                                <asp:Label runat="server" ID="Label2" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EmptyMessage="Select a Paramedic"
                                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" OnClientFocus="showDropDown">
                                    <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkIncludeClosed" Text="Show Closed" />
                                <asp:CheckBox runat="server" ID="chkIncludeFailed" Text="Show Failed" Checked="True" />
                                <asp:CheckBox runat="server" ID="chkHideEmptyIcd10" Text="Hide Empty ICD10" />
                                <%--<asp:CheckBox runat="server" ID="chkHideEmptySSN" Text="Hide Empty SSN" />--%>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterIncludeClosed" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkHideEmptySSN" Text="Hide Empty SSN" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" Visible="false" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblRegistrationNo" Text="Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterMedicalNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblPatientName" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By PatientName" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdRegisteredList" runat="server" OnNeedDataSource="grdRegisteredList_NeedDataSource"
        OnItemDataBound="grdRegisteredList_ItemDataBound" OnItemCommand="grdRegisteredList_ItemCommand"
        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true" ShowStatusBar="true" PageSize="15" AllowMultiRowSelection="true">
        <MasterTableView DataKeyNames="RegistrationNo,EncounterID" GroupLoadMode="Server">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsAllowClose") %>'></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                    HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="RegistrationTime" HeaderText="Time"
                    UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SSN" HeaderText="SSN"
                    UniqueName="SSN" SortExpression="SSN" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PatientBridgingID" HeaderText="Satu Sehat PID"
                    UniqueName="PatientBridgingID" SortExpression="PatientBridgingID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Gender"
                    UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="140px" />
                <telerik:GridCheckBoxColumn DataField="IsSoap" HeaderText="Soap"
                    UniqueName="IsSoap" SortExpression="IsSoap">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsIcd10" HeaderText="ICD-10"
                    UniqueName="IsIcd10" SortExpression="IsIcd10">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsVitalSign" HeaderText="VSign"
                    UniqueName="IsVitalSign" SortExpression="IsVitalSign">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsIcd9" HeaderText="ICD-9"
                    UniqueName="IsIcd9" SortExpression="IsIcd9">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsEduDiet" HeaderText="Diet"
                    UniqueName="IsEduDiet" SortExpression="IsEduDiet">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsPrescription" HeaderText="Presc."
                    UniqueName="IsPrescription" SortExpression="IsPrescription">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Satu Sehat EncounterID Result" UniqueName="EncounterID">
                    <HeaderStyle HorizontalAlign="Left" Width="300px" />
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <%# string.Format("<a style=\"cursor: pointer;\" onclick=\"openSatuSehatResultInfo('Encounter','{0}')\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Result View\" title=\"Result View\" />&nbsp;{0}</a>", DataBinder.Eval(Container.DataItem, "EncounterID"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Proccess Date" UniqueName="LastUpdateDateTime">
                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Left"
                    HeaderText="Last Error Process" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ErrorResponse")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#  string.Format("<a href=\"#\" onclick=\"CloseStatus('{0}'); return false;\"><img src=\"../../../Images/Toolbar/close.png\" border=\"0\" title=\"Close\" /></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="75px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PCareKdDokter" HeaderText="PCareKdDokter" UniqueName="PCareKdDokter" Display="False" />
                <telerik:GridBoundColumn DataField="ParamedicID" HeaderText="ParamedicID" UniqueName="ParamedicID" Display="False" />
            </Columns>
            <NestedViewTemplate>
                <fieldset style="padding-left: 20px; padding-bottom: 10px;">
                    <legend>Detail Proccess Log</legend>

                    <telerik:RadGrid ID="grdResult" runat="server"
                        AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="ResultID" ShowHeader="True">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ResourceType" HeaderText="Resource Type" UniqueName="ResourceType" HeaderStyle-Width="150px"
                                    SortExpression="ResourceType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Category" HeaderText="Category" UniqueName="Category" HeaderStyle-Width="150px"
                                    SortExpression="Category" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Code" HeaderText="Code" UniqueName="Code" HeaderStyle-Width="100px"
                                    SortExpression="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn HeaderText="Satu Sehat Result ID" UniqueName="ResultID">
                                    <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
<%--                                        <asp:LinkButton ID="lbtnResend" runat="server" CommandName="Resend" Visible='<%# DataBinder.Eval(Container.DataItem, "ResultID") == DBNull.Value%>'
                                            ToolTip='Resend' CommandArgument='<%# string.Format("{0}|{1}|{2}|{3}|{4}", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "HeaderEncounterID"),DataBinder.Eval(Container.DataItem, "ResourceType"),DataBinder.Eval(Container.DataItem, "Category"),DataBinder.Eval(Container.DataItem, "Code")) %>'>
                                    <img src="../../../Images/Toolbar/refresh16.png" border="0" />
                                        </asp:LinkButton>--%>

                                        <%# (DataBinder.Eval(Container.DataItem, "ResultID") != DBNull.Value && !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ResultID").ToString())) 
                                                ? string.Format("<a style=\"cursor: pointer;\" onclick=\"openSatuSehatResultInfo('{0}','{1}')\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Result View\" title=\"Result View\" />&nbsp;{1}</a>", DataBinder.Eval(Container.DataItem, "ResourceType"), DataBinder.Eval(Container.DataItem, "ResultID"))
                                                : string.Empty%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Proccess Date" UniqueName="LastUpdateDateTime">
                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Left"
                                    HeaderText="Last Error Process" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ErrorResponse")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="False">
                            <Selecting AllowRowSelect="False" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </NestedViewTemplate>
        </MasterTableView>

        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="False">
            <Resizing AllowColumnResize="False" />
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
