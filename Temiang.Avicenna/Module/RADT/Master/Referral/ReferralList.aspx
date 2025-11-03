<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="ReferralList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ReferralList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ReferralID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ReferralID" HeaderText="Referral ID"
                    UniqueName="ReferralID" SortExpression="ReferralID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ReferralName" HeaderText="Referral Name" UniqueName="ReferralName"
                    SortExpression="ReferralName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SRReferralGroup" HeaderText="Referral Group"
                    UniqueName="SRReferralGroup" SortExpression="SRReferralGroup" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="StreetName" HeaderText="Address"
                    UniqueName="StreetName" SortExpression="StreetName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="City" HeaderText="City"
                    UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsRefferalFrom" HeaderText="Referral From"
                    UniqueName="IsRefferalFrom" SortExpression="IsRefferalFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsRefferalTo" HeaderText="Referral To"
                    UniqueName="IsRefferalTo" SortExpression="IsRefferalTo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText=" Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
