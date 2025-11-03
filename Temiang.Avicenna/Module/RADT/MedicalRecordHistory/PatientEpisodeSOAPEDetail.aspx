<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="PatientEpisodeSOAPEDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientEpisodeSOAPEDetail" %>

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
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                                HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="RegistrationTime" HeaderText="Time"
                                UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />    
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PatientID" HeaderText="Patient ID"
                                UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Unit" UniqueName="ServiceUnitName"
                                SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />    
                            <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                                SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                                SortExpression="BedID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="RegistrationNo, SequenceNo" Name="grdDetail"
                                AutoGenerateColumns="False" AllowPaging="true" PageSize="10">
                                <Columns>
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
                                        SortExpression="Evaluation" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        Visible="false" />
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
