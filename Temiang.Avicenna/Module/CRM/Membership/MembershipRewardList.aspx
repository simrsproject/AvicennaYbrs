<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="MembershipRewardList.aspx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipRewardList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" AllowSorting="False">
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo" GroupLoadMode="Client">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                                UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                                UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PaymentTime" HeaderText="Payment Time"
                                UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                                SortExpression="MedicalNo">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                SortExpression="PatientName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"/>
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"/>
                            <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="TotalPaymentAmount"
                                HeaderText="Total Payment" UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RewardPoint"
                                HeaderText="Reward Points" UniqueName="RewardPoint" SortExpression="RewardPoint"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="True">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
