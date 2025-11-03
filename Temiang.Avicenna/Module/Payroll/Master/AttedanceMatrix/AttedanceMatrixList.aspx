<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AttedanceMatrixList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.AttedanceMatrixList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="AttedanceMatrixID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AttedanceMatrixID"
                    HeaderText="Attedance Matrix ID" UniqueName="AttedanceMatrixID" SortExpression="AttedanceMatrixID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="AttedanceMatrixName"
                    HeaderText="Attedance Matrix Name" UniqueName="AttedanceMatrixName" SortExpression="AttedanceMatrixName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="AttedanceMatrixFieldt"
                    HeaderText="Attedance Matrix Field" UniqueName="AttedanceMatrixFieldt" SortExpression="AttedanceMatrixFieldt"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
