<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="ServiceUnitBookingStatusList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitBookingStatusList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">
            function rowProcess(bno, status) {
                if (status == 'val') {
                    if (confirm('Are you sure to validate this selected transaction?')) {
                        __doPostBack("<%= grdList.UniqueID %>", bno + '|' + status);
                    }
                }
                else 
                    __doPostBack("<%= grdList.UniqueID %>", bno + '|' + status);
            }
            function openWinDialog(bno) {
                var oWnd = $find("<%= winUpdate.ClientID %>");
                oWnd.SetUrl("../ServiceUnitBookingRealization/ServiceUnitBookingRealizationDetail.aspx?id=" + bno + "&t=st");
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinDialogCancel(bno) {
                var oWnd = $find("<%= winUpdate.ClientID %>");
                oWnd.SetUrl("VoidDialog.aspx?id=" + bno);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                __doPostBack("<%= grdList.UniqueID %>", "rebind");
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRoomID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterBookingID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winUpdate" Animation="None" Width="1000px" Height="680px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Booking Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtBookingDate" runat="server" Width="100px" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>

                        <tr runat="server">
                            <td class="label">
                                <asp:Label ID="lblRoomID" runat="server" Text="Operating Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboRoomID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRoomID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBookingNo" runat="server" Text="Booking No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBookingNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterBookingID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboStatus" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" Height="560px"
        AutoGenerateColumns="False" GridLines="None" OnItemCommand="grdList_OnItemCommand" OnItemDataBound="grdList_OnItemDataBound"
        AllowSorting="true">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="BookingNo" ShowHeader="True" HierarchyDefaultExpanded="true">
            <NestedViewTemplate>
                <div style="padding-left: 20px; width: 98%">
                    <telerik:RadTabStrip ID="tabsBooking" runat="server" MultiPageID="mpEpisodeProcedure" ShowBaseLine="true"
                        Align="Left" PerTabScrolling="True" Width="100%"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Actual Operating Activities" PageViewID="pgTimeStamp"
                                Selected="True" />
                            <telerik:RadTab runat="server" Text="Booking Schedule Information" PageViewID="pgInfo" />
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="mpEpisodeProcedure" runat="server" SelectedIndex="0" ScrollBars="Auto" Width="100%"
                        CssClass="multiPage">
                        <telerik:RadPageView ID="pgTimeStamp" runat="server">
                            <telerik:RadGrid ID="grdListTimeStamp" runat="server"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="BookingNo" ShowHeader="True">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="PreSurgery" HeaderText="Pre Surgery">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsValidate").Equals(false) ? string.Empty : (DataBinder.Eval(Container.DataItem, "IsPreSurgery").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "PreSurgeryDateTimes"))
                                                        : string.Format("<a href=\"#\" onclick=\"rowProcess('{0}', '1'); return false;\"><img src=\"../../../../Images/start.png\" border=\"0\" title=\"Pre Surgery\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "BookingNo"))))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Anesthesia" HeaderText="Anesthesia">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsValidate").Equals(false) ? string.Empty : (DataBinder.Eval(Container.DataItem, "IsAnesthesia").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "AnesthesiaDateTimes"))
                                                        : string.Format("<a href=\"#\" onclick=\"rowProcess('{0}', '2'); return false;\"><img src=\"../../../../Images/start.png\" border=\"0\" title=\"Anesthesia\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "BookingNo"))))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Surgery" HeaderText="Surgery">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsValidate").Equals(false) ? string.Empty : (DataBinder.Eval(Container.DataItem, "IsSurgery").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "SurgeryDateTimes"))
                                                        : string.Format("<a href=\"#\" onclick=\"rowProcess('{0}', '3'); return false;\"><img src=\"../../../../Images/start.png\" border=\"0\" title=\"Surgery\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "BookingNo"))))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="PostSurgery" HeaderText="Post Surgery">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsValidate").Equals(false) ? string.Empty : (DataBinder.Eval(Container.DataItem, "IsPostSurgery").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "PostSurgeryDateTimes"))
                                                        : string.Format("<a href=\"#\" onclick=\"rowProcess('{0}', '4'); return false;\"><img src=\"../../../../Images/start.png\" border=\"0\" title=\"Post Surgery\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "BookingNo"))))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="MoveToTheWard" HeaderText="Move To The Ward">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsValidate").Equals(false) ? string.Empty : (DataBinder.Eval(Container.DataItem, "IsMoveToTheWard").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "MoveToTheWardDateTimes"))
                                                        : string.Format("<a href=\"#\" onclick=\"rowProcess('{0}', '5'); return false;\"><img src=\"../../../../Images/start.png\" border=\"0\" title=\"Move To The Ward\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "BookingNo"))))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="False">
                                    <Selecting AllowRowSelect="False" />
                                </ClientSettings>
                            </telerik:RadGrid>
                            <br />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pgInfo" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%; vertical-align: top">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblBookingDate" runat="server" Text="Booking Date/Time"></asp:Label>
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtBookingDateFrom" runat="server" Enabled="false" Width="110px"
                                                                    SelectedDate='<%#Eval("BookingDateTimeFrom")%>' DateInput-DateFormat="dd-MMM-yyyy">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="txtBookingTimeFrom" runat="server" Enabled="false" Width="80px"
                                                                    TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                    TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" SelectedDate='<%#Eval("BookingDateTimeFrom")%>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblRoomBooking" runat="server" Text="Room Booking"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtRoomBooking" runat="server" Text='<%#Eval("RoomName")%>' Width="100%" ReadOnly="true"  />
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblDiagnose" runat="server" Text="Pre Diagnosis"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtDiagnose" runat="server" Text='<%#Eval("Diagnose")%>' Width="100%" Height="45px"
                                                        TextMode="MultiLine" ReadOnly="true"/>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblProcedure" runat="server" Text="Primary Procedure"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtProcedure" runat="server" Text='<%#Eval("Procedure")%>' Width="100%" Height="45px"
                                                        TextMode="MultiLine" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblProcedure2" runat="server" Text="Secondary Procedure"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtProcedure2" runat="server" Text='<%#Eval("Procedure2")%>' Width="100%" Height="45px"
                                                        TextMode="MultiLine" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblAnestheticType" runat="server" Text="Anesthetic Type"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtAnestheticType" runat="server" Text='<%#Eval("AnestheticType")%>' Width="100%" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                </td>
                                                <td class="entry">
                                                     <asp:CheckBox ID="chkIsCito" runat="server" Text="Cito" Checked='<%#Eval("IsCito")%>' Enabled="false" />
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%; vertical-align: top">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblSurgion" runat="server" Text="Surgion"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                     <telerik:RadTextBox ID="txtSurgion" runat="server" Text='<%#Eval("Surgion")%>' Width="100%" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblAnesthetist" runat="server" Text="Anesthesiologist"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                     <telerik:RadTextBox ID="txtAnesthetist" runat="server" Text='<%#Eval("Anesthetist")%>' Width="100%" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblAssistentAnesthetist" runat="server" Text="Assistant Anesthetist"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                     <telerik:RadTextBox ID="txtAssistentAnesthetist" runat="server" Text='<%#Eval("AssistentAnesthetist")%>' Width="100%" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblInstrumentator1" runat="server" Text="Instrumentator"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                     <telerik:RadTextBox ID="txtInstrumentator" runat="server" Text='<%#Eval("Instrumentator1")%>' Width="100%" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblAssistant" runat="server" Text="Assistant"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                     <telerik:RadTextBox ID="txtAssistant" runat="server" Text='<%#Eval("Assistent")%>' Width="100%" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtNotes" runat="server" Text='<%#Eval("Notes")%>' Width="100%" Height="45px"
                                                        TextMode="MultiLine" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblVoidReason" runat="server" Text="Void Reason"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtVoidReason" runat="server" Text='<%#Eval("VoidReason")%>' Width="100%" Height="45px"
                                                        TextMode="MultiLine" ReadOnly="true"/>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </NestedViewTemplate>
            <Columns>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Operating Room" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BookingNo" HeaderText="Booking No" UniqueName="BookingNo"
                    SortExpression="BookingNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    AllowSorting="false" />
                <telerik:GridBoundColumn DataField="BookingDateTimeFrom" HeaderText="Date" UniqueName="BookingDateTimeFrom"
                    SortExpression="BookingDateTimeFrom" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
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
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsValidate"
                    HeaderText="Validate" UniqueName="IsValidate" SortExpression="IsValidate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApproved"
                    HeaderText="Approved" UniqueName="IsApproved" SortExpression="IsApproved"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid"
                    HeaderText="Void" UniqueName="IsVoid" SortExpression="IsVoid"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderStyle-Width="10px" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsValidate").Equals(true) ? string.Empty : string.Format("<a href=\"#\" onclick=\"rowProcess('{0}', 'val'); return false;\"><img src=\"../../../../Images/Toolbar/post_green_16.png\" border=\"0\" title=\"Validate\" /></a>",
                                DataBinder.Eval(Container.DataItem, "BookingNo"))) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) ? string.Empty : string.Format("<a href=\"#\" onclick=\"openWinDialog('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "BookingNo"))) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || 
                                DataBinder.Eval(Container.DataItem, "IsEpisodeProcedure").Equals(true) || DataBinder.Eval(Container.DataItem, "IsOperatingNotes").Equals(true) || 
                                DataBinder.Eval(Container.DataItem, "IsPatientHealthRecord").Equals(true) ? string.Empty : string.Format("<a href=\"#\" onclick=\"openWinDialogCancel('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/cancel16.png\" border=\"0\" title=\"Cancel\" /></a>",
                                DataBinder.Eval(Container.DataItem, "BookingNo"))) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
