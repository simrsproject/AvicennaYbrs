<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmbalaceList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.EmbalaceList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="EmbalaceID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EmbalaceID" HeaderText="Embalace ID"
                    UniqueName="EmbalaceID" SortExpression="EmbalaceID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmbalaceName" HeaderText="Embalace Name"
                    UniqueName="EmbalaceName" SortExpression="EmbalaceName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmbalaceLabel" HeaderText="Initial"
                    UniqueName="EmbalaceLabel" SortExpression="EmbalaceLabel" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EmbalaceFeeAmount" HeaderText="Fee Amount"
                    UniqueName="EmbalaceFeeAmount" SortExpression="EmbalaceFeeAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
