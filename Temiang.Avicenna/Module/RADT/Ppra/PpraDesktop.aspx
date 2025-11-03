<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="PpraDesktop.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Ppra.PpraDesktop" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">

            function openGyssensProccess(patid, regno) {
                openWindowMaxScreen("Gyssens/GyssensProccess.aspx?patid=" + patid + "&regno=" + regno)
            }
            function openWinEntryMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                openWindow(url, width - 40, height - 40);
            }
            function openWindowMaxScreen(url) {
                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }

            function openWindow(url, width, height) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();
                oWnd.show();
            }

            function openMedicationHist(patid, regno, fregno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid=' + patid + '&regno=' + regno + '&fregno=' + fregno;
                openWindowMaxScreen(url);
            }
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPrescriptionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="loadPanel" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Maximize, Close,Move" Modal="True" ShowOnTopWhenMaximized="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>

    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="510px" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td width="304px">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="304px" AllowCustomText="true"
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
                            <td>
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
                            <td>
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="450px" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">Include</td>
                            <td width="304px">
                                <asp:CheckBox ID="chkIncludeNotClosed" runat="server" Text="Not Closed" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkIncludeNotDischarged" runat="server" Text="Not Discharged Patients" />
                            </td>
                            <td style="text-align: left;"></td>
                        </tr>
                        <tr>
                            <td class="label">Prescription Period
                            </td>
                            <td width="304px">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="cboMonth" runat="server" Width="125px" />
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboYear" runat="server" Width="125px" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnPrescriptionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="PatientID, RegistrationNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="RegistrationDate" HeaderText="Reg. Date">
                    <ItemTemplate>
                        <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(AppConstant.DisplayFormat.DateShortMonth) %><br />
                        <%#DataBinder.Eval(Container.DataItem, "RegistrationTime") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="RegistrationNo" HeaderText="MRN / Reg No">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "MedicalNo")  %><br />
                        <%#DataBinder.Eval(Container.DataItem, "RegistrationNo") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="240px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <span style="font-size: 12pt;"><%# string.Format("{0} {1}",
                                DataBinder.Eval(Container.DataItem, "SalutationName"),
                                DataBinder.Eval(Container.DataItem, "PatientName")
                                )%>
                            <br />
                            <%# string.Format("{0}Y {1}M {2}D", Helper.GetAgeInYear (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), Helper.GetAgeInMonth(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), Helper.GetAgeInDay(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date))) %>
                            <br />
                            <%#DataBinder.Eval(Container.DataItem, "GuarantorName") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReferralGroupName" HeaderText="Referral Group" UniqueName="ReferralGroupName"
                    SortExpression="ReferralGroupName">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="RoomName" HeaderText="Room / Bed" SortExpression="RoomName">
                    <ItemTemplate>
                        R: &nbsp; <%#DataBinder.Eval(Container.DataItem, "RoomName")  %><br />
                        B: &nbsp;<%#DataBinder.Eval(Container.DataItem, "BedID") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="DischargeDate" HeaderText="Discharge Date" UniqueName="DischargeDate"
                    SortExpression="DischargeDate">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn UniqueName="Menu" HeaderText=" ">
                    <ItemTemplate>
                        <%# !DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals(AppConstant.RegistrationType.InPatient)? string.Empty: string.Format("<a href=\"#\" onclick=\"openMedicationHist('{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/history16.png\" border=\"0\" alt=\"MedHist\" title=\"Service Unit Kardex\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"), 
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                            DataBinder.Eval(Container.DataItem, "FromRegistrationNo"))%>
                        &nbsp;
                        <%# string.Format("<a href=\"#\" onclick=\"javascript:openGyssensProccess('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Gyssens\" title=\"Gyssens Observation Entry\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    <ItemTemplate>
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
