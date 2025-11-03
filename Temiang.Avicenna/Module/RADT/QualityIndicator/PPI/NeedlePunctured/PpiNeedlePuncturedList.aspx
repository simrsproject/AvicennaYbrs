<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="PpiNeedlePuncturedList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiNeedlePuncturedList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "PpiNeedlePuncturedDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="OfficerName" HeaderText="Officer Name"
                    UniqueName="OfficerName" SortExpression="OfficerName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DatePunctured"
                    HeaderText="Incident Date" UniqueName="DatePunctured" SortExpression="DatePunctured"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />    
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="PuncturedAreas" HeaderText="Exposed Areas"
                    UniqueName="PuncturedAreas" SortExpression="PuncturedAreas" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="CausePunctured" HeaderText="Reason" UniqueName="CausePunctured"
                    SortExpression="CausePunctured" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVerified" HeaderText="Follow Up"
                    UniqueName="IsVerified" SortExpression="IsVerified" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="FollowUpDate" HeaderText="Follow Up Date"
                    UniqueName="FollowUpDate" SortExpression="FollowUpDate" DataType="System.DateTime"
                    DataFormatString="{0:MM/dd/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FollowUpBy" HeaderText="Follow Up By" UniqueName="FollowUpBy"
                    SortExpression="FollowUpBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>