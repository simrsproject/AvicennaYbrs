<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientHealthRecordDetailList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientHealthRecordDetailList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript">
        function gotoViewUrl(regno, sno) {
            var url = 'PatientHealthRecordDetailListItem.aspx?md=view&regno=' + regno + '&sno=' + sno;
            window.location.href = url;
        }
    </script>

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
                    <MasterTableView DataKeyNames="RegistrationNo" GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="ItemName" HeaderText="Triage "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="ItemName" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PatientID" HeaderText="Patient ID"
                                UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                                UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                Visible="False" />
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Gender"
                                UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                Visible="False" />
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" HeaderStyle-Width="300px"
                                UniqueName="RoomName" SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="RegistrationNo, SequenceNo" Name="grdDetail"
                                AutoGenerateColumns="False" AllowPaging="true" PageSize="10">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                                        <ItemTemplate>
                                            <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>", 
                                                DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="RecordDate" HeaderText="Exam Date"
                                        UniqueName="RecordDate" SortExpression="RecordDate" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="RecordTime" HeaderText="Exam Time"
                                        UniqueName="RecordTime" SortExpression="RecordTime" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Examiner Name" UniqueName="EmployeeName"
                                        SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
