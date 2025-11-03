<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="StandardReferencePerGroupList.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Setting.StandardReferencePerGroupList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(gr) {
                var url = "StandardReferencePerGroupDetail.aspx?md=new&gr=" + gr;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="StandardReferenceID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="StandardReferenceID" HeaderText="ID"
                    UniqueName="StandardReferenceID" SortExpression="StandardReferenceID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StandardReferenceName" HeaderText="Description" UniqueName="StandardReferenceName"
                    SortExpression="StandardReferenceName">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn/>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>