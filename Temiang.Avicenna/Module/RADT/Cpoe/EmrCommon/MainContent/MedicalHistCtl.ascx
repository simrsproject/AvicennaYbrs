<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedicalHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MainContent.MedicalHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.EmrIp" %>
<telerik:RadCodeBlock ID="radCodeBlock" runat="server">

    <script type="text/javascript">
        function deletePrescription(prescNo) {
            // Akan dipanggil dari script yg digenerate pada codebehind
            if (confirm("Void this prescription (" + prescNo + "). Are you sure?")) {
                var masterTable = $find("<%= grdDiagAndPrescription.ClientID %>").get_masterTableView();
                masterTable.fireCommand('VoidPrescription', prescNo);
            }
        }
        function printPrescription(prescNo) {
            // Akan dipanggil dari script yg digenerate pada codebehind
            var masterTable = $find("<%= grdDiagAndPrescription.ClientID %>").get_masterTableView();
            masterTable.fireCommand('Print', prescNo);
        }
        function verifyPrescription(prescNo) {
            if (!confirm("Verify this Prescription No: " + prescNo + ". Continue ?")) return false;
            // Akan dipanggil dari script yg digenerate pada codebehind
            var masterTable = $find("<%= grdDiagAndPrescription.ClientID %>").get_masterTableView();
            masterTable.fireCommand('Verify', prescNo);
        }

        function refreshGridDiagAndPres() {
            // Lebih cepat responsenya dibanding menggunakan asp:LinkButton CommandName="refresh" (Handono 231204)
            var masterTable = $find("<%= grdDiagAndPrescription.ClientID %>").get_masterTableView();
            masterTable.rebind();
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManagerProxy ID="ajxpMedHist" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdDiagAndPrescription">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdDiagAndPrescription" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadGrid ID="grdDiagAndPrescription" runat="server" EnableViewState="False" Height="560px"
    OnNeedDataSource="grdDiagAndPrescription_NeedDataSource" OnItemCommand="grdDiagAndPrescription_ItemCommand"
    AutoGenerateColumns="False" GridLines="None">
    <MasterTableView DataKeyNames="RegistrationNo" ShowHeader="false" CommandItemDisplay="Top">
        <CommandItemTemplate>
            <div>
                <div class="l">
                    <%#IsUserAddAble? string.Format("<a href=\"#\" onclick=\"javascript:entryPrescription('new', '', ''); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Prescription</a>", Helper.UrlRoot()):string.Format("<img src=\"{0}/Images/Toolbar/new16_d.png\"  alt=\"New\" />&nbsp;Add Prescription",Helper.UrlRoot())%>
                    &nbsp;&nbsp;
                    <%#string.Format("<a href=\"#\" onclick=\"javascript:directPrescription(); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Direct Prescription</a>", Helper.UrlRoot())%>
                </div>
                <div class="r">
                    <%--                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="refresh" ImageUrl="~/Images/Toolbar/refresh16.png">
                                            <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" alt=""/>&nbsp;Refresh&nbsp;&nbsp;
                    </asp:LinkButton>--%>
                    <a href="#" onclick="javascript:refreshGridDiagAndPres();return false;">
                        <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" alt="" />&nbsp;Refresh&nbsp;&nbsp;</a>
                </div>

            </div>
        </CommandItemTemplate>
        <CommandItemStyle Height="29px" />
        <Columns>
            <telerik:GridTemplateColumn UniqueName="Prescription" HeaderText="">
                <ItemStyle VerticalAlign="Top"></ItemStyle>
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Prescription")%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="Diagnosis" HeaderText="" HeaderStyle-Width="40%">
                <ItemStyle VerticalAlign="Top"></ItemStyle>

                <ItemTemplate>
                    <fieldset>
                        <legend>Registration</legend>
                        <table>
                            <tr>
                                <td>No</td>
                                <td>:</td>
                                <td><%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%></td>
                            </tr>
                            <tr>
                                <td>Date</td>
                                <td>:</td>
                                <td><%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(AppConstant.DisplayFormat.Date)%></td>
                            </tr>

                            <tr>
                                <td>Psycian</td>
                                <td>:</td>
                                <td><%#DataBinder.Eval(Container.DataItem, "ParamedicName")%></td>
                            </tr>
                        </table>
                    </fieldset>

                    <%#DataBinder.Eval(Container.DataItem, "ICD10")%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>

        </Columns>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
</telerik:RadGrid>


