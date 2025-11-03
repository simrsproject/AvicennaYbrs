<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="PatientSOAPEDetailItem.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientSOAPEDetailItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">           
            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
            }
            
            function onClientClose(oWnd) {
            alert('test');
            }
            
            function onClientTabSelected(sender, eventArgs) {
            var tabIndex = eventArgs.get_tab().get_index();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close, Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="onClientClose">
        <Windows>
            <telerik:RadWindow ID="winProcess" Width="1000px" Height="450px" runat="server">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    ShowBaseLine="true" OnClientTabSelected="onClientTabSelected">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Episode SOAPE" PageViewID="pgSOAPE" Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Episode Diagnosis" PageViewID="pgDiagnose">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Episode Procedure" PageViewID="pgProcedure">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
                    BorderColor="Gray">
                    <telerik:RadPageView ID="pgSOAPE" runat="server">
                        <telerik:RadGrid ID="grdEpisodeSOAPE" runat="server" OnNeedDataSource="grdEpisodeSOAPE_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnItemCreated="grdEpisodeSOAPE_ItemCreated">
                            <HeaderContextMenu>
                                
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                        SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="SOAPEDate" HeaderText="Date"
                                        UniqueName="SOAPEDate" SortExpression="SOAPEDate" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="60" DataField="SOAPETime" HeaderText="Time"
                                        UniqueName="SOAPETime" SortExpression="SOAPETime" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="Subjective" HeaderText="Subjective" UniqueName="Subjective"
                                        SortExpression="Subjective" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Objective" HeaderText="Objective" UniqueName="Objective"
                                        SortExpression="Objective" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Assesment" HeaderText="Assesment" UniqueName="Assesment"
                                        SortExpression="Assesment" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Planning" HeaderText="Planning" UniqueName="Planning"
                                        SortExpression="Planning" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Evaluation" HeaderText="Evaluation" UniqueName="Evaluation"
                                        SortExpression="Evaluation" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsSummary" HeaderText="Summary"
                                        UniqueName="IsSummary" SortExpression="IsSummary" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid" HeaderText="Void"
                                        UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                                
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgDiagnose" runat="server">
                        <telerik:RadGrid ID="grdEpisodeDiagnose" runat="server" OnNeedDataSource="grdEpisodeDiagnose_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnItemCreated="grdEpisodeDiagnose_ItemCreated">
                            <HeaderContextMenu>
                                
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="DiagnoseID" HeaderText="Diagnosis ID"
                                        UniqueName="DiagnoseID" SortExpression="DiagnoseID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnosis Name" UniqueName="DiagnoseName"
                                        SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="DiagnoseType" HeaderText="Diagnosis Type" UniqueName="DiagnoseType"
                                        SortExpression="DiagnoseType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                        SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsChronicDisease"
                                        HeaderText="Chronic" UniqueName="IsChronicDisease" SortExpression="IsChronicDisease"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsOldCase" HeaderText="Old Case"
                                        UniqueName="IsOldCase" SortExpression="IsOldCase" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsConfirmed" HeaderText="Confirmed"
                                        UniqueName="IsConfirmed" SortExpression="IsConfirmed" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid" HeaderText="Void"
                                        UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                                
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgProcedure" runat="server">
                        <telerik:RadGrid ID="grdEpisodeProcedure" runat="server" OnNeedDataSource="grdEpisodeProcedure_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None">
                            <HeaderContextMenu>
                                
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="ProcedureDate" HeaderText="Date"
                                        UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ProcedureTime" HeaderText="Time"
                                        UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                        SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                                        SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                                
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>
