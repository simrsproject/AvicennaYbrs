<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ProcessDataToPCare.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.ProcessDataToPCare" %>

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
                    case "changepassword":
                        openChangePassword();
                        args.set_cancel(true); // Cegah postback
                        break;
                }
            }

            function openChangePassword() {
                var oWnd = $find("<%= winInfo.ClientID %>");
                var url = '<%=Page.ResolveUrl("~/Module/ControlPanel/Setting/Parameter/ParameterDetail.aspx?md=edit&id=PCarePassword&isrm=1")%>';
                oWnd.setUrl(url);
                oWnd.setSize(600, 450);
                oWnd.center();
                oWnd.show();
            }

            function openWinBpjsInfo(patientID, guarantorCardNo) {
                var oWnd = $find("<%= winInfo.ClientID %>");
                var url = '<%=Page.ResolveUrl("~/BpjsCommon/BpjsMemberInfo.aspx")%>?bpjsno=' + guarantorCardNo + '&regno=&patientid=' + patientID;
                // JANGAN PAKAI radopen,  urlnya harus lengkap dgn rootnya jika pakai radopen
                oWnd.setUrl(url);
                oWnd.setSize(1040, 450);
                oWnd.center();
                oWnd.show();
            }

            function CloseStatus(registrationNo) {
                if (confirm("Close this pending PCare data?"))
                    __doPostBack("<%= grdRegisteredList.UniqueID %>", "closestatus|" + registrationNo);
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
            <telerik:AjaxSetting AjaxControlID="grdRegisteredList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winInfo" Width="1000px" Height="450px" runat="server" Modal="true" VisibleStatusbar="false"
        DestroyOnClose="false" Behavior="Close, Move" ReloadOnShow="True" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Send Data To PCare For Selected Registration" Value="process" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Change PCare Password" Value="changepassword" ImageUrl="~/Images/Toolbar/refresh16.png"
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
                                <asp:CheckBox runat="server" ID="chkIsIncludeNotClosed" Text="Show with Registration Not Closed" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
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
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table width="100%">
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
        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowMultiRowSelection="true">
        <MasterTableView DataKeyNames="RegistrationNo" GroupLoadMode="Server">
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
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Gender"
                    UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn DataField="IsSOAP" HeaderText="SOAP"
                    UniqueName="IsSOAP" SortExpression="IsSOAP">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsPrescription" HeaderText="Presc."
                    UniqueName="IsPrescription" SortExpression="IsPrescription">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsICD" HeaderText="ICD-10"
                    UniqueName="IsICD" SortExpression="IsICD">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="BPJS No" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <%#string.Format("<a onclick=\"openWinBpjsInfo('{0}','{1}')\">{1}</a>",  DataBinder.Eval(Container.DataItem,"PatientID"),  DataBinder.Eval(Container.DataItem,"GuarantorCardNo")==DBNull.Value?"Update":DataBinder.Eval(Container.DataItem,"GuarantorCardNo")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ErrorResponse" HeaderText="Last Error Process" UniqueName="ErrorResponse"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
