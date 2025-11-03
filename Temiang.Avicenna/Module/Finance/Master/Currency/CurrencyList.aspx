<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrencyList.aspx.cs" MasterPageFile="~/MasterPage/MasterList.Master" Inherits="Temiang.Avicenna.Module.Finance.Master.CurrencyList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="CurrencyID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CurrencyID" HeaderText="Currency ID"
                    UniqueName="CurrencyID" SortExpression="CurrencyID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CurrencyName" HeaderText="Currency Name"
                    UniqueName="CurrencyName" SortExpression="CurrencyName">
                </telerik:GridBoundColumn>
                 <telerik:GridNumericColumn DataField="CurrencyRate" HeaderText="Currency Rate"
                    UniqueName="CurrencyRate" SortExpression="CurrencyRate" DataFormatString="{0:n2}" >
                </telerik:GridNumericColumn>
           </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
