<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AwardList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.AwardList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="AwardID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AwardID" HeaderText="Award ID"
                    UniqueName="AwardID" SortExpression="AwardID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AwardCode" HeaderText="Award Code"
                    UniqueName="AwardCode" SortExpression="AwardCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="AwardName" HeaderText="Award Name"
                    UniqueName="AwardName" SortExpression="AwardName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRAwardCriteria" HeaderText="Award Criteria"
                    UniqueName="SRAwardCriteria" SortExpression="SRAwardCriteria" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRAwardType" HeaderText="Award Type"
                    UniqueName="SRAwardType" SortExpression="SRAwardType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="AwardPrize" HeaderText="Award Prize"
                    UniqueName="AwardPrize" SortExpression="AwardPrize" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Note" HeaderText="Notes"
                    UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
