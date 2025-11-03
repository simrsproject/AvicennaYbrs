<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AssetMovementList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.AssetMovementList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "AssetMovementDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="AssetMovementNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="AssetMovementNo" HeaderText="Transaction No"
                    UniqueName="AssetMovementNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="MovementDate" HeaderText="Date"
                    UniqueName="MovementDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset" UniqueName="AssetName" />
                <telerik:GridBoundColumn DataField="FromServiceUnitName" HeaderText="From Service Unit"
                    UniqueName="FromServiceUnitName" />
                <telerik:GridBoundColumn DataField="FromLocationName" HeaderText="From Room"
                    UniqueName="FromLocationName" />
                <telerik:GridBoundColumn DataField="ToServiceUnitName" HeaderText="To Service Unit"
                    UniqueName="ToServiceUnitName" />
                <telerik:GridBoundColumn DataField="ToLocationName" HeaderText="To Room" UniqueName="ToLocationName" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="75px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="75px" DataField="IsPosted" HeaderText="Posted"
                    UniqueName="IsPosted" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
