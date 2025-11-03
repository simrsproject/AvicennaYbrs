<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="QuestionFormInServiceUnitDetail2.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QuestionFormInServiceUnitDetail2" %>

<%@ Register Src="~/CustomControl/MatrixCtl.ascx" TagName="Matrix" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitName" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdQuestionForm" runat="server" AutoGenerateColumns="false"
        AllowMultiRowSelection="true" OnNeedDataSource="grdQuestionForm_NeedDataSource">
        <MasterTableView DataKeyNames="QuestionFormID,IsSelect">
            <Columns>
                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="50px" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionFormID" HeaderText="ID"
                    UniqueName="QuestionFormID" SortExpression="QuestionFormID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form Name" UniqueName="QuestionFormName"
                    SortExpression="QuestionFormName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RmNO" HeaderText="RM No"
                    UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
