<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="QuestionFormList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QuestionFormList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="QuestionFormID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionFormID" HeaderText="ID"
                    UniqueName="QuestionFormID" SortExpression="QuestionFormID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form Name"
                    UniqueName="QuestionFormName" SortExpression="QuestionFormName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RmNO" HeaderText="RM No"
                    UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsSingleEntry" HeaderText="Single Entry"
                    UniqueName="IsSingleEntry" SortExpression="IsSingleEntry" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsSharingEdit" HeaderText="Sharing Edit"
                    UniqueName="IsSharingEdit" SortExpression="IsSharingEdit" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsUsingApproval" HeaderText="Using Approval"
                    UniqueName="IsUsingApproval" SortExpression="IsUsingApproval" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAskepForm" HeaderText="Nursing Care Form"
                    UniqueName="IsAskepForm" SortExpression="IsAskepForm" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsModeMapping" HeaderText="Mode Mapping"
                    UniqueName="IsModeMapping" SortExpression="IsModeMapping" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="RestrictionUserType" HeaderText="Restriction User Type"
                    UniqueName="RestrictionUserType" SortExpression="RestrictionUserType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
