<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="IncompleteMedicalRecordFileList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.IncompleteMedicalRecordFileList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoViewUrl(regno, id) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("IncompleteMedicalRecordFileDialog.aspx?regno=" + regno + "&id=" + id);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winProcess" Animation="None" Width="1000px" Height="600px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSubmitDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSubmitDate" runat="server" Text="Submit Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtSubmitDate" runat="server" Width="110px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnFilterSubmitDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
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
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboParamedicID_ItemDataBound"
                                    OnItemsRequested="cboParamedicID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 10 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
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
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView Name="master" DataKeyNames="RegistrationNo, TxId" ClientDataKeyNames="RegistrationNo, TxId"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/edit.png\" border=\"0\" title=\"Return\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "TxId"))) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="SubmitDate"
                    HeaderText="Submit Date" UniqueName="SubmitDate" SortExpression="SubmitDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="TransactionDate"
                    HeaderText="Analysis Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RegistrationDate"
                    HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="RegistrationTime" HeaderText="Time"
                    UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="DischargeDate" HeaderText="Discharge Date"
                    UniqueName="DischargeDate" SortExpression="DischargeDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DischargeTime" HeaderText="Time"
                    UniqueName="DischargeTime" SortExpression="DischargeTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Gender"
                    UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ParamedicName" HeaderText="Physician"
                    UniqueName="ParamedicName" SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="TxId" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Submit" Name="Submit" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Return" Name="Return" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="SubmitDate" HeaderText="Date"
                            UniqueName="SubmitDate" SortExpression="SubmitDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Submit" />
                        <telerik:GridBoundColumn HeaderStyle-Width="550px" DataField="SubmitNotes" HeaderText="Notes"
                            UniqueName="SubmitNotes" SortExpression="SubmitNotes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Submit" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SubmitBy" HeaderText="By"
                            UniqueName="SubmitBy" SortExpression="SubmitBy" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Submit" />

                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ReturnDate" HeaderText="Date"
                            UniqueName="ReturnDate" SortExpression="ReturnDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Return" />
                        <telerik:GridBoundColumn HeaderStyle-Width="550px" DataField="ReturnNotes" HeaderText="Notes"
                            UniqueName="ReturnNotes" SortExpression="ReturnNotes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Return" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ReturnBy" HeaderText="By"
                            UniqueName="ReturnBy" SortExpression="ReturnBy" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Return" />
                        <telerik:GridTemplateColumn />
                    </Columns>

                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
