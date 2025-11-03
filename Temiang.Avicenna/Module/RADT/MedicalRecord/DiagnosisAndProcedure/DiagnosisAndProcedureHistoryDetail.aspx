<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DiagnosisAndProcedureHistoryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DiagnosisAndProcedureHistoryDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
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
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="Episode Diagnosis" PageViewID="pgDiagnose" Selected="True" />
            <telerik:RadTab runat="server" Text="Episode Procedure" PageViewID="pgProcedure" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray"
        SelectedIndex="0">
        <telerik:RadPageView ID="pgDiagnose" runat="server">
            <telerik:RadGrid ID="grdEpisodeDiagnose" runat="server" OnNeedDataSource="grdEpisodeDiagnose_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowSorting="true">
                <MasterTableView DataKeyNames="RegistrationNo, SequenceNo" CommandItemDisplay="None">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="GroupName" HeaderText="Registration Date " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="RegistrationDate" SortOrder="Descending" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="DiagnoseID" HeaderText="Diagnosis ID"
                            UniqueName="DiagnoseID" SortExpression="DiagnoseID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnosis Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DiagnoseType" HeaderText="Diagnosis Type" UniqueName="DiagnoseType"
                            SortExpression="DiagnoseType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn  HeaderStyle-Width="200px" DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAcuteDisease" HeaderText="Acute"
                            UniqueName="IsAcuteDisease" SortExpression="IsAcuteDisease" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsChronicDisease"
                            HeaderText="Chronic" UniqueName="IsChronicDisease" SortExpression="IsChronicDisease"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsOldCase" HeaderText="Old Case"
                            UniqueName="IsOldCase" SortExpression="IsOldCase" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsConfirmed" HeaderText="Confirmed"
                            UniqueName="IsConfirmed" SortExpression="IsConfirmed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgProcedure" runat="server">
            <telerik:RadGrid ID="grdEpisodeProcedure" runat="server" OnNeedDataSource="grdEpisodeProcedure_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowSorting="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="GroupName" HeaderText="Registration No " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="RegistrationDate" SortOrder="Descending" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcedureDate" HeaderText="Date"
                            UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcedureTime" HeaderText="Time"
                            UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                            SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>