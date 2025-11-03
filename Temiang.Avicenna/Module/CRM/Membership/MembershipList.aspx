<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="MembershipList.aspx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "MembershipDetail.aspx?md=new&t=" + type;
                window.location.href = url;
            }
            function openPaymentList(id) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("MembershipRewardList.aspx?mid=" + id);
                oWnd.Show();
                oWnd.Maximize();
            }
            function openClaimedList(id) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("MembershipRedeemList.aspx?mid=" + id);
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" >
    </telerik:RadWindow>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="MembershipNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="MembershipNo" HeaderText="Member No"
                    UniqueName="MembershipNo" SortExpression="MembershipNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="JoinDate" HeaderText="Join Date" UniqueName="JoinDate"
                    SortExpression="JoinDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MembershipTypeName" HeaderText="Membership Type" UniqueName="MembershipTypeName"
                    SortExpression="MembershipTypeName" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EmployeeNumber" HeaderText="Employee No" UniqueName="EmployeeNumber"
                    SortExpression="EmployeeNumber">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="275px" DataField="MemberName" HeaderText="Member Name" UniqueName="MemberName"
                    SortExpression="MemberName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "MemberName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DateOfBirth" HeaderText="Date Of Birth" UniqueName="DateOfBirth"
                    SortExpression="DateOfBirth" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                    SortExpression="Address">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                    SortExpression="PhoneNo">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile Phone No" UniqueName="MobilePhoneNo"
                    SortExpression="MobilePhoneNo">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email"
                    SortExpression="Email">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsActive" HeaderText="Active" UniqueName="IsActive"
                    SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="PatientID" Name="grdListMember" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName" HeaderStyle-Width="275px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DateOfBirth" HeaderText="Date Of Birth" UniqueName="DateOfBirth"
                            SortExpression="DateOfBirth" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                            SortExpression="PhoneNo">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile Phone No" UniqueName="MobilePhoneNo"
                            SortExpression="MobilePhoneNo">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email"
                            SortExpression="Email">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsActive" HeaderText="Active" UniqueName="IsActive"
                            SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </telerik:GridTableView>
                <telerik:GridTableView DataKeyNames="MembershipDetailID" Name="grdListDetail" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="StartDate" HeaderText="Active Date" UniqueName="StartDate"
                            SortExpression="StartDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EndDate" HeaderText="Valid Thru" UniqueName="EndDate"
                            SortExpression="EndDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="RewardPoint" HeaderText="Reward Points"
                            UniqueName="RewardPoint" SortExpression="RewardPoint" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="ClaimedPoint" HeaderText="Redeemed Points"
                            UniqueName="ClaimedPoint" SortExpression="ClaimedPoint" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridTemplateColumn UniqueName="reward">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsEmployee").Equals(true) ? string.Empty: string.Format("<a href=\"#\" onclick=\"openPaymentList('{0}'); return false;\"><img src=\"../../../Images/rp16.png\" border=\"0\" title=\"Payment Transaction List\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID")))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="claimed">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsEmployee").Equals(true) ? string.Empty : string.Format("<a href=\"#\" onclick=\"openClaimedList('{0}'); return false;\"><img src=\"../../../Images/redeem16.png\" border=\"0\" title=\"Redeemed List\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID")))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
