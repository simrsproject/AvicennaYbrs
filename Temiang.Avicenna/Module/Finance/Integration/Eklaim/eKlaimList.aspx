<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="eKlaimList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Integration.Eklaim.eKlaimList" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(regno, isnew) {
                var md = ''
                if (isnew) md = 'new'
                else md = 'edit'

                var oWnd = $find("<%= winRujukan.ClientID %>");

                <% if (AppSession.Parameter.IsiDRGIntegration) { %>
                        oWnd.setUrl('iDRGDetail.aspx?md=' + md + '&regNo=' + regno);
                <% } else { %>
                    oWnd.setUrl('eKlaimDetail.aspx?md=' + md + '&regNo=' + regno);
                <% } %>
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind|" + oWnd.argument.mode);
                    oWnd.argument = '|';
                }
            }

            //function openPrint(sepno) {
            //    if (sepno == '') return;
            //    window.open('eKlaimPdf.aspx?sepno=' + sepno);
            //}

            function openPrint(sepno) {
                url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=eklaim&trno=" + sepno;
                //openWinEntryMaximize(url);
                //window.open(url);
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title('Print');
                oWnd.show();
                oWnd.maximize();
            }

            function OnSuccessCall(response) {
                alert(response.d);
            }

            function OnErrorCall(response) {
                alert(response.status + " " + response.statusText);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterNoReg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterNoKartu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTglReg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterJenisPelayanan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterNoSep">
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
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="500px" VisibleStatusbar="true" Modal="true" ID="winRujukan"
        OnClientClose="onClientClose" />
    <telerik:RadWindow ID="winDialog" Width="700px" Height="300px" runat="server" Behaviors="Close,Move" Modal="True">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table>
                        <tr>
                            <td class="label">No. MR
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNoMR" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterNoReg" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Nama Pasien
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">No. Kartu BPJS
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNoKartu" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterNoKartu" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">No. NIK
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNoNik" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterNoNik" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table>
                        <tr>
                            <td class="label" style="padding-left: 0px">
                                <asp:RadioButtonList ID="rblTglPulang" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Tgl Masuk" Value="01" Selected="True" />
                                    <asp:ListItem Text="Tgl Pulang" Value="02" />
                                </asp:RadioButtonList>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="txtStart" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="txtEnd" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="75px">
                                <asp:ImageButton ID="btnFilterTglReg" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />&nbsp;
                                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnExport_Click" ToolTip="Export" />
                                <asp:ImageButton ID="btnExportLog" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnExportLog_OnClick" ToolTip="Export Log Otomatisasi" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Jenis Pelayanan
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboJenisPelayanan" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterJenisPelayanan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
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
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnItemCommand="grdList_ItemCommand" GridLines="None" AutoGenerateColumns="false"
        AllowPaging="true" PageSize="50" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="PatientID,MedicalNo" ClientDataKeyNames="PatientID,MedicalNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}','{1}');\"><img src=\"../../../../Images/Toolbar/{2}16.png\" border=\"0\" title=\"{3}\" /></a>",
                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "Status"), 
                            Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Status")) ? "new" : "edit", 
                            Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Status")) ? "New" : "Edit")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Print" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CbgStatus").Equals("final") ? string.Format("<a href=\"#\" onclick=\"openPrint('{0}');\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print\" /></a>",
                            DataBinder.Eval(Container.DataItem, "BpjsSepNo")) : string.Empty%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsNewPatient" HeaderText="New Patient"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsNewPatient" SortExpression="IsNewPatient"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="No. MR"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="MedicalNo" SortExpression="MedicalNo"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Nama Pasien" UniqueName="PatientName"
                    SortExpression="PatientName" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateOfBirth" HeaderText="Tgl. Lahir"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="DateOfBirth" SortExpression="DateOfBirth"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="GuarantorCardNo" HeaderText="No. Kartu BPJS"
                    UniqueName="GuarantorCardNo" SortExpression="GuarantorCardNo" HeaderStyle-Width="110px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="BpjsSepNo" HeaderText="No. SEP" UniqueName="BpjsSepNo"
                    SortExpression="BpjsSepNo" HeaderStyle-Width="140px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                    HeaderText="Tgl. Masuk" HeaderStyle-HorizontalAlign="Center" UniqueName="RegistrationDate"
                    SortExpression="RegistrationDate" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DischargeDate" HeaderText="Tgl. Pulang"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="DischargeDate" SortExpression="DischargeDate"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Poli / Ruangan"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="DPJP" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Instansi Penjamin"
                    UniqueName="GuarantorName" SortExpression="GuarantorName" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CbgID" HeaderText="CBG"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CbgID" SortExpression="CbgID" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CbgStatus" HeaderText="CBG Status"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CbgStatus" SortExpression="CbgStatus"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CbgSentStatus" HeaderText="Sent Status"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CbgSentStatus" SortExpression="CbgSentStatus"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="RegistrationNo" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="15">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="No Reg."
                            HeaderStyle-HorizontalAlign="Center" UniqueName="RegistrationNo" SortExpression="RegistrationNo"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                            HeaderText="Tgl. Masuk" HeaderStyle-HorizontalAlign="Center" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DischargeDate" HeaderText="Tgl. Pulang"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="DischargeDate" SortExpression="DischargeDate"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="BpjsSepNo" HeaderText="No. SEP" UniqueName="BpjsSepNo"
                            SortExpression="BpjsSepNo" HeaderStyle-Width="140px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Penjamin" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Poli / Ruangan"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="DPJP" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Total" HeaderText="Klaim (Rp.)"
                            UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BillingAmount" HeaderText="Billing (Rp.)"
                            UniqueName="BillingAmount" SortExpression="BillingAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CbgID" HeaderText="CBG"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="CbgID" SortExpression="CbgID" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CbgStatus" HeaderText="CBG Status"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="CbgStatus" SortExpression="CbgStatus"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CbgSentStatus" HeaderText="Sent Status"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="CbgSentStatus" SortExpression="CbgSentStatus"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
