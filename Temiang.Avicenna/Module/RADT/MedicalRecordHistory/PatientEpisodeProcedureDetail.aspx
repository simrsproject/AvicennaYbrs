<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="PatientEpisodeProcedureDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientEpisodeProcedureDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    OnDetailTableDataBind="grdList_DetailTableDataBind" AutoGenerateColumns="false">
                    <MasterTableView DataKeyNames="RegistrationNo" GroupLoadMode="client">
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
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="RegistrationDate"
                                HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PatientID" HeaderText="Patient ID"
                                UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                                SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID" SortExpression="BedID"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="RegistrationNo, SequenceNo" Name="grdDetail"
                                AutoGenerateColumns="False" AllowPaging="true" PageSize="10">
                                <Columns>
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
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>
                    <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
