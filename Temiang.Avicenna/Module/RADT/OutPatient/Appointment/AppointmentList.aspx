<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AppointmentList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.AppointmentList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function openWinSchedule() {
                var oWnd = $find("<%= winSelectTime.ClientID %>");
                var cboParamedicID = $find("<%= cboParamedicID.ClientID %>");
                var cboServiceUnitID = $find("<%= cboClusterID.ClientID %>");
                var appDate = $find("<%= txtAppointmentDate.ClientID %>");
                var pmID = cboParamedicID.get_value();
                var suID = cboServiceUnitID._value;

                var date = appDate.get_selectedDate().format('MM/dd/yyyy');
                var url = "AppointmentSelectTimeReadOnly.aspx?pmID=" + pmID + "&suID=" + suID + "&pDate=" + date;
                oWnd.setUrl(url);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinImport(programID) {
                var unit = $find("<%= cboClusterID.ClientID %>");
                if (unit.get_value() == '') {
                    alert("Service unit is not selected.");
                    return;
                }
                var medic = $find("<%= cboParamedicID.ClientID %>");
                if (medic.get_value() == '') {
                    alert("Physician is not selected.");
                    return;
                }

                var oWnd = $find("<%= winImport.ClientID %>");
                oWnd.setUrl("AppointmentImportExcelDialog.aspx?at=" + '<%= Request.QueryString["at"] %>' + "&unit=" + unit.get_value() + "&medic=" + medic.get_value());
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.id != null) __doPostBack("<%= grdList.UniqueID %>", "rebind");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="600px" ReloadOnShow="true"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Behavior="Close"
        DestroyOnClose="false" ID="winSelectTime" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="350px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" ID="winImport" OnClientClose="onClientClose" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAppointmentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchCluster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image1" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAppointmentDate" runat="server" Text="Appointment Date"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtAppointmentDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnAppointmentDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClusterID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboClusterID" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchCluster" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician ID"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnPhysicianSchedule" runat="server" Text="Available Time" OnClientClick="javascript:openWinSchedule();return false;"
                                    Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="50">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFilterGuarantor" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="AppointmentNo" GroupLoadMode="Client">
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
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="AppointmentNo" HeaderText="Appointment No"
                    UniqueName="AppointmentNo" SortExpression="AppointmentNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="AppointmentDate"
                    HeaderText="Date" UniqueName="AppointmentDate" SortExpression="AppointmentDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="AppointmentTime" HeaderText="Time"
                    UniqueName="AppointmentTime" SortExpression="AppointmentTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="VisitTypeName" HeaderText="Visit Type" UniqueName="VisitTypeName"
                    SortExpression="VisitTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="VisitDuration" HeaderText="Duration"
                    UniqueName="VisitDuration" SortExpression="VisitDuration" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Status" UniqueName="ItemName"
                    HeaderStyle-Width="100px" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                    SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor Name" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                    HeaderText="UserID" UniqueName="UserID" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
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
