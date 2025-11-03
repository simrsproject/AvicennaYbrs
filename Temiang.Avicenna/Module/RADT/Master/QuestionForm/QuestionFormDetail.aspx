<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="QuestionFormDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QuestionFormDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">
                <asp:Label ID="lblQuestionFormID" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtQuestionFormID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvQuestionFormID" runat="server" ErrorMessage="QuestionForm ID required."
                    ValidationGroup="entry" ControlToValidate="txtQuestionFormID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblQuestionFormName" runat="server" Text="Question Form Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtQuestionFormName" runat="server" Width="300px" TextMode="MultiLine" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvQuestionFormName" runat="server" ErrorMessage="Question Form Name required."
                    ValidationGroup="entry" ControlToValidate="txtQuestionFormName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="RM No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRMNo" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRAutoNumber" runat="server" Text="Auto Number Code (for Generate Letter No)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSRAutoNumber" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td width="20"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsSingleEntry" runat="server" Text="Single Entry" />
            </td>
            <td width="20"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsSharingEdit" runat="server" Text="Sharing Edit" />
            </td>
            <td width="20"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsUsingApproval" runat="server" Text="Using Approval" />
            </td>
            <td width="20"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsAskepForm" runat="server" Text="Nursing Care Form" />
            </td>
            <td width="20"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsModeMapping" runat="server" Text="Mode Mapping (Nursing Diagnosis)" />
            </td>
            <td width="20"></td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Restriction User Type" PageViewID="pgvRestriction" Selected="true" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvRestriction" runat="server">
            <telerik:RadGrid ID="grdUserType" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdUserType_NeedDataSource">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                    Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemName" HeaderText="User Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

</asp:Content>
