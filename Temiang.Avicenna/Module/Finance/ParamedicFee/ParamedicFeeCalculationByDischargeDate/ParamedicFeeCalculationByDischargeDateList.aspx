<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeCalculationByDischargeDateList.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeCalculationByDischargeDateList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            //            function onClientClose(oWnd, args) {
            //                if (oWnd.argument) {
            //                    if (oWnd.argument == 'rebind') {
            //                        __doPostBack("<%= grdList.UniqueID %>", 'rebind');
            //                        oWnd.argument = 'undefined';
            //                    }
            //                }
            //            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "process":
                        __doPostBack("<%= grdList.UniqueID %>", 'rebind');
                        break;
                    case "refresh":
                        __doPostBack("<%= grdList.UniqueID %>", 'refresh');
                        break;
                    case "print":
                        __doPostBack("<%= grdList.UniqueID %>", 'print');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process" Value="process" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print (Draft)" Value="print" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <%--<telerik:RadWindow ID="winProcess" Animation="None" Width="600px" Height="300px"
        Title="Processing Periode" runat="server" Behavior="Close,Move" ShowContentDuringLoad="false"
        VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>--%>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDatePeriode" runat="server" Text="Period"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDatePeriode1" runat="server" Width="100px" />
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDatePeriode2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="304px" HighlightTemplatedItems="True"
                                    MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboParamedicID_ItemDataBound"
                                    OnItemsRequested="cboParamedicID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 15 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
        <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
            OnItemDataBound="grdList_ItemDataBound"
            GridLines="None" AutoGenerateColumns="false" AllowSorting="true">
            <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo"
                GroupLoadMode="Client">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Physician "></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit ">
                            </telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <telerik:GridCheckBoxColumn DataField="IsModified" HeaderText="IsModified" UniqueName="IsModified"
                                SortExpression="IsModified" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                        UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="DischargeDate" HeaderText="Discharge Date" UniqueName="DischargeDate"
                        SortExpression="DischargeDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                        SortExpression="PatientName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                        SortExpression="MedicalNo">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                        SortExpression="RegistrationNo">
                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ToServiceUnitName" HeaderText="To Unit" UniqueName="ToServiceUnitName"
                        SortExpression="ToServiceUnitName">
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Transaction Date"
                        UniqueName="TransactionDate" SortExpression="TransactionDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ParamedicFee" HeaderText="Initial Fee" UniqueName="ParamedicFee"
                        SortExpression="ParamedicFee" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DeductionAmount" HeaderText="Deduction" UniqueName="DeductionAmount"
                        SortExpression="DeductionAmount" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FeeAmount" HeaderText="Calculated Fee" UniqueName="FeeAmount"
                        SortExpression="FeeAmount" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="LastCalculatedDateTime" HeaderText="Calculated Date"
                        UniqueName="LastCalculatedDateTime" SortExpression="LastCalculatedDateTime">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="true">
                <Resizing AllowColumnResize="True" />
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
