<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
CodeBehind="RegistrationQuestionFormCheckList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.RegistrationQuestionFormCheckList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
                     ShowHeader="false" GridLines="None" 
                     onitemdatabound="grdList_ItemDataBound">
        <MasterTableView DataKeyNames="QuestionFormID">
            <Columns>
                <telerik:GridBoundColumn DataField="QuestionFormID" HeaderText="Form Check List" UniqueName="QuestionFormID"
                                         SortExpression="QuestionFormID" Visible="false">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Form Check List" UniqueName="QuestionFormName"
                                         SortExpression="QuestionFormName">
                    <HeaderStyle HorizontalAlign="Center" Width="90%" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:CheckBox ID="IsAttached" runat="server" AutoPostBack="true" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings Selecting-AllowRowSelect="true">
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>