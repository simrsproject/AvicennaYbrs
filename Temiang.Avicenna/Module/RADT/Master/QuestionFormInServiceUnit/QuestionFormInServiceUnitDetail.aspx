<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="QuestionFormInServiceUnitDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QuestionFormInServiceUnitDetail" %>

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
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="True" />
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
    <uc1:Matrix ID="ctlMatrix" runat="server" EntityClassNameMatrix="QuestionFormInServiceUnit"
        EntityClassNameSelection="QuestionForm" FieldNameLinkToHeaderTable="ServiceUnitID"
        FieldNameLinkToSelectionTable="QuestionFormID" FieldNameDisplayInSelectionTable="QuestionFormName"
        FieldNameValueInSelectionTable="QuestionFormID" LinkTextBoxToHeader="txtServiceUnitID"
        MatrixContainFieldRowIndex="False"></uc1:Matrix>
</asp:Content>
