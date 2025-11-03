<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedicalHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.MedicalHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadCodeBlock ID="radCodeBlock" runat="server">

    <script type="text/javascript">
        function deletePrescription(prescNo) {
            // Akan dipanggil dari script yg digenerate pada codebehind
            if (confirm("Delete prescription " + prescNo + ". Are you sure?")) {
                var masterTable = $find("<%= grdDiagAndPrescription.ClientID %>").get_masterTableView();
                masterTable.fireCommand('DeletePrescription', prescNo);
            }
        }
        function printPrescription(prescNo) {
            // Akan dipanggil dari script yg digenerate pada codebehind
            var masterTable = $find("<%= grdDiagAndPrescription.ClientID %>").get_masterTableView();
            masterTable.fireCommand('Print', prescNo);
        }
    </script>
</telerik:RadCodeBlock>

<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdDiagAndPrescription">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdDiagAndPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdPastMedicalHist">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdPastMedicalHist" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table style="width: 100%;" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 60%; vert-align: top">
            <telerik:RadGrid ID="grdDiagAndPrescription" runat="server" EnableViewState="False" Height="560px"
                OnNeedDataSource="grdDiagAndPrescription_NeedDataSource" OnItemCommand="grdDiagAndPrescription_ItemCommand"
                AutoGenerateColumns="False" GridLines="None">
                <MasterTableView DataKeyNames="RegistrationNo" ShowHeader="True" CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        <div>
                            <div class="l">
                                <%# (IsUserParamedicCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/new16_d.png\" />&nbsp;Add Prescription</a>" : string.Format("<a href=\"#\" onclick=\"javascript:entryPrescription('new', '', ''); return false;\"><img src=\"../../../Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Prescription</a>"))%>
                            </div>
                            <div class="r">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Rebind" ImageUrl="~/Images/Toolbar/refresh16.png">
                                            <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" alt=""/>&nbsp;Refresh&nbsp;&nbsp;
                                </asp:LinkButton>
                            </div>
                        </div>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Prescription" HeaderText="Prescription">
                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Prescription")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Diagnosis" HeaderText="Diagnosis" HeaderStyle-Width="40%">
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

                                <fieldset>
                                    <legend>Diagnosis</legend>
                                    <%#DataBinder.Eval(Container.DataItem, "Diagnosis")%>
                                    <%#DataBinder.Eval(Container.DataItem, "ICD10")%>
                                </fieldset>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="False">
                    <Selecting AllowRowSelect="False" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
            </telerik:RadGrid>

        </td>
        <td style="width: 4px">&nbsp;</td>
        <td style="width: 40%;" valign="top">
            <telerik:RadGrid ID="grdPastMedicalHist" runat="server" EnableViewState="False" Height="560px"
                OnNeedDataSource="grdPastMedicalHist_NeedDataSource" OnItemCommand="grdPastMedicalHist_ItemCommand" OnItemCreated="grdPastMedicalHist_ItemCreated"
                AutoGenerateColumns="False" GridLines="None">
                <MasterTableView ShowHeader="False" CommandItemDisplay="Top" ShowHeadersWhenNoRecords="False" EnableGroupsExpandAll="True" GroupsDefaultExpanded="True">
                    <CommandItemTemplate>
                        <div>
                            <div class="l" style="font-weight: bold;">
                                &nbsp;Medical History
                                        
                            </div>
                            <div class="r">
                                <%--                                <%# IsUserCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/edit16_d.png\" />&nbsp;Edit</a>" : 
                                            "<a href=\"#\" onclick=\"javascript:entryPastMedHist(); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\"  alt=\"Edit\" />&nbsp;Edit</a>"%>
&nbsp;&nbsp;--%>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="refresh" ImageUrl="~/Images/Toolbar/refresh16.png">
                                            <img src="../../../Images/Toolbar/refresh16.png" alt=""/>&nbsp;&nbsp;
                                </asp:LinkButton>
                            </div>
                        </div>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <GroupHeaderItemStyle Wrap="True" Height="25px" Font-Bold="True" Font-Size="9"/>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="GroupName" SortOrder="Ascending" />
                            </GroupByFields>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="GroupName" HeaderText="." />
                            </SelectFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Notes" HeaderText="">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "PastMedical")%>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="False">
                    <Selecting AllowRowSelect="False" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>

    </tr>
</table>
